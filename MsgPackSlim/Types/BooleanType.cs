using System.IO;

namespace MsgPackSlim.Types
{
    public class BooleanType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xc2, 1);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            return ValueInfo.TypeOnlyValue;
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            return formatByte == 0xc3;
        }
    }
}
