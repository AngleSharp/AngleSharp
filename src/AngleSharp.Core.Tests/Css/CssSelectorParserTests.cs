namespace AngleSharp.Core.Tests.Css
{
    using System;
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

        [Test]
        public void ParseSelector_WithHostPseudoClass_ReturnsSelector()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(":host");

            Assert.NotNull(selector);
            Assert.AreEqual(":host", selector!.Text);
        }

        [Test]
        public void ParseSelector_WithHostContextFunction_ReturnsSelector()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector(":host-context(.card)");

            Assert.NotNull(selector);
            Assert.AreEqual(":host-context(.card)", selector!.Text);
        }

        [Test]
        public void ParseSelector_WithOfClause_KeepsTheFilterInText()
        {
            var parser = new CssSelectorParser();

            var selector = parser.ParseSelector("li:nth-child(2 of .item)");

            Assert.NotNull(selector);
            Assert.AreEqual("li:nth-child(0n+2 of .item)", selector!.Text);
        }

        [TestCase("nth-child")]
        [TestCase("nth-last-child")]
        public void ParseSelector_WithOfClause_SerializesToAnEquivalentSelector(String name)
        {
            // The of-clause decides which siblings are counted at all, so losing it in Text
            // widens the selector and drops the inner selector out of the specificity with it.
            var text = $"li:{name}(2n+1 of .item)";

            var selector = new CssSelectorParser().ParseSelector(text);

            Assert.NotNull(selector);

            // A parser of its own: the one above caches by selector text and would answer a
            // re-parse of an unchanged serialization out of that cache.
            var reparsed = new CssSelectorParser().ParseSelector(selector!.Text);

            Assert.NotNull(reparsed);
            Assert.AreEqual(selector.Text, reparsed!.Text);
            Assert.AreEqual(selector.Specificity, reparsed.Specificity);
        }
    }
}
