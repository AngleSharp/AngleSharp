namespace AngleSharp.Core.Tests.Io
{
    using AngleSharp.Io;
    using NUnit.Framework;

    [TestFixture]
    public class MimeTypeNameTests
    {
        [Test]
        public void CommonMimeTypesAreCorrectlyDefined()
        {
            Assert.AreEqual("image/avif", MimeTypeNames.FromExtension(".avif"));
            Assert.AreEqual("image/gif", MimeTypeNames.FromExtension(".gif"));
            Assert.AreEqual("image/jpeg", MimeTypeNames.FromExtension(".jpeg"));
            Assert.AreEqual("image/jpeg", MimeTypeNames.FromExtension(".jpg"));
            Assert.AreEqual("image/png", MimeTypeNames.FromExtension(".png"));
            Assert.AreEqual("image/svg+xml", MimeTypeNames.FromExtension(".svg"));
            Assert.AreEqual("image/webp", MimeTypeNames.FromExtension(".webp"));
        }
    }
}