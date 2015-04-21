namespace AngleSharp.Core.Tests.Library
{
    using System.Linq;
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
            var config = new Configuration().WithCss();
            var service = config.GetService<IStylingService>();
            Assert.IsNotNull(service);
            var engine = service.GetEngine(MimeTypes.Css);
            Assert.IsNotNull(engine);
            Assert.IsInstanceOf<CssStyleEngine>(engine);
        }

        [Test]
        public void ConfigurationWithExtensionLeavesOriginallyUnmodified()
        {
            var original = new Configuration();
            var modified = original.WithCss();
            Assert.AreNotSame(original, modified);
            Assert.AreNotEqual(original.Services.Count(), modified.Services.Count());
            Assert.AreSame(original.Events, modified.Events);
            Assert.AreSame(original.Culture, modified.Culture);
        }

        [Test]
        public void ConfigurationSetCultureExtensionLeavesOriginallyUnmodified()
        {
            var original = new Configuration();
            var modified = original.SetCulture("de-at");
            Assert.AreNotSame(original, modified);
            Assert.AreEqual(original.Services.Count(), modified.Services.Count());
            Assert.AreSame(original.Events, modified.Events);
            Assert.AreNotSame(original.Culture, modified.Culture);
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
