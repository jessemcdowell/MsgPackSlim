using System;

namespace MsgPackSlim.Formats
{
    public static class RegistrationHelper
    {
        public static void RegisterBitMask(IMsgPackFormat[] formatMap, IMsgPackFormat format, byte baseValue, int bitLength)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            var count = 1 << bitLength;
            if (((count - 1) & baseValue) != 0)
                throw new ArgumentException("baseValue must have no bits set in the variable bit region", "baseValue");

            for (var i = 0; i < count; i++)
                formatMap[baseValue | i] = format;
        }
    }
}
