using AngleSharp.Parser.Html;
using NUnit.Framework;
using System;
using System.Text;

namespace AngleSharp.Core.Tests
{
    [TestFixture]
    public class HtmlTokenizationTests
    {
        [Test]
        public void TokenizationFinalEOF()
        {
            var s = new TextSource("");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.EndOfFile, token.Type);
        }

        [Test]
        public void TokenizationLongerCharacterReference()
        {
            var content = "&abcdefghijklmnopqrstvwxyzABCDEFGHIJKLMNOPQRSTV;";
            var s = new TextSource(content);
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
            Assert.AreEqual(content, token.Data);
        }
        
        [Test]
        public void TokenizationStartTagDetection()
        {
            var s = new TextSource("<p>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.StartTag, token.Type);
            Assert.AreEqual("p", ((HtmlTagToken)token).Name);
        }

        [Test]
        public void TokenizationBogusCommentEmpty()
        {
            var s = new TextSource("<!>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual(String.Empty, token.Data);
        }

        [Test]
        public void TokenizationBogusCommentQuestionMark()
        {
            var s = new TextSource("<?>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual("?", token.Data);
        }

        [Test]
        public void TokenizationBogusCommentClosingTag()
        {
            var s = new TextSource("</ >");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual(" ", token.Data);
        }
        
        [Test]
        public void TokenizationTagNameDetection()
        {
            var s = new TextSource("<span>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual("span", ((HtmlTagToken)token).Name);
        }
        
        [Test]
        public void TokenizationTagSelfClosingDetected()
        {
            var s = new TextSource("<img />");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(true, ((HtmlTagToken)token).IsSelfClosing);
        }
        
        [Test]
        public void TokenizationAttributesDetected()
        {
            var s = new TextSource("<a target='_blank' href='http://whatever' title='ho'>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(3, ((HtmlTagToken)token).Attributes.Count);
        }
        
        [Test]
        public void TokenizationAttributeNameDetection()
        {
            var s = new TextSource("<input required>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual("required", ((HtmlTagToken)token).Attributes[0].Key);
        }

        [Test]
        public void TokenizationTagMixedCaseHandling()
        {
            var s = new TextSource("<InpUT>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual("input", ((HtmlTagToken)token).Name);
        }

        [Test]
        public void TokenizationTagSpacesBehind()
        {
            var s = new TextSource("<i   >");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual("i", ((HtmlTagToken)token).Name);
        }
        
        [Test]
        public void TokenizationCharacterReferenceNotin()
        {
            var str = string.Empty;
            var src = "I'm &notin; I tell you";
            var s = new TextSource(src);
            var t = new HtmlTokenizer(s, null);
            var token = default(HtmlToken);

            do
            {
                token = t.Get();

                if (token.Type == HtmlTokenType.Character)
                    str += token.Data;
            }
            while (token.Type != HtmlTokenType.EndOfFile);

            Assert.AreEqual("I'm ∉ I tell you", str);
        }
        
        [Test]
        public void TokenizationCharacterReferenceNotIt()
        {
            var str = string.Empty;
            var src = "I'm &notit; I tell you";
            var s = new TextSource(src);
            var t = new HtmlTokenizer(s, null);
            var token = default(HtmlToken);

            do
            {
                token = t.Get();

                if (token.Type == HtmlTokenType.Character)
                    str += token.Data;
            }
            while (token.Type != HtmlTokenType.EndOfFile);

            Assert.AreEqual("I'm ¬it; I tell you", str);
        }
        
        [Test]
        public void TokenizationDoctypeDetected()
        {
            var s = new TextSource("<!doctype html>");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Doctype, token.Type);
        }
        
        [Test]
        public void TokenizationCommentDetected()
        {
            var s = new TextSource("<!-- hi my friend -->");
            var t = new HtmlTokenizer(s, null);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
        }

        [Test]
        public void TokenizationCDataDetected()
        {
            var s = new TextSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new HtmlTokenizer(s, null);
            t.IsAcceptingCharacterData = true;
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
        }

        [Test]
        public void TokenizationCDataCorrectCharacters()
        {
            StringBuilder sb = new StringBuilder();
            var s = new TextSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new HtmlTokenizer(s, null);
            var token = default(HtmlToken);
            t.IsAcceptingCharacterData = true;

            do
            {
                token = t.Get();

                if (token.Type == HtmlTokenType.Character)
                    sb.Append(token.Data);
            }
            while (token.Type != HtmlTokenType.EndOfFile);

            Assert.AreEqual("hi mum how <!-- are you doing />", sb.ToString());
        }

        [Test]
        public void TokenizationUnusualDoctype()
        {
            var s = new TextSource("<!DOCTYPE root_element SYSTEM \"DTD_location\">");
            var t = new HtmlTokenizer(s, null);
            var e = t.Get();
            Assert.AreEqual(HtmlTokenType.Doctype, e.Type);
            var d = (HtmlDoctypeToken)e;
            Assert.IsNotNull(d.Name);
            Assert.AreEqual("root_element", d.Name);
            Assert.IsFalse(d.IsSystemIdentifierMissing);
            Assert.AreEqual("DTD_location", d.SystemIdentifier);
        }
    }
}
