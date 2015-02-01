using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Html;
using AngleSharp.Parser.Css;
using AngleSharp.Parser.Html;
using ConsoleInteraction.Assets;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInteraction
{
    class Program
    {
        static void Main(string[] args)
        {
            //var form = new ConsoleForm(DocumentBuilder.Html(new Uri("http://localhost:48609/PostNormal"), new Configuration { AllowRequests = true }));
            //var form = new ConsoleForm("<form method=post action='http://requestb.in/1hc34c81'><input type=hidden name=q value=r><input type=text name=Name><input type=text name=Birthday><input type=text name=Remark></form>");
            //var form = new ConsoleForm("<form method=post action='http://requestb.in/1hc34c81' enctype='multipart/form-data'><input type=hidden name=q value=r><input type=text name=Name><input type=text name=Birthday><input type=text name=Remark><input type=file name=myfile></form>");
            //var form = new ConsoleForm("<form method=post action='http://requestb.in/1hc34c81' enctype='text/plain'><input type=hidden name=q value=r><input type=text name=Name><input type=text name=Birthday><input type=text name=Remark></form>");
            //form.FillInteractive();
            //form.Submit();

            TestAsync().Wait();

            TestCSSFrom("http://www.facebook.com");

            TestCSSFrom("http://www.tumblr.com");

            TestCSSFrom("http://www.flickr.com");

            TestCSS(Stylesheets.rsi, "a sample stylesheet");

            TestCSS(Stylesheets.CodeProject, "CodeProject's stylesheet");

            TestHtml(Snippets.Invalid, "an invalid snippet");

            ReadHtmlFiles(HtmlFiles.ResourceManager);

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

        static async Task ExtractContent(String url)
        {
            var requestUri = new Uri(url);
            var httpClient = new HttpClient();

            using (var response = await httpClient.GetStreamAsync(requestUri))
            {
                using (var fs = File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), requestUri.Host)))
                    await response.CopyToAsync(fs);
            }
        }

        public static async Task test()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetStreamAsync(new Uri("http://trade.500.com/bjdc/?expect=140107"));
            var document = DocumentBuilder.Html(response);

            var ls = document.GetElementsByClassName("vs_lines");
            var ls2 = document.GetElementsByClassName("vs_lines even");
            var c = ls.Concat(ls2);

            foreach (var item in c)
            {
                var tr = item as IHtmlTableRowElement;

                if (tr.Index % 2 == 0)
                    tr.Style.Display = "none";
            }
        }

        static async Task TestAsync()
        {
            Console.WriteLine("Starting async!");
            var parser = new HtmlParser(HtmlFiles.W3C);

            var task = parser.ParseAsync();
            var sw = Stopwatch.StartNew();

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

        static void TestHtml(String source, Boolean openConsole)
        {
            var doc = TestHtml(source);

            if (openConsole)
            {
                var console = new HtmlSharpConsole(doc);
                console.Capture();
            }
        }

        static IDocument TestHtml(String source, String title = "HTML document")
        {
            var sw = Stopwatch.StartNew();
            var html = DocumentBuilder.Html(source);
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took ... " + sw.ElapsedMilliseconds + "ms");
            return html;
        }

        static INodeList TestHtmlFragment(String source)
        {
            var sw = Stopwatch.StartNew();
            var nodes = DocumentBuilder.HtmlFragment(source);
            sw.Stop();
            Console.WriteLine(">>> START");

            foreach (var element in nodes.OfType<IElement>())
                Console.WriteLine(element.ToHtml());

            Console.WriteLine(">>> END");
            Console.WriteLine();
            Console.WriteLine("... " + sw.ElapsedMilliseconds + "ms");
            Console.WriteLine("=====================================");
            return nodes;
        }

        static ICssStyleSheet TestCSS(String source, String title = "CSS document")
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