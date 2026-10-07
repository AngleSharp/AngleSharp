namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class BarredValidationTests
    {
        [TestCase("<input disabled>")]
        [TestCase("<select disabled></select>")]
        [TestCase("<textarea disabled></textarea>")]
        [TestCase("<button disabled>Submit</button>")]
        [TestCase("<input readonly>")]
        [TestCase("<textarea readonly></textarea>")]
        [TestCase("<input type=hidden>")]
        [TestCase("<button type=button>Action</button>")]
        [TestCase("<input type=reset>")]
        [TestCase("<object></object>")]
        public void ControlBarredFromValidationPassesCheck(String markup)
        {
            var document = markup.ToHtmlDocument();
            var control = (IValidation)document.Body.FirstElementChild;
            control.SetCustomValidity("Custom error");

            Assert.IsFalse(control.WillValidate);
            Assert.IsTrue(control.CheckValidity());
        }

        [TestCase("<input id=control>", true)]
        [TestCase("<legend><input id=control></legend>", false)]
        public void DisabledFieldsetBarsControlsExceptItsFirstLegend(String markup, Boolean barred)
        {
            var document = ("<fieldset disabled>" + markup + "</fieldset>").ToHtmlDocument();
            var control = (IValidation)document.GetElementById("control");
            control.SetCustomValidity("Custom error");

            Assert.AreEqual(!barred, control.WillValidate);
            Assert.AreEqual(barred, control.CheckValidity());
        }

        [Test]
        public void EnabledControlStillReportsItsCurrentValidity()
        {
            var document = "<input disabled>".ToHtmlDocument();
            var control = document.QuerySelector<IHtmlInputElement>("input");
            control.SetCustomValidity("Custom error");
            Assert.IsTrue(control.CheckValidity());
            control.IsDisabled = false;
            Assert.IsTrue(control.WillValidate);
            Assert.IsFalse(control.CheckValidity());
            control.SetCustomValidity(String.Empty);
            Assert.IsTrue(control.CheckValidity());
        }
    }
}
