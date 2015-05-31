using System.Linq;
using MsgPackSlim.Formats;
using NUnit.Framework;

namespace MsgPackSlim
{
    [TestFixture]
    public class MsgPackFormatTests
    {
        [Test]
        public void FormatMap_HasNoNullItems()
        {
            var formatMap = MsgPackFormat.FormatMap;

            for (var i = 0; i < formatMap.Length; i++)
            {
                Assert.That(formatMap[i], Is.Not.Null, "index {0}", i);
            }
        }

        [Test]
        public void FormatMap_HasInstanceOfAllTypes()
        {
            var allTypes = typeof (IMsgPackFormat).Assembly.GetExportedTypes()
                .Where(type =>
                    (typeof (IMsgPackFormat)).IsAssignableFrom(type) &&
                    !type.IsAbstract)
                .ToList();
            Assert.That(allTypes, Is.Not.Empty);

            var allRegisteredTypes = MsgPackFormat.FormatMap
                .GroupBy(type => type.GetType())
                .Select(group => group.Key)
                .ToList();

            var typesNotRegistered = allTypes.Except(allRegisteredTypes);
            Assert.That(typesNotRegistered, Is.Empty);
        }
    }
}
