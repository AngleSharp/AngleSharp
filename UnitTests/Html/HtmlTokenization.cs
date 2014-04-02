using AngleSharp.Parser;
using AngleSharp.Parser.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text;

namespace UnitTests
{
    [TestClass]
    public class HtmlTokenizationTests
    {
        [TestMethod]
        public void TokenizationFinalEOF()
        {
            var s = new SourceManager("");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.EOF, token.Type);
        }
        
        [TestMethod]
        public void TokenizationStartTagDetection()
        {
            var s = new SourceManager("<p>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.StartTag, token.Type);
            Assert.AreEqual("p", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationBogusCommentEmpty()
        {
            var s = new SourceManager("<!>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual(String.Empty, ((HtmlCommentToken)token).Data);
        }

        [TestMethod]
        public void TokenizationBogusCommentQuestionMark()
        {
            var s = new SourceManager("<?>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual("?", ((HtmlCommentToken)token).Data);
        }

        [TestMethod]
        public void TokenizationBogusCommentClosingTag()
        {
            var s = new SourceManager("</ >");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual(" ", ((HtmlCommentToken)token).Data);
        }
        
        [TestMethod]
        public void TokenizationTagNameDetection()
        {
            var s = new SourceManager("<span>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("span", ((HtmlTagToken)token).Name);
        }
        
        [TestMethod]
        public void TokenizationTagSelfClosingDetected()
        {
            var s = new SourceManager("<img />");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(true, ((HtmlTagToken)token).IsSelfClosing);
        }
        
        [TestMethod]
        public void TokenizationAttributesDetected()
        {
            var s = new SourceManager("<a target='_blank' href='http://whatever' title='ho'>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(3, ((HtmlTagToken)token).Attributes.Count);
        }
        
        [TestMethod]
        public void TokenizationAttributeNameDetection()
        {
            var s = new SourceManager("<input required>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("required", ((HtmlTagToken)token).Attributes[0].Key);
        }

        [TestMethod]
        public void TokenizationTagMixedCaseHandling()
        {
            var s = new SourceManager("<InpUT>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("input", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationTagSpacesBehind()
        {
            var s = new SourceManager("<i   >");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("i", ((HtmlTagToken)token).Name);
        }
        
        [TestMethod]
        public void TokenizationCharacterReferenceNotin()
        {
            var str = string.Empty;
            var src = "I'm &notin; I tell you";
            var s = new SourceManager(src);
            var t = new HtmlTokenizer(s);
            HtmlToken token;

            do
            {
                token = t.Get();

                if (token.Type == HtmlTokenType.Character)
                    str += ((HtmlCharacterToken)token).Data;
            }
            while (token != HtmlToken.EOF);

            Assert.AreEqual("I'm ∉ I tell you", str);
        }
        
        [TestMethod]
        public void TokenizationCharacterReferenceNotIt()
        {
            var str = string.Empty;
            var src = "I'm &notit; I tell you";
            var s = new SourceManager(src);
            var t = new HtmlTokenizer(s);
            HtmlToken token;

            do
            {
                token = t.Get();

                if (token.Type == HtmlTokenType.Character)
                    str += ((HtmlCharacterToken)token).Data;
            }
            while (token != HtmlToken.EOF);

            Assert.AreEqual("I'm ¬it; I tell you", str);
        }
        
        [TestMethod]
        public void TokenizationDoctypeDetected()
        {
            var s = new SourceManager("<!doctype html>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.DOCTYPE, token.Type);
        }
        
        [TestMethod]
        public void TokenizationCommentDetected()
        {
            var s = new SourceManager("<!-- hi my friend -->");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
        }

        [TestMethod]
        public void TokenizationCDataDetected()
        {
            var s = new SourceManager("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new HtmlTokenizer(s);
            t.AcceptsCharacterData = true;
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
        }

        [TestMethod]
        public void TokenizationCDataCorrectCharacters()
        {
            StringBuilder sb = new StringBuilder();
            var s = new SourceManager("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new HtmlTokenizer(s);
            t.AcceptsCharacterData = true;
            HtmlToken token;

            do
            {
                token = t.Get();

                if (token.Type == HtmlTokenType.Character)
                    sb.Append(((HtmlCharacterToken)token).Data);
            }
            while (token != HtmlToken.EOF);

            Assert.AreEqual("hi mum how <!-- are you doing />", sb.ToString());
        }

        [TestMethod]
        public void TokenizationUnusualDoctype()
        {
            var s = new SourceManager("<!DOCTYPE root_element SYSTEM \"DTD_location\">");
            var t = new HtmlTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(HtmlTokenType.DOCTYPE, e.Type);
            var d = (HtmlDoctypeToken)e;
            Assert.IsFalse(d.IsNameMissing);
            Assert.AreEqual("root_element", d.Name);
            Assert.IsFalse(d.IsSystemIdentifierMissing);
            Assert.AreEqual("DTD_location", d.SystemIdentifier);
        }
    }
}
