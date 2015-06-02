using System.IO;
using System.Text;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Formats
{
    public class StringFormat : IMsgPackFormat
    {
        private static readonly UTF8Encoding Encoding = new UTF8Encoding(false);

        public void Register(IMsgPackFormat[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xa0, 5);
            formatMap[0xd9] = this;
            formatMap[0xda] = this;
            formatMap[0xdb] = this;
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            if ((formatByte & 0xe0) == 0xa0)
                return ValueInfo.ForHeaderlessContent(formatByte & 0x1f);

            switch (formatByte)
            {
                case 0xd9:
                    return GetValueInfo(stream, 1);

                case 0xda:
                    return GetValueInfo(stream, 2);

                case 0xdb:
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
            if (valueInfo.ContentSize == 0)
                return "";

            return Encoding.GetString(contentBytes);
        }
    }
}
