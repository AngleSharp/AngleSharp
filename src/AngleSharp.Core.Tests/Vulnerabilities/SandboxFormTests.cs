namespace AngleSharp.Core.Tests.Vulnerabilities
{
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
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

            var result = withSubmitter ? await form.SubmitAsync(document.QuerySelector<IHtmlElement>("button")) : await form.SubmitAsync();

            Assert.IsNull(result);
            Assert.AreSame(document, context.Active);
            Assert.IsNull(context.FindChild("new-window"));
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

            Assert.AreEqual(blocked, form.GetSubmission() is null);
            Assert.AreEqual(blocked, form.GetSubmission(submitter) is null);
        }
    }
}
