using System.IO;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Formats
{
    public class ArrayFormat : IMsgPackFormat
    {
        public void Register(IMsgPackFormat[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x90, 4);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xdc, 1);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            if ((formatByte & 0xf0) == 0x90)
                return ValueInfo.ForChildContainer(0, formatByte & 0x0f);

            int headerSize;
            switch (formatByte)
            {
                case 0xdc:
                    headerSize = 2;
                    break;
                case 0xdd:
                    headerSize = 4;
                    break;
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }

            var childCount = NumericParser.ReadInt32(stream, headerSize);
            return ValueInfo.ForChildContainer(headerSize, childCount);
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            return null;
        }
    }
}
