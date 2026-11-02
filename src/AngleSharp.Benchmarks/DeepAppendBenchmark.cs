namespace AngleSharp.Benchmarks
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Parser;
    using BenchmarkDotNet.Attributes;
    using System;

    /// <summary>
    /// Builds a chain of nested elements one AppendChild at a time, the way a tree builder that
    /// goes through the DOM (AngleSharp.Xml's XmlDomBuilder) does.
    /// </summary>
    /// <remarks>
    /// The hierarchy checks of every insert used to walk the ancestors of the insertion point, so
    /// this was quadratic in <see cref="Depth"/>. MutationBenchmark.AppendChild is the shallow
    /// counterpart, and the HTML parser never comes here: it builds through AddNode.
    /// </remarks>
    [MemoryDiagnoser]
    public class DeepAppendBenchmark
    {
        private IDocument _document;

        [Params(100, 1000, 10000)]
        public Int32 Depth { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            _document = new HtmlParser().ParseDocument("<!doctype html><body></body>");
        }

        [Benchmark]
        public INode AppendChain()
        {
            var document = _document;
            var current = (INode)document.CreateElement("div");

            for (var i = 0; i < Depth; i++)
            {
                current = current.AppendChild(document.CreateElement("a"));
            }

            return current;
        }
    }
}
