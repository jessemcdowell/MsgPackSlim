namespace MsgPackSlim.Types
{
    public class ArrayType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            RegistrationHelper.RegisterBitMask(formatMap, this, 0x90, 4);
            RegistrationHelper.RegisterBitMask(formatMap, this, 0xdc, 1);
        }
    }
}
