using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class ArrayTypeTests : TestBase
    {
        private ArrayType Type { get; set; }

        private const byte Array16FormatByte = 0xdc;
        private const byte Array32FormatByte = 0xdd;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new ArrayType();
        }

        [Test]
        public void ReadValueInfo_ForFixArray_ReturnsExpectedValues()
        {
            const byte formatByteWithOneItem = 0x91;

            var actual = Type.ReadValueInfo(formatByteWithOneItem, null);

            Assert.That(actual.ChildObjectCount, Is.EqualTo(1), "ChildObjectCount");
            Assert.That(actual.HeaderSize, Is.EqualTo(0), "HeaderSize");
            Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
        }

        [Test]
        public void ReadValueInfo_ForArray16_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x11, 0x22))
            {
                var actual = Type.ReadValueInfo(Array16FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0x1122), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForArray16_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff, 0xff))
            {
                var actual = Type.ReadValueInfo(Array16FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0xffff), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForArray32_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x11, 0x22, 0x33, 0x44))
            {
                var actual = Type.ReadValueInfo(Array32FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0x11223344), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(4), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }
    }
}
