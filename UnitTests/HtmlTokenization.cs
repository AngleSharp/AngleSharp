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
            var s = new HtmlSource("");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.EOF, token.Type);
        }

        [TestMethod]
        public void TokenizationStartTagDetection()
        {
            var s = new HtmlSource("<p>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.StartTag, token.Type);
        }

        [TestMethod]
        public void TokenizationTagNameDetection()
        {
            var s = new HtmlSource("<span>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("span", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationTagSelfClosingDetected()
        {
            var s = new HtmlSource("<img />");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(true, ((HtmlTagToken)token).IsSelfClosing);
        }

        [TestMethod]
        public void TokenizationAttributesDetected()
        {
            var s = new HtmlSource("<a target='_blank' href='http://whatever' title='ho'>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(3, ((HtmlTagToken)token).Attributes.Count);
        }

        [TestMethod]
        public void TokenizationAttributeNameDetection()
        {
            var s = new HtmlSource("<input required>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("required", ((HtmlTagToken)token).Attributes[0].Key);
        }

        [TestMethod]
        public void TokenizationTagMixedCaseHandling()
        {
            var s = new HtmlSource("<InpUT>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("input", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationTagSpacesBehind()
        {
            var s = new HtmlSource("<i   >");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("i", ((HtmlTagToken)token).Name);
        }

        [TestMethod]
        public void TokenizationCharacterReferenceNotin()
        {
            var str = string.Empty;
            var src = "I'm &notin; I tell you";
            var s = new HtmlSource(src);
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
            var s = new HtmlSource(src);
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
            var s = new HtmlSource("<!doctype html>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.DOCTYPE, token.Type);
        }

        [TestMethod]
        public void TokenizationCommentDetected()
        {
            var s = new HtmlSource("<!-- hi my friend -->");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
        }

        [TestMethod]
        public void TokenizationCDataDetected()
        {
            var s = new HtmlSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new HtmlTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
        }

        [TestMethod]
        public void TokenizationCDataCorrectCharacters()
        {
            StringBuilder sb = new StringBuilder();
            var s = new HtmlSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = new HtmlTokenizer(s);
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
    }
}
