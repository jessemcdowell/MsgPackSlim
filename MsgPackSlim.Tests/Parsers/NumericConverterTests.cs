using System;
using System.Linq;
using System.Linq.Expressions;
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

        [Test]
        public void ToUInt32_ReturnsExpectedValue()
        {
            var input = GetBytes(0x11, 0x22, 0x33, 0x44);
            const uint expected = 0x11223344;

            var actual = NumericConverter.ToUInt32(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }

        [Test]
        public void ToUInt32_ForMaximumValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xff, 0xff, 0xff, 0xff);
            const uint expected = 0xffffffff;

            var actual = NumericConverter.ToUInt32(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }

        [Test]
        public void ToUInt64_ReturnsExpectedValue()
        {
            var input = GetBytes(0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88);
            const ulong expected = 0x1122334455667788;

            var actual = NumericConverter.ToUInt64(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }

        [Test]
        public void ToUInt64_ForMaximumValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff);
            const ulong expected = 0xffffffffffffffff;

            var actual = NumericConverter.ToUInt64(input);

            Assert.That(actual, Is.EqualTo(expected), actual.ToString("x"));
        }

        [Test]
        public void ToSingle_WithValueZero_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x00);
            const float expected = 0;

            var actual = NumericConverter.ToSingle(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToSingle_ReturnsExpectedValue()
        {
            var input = GetBytes(0x42, 0xf6, 0xe9, 0x79);
            const float expected = 123.456f;

            var actual = NumericConverter.ToSingle(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToSingle_WithNegativeValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xc2, 0xf6, 0xe9, 0x79);
            const float expected = -123.456f;

            var actual = NumericConverter.ToSingle(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static readonly object[][] SpecialSingleValues =
        {
            new object[] {float.NaN, GetBytes(0xff, 0xc0, 0x00, 0x00)},
            new object[] {float.PositiveInfinity, GetBytes(0x7f, 0x80, 0x00, 0x00)},
            new object[] {float.NegativeInfinity, GetBytes(0xff, 0x80, 0x00, 0x00)},
            new object[] {float.MaxValue, GetBytes(0x7f, 0x7f, 0xff, 0xff)},
            new object[] {float.MinValue, GetBytes(0xff, 0x7f, 0xff, 0xff)},
            new object[] {float.Epsilon, GetBytes(0x00, 0x00, 0x00, 0x01)}
        };

        [TestCaseSource("SpecialSingleValues")]
        public void ToSingle_ForSpecialValues_ReturnsExpectedValue(float expected, byte[] input)
        {
            var actual = NumericConverter.ToSingle(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToDouble_WithValueZero_ReturnsExpectedValue()
        {
            var input = GetBytes(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00);
            const double expected = 0;

            var actual = NumericConverter.ToSingle(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToDouble_ForFloat64_ReturnsExpectedValue()
        {
            var input = GetBytes(0x40, 0x5e, 0xdd, 0x2f, 0x1a, 0x9f, 0xbe, 0x77);
            const double expected = 123.456;

            var actual = NumericConverter.ToDouble(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void ToDouble_WithNegativeValue_ReturnsExpectedValue()
        {
            var input = GetBytes(0xc0, 0x5e, 0xdd, 0x2f, 0x1a, 0x9f, 0xbe, 0x77);
            const double expected = -123.456;

            var actual = NumericConverter.ToDouble(input);

            Assert.That(actual, Is.EqualTo(expected));
        }

        public static readonly object[][] SpecialDoubleValues =
        {
            new object[] {double.NaN, GetBytes(0xff, 0xf8, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)},
            new object[] {double.PositiveInfinity, GetBytes(0x7f, 0xf0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)},
            new object[] {double.NegativeInfinity, GetBytes(0xff, 0xf0, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00)},
            new object[] {double.MaxValue, GetBytes(0x7f, 0xef, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff)},
            new object[] {double.MinValue, GetBytes(0xff, 0xef, 0xff, 0xff, 0xff, 0xff, 0xff, 0xff)},
            new object[] {double.Epsilon, GetBytes(0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01)}
        };

        [TestCaseSource("SpecialDoubleValues")]
        public void ToDouble_ForSpecialValues_ReturnsExpectedValue(double expected, byte[] input)
        {
            var actual = NumericConverter.ToDouble(input);

            Assert.That(actual, Is.EqualTo(expected));
        }
    }
}
