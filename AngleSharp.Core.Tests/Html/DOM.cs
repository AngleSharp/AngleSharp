namespace AngleSharp.Core.Tests
{
    using System;
    using System.Linq;
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Events;
    using AngleSharp.Html;
    using NUnit.Framework;

    [TestFixture]
    public class DOMTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ClosingSpanTagShouldNotResultInAnError()
        {
            var events = new EventReceiver<HtmlParseErrorEvent>();
            var config = new Configuration(events: events);
            var source = @"<!DOCTYPE html><html><head></head><body><span>test</span></body></html>";
            var document = source.ToHtmlDocument(config);
            Assert.AreEqual(0, events.Received.Count);

        }

        [Test]
        public void AppendMultipleNodesToParentNode()
        {
            var document = Html("");
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
            var div = new HtmlDivElement(document);
            div.ClassName = "";
            div.ClassList.Add(testClass);
            Assert.AreEqual(testClass, div.ClassName);
        }

        [Test]
        public void DOMTokenListCorrectlyInitializedFindsClass()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var div = new HtmlDivElement(document);
            div.ClassName = testClass + " whatever anotherclass";
            Assert.IsTrue(div.ClassList.Contains(testClass));
        }

        [Test]
        public void DOMTokenListCorrectlyInitializedNoClass()
        {
            var document = new HtmlDocument();
            var testClass = "myclass1";
            var div = new HtmlDivElement(document);
            div.ClassName = "myclass2 whatever anotherclass";
            Assert.IsFalse(div.ClassList.Contains(testClass));
        }

        [Test]
        public void DOMTokenListToggleWorksTurnOff()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var otherClasses = "otherClass someOther more";
            var div = new HtmlDivElement(document);
            div.ClassName = testClass + " " + otherClasses;
            div.ClassList.Toggle(testClass);
            Assert.AreEqual(div.ClassName, otherClasses);
        }

        [Test]
        public void DOMTokenListToggleWorksTurnOn()
        {
            var document = new HtmlDocument();
            var testClass = "myclass";
            var otherClasses = "otherClass someOther more";
            var div = new HtmlDivElement(document);
            div.ClassName = otherClasses;
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
            var html = document.CreateElement(Tags.Html);
            var head = document.CreateElement(Tags.Head);
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
        public void CSSStyleDeclarationEmpty()
        {
            var css = new CssStyleDeclaration();
            Assert.AreEqual("", css.CssText);
            Assert.AreEqual(0, css.Length);
        }

        [Test]
        public void CSSStyleDeclarationUnbound()
        {
            var css = new CssStyleDeclaration();
            var text = "background-color: red; color: black;";
            css.CssText = text;
            Assert.AreEqual(text, css.CssText);
            Assert.AreEqual(2, css.Length);
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
        public void CSSStyleDeclarationBoundOutboundDirectionIndirect()
        {
            var context = BrowsingContext.New(new Configuration().WithCss());
            var document = new HtmlDocument(context);
            var element = document.CreateElement<IHtmlSpanElement>();
            var text = "background-color: red; color: black;";
            element.SetAttribute("style", text);
            Assert.AreEqual(text, element.Style.CssText);
            Assert.AreEqual(2, element.Style.Length);
        }

        [Test]
        public void CSSStyleDeclarationBoundOutboundDirectionDirect()
        {
            var context = BrowsingContext.New(new Configuration().WithCss());
            var document = new HtmlDocument(context);
            var element = document.CreateElement<IHtmlSpanElement>();
            var text = "background-color: red; color: black;";
            element.SetAttribute("style", String.Empty);
            Assert.AreEqual(String.Empty, element.Style.CssText);
            element.SetAttribute("style", text);
            Assert.AreEqual(text, element.Style.CssText);
            Assert.AreEqual(2, element.Style.Length);
        }

        [Test]
        public void CSSStyleDeclarationBoundInboundDirection()
        {
            var context = BrowsingContext.New(new Configuration().WithCss());
            var document = new HtmlDocument(context);
            var element = new HtmlSpanElement(document);
            var text = "background-color: red; color: black;";
            element.Style.CssText = text;
            Assert.AreEqual(text, element.GetAttribute("style"));
            Assert.AreEqual(2, element.Style.Length);
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
            var document = Html("<title>sample</title>");
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
            var document = Html("");
            var head = document.GetElementsByTagName("head")[0];
            Assert.AreEqual(head, document.Head);
            document.DocumentElement.AppendChild(document.CreateElement("head"));
            Assert.AreEqual(head, document.Head);
            var head2 = document.CreateElement("head");
            document.DocumentElement.InsertBefore(head2, head);
            Assert.AreEqual(head2, document.Head);
        }
    }
}
