using System;
using AngleSharp.Core.Tests.Mocks;
using AngleSharp.Dom;
using AngleSharp.Parser.Html;
using NUnit.Framework;

namespace AngleSharp.Core.Tests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests16.dat
    /// </summary>
    [TestFixture]
    public class HtmlScriptTests
    {
        static IDocument Html(String code)
        {
            return code.ToHtmlDocument();
        }

        [Test]
        public void ScriptElementAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script>a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    

        }
 
        [Test]
        public void ScriptWithOpenClosingBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingUppercaseLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></S");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</S", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingTwoUppercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></SC");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SC", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingThreeUppercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></SCR");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCR", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFourUppercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></SCRI");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRI", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFiveUppercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></SCRIP");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRIP", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSixUppercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></SCRIPT");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRIPT", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSevenUppercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></SCRIPT ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingLowercaseLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></s");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingTwoLowercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></sc");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</sc", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingThreeLowercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></scr");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scr", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFourLowercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></scri");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scri", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFiveLowercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></scrip");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scrip", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSixLowercaseLettersAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSixLowercaseLettersAndSpaceAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script></script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAndLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAndDashAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!-");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!-", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAndDashLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!-a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!-a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenSlashAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--</");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--</", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashClosingScriptUnfinishedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--</script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--</script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashClosingScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--</script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenLetterSAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<s");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    

        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesOpenAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script <");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script <", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesOpenLetterAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script <a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script <a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentScriptTagInsideAndClosingAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingSTagAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </s");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingScriptTagUnfinishedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingScriptMisspelledUnfinishedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </scripta");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </scripta", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosedScriptAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenScriptTagAndTrailingSlashWhenClosingScriptAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script/");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script/", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenScriptTagAndOpenBracketAfterSpaceAfterClosingScriptAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script <");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script <", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndOpenLowercaseAAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script <a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script <a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingTagAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script </");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script </", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script </script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script </script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagUnfinishedWithSpacesAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script </script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagUnfinishedTrailingSlashAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script </script/");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosedScriptTagAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script </script </script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script -");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAndLowercaseAAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script -a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAndOpenBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script -<");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesLowercaseAAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesOpenBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --<");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script -->");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementAndOpenBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --><");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --><", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementAndOpenClosingBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --></");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementUnfinishedClosingBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --></script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementUnfinishedClosingBracketWithSpacesAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --></script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsUnfinishedScriptElementAndClosingUnfinishedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --></script/");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsUnfinishedScriptElementClosedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script --></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsScriptPairWithMistakeClosedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script><\/script>--></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual(@"<!--<script><\/script>-->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsScriptPairWithMistakesClosedAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script></scr'+'ipt>--></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt>-->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptPairAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script></script><script></script></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptPairAndClosingBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script></script><script></script>--><!--</script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>--><!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptPairAndHasASpaceBeforeClosingBracketAfterDoctype()
        {
            var doc = Html(@"<!doctype html><script><!--<script></script><script></script>-- ></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>-- >", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithMultipleEscapedCommentsWrongBarelyClosed()
        {
            var doc = Html(@"<!doctype html><script><!--<script></script><script></script>- -></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- ->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithMultipleEscapedCommentsWrongClearlyClosed()
        {
            var doc = Html(@"<!doctype html><script><!--<script></script><script></script>- - ></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- - >", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithMultipleEscapedCommentsWrongWronglyClosed()
        {
            var doc = Html(@"<!doctype html><script><!--<script></script><script></script>-></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithEscapedOpenedScriptTagFollowedByText()
        {
            var doc = Html(@"<!doctype html><script><!--<script>--!></script>X");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script>--!></script>X", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithSpecialCharactersInWronglyEscapedScriptTag()
        {
            var doc = Html(@"<!doctype html><script><!--<scr'+'ipt></script>--></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<scr'+'ipt>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }
 
        [Test]
        public void ScriptWithEscapedScriptTagClosedWrongWithSpecialCharacters()
        {
            var doc = Html(@"<!doctype html><script><!--<script></scr'+'ipt></script>X");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt></script>X", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptNoScriptWithClosedCommentThatContainsAnotherClosedNoScriptElement()
        {
            var source = "<!doctype html><noscript><!--<noscript></noscript>--></noscript>";
            var config = Configuration.Default.With(new EnableScripting());
            var parser = new HtmlParser(source, config); 
            var doc = parser.Parse();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--<noscript>", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }
 
        [Test]
        public void ScriptNoScriptWithCommentStartAndTextInsideBeforeClosing()
        {
            var source = "<!doctype html><noscript><!--</noscript>X<noscript>--></noscript>";
            var config = Configuration.Default.With(new EnableScripting());
            var parser = new HtmlParser(source, config);
            var doc = parser.Parse();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
    
            var dochtml1body1noscript1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1noscript1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1noscript1.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1body1noscript1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1noscript1.NodeType);
    
            var dochtml1body1noscript1Text0 = dochtml1body1noscript1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1noscript1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1noscript1Text0.TextContent);
        }
 
        [Test]
        public void ScriptNoScriptAfterDoctypeWithIFrameContentAndTextAfter()
        {
            var source = "<!doctype html><noscript><iframe></noscript>X";
            var config = Configuration.Default.With(new EnableScripting());
            var parser = new HtmlParser(source, config);
            var doc = parser.Parse();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<iframe>", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
        }
 
        [Test]
        public void ScriptWithinBodyThatisInsideNoframes()
        {
            var doc = Html(@"<!doctype html><noframes><body><script><!--...</script></body></noframes></html>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noframes0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noframes0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noframes0.Attributes.Count);
            Assert.AreEqual("noframes", dochtml1head0noframes0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noframes0.NodeType);
    
            var dochtml1head0noframes0Text0 = dochtml1head0noframes0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noframes0Text0.NodeType);
            Assert.AreEqual("<body><script><!--...</script></body>", dochtml1head0noframes0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptStandalone()
        {
            var doc = Html(@"<script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextLowercaseA()
        {
            var doc = Html(@"<script>a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextLt()
        {
            var doc = Html(@"<script><");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextLtSlash()
        {
            var doc = Html(@"<script></");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagSpace()
        {
            var doc = Html(@"<script></SCRIPT ");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseS()
        {
            var doc = Html(@"<script></s");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</s", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSC()
        {
            var doc = Html(@"<script></sc");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</sc", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCR()
        {
            var doc = Html(@"<script></scr");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scr", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCRI()
        {
            var doc = Html(@"<script></scri");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scri", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCRIP()
        {
            var doc = Html(@"<script></scrip");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scrip", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCRIPT()
        {
            var doc = Html(@"<script></script");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptSpaceInsteadOfGt()
        {
            var doc = Html(@"<script></script ");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEm()
        {
            var doc = Html(@"<script><!");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmLowercaseA()
        {
            var doc = Html(@"<script><!a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDash()
        {
            var doc = Html(@"<script><!-");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!-", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashLowercaseA()
        {
            var doc = Html(@"<script><!-a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!-a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDash()
        {
            var doc = Html(@"<script><!--");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLowercaseA()
        {
            var doc = Html(@"<script><!--a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLt()
        {
            var doc = Html(@"<script><!--<");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLtLowercaseA()
        {
            var doc = Html(@"<script><!--<a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLtSlash()
        {
            var doc = Html(@"<script><!--</");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--</", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLtSlashLowercaseSCRIPT()
        {
            var doc = Html(@"<script><!--</script");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--</script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithStartCommentScriptInside()
        {
            var doc = Html(@"<script><!--<script </s");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </s", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithStartCommentAndThreeEscapes()
        {
            var doc = Html(@"<script><!--<script </script </script ");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithStartCommentAndEffectivelyClosed()
        {
            var doc = Html(@"<script><!--<script </script </script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpeningCommentAndDashLowercaseA()
        {
            var doc = Html(@"<script><!--<script -a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptThatTriesToEscapeAnotherScriptTag()
        {
            var doc = Html(@"<script><!--<script --");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptThatContainsAnotherScriptTagInsideCommentAndIsNotFinished()
        {
            var doc = Html(@"<script><!--<script --><");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --><", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptThatContainsAnotherScriptTagInsideAComment()
        {
            var doc = Html(@"<script><!--<script --></script");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatTriesToOpenCloseButMisspells()
        {
            var doc = Html(@"<script><!--<script><\/script>--></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual(@"<!--<script><\/script>-->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentCommentBeforeClosing()
        {
            var doc = Html(@"<script><!--<script></script><script></script>--><!--</script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>--><!--", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentSpaceBeforeBracket()
        {
            var doc = Html(@"<script><!--<script></script><script></script>-- ></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>-- >", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentSpaceBetweenDash()
        {
            var doc = Html(@"<script><!--<script></script><script></script>- -></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- ->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentDashMissing()
        {
            var doc = Html(@"<script><!--<script></script><script></script>-></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithValidCommentAndTextAfter()
        {
            var doc = Html(@"<script><!--<script>--!></script>X");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script>--!></script>X", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingMisspelledTextAfter()
        {
            var doc = Html(@"<script><!--<script></scr'+'ipt></script>X");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt></script>X", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
    }
}
