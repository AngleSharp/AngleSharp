namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;
    using System.IO;

    [MemoryDiagnoser, ShortRunJob]
    public class HtmlSerializationBenchmark
    {
        private IDocument document;

        [GlobalSetup]
        public void GlobalSetup()
        {
            var page = File.ReadAllText(FindPagePath());
            document = new HtmlParser().ParseDocument(page);
        }

        [Benchmark]
        public String SerializeW3CPage() => document.ToHtml();

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
