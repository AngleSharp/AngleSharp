namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Media;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ImageCandidatesTests
    {
        private static readonly String BaseUrl = "http://localhost";

        private static HtmlImageElement Construct(String str, IConfiguration config = null)
        {
            var context = BrowsingContext.New(config);
            var document = context.OpenNewAsync(BaseUrl).Result;
            document.Body.InnerHtml = str;
            return document.Body.QuerySelector("img") as HtmlImageElement;
        }

        [Test]
        public void ImageElementRetrievePictureFromSrc()
        {
            var source = "<img src=cat.jpg alt=cat>";
            var img = Construct(source);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/cat.jpg", candidate.Href);
        }

        [Test]
        public void ImageElementRetrievePictureFromSrcSetWithDensity()
        {
            var source = "<img src=\"cat.jpg\" alt=\"cat\" srcset=\"cat.jpg, cat-2x.jpg 2x\">";
            var img = Construct(source);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/cat.jpg", candidate.Href);
        }

        [Test]
        public void ImageElementRetrievePictureFromSrcSetWithWidth()
        {
            var source = "<img src=\"cat.jpg\" alt=\"cat\" srcset=\"cat-160.jpg 160w, cat-320.jpg 320w, cat-640.jpg 640w, cat-1280.jpg 1280w\">";
            var img = Construct(source);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/cat-160.jpg", candidate.Href);
        }

        [Test]
        public void ImageElementRetrievePictureFromPictureSimpleSrcSet()
        {
            var source = @"<picture>
  <source media=""(min-width: 900px)"" srcset=""cat-vertical.jpg"">
  <source media=""(min-width: 750px)"" srcset=""cat-horizontal.jpg"">
  <img src=""cat.jpg"" alt=""cat"">
</picture>";
            var img = Construct(source);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/cat-vertical.jpg", candidate.Href);
        }

        [Test]
        public void ImageElementRetrievePictureFromPictureCombinedSrcSet()
        {
            var source = @"<picture>
  <source srcset=""homepage-person@desktop.png, homepage-person@desktop-2x.png 2x"" media=""(min-width: 990px)"">
  <source srcset=""homepage-person@tablet.png, homepage-person@tablet-2x.png 2x"" media=""(min-width: 750px)"">
  <img srcset=""homepage-person@mobile.png, homepage-person@mobile-2x.png 2x"" alt=""Shopify Merchant, Corrine Anestopoulos"">
</picture>";
            var img = Construct(source);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/homepage-person@desktop.png", candidate.Href);
        }

        [Test]
        public void ImageElementRetrievePictureFromPictureTypeSelectionNoSupport()
        {
            var source = @"<picture>
  <source type=""image/svg+xml"" srcset=""logo.xml"">
  <source type=""image/webp"" srcset=""logo.webp""> 
  <img src=""logo.png"" alt=""ACME Corp"">
</picture>";
            var img = Construct(source);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/logo.png", candidate.Href);
        }

        [Test]
        public void ImageElementRetrievePictureFromPictureTypeSelectionSupportWebp()
        {
            var source = @"<picture>
  <source type=""image/svg+xml"" srcset=""logo.xml"">
  <source type=""image/webp"" srcset=""logo.webp""> 
  <img src=""logo.png"" alt=""ACME Corp"">
</picture>";
            var service = new ResourceService<IImageInfo>("image/webp", resp => null);
            var config = Configuration.Default.With(service);
            var img = Construct(source, config);
            var candidate = img.GetImageCandidate();
            Assert.AreEqual(BaseUrl + "/logo.webp", candidate.Href);
        }
    }
}
