using System;

namespace MsgPackSlim.Parsers
{
    public static class NumericConverter
    {
        private static readonly bool RequiresEndianConversion;

        static NumericConverter()
        {
            var input = new byte[] {0, 0, 0, 1};
            RequiresEndianConversion = (ToInt32(input) != 1);
        }

        public static int ToInt32(byte[] bytes)
        {
            var value = BitConverter.ToInt32(bytes, 0);
            ConvertByteOrder(ref value);

            return value;
        }

        private static void ConvertByteOrder(ref int value)
        {
            if (!RequiresEndianConversion)
                return;
            value =
                ((value >> 24) & 0xff) |
                ((value >> 08) & 0xff00) |
                ((value << 08) & 0xff0000) |
                (value << 24);
        }

        public static uint ToUInt32(byte[] bytes)
        {
            return (uint)ToInt32(bytes);
        }

        public static long ToInt64(byte[] bytes)
        {
            var value = BitConverter.ToInt64(bytes, 0);
            ConvertByteOrder(ref value);
            return value;
        }

        private static void ConvertByteOrder(ref long value)
        {
            if (!RequiresEndianConversion)
                return;
            value =
                ((value >> 56) & 0xff) |
                ((value >> 40) & 0xff00) |
                ((value >> 24) & 0xff0000) |
                ((value >> 08) & 0xff000000) |
                ((value << 08) & 0xff00000000) |
                ((value << 24) & 0xff0000000000) |
                ((value << 40) & 0xff000000000000) |
                (value << 56);
        }

        public static ulong ToUInt64(byte[] bytes)
        {
            return (ulong) ToInt64(bytes);
        }
    }
}
