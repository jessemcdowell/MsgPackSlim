namespace MsgPackSlim.Types
{
    public class UnsignedIntegerType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xcc, 2);
        }
    }
}
