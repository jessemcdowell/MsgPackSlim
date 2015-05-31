using System.IO;

namespace MsgPackSlim.Types
{
    public class NilType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            formatMap[0xc0] = this;
        }

        public ValueInfo ReadValueInfo(byte formatByte, Stream stream)
        {
            return ValueInfo.TypeOnlyValue;
        }

        public object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes)
        {
            return null;
        }
    }
}
