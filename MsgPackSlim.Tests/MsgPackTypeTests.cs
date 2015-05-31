using NUnit.Framework;

namespace MsgPackSlim
{
    [TestFixture]
    public class MsgPackTypeTests
    {
        [Test]
        public void TypeMap_HasNoNullItems()
        {
            var typeMap = MsgPackType.TypeMap;

            for (var i = 0; i < typeMap.Length; i++)
            {
                Assert.That(typeMap[i], Is.Not.Null, "index {0}", i);
            }
        }
    }
}
