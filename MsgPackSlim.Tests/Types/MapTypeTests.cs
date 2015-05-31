﻿using NUnit.Framework;

namespace MsgPackSlim.Types
{
    [TestFixture]
    public class MapTypeTests : TestBase
    {
        private MapType Type { get; set; }

        private const byte Map16FormatByte = 0xde;
        private const byte Map32FormatByte = 0xdf;

        [TestFixtureSetUp]
        public void FixtureSetUp()
        {
            Type = new MapType();
        }

        [Test]
        public void ReadValueInfo_ForFixMap_ReturnsExpectedValues()
        {
            const byte formatByteWithOneItem = 0x81;

            var actual = Type.ReadValueInfo(formatByteWithOneItem, null);

            Assert.That(actual.ChildObjectCount, Is.EqualTo(2), "ChildObjectCount");
            Assert.That(actual.HeaderSize, Is.EqualTo(0), "HeaderSize");
            Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
        }

        [Test]
        public void ReadValueInfo_ForMap16_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x11, 0x22))
            {
                var actual = Type.ReadValueInfo(Map16FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0x1122 * 2), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForMap16_WithMaximumLength_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0xff, 0xff))
            {
                var actual = Type.ReadValueInfo(Map16FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0xffff * 2), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(2), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }

        [Test]
        public void ReadValueInfo_ForMap32_ReturnsExpectedValues()
        {
            using (var stream = GetStream(0x11, 0x22, 0x33, 0x44))
            {
                var actual = Type.ReadValueInfo(Map32FormatByte, stream);

                Assert.That(actual.ChildObjectCount, Is.EqualTo(0x11223344 * 2), "ChildObjectCount");
                Assert.That(actual.HeaderSize, Is.EqualTo(4), "HeaderSize");
                Assert.That(actual.ContentSize, Is.EqualTo(0), "ContentSize");
            }
        }
    }
}
