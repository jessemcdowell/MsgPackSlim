using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class NilTypeTests : TestBase
    {
        private NilType Type { get; set; }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new NilType();
        }

        [Test]
        public void GetValue_ReturnsExpectedValue()
        {
            const byte nilFormatByte = 0xc0;

            var actual = Type.GetValue(nilFormatByte, null, null);

            Assert.That(actual, Is.Null);
        }
    }
}
