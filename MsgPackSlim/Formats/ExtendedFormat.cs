using System.IO;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Formats
{
    public class ExtendedFormat : IMsgPackFormat
    {
        public const byte Fixed1Format = 0xd4;
        public const byte Fixed2Format = 0xd5;
        public const byte Fixed4Format = 0xd6;
        public const byte Fixed8Format = 0xd7;
        public const byte Fixed16Format = 0xd8;
        public const byte Variable8Format = 0xc7;
        public const byte Variable16Format = 0xc8;
        public const byte Variable32Format = 0xc9;

        public void Register(IMsgPackFormat[] formatMap)
        {
            formatMap[Fixed1Format] = this;
            formatMap[Fixed2Format] = this;
            formatMap[Fixed4Format] = this;
            formatMap[Fixed8Format] = this;
            formatMap[Fixed16Format] = this;
            formatMap[Variable8Format] = this;
            formatMap[Variable16Format] = this;
            formatMap[Variable32Format] = this;
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            switch (formatByte)
            {
                case Fixed1Format:
                    return ReadFixedValueInfo(stream, 1);
                case Fixed2Format:
                    return ReadFixedValueInfo(stream, 2);
                case Fixed4Format:
                    return ReadFixedValueInfo(stream, 4);
                case Fixed8Format:
                    return ReadFixedValueInfo(stream, 8);
                case Fixed16Format:
                    return ReadFixedValueInfo(stream, 16);
                case Variable8Format:
                    return ReadVariableValueInfo(stream, 1);
                case Variable16Format:
                    return ReadVariableValueInfo(stream, 2);
                case Variable32Format:
                    return ReadVariableValueInfo(stream, 4);
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
        }

        private ValueInfo ReadFixedValueInfo(Stream stream, int contentSize)
        {
            return ReadExtendedType(stream, 0, contentSize);
        }

        private ValueInfo ReadVariableValueInfo(Stream stream, int contentHeaderSize)
        {
            var contentSize = NumericParser.ReadInt32(stream, contentHeaderSize);
            return ReadExtendedType(stream, contentHeaderSize, contentSize);
        }

        private ValueInfo ReadExtendedType(Stream stream, int contentHeaderSize, int contentSize)
        {
            var extendedType = stream.ReadByte();
            if (extendedType == -1)
                throw Exceptions.UnexpectedEnd();

            return ValueInfo.ForExtendedType(contentHeaderSize + 1, contentSize, (byte)extendedType);
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            return contentBytes;
        }
    }
}
