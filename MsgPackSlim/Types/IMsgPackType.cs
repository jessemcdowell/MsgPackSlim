namespace MsgPackSlim.Types
{
    public interface IMsgPackType
    {
        void Register(IMsgPackType[] formatMap);
    }
}
