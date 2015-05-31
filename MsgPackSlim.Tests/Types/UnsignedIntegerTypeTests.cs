using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class UnsignedIntegerTypeTests : TestBase
    {
        private UnsignedIntegerType Type { get; set; }

        private const byte UInt8FormatByte = 0xcc;
        private const byte UInt16FormatByte = 0xcd;
        private const byte UInt32FormatByte = 0xce;
        private const byte UInt64FormatByte = 0xcf;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new UnsignedIntegerType();
        }

        [Test]
        public void GetValue_ForUInt8_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x12);
            const uint expected = 0x12;

            var actual = Type.GetValue(UInt8FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt8_WithMaximumValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0xff);
            const uint expected = 0xff;

            var actual = Type.GetValue(UInt8FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt16_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x12, 0x34);
            const uint expected = 0x1234;

            var actual = Type.GetValue(UInt16FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt16_WithMaximumValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0xff, 0xff);
            const uint expected = 0xffff;

            var actual = Type.GetValue(UInt16FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt32_ReturnsExpectedValue()
        {
            var input = GetBytes(0x12, 0x34, 0x56, 0x78);
            const uint expected = 0x12345678;

            var actual = Type.GetValue(UInt32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt32_WithMaximumValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xff, 0xff, 0xff, 0xff);
            const uint expected = 0xffffffff;

            var actual = Type.GetValue(UInt32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt64_ReturnsExpectedValue()
        {
            var input = GetBytes(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88);
            const ulong expected = 0x1122334455667788;

            var actual = Type.GetValue(UInt64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForUInt64_WithMaximumValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff);
            const ulong expected = 0xffffffffffffffff;

            var actual = Type.GetValue(UInt64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
