using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class IntegerTypeTests : TestBase
    {
        private IntegerType Type { get; set; }

        private const byte Int8FormatByte = 0xd0;
        private const byte Int16FormatByte = 0xd1;
        private const byte Int32FormatByte = 0xd2;
        private const byte Int64FormatByte = 0xd3;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new IntegerType();
        }

        [Test]
        public void GetValue_ForFixInt_WithValueZero_ReturnsExpectedValue()
        {
            const byte formatByte = 0x00;
            const int expected = 0;

            var actual = Type.GetValue(formatByte, null, null);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForFixInt_ReturnsExpectedValue()
        {
            const byte formatByte = 0x76;
            const int expected = 0x76;

            var actual = Type.GetValue(formatByte, null, null);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForNegativeFixInt_WithValueNegativeOne_ReturnsExpectedValue()
        {
            const byte formatByte = 0xff;
            const int expected = -1;

            var actual = Type.GetValue(formatByte, null, null);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForNegativeFixInt_ReturnsExpectedValue()
        {
            const byte formatByte = 0xeb;
            const int expected = -0x15;

            var actual = Type.GetValue(formatByte, null, null);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForPositiveInt8_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x12);
            const int expected = 0x12;

            var actual = Type.GetValue(Int8FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForNegativeInt8_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x9a);
            const int expected = -0x66;

            var actual = Type.GetValue(Int8FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForPositiveInt16_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x12, 0x34);
            const int expected = 0x1234;

            var actual = Type.GetValue(Int16FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForNegativeInt16_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x9a, 0xbc);
            const int expected = -0x6544;

            var actual = Type.GetValue(Int16FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForPositiveInt32_ReturnsExpectedValue()
        {
            var input = GetBytes(0x12, 0x34, 0x56, 0x78);
            const int expected = 0x12345678;

            var actual = Type.GetValue(Int32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForNegativeInt32_ReturnsExpectedValue()
        {
            var input = GetBytes(0x9a, 0xbc, 0xde, 0xf1);
            const int expected = -0x6543210f;

            var actual = Type.GetValue(Int32FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForPositiveInt64_ReturnsExpectedValue()
        {
            var input = GetBytes(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88);
            const long expected = 0x1122334455667788;

            var actual = Type.GetValue(Int64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void GetValue_ForNegativeInt64_ReturnsExpectedValue()
        {
            var input = GetBytes(0x88, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0xff, 0x11);
            const long expected = -0x77554433221100EF;

            var actual = Type.GetValue(Int64FormatByte, null, input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
