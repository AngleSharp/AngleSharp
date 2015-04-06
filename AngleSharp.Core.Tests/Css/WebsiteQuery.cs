using AngleSharp.Dom.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests.Css
{
    [TestFixture]
    public class WebsiteQueryTests
    {
        [Test]
        public void HtmlCodeTutorialFindTableChildren()
        {
            var content = Helper.StreamFromBytes(Assets.htmlcodetutorial);
            var document = content.ToHtmlDocument();
            var query = "table:nth-child(21)";
            var result = document.QuerySelectorAll(query);
            Assert.AreEqual(1, result.Length);
        }

        [Test]
        public void HtmlCodeTutorialTableInParagraphElement()
        {
            var content = Helper.StreamFromBytes(Assets.htmlcodetutorial);
            var document = content.ToHtmlDocument();
            var cell = document.QuerySelector("td.content");
            Assert.AreEqual(22, cell.ChildElementCount);
            Assert.IsInstanceOf<HtmlTableElement>(cell.Children[7]);
            Assert.IsInstanceOf<HtmlTableElement>(cell.Children[13]);
            Assert.IsInstanceOf<HtmlTableElement>(cell.Children[20]);
        }
    }
}
