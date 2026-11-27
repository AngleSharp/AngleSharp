namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The expectations of the issue 1434 tests were checked against Chrome 131 with the same
    /// inputs, attribute by attribute (name, local name, prefix, namespace, value, order).
    /// </summary>
    [TestFixture]
    public class ForeignAttributesTests
    {
        // Reference behaviour taken from Blink and WebKit; both agree on every row.
        [TestCase("<svg xml:lang[>", "xml:lang[", null, null, "")]
        [TestCase("<svg xml:base[>", "xml:base[", null, null, "")]
        [TestCase("<svg xml:space[>", "xml:space[", null, null, "")]
        [TestCase("<svg xml:lang<x=y>", "xml:lang<x", null, null, "y")]
        [TestCase("<svg xml:lang\"x=1>", "xml:lang\"x", null, null, "1")]
        [TestCase("<math xml:lang[>", "xml:lang[", null, null, "")]
        [TestCase("<svg xlink:href[>", "xlink:href[", null, null, "")]
        [TestCase("<svg xmlns:foo[>", "xmlns:foo[", null, null, "")]
        [TestCase("<svg xml:lang=\"en\">", "xml:lang", "http://www.w3.org/XML/1998/namespace", "xml", "en")]
        [TestCase("<svg xml:space=\"preserve\">", "xml:space", "http://www.w3.org/XML/1998/namespace", "xml", "preserve")]
        [TestCase("<svg xmlns=\"u\">", "xmlns", "http://www.w3.org/2000/xmlns/", null, "u")]
        [TestCase("<svg xmlns:xlink=\"u\">", "xmlns:xlink", "http://www.w3.org/2000/xmlns/", "xmlns", "u")]
        [TestCase("<svg xml:langue=\"1\">", "xml:langue", null, null, "1")]
        [TestCase("<svg xml:spaceship=\"1\">", "xml:spaceship", null, null, "1")]
        [TestCase("<svg xml:base=\"b\">", "xml:base", null, null, "b")]
        public void ForeignAttributeMatchesBrowserBehavior_Issue1294(String source, String name, String namespaceUri, String prefix, String value)
        {
            var doc = source.ToHtmlDocument();
            var element = doc.Body.FirstElementChild;
            Assert.AreEqual(1, element.Attributes.Length);

            var attr = element.Attributes.GetNamedItem(name);
            Assert.IsNotNull(attr);
            Assert.AreEqual(value, attr.Value);
            Assert.AreEqual(namespaceUri, attr.NamespaceUri);
            Assert.AreEqual(prefix, attr.Prefix);
        }

        [TestCase("xlink:actuate", "actuate")]
        [TestCase("xlink:arcrole", "arcrole")]
        [TestCase("xlink:href", "href")]
        [TestCase("xlink:role", "role")]
        [TestCase("xlink:show", "show")]
        [TestCase("xlink:title", "title")]
        [TestCase("xlink:type", "type")]
        public void XLinkAttributeInAdjustmentTableIsNamespaced_Issue1294(String name, String localName)
        {
            var doc = ("<svg " + name + "=\"v\">").ToHtmlDocument();
            var element = doc.Body.FirstElementChild;
            var attr = element.Attributes.GetNamedItem(name);
            Assert.IsNotNull(attr);
            Assert.AreEqual("v", attr.Value);
            Assert.AreEqual(name, attr.Name);
            Assert.AreEqual(localName, attr.LocalName);
            Assert.AreEqual(NamespaceNames.XLinkUri, attr.NamespaceUri);
            Assert.AreEqual("xlink", attr.Prefix);
            Assert.AreSame(attr, element.Attributes.GetNamedItem(NamespaceNames.XLinkUri, localName));
            Assert.IsNull(element.Attributes.GetNamedItem(localName));
            Assert.AreEqual("v", element.GetAttribute(name));
            Assert.AreEqual("v", element.GetAttribute(NamespaceNames.XLinkUri, localName));
            Assert.IsNull(element.GetAttribute(localName));
        }

        [TestCase("<math><mi xlink:href=\"v\"></mi></math>")]
        [TestCase("<svg><foreignObject xlink:href=\"v\"></foreignObject></svg>")]
        public void XLinkAttributeKeepsItsPrefixOnEveryForeignElement_Issue1434(String source)
        {
            var element = source.ToHtmlDocument().QuerySelector("mi, foreignObject");
            var attr = element.Attributes[0];
            Assert.AreEqual("xlink:href", attr.Name);
            Assert.AreEqual("xlink", attr.Prefix);
            Assert.AreEqual("href", attr.LocalName);
            Assert.AreEqual(NamespaceNames.XLinkUri, attr.NamespaceUri);
        }

        // Reference behaviour from the issue: a browser keeps both attributes in source order,
        // whichever of the two comes first.
        [TestCase("<svg><a href=\"https://y.example/\" xlink:href=\"https://x.example/\"></a></svg>", 0, 1)]
        [TestCase("<svg><a xlink:href=\"https://x.example/\" href=\"https://y.example/\"></a></svg>", 1, 0)]
        public void XLinkAttributeDoesNotReplacePlainAttributeOfSameLocalName_Issue1434(String source, Int32 plainIndex, Int32 xlinkIndex)
        {
            var element = source.ToHtmlDocument().QuerySelector("a");
            Assert.AreEqual(2, element.Attributes.Length);

            var plain = element.Attributes[plainIndex];
            Assert.AreEqual("href", plain.Name);
            Assert.AreEqual("href", plain.LocalName);
            Assert.IsNull(plain.Prefix);
            Assert.IsNull(plain.NamespaceUri);
            Assert.AreEqual("https://y.example/", plain.Value);

            var xlink = element.Attributes[xlinkIndex];
            Assert.AreEqual("xlink:href", xlink.Name);
            Assert.AreEqual("href", xlink.LocalName);
            Assert.AreEqual("xlink", xlink.Prefix);
            Assert.AreEqual(NamespaceNames.XLinkUri, xlink.NamespaceUri);
            Assert.AreEqual("https://x.example/", xlink.Value);

            Assert.AreEqual("https://y.example/", element.GetAttribute("href"));
            Assert.AreEqual("https://y.example/", element.GetAttribute(null, "href"));
            Assert.AreEqual("https://x.example/", element.GetAttribute("xlink:href"));
            Assert.AreEqual("https://x.example/", element.GetAttribute(NamespaceNames.XLinkUri, "href"));
        }

        [Test]
        public void XLinkAttributeRoundTripsThroughSerialization_Issue1434()
        {
            var source = "<svg><a href=\"y\" xlink:href=\"x\"></a></svg>";
            var html = source.ToHtmlDocument().Body.InnerHtml;
            Assert.AreEqual(source, html);
            Assert.AreEqual(source, html.ToHtmlDocument().Body.InnerHtml);
        }

        [Test]
        public void SetAttributeNsDoesNotReplaceAttributeOfAnotherNamespace_Issue1434()
        {
            var element = "<svg><a href=\"y\"></a></svg>".ToHtmlDocument().QuerySelector("a");

            element.SetAttribute(NamespaceNames.XLinkUri, "xlink:href", "x");

            Assert.AreEqual(2, element.Attributes.Length);
            Assert.AreEqual("y", element.GetAttribute("href"));
            Assert.AreEqual("x", element.GetAttribute(NamespaceNames.XLinkUri, "href"));
            Assert.AreEqual("xlink:href", element.Attributes[1].Name);
        }

        [Test]
        public void SetAttributeNsChangesTheExistingAttributeOfThatNamespaceAndLocalName_Issue1434()
        {
            // https://dom.spec.whatwg.org/#concept-element-attributes-set-value - an existing
            // attribute is changed in place, so it keeps its prefix and its identity.
            var element = "<svg><a xlink:href=\"x\"></a></svg>".ToHtmlDocument().QuerySelector("a");
            var attr = element.Attributes[0];

            element.SetAttribute(NamespaceNames.XLinkUri, "other:href", "z");

            Assert.AreEqual(1, element.Attributes.Length);
            Assert.AreSame(attr, element.Attributes[0]);
            Assert.AreEqual("xlink:href", attr.Name);
            Assert.AreEqual("z", attr.Value);
        }

        [Test]
        public void SetAttributeNsWithoutPrefixDoesNotReplacePrefixedAttributeOfSameName_Issue1434()
        {
            // An attribute without a namespace that happens to be called like a prefixed one
            // still differs from it, as the namespace and the local name do.
            var element = "<svg><a xlink:href=\"x\"></a></svg>".ToHtmlDocument().QuerySelector("a");

            element.SetAttribute(null, "href", "y");

            Assert.AreEqual(2, element.Attributes.Length);
            Assert.AreEqual("x", element.GetAttribute(NamespaceNames.XLinkUri, "href"));
            Assert.AreEqual("y", element.GetAttribute(null, "href"));
        }

        [Test]
        public void SetAttributeNsIsReportedToMutationObservers_Issue1434()
        {
            var document = "<svg><a href=\"y\"></a></svg>".ToHtmlDocument();
            var element = document.QuerySelector("a");
            var records = new List<IMutationRecord>();
            var observer = new MutationObserver((mutations, _) => records.AddRange(mutations));
            observer.Connect(element, attributes: true, attributeOldValue: true);

            element.SetAttribute(NamespaceNames.XLinkUri, "xlink:href", "x");
            element.SetAttribute(NamespaceNames.XLinkUri, "xlink:href", "z");

            Assert.AreEqual(2, records.Count);
            Assert.AreEqual("href", records[0].AttributeName);
            Assert.AreEqual(NamespaceNames.XLinkUri, records[0].AttributeNamespace);
            Assert.IsNull(records[0].PreviousValue);
            Assert.AreEqual("x", records[1].PreviousValue);
        }

        [Test]
        public void SetAttributeWithQualifiedNameChangesTheNamespacedAttribute_Issue1434()
        {
            // https://dom.spec.whatwg.org/#dom-element-setattribute looks up by qualified name.
            var element = "<svg><a xlink:href=\"x\"></a></svg>".ToHtmlDocument().QuerySelector("a");
            var attr = element.Attributes[0];

            element.SetAttribute("xlink:href", "q");

            Assert.AreEqual(1, element.Attributes.Length);
            Assert.AreSame(attr, element.Attributes[0]);
            Assert.AreEqual("q", attr.Value);
            Assert.AreEqual(NamespaceNames.XLinkUri, attr.NamespaceUri);
        }

        [Test]
        public void SetAttributeAndReflectedPropertyChangeTheExistingAttributeInPlace()
        {
            var element = "<svg><a id=\"i\" href=\"y\"></a></svg>".ToHtmlDocument().QuerySelector("a");
            var id = element.Attributes[0];
            var href = element.Attributes[1];

            element.Id = "j";
            element.SetAttribute("href", "q");

            Assert.AreEqual(2, element.Attributes.Length);
            Assert.AreSame(id, element.Attributes[0]);
            Assert.AreSame(href, element.Attributes[1]);
            Assert.AreEqual("j", id.Value);
            Assert.AreEqual("q", href.Value);
        }

        // https://dom.spec.whatwg.org/#concept-element-attributes-set - setNamedItem and
        // setNamedItemNS are the same algorithm, matching by namespace and local name.
        [TestCase(false)]
        [TestCase(true)]
        public void SetNamedItemDoesNotReplaceAttributeOfAnotherNamespace_Issue1434(Boolean ns)
        {
            var document = "<svg><a href=\"y\"></a></svg>".ToHtmlDocument();
            var element = document.QuerySelector("a");
            var attr = document.CreateAttribute(NamespaceNames.XLinkUri, "xlink:href");
            attr.Value = "x";

            var old = ns ? element.Attributes.SetNamedItemWithNamespaceUri(attr) : element.Attributes.SetNamedItem(attr);

            Assert.IsNull(old);
            Assert.AreEqual(2, element.Attributes.Length);
            Assert.AreEqual("y", element.GetAttribute(null, "href"));
            Assert.AreEqual("x", element.GetAttribute(NamespaceNames.XLinkUri, "href"));
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SetNamedItemReplacesAttributeOfSameNamespaceAndLocalName_Issue1434(Boolean ns)
        {
            var document = "<svg><a xlink:href=\"x\"></a></svg>".ToHtmlDocument();
            var element = document.QuerySelector("a");
            var previous = element.Attributes[0];
            var attr = document.CreateAttribute(NamespaceNames.XLinkUri, "other:href");
            attr.Value = "z";

            var old = ns ? element.Attributes.SetNamedItemWithNamespaceUri(attr) : element.Attributes.SetNamedItem(attr);

            Assert.AreSame(previous, old);
            Assert.AreEqual("xlink:href", old.Name);
            Assert.AreEqual("x", old.Value);
            Assert.AreEqual(1, element.Attributes.Length);
            Assert.AreSame(attr, element.Attributes[0]);
            Assert.AreEqual("other:href", element.Attributes[0].Name);
        }

        [TestCase(false)]
        [TestCase(true)]
        public void SetNamedItemDoesNotReplaceNamespacedAttributeOfSameQualifiedName_Issue1434(Boolean ns)
        {
            var document = "<svg><a xlink:href=\"x\"></a></svg>".ToHtmlDocument();
            var element = document.QuerySelector("a");
            var attr = document.CreateAttribute("xlink:href");
            attr.Value = "z";

            var old = ns ? element.Attributes.SetNamedItemWithNamespaceUri(attr) : element.Attributes.SetNamedItem(attr);

            Assert.IsNull(old);
            Assert.AreEqual(2, element.Attributes.Length);
            Assert.AreEqual(NamespaceNames.XLinkUri, element.Attributes[0].NamespaceUri);
            Assert.AreEqual("x", element.Attributes[0].Value);
            Assert.IsNull(element.Attributes[1].NamespaceUri);
            Assert.AreEqual("xlink:href", element.Attributes[1].LocalName);
            Assert.AreEqual("z", element.Attributes[1].Value);
        }

        [TestCase("xlink:hrefs")]
        [TestCase("xlink:foo")]
        public void XLinkAttributeNotInAdjustmentTableIsNotNamespaced_Issue1294(String name)
        {
            var doc = ("<svg " + name + "=\"v\">").ToHtmlDocument();
            var attr = doc.Body.FirstElementChild.Attributes.GetNamedItem(name);
            Assert.IsNotNull(attr);
            Assert.AreEqual("v", attr.Value);
            Assert.AreEqual(null, attr.NamespaceUri);
            Assert.AreEqual(null, attr.Prefix);
        }
    }
}
