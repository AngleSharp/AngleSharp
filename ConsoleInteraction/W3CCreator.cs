using AngleSharp;
using AngleSharp.Css;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Xml;
using System;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;

namespace ConsoleInteraction
{
    /// <summary>
    /// Creates tests based on the official tests
    /// specified at the W3C page:
    /// http://www.w3.org/Style/CSS/Test/
    /// </summary>
    class W3CCreator
    {
        /// <summary>
        /// Gets all the tests from the url given below.
        /// </summary>
        public static void CreateCssSelectorTests()
        {
            var url = "http://www.w3.org/Style/CSS/Test/CSS3/Selectors/current/xml/full/flat/index.html";
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var source = result.Content.ReadAsStreamAsync().Result;
            var doc = DocumentBuilder.Html(source);
            doc.BaseURI = url;
            var links = doc.QuerySelectorAll("body > ul > li > a");

            foreach (HTMLAnchorElement link in links)
                CreateCssSelectorTest(link.Href);
        }

        static void CreateCssSelectorTest(String url)
        {
            Console.Write("Loading " + url + " ... ");
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var source = result.Content.ReadAsStreamAsync().Result;

            XMLDocument xml = null;

            try { xml = DocumentBuilder.Xml(source); }
            catch { Console.WriteLine("error!!!"); return; }

            var title = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(xml.GetElementsByTagName("title")[0].TextContent).Replace(" ", "");
            var content = xml.GetElementsByTagName("content")[0].InnerHTML.Trim().Replace("\"", "\"\"");
            var css = xml.GetElementsByTagName("css")[0].TextContent;
            var sheet = CssParser.ParseStyleSheet(css);
            var selectors = new StringBuilder();
            var i = 1;

            foreach (var rule in sheet.CssRules)
            {
                if (rule is CSSStyleRule)
                {
                    selectors.Append(@"
	        var selectorINDEX = doc.QuerySelectorAll(SELECTOR);
	        Assert.AreEqual(0, selectorINDEX.Length);"
                .Replace("SELECTOR", ((CSSStyleRule)rule).SelectorText)
                .Replace("INDEX", i.ToString()));
                    i++;
                }
            }

            File.AppendAllText("test.cs", @"
        /// <summary>
        /// Test taken from URL
        /// </summary>
        public void TITLE()
        {
	        var source = @""HTML"";
	        var doc = DocumentBuilder.Html(source);
	        SELECTORS
        }
"
            .Replace("URL", url)
            .Replace("TITLE", title)
            .Replace("HTML", content)
            .Replace("SELECTORS", selectors.ToString())
            );
            Console.WriteLine("success.");
        }
    }
}
