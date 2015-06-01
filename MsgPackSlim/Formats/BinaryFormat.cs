using System.IO;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Formats
{
    public class BinaryFormat : IMsgPackFormat
    {
        public void Register(IMsgPackFormat[] formatMap)
        {
            formatMap[0xc4] = this;
            formatMap[0xc5] = this;
            formatMap[0xc6] = this;
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            switch (formatByte)
            {
                case 0xc4:
                    return GetValueInfo(stream, 1);

                case 0xc5:
                    return GetValueInfo(stream, 2);

                case 0xc6:
                    return GetValueInfo(stream, 4);

                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
        }

        private ValueInfo GetValueInfo(Stream stream, int headerSize)
        {
            var bytes = NumericParser.ReadInt32(stream, headerSize);
            return ValueInfo.ForContent(headerSize, bytes);
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            return contentBytes;
        }
    }
}
