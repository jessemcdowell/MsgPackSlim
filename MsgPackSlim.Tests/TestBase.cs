using System.IO;

namespace MsgPackSlim
{
    public abstract class TestBase
    {
        protected static MemoryStream GetStream(params byte[] bytes)
        {
            return new MemoryStream(bytes);
        }

        protected static byte[] GetBytes(params byte[] bytes)
        {
            return bytes;
        }
    }
}
