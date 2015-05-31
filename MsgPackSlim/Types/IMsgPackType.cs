using System.IO;

namespace MsgPackSlim.Types
{
    public interface IMsgPackType
    {
        void Register(IMsgPackType[] formatMap);

        ValueInfo ReadValueInfo(byte formatByte, Stream stream);

        object GetValue(byte formatByte, ValueInfo valueInfo, byte[] contentBytes);
    }
}
