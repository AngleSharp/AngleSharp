using System;
using System.Diagnostics;
using System.Net.Http;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Collections;
using AngleSharp.Css;
using AngleSharp.Html;
using System.Text;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Xml;

namespace ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {            
            CssSelectorTest.Slickspeed();

            //TestCSSFrom("http://www.facebook.com");

            //TestCSSFrom("http://www.tumblr.com");

            //TestCSSFrom("http://www.flickr.com");

            TestCSS(Stylesheets.rsi, "a sample stylesheet");

            TestCSS(Stylesheets.CodeProject, "CodeProject's stylesheet");

            TestHtml(Snippets.Invalid, "an invalid snippet");

            TestHtml(HtmlFiles.Test, "a test page");

            TestHtml(HtmlFiles.CodeProject, "CodeProject's webpage");

            TestHtml(HtmlFiles.Simon, "Simon's HP");

            TestHtml(HtmlFiles.W3C, "the W3C webpage");

            TestXml(XmlFiles.Note, "The XML note file");

            TestWebRequest("http://www.imdb.com/", false);

            TestWebRequest("http://www.dailymail.co.uk/‎", false);

            TestWebRequest("http://news.google.com/", false);

            TestWebRequest("http://www.huffingtonpost.com/", false);

            TestWebRequest("http://www.nbcnews.com/", false);

            TestWebRequest("http://www.amazon.com/", false);

            TestWebRequest("http://www.yahoo.com/", false);

            TestWebRequest("http://www.codeproject.com/", false);

            TestWebRequest("http://www.florian-rappl.de/", false);
        }

        static void TestCSSFrom(String url)
        {
            var sw = Stopwatch.StartNew();
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var source = result.Content.ReadAsStreamAsync().Result;

            sw.Stop();
            Console.WriteLine("Loading " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            var html = DocumentBuilder.Html(source);
            sw.Stop();

            Console.WriteLine("Parsing " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
            var sb = new StringBuilder();

            for (int i = 0; i < html.StyleSheets.Length; i++)
			{
                if (String.IsNullOrEmpty(html.StyleSheets[i].Href))
                    sb.Append(html.StyleSheets[i].OwnerNode.TextContent);
                else
                {
                    url = html.StyleSheets[i].Href;

                    if (url.StartsWith("//"))
                        url = "http:" + url;

                    sw.Restart();
                    sb.Append(client.GetAsync(url).Result.Content.ReadAsStringAsync().Result);
                    sw.Stop();
                    Console.WriteLine("Loading " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
                }
			}

            sw.Restart();
            var sheet = CssParser.ParseStyleSheet(sb.ToString());
            sw.Stop();
            Console.WriteLine("Parsing all stylesheets took ... " + sw.ElapsedMilliseconds + "ms");
        }

        static void TestWebRequest(String url, Boolean openConsole)
        {
            var sw = Stopwatch.StartNew();
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var source = result.Content.ReadAsStreamAsync().Result;

            sw.Stop();
            Console.WriteLine("Loading " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            var html = DocumentBuilder.Html(source);
            sw.Stop();

            Console.WriteLine("Parsing " + url + " took ... " + sw.ElapsedMilliseconds + "ms");

            if (openConsole)
            {
                var console = new HtmlSharpConsole(html);
                console.Capture();
            }
        }

        static void TestHtml(String source, Boolean openConsole)
        {
            var doc = TestHtml(source);

            if (openConsole)
            {
                var console = new HtmlSharpConsole(doc);
                console.Capture();
            }
        }

        static XMLDocument TestXml(String source, String title = "XML document")
        {
            var sw = Stopwatch.StartNew();
            var xml = DocumentBuilder.Xml(source);
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took ... " + sw.ElapsedMilliseconds + "ms");
            return xml;
        }

        static HTMLDocument TestHtml(String source, String title = "HTML document")
        {
            var sw = Stopwatch.StartNew();
            var html = DocumentBuilder.Html(source);
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took ... " + sw.ElapsedMilliseconds + "ms");
            return html;
        }

        static NodeList TestHtmlFragment(String source)
        {
            var sw = Stopwatch.StartNew();
            var nodes = DocumentBuilder.HtmlFragment(source);
            sw.Stop();
            Console.WriteLine(">>> START");
            Console.WriteLine(nodes.ToHtml());
            Console.WriteLine(">>> END");
            Console.WriteLine();
            Console.WriteLine("... " + sw.ElapsedMilliseconds + "ms");
            Console.WriteLine("=====================================");
            return nodes;
        }

        static CSSStyleSheet TestCSS(String source, String title = "CSS document")
        {
            var parser = new CssParser(source);
            var sw = Stopwatch.StartNew();
            var doc = parser.Result;
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took ... " + sw.ElapsedMilliseconds + "ms");
            return doc;
        }
    }
}