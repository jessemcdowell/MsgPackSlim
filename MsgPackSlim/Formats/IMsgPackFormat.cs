using System.IO;

namespace MsgPackSlim.Formats
{
    public interface IMsgPackFormat
    {
        void Register(IMsgPackFormat[] formatMap);

        ValueInfo ReadValueInfo(byte formatByte, Stream stream);

        object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes);
    }
}
