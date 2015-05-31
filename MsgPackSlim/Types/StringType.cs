namespace MsgPackSlim.Types
{
    public class StringType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xa0, 4);
            formatMap[0xd9] = this;
            formatMap[0xda] = this;
            formatMap[0xdb] = this;
        }
    }
}
