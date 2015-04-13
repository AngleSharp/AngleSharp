namespace AngleSharp.Core.Tests.Library
{
    using System;
    using System.Linq;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using NUnit.Framework;

    [TestFixture]
    public class MutationObserverTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ConnectMutationObserverChildNodesTriggerManually()
        {
            var called = false;

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                var record = mut[0];
                Assert.IsNotNull(record.Added);
                Assert.AreEqual(1, record.Added.Length);
            });

            var document = Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            document.Body.AppendChild(document.CreateElement("span"));
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverAttributesTriggerManually()
        {
            var called = false;
            var attrName = "something";
            var attrValue = "test";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.AreEqual(attrName, mut[0].AttributeName);
                Assert.IsNull(mut[0].PreviousValue);
            });

            var document = Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingAttributes = true
            });

            document.Body.SetAttribute(attrName, attrValue);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverMultipleAttributesDescendentTriggerManually()
        {
            var called1 = false;
            var called2 = false;
            var called3 = false;
            var attrName = "something";
            var attrValue = "test";

            var document = Html("");

            var observer1 = new MutationObserver((mut, obs) =>
            {
                called1 = true;
                Assert.AreEqual(1, mut.Length);
            });

            observer1.Connect(document.DocumentElement, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true
            });

            var observer2 = new MutationObserver((mut, obs) =>
            {
                called2 = true;
                Assert.AreEqual(0, mut.Length);
            });

            observer2.Connect(document.DocumentElement, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = false
            });

            var observer3 = new MutationObserver((mut, obs) =>
            {
                called3 = true;
                Assert.AreEqual(1, mut.Length);
            });

            observer3.Connect(document.Body, new MutationObserverInit
            {
                IsObservingAttributes = true
            });

            document.Body.SetAttribute(attrName, attrValue);
            observer1.TriggerWith(observer1.Flush().ToArray());
            observer2.TriggerWith(observer2.Flush().ToArray());
            observer3.TriggerWith(observer3.Flush().ToArray());
            Assert.IsTrue(called1);
            Assert.IsTrue(called2);
            Assert.IsTrue(called3);
        }

        [Test]
        public void ConnectMutationObserverTextWithDescendentsAndClearOldValueTriggerManually()
        {
            var called = false;
            var text = "something";
            var replaced = "different";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.IsNull(mut[0].PreviousValue);
                var tn = mut[0].Target as TextNode;
                Assert.IsNotNull(tn);
                Assert.AreEqual(text + replaced, tn.TextContent);
            });

            var document = Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = true,
                IsExaminingOldCharacterData = false
            });

            document.Body.TextContent = text;
            var textNode = document.Body.ChildNodes[0] as TextNode;
            textNode.Replace(text.Length, 0, replaced);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverTextWithDescendentsAndExaminingOldValueTriggerManually()
        {
            var called = false;
            var text = "something";
            var replaced = "different";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.AreEqual(text, mut[0].PreviousValue);
                var tn = mut[0].Target as TextNode;
                Assert.IsNotNull(tn);
                Assert.AreEqual(text + replaced, tn.TextContent);
            });

            var document = Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = true,
                IsExaminingOldCharacterData = true
            });

            document.Body.TextContent = text;
            var textNode = document.Body.ChildNodes[0] as TextNode;
            textNode.Replace(text.Length, 0, replaced);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverTextNoDescendentsTriggerManually()
        {
            var called = false;
            var text = "something";
            var replaced = "different";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(0, mut.Length);
            });

            var document = Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = false
            });

            document.Body.TextContent = text;
            var textNode = document.Body.ChildNodes[0] as TextNode;
            textNode.Replace(text.Length, 0, replaced);
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void ConnectMutationObserverTextNoDescendentsButCreatedTriggerManually()
        {
            var called = false;
            var text = "something";

            var observer = new MutationObserver((mut, obs) =>
            {
                called = true;
                Assert.AreEqual(1, mut.Length);
                Assert.AreEqual(1, mut[0].Added.Length);
                Assert.AreEqual(text, mut[0].Added[0].TextContent);
            });

            var document = Html("");

            observer.Connect(document.Body, new MutationObserverInit
            {
                IsObservingSubtree = false,
                IsObservingChildNodes = true
            });

            document.Body.TextContent = text;
            observer.TriggerWith(observer.Flush().ToArray());
            Assert.IsTrue(called);
        }

        [Test]
        public void MutationObserverAttr()
        {
            var document = Html("");

            var div = document.CreateElement("div");
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true
            });
            div.SetAttribute("a", "A");
            div.SetAttribute("a", "B");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a",
                AttributeNamespace = null
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a",
                AttributeNamespace = null
            });
        }

        [Test]
        public void MutationObserverAttrWithOldvalue()
        {
            var document = Html("");

            var div = document.CreateElement("div");
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsExaminingOldAttributeValue = true
            });
            div.SetAttribute("a", "A");
            div.SetAttribute("a", "B");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a",
                AttributeNamespace = null,
                PreviousValue = null
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a",
                AttributeNamespace = null,
                PreviousValue = "A"
            });
        }

        [Test]
        public void MutationObserverAttrChangeInSubtreeShouldNotGenereateARecord()
        {
            var document = Html("");

            var div = document.CreateElement("div");
            var child = document.CreateElement("div");
            div.AppendChild(child);

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true
            });
            child.SetAttribute("a", "A");
            child.SetAttribute("a", "B");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 0);
        }

        [Test]
        public void MutationObserverAttrChangeSubtree()
        {
            var document = Html("");

            var div = document.CreateElement("div");
            var child = document.CreateElement("div");
            div.AppendChild(child);

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true
            });
            child.SetAttribute("a", "A");
            child.SetAttribute("a", "B");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "a"
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "a"
            });
        }

        [Test]
        public void MutationObserverMultipleObserversOnSameTarget()
        {
            var document = Html("");
            var div = document.CreateElement("div");

            var observer1 = new MutationObserver((obs, mut) => { });
            observer1.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsExaminingOldAttributeValue = true
            });

            var observer2 = new MutationObserver((obs, mut) => { });
            observer2.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                AttributeFilters = new[] { "b" }
            });
            div.SetAttribute("a", "A");
            div.SetAttribute("a", "A2");
            div.SetAttribute("b", "B");

            var records = observer1.Flush().ToArray();
            Assert.AreEqual(records.Count(), 3);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a"
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a",
                PreviousValue = "A"
            });
            AssertRecord(records[2], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "b"
            });

            records = observer2.Flush().ToArray();
            Assert.AreEqual(1, records.Count());

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "b"
            });
        }

        [Test]
        public void MutationObserverObserverObservesOnDifferentTarget()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var child = document.CreateElement("div");
            div.AppendChild(child);

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(child, new MutationObserverInit
            {
                IsObservingAttributes = true
            });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true,
                IsExaminingOldAttributeValue = true
            });

            child.SetAttribute("a", "A");
            child.SetAttribute("a", "A2");
            child.SetAttribute("b", "B");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(3, records.Count());

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "a"
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "a",
                PreviousValue = "A"
            });
            AssertRecord(records[2], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "b"
            });
        }

        [Test]
        public void MutationObserverObservingOnTheSameNodeShouldUpdateTheOptions()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                AttributeFilters = new[] { "a" }
            });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                AttributeFilters = new[] { "b" }
            });

            div.SetAttribute("a", "A");
            div.SetAttribute("b", "B");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 1);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "b"
            });
        }

        [Test]
        public void MutationObserverDisconnectShouldStopAllEventsAndEmptyTheRecords()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
            });

            div.SetAttribute("a", "A");
            observer.Disconnect();
            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 0);

            div.SetAttribute("b", "B");
            records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 0);
        }

        [Test]
        public void MutationObserverDisconnectShouldNotAffectOtherObservers()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var observer1 = new MutationObserver((obs, mut) => { });
            observer1.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
            });
            var observer2 = new MutationObserver((obs, mut) => { });
            observer2.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
            });

            div.SetAttribute("a", "A");

            observer1.Disconnect();
            var records1 = observer1.Flush().ToArray();
            Assert.AreEqual(records1.Count(), 0);

            var records2 = observer2.Flush().ToArray();
            Assert.AreEqual(records2.Count(), 1);
            AssertRecord(records2[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a"
            });

            div.SetAttribute("b", "B");

            records1 = observer1.Flush().ToArray();
            Assert.AreEqual(records1.Count(), 0);

            records2 = observer2.Flush().ToArray();
            Assert.AreEqual(records2.Count(), 1);
            AssertRecord(records2[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "b"
            });
        }

        [Test]
        public void MutationObserverOneObserverTwoAttributeChanges()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var observer = new MutationObserver((records, obs) =>
            {
                Assert.AreEqual(records.Count(), 2);

                AssertRecord(records[0], new TestMutationRecord
                {
                    Type = "attributes",
                    Target = div,
                    AttributeName = "a",
                    AttributeNamespace = null
                });
                AssertRecord(records[1], new TestMutationRecord
                {
                    Type = "attributes",
                    Target = div,
                    AttributeName = "a",
                    AttributeNamespace = null
                });
            });

            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true
            });

            div.SetAttribute("a", "A");
            div.SetAttribute("a", "B");
        }

        [Test]
        public void MutationObserverNestedChanges()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var fresh = true;
            var observer = new MutationObserver((records, obs) =>
            {
                Assert.AreEqual(records.Count(), 1);

                if (fresh)
                {
                    AssertRecord(records[0], new TestMutationRecord
                    {
                        Type = "attributes",
                        Target = div,
                        AttributeName = "a",
                        AttributeNamespace = null
                    });
                    div.SetAttribute("b", "B");
                    fresh = false;
                }
                else
                {
                    AssertRecord(records[0], new TestMutationRecord
                    {
                        Type = "attributes",
                        Target = div,
                        AttributeName = "b",
                        AttributeNamespace = null
                    });
                }
            });

            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true
            });

            div.SetAttribute("a", "A");
        }

        [Test]
        public void MutationObserverCharacterdata()
        {
            var document = Html("");

            var text = document.CreateTextNode("abc");
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(text, new MutationObserverInit
            {
                IsObservingCharacterData = true
            });
            text.TextContent = "def";
            text.TextContent = "ghi";

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "characterData",
                Target = text
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "characterData",
                Target = text
            });
        }

        [Test]
        public void MutationObserverCharacterdataWithOldValue()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var text = testDiv.AppendChild(document.CreateTextNode("abc"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(text, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsExaminingOldCharacterData = true
            });
            text.TextContent = "def";
            text.TextContent = "ghi";

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "characterData",
                Target = text,
                PreviousValue = "abc"
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "characterData",
                Target = text,
                PreviousValue = "def"
            });
        }

        [Test]
        public void MutationObserverCharacterdataChangeInSubtreeShouldNotGenerateARecord()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var text = div.AppendChild(document.CreateTextNode("abc"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingCharacterData = true
            });
            text.TextContent = "def";
            text.TextContent = "ghi";

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 0);
        }

        [Test]
        public void MutationObserverCharacterdataChangeInSubtree()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var text = div.AppendChild(document.CreateTextNode("abc"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = true
            });
            text.TextContent = "def";
            text.TextContent = "ghi";

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "characterData",
                Target = text
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "characterData",
                Target = text
            });
        }

        [Test]
        public void MutationObserverAppendChild()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });
            var a = document.CreateElement("a");
            var b = document.CreateElement("b");

            div.AppendChild(a);
            div.AppendChild(b);

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Added = ToNodeList(a)
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Added = ToNodeList(b),
                PreviousSibling = a
            });

        }

        [Test]
        public void MutationObserverInsertBefore()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var a = document.CreateElement("a");
            var b = document.CreateElement("b");
            var c = document.CreateElement("c");
            div.AppendChild(a);

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            div.InsertBefore(b, a);
            div.InsertBefore(c, a);

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Added = ToNodeList(b),
                NextSibling = a
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Added = ToNodeList(c),
                NextSibling = a,
                PreviousSibling = b
            });
        }

        [Test]
        public void MutationObserverRemovechild()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var a = div.AppendChild(document.CreateElement("a"));
            var b = div.AppendChild(document.CreateElement("b"));
            var c = div.AppendChild(document.CreateElement("c"));

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            div.RemoveChild(b);
            div.RemoveChild(a);

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Removed = ToNodeList(b),
                NextSibling = c,
                PreviousSibling = a
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Removed = ToNodeList(a),
                NextSibling = c
            });
        }

        [Test]
        public void MutationObserverDirectChildren()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });
            var a = document.CreateElement("a");
            var b = document.CreateElement("b");

            div.AppendChild(a);
            div.InsertBefore(b, a);
            div.RemoveChild(b);

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 3);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Added = ToNodeList(a)
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                NextSibling = a,
                Added = ToNodeList(b)
            });

            AssertRecord(records[2], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                NextSibling = a,
                Removed = ToNodeList(b)
            });
        }

        [Test]
        public void MutationObserverSubtree()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var child = div.AppendChild(document.CreateElement("div"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(child, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });
            var a = document.CreateTextNode("a");
            var b = document.CreateTextNode("b");

            child.AppendChild(a);
            child.InsertBefore(b, a);
            child.RemoveChild(b);

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 3);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = child,
                Added = ToNodeList(a)
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = child,
                NextSibling = a,
                Added = ToNodeList(b)
            });

            AssertRecord(records[2], new TestMutationRecord
            {
                Type = "childList",
                Target = child,
                NextSibling = a,
                Removed = ToNodeList(b)
            });
        }

        [Test]
        public void MutationObserverBothDirectAndSubtree()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var child = div.AppendChild(document.CreateElement("div"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true,
                IsObservingSubtree = true
            });
            observer.Connect(child, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            var a = document.CreateTextNode("a");
            var b = document.CreateTextNode("b");

            child.AppendChild(a);
            div.AppendChild(b);

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = child,
                Added = ToNodeList(a)
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Added = ToNodeList(b),
                PreviousSibling = child
            });
        }

        [Test]
        public void MutationObserverAppendMultipleAtOnceAtTheEnd()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var a = div.AppendChild(document.CreateTextNode("a"));

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            var df = document.CreateDocumentFragment();
            var b = df.AppendChild(document.CreateTextNode("b"));
            var c = df.AppendChild(document.CreateTextNode("c"));
            var d = df.AppendChild(document.CreateTextNode("d"));

            div.AppendChild(df);

            var records = observer.Flush().ToArray();
            var merged = MergeRecords(records);

            AssertArrayEqual(merged.Item1, ToNodeList(b, c, d));
            AssertArrayEqual(merged.Item2, ToNodeList());
            AssertAll(records, new TestMutationRecord
            {
                Type = "childList",
			    Target = div
            });
        }

        [Test]
        public void MutationObserverAppendMultipleAtOnceAtTheFront()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var a = div.AppendChild(document.CreateTextNode("a"));

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            var df = document.CreateDocumentFragment();
            var b = df.AppendChild(document.CreateTextNode("b"));
            var c = df.AppendChild(document.CreateTextNode("c"));
            var d = df.AppendChild(document.CreateTextNode("d"));

            div.InsertBefore(df, a);

            var records = observer.Flush().ToArray();
            var merged = MergeRecords(records);

            AssertArrayEqual(merged.Item1, ToNodeList(b, c, d));
            AssertArrayEqual(merged.Item2, ToNodeList());
            AssertAll(records, new TestMutationRecord
            {
                Type = "childList",
			    Target = div
            });
        }

        [Test]
        public void MutationObserverAppendMultipleAtOnceInTheMiddle()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = document.CreateElement("div");
            testDiv.AppendChild(div);
            var a = div.AppendChild(document.CreateTextNode("a"));
            var b = div.AppendChild(document.CreateTextNode("b"));

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            var df = document.CreateDocumentFragment();
            var c = df.AppendChild(document.CreateTextNode("c"));
            var d = df.AppendChild(document.CreateTextNode("d"));

            div.InsertBefore(df, b);

            var records = observer.Flush().ToArray();
            var merged = MergeRecords(records);

            AssertArrayEqual(merged.Item1, ToNodeList(c, d));
            AssertArrayEqual(merged.Item2, ToNodeList());
            AssertAll(records, new TestMutationRecord
            {
                Type = "childList",
			    Target = div
            });
        }

        [Test]
        public void MutationObserverRemoveAllChildren()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = document.CreateElement("div");
            testDiv.AppendChild(div);
            var a = div.AppendChild(document.CreateTextNode("a"));
            var b = div.AppendChild(document.CreateTextNode("b"));
            var c = div.AppendChild(document.CreateTextNode("c"));

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            div.InnerHtml = "";

            var records = observer.Flush().ToArray();
            var merged = MergeRecords(records);

            AssertArrayEqual(merged.Item1, ToNodeList());
            AssertArrayEqual(merged.Item2, ToNodeList(a, b, c));
            AssertAll(records, new TestMutationRecord
            {
                Type = "childList",
			    Target = div
            });
        }

        [Test]
        public void MutationObserverReplaceAllChildrenUsingInnerhtml()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = document.CreateElement("div");
            testDiv.AppendChild(div);
            var a = div.AppendChild(document.CreateTextNode("a"));
            var b = div.AppendChild(document.CreateTextNode("b"));

            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true
            });

            div.InnerHtml = "<c></c><d></d>";

            var c = div.FirstChild;
            var d = div.LastChild;
            var records = observer.Flush().ToArray();
            var merged = MergeRecords(records);

            AssertArrayEqual(merged.Item1, ToNodeList(c, d));
            AssertArrayEqual(merged.Item2, ToNodeList(a, b));
            AssertAll(records, new TestMutationRecord
            {
                Type = "childList",
			    Target = div
            });
        }

        [Test]
        public void MutationObserverAttrAndCharacterdata()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var text = div.AppendChild(document.CreateTextNode("text"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingCharacterData = true,
                IsObservingSubtree = true
            });

            div.SetAttribute("a", "A");
            div.FirstChild.TextContent = "changed";

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = div,
                AttributeName = "a",
                AttributeNamespace = null
            });
            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "characterData",
                Target = div.FirstChild
            });
        }

        [Test]
        public void MutationObserverAttrChanged()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var child = document.CreateElement("div");
            div.AppendChild(child);
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true
            });
            div.RemoveChild(child);
            child.SetAttribute("a", "A");

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 1);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "a",
                AttributeNamespace = null
            });

            child.SetAttribute("b", "B");
            records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 1);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "attributes",
                Target = child,
                AttributeName = "b",
                AttributeNamespace = null
            });
        }

        [Test]
        public void MutationObserverAttrCallback()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var child = document.CreateElement("div");
            div.AppendChild(child);
            var i = 0;
            var observer = new MutationObserver((records, obs) =>
            {
                Assert.LessOrEqual(++i, 2);
                Assert.AreEqual(1, records.Count());

                AssertRecord(records[0], new TestMutationRecord
                {
                    Type = "attributes",
                    Target = child,
                    AttributeName = "a",
                    AttributeNamespace = null
                });

                // The transient observers are removed before the callback is called.
                child.SetAttribute("b", "B");
                records = obs.Flush().ToArray();
                Assert.AreEqual(0, records.Count());
            });

            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true
            });

            div.RemoveChild(child);
            child.SetAttribute("a", "A");
            observer.Trigger();
        }

        [Test]
        public void MutationObserverAttrMakeSureTransientGetsRemoved()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var child = document.CreateElement("div");
            div.AppendChild(child);
            var i = 0;
            var observer = new MutationObserver((records, obs) =>
            {
                Assert.AreNotEqual(2, ++i);
                Assert.AreEqual(records.Count(), 1);

                AssertRecord(records[0], new TestMutationRecord
                {
                    Type = "attributes",
                    Target = child,
                    AttributeName = "a",
                    AttributeNamespace = null
                });
            });

            observer.Connect(div, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true
            });

            div.RemoveChild(child);
            child.SetAttribute("a", "A");
            observer.Trigger();

            var div2 = document.CreateElement("div");
            var observer2 = new MutationObserver((records, obs) =>
            {
                Assert.LessOrEqual(++i, 3);
                Assert.AreEqual(records.Count(), 1);

                AssertRecord(records[0], new TestMutationRecord
                {
                    Type = "attributes",
                    Target = child,
                    AttributeName = "b",
                    AttributeNamespace = null
                });
            });

            observer2.Connect(div2, new MutationObserverInit
            {
                IsObservingAttributes = true,
                IsObservingSubtree = true,
            });

            div2.AppendChild(child);
            child.SetAttribute("b", "B");
            observer2.Trigger();
        }

        [Test]
        public void MutationObserverChildListCharacterdata()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var child = div.AppendChild(document.CreateTextNode("text"));
            var observer = new MutationObserver((obs, mut) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = true
            });
            div.RemoveChild(child);
            child.TextContent = "changed";

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 1);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "characterData",
                Target = child
            });

            child.TextContent += " again";

            records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 1);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "characterData",
                Target = child
            });
        }

        [Test]
        public void MutationObserverCharacterdataCallback()
        {
            var document = Html("");
            var div = document.CreateElement("div");
            var child = div.AppendChild(document.CreateTextNode("text"));
            var i = 0;
            var observer = new MutationObserver((records, obs) =>
            {
                Assert.LessOrEqual(++i, 2);
                Assert.AreEqual(1, records.Count());

                AssertRecord(records[0], new TestMutationRecord
                {
                    Type = "characterData",
                    Target = child
                });

                // The transient observers are removed before the callback is called.
                child.TextContent += " again";
                records = obs.Flush().ToArray();
                Assert.AreEqual(0, records.Count());
            });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingCharacterData = true,
                IsObservingSubtree = true
            });
            div.RemoveChild(child);
            child.TextContent = "changed";
            observer.Trigger();
        }

        [Test]
        public void MutationObserverChildlist()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var child = div.AppendChild(document.CreateElement("div"));
            var observer = new MutationObserver((mut, obs) => { });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true,
                IsObservingSubtree = true
            });
            div.RemoveChild(child);
            var grandChild = child.AppendChild(document.CreateElement("span"));

            var records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 2);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = div,
                Removed = ToNodeList(child)
            });

            AssertRecord(records[1], new TestMutationRecord
            {
                Type = "childList",
                Target = child,
                Added = ToNodeList(grandChild)
            });

            child.RemoveChild(grandChild);

            records = observer.Flush().ToArray();
            Assert.AreEqual(records.Count(), 1);

            AssertRecord(records[0], new TestMutationRecord
            {
                Type = "childList",
                Target = child,
                Removed = ToNodeList(grandChild)
            });
        }

        [Test]
        public void MutationObserverChildlistCallback()
        {
            var document = Html("");
            var testDiv = document.Body.AppendChild(document.CreateElement("div"));
            var div = testDiv.AppendChild(document.CreateElement("div"));
            var child = div.AppendChild(document.CreateElement("div"));
            var grandChild = document.CreateElement("span");
            var i = 0;
            var observer = new MutationObserver((records, obs) =>
            {
                Assert.LessOrEqual(++i, 2);
                Assert.AreEqual(2, records.Count());

                AssertRecord(records[0], new TestMutationRecord
                {
                    Type = "childList",
                    Target = div,
                    Removed = ToNodeList(child)
                });

                AssertRecord(records[1], new TestMutationRecord
                {
                    Type = "childList",
                    Target = child,
                    Added = ToNodeList(grandChild)
                });

                // The transient observers are removed before the callback is called.
                child.RemoveChild(grandChild);

                records = obs.Flush().ToArray();
                Assert.AreEqual(0, records.Count());
            });
            observer.Connect(div, new MutationObserverInit
            {
                IsObservingChildNodes = true,
                IsObservingSubtree = true
            });
            div.RemoveChild(child);
            child.AppendChild(grandChild);
            observer.Trigger();
        }

        static Tuple<NodeList, NodeList> MergeRecords(IMutationRecord[] records)
        {
            var added = new NodeList();
            var removed = new NodeList();

            foreach (var record in records)
            {
                if (record.Added != null)
                    added.AddRange((NodeList)record.Added);

                if (record.Removed != null)
                    removed.AddRange((NodeList)record.Removed);
            }

            return Tuple.Create(added, removed);
        }

        static void AssertArrayEqual(INodeList actual, INodeList expected)
        {
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
                Assert.AreSame(expected[i], actual[i]);
        }

        static void AssertAll(IMutationRecord[] actualRecords, TestMutationRecord expected)
        {
            foreach (var actualRecord in actualRecords)
            {
                Assert.AreEqual(expected.Type, actualRecord.Type);
                Assert.AreEqual(expected.Target, actualRecord.Target);
            }
        }

        static void AssertRecord(IMutationRecord actual, TestMutationRecord expected)
        {
            Assert.AreEqual(expected.AttributeName, actual.AttributeName);
            Assert.AreEqual(expected.AttributeNamespace, actual.AttributeNamespace);
            Assert.AreEqual(expected.NextSibling, actual.NextSibling);
            Assert.AreEqual(expected.PreviousSibling, actual.PreviousSibling);
            Assert.AreEqual(expected.PreviousValue, actual.PreviousValue);
            Assert.AreEqual(expected.Type, actual.Type);
            Assert.AreEqual(expected.Target, actual.Target);
        }

        static INodeList ToNodeList(params INode[] nodes)
        {
            var list = new NodeList();

            foreach (var node in nodes)
                list.Add((Node)node);

            return list;
        }

        class TestMutationRecord : IMutationRecord
        {
            public INodeList Added
            {
                get;
                set;
            }

            public string AttributeName
            {
                get;
                set;
            }

            public string AttributeNamespace
            {
                get;
                set;
            }

            public INode NextSibling
            {
                get;
                set;
            }

            public INode PreviousSibling
            {
                get;
                set;
            }

            public string PreviousValue
            {
                get;
                set;
            }

            public INodeList Removed
            {
                get;
                set;
            }

            public INode Target
            {
                get;
                set;
            }

            public string Type
            {
                get;
                set;
            }
        }
    }
}
