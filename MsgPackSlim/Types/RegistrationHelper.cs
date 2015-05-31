using System;

namespace MsgPackSlim.Types
{
    public static class RegistrationHelper
    {
        public static void RegisterBitMask(IMsgPackType[] formatMap, IMsgPackType type, byte baseValue, int bitLength)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            var count = 1 << bitLength;
            if (((count - 1) & baseValue) != 0)
                throw new ArgumentException("baseValue must have no bits set in the variable bit region", "baseValue");

            for (var i = 0; i < count; i++)
                formatMap[baseValue | i] = type;
        }
    }
}
