namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Css.Parser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System.Threading.Tasks;

    /// <summary>
    /// Found on the renderer test pages (anglesharp-tests.anglevisions.com/renderer/): dialogs
    /// closed by their forms, templates, form-associated custom elements and ::part().
    /// </summary>
    [TestFixture]
    public class RendererTestsPageTests
    {
        private static Task<IDocument> OpenAsync(string html) =>
            BrowsingContext.New(Configuration.Default).OpenAsync(req => req.Content(html));

        [Test]
        public async Task FormOfMethodDialogClosesItsDialogWithTheSubmitterValue()
        {
            var document = await OpenAsync("<dialog open><form method=dialog><button value=confirm>OK</button></form></dialog>");
            var dialog = (IHtmlDialogElement)document.QuerySelector("dialog");
            var button = (IHtmlButtonElement)document.QuerySelector("button");

            var navigated = await ((IHtmlFormElement)document.QuerySelector("form")).SubmitAsync(button);

            Assert.IsNull(navigated);
            Assert.IsFalse(dialog.Open);
            Assert.AreEqual("confirm", dialog.ReturnValue);
        }

        [Test]
        public async Task ShowModalOfADisconnectedDialogThrows()
        {
            var document = await OpenAsync("");
            var dialog = (IHtmlDialogElement)document.CreateElement("dialog");

            var error = Assert.Throws<DomException>(() => dialog.ShowModal());
            Assert.AreEqual((int)DomError.InvalidState, error.Code);
        }

        [Test]
        public async Task ScriptsInATemplateDoNotRun()
        {
            var runs = 0;
            var config = Configuration.Default.WithScripts(new CallbackScriptEngine(_ => runs++));
            var document = await BrowsingContext.New(config).OpenAsync(m => m.Content("<template><script type=c-sharp>run</script></template><script type=c-sharp>run</script>"));

            Assert.IsNotNull(document);
            Assert.AreEqual(1, runs);
        }

        [Test]
        public async Task InnerHtmlOfATemplateIsItsContent()
        {
            var document = await OpenAsync("");
            var template = (IHtmlTemplateElement)document.CreateElement("template");

            template.InnerHtml = "<b>x</b><i>y</i>";

            Assert.AreEqual(0, template.ChildNodes.Length);
            Assert.AreEqual(2, template.Content.ChildNodes.Length);
            Assert.AreEqual("<b>x</b><i>y</i>", template.InnerHtml);
        }

        [Test]
        public async Task FormAssociatedCustomElementIsSubmittedAndValidatedWithItsForm()
        {
            var document = await OpenAsync("<form><rt-rating name=stars></rt-rating></form>");
            var form = (IHtmlFormElement)document.QuerySelector("form");
            var rating = (IHtmlElement)document.QuerySelector("rt-rating");
            var internals = rating.AttachInternals();

            Assert.AreEqual(form, internals.Form);
            internals.SetValidity(new ValidityStateFlags { ValueMissing = true }, "Pick a rating");
            Assert.IsFalse(form.CheckValidity());

            internals.SetValidity();
            internals.SetFormValue("4");
            Assert.IsTrue(form.CheckValidity());
            StringAssert.Contains("stars=4", form.GetSubmission().Target.Query);
            Assert.Throws<DomException>(() => rating.AttachInternals());
        }

        [Test]
        public async Task SlotsTellTheirAssignedNodesAndFallback()
        {
            var document = await OpenAsync("<div id=host><span slot=icon>*</span>Item</div><div id=empty></div>");
            var host = document.GetElementById("host");
            var root = host.AttachShadow();
            root.InnerHtml = "<slot name=icon></slot><slot>fallback</slot>";
            var empty = document.GetElementById("empty").AttachShadow();
            empty.InnerHtml = "<slot>fallback</slot>";
            var slots = root.QuerySelectorAll("slot");
            var unassigned = (IHtmlSlotElement)empty.QuerySelector("slot");

            Assert.AreEqual(1, ((IHtmlSlotElement)slots[0]).AssignedNodes().Length);
            Assert.AreEqual("Item", ((IHtmlSlotElement)slots[1]).AssignedNodes()[0].TextContent);
            Assert.AreEqual(0, unassigned.AssignedNodes().Length);
            Assert.AreEqual("fallback", unassigned.AssignedNodes(new AssignedNodesOptions { Flatten = true })[0].TextContent);
        }

        [Test]
        public async Task ClosedShadowRootIsHiddenFromScripts()
        {
            var document = await OpenAsync("<div id=open></div><div id=closed></div>");
            var open = document.GetElementById("open");
            var closed = document.GetElementById("closed");
            open.AttachShadow(ShadowRootMode.Open);
            closed.AttachShadow(ShadowRootMode.Closed);

            Assert.IsNotNull(open.GetShadowRoot());
            Assert.IsNull(closed.GetShadowRoot());
            Assert.IsNotNull(closed.ShadowRoot);
            var error = Assert.Throws<DomException>(() => open.AttachShadow());
            Assert.AreEqual((int)DomError.NotSupported, error.Code);
        }

        [Test]
        public async Task CustomStatesAreMatchedByState()
        {
            var document = await OpenAsync("<rt-counter></rt-counter>");
            var counter = (IHtmlElement)document.QuerySelector("rt-counter");
            var internals = counter.AttachInternals();

            Assert.IsFalse(counter.Matches(":state(odd)"));
            internals.States.Add("odd");
            Assert.IsTrue(counter.Matches(":state(odd)"));
            Assert.IsTrue(internals.States.Delete("odd"));
            Assert.IsFalse(counter.Matches(":state(odd)"));
        }

        [Test]
        public async Task SetHtmlUnsafeCreatesDeclarativeShadowRoots()
        {
            var document = await OpenAsync("");
            var div = document.CreateElement("div");

            div.InnerHtml = "<div><template shadowrootmode=open><b>s</b></template></div>";
            Assert.IsNull(div.FirstElementChild.ShadowRoot);

            div.SetHtmlUnsafe("<div><template shadowrootmode=open><b>s</b></template></div>");
            Assert.AreEqual("<b>s</b>", div.FirstElementChild.ShadowRoot?.InnerHtml);
        }

        [Test]
        public async Task FocusMovesRightAway()
        {
            var document = await OpenAsync("<input id=a><input id=b>");
            var a = (IHtmlElement)document.GetElementById("a");
            var events = 0;
            a.AddEventListener("focus", (_, _) => events++);

            a.DoFocus();

            Assert.AreEqual(a, document.ActiveElement);
            Assert.AreEqual(1, events);
        }

        [Test]
        public async Task TemplateContentsBelongToAnInertDocument()
        {
            var document = await OpenAsync("<template><p>a</p></template>");
            var template = (IHtmlTemplateElement)document.QuerySelector("template");

            Assert.AreNotSame(document, template.Content.Owner);
            // with the services of the page, e.g. for the style of a cloned element
            Assert.AreSame(document.Context, template.Content.Owner.Context);
            Assert.AreSame(template.Content.Owner, template.Content.FirstChild.Owner);
            Assert.AreSame(document, document.Import(template.Content, true).Owner);
        }

        [Test]
        public void PartPseudoElementIsParsed()
        {
            var selector = new CssSelectorParser().ParseSelector("rt-badge::part(label)");

            Assert.IsNotNull(selector);
            Assert.AreEqual("rt-badge::part(label)", selector.Text);
        }
    }
}
