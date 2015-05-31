namespace MsgPackSlim.Types
{
    public class BooleanType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xc2, 1);
        }
    }
}
