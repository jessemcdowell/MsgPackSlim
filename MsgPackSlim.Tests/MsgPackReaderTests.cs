using NUnit.Framework;

namespace MsgPackSlim
{
    [TestFixture]
    public class MsgPackReaderTests : TestBase
    {
        [Test]
        public void ParseCanonicalExample()
        {
            // {"compact":true,"schema":0}
            using (var stream = GetStream(
                0x82,
                0xa7, 0x63, 0x6f, 0x6d, 0x70, 0x61, 0x63, 0x74,
                0xc3,
                0xa6, 0x73, 0x63, 0x68, 0x65, 0x6d, 0x61,
                0x00))
            using (var reader = new MsgPackReader(stream))
            {
                Assert.That(reader.ReadNext(), Is.True, "read opening map");
                Assert.That(reader.IsMap, Is.True, "IsMap");
                Assert.That(reader.ContentBytes, Is.Null);
                Assert.That(reader.ChildObjectCount, Is.EqualTo(4));

                Assert.That(reader.ReadNext(), Is.True, "read first key");
                Assert.That(reader.IsString, Is.True, "first key IsString");
                Assert.That(reader.GetValue(), Is.EqualTo("compact"), "first key");

                Assert.That(reader.ReadNext(), Is.True, "read first value");
                Assert.That(reader.GetValue(), Is.True, "first value");

                Assert.That(reader.ReadNext(), Is.True, "read second key");
                Assert.That(reader.IsString, Is.True, "second key IsString");
                Assert.That(reader.GetValue(), Is.EqualTo("schema"), "second key");

                Assert.That(reader.ReadNext(), Is.True, "read second value");
                Assert.That(reader.GetValue(), Is.EqualTo(0), "second value");

                Assert.That(reader.ReadNext(), Is.False, "read end of document");
            }
        }
    }
}
