namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using AngleSharp.Parser.Html;
    using AngleSharp.Parser.Xml;
    using AngleSharp.Services;
    using NUnit.Framework;
    using System.Collections.Generic;
    using System.Linq;

    [TestFixture]
    public class BasicConfigurationTests
    {
        [Test]
        public void HasStyleEngine()
        {
            var config = new Configuration().WithCss();
            var provider = config.GetProvider<IStylingProvider>();
            Assert.IsNotNull(provider);
            var engine = provider.GetEngine(MimeTypeNames.Css);
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
        }

        [Test]
        public void ConfigurationSetCultureExtensionLeavesOriginallyUnmodified()
        {
            var original = new Configuration();
            var modified = original.SetCulture("de-at");
            Assert.AreNotSame(original, modified);
            Assert.AreNotEqual(original.Services.Count(), modified.Services.Count());
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

        [Test]
        public void ObtainElementPositionsFromHtml()
        {
            var positions = new Dictionary<IElement, TextPosition>();
            var source = @"<article class=""grid-item large"">
    <div class=""grid-image""><a href=""/News/Page/298/cpp-mva-course""><img src=""/img/news/maxresdefault700x240.png"" alt=""Icon"" title=""C++ MVA Course"" /></a></div>
    <div class=""grid-title""><a href=""/News/Page/298/cpp-mva-course"">C++ MVA Course</a></div>
    <div class=""grid-abstract"">My Microsoft Virtual Academy course about modern C++ is now available.</div>
    <div class=""grid-date"">6/5/2015</div>
        <div class=""grid-admin"">        <a href=""/Page/Delete/298"">Delete</a> | <a href=""/Page/Edit/298"">Edit</a> | <a href=""/Page/Create?parentId=1"">Create New</a>
</div>
    </article>";
            var parser = new HtmlParser(new HtmlParserOptions
            {
                OnCreated = (element, position) => positions[element] = position
            });
            var document = parser.Parse(source);
            Assert.AreEqual(15, positions.Count);
        }

        [Test]
        public void ObtainElementPositionsFromXml()
        {
            var positions = new Dictionary<IElement, TextPosition>();
            var source = @"<hello>
   <foo />
   <bar>
      <test></test><test></test><test></test>
   </bar>
</hello>";
            var parser = new XmlParser(new XmlParserOptions
            {
                OnCreated = (element, position) => positions[element] = position
            });
            var document = parser.Parse(source);
            Assert.AreEqual(6, positions.Count);
        }
    }
}
