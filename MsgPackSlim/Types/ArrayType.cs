using System.IO;

namespace MsgPackSlim.Types
{
    public class ArrayType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x90, 4);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xdc, 1);
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            throw new System.NotImplementedException();
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}
