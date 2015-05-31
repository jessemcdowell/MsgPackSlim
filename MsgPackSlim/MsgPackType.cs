using MsgPackSlim.Types;

namespace MsgPackSlim
{
    public static class MsgPackType
    {
        public static readonly IMsgPackType[] FormatMap = new IMsgPackType[256];

        public static readonly NotSupportedType NotSupported = new NotSupportedType();
        public static readonly IntegerType Integer = Register(new IntegerType());
        public static readonly MapType Map = Register(new MapType());
        public static readonly ArrayType Array = Register(new ArrayType());
        public static readonly StringType String = Register(new StringType());
        public static readonly NilType Nil = Register(new NilType());
        public static readonly BooleanType Boolean = Register(new BooleanType());

        //bin 8 11000100 0xc4
        //bin 16 11000101 0xc5
        //bin 32 11000110 0xc6
        //ext 8 11000111 0xc7
        //ext 16 11001000 0xc8
        //ext 32 11001001 0xc9

        public static readonly FloatingPointType FloatingPoint = Register(new FloatingPointType());
        public static readonly UnsignedIntegerType UnsignedInteger = Register(new UnsignedIntegerType());

        //fixext 1 11010100 0xd4
        //fixext 2 11010101 0xd5
        //fixext 4 11010110 0xd6
        //fixext 8 11010111 0xd7
        //fixext 16 11011000 0xd8

        static MsgPackType()
        {
            for (var i = 0; i < 256; i++)
            {
                if (FormatMap[i] == null)
                    FormatMap[i] = NotSupported;
            }
        }

        private static T Register<T>(T type) where T : IMsgPackType
        {
            type.Register(FormatMap);
            return type;
        }
    }
}
