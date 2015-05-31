using System.IO;

namespace MsgPackSlim.Parsers
{
    public static class NumericParser
    {
        public static int ReadInt32(Stream stream, int count)
        {
            var offset = 4 - count;
            var buffer = new byte[4];

            var bytesRead = stream.Read(buffer, offset, count);
            if (bytesRead < count)
                throw Exceptions.UnexpectedEnd();

            return NumericConverter.ToInt32(buffer);
        }
    }
}
