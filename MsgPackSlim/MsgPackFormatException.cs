using System;

namespace MsgPackSlim
{
    public class MsgPackFormatException : Exception
    {
        public MsgPackFormatException(string message)
            : base(message)
        { }
    }
}
