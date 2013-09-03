using System;
using System.Text;
using System.Net.Http;
using System.Resources;
using System.Diagnostics;
using System.Globalization;
using AngleSharp;
using AngleSharp.Css;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Css;
using AngleSharp.DOM.Xml;
using AngleSharp.DOM.Collections;
using ConsoleInteraction.Assets;
using AngleSharp.Html;
using System.Threading.Tasks;

namespace ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            TestAsync().Wait();
  
            CssSelectorTest.Slickspeed();

            //TestCSSFrom("http://www.facebook.com");

            //TestCSSFrom("http://www.tumblr.com");

            //TestCSSFrom("http://www.flickr.com");

            TestCSS(Stylesheets.rsi, "a sample stylesheet");

            TestCSS(Stylesheets.CodeProject, "CodeProject's stylesheet");

            TestHtml(Snippets.Invalid, "an invalid snippet");

            ReadHtmlFiles(HtmlFiles.ResourceManager);

            TestXml(XmlFiles.Note, "The XML note file");

            TestHtmlFrom("http://www.imdb.com/", false);

            TestHtmlFrom("http://www.dailymail.co.uk/‎", false);

            TestHtmlFrom("http://news.google.com/", false);

            TestHtmlFrom("http://www.huffingtonpost.com/", false);

            TestHtmlFrom("http://www.nbcnews.com/", false);

            TestHtmlFrom("http://www.amazon.com/", false);

            TestHtmlFrom("http://www.yahoo.com/", false);

            TestHtmlFrom("http://www.codeproject.com/", false);

            TestHtmlFrom("http://www.florian-rappl.de/", false);
        }

        static async Task TestAsync()
        {
            Console.WriteLine("Starting async!");
            var sw = Stopwatch.StartNew();
            var parser = new HtmlParser(HtmlFiles.W3C);

            var task = parser.ParseAsync();

            while (!task.IsCompleted)
            {
                await Task.Delay(15);
                Console.WriteLine("{0} | {1} elements", sw.ElapsedMilliseconds, parser.Result.All.Length);
            }

            sw.Stop();
            Console.WriteLine("Done!");
        }

        static void ReadHtmlFiles(ResourceManager resourceManager)
        {
            var rs = resourceManager.GetResourceSet(CultureInfo.CurrentCulture, true, true);
            var it = rs.GetEnumerator();

            while (it.MoveNext())
                TestHtml(it.Value.ToString(), it.Key.ToString());
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

        static void TestHtmlFrom(String url, Boolean openConsole)
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

        static void TestXmlFrom(String url)
        {
            var sw = Stopwatch.StartNew();
            var client = new HttpClient();
            var result = client.GetAsync(url).Result;
            var source = result.Content.ReadAsStreamAsync().Result;

            sw.Stop();
            Console.WriteLine("Loading " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            var xml = DocumentBuilder.Xml(source);
            sw.Stop();

            Console.WriteLine("Parsing " + url + " took ... " + sw.ElapsedMilliseconds + "ms");
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