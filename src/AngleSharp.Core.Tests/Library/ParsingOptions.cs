namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Html.Parser;
    using NUnit.Framework;

    [TestFixture]
    public class ParsingOptionsTests
    {
        [Test]
        public void PreserveAttributesDisabled_Issue897()
        {
            var source = @"<div *ngIf=""condition"">Content to render when condition is true.</div>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsPreservingAttributeNames = false,
            });
            var document = parser.ParseDocument(source);
            var element = document.QuerySelector("div");
            Assert.AreEqual(@"<div *ngif=""condition"">Content to render when condition is true.</div>", element.ToHtml());
        }

        [Test]
        public void PreserveAttributesEnabled_Issue897()
        {
            var source = @"<div *ngIf=""condition"">Content to render when condition is true.</div>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsPreservingAttributeNames = true,
            });
            var document = parser.ParseDocument(source);
            var element = document.QuerySelector("div");
            Assert.AreEqual(@"<div *ngIf=""condition"">Content to render when condition is true.</div>", element.ToHtml());
        }

        [Test]
        public void PreserveAttributesEnabledInFullForm_Issue897()
        {
            var source = @"<ng-template [ngIf]=""condition""><div>Content to render when condition is true.</div></ng-template>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                IsPreservingAttributeNames = true,
            });
            var document = parser.ParseDocument(source);
            var element = document.QuerySelector("ng-template");
            Assert.AreEqual(@"<ng-template [ngIf]=""condition""><div>Content to render when condition is true.</div></ng-template>", element.ToHtml());
        }
    }
}
