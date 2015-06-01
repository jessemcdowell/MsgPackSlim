using MsgPackSlim.Formats;

namespace MsgPackSlim
{
    public static class MsgPackFormat
    {
        public static readonly IMsgPackFormat[] FormatMap = new IMsgPackFormat[256];

        public static readonly NotSupportedFormat NotSupported = new NotSupportedFormat();
        public static readonly IntegerFormat Integer = Register(new IntegerFormat());
        public static readonly MapFormat Map = Register(new MapFormat());
        public static readonly ArrayFormat Array = Register(new ArrayFormat());
        public static readonly StringFormat String = Register(new StringFormat());
        public static readonly NilFormat Nil = Register(new NilFormat());
        public static readonly BooleanFormat Boolean = Register(new BooleanFormat());
        public static readonly BinaryFormat Binary = Register(new BinaryFormat());

        //ext 8 11000111 0xc7
        //ext 16 11001000 0xc8
        //ext 32 11001001 0xc9

        public static readonly FloatingPointFormat FloatingPoint = Register(new FloatingPointFormat());
        public static readonly UnsignedIntegerFormat UnsignedInteger = Register(new UnsignedIntegerFormat());

        //fixext 1 11010100 0xd4
        //fixext 2 11010101 0xd5
        //fixext 4 11010110 0xd6
        //fixext 8 11010111 0xd7
        //fixext 16 11011000 0xd8

        static MsgPackFormat()
        {
            for (var i = 0; i < 256; i++)
            {
                if (FormatMap[i] == null)
                    FormatMap[i] = NotSupported;
            }
        }

        private static T Register<T>(T type) where T : IMsgPackFormat
        {
            type.Register(FormatMap);
            return type;
        }
    }
}
