namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Html.Dom;
    using NUnit.Framework;

    [TestFixture]
    public class TextControlSelectionTests
    {
        [Test]
        public void TheCursorOfAnUntouchedInputIsAtTheEndOfItsValue()
        {
            var document = ("<input id=target value=abc>").ToHtmlDocument();
            var input = (IHtmlInputElement)document.GetElementById("target");

            Assert.AreEqual(3, input.SelectionStart);
            Assert.AreEqual(3, input.SelectionEnd);

            input.DefaultValue = "hello";

            Assert.AreEqual(5, input.SelectionStart);
        }

        [Test]
        public void SettingTheValueOfAnInputMovesTheCursorToItsEnd()
        {
            var document = ("<input id=target value=abc>").ToHtmlDocument();
            var input = (IHtmlInputElement)document.GetElementById("target");
            input.SelectionStart = 1;
            input.SelectionEnd = 2;

            input.Value = "hello";

            Assert.AreEqual(5, input.SelectionStart);
            Assert.AreEqual(5, input.SelectionEnd);
            Assert.AreEqual("none", input.SelectionDirection);
        }

        [Test]
        public void SettingTheSameValueOfAnInputKeepsTheSelection()
        {
            var document = ("<input id=target value=abc>").ToHtmlDocument();
            var input = (IHtmlInputElement)document.GetElementById("target");
            input.Select(1, 2);

            input.Value = "abc";

            Assert.AreEqual(1, input.SelectionStart);
            Assert.AreEqual(2, input.SelectionEnd);
        }

        [Test]
        public void SettingTheValueOfATextAreaMovesTheCursorToItsEnd()
        {
            var document = ("<textarea id=target>abc</textarea>").ToHtmlDocument();
            var area = (IHtmlTextAreaElement)document.GetElementById("target");
            area.SelectionStart = 0;

            area.Value = "line\nnext";

            Assert.AreEqual(9, area.SelectionStart);
            Assert.AreEqual(9, area.SelectionEnd);
        }
    }
}
