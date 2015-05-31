using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class BooleanTypeTests : TestBase
    {
        private BooleanType Type { get; set; }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new BooleanType();
        }

        [Test]
        public void GetValue_ForTrue_ReturnsExpectedValue()
        {
            const byte trueFormatByte = 0xc3;

            var actual = Type.GetValue(trueFormatByte, null, null);

            Assert.That(actual, Is.EqualTo(true));
        }

        [Test]
        public void GetValue_ForFalse_ReturnsExpectedValue()
        {
            const byte falseFormatByte = 0xc2;

            var actual = Type.GetValue(falseFormatByte, null, null);

            Assert.That(actual, Is.EqualTo(false));
        }
    }
}
