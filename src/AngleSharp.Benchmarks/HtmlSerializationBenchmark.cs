namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.IO;
    using System.Text;

    [MemoryDiagnoser, ShortRunJob]
    public class HtmlSerializationBenchmark
    {
        private IDocument document;
        private IDocument wideDocument;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var page = File.ReadAllText(FindPagePath());
            document = new HtmlParser().ParseDocument(page);

            // Past the first UInt16.MaxValue children a node list keeps no index hint, which is
            // where asking each child for its position among its siblings turned serialization
            // quadratic (#1435). A real page never comes near that, so page.html cannot show it.
            var wide = new StringBuilder().Insert(0, "<b>x</b>", 100_000).ToString();
            wideDocument = new HtmlParser().ParseDocument(wide);
        }

        [Benchmark]
        public String SerializeW3CPage() => document.ToHtml();

        [Benchmark]
        public String SerializeWideChildList() => wideDocument.ToHtml();

        private static String FindPagePath()
        {
            for (var directory = new DirectoryInfo(AppContext.BaseDirectory);
                 directory is not null;
                 directory = directory.Parent)
            {
                var path = Path.Combine(directory.FullName, "page.html");

                if (File.Exists(path))
                {
                    return path;
                }
            }

            throw new FileNotFoundException("Could not locate the page.html benchmark fixture.");
        }
    }
}
