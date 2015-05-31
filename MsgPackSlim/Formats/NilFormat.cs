using System.IO;

namespace MsgPackSlim.Formats
{
    public class NilFormat : IMsgPackFormat
    {
        public void Register(IMsgPackFormat[] formatMap)
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
