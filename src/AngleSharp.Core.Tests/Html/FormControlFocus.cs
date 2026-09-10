namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Only IHtmlAnchorElement (and area) used to actually implement DoFocus()/DoBlur() - every
    /// other HtmlElement's own base implementation was a no-op ("only certain elements can be
    /// focused"), so an <input>/<select>/<textarea>/<button> could never become the document's
    /// real ActiveElement, no matter how a caller tried (DoFocus() directly, or an extension like
    /// AngleSharp.Css's ElementExtensions.SetPseudoClass("focus"), which itself just delegates to
    /// DoFocus()). These tests pin down the fix: the four common form-control elements now focus
    /// like a real browser's native controls, disabled elements and input[type=hidden] excepted.
    /// </summary>
    [TestFixture]
    public class FormControlFocusTests
    {
        [Test]
        public void FocusingATextInputMakesItTheActiveElement()
        {
            var document = ("<input id=target type=text>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;

            target.DoFocus();

            Assert.IsTrue(target.IsFocused);
            Assert.AreSame(target, document.ActiveElement);
        }

        [Test]
        public void FocusingATextAreaMakesItTheActiveElement()
        {
            var document = ("<textarea id=target></textarea>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;

            target.DoFocus();

            Assert.IsTrue(target.IsFocused);
            Assert.AreSame(target, document.ActiveElement);
        }

        [Test]
        public void FocusingASelectMakesItTheActiveElement()
        {
            var document = ("<select id=target></select>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;

            target.DoFocus();

            Assert.IsTrue(target.IsFocused);
            Assert.AreSame(target, document.ActiveElement);
        }

        [Test]
        public void FocusingAButtonMakesItTheActiveElement()
        {
            var document = ("<button id=target>Go</button>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;

            target.DoFocus();

            Assert.IsTrue(target.IsFocused);
            Assert.AreSame(target, document.ActiveElement);
        }

        [Test]
        public void FocusingADisabledInputDoesNotFocusIt()
        {
            var document = ("<input id=target type=text disabled>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;

            target.DoFocus();

            Assert.IsFalse(target.IsFocused);
            Assert.AreNotSame(target, document.ActiveElement);
        }

        [Test]
        public void FocusingAHiddenInputDoesNotFocusIt()
        {
            // A hidden input is never rendered, and a real browser never lets it become focused
            // either - unlike the plain IsDisabled check every other form control gets, this needs
            // its own override on HtmlInputElement specifically.
            var document = ("<input id=target type=hidden>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;

            target.DoFocus();

            Assert.IsFalse(target.IsFocused);
        }

        [Test]
        public void BlurringAFocusedTextInputClearsFocus()
        {
            var document = ("<input id=target type=text>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;
            target.DoFocus();

            target.DoBlur();

            Assert.IsFalse(target.IsFocused);
            Assert.IsNull(document.ActiveElement);
        }

        /// <summary>
        /// Document.SetFocus previously just overwrote its own _focus field with no idea a
        /// different element had held it a moment ago - IsFocused's getter is derived from that
        /// single field, so it still read correctly for both elements, but nothing ever told the
        /// outgoing element it was losing focus: no blur event fired, and nothing equivalent to
        /// DoBlur() ran on it. Real, confirmed (not hypothetical): reproduced by focusing two plain
        /// inputs in sequence and observing zero blur events on the first. Fixed by moving the
        /// transition into Document.SetFocus itself, the one place that actually sees both the old
        /// and new element at once.
        /// </summary>
        [Test]
        public void MovingFocusToAnotherElementFiresBlurOnThePreviousOneAndFocusOnTheNewOne()
        {
            var document = ("<input id=a type=text><input id=b type=text>").ToHtmlDocument();
            var a = document.GetElementById("a") as IHtmlElement;
            var b = document.GetElementById("b") as IHtmlElement;
            var blurredOnA = false;
            var focusedOnB = false;
            a.AddEventListener("blur", (s, e) => blurredOnA = true);
            b.AddEventListener("focus", (s, e) => focusedOnB = true);

            a.DoFocus();
            b.DoFocus();

            Assert.IsFalse(a.IsFocused, "a should no longer be focused once b is");
            Assert.IsTrue(b.IsFocused);
            Assert.AreSame(b, document.ActiveElement);
            Assert.IsTrue(blurredOnA, "a should receive a blur event when focus moves away from it");
            Assert.IsTrue(focusedOnB, "b should receive a focus event when it becomes focused");
        }

        [Test]
        public void RefocusingTheAlreadyFocusedElementDoesNotFireARedundantFocusEvent()
        {
            var document = ("<input id=target type=text>").ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlElement;
            target.DoFocus();
            var focusCount = 0;
            target.AddEventListener("focus", (s, e) => focusCount++);

            target.DoFocus();

            Assert.AreEqual(0, focusCount, "Document.SetFocus should no-op when the target is already the focused element");
        }
    }
}
