using AngleSharp;
using AngleSharp.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class AnalysisWindowTests
    {
        [TestMethod]
        public void GetComputedStyleTrivialInitialScenario()
        {
            var sourceCode = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";
            var window = new AnalysisWindow();
            var document = DocumentBuilder.Html(sourceCode);
            Assert.IsNotNull(document);
            window.Document = document;
            window.ScreenX = 0;
            window.ScreenY = 0;
            window.OuterHeight = 768;
            window.OuterWidth = 1024;
            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);
            Assert.AreEqual("span", element.TagName);
            Assert.AreEqual("bold", element.ClassName);
            var style = window.GetComputedStyle(element);
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }
    }
}
