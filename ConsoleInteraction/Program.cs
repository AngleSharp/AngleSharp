using System;
using System.Diagnostics;
using System.Net.Http;
using AngleSharp;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;
using AngleSharp.DOM.Collections;
using AngleSharp.Css;
using AngleSharp.Html;

namespace ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            //IMPORTANT:
            //http://www.w3.org/TR/domcore/#interface-htmlcollection

            CssSelectorTest.Slickspeed();

            TestCSS(Stylesheets.rsi, "a sample stylesheet");

            //TestHtml(Snippets.Invalid);

            //TestHtml(Webpages.Simon);

            //TestHtml(Webpages.Test);

            //TestHtml(Snippets.SelfClosedP);

            //TestHtml(Snippets.NormalClosedP);

            //TestHtmlFragment(@"<a href='?hi&amp=5'>hi</a>");

            //TestHtml("<div id=mydiv class=hi></div>", true);

            TestHtml(Webpages.CodeProject, "CodeProject");

            TestHtml(Webpages.Simon, "Simon's HP");

            TestHtml(Webpages.W3C, "W3C");

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

        static void TestWebRequest(string url, bool openConsole)
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

        static void TestHtml(string source, bool openConsole)
        {
            var doc = TestHtml(source);

            if (openConsole)
            {
                var console = new HtmlSharpConsole(doc);
                console.Capture();
            }
        }

        static HTMLDocument TestHtml(string source, string title = "the webpage")
        {
            var sw = Stopwatch.StartNew();
            var html = DocumentBuilder.Html(source);
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took ... " + sw.ElapsedMilliseconds + "ms");
            return html;
        }

        static NodeList TestHtmlFragment(string source)
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

        static void TestCSS(string source, string title = "the stylesheet")
        {
            var parser = new CssParser(source);
            var sw = Stopwatch.StartNew();
            var doc = parser.Result;
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took ... " + sw.ElapsedMilliseconds + "ms");
        }
    }
}