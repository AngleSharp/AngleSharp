namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Html;
    using AngleSharp.Html.Parser;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    [TestFixture]
    public class HtmlTokenizationTests
    {
        private static HtmlTokenizer CreateTokenizer(TextSource source)
        {
            return new HtmlTokenizer(source, HtmlEntityProvider.Resolver);
        }

        [Test]
        public void TokenizationFinalEOF()
        {
            var s = new TextSource("");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.EndOfFile, token.Type);
        }
        [Test]
        public void TokenizationLongerCharacterReference()
        {
            var content = "&abcdefghijklmnopqrstvwxyzABCDEFGHIJKLMNOPQRSTV;";
            var s = new TextSource(content);
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
            Assert.AreEqual(content, token.Data);
        }
        
        [Test]
        public void TokenizationStartTagDetection()
        {
            var s = new TextSource("<p>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.StartTag, token.Type);
            Assert.AreEqual("p", ((HtmlTagToken)token).Name);
        }

        [Test]
        public void TokenizationBogusCommentEmpty()
        {
            var s = new TextSource("<!>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual(String.Empty, token.Data);
        }

        [Test]
        public void TokenizationBogusCommentQuestionMark()
        {
            var s = new TextSource("<?>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual("?", token.Data);
        }

        [Test]
        public void TokenizationBogusCommentClosingTag()
        {
            var s = new TextSource("</ >");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
            Assert.AreEqual(" ", token.Data);
        }
        
        [Test]
        public void TokenizationTagNameDetection()
        {
            var s = new TextSource("<span>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("span", ((HtmlTagToken)token).Name);
        }
        
        [Test]
        public void TokenizationTagSelfClosingDetected()
        {
            var s = new TextSource("<img />");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(true, ((HtmlTagToken)token).IsSelfClosing);
        }
        
        [Test]
        public void TokenizationAttributesDetected()
        {
            var s = new TextSource("<a target='_blank' href='http://whatever' title='ho'>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(3, ((HtmlTagToken)token).Attributes.Count);
        }
        
        [Test]
        public void TokenizationAttributeNameDetection()
        {
            var s = new TextSource("<input required>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("required", ((HtmlTagToken)token).Attributes[0].Key);
        }

        [Test]
        public void TokenizationTagMixedCaseHandling()
        {
            var s = new TextSource("<InpUT>");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("input", ((HtmlTagToken)token).Name);
        }

        [Test]
        public void TokenizationTagSpacesBehind()
        {
            var s = new TextSource("<i   >");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual("i", ((HtmlTagToken)token).Name);
        }
        
        [Test]
        public void TokenizationCharacterReferenceNotin()
        {
            var str = string.Empty;
            var src = "I'm &notin; I tell you";
            var s = new TextSource(src);
            var t = CreateTokenizer(s);
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
            var t = CreateTokenizer(s);
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
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Doctype, token.Type);
        }
        
        [Test]
        public void TokenizationCommentDetected()
        {
            var s = new TextSource("<!-- hi my friend -->");
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Comment, token.Type);
        }

        [Test]
        public void TokenizationCDataDetected()
        {
            var s = new TextSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = CreateTokenizer(s);
            t.IsAcceptingCharacterData = true;
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
        }

        [Test]
        public void TokenizationCDataCorrectCharacters()
        {
            StringBuilder sb = new StringBuilder();
            var s = new TextSource("<![CDATA[hi mum how <!-- are you doing />]]>");
            var t = CreateTokenizer(s);
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
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(HtmlTokenType.Doctype, e.Type);
            var d = (HtmlDoctypeToken)e;
            Assert.IsNotNull(d.Name);
            Assert.AreEqual("root_element", d.Name);
            Assert.IsFalse(d.IsSystemIdentifierMissing);
            Assert.AreEqual("DTD_location", d.SystemIdentifier);
        }

        [Test]
        public void TokenizationOnlyCarriageReturn()
        {
            var s = new TextSource("\r");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, e.Type);
            Assert.AreEqual("\n", e.Data);
        }

        [Test]
        public void TokenizationOnlyLineFeed()
        {
            var s = new TextSource("\n");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, e.Type);
            Assert.AreEqual("\n", e.Data);
        }

        [Test]
        public void TokenizationCarriageReturnLineFeed()
        {
            var s = new TextSource("\r\n");
            var t = CreateTokenizer(s);
            var e = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, e.Type);
            Assert.AreEqual("\n", e.Data);
        }

        [Test]
        public async Task TokenizationChangeEncodingWithMultibyteCharacter()
        {
            var phrase = "ＡＢＣＤＥＦＧＨＩＪＫＬＭＮＯＰＱＲＳＴＵＶＷＸＹＺ";  // 78 bytes
            var content = String.Concat(Enumerable.Repeat(phrase, 53));    // x53 => 4134 bytes
            var encoding = new UTF8Encoding(false);
            using (var contentStm = new MemoryStream(encoding.GetBytes(content)))
            {
                var s = new TextSource(contentStm, encoding);
                var t = CreateTokenizer(s);
                // Read 4096 bytes to buffer
                await s.PrefetchAsync(100, CancellationToken.None);
                // Change encoding utf-8 to utf-8. (Same, but different instance)
                s.CurrentEncoding = TextEncoding.Utf8;
                var token = t.Get();
                Assert.IsTrue(s.CurrentEncoding == TextEncoding.Utf8);
                Assert.IsTrue(s.CurrentEncoding != encoding);
                Assert.AreEqual(content, token.Data);
            }
        }

        [Test]
        public void TokenizationLongestLegalCharacterReference()
        {
            var content = "&CounterClockwiseContourIntegral;";
            var s = new TextSource(content);
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
            Assert.AreEqual("∳", token.Data);
        }

        [Test]
        public void TokenizationLongestIllegalCharacterReference()
        {
            var content = "&CounterClockwiseContourIntegralWithWrongName;";
            var s = new TextSource(content);
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.AreEqual(HtmlTokenType.Character, token.Type);
            Assert.AreEqual("&CounterClockwiseContourIntegralWithWrongName;", token.Data);
        }

        [Test]
        public void TokenizationWithReallyLongAttributeShouldNotBreak()
        {
            var content = Assets.GetManifestResourceString("Html.HtmlTokenization.TokenizationWithReallyLongAttributeShouldNotBreak.txt");
            var s = new TextSource(content);
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.IsNotNull(token);
            Assert.IsInstanceOf<HtmlTagToken>(token);
        }

        [Test]
        public void TokenizationWithManyAttributesShouldNotBreak()
        {
            var content = Assets.GetManifestResourceString("Html.HtmlTokenization.TokenizationWithManyAttributesShouldNotBreak.txt");
            var s = new TextSource(content);
            var t = CreateTokenizer(s);
            var token = t.Get();
            Assert.IsNotNull(token);
            Assert.IsInstanceOf<HtmlTagToken>(token);
        }
    }
}
