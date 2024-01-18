namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class SelectorPriorityTests
    {
        [Test]
        public void IdentifyMultiSelector_Issue1161()
        {
            var source = @"<h3 id='target'>Test</h3>";
            var doc = source.ToHtmlDocument();
            var selector = doc.Context.GetService<ICssSelectorParser>().ParseSelector("h3, #notarget");

            Assert.AreEqual(new Priority(0, 1, 0, 0), selector.Specificity);

            var ms = selector as IMultiSelector;

            var subSelector = ms.GetMatchingSelector(doc.QuerySelector("h3, #notarget"));

            Assert.AreEqual(new Priority(0, 0, 0, 1), subSelector.Specificity);
        }

        [Test]
        public void MultiSelectorReturnsNullIfNothingMatches_Issue1161()
        {
            var source = @"<h3 id='target'>Test</h3>";
            var doc = source.ToHtmlDocument();
            var selector = doc.Context.GetService<ICssSelectorParser>().ParseSelector("h3, #notarget");

            Assert.AreEqual(new Priority(0, 1, 0, 0), selector.Specificity);

            var ms = selector as IMultiSelector;

            var subSelector = ms.GetMatchingSelector(doc.QuerySelector("body"));

            Assert.IsNull(subSelector);
        }

        [Test]
        public void MultiSelectorReturnsHighestPrioIfMultiMatches_Issue1161()
        {
            var source = @"<h3 id='target'>Test</h3>";
            var doc = source.ToHtmlDocument();
            var selector = doc.Context.GetService<ICssSelectorParser>().ParseSelector("h3, #target");

            Assert.AreEqual(new Priority(0, 1, 0, 0), selector.Specificity);

            var ms = selector as IMultiSelector;

            var subSelector = ms.GetMatchingSelector(doc.QuerySelector("h3"));

            Assert.AreEqual(new Priority(0, 1, 0, 0), subSelector.Specificity);
        }

        [Test]
        public void IsSelectorMatchesInside()
        {
            var selectorText = $@":is(article, section, aside) h1";
            var source = @"<section><div><h1>Foo</h1></div></section><p><h1>Other</h1></p>";
            var doc = source.ToHtmlDocument();

            var result = doc.QuerySelectorAll(selectorText);
            Assert.AreEqual(1, result.Length);

            Assert.AreEqual("div", result[0].ParentElement.LocalName);
        }

        [Test]
        public void NotSelectorWhenNotMatchesInside()
        {
            var selectorText = $@":not(article, section, aside) h1";
            var source = @"<section><div><h1>Foo</h1></div></section><p><h1>Other</h1></p>";
            var doc = source.ToHtmlDocument();

            var result = doc.QuerySelectorAll(selectorText);
            Assert.AreEqual(2, result.Length);
        }

        [Test]
        public void HasSelectorWithFollowUpWorks()
        {
            var selectorText = $@"h1:has(+ p)";
            var source = @"<h1>Foo</h1><p>Text</p><h1>Other</h1>";
            var doc = source.ToHtmlDocument();

            var result = doc.QuerySelectorAll(selectorText);
            Assert.AreEqual(1, result.Length);

            Assert.AreEqual("Foo", result[0].TextContent);
        }

        [Test]
        public void WhereHasZeroSpecificity()
        {
            var source = @"<h3 id='target'>Test</h3>";
            var doc = source.ToHtmlDocument();
            var selector = doc.Context.GetService<ICssSelectorParser>().ParseSelector(":where(h1)");

            Assert.AreEqual(new Priority(0, 0, 0, 0), selector.Specificity);
        }

        [Test]
        public void NestedSelectorRepresentsScopingRoot()
        {
            var source = @"<h3 id='target'>Test</h3>";
            var doc = source.ToHtmlDocument();
            var selector = doc.Context.GetService<ICssSelectorParser>().ParseSelector("&");

            Assert.AreEqual(new Priority(0, 0, 1, 0), selector.Specificity);
        }

        [Test]
        public void IsSelectorIsForgiving()
        {
            var source = @"<div class=valid-class>Foo</div>";
            var selectorText = ":is(.valid-class, :invalid-pseudo-class)";
            var doc = source.ToHtmlDocument();

            var result = doc.QuerySelectorAll(selectorText);
            Assert.AreEqual(1, result.Length);

            Assert.AreEqual("Foo", result[0].TextContent);
        }

        [Test]
        public void StandardSelectorIsNotForgiving()
        {
            var source = @"<div class=valid-class>Foo</div>";
            var selectorText = ".valid-class, :invalid-pseudo-class";
            var doc = source.ToHtmlDocument();

            Assert.Throws<DomException>(() =>
            {
                doc.QuerySelectorAll(selectorText);
            });
        }
    }
}
