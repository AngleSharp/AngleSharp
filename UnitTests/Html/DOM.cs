using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class DOMTests
    {
        [TestMethod]
        public void DOMTokenListWritesBack()
        {
            var testClass = "myclass";
            var div = new HTMLDivElement();
            div.ClassName = "";
            div.ClassList.Add(testClass);
            Assert.AreEqual(testClass, div.ClassName);
        }

        [TestMethod]
        public void DOMTokenListCorrectlyInitializedFindsClass()
        {
            var testClass = "myclass";
            var div = new HTMLDivElement();
            div.ClassName = testClass + " whatever anotherclass";
            Assert.IsTrue(div.ClassList.Contains(testClass));
        }

        [TestMethod]
        public void DOMTokenListCorrectlyInitializedNoClass()
        {
            var testClass = "myclass1";
            var div = new HTMLDivElement();
            div.ClassName = "myclass2 whatever anotherclass";
            Assert.IsFalse(div.ClassList.Contains(testClass));
        }

        [TestMethod]
        public void DOMTokenListToggleWorksTurnOff()
        {
            var testClass = "myclass";
            var otherClasses = "otherClass someOther more";
            var div = new HTMLDivElement();
            div.ClassName = testClass + " " + otherClasses;
            div.ClassList.Toggle(testClass);
            Assert.AreEqual(div.ClassName, otherClasses);
        }

        [TestMethod]
        public void DOMTokenListToggleWorksTurnOn()
        {
            var testClass = "myclass";
            var otherClasses = "otherClass someOther more";
            var div = new HTMLDivElement();
            div.ClassName = otherClasses;
            div.ClassList.Toggle(testClass);
            Assert.AreEqual(div.ClassName, otherClasses + " " + testClass);
        }

        [TestMethod]
        public void DOMStringMapBindingGet()
        {
            var value = "SomeUser";
            var div = new HTMLDivElement();
            div.SetAttribute("data-user", value);
            Assert.AreEqual(div.Dataset["user"], value);
        }

        [TestMethod]
        public void DOMStringMapBindingSet()
        {
            var value = "SomeUser";
            var div = new HTMLDivElement();
            div.Dataset["user"] = value;
            Assert.AreEqual(div.GetAttribute("data-user"), value);
        }

        [TestMethod]
        public void DOMStringMapHasNoAttribute()
        {
            var div = new HTMLDivElement();
            Assert.IsTrue(div.Dataset["user"] == null);
        }

        [TestMethod]
        public void DOMStringMapHasAttributesButRequestedMissing()
        {
            var div = new HTMLDivElement();
            div.SetAttribute("data-some", "test");
            div.SetAttribute("data-another", "");
            div.SetAttribute("data-test", "third attribute");
            Assert.IsTrue(div.Dataset["user"] == null);
        }

        [TestMethod]
        public void DOMStringMapIEnumerableWorking()
        {
            var div = new HTMLDivElement();
            div.SetAttribute("data-some", "test");
            div.SetAttribute("data-another", "");
            div.SetAttribute("data-test", "third attribute");
            Assert.AreEqual(3, div.Dataset.Count());
            Assert.AreEqual("some", div.Dataset.First().Key);
            Assert.AreEqual("test", div.Dataset.First().Value);
            Assert.AreEqual("test", div.Dataset.Last().Key);
            Assert.AreEqual("third attribute", div.Dataset.Last().Value);
        }

        [TestMethod]
        public void HtmlCustomTitleGeneration()
        {
            var doc = new Document();
            var title = "My Title";
            doc.Title = title;
            Assert.AreEqual("", doc.Title);
            var html = doc.CreateElement(Tags.Html);
            var head = doc.CreateElement(Tags.Head);
            doc.AppendChild(html);
            html.AppendChild(head);
            doc.Title = title;
            Assert.AreEqual(title, doc.Title);
        }

        [TestMethod]
        public void HtmlHasRightHeadElement()
        {
            var doc = new Document();
            var root = new HTMLHtmlElement();
            doc.AppendChild(root);
            var head = new HTMLHeadElement();
            root.AppendChild(head);
            Assert.AreEqual(head, doc.Head);
        }

        [TestMethod]
        public void HtmlHasRightBodyElement()
        {
            var doc = new Document();
            var root = new HTMLHtmlElement();
            doc.AppendChild(root);
            var body = new HTMLBodyElement();
            root.AppendChild(body);
            Assert.AreEqual(body, doc.Body);
        }

        [TestMethod]
        public void NormalizeRemovesEmptyTextNodes()
        {
            var div = new HTMLDivElement();
            div.AppendChild(new HTMLAnchorElement());
            div.AppendChild(new TextNode());
            div.AppendChild(new HTMLDivElement());
            div.AppendChild(new TextNode("Hi there!"));
            div.AppendChild(new HTMLImageElement());
            div.Normalize();
            Assert.AreEqual(div.ChildNodes.Length, 4);
        }

        [TestMethod]
        public void NormalizeRemovesEmptyTextNodesNested()
        {
            var div = new HTMLDivElement();
            var a = new HTMLAnchorElement();
            a.AppendChild(new TextNode());
            a.AppendChild(new TextNode("Not empty."));
            div.AppendChild(a);
            div.AppendChild(new TextNode());
            div.AppendChild(new HTMLDivElement());
            div.AppendChild(new TextNode("Certainly not empty!"));
            div.AppendChild(new HTMLImageElement());
            div.Normalize();
            Assert.AreEqual(a.ChildNodes.Length, 1);
        }

        [TestMethod]
        public void NormalizeMergeTextNodes()
        {
            var div = new HTMLDivElement();
            var a = new HTMLAnchorElement();
            a.AppendChild(new TextNode());
            a.AppendChild(new TextNode("Not empty."));
            div.AppendChild(a);
            div.AppendChild(new TextNode());
            div.AppendChild(new HTMLDivElement());
            div.AppendChild(new TextNode("Certainly not empty!"));
            div.AppendChild(new TextNode("Certainly not empty!"));
            div.AppendChild(new TextNode("Certainly not empty!"));
            div.AppendChild(new TextNode("Certainly not empty!"));
            div.AppendChild(new HTMLImageElement());
            div.Normalize();
            Assert.AreEqual(div.ChildNodes.Length, 4);
        }

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
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

        [TestMethod]
        public void CSSStyleDeclarationEmpty()
        {
            var css = new CSSStyleDeclaration();
            Assert.AreEqual("", css.CssText);
            Assert.AreEqual(0, css.Length);
        }

        [TestMethod]
        public void CSSStyleDeclarationUnbound()
        {
            var css = new CSSStyleDeclaration();
            var text = "background: red; color: black;";
            css.CssText = text;
            Assert.AreEqual(text, css.CssText);
            Assert.AreEqual(2, css.Length);
        }

        [TestMethod]
        public void CSSStyleDeclarationBoundOutboundDirectionIndirect()
        {
            var doc = new Document();
            var element = doc.CreateElement<IHtmlSpanElement>();
            var text = "background: red; color: black;";
            element.SetAttribute("style", text);
            Assert.AreEqual(text, element.Style.CssText);
            Assert.AreEqual(2, element.Style.Length);
        }

        [TestMethod]
        public void CSSStyleDeclarationBoundOutboundDirectionDirect()
        {
            var doc = new Document();
            var element = doc.CreateElement<IHtmlSpanElement>();
            var text = "background: red; color: black;";
            element.SetAttribute("style", String.Empty);
            Assert.AreEqual(String.Empty, element.Style.CssText);
            element.SetAttribute("style", text);
            Assert.AreEqual(text, element.Style.CssText);
            Assert.AreEqual(2, element.Style.Length);
        }

        [TestMethod]
        public void CSSStyleDeclarationBoundInboundDirection()
        {
            var element = new HTMLSpanElement();
            var text = "background: red; color: black;";
            element.Style.CssText = text;
            Assert.AreEqual(text, element.GetAttribute("style"));
            Assert.AreEqual(2, element.Style.Length);
        }

        [TestMethod]
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

            var doc = DocumentBuilder.Html(content);

            var docType = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType);
            Assert.AreEqual(NodeType.DocumentType, docType.NodeType);
            Assert.AreEqual(@"html", docType.Name);
            
            var html = doc.DocumentElement;
            Assert.AreEqual(2, html.ChildNodes.Length);
            Assert.AreEqual(2, html.Attributes.Count());
            Assert.AreEqual(NodeType.Element, html.NodeType);
            Assert.AreEqual(@"html", html.NodeName);

            var head = doc.Head;
            Assert.AreEqual(19, head.ChildNodes.Length);
            Assert.AreEqual(0, head.Attributes.Count());
            Assert.AreEqual("head", head.NodeName);
            Assert.AreEqual(NodeType.Element, head.NodeType);
        }
    }
}
