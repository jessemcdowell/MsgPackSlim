using NUnit.Framework;

namespace MsgPackSlim.Formats
{
    [TestFixture]
    public class ExtendedFormatTests : TestBase
    {
        private ExtendedFormat Format { get; set; }

        private static readonly byte[] TestBytes = GetBytes(0xa8, 0x55, 0x55, 0x44, 0x44, 0x4c, 0x52, 0x4c, 0x52);

        private const byte TestExtendedType = 0x12;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Format = new ExtendedFormat();
        }

        public static readonly object[][] FixedExtendedFormats =
        {
            new object[] {1,  ExtendedFormat.Fixed1Format},
            new object[] {2,  ExtendedFormat.Fixed2Format},
            new object[] {4,  ExtendedFormat.Fixed4Format},
            new object[] {8,  ExtendedFormat.Fixed8Format},
            new object[] {16, ExtendedFormat.Fixed16Format},
        };

        [TestCaseSource("FixedExtendedFormats")]
        public void ReadValueInfo_ForFixedExtended_ReturnsExpectedValues(int contentSize, byte formatByte)
        {
            using (var stream = GetStream(TestExtendedType))
            {
                var actual = Format.ReadValueInfo(formatByte, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(1), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(contentSize), "ContentSize");
                Assert.That(actual.ExtendedType, Is.EqualTo(TestExtendedType), "ExtendedType");
            }
        }

        public static readonly byte[] AllExtendedFormats =
        {
            ExtendedFormat.Fixed1Format,
            ExtendedFormat.Fixed2Format,
            ExtendedFormat.Fixed4Format,
            ExtendedFormat.Fixed8Format,
            ExtendedFormat.Fixed16Format,
            ExtendedFormat.Variable8Format,
            ExtendedFormat.Variable16Format,
            ExtendedFormat.Variable32Format
        };

        [TestCaseSource("AllExtendedFormats")]
        public void GetValue_ForAllFormats_ReturnsBytesPassedIn(byte formatByte)
        {
            var info = ValueInfo.ForExtendedType(1, TestBytes.Length, TestExtendedType);

            var actual = Format.GetValue(formatByte, info, TestBytes);

            Assert.That(actual, Is.EqualTo(TestBytes));
        }

        [Test]
        public void ReadValueInfo_ForExtended8_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, TestExtendedType))
            {
                var actual = Format.ReadValueInfo(ExtendedFormat.Variable8Format, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x12), "ContentSize");
                Assert.That(actual.ExtendedType, Is.EqualTo(TestExtendedType), "ExtendedType");
            }
        }

        [Test]
        public void ReadValueInfo_ForExtended8_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff, TestExtendedType))
            {
                var actual = Format.ReadValueInfo(ExtendedFormat.Variable8Format, stream);

                Assert.That(actual.ContentSize, Is.EqualTo(0xff), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForExtended16_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, 0x34, TestExtendedType))
            {
                var actual = Format.ReadValueInfo(ExtendedFormat.Variable16Format, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(3), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x1234), "ContentSize");
                Assert.That(actual.ExtendedType, Is.EqualTo(TestExtendedType), "ExtendedType");
            }
        }

        [Test]
        public void ReadValueInfo_ForExtended16_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff, 0xff, TestExtendedType))
            {
                var actual = Format.ReadValueInfo(ExtendedFormat.Variable16Format, stream);

                Assert.That(actual.ContentSize, Is.EqualTo(0xffff), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForExtended32_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x12, 0x34, 0x56, 0x78, TestExtendedType))
            {
                var actual = Format.ReadValueInfo(ExtendedFormat.Variable32Format, stream);

                Assert.That(actual.HeaderSize, Is.EqualTo(5), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0x12345678), "ContentSize");
                Assert.That(actual.ExtendedType, Is.EqualTo(TestExtendedType), "ExtendedType");
            }
        }
    }
}
