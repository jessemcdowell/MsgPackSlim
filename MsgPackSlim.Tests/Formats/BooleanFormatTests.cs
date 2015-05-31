using NUnit.Framework;

namespace MsgPackSlim.Formats
{
    [TestFixture]
    public class BooleanFormatTests : TestBase
    {
        private BooleanFormat Format { get; set; }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Format = new BooleanFormat();
        }

        [Test]
        public void GetValue_ForTrue_ReturnsExpectedValue()
        {
            const byte trueFormatByte = 0xc3;

            var actual = Format.GetValue(trueFormatByte, null, null);

            Assert.That(actual, Is.EqualTo(true));
        }

        [Test]
        public void GetValue_ForFalse_ReturnsExpectedValue()
        {
            const byte falseFormatByte = 0xc2;

            var actual = Format.GetValue(falseFormatByte, null, null);

            Assert.That(actual, Is.EqualTo(false));
        }
    }
}
