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
            var document = DocumentBuilder.Html(content);
            var query = "table:nth-child(21)";
            var result = document.QuerySelectorAll(query);
            Assert.AreEqual(1, result.Length);
        }
    }
}
