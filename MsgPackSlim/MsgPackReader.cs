using System;
using System.IO;
using MsgPackSlim.Formats;

namespace MsgPackSlim
{
    public class MsgPackReader : IDisposable
    {
        private readonly Stream _stream;
        private readonly bool _disposeStream;
        private readonly IMsgPackFormat[] _formatMap;

        public byte FormatByte { get; private set; }
        public IMsgPackFormat Format { get; private set; }
        public byte[] ContentBytes { get; private set; }

        private ValueInfo _valueInfo;
        private readonly byte[] _smallBuffer = new byte[4];

        public MsgPackReader(Stream stream, bool disposeStream = true, IMsgPackFormat[] formatMap = null)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            _stream = stream;
            _disposeStream = disposeStream;
            _formatMap = formatMap ?? MsgPackFormat.FormatMap;
        }

        ~MsgPackReader()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_disposeStream)
                    _stream.Dispose();
                ResetValues();
            }
        }

        public bool ReadNext()
        {
            ResetValues();

            if (!ReadFormatByte())
                return false;
            Format = _formatMap[FormatByte];

            _valueInfo = Format.ReadValueInfo(FormatByte, _stream);

            if (_valueInfo.ContentSize < 0)
                throw new NotSupportedException("Error loading value data: content size not supported");
            if (_valueInfo.ContentSize > 4)
                ReadContent();
            else if (_valueInfo.ContentSize > 0)
                ReadSmallContent();

            return true;
        }

        private void ResetValues()
        {
            Format = null;
            _valueInfo = null;
            ContentBytes = null;
        }

        private bool ReadFormatByte()
        {
            var formatByte = _stream.ReadByte();
            if (formatByte == -1)
            {
                FormatByte = 0;
                return false;
            }

            FormatByte = (byte) formatByte;
            return true;
        }

        private void ReadContent()
        {
            var bytes = new byte[_valueInfo.ContentSize];
            ReadContent(bytes, 0, _valueInfo.ContentSize);
        }

        private void ReadContent(byte[] buffer, int offset, int count)
        {
            var bytesRead = _stream.Read(buffer, offset, count);
            if (bytesRead != _valueInfo.ContentSize)
                throw Exceptions.UnexpectedEnd();
            ContentBytes = buffer;
        }

        private void ReadSmallContent()
        {
            var startIndex = 4 - _valueInfo.ContentSize;
            for (var i = 0; i < startIndex; i++)
                _smallBuffer[i] = 0;
            ReadContent(_smallBuffer, startIndex, _valueInfo.ContentSize);
        }

        public int HeaderSize
        {
            get { return _valueInfo.HeaderSize; }
        }

        public int ContentSize
        {
            get { return _valueInfo.ContentSize; }
        }

        public int TotalSize
        {
            get { return _valueInfo.HeaderSize + _valueInfo.ContentSize + 1; }
        }

        public int ChildObjectCount
        {
            get { return _valueInfo.ChildObjectCount; }
        }

        public bool IsMap
        {
            get { return Format is MapFormat; }
        }

        public bool IsArray
        {
            get { return Format is ArrayFormat; }
        }

        public bool IsString
        {
            get { return Format is StringFormat; }
        }

        public object GetValue()
        {
            if (_valueInfo == null)
                throw new InvalidOperationException("There is no current value");
            return Format.GetValue(FormatByte, _valueInfo, ContentBytes);
        }
    }
}
