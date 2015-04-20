namespace AngleSharp.Playground.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;

    /// <summary>
    /// Creates tests based on the official tests
    /// specified at the W3C XML group:
    /// http://www.w3.org/XML/Test/xmlconf-20031210.html
    /// Also see the latest XML REC.
    /// http://www.w3.org/TR/REC-xml/
    /// </summary>
    class XMLConfTest
    {
        #region Fields

        readonly Dictionary<Mode, List<Entry>> tests;

        #endregion

        #region ctor

        XMLConfTest()
        {
            tests = new Dictionary<Mode, List<Entry>>();
        }

        #endregion

        #region Properties

        public Dictionary<Mode, List<Entry>> Tests
        {
            get { return tests; }
        }

        #endregion

        #region Methods

        public String ToCode()
        {
            var sb = new StringBuilder();

            foreach (var test in tests)
            {
                var mode = test.Key;
                var entries = test.Value;

                foreach (var entry in entries)
                {
                    sb.AppendLine("\t\t/// <summary>");
                    sb.Append    ("\t\t/// ").AppendLine(Description(entry));
                    sb.AppendLine("\t\t/// </summary>");
                    sb.AppendLine("\t\t[TestMethod]");
                    sb.Append    ("\t\tpublic void Xml").Append(mode.ToString()).Append(Transform(entry.FileName)).AppendLine("()");
                    sb.AppendLine("\t\t{");
                    sb.Append    ("\t\t\tvar document = DocumentBuilder.Xml(@\"").Append(entry.Content.Replace("\"", "\"\"")).AppendLine("\");");
                    sb.AppendLine("\t\t}");
                }
            }

            return sb.ToString();
        }

        #endregion

        #region Generator

        public static void Run()
        {
            Console.Write("Processing files ... ");
            var sw = Stopwatch.StartNew();
            var a = XMLConfTest.GenerateFromFile(@"...\xmlconf-20010315.htm");
            var code = a.ToCode();

            File.WriteAllText("xmltest.cs", code);
            sw.Stop();
            Console.WriteLine("done!");
            Console.WriteLine("{0} files have been processed in {1} ms.", a.Tests.Select(m => m.Value).Select(m => m.Count).Sum(), sw.ElapsedMilliseconds);
        }

        static String baseDir;

        /// <summary>
        /// Generates the unit tests starting at the given root path.
        /// e.g. Generate(@"C:\Downloads\xmlconf\xmlconf-20010315.htm")
        /// </summary>
        /// <param name="path">The path of the XML conf test suite overview.</param>
        public static XMLConfTest GenerateFromFile(String path)
        {
            if (!File.Exists(path))
                throw new ArgumentException("The given path is not valid.");

            baseDir = Path.GetDirectoryName(path);

            using (var fs = File.OpenRead(path))
            {
                var document = DocumentBuilder.Html(fs);
                return GenerateFromHtml(document);
            }
        }

        /// <summary>
        /// Generates the unit tests from the given overview document.
        /// </summary>
        /// <param name="document">The document to inspect.</param>
        public static XMLConfTest GenerateFromHtml(IDocument document)
        {
            var body = document.Body;
            var mode = Mode.None;
            var test = new XMLConfTest();

            for (int i = 0; i < body.ChildNodes.Length; i++)
            {
                var child = body.ChildNodes[i];
                var element = child as IHtmlAnchorElement;

                if (element != null && element.HasAttribute("name"))
                {
                    switch (element.GetAttribute("name"))
                    {
                        case "valid":
                            mode = Mode.Valid;
                            break;
                        case "invalid":
                            mode = Mode.Invalid;
                            break;
                        case "not-wf":
                            mode = Mode.NotWf;
                            break;
                        case "error":
                            mode = Mode.Error;
                            break;
                        default:
                            continue;
                    }

                    test.tests.Add(mode, new List<Entry>());
                }
                else if (mode != Mode.None && child is IHtmlTableElement)
                {
                    var list = test.tests[mode];
                    Inspect((IHtmlTableElement)child, list);
                }
            }

            return test;
        }

        static void Inspect(IHtmlTableElement table, List<Entry> list)
        {
            var entry = new Entry();
            var cells = table.QuerySelectorAll("table td");
            var blub = cells.Select(m => m.TextContent).ToArray();
            var path = String.Empty;
            entry.Rules = cells[2].TextContent;
            entry.FileName = cells[4].TextContent;
            entry.Collection = cells[6].TextContent;
            entry.Text = cells[7].TextContent.Trim();

            if (entry.Collection.StartsWith("IBM"))
                path = IBMPath(entry.FileName);
            else if (entry.Collection.StartsWith("James Clark"))
                path = XmlTestPath(entry.FileName);
            else if (entry.Collection.StartsWith("Sun"))
                path =SunPath(entry.FileName);
            else if (entry.Collection.StartsWith("OASIS"))
                path = OasisPath(entry.FileName);
            else if (entry.Collection.StartsWith("Fuji"))
                path = XmlFujiPath(entry.FileName);
            else
                path = EdUniPath(entry.FileName);

            if (File.Exists(path))
            {
                entry.Content = File.ReadAllText(path);
                list.Add(entry);
            }
        }

        #endregion

        #region Enumeration

        public enum Mode
        {
            None,
            Valid,
            Invalid,
            NotWf,
            Error
        }

        #endregion

        #region Entry

        public class Entry
        {
            public String FileName
            {
                get;
                set;
            }

            public String Rules
            {
                get;
                set;
            }

            public String Text
            {
                get;
                set;
            }

            public String Collection
            {
                get;
                set;
            }

            public String Content
            {
                get;
                set;
            }
        }

        #endregion

        #region Path Helpers

        static String EdUniPath(String path)
        {
            return null;
        }

        static String XmlFujiPath(String path)
        {
            return Path.Combine(Path.Combine(baseDir, "japanese"), path + ".xml");
        }

        static String IBMPath(String path)
        {
            return Path.Combine(Path.Combine(baseDir, "ibm"), path.Remove(0, 4).Replace('-', Path.DirectorySeparatorChar));
        }

        static String OasisPath(String path)
        {
            return Path.Combine(Path.Combine(baseDir, "oasis"), path.Remove(0, 2) + ".xml");
        }

        static String SunPath(String path)
        {
            var files = Directory.GetFiles(Path.Combine(baseDir, "sun"), path + ".xml", SearchOption.AllDirectories);

            if(files.Length > 0)
                return files[0];

            if(path.StartsWith("v-"))
                files = Directory.GetFiles(Path.Combine(Path.Combine(baseDir, "sun"), "valid"), path.Remove(0, 2) + ".xml", SearchOption.AllDirectories);

            if (files.Length > 0)
                return files[0];

            if (path.StartsWith("inv-"))
                files = Directory.GetFiles(Path.Combine(Path.Combine(baseDir, "sun"), "invalid"), path.Remove(0, 4) + ".xml", SearchOption.AllDirectories);

            return files.Length > 0 ? files[0] : null;
        }

        static String XmlTestPath(String path)
        {
            //keep ext-sa, not-sa
            path = path.Replace("ext-sa", "ext+sa").Replace("not-sa", "not+sa");
            path = path.Replace('-', Path.DirectorySeparatorChar);
            path = path.Replace("ext+sa", "ext-sa").Replace("not+sa", "not-sa");
            return Path.Combine(Path.Combine(baseDir, "xmltest"),  path + ".xml");
        }

        #endregion

        #region Helpers

        static String Description(Entry entry)
        {
            var text = entry.Text.Replace('\n', ' ');
            text = Regex.Replace(text, @"\s+", " ");

            return String.Format("{0} Here the section(s) {1} apply. This test is taken from the collection {2}.", 
                text, entry.Rules, entry.Collection);
        }

        static String Transform(String p)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(p.Replace(".xml", "").Replace('-', ' ')).Replace(" ", "");
        }

        #endregion
    }
}
