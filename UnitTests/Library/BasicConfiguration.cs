using AngleSharp;
using AngleSharp.DOM.Css;
using AngleSharp.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests.Library
{
    [TestClass]
    public class BasicConfigurationTests
    {
        [TestMethod]
        public void HasStyleEngine()
        {
            var config = new Configuration();
            var engine = config.StyleEngines.FirstOrDefault();
            Assert.IsNotNull(engine);
            Assert.IsInstanceOfType(engine, typeof(CssStyleEngine));
        }

        [TestMethod]
        public void ObtainDefaultSheet()
        {
            var engine = new CssStyleEngine();
            Assert.IsNotNull(engine.Default);
            Assert.AreEqual("text/css", engine.Type);
            var sheet = engine.Default as CSSStyleSheet;
            Assert.IsNotNull(sheet);
            Assert.AreEqual(49, sheet.Rules.Length);
        }
    }
}
