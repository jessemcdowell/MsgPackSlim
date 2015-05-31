namespace MsgPackSlim.Types
{
    public class MapType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x80, 4);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xde, 1);
        }
    }
}
