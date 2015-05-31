namespace MsgPackSlim.Types
{
    public class NilType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            formatMap[0xc0] = this;
        }
    }
}
