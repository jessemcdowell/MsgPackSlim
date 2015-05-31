using NUnit.Framework;

namespace MsgPackSlim.Formats
{
    [TestFixture]
    public class NilFormatTests : TestBase
    {
        private NilFormat Format { get; set; }

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Format = new NilFormat();
        }

        [Test]
        public void GetValue_ReturnsExpectedValue()
        {
            const byte nilFormatByte = 0xc0;

            var actual = Format.GetValue(nilFormatByte, null, null);

            Assert.That(actual, Is.Null);
        }
    }
}
