namespace AngleSharp.Core.Tests.Examples
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class FormsTests
    {
        [Test]
        public async Task SubmittingToGoogleWithCookiesShouldWork()
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader().WithCookies());
            var queryDocument = await context.OpenAsync("https://google.com");
            var form = queryDocument.QuerySelector<IHtmlFormElement>("form");
            var resultDocument = await form.SubmitAsync(new { q = "AngleSharp" });
            var itemCount = resultDocument.QuerySelectorAll<IHtmlAnchorElement>("#ires .g h3.r a").Select(m => m.Href).Count();

            Assert.Greater(itemCount, 0);
        }
    }
}

