using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class MapTypeTests : TestBase
    {
        private MapType Type { get; set; }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new MapType();
        }

        [Test]
        public void ReadValueInfo_ForFixMap_ReturnsExpectedValues()
        {
            const byte formatByteWithOneItem = 0x81;

            var actual = Type.ReadValueInfo(formatByteWithOneItem, null);

            Assert.That(actual.ChildObjectCount, Is.EqualTo(2), "ChildObjectCount");
            Assert.That(actual.HeaderSize, Is.EqualTo(0), "HeaderSize");
            Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
        }

        [Test]
        public void ReadValueInfo_ForMap16_ReturnsExpectedValues()
        {
            const byte map16FormatByte = 0xde;

            using (var stream = GetStream(0x11, 0x22))
            {
                var actual = Type.ReadValueInfo(map16FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0x1122 * 2), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForMap16_WithMaximumLength_ReturnsExpectedValues()
        {
            const byte map16FormatByte = 0xde;

            using (var stream = GetStream(0xff, 0xff))
            {
                var actual = Type.ReadValueInfo(map16FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0xffff * 2), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForMap32_ReturnsExpectedValues()
        {
            const byte map32FormatByte = 0xdf;

            using (var stream = GetStream(0x11, 0x22, 0x33, 0x44))
            {
                var actual = Type.ReadValueInfo(map32FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0x11223344 * 2), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(4), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }
    }
}
