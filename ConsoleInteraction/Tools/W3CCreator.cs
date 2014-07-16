using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Html;
using AngleSharp.Parser.Css;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleInteraction
{
    /// <summary>
    /// Creates tests based on the official tests
    /// specified at the W3C page:
    /// http://www.w3.org/Style/CSS/Test/
    /// </summary>
    class W3CCreator
    {
        static readonly Regex replaceIdentifier = new Regex("[^a-zA-Z0-9]");

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
            doc.BaseUri = url;
            var links = doc.QuerySelectorAll("body > ul > li > a");
            var methods = new List<String>();

            foreach (IHtmlAnchorElement link in links)
                CreateCssSelectorTest(link.Href, methods);
        }

        static void CreateCssSelectorTest(String url, List<String> methods)
        {
            Console.Write("Loading " + url + " ... ");
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var source = result.Content.ReadAsStreamAsync().Result;

            Document html = null;

            try { html = DocumentBuilder.Html(source); }
            catch { Console.WriteLine("error!!!"); return; }

            var title = Sanatize(html.GetElementsByTagName("title")[0].TextContent);
            var content = html.GetElementsByTagName("content")[0].InnerHTML.Trim().Replace("\"", "\"\"");
            var css = html.GetElementsByTagName("css")[0].TextContent;
            var sheet = CssParser.ParseStyleSheet(css);
            var selectors = new StringBuilder();
            var i = 1;

            if (methods.Contains(title))
            {
                var ltr = 'A';

                while (methods.Contains(title + ltr.ToString()))
                    ltr = (Char)((int)ltr + 1);

                title += ltr.ToString();
            }

            foreach (var rule in sheet.CssRules)
            {
                if (rule is CSSStyleRule)
                {
                    selectors.Append(@"
	        var selectorINDEX = doc.QuerySelectorAll(""SELECTOR"");
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
            methods.Add(title);
        }

        static String Sanatize(String p)
        {
            var name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p).Replace(" ", "");

            if (String.IsNullOrEmpty(name))
                return "TestMethod";
            else if (!Char.IsLetter(name[0]))
                name = "Test" + name;

            return replaceIdentifier.Replace(name, String.Empty);
        }
    }
}
