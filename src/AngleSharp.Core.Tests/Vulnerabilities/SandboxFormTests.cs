namespace AngleSharp.Core.Tests.Vulnerabilities
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Dom.Events;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [TestFixture]
    public class SandboxFormTests
    {
        [TestCase(false)]
        [TestCase(true)]
        public async Task BlockedSubmissionDoesNotNavigateOrCreateItsTarget(Boolean withSubmitter)
        {
            var parent = BrowsingContext.New(Configuration.Default);
            var context = parent.CreateChild(null, Sandboxes.Forms);
            var document = await context.OpenAsync(request => request
                .Address("https://sandbox.example/form")
                .Content("<form action='/submit' target='new-window'><button>Send</button></form>"));
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var errors = new List<Exception>();
            context.AddEventListener(EventNames.Error, (_, ev) => errors.Add(((TrackEvent)ev).Error));

            var result = withSubmitter ? await form.SubmitAsync(document.QuerySelector<IHtmlElement>("button")) : await form.SubmitAsync();

            Assert.IsNull(result);
            Assert.AreSame(document, context.Active);
            Assert.IsNull(context.FindChild("new-window"));
            Assert.AreEqual(1, errors.Count);
            var error = (DomException)errors[0];
            Assert.AreEqual((Int32)DomError.Security, error.Code);
            StringAssert.Contains("Blocked form submission", error.Message);
        }

        [TestCase(false)]
        [TestCase(true)]
        public async Task FormSubmissionHonorsTheDocumentsContextSandbox(Boolean blocked)
        {
            // https://html.spec.whatwg.org/multipage/form-control-infrastructure.html#form-submission-algorithm
            var parent = BrowsingContext.New(Configuration.Default);
            var context = parent.CreateChild(null, blocked ? Sandboxes.Forms : Sandboxes.None);
            var document = await context.OpenAsync(request => request
                .Address("https://sandbox.example/form")
                .Content("<form action='/submit'><input name='value' value='42'><button>Send</button></form>"));
            var form = document.QuerySelector<IHtmlFormElement>("form");
            var submitter = document.QuerySelector<IHtmlElement>("button");
            var errors = new List<Exception>();
            context.AddEventListener(EventNames.Error, (_, ev) => errors.Add(((TrackEvent)ev).Error));

            Assert.AreEqual(blocked, form.GetSubmission() is null);
            Assert.AreEqual(blocked, form.GetSubmission(submitter) is null);
            Assert.AreEqual(blocked ? 2 : 0, errors.Count);
            foreach (var error in errors)
            {
                Assert.AreEqual((Int32)DomError.Security, ((DomException)error).Code);
                StringAssert.Contains("Blocked form submission", error.Message);
            }
        }
    }
}
