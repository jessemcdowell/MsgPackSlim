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
        public static readonly ExtendedFormat Extended = Register(new ExtendedFormat());
        public static readonly FloatingPointFormat FloatingPoint = Register(new FloatingPointFormat());
        public static readonly UnsignedIntegerFormat UnsignedInteger = Register(new UnsignedIntegerFormat());

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
