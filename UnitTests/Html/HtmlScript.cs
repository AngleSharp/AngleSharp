using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using AngleSharp.Parser.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests (*)
    /// to be more specific: (*)/blob/master/tree-construction/tests16.dat
    /// </summary>
    [TestClass]
    public class HtmlScriptTests
    {
        [TestMethod]
        public void ScriptElementAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithTextAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script>a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    

        }
 
        [TestMethod]
        public void ScriptWithOpenClosingBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingUppercaseLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></S");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</S", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingTwoUppercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></SC");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SC", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingThreeUppercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></SCR");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCR", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingFourUppercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></SCRI");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRI", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingFiveUppercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></SCRIP");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRIP", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingSixUppercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></SCRIPT");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRIPT", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingSevenUppercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></SCRIPT ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingLowercaseLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></s");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingTwoLowercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></sc");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</sc", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingThreeLowercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></scr");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scr", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingFourLowercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></scri");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scri", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingFiveLowercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></scrip");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scrip", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingSixLowercaseLettersAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenClosingSixLowercaseLettersAndSpaceAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script></script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenBogusCommentAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenBogusCommentAndLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenBogusCommentAndDashAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!-");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!-", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenBogusCommentAndDashLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!-a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!-a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenSlashAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--</");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--</", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashClosingScriptUnfinishedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--</script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--</script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashClosingScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--</script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenLetterSAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<s");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    

        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesOpenAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script <");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script <", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesOpenLetterAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script <a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script <a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentScriptTagInsideAndClosingAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndClosingSTagAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </s");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndClosingScriptTagUnfinishedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndClosingScriptMisspelledUnfinishedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </scripta");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </scripta", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndClosingScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndClosedScriptAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenScriptTagAndTrailingSlashWhenClosingScriptAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script/");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script/", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenScriptTagAndOpenBracketAfterSpaceAfterClosingScriptAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script <");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script <", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndOpenLowercaseAAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script <a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script <a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingTagAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script </");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script </", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script </script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script </script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagUnfinishedWithSpacesAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script </script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagUnfinishedTrailingSlashAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script </script/");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosedScriptTagAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script </script </script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script -");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAndLowercaseAAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script -a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAndOpenBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script -<");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesLowercaseAAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --a");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesOpenBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --<");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithCommentThatHostsScriptElementAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script -->");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithCommentThatHostsScriptElementAndOpenBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --><");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --><", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithCommentThatHostsScriptElementAndOpenClosingBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --></");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithCommentThatHostsScriptElementUnfinishedClosingBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --></script");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithCommentThatHostsScriptElementUnfinishedClosingBracketWithSpacesAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --></script ");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosedCommentThatHostsUnfinishedScriptElementAndClosingUnfinishedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --></script/");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosedCommentThatHostsUnfinishedScriptElementClosedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script --></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosedCommentThatHostsScriptPairWithMistakeClosedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script><\/script>--></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual(@"<!--<script><\/script>-->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosedCommentThatHostsScriptPairWithMistakesClosedAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></scr'+'ipt>--></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt>-->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptPairAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></script><script></script></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptPairAndClosingBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></script><script></script>--><!--</script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>--><!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentThatHostsScriptPairAndHasASpaceBeforeClosingBracketAfterDoctype()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></script><script></script>-- ></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>-- >", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithMultipleEscapedCommentsWrongBarelyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></script><script></script>- -></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- ->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithMultipleEscapedCommentsWrongClearlyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></script><script></script>- - ></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- - >", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithMultipleEscapedCommentsWrongWronglyClosed()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></script><script></script>-></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithEscapedOpenedScriptTagFollowedByText()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script>--!></script>X");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script>--!></script>X", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithSpecialCharactersInWronglyEscapedScriptTag()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<scr'+'ipt></script>--></script>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<scr'+'ipt>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }
 
        [TestMethod]
        public void ScriptWithEscapedScriptTagClosedWrongWithSpecialCharacters()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><script><!--<script></scr'+'ipt></script>X");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml1head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt></script>X", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptNoScriptWithClosedCommentThatContainsAnotherClosedNoScriptElement()
        {
            var doc = new Document("<!doctype html><noscript><!--<noscript></noscript>--></noscript>");
            var parser = new HtmlParser(doc);
            doc.Options = new Configuration { IsScripting = true };
            parser.Parse();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1head0noscript0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--<noscript>", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }
 
        [TestMethod]
        public void ScriptNoScriptWithCommentStartAndTextInsideBeforeClosing()
        {
            var doc = new Document("<!doctype html><noscript><!--</noscript>X<noscript>--></noscript>");
            var parser = new HtmlParser(doc);
            doc.Options = new Configuration { IsScripting = true };
            parser.Parse();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1head0noscript0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
    
            var dochtml1body1noscript1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1noscript1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1noscript1.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1body1noscript1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1noscript1.NodeType);
    
            var dochtml1body1noscript1Text0 = dochtml1body1noscript1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1noscript1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1noscript1Text0.TextContent);
        }
 
        [TestMethod]
        public void ScriptNoScriptAfterDoctypeWithIFrameContentAndTextAfter()
        {
            var doc = new Document("<!doctype html><noscript><iframe></noscript>X");
            var parser = new HtmlParser(doc);
            doc.Options = new Configuration { IsScripting = true };
            parser.Parse();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Count);
            Assert.AreEqual("noscript", dochtml1head0noscript0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<iframe>", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
        }
 
        [TestMethod]
        public void ScriptWithinBodyThatisInsideNoframes()
        {
            var doc = DocumentBuilder.Html(@"<!doctype html><noframes><body><script><!--...</script></body></noframes></html>");
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Count);
            Assert.AreEqual("html", dochtml1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Count);
            Assert.AreEqual("head", dochtml1head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noframes0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noframes0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noframes0.Attributes.Count);
            Assert.AreEqual("noframes", dochtml1head0noframes0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1head0noframes0.NodeType);
    
            var dochtml1head0noframes0Text0 = dochtml1head0noframes0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noframes0Text0.NodeType);
            Assert.AreEqual("<body><script><!--...</script></body>", dochtml1head0noframes0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Count);
            Assert.AreEqual("body", dochtml1body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptStandalone()
        {
            var doc = DocumentBuilder.Html(@"<script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithTextLowercaseA()
        {
            var doc = DocumentBuilder.Html(@"<script>a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithTextLt()
        {
            var doc = DocumentBuilder.Html(@"<script><");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithTextLtSlash()
        {
            var doc = DocumentBuilder.Html(@"<script></");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagSpace()
        {
            var doc = DocumentBuilder.Html(@"<script></SCRIPT ");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagLowercaseS()
        {
            var doc = DocumentBuilder.Html(@"<script></s");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</s", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagLowercaseSC()
        {
            var doc = DocumentBuilder.Html(@"<script></sc");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</sc", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagLowercaseSCR()
        {
            var doc = DocumentBuilder.Html(@"<script></scr");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scr", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagLowercaseSCRI()
        {
            var doc = DocumentBuilder.Html(@"<script></scri");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scri", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagLowercaseSCRIP()
        {
            var doc = DocumentBuilder.Html(@"<script></scrip");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scrip", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptTagLowercaseSCRIPT()
        {
            var doc = DocumentBuilder.Html(@"<script></script");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithClosingScriptSpaceInsteadOfGt()
        {
            var doc = DocumentBuilder.Html(@"<script></script ");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEm()
        {
            var doc = DocumentBuilder.Html(@"<script><!");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmLowercaseA()
        {
            var doc = DocumentBuilder.Html(@"<script><!a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDash()
        {
            var doc = DocumentBuilder.Html(@"<script><!-");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!-", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashLowercaseA()
        {
            var doc = DocumentBuilder.Html(@"<script><!-a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!-a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashDash()
        {
            var doc = DocumentBuilder.Html(@"<script><!--");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashDashLowercaseA()
        {
            var doc = DocumentBuilder.Html(@"<script><!--a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashDashLt()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashDashLtLowercaseA()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashDashLtSlash()
        {
            var doc = DocumentBuilder.Html(@"<script><!--</");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--</", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptLtEmDashDashLtSlashLowercaseSCRIPT()
        {
            var doc = DocumentBuilder.Html(@"<script><!--</script");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--</script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithStartCommentScriptInside()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script </s");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </s", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithStartCommentAndThreeEscapes()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script </script </script ");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithStartCommentAndEffectivelyClosed()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script </script </script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpeningCommentAndDashLowercaseA()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script -a");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptThatTriesToEscapeAnotherScriptTag()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script --");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptThatContainsAnotherScriptTagInsideCommentAndIsNotFinished()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script --><");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --><", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptThatContainsAnotherScriptTagInsideAComment()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script --></script");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithCommentThatTriesToOpenCloseButMisspells()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script><\/script>--></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual(@"<!--<script><\/script>-->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithScriptsInCommentCommentBeforeClosing()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script></script><script></script>--><!--</script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>--><!--", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithScriptsInCommentSpaceBeforeBracket()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script></script><script></script>-- ></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>-- >", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithScriptsInCommentSpaceBetweenDash()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script></script><script></script>- -></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- ->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithScriptsInCommentDashMissing()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script></script><script></script>-></script>");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithValidCommentAndTextAfter()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script>--!></script>X");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script>--!></script>X", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [TestMethod]
        public void ScriptWithOpenCommentAndClosingMisspelledTextAfter()
        {
            var doc = DocumentBuilder.Html(@"<script><!--<script></scr'+'ipt></script>X");
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Count);
            Assert.AreEqual("html", dochtml0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Count);
            Assert.AreEqual("head", dochtml0head0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Count);
            Assert.AreEqual("script", dochtml0head0script0.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt></script>X", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Count);
            Assert.AreEqual("body", dochtml0body1.NodeName);
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
    }
}
