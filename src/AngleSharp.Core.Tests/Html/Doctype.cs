namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/doctype01.dat
    /// </summary>
    [TestFixture]
    public class DoctypeTests
    {
        [Test]
        public void DoctypeHtml5Standard()
        {
            var doc = (@"<!DOCTYPE html>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeHtml5MixedCase()
        {
            var doc = (@"<!dOctYpE HtMl>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeHtml5MissingSpace()
        {
            var doc = (@"<!DOCTYPEhtml>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeWithoutName()
        {
            var doc = (@"<!DOCTYPE>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeOnlySpace()
        {
            var doc = (@"<!DOCTYPE >Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPotato()
        {
            var doc = (@"<!DOCTYPE potato>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomTrailingSpace()
        {
            var doc = (@"<!DOCTYPE potato >Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomAdditionalIdentifier()
        {
            var doc = (@"<!DOCTYPE potato taco>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomAdditionalIdentifierAndString()
        {
            var doc = (@"<!DOCTYPE potato taco ""ddd>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifier()
        {
            var doc = (@"<!DOCTYPE potato sYstEM>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifierTrailingSpaces()
        {
            var doc = (@"<!DOCTYPE potato sYstEM    >Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifierAdditionalIdentifierWithMoreSpaces()
        {
            var doc = (@"<!DOCTYPE   potato       sYstEM  ggg>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifierAdditionalIdentifierTrailingSpaces()
        {
            var doc = (@"<!DOCTYPE potato SYSTEM taco  >Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifierWithInformationDoubleInSingleQuotes()
        {
            var doc = (@"<!DOCTYPE potato SYSTEM 'taco""'>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("taco\"", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifierWithInformationNormalQuotes()
        {
            var doc = (@"<!DOCTYPE potato SYSTEM ""taco"">Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("taco", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomSystemIdentifierWithInformationSingleInDoubleQuotes()
        {
            var doc = (@"<!DOCTYPE potato SYSTEM ""tai'co"">Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("tai'co", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomInvalidSystemIdentifier()
        {
            var doc = (@"<!DOCTYPE potato SYSTEMtaco ""ddd"">Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomUnknownIdentifierBeforeSystemIdentifier()
        {
            var doc = (@"<!DOCTYPE potato grass SYSTEM taco>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifier()
        {
            var doc = (@"<!DOCTYPE potato pUbLIc>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifierTrailingSpace()
        {
            var doc = (@"<!DOCTYPE potato pUbLIc >Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomInvalidPublicIdentifier()
        {
            var doc = (@"<!DOCTYPE potato pUbLIcgoof>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifierWithAdditionalIdentifier()
        {
            var doc = (@"<!DOCTYPE potato PUBLIC goof>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifierWithInformationSingleQuoteInDoubleQuotes()
        {
            var doc = (@"<!DOCTYPE potato PUBLIC ""go'of"">Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("go'of", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifierWithInformationSingleQuotesWithInvalidIdentifier()
        {
            var doc = (@"<!DOCTYPE potato PUBLIC 'go'of'>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("go", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifierWithInformationSingleQuotes()
        {
            var doc = (@"<!DOCTYPE potato PUBLIC 'go:hh   of' >Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("go:hh   of", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomPublicIdentifierWithInformationAndSystemIdentifierWithInvalidIdentifier()
        {
            var doc = (@"<!DOCTYPE potato PUBLIC ""W3C-//dfdf"" SYSTEM ggg>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("potato", docType0.Name);
            Assert.AreEqual("W3C-//dfdf", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemStrictWithLineBreak()
        {
            var doc = (@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN""
   ""http://www.w3.org/TR/html4/strict.dtd"">Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD HTML 4.01//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/html4/strict.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeCustomDots()
        {
            var doc = (@"<!DOCTYPE ...>Hello").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("...", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("Hello", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemTransitionalWithLineBreak()
        {
            var doc = (@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN""
""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD XHTML 1.0 Transitional//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemFramesetWithLineBreak()
        {
            var doc = (@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Frameset//EN""
""http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd"">").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD XHTML 1.0 Frameset//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/xhtml1/DTD/xhtml1-frameset.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void DoctypeCustomInvalidIdentifiers()
        {
            var doc = (@"<!DOCTYPE root-element [SYSTEM OR PUBLIC FPI] ""uri"" [ 
<!-- internal declarations -->
]>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"root-element", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("]>", dochtml1body1Text0.TextContent);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemUnknownWithLineBreaks()
        {
            var doc = (@"<!DOCTYPE html PUBLIC
  ""-//WAPFORUM//DTD XHTML Mobile 1.0//EN""
    ""http://www.wapforum.org/DTD/xhtml-mobile10.dtd"">").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//WAPFORUM//DTD XHTML Mobile 1.0//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.wapforum.org/DTD/xhtml-mobile10.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void DoctypeHtmlSystemStrictWithBodyFollowing()
        {
            var doc = (@"<!DOCTYPE HTML SYSTEM ""http://www.w3.org/DTD/HTML4-strict.dtd""><body><b>Mine!</b></body>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/DTD/HTML4-strict.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

            var dochtml1body1b0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(1, dochtml1body1b0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1b0).Attributes.Length);
            Assert.AreEqual("b", dochtml1body1b0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1b0.NodeType);

            var dochtml1body1b0Text0 = dochtml1body1b0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1b0Text0.NodeType);
            Assert.AreEqual("Mine!", dochtml1body1b0Text0.TextContent);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemStrictFollowedDirectlySameQuotesBothDouble()
        {
            var doc = (@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN""""http://www.w3.org/TR/html4/strict.dtd"">").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD HTML 4.01//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/html4/strict.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemStrictFollowedDirectlyDifferentQuotesLeadingSingle()
        {
            var doc = (@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.01//EN""'http://www.w3.org/TR/html4/strict.dtd'>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD HTML 4.01//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/html4/strict.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemStrictFollowedDirectlyDifferentQuotesLeadingDouble()
        {
            var doc = (@"<!DOCTYPE HTML PUBLIC""-//W3C//DTD HTML 4.01//EN""'http://www.w3.org/TR/html4/strict.dtd'>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD HTML 4.01//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/html4/strict.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);

        }

        [Test]
        public void DoctypeHtmlPublicAndSystemStrictFollowedDirectlySameQuotesBothSingle()
        {
            var doc = (@"<!DOCTYPE HTML PUBLIC'-//W3C//DTD HTML 4.01//EN''http://www.w3.org/TR/html4/strict.dtd'>").ToHtmlDocument();

            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual("html", docType0.Name);
            Assert.AreEqual("-//W3C//DTD HTML 4.01//EN", docType0.PublicIdentifier);
            Assert.AreEqual("http://www.w3.org/TR/html4/strict.dtd", docType0.SystemIdentifier);

            var dochtml1 = doc.ChildNodes[1];
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1).Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);

            var dochtml1head0 = dochtml1.ChildNodes[0];
            Assert.AreEqual(0, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1head0).Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);

            var dochtml1body1 = dochtml1.ChildNodes[1];
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, ((Element)dochtml1body1).Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
    }
}