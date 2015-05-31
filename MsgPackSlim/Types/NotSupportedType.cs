using System;

namespace MsgPackSlim.Types
{
    public class NotSupportedType : IMsgPackType
    {
        public void Register(IMsgPackType[] formatMap)
        {
            throw new NotSupportedException();
        }
    }
}
