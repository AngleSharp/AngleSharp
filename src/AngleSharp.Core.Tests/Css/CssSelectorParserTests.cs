namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class CssSelectorParserTests
    {
        [Test]
        public void ParseSelector_WithSameText_ReusesCachedInstance()
        {
            var parser = new CssSelectorParser();

            var first = parser.ParseSelector("div > p");
            var second = parser.ParseSelector("div > p");

            Assert.NotNull(first);
            Assert.AreSame(first, second);
        }

        [Test]
        public void ParseSelector_WhenCacheCapacityExceeded_EvictsLeastRecentlyUsedSelector()
        {
            var parser = new CssSelectorParser();
            var first = parser.ParseSelector("div");

            Assert.NotNull(first);

            for (var i = 0; i < 300; i++)
            {
                var selector = parser.ParseSelector($"div:nth-child({i + 1})");
                Assert.NotNull(selector);
            }

            var second = parser.ParseSelector("div");

            Assert.NotNull(second);
            Assert.AreNotSame(first, second);
        }
    }
}
