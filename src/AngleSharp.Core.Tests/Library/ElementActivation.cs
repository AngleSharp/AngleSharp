namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Browser;
    using AngleSharp.Browser.Dom.Events;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Events;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class ElementActivationTests
    {
        private sealed class ThrowingNavigationHandler : INavigationHandler
        {
            public Boolean SupportsProtocol(String protocol) => true;

            public Task<IDocument> NavigateAsync(DocumentRequest request, CancellationToken token) =>
                throw new InvalidOperationException("navigation failed");
        }

        private static async Task<IDocument> LoadAsync(String html, IConfiguration config = null)
        {
            var context = BrowsingContext.New(config ?? Configuration.Default);
            return await context.OpenAsync(req => req.Content(html).Address("http://localhost/"));
        }

        [Test]
        public async Task DoClickAsyncSurfacesANavigationFailure()
        {
            var config = Configuration.Default.With<INavigationHandler>(_ => new ThrowingNavigationHandler());
            var document = await LoadAsync("<a id=a href='/next'>x</a>", config);
            var link = (IHtmlElement)document.GetElementById("a");

            var ex = Assert.ThrowsAsync<InvalidOperationException>(() => link.DoClickAsync());

            Assert.AreEqual("navigation failed", ex.Message);
        }

        [Test]
        public async Task DoClickReportsANavigationFailureAsAContextError()
        {
            var config = Configuration.Default.With<INavigationHandler>(_ => new ThrowingNavigationHandler());
            var document = await LoadAsync("<a id=a href='/next'>x</a>", config);
            var link = (IHtmlElement)document.GetElementById("a");
            var tracked = new TaskCompletionSource<Exception>(TaskCreationOptions.RunContinuationsAsynchronously);
            document.Context.AddEventListener(EventNames.Error, (_, ev) => tracked.TrySetResult(((TrackEvent)ev).Error));

            link.DoClick();

            var completed = await Task.WhenAny(tracked.Task, Task.Delay(5000));
            Assert.AreSame(tracked.Task, completed);
            Assert.IsInstanceOf<InvalidOperationException>(tracked.Task.Result);
        }

        [Test]
        public async Task DoClickAsyncCompletesAfterTheClickEvent()
        {
            var document = await LoadAsync("<div id=d>x</div>");
            var div = (IHtmlElement)document.GetElementById("d");
            var clicks = 0;
            div.AddEventListener(EventNames.Click, (_, _) => clicks++);

            await div.DoClickAsync();

            Assert.AreEqual(1, clicks);
        }

        [Test]
        public void SettingSelectedIndexSelectsThatOption()
        {
            var document = "<select id=s><option>a<option selected>b<option>c</select>".ToHtmlDocument();
            var select = (IHtmlSelectElement)document.GetElementById("s");

            select.Options.SelectedIndex = 2;

            Assert.AreEqual(2, select.SelectedIndex);
            Assert.AreEqual("c", select.Value);
            Assert.IsFalse(select.Options[1].IsSelected);
        }

        [Test]
        public void SettingSelectedIndexToMinusOneDeselectsEveryOption()
        {
            var document = "<select id=s><option>a<option selected>b</select>".ToHtmlDocument();
            var select = (IHtmlSelectElement)document.GetElementById("s");

            select.Options.SelectedIndex = -1;

            Assert.AreEqual(-1, select.SelectedIndex);
            Assert.IsNull(select.Value);
        }

        [Test]
        public async Task ClickingTheSummaryTogglesItsDetails()
        {
            var document = await LoadAsync("<details id=d><summary id=s>s</summary>body</details>");
            var details = (IHtmlDetailsElement)document.GetElementById("d");
            var summary = (IHtmlElement)document.GetElementById("s");

            await summary.DoClickAsync();
            Assert.IsTrue(details.IsOpen);

            await summary.DoClickAsync();
            Assert.IsFalse(details.IsOpen);
        }

        [Test]
        public async Task ClickingACanceledSummaryDoesNotToggleItsDetails()
        {
            var document = await LoadAsync("<details id=d><summary id=s>s</summary>body</details>");
            var details = (IHtmlDetailsElement)document.GetElementById("d");
            var summary = (IHtmlElement)document.GetElementById("s");
            summary.AddEventListener(EventNames.Click, (_, ev) => ev.Cancel());

            await summary.DoClickAsync();

            Assert.IsFalse(details.IsOpen);
        }

        [TestCase("<details id=d><summary>first</summary><summary id=s>second</summary></details>")]
        [TestCase("<details id=d><div><summary id=s>nested</summary></div></details>")]
        public async Task ClickingASummaryThatIsNotTheDetailsSummaryDoesNothing(String markup)
        {
            var document = await LoadAsync(markup);
            var details = (IHtmlDetailsElement)document.GetElementById("d");
            var summary = (IHtmlElement)document.GetElementById("s");

            await summary.DoClickAsync();

            Assert.IsFalse(details.IsOpen);
        }
    
        private static IHtmlDocument Form(String html) => (IHtmlDocument)html.ToHtmlDocument();

        [Test]
        public void ClickDispatchedAtASubmitButtonSubmitsItsForm()
        {
            var document = Form("<form id=f><input name=x><button id=b>Add</button></form>");
            var submits = new List<Event>();
            document.GetElementById("f").AddEventListener(EventNames.Submit, (_, ev) => { submits.Add(ev); ev.Cancel(); });

            var proceeds = document.GetElementById("b").Dispatch(new AngleSharp.Html.Dom.Events.MouseEvent(EventNames.Click, bubbles: true, cancelable: true));

            Assert.IsTrue(proceeds);
            Assert.AreEqual(1, submits.Count);
            Assert.AreSame(document.GetElementById("b"), ((AngleSharp.Html.Dom.Events.SubmitEvent)submits[0]).Submitter);
        }

        [Test]
        public void ClickDispatchedAtTheContentOfAButtonSubmitsItsForm()
        {
            var document = Form("<form id=f><button><span id=s>Add</span></button></form>");
            var submits = 0;
            document.GetElementById("f").AddEventListener(EventNames.Submit, (_, ev) => { submits++; ev.Cancel(); });

            document.GetElementById("s").Dispatch(new AngleSharp.Html.Dom.Events.MouseEvent(EventNames.Click, bubbles: true, cancelable: true));

            Assert.AreEqual(1, submits);
        }

        [Test]
        public void CanceledClickDispatchedAtASubmitButtonDoesNotSubmit()
        {
            var document = Form("<form id=f><button id=b>Add</button></form>");
            var submits = 0;
            var button = document.GetElementById("b");
            document.GetElementById("f").AddEventListener(EventNames.Submit, (_, ev) => { submits++; ev.Cancel(); });
            button.AddEventListener(EventNames.Click, (_, ev) => ev.Cancel());

            var proceeds = button.Dispatch(new AngleSharp.Html.Dom.Events.MouseEvent(EventNames.Click, bubbles: true, cancelable: true));

            Assert.IsFalse(proceeds);
            Assert.AreEqual(0, submits);
        }

        [Test]
        public void ClickDispatchedAtACheckboxChecksItBeforeTheListenersSeeIt()
        {
            var document = Form("<input type=checkbox id=c>");
            var checkbox = (IHtmlInputElement)document.GetElementById("c");
            var seen = new List<Boolean>();
            var changes = 0;
            checkbox.AddEventListener(EventNames.Click, (_, _) => seen.Add(checkbox.IsChecked));
            checkbox.AddEventListener(EventNames.Change, (_, _) => changes++);

            checkbox.Dispatch(new AngleSharp.Html.Dom.Events.MouseEvent(EventNames.Click, bubbles: true, cancelable: true));

            Assert.IsTrue(checkbox.IsChecked);
            CollectionAssert.AreEqual(new[] { true }, seen);
            Assert.AreEqual(1, changes);
        }

        [Test]
        public void CanceledClickDispatchedAtACheckboxLeavesItAsItWas()
        {
            var document = Form("<input type=checkbox id=c>");
            var checkbox = (IHtmlInputElement)document.GetElementById("c");
            checkbox.AddEventListener(EventNames.Click, (_, ev) => ev.Cancel());

            checkbox.Dispatch(new AngleSharp.Html.Dom.Events.MouseEvent(EventNames.Click, bubbles: true, cancelable: true));

            Assert.IsFalse(checkbox.IsChecked);
        }

        [Test]
        public void DispatchedClickThatIsNoMouseEventDoesNothingElse()
        {
            var document = Form("<input type=checkbox id=c>");
            var checkbox = (IHtmlInputElement)document.GetElementById("c");
            var clicks = 0;
            checkbox.AddEventListener(EventNames.Click, (_, _) => clicks++);

            checkbox.Dispatch(new Event(EventNames.Click, bubbles: true, cancelable: true));

            Assert.AreEqual(1, clicks);
            Assert.IsFalse(checkbox.IsChecked);
        }

        [Test]
        public void ClickOfASubmitButtonSubmitsOnceWithATrustedEvent()
        {
            var document = Form("<form id=f><button id=b>Add</button></form>");
            var submits = new List<Event>();
            var clicks = 0;
            var button = (IHtmlElement)document.GetElementById("b");
            button.AddEventListener(EventNames.Click, (_, _) => clicks++);
            document.GetElementById("f").AddEventListener(EventNames.Submit, (_, ev) => { submits.Add(ev); ev.Cancel(); });

            button.DoClick();

            Assert.AreEqual(1, clicks);
            Assert.AreEqual(1, submits.Count);
            Assert.IsTrue(submits[0].IsTrusted);
        }
}
}
