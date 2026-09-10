namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class FormOwnerTests
    {
        // https://html.spec.whatwg.org/multipage/form-control-infrastructure.html#reset-the-form-owner
        [TestCase("input")]
        [TestCase("button")]
        [TestCase("select")]
        [TestCase("textarea")]
        [TestCase("fieldset")]
        [TestCase("object")]
        [TestCase("output")]
        public void ExplicitFormOverridesAncestor(String tagName)
        {
            var document = "<form id=first></form><form id=second></form>".ToHtmlDocument();
            var control = (HtmlFormControlElement)document.CreateElement(tagName);
            control.SetAttribute("form", "second");
            document.Forms[0].AppendChild(control);

            Assert.AreSame(document.Forms[1], control.Form);
            Assert.AreEqual(0, document.Forms[0].Elements.Length);
            Assert.AreSame(control, document.Forms[1].Elements[0]);
        }

        [TestCase("", "<form id=''></form>")]
        [TestCase("missing", "")]
        [TestCase("target", "<div id=target></div>")]
        [TestCase("target", "<div id=target></div><form id=target></form>")]
        [TestCase("TARGET", "<form id=target></form>")]
        public void ExplicitInvalidTargetSuppressesAncestor(String formId, String target)
        {
            var document = ("<form id=ancestor><input></form>" + target).ToHtmlDocument();
            var input = document.QuerySelector<IHtmlInputElement>("input");
            input.SetAttribute("form", formId);

            Assert.IsNull(input.Form);
        }

        [Test]
        public void ParsedExternalAndNestedControlsUseFirstMatchingForm()
        {
            var document = ("<form id=ancestor><input form=target></form>" +
                "<input form=target><form id=target></form><form id=target></form>").ToHtmlDocument();

            foreach (var input in document.QuerySelectorAll<IHtmlInputElement>("input"))
            {
                Assert.AreSame(document.Forms[1], input.Form);
            }
        }

        [Test]
        public void FormAttributeChangesReassociateControl()
        {
            var document = "<form id=first><input></form><form id=second></form>".ToHtmlDocument();
            var input = document.QuerySelector<IHtmlInputElement>("input");
            Assert.AreSame(document.Forms[0], input.Form);

            input.SetAttribute("form", "second");
            Assert.AreSame(document.Forms[1], input.Form);

            input.SetAttribute("form", "missing");
            Assert.IsNull(input.Form);

            input.RemoveAttribute("form");
            Assert.AreSame(document.Forms[0], input.Form);
        }

        [Test]
        public void TargetIdAndTreeChangesReassociateControl()
        {
            var document = "<input form=target><form id=target></form><form id=target></form>".ToHtmlDocument();
            var input = document.QuerySelector<IHtmlInputElement>("input");
            var first = document.Forms[0];
            var second = document.Forms[1];
            Assert.AreSame(first, input.Form);

            first.Id = "other";
            Assert.AreSame(second, input.Form);

            first.Id = "target";
            Assert.AreSame(first, input.Form);

            document.Body.InsertBefore(second, first);
            Assert.AreSame(second, input.Form);

            second.Remove();
            Assert.AreSame(first, input.Form);

            var blocker = document.CreateElement("div");
            blocker.Id = "target";
            document.Body.InsertBefore(blocker, first);
            Assert.IsNull(input.Form);

            blocker.Remove();
            Assert.AreSame(first, input.Form);
        }

        [Test]
        public void DisconnectingAndReconnectingUsesTheAppropriateAssociation()
        {
            var document = "<form id=first><input form=second></form><form id=second></form>".ToHtmlDocument();
            var first = document.Forms[0];
            var second = document.Forms[1];
            var input = document.QuerySelector<IHtmlInputElement>("input");
            Assert.AreSame(second, input.Form);

            first.Remove();
            Assert.AreSame(first, input.Form);

            input.Remove();
            Assert.IsNull(input.Form);

            first.AppendChild(input);
            Assert.AreSame(first, input.Form);

            document.Body.AppendChild(first);
            Assert.AreSame(second, input.Form);
        }

        [Test]
        public void DisconnectedFragmentDoesNotResolveExplicitTargets()
        {
            var document = "<form id=target></form>".ToHtmlDocument();
            var fragment = document.CreateDocumentFragment();
            var ancestor = document.CreateElement("form");
            var target = document.CreateElement("form");
            target.Id = "target";
            var input = (IHtmlInputElement)document.CreateElement("input");
            input.SetAttribute("form", "target");
            fragment.AppendChild(ancestor);
            fragment.AppendChild(target);
            ancestor.AppendChild(input);
            Assert.AreSame(ancestor, input.Form);

            fragment.AppendChild(input);
            Assert.IsNull(input.Form);
        }

        [Test]
        public void MovingWithoutExplicitFormUsesNearestAncestor()
        {
            var document = "<form id=first><input></form><form id=second></form>".ToHtmlDocument();
            var input = document.QuerySelector<IHtmlInputElement>("input");
            var second = document.Forms[1];
            second.AppendChild(input);
            Assert.AreSame(second, input.Form);

            var nested = document.CreateElement("form");
            second.AppendChild(nested);
            nested.AppendChild(input);
            Assert.AreSame(nested, input.Form);
        }

        [Test]
        public void MovingToAnotherDocumentResolvesThatDocumentsTarget()
        {
            var first = "<form id=target><input form=target></form>".ToHtmlDocument();
            var second = "<form id=target></form>".ToHtmlDocument();
            var input = first.QuerySelector<IHtmlInputElement>("input");
            Assert.AreSame(first.Forms[0], input.Form);

            second.Body.AppendChild(input);
            Assert.AreSame(second.Forms[0], input.Form);
        }

        [Test]
        public void ShadowTreeTargetsStayWithinTheControlsTree()
        {
            var document = "<form id=target></form><div></div>".ToHtmlDocument();
            var host = document.QuerySelector("div");
            var shadow = host.AttachShadow(ShadowRootMode.Open);
            shadow.InnerHtml = "<form id=ancestor><input form=target></form><form id=target></form>";
            var input = shadow.QuerySelector<IHtmlInputElement>("input");
            var target = shadow.GetElementById("target");
            var ancestor = shadow.GetElementById("ancestor");
            Assert.AreSame(target, input.Form);

            target.Remove();
            Assert.IsNull(input.Form);

            host.Remove();
            Assert.AreSame(ancestor, input.Form);
        }
    }
}
