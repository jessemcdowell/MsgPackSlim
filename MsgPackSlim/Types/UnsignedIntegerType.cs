using System.IO;
using MsgPackSlim.Parsers;

namespace MsgPackSlim.Types
{
    public class UnsignedIntegerType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xcc, 2);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            switch (formatByte)
            {
                case 0xcc:
                    return ValueInfo.ForHeaderlessContent(1);
                case 0xcd:
                    return ValueInfo.ForHeaderlessContent(2);
                case 0xce:
                    return ValueInfo.ForHeaderlessContent(4);
                case 0xcf:
                    return ValueInfo.ForHeaderlessContent(8);
                default:
                    throw Exceptions.FormatByteNotSupported(formatByte);
            }
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            if (formatByte == 0xcf)
                return NumericConverter.ToUInt64(contentBytes);

            return NumericConverter.ToUInt32(contentBytes);
        }
    }
}
