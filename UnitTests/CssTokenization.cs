using AngleSharp;
using AngleSharp.Css;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestClass]
    public class CssTokenization
    {
        [TestMethod]
        public void CssParserIdentifier()
        {
            var teststring = "h1 { background: blue; }";
            var parser = new CssParser(teststring);
            var list = parser.GetTokens();
            CssToken token = null;

            foreach (var item in list)
            {
                token = item;
                break;
            }

            Assert.AreEqual(CssTokenType.Ident, token.Type);
        }

        [TestMethod]
        public void CssParserAtRule()
        {
            var teststring = "@media { background: blue; }";
            var parser = new CssParser(teststring);
            var list = parser.GetTokens();
            CssToken token = null;

            foreach (var item in list)
            {
                token = item;
                break;
            }

            Assert.AreEqual(CssTokenType.AtKeyword, token.Type);
        }

        [TestMethod]
        public void CssParserUrlUnquoted()
        {
            var url = "http://someurl";
            var teststring = "url(" + url + ")";
            var parser = new CssParser(teststring);
            var list = parser.GetTokens();
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }

        [TestMethod]
        public void CssParserUrlDoubleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url(\"" + url + "\")";
            var parser = new CssParser(teststring);
            var list = parser.GetTokens();
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }

        [TestMethod]
        public void CssParserUrlSingleQuoted()
        {
            var url = "http://someurl";
            var teststring = "url('" + url + "')";
            var parser = new CssParser(teststring);
            var list = parser.GetTokens();
            CssStringToken token = null;

            foreach (var item in list)
            {
                token = item as CssStringToken;
                break;
            }

            Assert.AreEqual(url, token.Data);
        }
    }
}
