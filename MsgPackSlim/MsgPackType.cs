using System;

namespace MsgPackSlim
{
    public static class MsgPackType
    {
        public static readonly IMsgPackType[] TypeMap = new IMsgPackType[256];

        public static readonly UnsupportedType Unsupported = new UnsupportedType();

        static MsgPackType()
        {
            for (var i = 0; i < 256; i++)
            {
                if (TypeMap[i] == null)
                    TypeMap[i] = Unsupported;
            }
        }

    }
}
