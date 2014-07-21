using AngleSharp;
using AngleSharp.Tools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class AnalysisWindowTests
    {
        AnalysisWindow window;

        [TestInitialize]
        public void CreateDefaultAnalysisWindow()
        {
            window = new AnalysisWindow();
            window.ScreenX = 0;
            window.ScreenY = 0;
            window.OuterHeight = 768;
            window.OuterWidth = 1024;
        }

        [TestMethod]
        public void GetComputedStyleTrivialInitialScenario()
        {
            var sourceCode = "<!doctype html><head><style>p > span { color: blue; } span.bold { font-weight: bold; }</style></head><body><div><p><span class='bold'>Bold text";

            var document = DocumentBuilder.Html(sourceCode);
            Assert.IsNotNull(document);
            window.Document = document;

            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);

            Assert.AreEqual("span", element.TagName);
            Assert.AreEqual("bold", element.ClassName);

            var style = window.GetComputedStyle(element);
            Assert.IsNotNull(style);
            Assert.AreEqual(2, style.Length);
        }

        [TestMethod]
        public void GetComputedStyleTrivialInitialScenarioTwo()
        {
            var source = new StringBuilder("<!doctype html> ");

            var styles = new StringBuilder("<head><style>");
            styles.Append("p {text-align: center;}");
            styles.Append("p > span { color: blue; }");
            styles.Append("p > span { color: red; }");
            styles.Append("span.bold { font-weight: bold !important; }");
            styles.Append("span.bold { font-weight: lighter; }");

            styles.Append("#prioOne { color: black; }");
            styles.Append("div {color: green; }");
            styles.Append("</style></head>");

            var body = new StringBuilder("<body>");
            body.Append("<div><p><span class='bold'>Bold text</span></p></div>");
            body.Append("<div id='prioOne'>prioOne</div>");
            body.Append("</body>");

            source.Append(styles);
            source.Append(body);

            var document = DocumentBuilder.Html(source.ToString());
            Assert.IsNotNull(document);
            window.Document = document;

            // checks for element with text bold text
            var element = document.QuerySelector("span.bold");
            Assert.IsNotNull(element);
            Assert.AreEqual("span", element.TagName);
            Assert.AreEqual("bold", element.ClassName);

            var computedStyle = window.GetComputedStyle(element);
            Assert.AreEqual("red", computedStyle.Color);
            Assert.AreEqual("bold", computedStyle.FontWeight);
            Assert.AreEqual(3, computedStyle.Length);

            // checks for element with text prioOne
            var prioOne = document.QuerySelector("#prioOne");
            Assert.IsNotNull(prioOne);
            Assert.AreEqual("div", prioOne.TagName);
            Assert.AreEqual("prioOne", prioOne.Id);

            var computePrioOneStyle = window.GetComputedStyle(prioOne);
            //Assert.AreEqual("black", computePrioOneStyle.Color); // TODO this test is red (as expected). Priority of styles needs to be implemented.
        }
    }
}
