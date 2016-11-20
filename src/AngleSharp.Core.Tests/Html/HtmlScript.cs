namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Core.Tests.Mocks;
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using AngleSharp.Text;
    using NUnit.Framework;

    /// <summary>
    /// Tests from https://github.com/html5lib/html5lib-tests:
    /// tree-construction/tests16.dat
    /// </summary>
    [TestFixture]
    public class HtmlScriptTests
    {
        [Test]
        public void ScriptElementAfterDoctype()
        {
            var doc = (@"<!doctype html><script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextAfterDoctype()
        {
            var doc = (@"<!doctype html><script>a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    

        }
 
        [Test]
        public void ScriptWithOpenClosingBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script></").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingUppercaseLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script></S").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</S", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingTwoUppercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></SC").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SC", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingThreeUppercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></SCR").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCR", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFourUppercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></SCRI").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRI", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFiveUppercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></SCRIP").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRIP", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSixUppercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></SCRIPT").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</SCRIPT", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSevenUppercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></SCRIPT ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingLowercaseLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script></s").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingTwoLowercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></sc").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</sc", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingThreeLowercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></scr").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scr", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFourLowercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></scri").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scri", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingFiveLowercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></scrip").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</scrip", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSixLowercaseLettersAfterDoctype()
        {
            var doc = (@"<!doctype html><script></script").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("</script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenClosingSixLowercaseLettersAndSpaceAfterDoctype()
        {
            var doc = (@"<!doctype html><script></script ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAndLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAndDashAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!-").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!-", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenBogusCommentAndDashLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!-a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!-a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenSlashAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--</").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--</", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashClosingScriptUnfinishedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--</script").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--</script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashClosingScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--</script ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenLetterSAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<s").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    

        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesOpenAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script <").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script <", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndDashDashOpenScriptUnfinishedSpacesOpenLetterAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script <a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script <a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentScriptTagInsideAndClosingAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingSTagAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </s").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </s", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingScriptTagUnfinishedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingScriptMisspelledUnfinishedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </scripta").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </scripta", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingScriptUnfinishedSpacesAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosedScriptAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenScriptTagAndTrailingSlashWhenClosingScriptAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script/").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script/", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenScriptTagAndOpenBracketAfterSpaceAfterClosingScriptAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script <").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script <", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndOpenLowercaseAAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script <a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script <a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingTagAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script </").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script </", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script </script").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script </script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagUnfinishedWithSpacesAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script </script ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosingScriptTagUnfinishedTrailingSlashAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script </script/").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagUnfinishedSpaceAndClosedScriptTagAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script </script </script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script -").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAndLowercaseAAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script -a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndOneFinalDashAndOpenBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script -<").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesLowercaseAAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --a").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --a", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptElementAndTwoFinalDashesOpenBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --<").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --<", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script -->").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementAndOpenBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --><").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --><", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementAndOpenClosingBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --></").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementUnfinishedClosingBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --></script").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></script", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatHostsScriptElementUnfinishedClosingBracketWithSpacesAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --></script ").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsUnfinishedScriptElementAndClosingUnfinishedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --></script/").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsUnfinishedScriptElementClosedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script --></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsScriptPairWithMistakeClosedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script><\/script>--></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual(@"<!--<script><\/script>-->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosedCommentThatHostsScriptPairWithMistakesClosedAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script></scr'+'ipt>--></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt>-->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptPairAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script></script><script></script></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptPairAndClosingBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script></script><script></script>--><!--</script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>--><!--", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentThatHostsScriptPairAndHasASpaceBeforeClosingBracketAfterDoctype()
        {
            var doc = (@"<!doctype html><script><!--<script></script><script></script>-- ></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>-- >", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithMultipleEscapedCommentsWrongBarelyClosed()
        {
            var doc = (@"<!doctype html><script><!--<script></script><script></script>- -></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- ->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithMultipleEscapedCommentsWrongClearlyClosed()
        {
            var doc = (@"<!doctype html><script><!--<script></script><script></script>- - ></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- - >", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithMultipleEscapedCommentsWrongWronglyClosed()
        {
            var doc = (@"<!doctype html><script><!--<script></script><script></script>-></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>->", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithEscapedOpenedScriptTagFollowedByText()
        {
            var doc = (@"<!doctype html><script><!--<script>--!></script>X").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script>--!></script>X", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptWithSpecialCharactersInWronglyEscapedScriptTag()
        {
            var doc = (@"<!doctype html><script><!--<scr'+'ipt></script>--></script>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<scr'+'ipt>", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("-->", dochtml1body1Text0.TextContent);
        }
 
        [Test]
        public void ScriptWithEscapedScriptTagClosedWrongWithSpecialCharacters()
        {
            var doc = (@"<!doctype html><script><!--<script></scr'+'ipt></script>X").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0script0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml1head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0script0.NodeType);
    
            var dochtml1head0script0Text0 = dochtml1head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt></script>X", dochtml1head0script0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptNoScriptWithClosedCommentThatContainsAnotherClosedNoScriptElement()
        {
            var source = "<!doctype html><noscript><!--<noscript></noscript>--></noscript>";
            var config = Configuration.Default.WithScripting();
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var doc = parser.ParseDocument(source);
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Length);
            Assert.AreEqual("noscript", dochtml1head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--<noscript>", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
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
            var config = Configuration.Default.WithScripting();
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var doc = parser.ParseDocument(source);
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Length);
            Assert.AreEqual("noscript", dochtml1head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
    
            var dochtml1body1noscript1 = dochtml1body1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1noscript1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1noscript1.Attributes.Length);
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
            var config = Configuration.Default.WithScripting();
            var context = BrowsingContext.New(config);
            var parser = context.GetService<IHtmlParser>();
            var doc = parser.ParseDocument(source);
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noscript0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noscript0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noscript0.Attributes.Length);
            Assert.AreEqual("noscript", dochtml1head0noscript0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noscript0.NodeType);
    
            var dochtml1head0noscript0Text0 = dochtml1head0noscript0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noscript0Text0.NodeType);
            Assert.AreEqual("<iframe>", dochtml1head0noscript0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(1, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
    
            var dochtml1body1Text0 = dochtml1body1.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1body1Text0.NodeType);
            Assert.AreEqual("X", dochtml1body1Text0.TextContent);
        }
 
        [Test]
        public void ScriptWithinBodyThatisInsideNoframes()
        {
            var doc = (@"<!doctype html><noframes><body><script><!--...</script></body></noframes></html>").ToHtmlDocument();
      
            var docType0 = doc.ChildNodes[0] as DocumentType;
            Assert.IsNotNull(docType0);
            Assert.AreEqual(NodeType.DocumentType, docType0.NodeType);
            Assert.AreEqual(@"html", docType0.Name);

            var dochtml1 = doc.ChildNodes[1] as Element;
            Assert.AreEqual(2, dochtml1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1.Attributes.Length);
            Assert.AreEqual("html", dochtml1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1.NodeType);
    
            var dochtml1head0 = dochtml1.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0.Attributes.Length);
            Assert.AreEqual("head", dochtml1head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0.NodeType);
    
            var dochtml1head0noframes0 = dochtml1head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml1head0noframes0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1head0noframes0.Attributes.Length);
            Assert.AreEqual("noframes", dochtml1head0noframes0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1head0noframes0.NodeType);
    
            var dochtml1head0noframes0Text0 = dochtml1head0noframes0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml1head0noframes0Text0.NodeType);
            Assert.AreEqual("<body><script><!--...</script></body>", dochtml1head0noframes0Text0.TextContent);
    
            var dochtml1body1 = dochtml1.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml1body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml1body1.Attributes.Length);
            Assert.AreEqual("body", dochtml1body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml1body1.NodeType);
        }
 
        [Test]
        public void ScriptStandalone()
        {
            var doc = (@"<script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextLowercaseA()
        {
            var doc = (@"<script>a").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextLt()
        {
            var doc = (@"<script><").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithTextLtSlash()
        {
            var doc = (@"<script></").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagSpace()
        {
            var doc = (@"<script></SCRIPT ").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseS()
        {
            var doc = (@"<script></s").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</s", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSC()
        {
            var doc = (@"<script></sc").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</sc", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCR()
        {
            var doc = (@"<script></scr").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scr", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCRI()
        {
            var doc = (@"<script></scri").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scri", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCRIP()
        {
            var doc = (@"<script></scrip").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</scrip", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptTagLowercaseSCRIPT()
        {
            var doc = (@"<script></script").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("</script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithClosingScriptSpaceInsteadOfGt()
        {
            var doc = (@"<script></script ").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(0, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEm()
        {
            var doc = (@"<script><!").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmLowercaseA()
        {
            var doc = (@"<script><!a").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDash()
        {
            var doc = (@"<script><!-").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!-", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashLowercaseA()
        {
            var doc = (@"<script><!-a").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!-a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDash()
        {
            var doc = (@"<script><!--").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLowercaseA()
        {
            var doc = (@"<script><!--a").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLt()
        {
            var doc = (@"<script><!--<").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLtLowercaseA()
        {
            var doc = (@"<script><!--<a").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLtSlash()
        {
            var doc = (@"<script><!--</").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--</", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptLtEmDashDashLtSlashLowercaseSCRIPT()
        {
            var doc = (@"<script><!--</script").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--</script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithStartCommentScriptInside()
        {
            var doc = (@"<script><!--<script </s").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </s", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithStartCommentAndThreeEscapes()
        {
            var doc = (@"<script><!--<script </script </script ").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithStartCommentAndEffectivelyClosed()
        {
            var doc = (@"<script><!--<script </script </script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script </script ", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpeningCommentAndDashLowercaseA()
        {
            var doc = (@"<script><!--<script -a").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script -a", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptThatTriesToEscapeAnotherScriptTag()
        {
            var doc = (@"<script><!--<script --").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptThatContainsAnotherScriptTagInsideCommentAndIsNotFinished()
        {
            var doc = (@"<script><!--<script --><").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --><", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptThatContainsAnotherScriptTagInsideAComment()
        {
            var doc = (@"<script><!--<script --></script").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script --></script", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithCommentThatTriesToOpenCloseButMisspells()
        {
            var doc = (@"<script><!--<script><\/script>--></script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual(@"<!--<script><\/script>-->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentCommentBeforeClosing()
        {
            var doc = (@"<script><!--<script></script><script></script>--><!--</script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>--><!--", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentSpaceBeforeBracket()
        {
            var doc = (@"<script><!--<script></script><script></script>-- ></script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>-- >", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentSpaceBetweenDash()
        {
            var doc = (@"<script><!--<script></script><script></script>- -></script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>- ->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithScriptsInCommentDashMissing()
        {
            var doc = (@"<script><!--<script></script><script></script>-></script>").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></script><script></script>->", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithValidCommentAndTextAfter()
        {
            var doc = (@"<script><!--<script>--!></script>X").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script>--!></script>X", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }
 
        [Test]
        public void ScriptWithOpenCommentAndClosingMisspelledTextAfter()
        {
            var doc = (@"<script><!--<script></scr'+'ipt></script>X").ToHtmlDocument();
      
            var dochtml0 = doc.ChildNodes[0] as Element;
            Assert.AreEqual(2, dochtml0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0.Attributes.Length);
            Assert.AreEqual("html", dochtml0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0.NodeType);
    
            var dochtml0head0 = dochtml0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0.Attributes.Length);
            Assert.AreEqual("head", dochtml0head0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0.NodeType);
    
            var dochtml0head0script0 = dochtml0head0.ChildNodes[0] as Element;
            Assert.AreEqual(1, dochtml0head0script0.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0head0script0.Attributes.Length);
            Assert.AreEqual("script", dochtml0head0script0.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0head0script0.NodeType);
    
            var dochtml0head0script0Text0 = dochtml0head0script0.ChildNodes[0];
            Assert.AreEqual(NodeType.Text, dochtml0head0script0Text0.NodeType);
            Assert.AreEqual("<!--<script></scr'+'ipt></script>X", dochtml0head0script0Text0.TextContent);
    
            var dochtml0body1 = dochtml0.ChildNodes[1] as Element;
            Assert.AreEqual(0, dochtml0body1.ChildNodes.Length);
            Assert.AreEqual(0, dochtml0body1.Attributes.Length);
            Assert.AreEqual("body", dochtml0body1.GetTagName());
            Assert.AreEqual(NodeType.Element, dochtml0body1.NodeType);
        }

        [Test]
        public void LargeInlineScriptShouldNotExceedStack()
        {
            var bytes = Assets.longscript;
            var content = TextEncoding.Utf8.GetString(bytes);
            var doc = (content).ToHtmlDocument();
            Assert.IsNotNull(doc);
        }
    }
}
