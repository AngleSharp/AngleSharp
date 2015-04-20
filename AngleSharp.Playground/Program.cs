namespace AngleSharp.Playground
{
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Net.Http;
    using System.Resources;
    using System.Threading.Tasks;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using AngleSharp.Parser.Html;
    using AngleSharp.Playground.Tools;
    using ConsoleInteraction.Assets;

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

            TestCss(Stylesheets.rsi, "a sample stylesheet");

            TestCss(Stylesheets.CodeProject, "CodeProject's stylesheet");

            TestHtml(Snippets.Invalid, "an invalid snippet");

            ReadHtmlFiles(HtmlFiles.ResourceManager);
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
            var parser = new HtmlParser(source);
            var sw = Stopwatch.StartNew();
            var document = parser.Parse();
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took " + sw.ElapsedMilliseconds + "ms");
            return document;
        }

        static ICssStyleSheet TestCss(String source, String title = "CSS document")
        {
            var parser = new CssParser(source);
            var sw = Stopwatch.StartNew();
            var sheet = parser.Result;
            sw.Stop();
            Console.WriteLine("Parsing " + title + " took " + sw.ElapsedMilliseconds + "ms");
            return sheet;
        }
    }
}