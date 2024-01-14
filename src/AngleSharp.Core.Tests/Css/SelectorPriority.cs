namespace AngleSharp.Core.Tests.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Dom;
    using AngleSharp.Css.Parser;
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
    }
}
