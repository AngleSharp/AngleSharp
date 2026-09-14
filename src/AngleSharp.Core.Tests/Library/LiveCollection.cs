namespace AngleSharp.Core.Tests.Library
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class LiveCollectionTests
    {
        private static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void HtmlLiveCollectionUpdates()
        {
            var document = Html("<ul><li>A<li>B<li>C<li>D</ul>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(1, body.ChildNodes.Length);

            var ul = body.ChildNodes[0];
            Assert.AreEqual("ul", ul.GetTagName());
            Assert.AreEqual(NodeType.Element, ul.NodeType);

            var live = ((Element)ul).Children;
            Assert.AreEqual(4, live.Length);
            Assert.AreEqual("A", live[0].TextContent);
            Assert.AreEqual("B", live[1].TextContent);
            Assert.AreEqual("C", live[2].TextContent);
            Assert.AreEqual("D", live[3].TextContent);

            var newElement = document.CreateElement(TagNames.Li);
            newElement.TextContent = "E";
            ul.AppendChild(newElement);

            Assert.AreEqual(5, live.Length);
            Assert.AreEqual("E", live[4].TextContent);
        }

        [Test]
        public void HtmlLiveCollectionCompleteDOMRebuildWithInnerHtml()
        {
            var document = Html("<p><p><p><p><p>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(5, body.ChildNodes.Length);

            var live = body.Children;
            Assert.AreEqual(5, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.GetTagName());
                Assert.AreEqual(0, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual("", child.TextContent);
            }

            body.InnerHtml = "<p>First<p>Second<p>Third";
            Assert.AreEqual(3, body.ChildNodes.Length);
            Assert.AreEqual(3, live.Length);

            var i = 0;
            var str = new[] { "First", "Second", "Third" };

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.GetTagName());
                Assert.AreEqual(1, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual(str[i++], child.TextContent);
            }
        }

        [Test]
        public void HtmlLiveCollectionCompleteDOMRebuildWithText()
        {
            var document = Html("<p><p><p><p><p>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(5, body.ChildNodes.Length);

            var live = body.Children;
            Assert.AreEqual(5, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("p", child.GetTagName());
                Assert.AreEqual(0, child.ChildNodes.Length);
                Assert.AreEqual(0, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.AreEqual("", child.TextContent);
            }

            body.InnerHtml = "This is pure text!";
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(0, live.Length);

            body.InnerHtml = "<b>Proof that we still have live view</b>";
            Assert.AreEqual(1, body.ChildNodes.Length);
            Assert.AreEqual(1, live.Length);
        }

        [Test]
        public void HtmlLiveCollectionWithAttr()
        {
            var document = Html("<a name=first>some name</a><a name=second>more</a><div><a name=third>last</a><a id=change>not really an anchor</a></div>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(3, body.ChildNodes.Length);

            var live = document.Anchors;
            Assert.AreEqual(3, live.Length);

            foreach (var child in live)
            {
                Assert.AreEqual("a", child.GetTagName());
                Assert.AreEqual(1, child.Attributes.Count());
                Assert.AreEqual(NodeType.Element, child.NodeType);
                Assert.IsNotNull(child.GetAttribute("name"));
            }

            var a = document.QuerySelector("#change");
            Assert.IsNotNull(a);

            a.SetAttribute("name", "changed");
            Assert.AreEqual(4, live.Length);
        }

        [Test]
        public void HtmlLiveCollectionMultiple()
        {
            var document = Html("<embed></embed><div><object></object><applet></applet>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ChildNodes.Length);

            var live = document.Plugins;
            Assert.AreEqual(1, live.Length);

            var div = document.QuerySelector(TagNames.Div);
            Assert.IsNotNull(div);

            var embed = document.CreateElement(TagNames.Embed);
            div.AppendChild(embed);

            Assert.AreEqual(2, live.Length);
        }

        [Test]
        public void HtmlLiveCollectionMultipleWithAttr()
        {
            var document = Html("<a href='http://127.0.0.1'></a><div class='container'><area href='#'>my area</area>");

            var body = document.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(2, body.ChildNodes.Length);

            var live = document.Links;
            Assert.AreEqual(2, live.Length);

            var div = document.QuerySelector("body > div.container");
            Assert.IsNotNull(div);

            var a = document.CreateElement(TagNames.A);
            div.AppendChild(a);

            Assert.AreEqual(2, live.Length);

            a.SetAttribute("href", "http://localhost");
            Assert.AreEqual(3, live.Length);

            foreach (var element in live)
            {
                Assert.IsNotNull(element.GetAttribute("href"));
                Assert.IsTrue(element.GetTagName() == "a" || element.GetTagName() == "area");
            }
        }

        [Test]
        public void HtmlFormLiveCollectionContainsNonChildAssignedElements()
        {
            var document = Html("<form id=main><input><input><input></form><input form=main>");
            var form = document.QuerySelector("form") as IHtmlFormElement;
            var elements = form.Elements;
            Assert.AreEqual(4, elements.Length);
            Assert.AreEqual("main", elements[3].GetAttribute("form"));
            Assert.AreEqual(form, (elements[3] as IHtmlInputElement).Form);
        }

        [Test]
        public void HtmlFormLiveCollectionIsInDocumentTreeOrderNotFormFirst()
        {
            // form.elements is the listed elements whose form owner is the form, in tree order
            // over the whole document - so a control associated by form="id" that precedes the
            // form element precedes its in-form controls as well.
            var document = Html("<input id=before form=main><form id=main><div><input id=inside></div></form><input id=after form=main>");
            var form = document.QuerySelector("form") as IHtmlFormElement;
            var elements = form.Elements;

            Assert.AreEqual(3, elements.Length);
            CollectionAssert.AreEqual(new[]
            {
                document.GetElementById("before"),
                document.GetElementById("inside"),
                document.GetElementById("after")
            }, elements.ToArray());
        }

        [Test]
        public void HtmlFormLiveCollectionExcludesControlsOwnedByAnotherForm()
        {
            var document = Html("<form id=first><input id=a><input id=b form=second></form><form id=second></form>");
            var first = document.GetElementById("first") as IHtmlFormElement;
            var second = document.GetElementById("second") as IHtmlFormElement;

            CollectionAssert.AreEqual(new[] { document.GetElementById("a") }, first.Elements.ToArray());
            CollectionAssert.AreEqual(new[] { document.GetElementById("b") }, second.Elements.ToArray());
        }

        [Test]
        public void HtmlFormLiveCollectionSkipsImageInputsBetweenOtherControls()
        {
            // <input type=image> is a listed element but is excluded from form.elements, so the
            // controls after it shift down by one.
            var document = Html("<form id=main><input id=a><input id=img type=image><input id=b></form>");
            var form = document.QuerySelector("form") as IHtmlFormElement;
            var elements = form.Elements;

            Assert.AreEqual(2, elements.Length);
            Assert.AreSame(document.GetElementById("a"), elements[0]);
            Assert.AreSame(document.GetElementById("b"), elements[1]);
            Assert.IsNull(elements["img"]);
        }

        [Test]
        public void HtmlFormLiveCollectionNamedItemPrefersAnIdOverAnEarlierName()
        {
            var document = Html("<form id=main><input id=one name=target><input id=two class=first><input id=three class=second></form>");
            document.GetElementById("two").Id = "target";
            document.GetElementById("three").Id = "target";
            var form = document.QuerySelector("form") as IHtmlFormElement;
            var elements = form.Elements;

            // An id match anywhere beats a name match that came earlier in tree order, and the
            // first of two id matches wins.
            Assert.AreSame(document.QuerySelector(".first"), elements["target"]);
        }

        [Test]
        public void HtmlFormLiveCollectionNamedItemReturnsTheFirstNameMatchInTreeOrder()
        {
            var document = Html("<form id=main><input id=one name=target><input id=two name=target></form>");
            var form = document.QuerySelector("form") as IHtmlFormElement;

            Assert.AreSame(document.GetElementById("one"), form.Elements["target"]);
            Assert.IsNull(form.Elements["missing"]);
        }

        [Test]
        public void HtmlFormLiveCollectionIndexerThrowsOutsideTheRange()
        {
            var document = Html("<form id=main><input></form>");
            var form = document.QuerySelector("form") as IHtmlFormElement;
            var elements = form.Elements;

            Assert.AreEqual(1, elements.Length);
            Assert.Throws<ArgumentOutOfRangeException>(() => { _ = elements[1]; });
            Assert.Throws<ArgumentOutOfRangeException>(() => { _ = elements[-1]; });
        }

        [Test]
        public void HtmlFormLiveCollectionFollowsAppendRemoveAndReparent()
        {
            var document = Html("<form id=main><input id=a></form><div id=box></div>");
            var form = document.GetElementById("main") as IHtmlFormElement;
            var box = document.GetElementById("box");
            // One collection object answers every read below - the collection is live, so no
            // read may be served from anything captured when it was created.
            var elements = form.Elements;

            Assert.AreEqual(1, elements.Length);

            var added = document.CreateElement<IHtmlInputElement>();
            added.Id = "b";
            form.AppendChild(added);

            Assert.AreEqual(2, elements.Length);
            Assert.AreSame(added, elements[1]);
            Assert.AreSame(added, elements["b"]);

            // Re-parented out of the form: no form owner any more, so it leaves the collection.
            box.AppendChild(added);

            Assert.AreEqual(1, elements.Length);
            Assert.IsNull(elements["b"]);

            // Re-parented back, ahead of the control that was already there.
            form.InsertBefore(added, form.FirstChild);

            Assert.AreEqual(2, elements.Length);
            Assert.AreSame(added, elements[0]);
            CollectionAssert.AreEqual(new[] { added, document.GetElementById("a") }, elements.ToArray());

            added.Remove();

            Assert.AreEqual(1, elements.Length);
            CollectionAssert.AreEqual(new[] { document.GetElementById("a") }, elements.ToArray());
        }

        [Test]
        public void HtmlFieldSetLiveCollectionOutsideAnyFormListsItsUnownedControls()
        {
            // A fieldset with no form owner is handed a null form, and its collection is then the
            // controls under it that have no form owner either - rooted at the fieldset, which is
            // itself a form control and so is never a member of its own collection.
            var document = Html("<fieldset id=fs><input id=a><div><select id=b></select></div></fieldset>");
            var fieldSet = document.GetElementById("fs") as IHtmlFieldSetElement;

            Assert.IsNull(fieldSet.Form);
            Assert.AreEqual(2, fieldSet.Elements.Length);
            CollectionAssert.AreEqual(new[]
            {
                document.GetElementById("a"),
                document.GetElementById("b")
            }, fieldSet.Elements.ToArray());
        }
    }
}
