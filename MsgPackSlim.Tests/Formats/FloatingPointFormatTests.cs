using NUnit.Framework;

namespace MsgPackSlim.Formats
{
    [TestFixture]
    public class FloatingPointFormatTests : TestBase
    {
        private FloatingPointFormat Format { get; set; }

        private const byte Float32FormatByte = 0xca;
        private const byte Float64FormatByte = 0xcb;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Format = new FloatingPointFormat();
        }

        [Test]
        public void GetValue_ForFloat32_WithValueZero_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x00);
            const float expected = 0;

            var actual = Format.GetValue(Float32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForFloat32_ReturnsExpectedValue()
        {
            var input = GetBytes(0x42, 0xf6, 0xe9, 0x79);
            const float expected = 123.456f;

            var actual = Format.GetValue(Float32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForFloat32_WithNegativeValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xc2, 0xf6, 0xe9, 0x79);
            const float expected = -123.456f;

            var actual = Format.GetValue(Float32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForFloat64_WithValueZero_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
            const double expected = 0;

            var actual = Format.GetValue(Float64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForFloat64_ReturnsExpectedValue()
        {
            var input = GetBytes(0x40, 0x5e, 0xdd, 0x2f, 0x1a, 0x9f, 0xbe, 0x77);
            const double expected = 123.456;

            var actual = Format.GetValue(Float64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForFloat64_WithNegativeValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xc0, 0x5e, 0xdd, 0x2f, 0x1a, 0x9f, 0xbe, 0x77);
            const double expected = -123.456;

            var actual = Format.GetValue(Float64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
