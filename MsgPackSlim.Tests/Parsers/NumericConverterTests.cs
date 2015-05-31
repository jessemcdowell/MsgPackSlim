using NUnit.Framework;

namespace MsgPackSlim.Parsers
{
    [TestFixture]
    public class NumericConverterTests : TestBase
    {
        [Test]
        public void ToInt32_ForPositiveNumber_ReturnsExpectedValue()
        {
            var input = GetBytes(0x11, 0x22, 0x33, 0x44);
            const int expected = 0x11223344;

            var actual = NumericConverter.ToInt32(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }

        [Test]
        public void ToInt32_FoNegativeNumber_ReturnsExpectedValue()
        {
            var input = GetBytes(0x88, 0x99, 0xaa, 0xbb);
            const int expected = -0x77665545;

            var actual = NumericConverter.ToInt32(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }

        [Test]
        public void ToInt64_ForPositiveNumber_ReturnsExpectedValue()
        {
            var input = GetBytes(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88);
            const long expected = 0x1122334455667788;

            var actual = NumericConverter.ToInt64(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }


        [Test]
        public void ToInt64_ForNegativeNumber_ReturnsExpectedValue()
        {
            var input = GetBytes(0x88, 0x99, 0xaa, 0xbb, 0xcc, 0xdd, 0xee, 0x89);
            const long expected = -0x7766554433221177;

            var actual = NumericConverter.ToInt64(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }
    }
}
