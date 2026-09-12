namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Every attribute change has to run the attribute change steps and queue a mutation record,
    /// no matter which IDL member performed the write. These cover the members that used to write
    /// the content attribute silently.
    /// </summary>
    [TestFixture]
    public class AttributeObservabilityTests
    {
        private sealed class RecordingAttributeObserver : IAttributeObserver
        {
            public List<Tuple<IElement, String, String>> Changes { get; } = new List<Tuple<IElement, String, String>>();

            public void NotifyChange(IElement host, String name, String value)
            {
                Changes.Add(Tuple.Create(host, name, value));
            }
        }

        private sealed class ReentrantAttributeObserver : IAttributeObserver
        {
            public Int32 Count { get; private set; }

            public void NotifyChange(IElement host, String name, String value)
            {
                if (name == "class")
                {
                    Count++;

                    // An observer is allowed to write the very list that notified it. This has to
                    // terminate instead of recursing through the token list forever.
                    host.ClassList.Add("observed");
                }
            }
        }

        private sealed class RewritingAttributeObserver : IAttributeObserver
        {
            private readonly String _name;
            private readonly String _replacement;

            public RewritingAttributeObserver(String name, String replacement)
            {
                _name = name;
                _replacement = replacement;
            }

            public void NotifyChange(IElement host, String name, String value)
            {
                if (name == _name && value != _replacement)
                {
                    // An observer may write the same attribute with a value of its own. For an
                    // embedder this is user script inside a custom element's attributeChangedCallback.
                    host.SetAttribute(_name, _replacement);
                }
            }
        }

        private static IDocument Observed(String source, RecordingAttributeObserver observer)
        {
            return source.ToHtmlDocument(Configuration.Default.With(observer));
        }

        private static MutationObserver Recorder(List<IMutationRecord> records)
        {
            return new MutationObserver((mutations, _) => records.AddRange(mutations));
        }

        [Test]
        public void ClassListAddQueuesAMutationRecord()
        {
            var document = "<div id=target></div>".ToHtmlDocument();
            var target = document.GetElementById("target");
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributes: true, attributeOldValue: true);
            target.ClassList.Add("alpha");

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual("class", records[0].AttributeName);
            Assert.AreEqual("alpha", target.ClassName);
        }

        [Test]
        public void ClassListRemoveQueuesAMutationRecord()
        {
            var document = "<div id=target class='alpha beta'></div>".ToHtmlDocument();
            var target = document.GetElementById("target");
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributes: true, attributeOldValue: true);
            target.ClassList.Remove("alpha");

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual("class", records[0].AttributeName);
            Assert.AreEqual("alpha beta", records[0].PreviousValue);
            Assert.AreEqual("beta", target.ClassName);
        }

        [Test]
        public void ClassListToggleQueuesAMutationRecord()
        {
            var document = "<div id=target></div>".ToHtmlDocument();
            var target = document.GetElementById("target");
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributes: true);

            Assert.IsTrue(target.ClassList.Toggle("alpha"));
            Assert.IsFalse(target.ClassList.Toggle("alpha"));
            Assert.AreEqual(2, records.Count);
            Assert.AreEqual(String.Empty, target.ClassName);
        }

        [Test]
        public void ClassListAddPassesAnAttributeFilterOnClass()
        {
            var document = "<div id=target></div>".ToHtmlDocument();
            var target = document.GetElementById("target");
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributeFilter: new[] { "class" });
            target.ClassList.Add("alpha");

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual("class", records[0].AttributeName);
        }

        [Test]
        public void ClassListAddNotifiesAnAttributeObserverExactlyOnce()
        {
            var observer = new RecordingAttributeObserver();
            var document = Observed("<div id=target></div>", observer);
            var target = document.GetElementById("target");
            observer.Changes.Clear();

            target.ClassList.Add("alpha");

            Assert.AreEqual(1, observer.Changes.Count);
            Assert.AreEqual("class", observer.Changes[0].Item2);
            Assert.AreEqual("alpha", observer.Changes[0].Item3);
            Assert.AreSame(target, observer.Changes[0].Item1);
        }

        [Test]
        public void ClassListWriteDoesNotFeedBackIntoTheClassList()
        {
            var observer = new RecordingAttributeObserver();
            var document = Observed("<div id=target class='alpha'></div>", observer);
            var target = document.GetElementById("target");
            var list = target.ClassList;
            observer.Changes.Clear();

            list.Add("beta");
            list.Add("gamma");
            list.Remove("alpha");

            // One notification per write, and the list is neither re-parsed into a different shape
            // nor applied twice.
            Assert.AreEqual(3, observer.Changes.Count);
            Assert.AreSame(list, target.ClassList);
            Assert.AreEqual(2, list.Length);
            Assert.AreEqual("beta", list[0]);
            Assert.AreEqual("gamma", list[1]);
            Assert.AreEqual("beta gamma", target.ClassName);
            Assert.AreEqual("beta gamma", target.GetAttribute("class"));
        }

        [Test]
        public void ClassListWriteThatChangesNothingIsNotObservable()
        {
            var observer = new RecordingAttributeObserver();
            var document = Observed("<div id=target class='alpha'></div>", observer);
            var target = document.GetElementById("target");
            observer.Changes.Clear();

            target.ClassList.Add("alpha");
            target.ClassList.Remove("beta");

            Assert.AreEqual(0, observer.Changes.Count);
        }

        [Test]
        public void ClassListWriteFromAnObserverTerminates()
        {
            var reentrant = new ReentrantAttributeObserver();
            var document = "<div id=target></div>".ToHtmlDocument(Configuration.Default.With(reentrant));
            var target = document.GetElementById("target");

            target.ClassList.Add("alpha");

            // The outer write plus the one the observer made, and nothing beyond it.
            Assert.AreEqual(2, reentrant.Count);
            Assert.AreEqual("alpha observed", target.ClassName);
            Assert.AreEqual(2, target.ClassList.Length);
        }

        [Test]
        public void ClassListAgreesWithTheAttributeAfterAnObserverRewritesIt()
        {
            var config = Configuration.Default.With(new RewritingAttributeObserver("class", "zzz"));
            var document = "<div id=target></div>".ToHtmlDocument(config);
            var target = document.GetElementById("target");

            target.ClassList.Add("alpha");

            // The observer's write is not ours, so it has to be parsed into the list like any other
            // setAttribute. The list and the content attribute must not diverge.
            Assert.AreEqual("zzz", target.GetAttribute("class"));
            Assert.AreEqual("zzz", target.ClassName);
            Assert.AreEqual(1, target.ClassList.Length);
            Assert.AreEqual("zzz", target.ClassList[0]);
            Assert.IsFalse(target.ClassList.Contains("alpha"));
        }

        [Test]
        public void SandboxListAgreesWithTheAttributeAfterAnObserverRewritesIt()
        {
            // The guard lives in TokenList, so every reflected token list is covered by it - rel,
            // sizes, ping, sandbox, headers, for and dropzone all reach the same code.
            var config = Configuration.Default.With(new RewritingAttributeObserver("sandbox", "allow-forms"));
            var document = "<iframe id=target></iframe>".ToHtmlDocument(config);
            var target = document.GetElementById("target") as IHtmlInlineFrameElement;

            target.Sandbox.Add("allow-scripts");

            Assert.AreEqual("allow-forms", target.GetAttribute("sandbox"));
            Assert.AreEqual(1, target.Sandbox.Length);
            Assert.AreEqual("allow-forms", target.Sandbox[0]);
            Assert.IsFalse(target.Sandbox.Contains("allow-scripts"));
        }

        [Test]
        public void RelationListAddQueuesAMutationRecord()
        {
            var document = "<link id=target rel=alternate>".ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlLinkElement;
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributes: true);
            target.RelationList.Add("stylesheet");

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual("rel", records[0].AttributeName);
            Assert.AreEqual("alternate stylesheet", target.Relation);
        }

        [Test]
        public void SandboxListAddQueuesAMutationRecord()
        {
            var document = "<iframe id=target></iframe>".ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlInlineFrameElement;
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributes: true);
            target.Sandbox.Add("allow-scripts");

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual("sandbox", records[0].AttributeName);
            Assert.AreEqual("allow-scripts", target.GetAttribute("sandbox"));
        }

        [Test]
        public void RemovingABooleanAttributeQueuesAMutationRecord()
        {
            var document = "<input id=target disabled>".ToHtmlDocument();
            var target = document.GetElementById("target") as IHtmlInputElement;
            var records = new List<IMutationRecord>();

            Recorder(records).Connect(target, attributes: true, attributeOldValue: true);
            target.IsDisabled = false;

            Assert.AreEqual(1, records.Count);
            Assert.AreEqual("disabled", records[0].AttributeName);
            Assert.AreEqual(String.Empty, records[0].PreviousValue);
            Assert.IsNull(target.GetAttribute("disabled"));
        }

        [Test]
        public void RemovingABooleanAttributeNotifiesAnAttributeObserver()
        {
            var observer = new RecordingAttributeObserver();
            var document = Observed("<input id=target disabled>", observer);
            var target = document.GetElementById("target") as IHtmlInputElement;
            observer.Changes.Clear();

            target.IsDisabled = false;

            Assert.AreEqual(1, observer.Changes.Count);
            Assert.AreEqual("disabled", observer.Changes[0].Item2);
            Assert.IsNull(observer.Changes[0].Item3);
        }
    }
}
