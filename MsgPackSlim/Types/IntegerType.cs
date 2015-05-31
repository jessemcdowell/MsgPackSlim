namespace MsgPackSlim.Types
{
    public class IntegerType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x00, 7);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xd0, 2);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xe0, 5);
        }
    }
}
