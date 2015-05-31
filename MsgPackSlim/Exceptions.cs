using System;

namespace MsgPackSlim
{
    internal static class Exceptions
    {
        internal static Exception UnexpectedEnd()
        {
            return new MsgPackFormatException("Unexpected end to MsgPack data");
        }

        public static Exception FormatByteNotSupported(byte formatByte)
        {
            return new NotSupportedException("Format type not supported: " + formatByte.ToString("x"));
        }
    }
}
