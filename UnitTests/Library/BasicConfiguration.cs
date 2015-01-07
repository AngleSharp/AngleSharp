using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.Infrastructure;
using NUnit.Framework;
using System.Linq;

namespace UnitTests.Library
{
    [TestFixture]
    public class BasicConfigurationTests
    {
        [Test]
        public void HasStyleEngine()
        {
            var config = new Configuration();
            var engine = config.StyleEngines.FirstOrDefault();
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
