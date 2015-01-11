using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Collections;
using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests.Library
{
    [TestFixture]
    public class MutationObserverTests
    {
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

            var document = DocumentBuilder.Html("");

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

            var document = DocumentBuilder.Html("");

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

            var document = DocumentBuilder.Html("");

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

            var document = DocumentBuilder.Html("");

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

            var document = DocumentBuilder.Html("");

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

            var document = DocumentBuilder.Html("");

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

            var document = DocumentBuilder.Html("");

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
            var document = DocumentBuilder.Html("");

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
            var document = DocumentBuilder.Html("");

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
            var document = DocumentBuilder.Html("");

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
            var document = DocumentBuilder.Html("");

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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");
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
            var document = DocumentBuilder.Html("");

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

        static void AssertRecord(IMutationRecord actual, TestMutationRecord expected)
        {
            Assert.AreEqual(expected.AttributeName, actual.AttributeName);
            Assert.AreEqual(expected.AttributeNamespace, actual.AttributeNamespace);
            Assert.AreEqual(expected.NextSibling, actual.NextSibling);
            Assert.AreEqual(expected.PreviousSibling, actual.PreviousSibling);
            Assert.AreEqual(expected.PreviousValue, actual.PreviousValue);
            Assert.AreEqual(expected.Type, actual.Type);
        }

        static INodeList ToNodeList(params Node[] nodes)
        {
            var list = new NodeList();

            foreach (var node in nodes)
                list.Add(node);

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
