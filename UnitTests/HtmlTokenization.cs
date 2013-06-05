using AngleSharp;
using AngleSharp.Html;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class HtmlTokenization
    {
        [TestMethod]
        public void TokenizationFinalEOF()
        {
            HtmlToken token = null;
            var s = new HtmlSource("");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                token = e.Token;
            };
            t.Start();
            Assert.AreEqual(HtmlTokenType.EOF, token.Type);
        }

        [TestMethod]
        public void TokenizationStartTagDetection()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<p>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if(token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual(HtmlTokenType.StartTag, token.Type);
        }

        [TestMethod]
        public void TokenizationTagNameDetection()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<span>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual("span", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationTagSelfClosingDetected()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<img />");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual(true, ((HtmlTagToken)token).IsSelfClosing);
        }

        [TestMethod]
        public void TokenizationAttributesDetected()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<a target='_blank' href='http://whatever' title='ho'>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual(3, ((HtmlTagToken)token).Attributes.Count);
        }

        [TestMethod]
        public void TokenizationAttributeNameDetection()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<input required>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual("required", ((HtmlTagToken)token).Attributes[0].Key);
        }

        [TestMethod]
        public void TokenizationTagMixedCaseHandling()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<InpUT>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual("input", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationTagSpacesBehind()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<i   >");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual("i", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationCharacterReferenceNotin()
        {
            var str = string.Empty;
            var src = "I'm &notin; I tell you";
            var s = new HtmlSource(src);
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (e.Token.Type == HtmlTokenType.Character)
                    str += ((HtmlCharacterToken)e.Token).Data;
            };
            t.Start();
            Assert.AreEqual("I'm ∉ I tell you", str);
        }

        [TestMethod]
        public void TokenizationCharacterReferenceNotIt()
        {
            var str = string.Empty;
            var src = "I'm &notit; I tell you";
            var s = new HtmlSource(src);
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if(e.Token.Type == HtmlTokenType.Character)
                    str += ((HtmlCharacterToken)e.Token).Data;
            };
            t.Start();
            Assert.AreEqual("I'm ¬it; I tell you", str);
        }

        [TestMethod]
        public void TokenizationDoctypeDetected()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<!doctype html>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual(HtmlTokenType.DOCTYPE, token.Type);
        }

        [TestMethod]
        public void TokenizationCommentDetected()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<!-- hi my friend -->");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
        }

        [TestMethod]
        public void TokenizationCDataDetected()
        {
            HtmlToken token = null;
            var s = new HtmlSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if (token == null)
                    token = e.Token;
            };
            t.Start();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
        }

        [TestMethod]
        public void TokenizationCDataCorrectCharacters()
        {
            StringBuilder sb = new StringBuilder();
            var s = new HtmlSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new Tokenization(s);
            t.TokenEmitted += (sender, e) =>
            {
                if(e.Token is HtmlCharacterToken)
                    sb.Append(((HtmlCharacterToken)e.Token).Data);
            };
            t.Start();
            Assert.AreEqual("hi mum how <!-- are you doing />", sb.ToString());
        }
    }
}
