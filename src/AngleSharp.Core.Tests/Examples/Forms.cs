namespace AngleSharp.Core.Tests.Examples
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class FormsTests
    {
        [Test]
        [Ignore("Google may show an altered version - we should not depend on externals for such tests")]
        public async Task SubmittingToGoogleWithCookiesShouldWork()
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader().WithDefaultCookies());
            var queryDocument = await context.OpenAsync("https://google.com");
            var form = queryDocument.QuerySelector<IHtmlFormElement>("form");
            var resultDocument = await form.SubmitAsync(new { q = "AngleSharp" });
            var itemCount = resultDocument.QuerySelectorAll<IHtmlAnchorElement>("a").Select(m => m.Href).Count();

            Assert.Greater(itemCount, 0);
        }
    }
}

