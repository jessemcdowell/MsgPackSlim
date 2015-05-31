using System.Linq;
using MsgPackSlim.Types;
using NUnit.Framework;

namespace MsgPackSlim
{
    [TestFixture]
    public class MsgPackTypeTests
    {
        [Test]
        public void FormatMap_HasNoNullItems()
        {
            var formatMap = MsgPackType.FormatMap;

            for (var i = 0; i < formatMap.Length; i++)
            {
                Assert.That(formatMap[i], Is.Not.Null, "index {0}", i);
            }
        }

        [Test]
        public void FormatMap_HasInstanceOfAllTypes()
        {
            var allTypes = typeof (IMsgPackType).Assembly.GetExportedTypes()
                .Where(type =>
                    (typeof (IMsgPackType)).IsAssignableFrom(type) &&
                    !type.IsAbstract)
                .ToList();
            Assert.That(allTypes, Is.Not.Empty);

            var allRegisteredTypes = MsgPackType.FormatMap
                .GroupBy(type => type.GetType())
                .Select(group => group.Key)
                .ToList();

            var typesNotRegistered = allTypes.Except(allRegisteredTypes);
            Assert.That(typesNotRegistered, Is.Empty);
        }
    }
}
