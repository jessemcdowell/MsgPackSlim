using NUnit.Framework;

namespace MsgPackSlim.Formats
{
    [TestFixture]
    public class StringFormatTests : TestBase
    {
        private StringFormat Format { get; set; }

        private const byte FixStringWithZeroLength = 0xa0;
        private const byte String8FormatByte = 0xd9;
        private const byte String16FormatByte = 0xda;
        private const byte String32FormatByte = 0xdb;

        private const string TestText = "hello world";
        private static readonly byte[] TestBytes = GetBytes(0x68, 0x65, 0x6c, 0x6c, 0x6f, 0x20, 0x77, 0x6f, 0x72, 0x6c, 0x64);

        private const string MultiByteTestText = "こんにちは、";
        private static readonly byte[] MultiByteTestBytes = GetBytes(0xe3, 0x81, 0x93, 0xe3, 0x82, 0x93, 0xe3, 0x81, 0xab,
            0xe3, 0x81, 0xa1, 0xe3, 0x81, 0xaf, 0xe3, 0x80, 0x81);

        private const string LongFixStrText = "this is a long fixstr";
        private static readonly byte[] LongFixStrBytes = GetBytes(0x74, 0x68, 0x69, 0x73, 0x20, 0x69, 0x73, 0x20, 0x61,
            0x20, 0x6c, 0x6f, 0x6e, 0x67, 0x20, 0x66, 0x69, 0x78, 0x73, 0x74, 0x72);

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Format = new StringFormat();
        }

        [Test]
        public void ReadValueInfo_ForFixString_WithZeroLength_ReturnsExpectedValues()
        {
            var actual = Format.ReadValueInfo(FixStringWithZeroLength, null);

            Assert.That(actual.HeaderSize, Is.EqualTo(0), "HeaderSize");
            Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
        }

        [Test]
        public void GetValue_ForFixString_WithLengthZero_ReturnsEmptyString()
        {
            var info = ValueInfo.ForHeaderlessContent(0);

            var actual = Format.GetValue(FixStringWithZeroLength, info, null);

            Assert.That(actual, Is.EqualTo(""));
        }

        [Test]
        public void ReadValueInfo_ForFixString_ReturnsExpectedValues()
        {
            const byte formatByte = 0xa5;

            var actual = Format.ReadValueInfo(formatByte, null);

            Assert.That(actual.HeaderSize, Is.EqualTo(0), "HeaderSize");
            Assert.That(actual.ContentSize, Is.EqualTo(5), "ContentSize");
        }

        [Test]
        public void GetValue_ForFixString_ReturnsExpectedValue()
        {
            const byte formatByte = 0xab;
            var info = ValueInfo.ForHeaderlessContent(LongFixStrBytes.Length);

            var actual = Format.GetValue(formatByte, info, LongFixStrBytes);

            Assert.That(actual, Is.EqualTo(LongFixStrText));
        }


        [Test]
        public void GetValue_ForFixString_ThatIsLong_ReturnsExpectedValue()
        {
            const byte formatByte = 0xb5;
            var info = ValueInfo.ForHeaderlessContent(TestBytes.Length);

            var actual = Format.GetValue(formatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestText));
        }

        [Test]
        public void ReadValueInfo_ForString8_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12))
            {
                var actual = Format.ReadValueInfo(String8FormatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(1), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x12), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForString8_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff))
            {
                var actual = Format.ReadValueInfo(String8FormatByte, stream);

                Assert.That(actual.ContentSize, Is.EqualTo(0xff), "ContentSize");
            }
        }

        [Test]
        public void GetValue_ForString8_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(1, TestBytes.Length);

            var actual = Format.GetValue(String8FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestText));
        }

        [Test]
        public void ReadValueInfo_ForString16_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, 0x34))
            {
                var actual = Format.ReadValueInfo(String16FormatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x1234), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForString16_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff, 0xff))
            {
                var actual = Format.ReadValueInfo(String16FormatByte, stream);

                Assert.That(actual.ContentSize, Is.EqualTo(0xffff), "ContentSize");
            }
        }

        [Test]
        public void GetValue_ForString16_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(2, TestBytes.Length);

            var actual = Format.GetValue(String16FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestText));
        }

        [Test]
        public void ReadValueInfo_ForString32_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, 0x34, 0x56, 0x78))
            {
                var actual = Format.ReadValueInfo(String32FormatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(4), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x12345678), "ContentSize");
            }
        }

        [Test]
        public void GetValue_ForString32_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(4, TestBytes.Length);

            var actual = Format.GetValue(String32FormatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestText));
        }

        [Test]
        public void GetValue_ForMultiByteCharacters_ReturnsExpectedValue()
        {
            var info = ValueInfo.ForContent(2, MultiByteTestBytes.Length);

            var actual = Format.GetValue(String16FormatByte, info, MultiByteTestBytes);

            Assert.That(actual, Is.EqualTo(MultiByteTestText));
        }
    }
}
