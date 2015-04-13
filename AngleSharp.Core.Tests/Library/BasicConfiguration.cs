namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Services;
    using NUnit.Framework;

    [TestFixture]
    public class BasicConfigurationTests
    {
        [Test]
        public void HasStyleEngine()
        {
            var config = new Configuration();
            var service = config.GetService<IStylingService>();
            Assert.IsNotNull(service);
            var engine = service.GetEngine(MimeTypes.Css);
            Assert.IsNotNull(engine);
            Assert.IsInstanceOf<CssStyleEngine>(engine);
        }

        [Test]
        public void ObtainDefaultSheet()
        {
            var engine = new CssStyleEngine();
            Assert.IsNotNull(engine.Default);
            Assert.AreEqual("text/css", engine.Type);
            var sheet = engine.Default as CssStyleSheet;
            Assert.IsNotNull(sheet);
            Assert.AreEqual(49, sheet.Rules.Length);
        }
    }
}
