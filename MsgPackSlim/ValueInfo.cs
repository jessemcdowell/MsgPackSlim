namespace MsgPackSlim
{
    public class ValueInfo
    {
        public int HeaderSize { get; private set; }
        public int ContentSize { get; private set; }
        public int ChildObjectCount { get; private set; }
        public byte ExtendedType { get; private set; }

        public static readonly ValueInfo TypeOnlyValue = new ValueInfo(0, 0, 0, 0);

        public static ValueInfo ForChildContainer(int headerSize, int childCount)
        {
            return new ValueInfo(headerSize, 0, childCount, 0);
        }

        public static ValueInfo ForHeaderlessContent(int contentSize)
        {
            return new ValueInfo(0, contentSize, 0, 0);
        }

        public static ValueInfo ForContent(int headerSize, int contentSize)
        {
            return new ValueInfo(headerSize, contentSize, 0, 0);
        }

        public static ValueInfo ForExtendedType(int headerSize, int contentSize, byte extendedType)
        {
            return new ValueInfo(headerSize, contentSize, 0, extendedType);
        }

        private ValueInfo(int headerSize, int contentSize, int childObjectCount, byte extendedType)
        {
            ChildObjectCount = childObjectCount;
            HeaderSize = headerSize;
            ContentSize = contentSize;
            ExtendedType = extendedType;
        }
    }
}
