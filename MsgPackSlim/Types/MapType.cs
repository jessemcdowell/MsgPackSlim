using System.IO;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Types
{
    public class MapType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x80, 4);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xde, 1);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            if ((formatByte & 0xf0) == 0x80)
                return ValueInfo.ForChildContainer(0, (formatByte & 0x0f) * 2);

            int headerSize;
            switch (formatByte)
            {
                case 0xde:
                    headerSize = 2;
                    break;
                case 0xdf:
                    headerSize = 4;
                    break;
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }

            var childCount = NumericParser.ReadInt32(stream, headerSize) * 2;
            return ValueInfo.ForChildContainer(headerSize, childCount);
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            return null;
        }
    }
}
