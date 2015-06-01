using NUnit.Framework;

namespace MsgPackSlim.Formats
{
    [TestFixture]
    public class BinaryFormatTests : TestBase
    {
        private BinaryFormat Format { get; set; }

        private const byte Binary8FormatByte = 0xc4;
        private const byte Binary16FormatByte = 0xc5;
        private const byte Binary32FormatByte = 0xc6;

        private static readonly byte[] TestBytes = GetBytes(0xa8, 0x55, 0x55, 0x44, 0x44, 0x4c, 0x52, 0x4c, 0x52);

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Format = new BinaryFormat();
        }

        [Test]
        public void ReadValueInfo_ForBinary8_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12))
            {
                var actual = Format.ReadValueInfo(Binary8FormatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(1), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x12), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForBinary8_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff))
            {
                var actual = Format.ReadValueInfo(Binary8FormatByte, stream);

                Assert.That(actual.ContentSize, Is.EqualTo(0xff), "ContentSize");
            }
        }

        [Test]
        public void GetValue_ForBinary8_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(1, TestBytes.Length);

            var actual = Format.GetValue(Binary8FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestBytes));
        }

        [Test]
        public void GetValue_ForBinary8_WithEmptyBytes_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(1, 0);

            var actual = Format.GetValue(Binary8FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestBytes));
        }

        [Test]
        public void ReadValueInfo_ForBinary16_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, 0x34))
            {
                var actual = Format.ReadValueInfo(Binary16FormatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x1234), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForBinary16_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff, 0xff))
            {
                var actual = Format.ReadValueInfo(Binary16FormatByte, stream);

                Assert.That(actual.ContentSize, Is.EqualTo(0xffff), "ContentSize");
            }
        }

        [Test]
        public void GetValue_ForBinary16_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(2, TestBytes.Length);

            var actual = Format.GetValue(Binary16FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestBytes));
        }

        [Test]
        public void ReadValueInfo_ForBinary32_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, 0x34, 0x56, 0x78))
            {
                var actual = Format.ReadValueInfo(Binary32FormatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(4), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x12345678), "ContentSize");
            }
        }

        [Test]
        public void GetValue_ForBinary32_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(4, TestBytes.Length);

            var actual = Format.GetValue(Binary32FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestBytes));
        }
    }
}
