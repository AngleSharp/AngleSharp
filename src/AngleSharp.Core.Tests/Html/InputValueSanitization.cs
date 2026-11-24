namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System.Net;

    [TestFixture]
    public class InputValueSanitizationTests
    {
        [TestCase("date", "foobar", "")]
        [TestCase("date", "2020-01-31", "2020-01-31")]
        [TestCase("month", "foobar", "")]
        [TestCase("month", "2020-01", "2020-01")]
        [TestCase("week", "foobar", "")]
        [TestCase("week", "2020-W05", "2020-W05")]
        [TestCase("time", "foobar", "")]
        [TestCase("time", "12:30", "12:30")]
        [TestCase("datetime-local", "foobar", "")]
        [TestCase("datetime-local", "2024-05-03T13:05", "2024-05-03T13:05")]
        [TestCase("datetime-local", "2024-05-03T13:05:30", "2024-05-03T13:05:30")]
        [TestCase("number", "foobar", "")]
        [TestCase("number", "1e3", "1e3")]
        [TestCase("range", "foobar", "50")]
        [TestCase("range", "200", "100")]
        [TestCase("range", "20", "20")]
        [TestCase("color", "foobar", "#000000")]
        [TestCase("color", "#AABBCC", "#aabbcc")]
        [TestCase("text", "foobar", "foobar")]
        public void ValueIsSanitizedByTheType(string type, string value, string expected)
        {
            var document = "".ToHtmlDocument();
            var input = (IHtmlInputElement)document.CreateElement("input");
            input.SetAttribute("type", type);
            input.Value = value;
            Assert.AreEqual(expected, input.Value);
        }

        [Test]
        public void DefaultValueOfARangeIsBetweenItsMinimumAndMaximum()
        {
            var document = "<input type=range min=10 max=20>".ToHtmlDocument();
            var input = (IHtmlInputElement)document.QuerySelector("input");
            Assert.AreEqual("15", input.Value);
        }

        [TestCase("url", "foo", "http://foo.org")]
        [TestCase("email", "foo", "foo@bar.org")]
        public void FieldIsValidAgainOnceItHasAValidValue(string type, string invalid, string valid)
        {
            var document = "".ToHtmlDocument();
            var input = (IHtmlInputElement)document.CreateElement("input");
            input.SetAttribute("type", type);
            input.Value = invalid;
            Assert.IsFalse(input.Validity.IsValid);
            input.Value = valid;
            Assert.IsTrue(input.Validity.IsValid);
            input.Value = invalid;
            Assert.IsFalse(input.Validity.IsValid);
        }

        [Test]
        public void LabelsOfAFieldAreTheLabelsForIt()
        {
            var document = "<label for=a>A</label><input id=a><label>B <input id=b></label><label for=b>C</label>".ToHtmlDocument();
            var a = (IHtmlInputElement)document.GetElementById("a");
            var b = (IHtmlInputElement)document.GetElementById("b");

            Assert.AreEqual(1, a.Labels.Length);
            Assert.AreEqual("A", a.Labels[0].TextContent);
            // the one it is in, and the one for it
            Assert.AreEqual(2, b.Labels.Length);
            Assert.AreEqual("B ", b.Labels[0].TextContent);
            Assert.AreEqual("C", b.Labels[1].TextContent);
        }

        [Test]
        public void LabelWithoutForAttributeLabelsItsFirstLabelableDescendant()
        {
            var document = "<label id=l>Name <input id=a><input id=b></label>".ToHtmlDocument();
            var label = (IHtmlLabelElement)document.GetElementById("l");
            var a = (IHtmlInputElement)document.GetElementById("a");

            Assert.AreSame(a, label.Control);
            Assert.AreEqual(1, a.Labels.Length);
        }

        [Test]
        public void LabelsOfAFieldFollowTheDocument()
        {
            var document = "<input id=a>".ToHtmlDocument();
            var input = (IHtmlInputElement)document.GetElementById("a");
            Assert.AreEqual(0, input.Labels.Length);
            var label = (IHtmlLabelElement)document.CreateElement("label");
            label.HtmlFor = "a";
            document.Body.AppendChild(label);
            Assert.AreEqual(1, input.Labels.Length);
        }

        [Test]
        public void TypeOfAMenuIsAToolbarByDefaultAndAContextMenuInOne()
        {
            var document = "<menu id=a></menu><menu id=b type=context><menu id=c></menu></menu><menu id=d type=foo></menu>".ToHtmlDocument();

            Assert.AreEqual("toolbar", ((IHtmlMenuElement)document.GetElementById("a")).Type);
            Assert.AreEqual("context", ((IHtmlMenuElement)document.GetElementById("b")).Type);
            Assert.AreEqual("context", ((IHtmlMenuElement)document.GetElementById("c")).Type);
            Assert.AreEqual("toolbar", ((IHtmlMenuElement)document.GetElementById("d")).Type);
        }

        [Test]
        public void EmptyCanvasGivesAnEmptyDataUrl()
        {
            var document = "<canvas width=0 height=0></canvas><canvas></canvas>".ToHtmlDocument();
            var canvases = document.QuerySelectorAll("canvas");

            Assert.AreEqual("data:,", ((IHtmlCanvasElement)canvases[0]).ToDataUrl());
            // without a rendering service there is no image either
            Assert.AreEqual("data:,", ((IHtmlCanvasElement)canvases[1]).ToDataUrl("image/png"));
        }

        [Test]
        public void PermanentRedirectIsARedirect()
        {
            Assert.IsTrue(((HttpStatusCode)308).IsRedirected());
            Assert.IsTrue(HttpStatusCode.TemporaryRedirect.IsRedirected());
            Assert.IsFalse(HttpStatusCode.OK.IsRedirected());
        }
    }
}
