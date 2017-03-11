namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [TestFixture]
    public class DOMTests
    {
        [Test]
        public async Task ClosingSpanTagShouldNotResultInAnError()
        {
            var context = BrowsingContext.New();
            var events = new EventReceiver<HtmlErrorEvent>(callback => context.GetService<IHtmlParser>().Error += callback);
            var source = @"<!DOCTYPE html><html><head></head><body><span>test</span></body></html>";
            await context.OpenAsync(res => res.Content(source));
            Assert.AreEqual(0, events.Received.Count);
        }

        [Test]
        public void AppendMultipleNodesToParentNode()
        {
            var document = String.Empty.ToHtmlDocument();
            var children = new[]
            {
                document.CreateElement("span"),
                document.CreateElement("em")
            };
            Assert.AreEqual(2, children.Length);
            document.Body.Append(children);
            Assert.AreEqual(2, document.Body.ChildNodes.Length);
            var span = document.Body.ChildNodes[0];
            var em = document.Body.ChildNodes[1];
            Assert.AreEqual("span", span.GetTagName());
            Assert.AreEqual(0, span.ChildNodes.Length);
            Assert.AreEqual("em", em.GetTagName());
            Assert.AreEqual(0, em.ChildNodes.Length);
        }

        [Test]
        public void ReadAttributesCorrectlyInAttributeMadness()
        {
            var content = Helper.StreamFromBytes(Assets.IrishCentral);
            var expected = new[]
            {
                "src",
                "alt",
                "hilde",
                "michnia",
                "is",
                "under",
                "investigation",
                "as",
                "a",
                "former",
                "concentration",
                "camp",
                "guard",
                "after",
                "holocaust",
                "documentary.\\\"",
                "class",
                "style"
            };
            var document = content.ToHtmlDocument();
            var img = document.QuerySelector("img.img-responsive");
            Assert.IsNotNull(img);
            var attributes = img.Attributes.ToArray();
            Assert.AreEqual(18, attributes.Length);

            for (int i = 0; i < attributes.Length; i++)
                Assert.AreEqual(expected[i], attributes[i].Name);
        }

        [Test]
        public void DOMTokenListWritesBack()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var div = new HtmlDivElement(document) { ClassName = "" };
            div.ClassList.Add(testClass);
            Assert.AreEqual(testClass, div.ClassName);
        }

        [Test]
        public void DOMTokenListCorrectlyInitializedFindsClass()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var div = new HtmlDivElement(document) { ClassName = testClass + " whatever anotherclass" };
            Assert.IsTrue(div.ClassList.Contains(testClass));
        }

        [Test]
        public void DOMTokenListCorrectlyInitializedNoClass()
        {
            var document = new HtmlDocument();
            var testClass = "myclass1";
            var div = new HtmlDivElement(document) { ClassName = "myclass2 whatever anotherclass" };
            Assert.IsFalse(div.ClassList.Contains(testClass));
        }

        [Test]
        public void DOMTokenListToggleWorksTurnOff()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var otherClasses = "otherClass someOther more";
            var div = new HtmlDivElement(document) { ClassName = testClass + " " + otherClasses };
            div.ClassList.Toggle(testClass);
            Assert.AreEqual(div.ClassName, otherClasses);
        }

        [Test]
        public void DOMTokenListToggleWorksTurnOn()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var otherClasses = "otherClass someOther more";
            var div = new HtmlDivElement(document) { ClassName = otherClasses };
            div.ClassList.Toggle(testClass);
            Assert.AreEqual(div.ClassName, otherClasses + " " + testClass);
        }

        [Test]
        public void DOMStringMapBindingGet()
        {
            var document = new HtmlDocument();
            var value = "SomeUser";
            var div = new HtmlDivElement(document);
            div.SetAttribute("data-user", value);
            Assert.AreEqual(div.Dataset["user"], value);
        }

        [Test]
        public void DOMStringMapBindingSet()
        {
            var document = new HtmlDocument();
            var value = "SomeUser";
            var div = new HtmlDivElement(document);
            div.Dataset["user"] = value;
            Assert.AreEqual(div.GetAttribute("data-user"), value);
        }

        [Test]
        public void DOMStringMapHasNoAttribute()
        {
            var document = new HtmlDocument();
            var div = new HtmlDivElement(document);
            Assert.IsTrue(div.Dataset["user"] == null);
        }

        [Test]
        public void DOMStringMapHasAttributesButRequestedMissing()
        {
            var document = new HtmlDocument();
            var div = new HtmlDivElement(document);
            div.SetAttribute("data-some", "test");
            div.SetAttribute("data-another", "");
            div.SetAttribute("data-test", "third attribute");
            Assert.IsTrue(div.Dataset["user"] == null);
        }

        [Test]
        public void DOMStringMapIEnumerableWorking()
        {
            var document = new HtmlDocument();
            var div = new HtmlDivElement(document);
            div.SetAttribute("data-some", "test");
            div.SetAttribute("data-another", "");
            div.SetAttribute("data-test", "third attribute");
            Assert.AreEqual(3, div.Dataset.Count());
            Assert.AreEqual("some", div.Dataset.First().Key);
            Assert.AreEqual("test", div.Dataset.First().Value);
            Assert.AreEqual("test", div.Dataset.Last().Key);
            Assert.AreEqual("third attribute", div.Dataset.Last().Value);
        }

        [Test]
        public void HtmlCustomTitleGeneration()
        {
            var document = new HtmlDocument();
            var title = "My Title";
            document.Title = title;
            Assert.AreEqual("", document.Title);
            var html = document.CreateElement(TagNames.Html);
            var head = document.CreateElement(TagNames.Head);
            document.AppendChild(html);
            html.AppendChild(head);
            document.Title = title;
            Assert.AreEqual(title, document.Title);
        }

        [Test]
        public void HtmlHasRightHeadElement()
        {
            var document = new HtmlDocument();
            var root = new HtmlHtmlElement(document);
            document.AppendChild(root);
            var head = new HtmlHeadElement(document);
            root.AppendChild(head);
            Assert.AreEqual(head, document.Head);
        }

        [Test]
        public void HtmlHasRightBodyElement()
        {
            var document = new HtmlDocument();
            var root = new HtmlHtmlElement(document);
            document.AppendChild(root);
            var body = new HtmlBodyElement(document);
            root.AppendChild(body);
            Assert.AreEqual(body, document.Body);
        }

        [Test]
        public void NormalizeRemovesEmptyTextNodes()
        {
            var document = new HtmlDocument();
            var div = document.CreateElement("div");
            div.AppendChild(document.CreateElement("a"));
            div.AppendChild(document.CreateTextNode(String.Empty));
            div.AppendChild(document.CreateElement("div"));
            div.AppendChild(document.CreateTextNode("Hi there!"));
            div.AppendChild(document.CreateElement("img"));
            div.Normalize();
            Assert.AreEqual(div.ChildNodes.Length, 4);
        }

        [Test]
        public void NormalizeRemovesEmptyTextNodesNested()
        {
            var document = new HtmlDocument();
            var div = document.CreateElement("div");
            var a = document.CreateElement("a");
            a.AppendChild(document.CreateTextNode(""));
            a.AppendChild(document.CreateTextNode("Not empty."));
            div.AppendChild(a);
            div.AppendChild(document.CreateTextNode(""));
            div.AppendChild(document.CreateElement("div"));
            div.AppendChild(document.CreateTextNode("Certainly not empty!"));
            div.AppendChild(document.CreateElement("img"));
            div.Normalize();
            Assert.AreEqual(a.ChildNodes.Length, 1);
        }

        [Test]
        public void NormalizeMergeTextNodes()
        {
            var document = new HtmlDocument();
            var div = document.CreateElement("div");
            var a = document.CreateElement("a");
            a.AppendChild(document.CreateTextNode(""));
            a.AppendChild(document.CreateTextNode("Not empty."));
            div.AppendChild(a);
            div.AppendChild(document.CreateTextNode(""));
            div.AppendChild(document.CreateElement("div"));
            div.AppendChild(document.CreateTextNode("Certainly not empty!"));
            div.AppendChild(document.CreateTextNode("Certainly not empty!"));
            div.AppendChild(document.CreateTextNode("Certainly not empty!"));
            div.AppendChild(document.CreateTextNode("Certainly not empty!"));
            div.AppendChild(document.CreateElement("img"));
            div.Normalize();
            Assert.AreEqual(div.ChildNodes.Length, 4);
        }

        [Test]
        public void LocationCorrectAddressWithoutPort()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var path = "/some/path";
            var address = protocol + "//" + hostname + path;
            var location = new Location(address);
            Assert.AreEqual(String.Empty, location.Hash);
            Assert.AreEqual(hostname, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(String.Empty, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
        }

        [Test]
        public void LocationCorrectAddressWithoutPortButWithHash()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var path = "/some/path";
            var hash = "#myhash";
            var address = protocol + "//" + hostname + path + hash;
            var location = new Location(address);
            Assert.AreEqual(hash, location.Hash);
            Assert.AreEqual(hostname, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(String.Empty, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
        }

        [Test]
        public void LocationCorrectAddressWithPort()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var port = "8080";
            var path = "/some/path";
            var host = hostname + ":" + port;
            var address = protocol + "//" + host + path;
            var location = new Location(address);
            Assert.AreEqual(String.Empty, location.Hash);
            Assert.AreEqual(host, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(port, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
        }

        [Test]
        public void LocationCorrectAddressWithPortAndHash()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var port = "8080";
            var path = "/some/path";
            var hash = "#myhash";
            var host = hostname + ":" + port;
            var address = protocol + "//" + host + path + hash;
            var location = new Location(address);
            Assert.AreEqual(hash, location.Hash);
            Assert.AreEqual(host, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(port, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
        }

        [Test]
        public void LocationCorrectAddressWithHashChange()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var path = "/some/path";
            var hash = "#myhash";
            var address = protocol + "//" + hostname + path;
            var location = new Location(address);
            Assert.AreEqual(String.Empty, location.Hash);
            location.Hash = hash;
            address = protocol + "//" + hostname + path + hash;
            Assert.AreEqual(hash, location.Hash);
            Assert.AreEqual(hostname, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(String.Empty, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
            Assert.AreEqual(address, location.Href);
        }

        [Test]
        public void LocationCorrectAddressWithProtocolChange()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var path = "/some/path";
            var address = protocol + "//" + hostname + path;
            var location = new Location(address);
            Assert.AreEqual(protocol, location.Protocol);
            protocol = "https:";
            location.Protocol = protocol;
            address = protocol + "//" + hostname + path;
            Assert.AreEqual(protocol, location.Protocol);
            Assert.AreEqual(String.Empty, location.Hash);
            Assert.AreEqual(hostname, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(String.Empty, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
            Assert.AreEqual(address, location.Href);
        }

        [Test]
        public void LocationCorrectAddressWithPathChange()
        {
            var hostname = "www.w3.org";
            var protocol = "http:";
            var path = "/some/path";
            var address = protocol + "//" + hostname + path;
            var location = new Location(address);
            Assert.AreEqual(path, location.PathName);
            path = "/";
            location.PathName = "";
            address = protocol + "//" + hostname + path;
            Assert.AreEqual(protocol, location.Protocol);
            Assert.AreEqual(String.Empty, location.Hash);
            Assert.AreEqual(hostname, location.Host);
            Assert.AreEqual(hostname, location.HostName);
            Assert.AreEqual(address, location.Href);
            Assert.AreEqual(path, location.PathName);
            Assert.AreEqual(String.Empty, location.Port);
            Assert.AreEqual(protocol, location.Protocol);
            Assert.AreEqual(address, location.Href);
        }

        [Test]
        public void HtmlAnchorToggleProperties()
        {
            var document = new HtmlDocument();
            var element = document.CreateElement<IHtmlAnchorElement>();

            Assert.IsTrue(element.IsTranslated);
            Assert.IsFalse(element.IsSpellChecked);
            Assert.IsFalse(element.IsContentEditable);
            Assert.IsFalse(element.IsDraggable);
            Assert.IsFalse(element.IsHidden);

            element.IsTranslated = false;
            element.IsSpellChecked = true;
            element.IsDraggable = true;
            element.IsHidden = true;

            Assert.IsFalse(element.IsTranslated);
            Assert.IsTrue(element.IsSpellChecked);
            Assert.IsTrue(element.IsDraggable);
            Assert.IsTrue(element.IsHidden);
        }

        [Test]
        public void HtmlButtonToggleProperties()
        {
            var document = new HtmlDocument();
            var form = document.CreateElement<IHtmlFormElement>();
            var element = document.CreateElement<IHtmlButtonElement>();
            document.AppendChild(form);
            form.AppendChild(element);

            Assert.IsFalse(element.IsDisabled);
            Assert.IsFalse(element.IsDraggable);
            Assert.AreEqual(String.Empty, element.FormMethod);

            element.IsDisabled = true;
            element.IsDraggable = true;
            element.FormMethod = "get";

            Assert.IsTrue(element.IsDisabled);
            Assert.IsTrue(element.IsDraggable);
            Assert.AreEqual("get", element.FormMethod);
        }

        [Test]
        public void HtmlStandardHead()
        {
            var content = @"<!doctype html>
<html class=""no-js"" lang=""en"">
<head>
<meta charset=""utf-8"" />
<meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1"" />
<title>Allgemeines - Webseite von Florian Rappl</title>
<meta name=""keywords"" content=""Florian Rappl, Rappl, Regensburg, Physics, Quantum Chromo Dynamics, QCD, Lattice QCD, IT, C#, .NET, HTML5, JavaScript, Web, Software, Software engineer, Programmer, Modern, Professional,"" />
<meta name=""description"" content=""The personal homepage of Florian Rappl from Regensburg, Germany. Physicist, software engineer, developer and designer for modern information technology."" />
<meta name=""author"" content=""Florian Rappl"" />
<meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
<link href=""/Content/style?v=o2O40dFmfq2JG0tQyfQctozyaA9IcUQxq9b6x16JOKw1"" rel=""stylesheet""/>

<!--[if lt IE 9]><script src=""//html5shim.googlecode.com/svn/trunk/html5.js""></script><![endif]-->
</head><body></body></html>";

            var doc = content.ToHtmlDocument();

            var docType = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType);
            Assert.AreEqual(NodeType.DocumentType, docType.NodeType);
            Assert.AreEqual(@"html", docType.Name);
            
            var html = doc.DocumentElement;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(2, html.Attributes.Count());
            Assert.AreEqual(NodeType.Element, html.NodeType);
            Assert.AreEqual(@"html", html.GetTagName());

            var head = doc.Head;
            Assert.AreEqual(19, head.ChildNodes.Length);
            Assert.AreEqual(0, head.Attributes.Count());
            Assert.AreEqual("head", head.GetTagName());
            Assert.AreEqual(NodeType.Element, head.NodeType);
        }

        [Test]
        public void HtmlCharacterSerialization()
        {
            var content = @"<!doctype html><html><head></head><body></body></html>";
            var doc = content.ToHtmlDocument();

            var body = doc.Body;
            Assert.IsNotNull(body);
            Assert.AreEqual(String.Empty, body.InnerHtml);

            body.TextContent = "&";
            Assert.AreEqual("&amp;", body.InnerHtml);

            body.TextContent = "<";
            Assert.AreEqual("&lt;", body.InnerHtml);

            body.TextContent = ">";
            Assert.AreEqual("&gt;", body.InnerHtml);
        }

        [Test]
        public void HtmlWithLangAttributeFromString()
        {
            var content = @"<html>
<head>
<meta http-equiv=Content-Type content=""text/html; charset=utf-8"">
</head>
<body lang=RU style='tab-interval:35.4pt'>
<!--StartFragment-->
<p class=MsoNormal><span lang=EN-US style='mso-ansi-language:EN-US'>тест</span><o:p></o:p></p>
<!--EndFragment-->
</body>
</html>";
            var doc = content.ToHtmlDocument();

            var body = doc.Body;
            var span = body.QuerySelector("span") as HtmlSpanElement;
            Assert.AreEqual("RU", body.Language);
            Assert.AreEqual("тест", span.TextContent);
            Assert.AreEqual("EN-US", span.GetAttribute("lang"));
            Assert.AreEqual("EN-US", span.Language);
            Assert.AreEqual("mso-ansi-language:EN-US", span.GetAttribute("style"));
        }

        [Test]
        public void HtmlWithLangAttributeFromStream()
        {
            var fs = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("AngleSharp.Core.Tests.Pages.encoding.html");
            var doc = fs.ToHtmlDocument();

            var body = doc.Body;
            var span = body.QuerySelector("span") as HtmlSpanElement;
            Assert.AreEqual("RU", body.Language);
            Assert.AreEqual("тест", span.TextContent);
            Assert.AreEqual("EN-US", span.GetAttribute("lang"));
            Assert.AreEqual("EN-US", span.Language);
            Assert.AreEqual("mso-ansi-language:EN-US", span.GetAttribute("style"));

            fs.Dispose();
        }

        [Test]
        public void TitleRemovalAndAssignment()
        {
            var document = ("<title>sample</title>").ToHtmlDocument();
            var head = document.DocumentElement.FirstChild;
            head.RemoveChild(head.FirstChild);
            Assert.AreEqual("", document.Title);
            document.Title = "";
            Assert.AreEqual("", document.Title);
            Assert.IsInstanceOf<HtmlTitleElement>(head.LastChild);
            Assert.IsNull(head.LastChild.FirstChild);
        }

        [Test]
        public void HeadDuplicatedAndInserted()
        {
            var document = ("").ToHtmlDocument();
            var head = document.GetElementsByTagName("head")[0];
            Assert.AreEqual(head, document.Head);
            document.DocumentElement.AppendChild(document.CreateElement("head"));
            Assert.AreEqual(head, document.Head);
            var head2 = document.CreateElement("head");
            document.DocumentElement.InsertBefore(head2, head);
            Assert.AreEqual(head2, document.Head);
        }

        [Test]
        public void CreateMarkElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var mark = document.CreateElement("mark");
            Assert.AreEqual("MARK", mark.TagName);
            Assert.IsInstanceOf<IHtmlElement>(mark);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(mark);
        }

        [Test]
        public void CreateDefineElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var dfn = document.CreateElement("dfn");
            Assert.AreEqual("DFN", dfn.TagName);
            Assert.IsInstanceOf<IHtmlElement>(dfn);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(dfn);
        }

        [Test]
        public void CreateKeyboardElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var kbd = document.CreateElement("kbd");
            Assert.AreEqual("KBD", kbd.TagName);
            Assert.IsInstanceOf<IHtmlElement>(kbd);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(kbd);
        }

        [Test]
        public void CreateBdoElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var bdo = document.CreateElement("bdo");
            Assert.AreEqual("BDO", bdo.TagName);
            Assert.IsInstanceOf<IHtmlElement>(bdo);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(bdo);
        }

        [Test]
        public void CreateSpanElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var span = document.CreateElement("span");
            Assert.AreEqual("SPAN", span.TagName);
            Assert.IsInstanceOf<IHtmlElement>(span);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(span);
        }

        [Test]
        public void CreateProgressElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var progress = document.CreateElement("progress");
            Assert.AreEqual("PROGRESS", progress.TagName);
            Assert.IsInstanceOf<IHtmlElement>(progress);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(progress);
        }

        [Test]
        public void CreateTimeElementIsNotUnknown()
        {
            var document = ("").ToHtmlDocument();
            var time = document.CreateElement("time");
            Assert.AreEqual("TIME", time.TagName);
            Assert.IsInstanceOf<IHtmlElement>(time);
            Assert.IsNotInstanceOf<IHtmlUnknownElement>(time);
        }

        [Test]
        public void Base64ImageTestIsChanged()
        {
            var data = "data:image/png;base64,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";
            var source = "<p> <img src=\"" + data + "\" /></p>";
            var document = source.ToHtmlDocument();
            var img = document.QuerySelector("img");
            Assert.AreEqual(data, img.GetAttribute("src"));
            Assert.AreEqual(data, ((IHtmlImageElement)img).Source);
        }

        [Test]
        public void CreateDeepUnknownElementDocument()
        {
            var document = String.Empty.ToHtmlDocument();
            var html = String.Join("", Enumerable.Repeat("<xyz>", 50 * 1000));
            document.Body.InnerHtml = html;
            Assert.True(true);
        }

        [Test]
        public void SettingBaseUrlForDeepElements()
        {
            var document = String.Empty.ToHtmlDocument();
            var html = String.Join("", Enumerable.Repeat("<xyz>", 50 * 1000)) + "<img src=\"http://www.example.com/\">";
            document.Body.InnerHtml = html;
            Assert.True(true);
        }
    }
}
