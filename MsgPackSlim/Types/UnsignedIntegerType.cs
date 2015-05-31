using System.IO;

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
            throw new System.NotImplementedException();
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            throw new System.NotImplementedException();
        }
    }
}
