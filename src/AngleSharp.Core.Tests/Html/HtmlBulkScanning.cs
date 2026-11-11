namespace AngleSharp.Core.Tests.Html
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.Dom.Events;
    using AngleSharp.Html.Parser;
    using AngleSharp.Html.Parser.Tokens;
    using AngleSharp.Text;
    using NUnit.Framework;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The tokenizer takes runs of plain text - character data, raw text, attribute values,
    /// comments, names - out of a string or an array in one go, and reads every other source
    /// one character at a time. Both ways have to arrive at the same tokens, the same errors
    /// and the same positions, so each input here goes through all of them.
    /// </summary>
    [TestFixture]
    public class HtmlBulkScanningTests
    {
        private static readonly String[] Sources =
        [
            // character data with every kind of line break, a NUL and references
            "plain\r\ntext\rwith\nbreaks &amp; a\0NUL &notanentity; &#x41;<p>and\r\n\r\nmore</p>",
            // names: upper case, NUL, quotes, equals, solidus, non-ASCII
            "<DIV ID=a CLASS='B'><sPaN dAtA-X=\"1\"></SPAN><x-ä ö=ü></x-ä><a\0b c\0d=e></a\0b><p a\"b='c' d<e=f g=h/ i></p></DIV>",
            // attribute values: both quotes, unquoted, references, breaks, NUL, empty, missing
            "<a href=\"a&amp;b\r\nc\0d&notit;e&lt\" title='x\"y&#39;z\r' data-a=un&amp;quoted data-b=\"\" data-c='' data-d data-e = \"spaced\" >x</a>",
            // many attributes, duplicates, more than fit a token inline, more than the plain scan covers
            "<i a=1 b=2 c=3 d=4 e=5 A=6 f=7 g=8 h=9 i=10 b=11 j=12></i><b a=1 b=2 c=3 d=4 e=5 f=6 g=7 h=8 i=9 j=10></b>",
            // comments
            "<!-- a - b -- c --- d\r\ne\0f --><!----><!--->--><!-- unfinished - -",
            "<!-- a --!> b <!-- c --!- d --><!-x><!doctype html><?pi x?>",
            // raw text, RCDATA and their end tags
            "<style>a < b\r\n</styl></STYLE ><title>t &amp; <b>\0\r</TITLE><textarea>\r\n</textare x\r</textarea><xmp>&amp;<</xmp>",
            // script data, escaped and double escaped
            "<script>if (a < b && c > d) { '</scrip' }\r\n\0</script><script><!-- - -- <script>'</script>' --> -</script> x</script>",
            "<script><!--<script>-</script>--<x\r\n--></script><script><!-- unfinished -",
            // plain text swallows the rest
            "<p>before</p><plaintext>after </plaintext> <b>\r\n\0 &amp; all of it",
            // foreign content, CDATA
            "<svg viewBox='0 0 1 1'><foreignObject><P>x</P></foreignObject><![CDATA[a ]] b ]]> c]]></svg><math><mi xlink:href=a>&lt;</mi></math>",
            // everything that can end early
            "<", "</", "<a", "<a ", "<a b", "<a b=", "<a b='c", "<a b=\"c", "<a b=c", "<a b='c'", "<a/", "<!--", "<!-- a", "&", "&am", "a\r", "<a b\r", "<A\r\n",
        ];

        [TestCaseSource(nameof(Sources))]
        public void TokensDoNotDependOnHowTheSourceIsRead(String html)
        {
            var expected = Tokenize(new TextSource(html));

            Assert.AreEqual(expected, Tokenize(new TextSource(new ReadOnlyMemoryTextSource(html.AsMemory()))), "from memory");
            Assert.AreEqual(expected, Tokenize(new TextSource(new ReadOnlyMemoryTextSource(Padded(html)))), "from a slice");
            Assert.AreEqual(expected, Tokenize(new TextSource(new CharArrayTextSource(html.ToCharArray(), html.Length))), "from an array");
        }

        [TestCaseSource(nameof(Sources))]
        public void TreeDoesNotDependOnHowTheSourceIsRead(String html)
        {
            var expected = Parse(parser => parser.ParseDocument(new TextSource(html)));

            Assert.AreEqual(expected, Parse(parser => parser.ParseDocument(html)), "from a string");
            Assert.AreEqual(expected, Parse(parser => parser.ParseDocument(html.AsMemory())), "from memory");
            Assert.AreEqual(expected, Parse(parser => parser.ParseDocument(html.ToCharArray(), html.Length)), "from an array");
        }

        [Test]
        public void TreeOfAllSourcesTogetherDoesNotDependOnHowTheSourceIsRead()
        {
            // One input after the other, so that each kind of run also starts where another ended.
            var html = String.Join("\r\n", Sources, 0, 11);
            var expected = Parse(parser => parser.ParseDocument(new TextSource(html)));

            Assert.AreEqual(expected, Parse(parser => parser.ParseDocument(html)));
            Assert.AreEqual(expected, Parse(parser => parser.ParseDocument(html.AsMemory())));
        }

        [Test]
        public void NamesAreTakenOverWithTheirCaseWhenPreserved()
        {
            var html = "<svg viewBox=a><P ID=x dataFoo=y></P></svg>";
            var options = new HtmlParserOptions { IsPreservingAttributeNames = true };
            var expected = Parse(parser => parser.ParseDocument(new TextSource(html)), options);

            Assert.AreEqual(expected, Parse(parser => parser.ParseDocument(html.AsMemory()), options));
            StringAssert.Contains("dataFoo=\"y\"", expected);
        }

        [Test]
        public void DuplicatesAreFoundInEachTagOfManyAttributesOnItsOwn()
        {
            // More than eight attributes take the set-based check, and the set is shared by all
            // tags of a document: the names of the first tag must not count against the second.
            var html = "<p a=1 b=2 c=3 d=4 e=5 f=6 g=7 h=8 i=9 a=10></p><p a=1 b=2 c=3 d=4 e=5 f=6 g=7 h=8 i=9 j=10></p>";
            var errors = 0;
            var others = 0;
            var document = html.ToHtmlDocument(onError: (_, _) => errors++);
            var paragraphs = document.QuerySelectorAll("p");
            // What a document reports that has no attribute twice, e.g., the missing doctype.
            "<p a=1 b=2 c=3 d=4 e=5 f=6 g=7 h=8 i=9 j=10></p>".ToHtmlDocument(onError: (_, _) => others++);

            Assert.AreEqual(9, paragraphs[0].Attributes.Length);
            Assert.AreEqual("1", paragraphs[0].GetAttribute("a"));
            Assert.AreEqual(10, paragraphs[1].Attributes.Length);
            Assert.AreEqual("10", paragraphs[1].GetAttribute("j"));
            Assert.AreEqual(others + 1, errors);
        }

        [Test]
        public void LongRunsOfEveryKindAreTakenOverComplete()
        {
            // Longer than any window a search is done in, with the terminator at the very end.
            var run = new String('x', 5000);
            var html = $"<p class=\"{run}\" id='{run}'>{run}</p><!--{run}--><script>{run}</script><style>{run}</style><{run} {run}={run}>";
            var document = html.ToHtmlDocument();
            var paragraph = document.QuerySelector("p");

            Assert.AreEqual(run, paragraph.GetAttribute("class"));
            Assert.AreEqual(run, paragraph.GetAttribute("id"));
            Assert.AreEqual(run, paragraph.TextContent);
            Assert.AreEqual(run, document.Body.ChildNodes[1].TextContent);
            Assert.AreEqual(run, document.QuerySelector("script").TextContent);
            Assert.AreEqual(run, document.QuerySelector("style").TextContent);
            Assert.AreEqual(run, document.Body.LastElementChild.LocalName);
            Assert.AreEqual(run, document.Body.LastElementChild.GetAttribute(run));
        }

        private static ReadOnlyMemory<Char> Padded(String html)
        {
            // A slice that neither starts at the beginning of its array nor ends at its end.
            var chars = ("<b>" + html + "</b>").ToCharArray();
            return new ReadOnlyMemory<Char>(chars, 3, html.Length);
        }

        private static String Tokenize(TextSource source)
        {
            var sb = new StringBuilder();
            using var tokenizer = new HtmlTokenizer(source, HtmlEntityProvider.Resolver);
            tokenizer.Error += (_, error) => sb.Append("error ").Append(error.Code).Append(" @").Append(error.Position).Append('\n');

            while (true)
            {
                var token = tokenizer.Get();
                sb.Append(token.Type).Append(" @").Append(token.Position).Append(" [").Append(token.Data).Append(']');

                if (token is HtmlTagToken tag)
                {
                    sb.Append(tag.IsSelfClosing ? " /" : " ");

                    foreach (var attribute in tag.Attributes)
                    {
                        sb.Append(" @").Append(attribute.Position).Append(' ').Append(attribute.Name).Append("=[").Append(attribute.Value).Append(']');
                    }
                }

                sb.Append('\n');

                if (token.Type == HtmlTokenType.EndOfFile)
                {
                    return sb.ToString();
                }
            }
        }

        private static String Parse(Func<HtmlParser, IDocument> parse, HtmlParserOptions options = default)
        {
            var errors = new List<String>();
            var parser = new HtmlParser(options);
            parser.Error += (_, ev) => errors.Add(ev is HtmlErrorEvent error ? $"{error.Code} @{error.Position}" : ev.Type);
            var document = parse(parser);
            return document.ToHtml() + "\n" + String.Join("\n", errors);
        }
    }
}
