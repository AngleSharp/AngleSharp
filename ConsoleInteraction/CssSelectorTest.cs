using AngleSharp;
using AngleSharp.DOM.Html;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleInteraction
{
    /// <summary>
    /// Represents a class to make some CSS selector(speed) tests.
    /// </summary>
    class CssSelectorTest
    {
        string _url;
        HTMLDocument _doc;
        List<string> _tests;
        int n;

        /// <summary>
        /// Creates a new speed test.
        /// </summary>
        public CssSelectorTest()
        {
            _tests = new List<string>();
            n = 100;
        }

        /// <summary>
        /// Gets or sets the number of iterations (between 1 and 1000000).
        /// </summary>
        public int N
        {
            get { return n; }
            set
            {
                if(value > 0 && value <= 1000000)
                    n = value;
            }
        }

        /// <summary>
        /// Loads a document from the given URL.
        /// </summary>
        /// <param name="url">
        /// A valid URL e.g. http://www.w3.org/TR/2001/CR-css3-selectors-20011113/.
        /// </param>
        /// <returns>The current instance.</returns>
        public CssSelectorTest LoadFromUrl(string url)
        {
            Console.Write("Loading ");
            Console.Write(url);
            Console.Write(" . . . ");
            _url = url;
            HttpClient client = new HttpClient();
            var content = client.GetAsync(url).Result.Content.ReadAsStreamAsync().Result;
            _doc = DocumentBuilder.Html(content);
            Console.WriteLine("loaded !");
            return this;
        }

        /// <summary>
        /// Loads a document from the given source.
        /// </summary>
        /// <param name="source">
        /// A valid HTML document.
        /// </param>
        /// <returns>The current instance.</returns>
        public CssSelectorTest LoadFromSource(string source)
        {
            _doc = DocumentBuilder.Html(source);
            return this;
        }

        /// <summary>
        /// Adds a test to the number of tests.
        /// </summary>
        /// <param name="test">The selector to test.</param>
        /// <returns>The test instance.</returns>
        public CssSelectorTest AddTest(string test)
        {
            _tests.Add(test);
            return this;
        }

        /// <summary>
        /// Adds tests to the number of tests.
        /// </summary>
        /// <param name="tests">The selector(s) to test.</param>
        /// <returns>The test instance.</returns>
        public CssSelectorTest AddTests(params string[] tests)
        {
            _tests.AddRange(tests);
            return this;
        }

        /// <summary>
        /// Applies all tests.
        /// </summary>
        /// <returns>The test instance.</returns>
        public CssSelectorTest Run()
        {
            Console.WriteLine("Running tests ...");
            var totalTime = 0L;

            for (int i = 0; i < _tests.Count; i++)
            {
                var test = _tests[i];
                var result = _doc.QuerySelectorAll(test).Length;
                var sw = Stopwatch.StartNew();

                for (int j = 0; j < n; j++)
                    _doc.QuerySelectorAll(test);

                sw.Stop();
                var time = sw.ElapsedMilliseconds / n;
                totalTime += time;
                Console.WriteLine("\t{0}\t{1}\t( {2} ms )", test.PadRight(25), result.ToString().PadLeft(4), time);
            }

            Console.WriteLine("... finished [ {0} ms ] !", totalTime);
            return this;
        }

        /// <summary>
        /// Runs a query and shows the result.
        /// </summary>
        /// <param name="query">The query to run.</param>
        /// <returns>The current instance.</returns>
        public CssSelectorTest Show(string query)
        {
            var sw = Stopwatch.StartNew();
            var results = _doc.QuerySelectorAll(query);
            sw.Stop();
            Console.WriteLine("Fetching {0} results took {1} ms.", results.Length, sw.ElapsedMilliseconds);

            foreach (var result in results)
            {
                Console.WriteLine("\t<{0}{1}>", result.NodeName, result.Attributes.ToHtml());
            }

            return this;
        }

        /// <summary>
        /// A standard test (Slickspeed). Found at:
        /// http://mootools.net/slickspeed/
        /// </summary>
        public static void Slickspeed()
        {
            var test = new CssSelectorTest();
            test.AddTests("body", "div", "body div", "div p", "div > p", "div + p", "div ~ p", "div[class^=exa][class$=mple]", "div p a", 
                "div, p, a", ".note", "div.example", "ul .tocline2", "div.example, div.note", "#title", "h1#title", "div #title", 
                "ul.toc li.tocline2", "ul.toc > li.tocline2", "h1#title + div > p", "h1[id]:contains(Selectors)", "a[href][lang][class]", 
                "div[class]", "div[class=example]", "div[class^=exa]", "div[class$=mple]", "div[class*=e]", "div[class|=dialog]", 
                "div[class!=made_up]", "div[class~=example]", "div:not(.example)", "p:contains(selectors)", "p:nth-child(even)", 
                "p:nth-child(2n)", "p:nth-child(odd)", "p:nth-child(2n+1)", "p:nth-child(n)", "p:only-child", "p:last-child", "p:first-child");

            try
            {
                test.LoadFromUrl("http://mootools.net/slickspeed/system/template.php");
            }
            catch (AggregateException)
            {
                Console.WriteLine();
                Console.WriteLine("Could not load the document from the given URL, switching to local version . . .");
                test.LoadFromSource(Webpages.W3C);
            }

            test.Run();

            //test.Show("div > p");

            /*
***************************************************  final time should be between 12 and 407 ms  ***************************************************
body	                        0 ms | 1 found	    1 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    1 ms | 1 found
div	                            1 ms | 51 found	    1 ms | 51 found	    1 ms | 51 found	    0 ms | 51 found	    1 ms | 51 found	    0 ms | 51 found
body div	                    1 ms | 51 found	    0 ms | 51 found	    1 ms | 51 found	    0 ms | 51 found	    2 ms | 51 found	    1 ms | 51 found
div p	                        2 ms | 140 found	1 ms | 140 found	0 ms | 140 found	0 ms | 140 found	3 ms | 140 found	1 ms | 140 found
div > p	                        2 ms | 134 found	1 ms | 134 found	0 ms | 134 found	0 ms | 134 found	2 ms | 134 found	1 ms | 134 found
div + p	                        2 ms | 22 found	    0 ms | 22 found	    0 ms | 22 found	    0 ms | 22 found	    3 ms | 22 found	    1 ms | 22 found
div ~ p	                        3 ms | 183 found	1 ms | 183 found	1 ms | 183 found	1 ms | 183 found	4 ms | 183 found	1 ms | 183 found
div[class^=exa][class$=mple]	2 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    2 ms | 43 found	    1 ms | 43 found
div p a	                        3 ms | 12 found	    1 ms | 12 found	    1 ms | 12 found	    0 ms | 12 found	    5 ms | 12 found	    1 ms | 12 found
div, p, a	                    3 ms | 671 found    3 ms | 671 found	1 ms | 671 found	1 ms | 671 found	8 ms | 671 found	1 ms | 671 found
.note	                        5 ms | 14 found	    1 ms | 14 found	    1 ms | 14 found	    0 ms | 14 found	    5 ms | 14 found	    1 ms | 14 found
div.example	                    1 ms | 43 found	    1 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    1 ms | 43 found	    1 ms | 43 found
ul .tocline2	                2 ms | 12 found	    0 ms | 12 found	    0 ms | 12 found	    0 ms | 12 found	    9 ms | 12 found	    0 ms | 12 found
div.example, div.note	        2 ms | 44 found	    1 ms | 44 found	    0 ms | 44 found	    0 ms | 44 found	    4 ms | 44 found	    0 ms | 44 found
#title	                        1 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    1 ms | 1 found
h1#title	                    1 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    1 ms | 0 found
div #title	                    1 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    1 ms | 1 found	    1 ms | 1 found
ul.toc li.tocline2	            1 ms | 12 found	    0 ms | 12 found	    0 ms | 12 found	    0 ms | 12 found	    2 ms | 12 found	    1 ms | 12 found
ul.toc > li.tocline2	        1 ms | 12 found	    0 ms | 12 found	    0 ms | 12 found	    0 ms | 12 found	    1 ms | 12 found	    1 ms | 12 found
h1#title + div > p	            0 ms | 0 found	    0 ms | 0 found	    0 ms | 0 found	    0 ms | 0 found	    2 ms | 0 found	    0 ms | 0 found
h1[id]:contains(Selectors)	    1 ms | 1 found	    0 ms | 1 found	    1 ms | 1 found	    1 ms | 1 found	    0 ms | 1 found	    1 ms | 1 found
a[href][lang][class]	        2 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    0 ms | 1 found	    3 ms | 1 found	    1 ms | 1 found
div[class]	                    1 ms | 51 found	    0 ms | 51 found	    0 ms | 51 found	    0 ms | 51 found	    1 ms | 51 found	    1 ms | 51 found
div[class=example]	            2 ms | 43 found	    1 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    1 ms | 43 found	    0 ms | 43 found
div[class^=exa]	                1 ms | 43 found	    1 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    1 ms | 43 found	    1 ms | 43 found
div[class$=mple]	            1 ms | 43 found	    1 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    1 ms | 43 found	    1 ms | 43 found
div[class*=e]	                1 ms | 50 found	    0 ms | 50 found	    0 ms | 50 found	    0 ms | 50 found	    1 ms | 50 found	    1 ms | 50 found
div[class|=dialog]	            0 ms | 0 found	    0 ms | 0 found	    0 ms | 0 found	    0 ms | 0 found	    0 ms | 0 found	    1 ms | 0 found
div[class!=made_up]	            1 ms | 51 found	    2 ms | 51 found	    2 ms | 51 found	    2 ms | 51 found	    1 ms | 51 found	    1 ms | 0 found
div[class~=example]	            1 ms | 43 found	    1 ms | 43 found	    0 ms | 43 found	    0 ms | 43 found	    1 ms | 43 found	    1 ms | 43 found
div:not(.example)	            1 ms | 8 found	    0 ms | 8 found	    0 ms | 8 found	    0 ms | 8 found	    5 ms | 8 found	    0 ms | 8 found
p:contains(selectors)	        3 ms | 54 found	    3 ms | 54 found	    4 ms | 54 found	    3 ms | 54 found	    3 ms | 54 found	    1 ms | 57 found
p:nth-child(even)	            5 ms | 158 found	1 ms | 158 found	1 ms | 158 found	0 ms | 158 found	39 ms | 158 found	1 ms | 158 found
p:nth-child(2n)	                4 ms | 158 found	1 ms | 158 found	0 ms | 158 found	1 ms | 158 found	44 ms | 158 found	1 ms | 158 found
p:nth-child(odd)	            4 ms | 166 found	1 ms | 166 found	0 ms | 166 found	0 ms | 166 found	44 ms | 166 found	1 ms | 166 found
p:nth-child(2n+1)	            3 ms | 166 found	1 ms | 166 found	1 ms | 166 found	1 ms | 166 found	41 ms | 166 found	1 ms | 166 found
p:nth-child(n)	                3 ms | 324 found	1 ms | 324 found	1 ms | 324 found	1 ms | 324 found	43 ms | 324 found	1 ms | 324 found
p:only-child	                2 ms | 3 found	    0 ms | 3 found	    0 ms | 3 found	    0 ms | 3 found	    41 ms | 3  found	1 ms | 3 found
p:last-child	                2 ms | 19 found	    1 ms | 19 found	    0 ms | 19 found	    0 ms | 19 found	    41 ms | 19 found	0 ms | 19 found
p:first-child	                2 ms | 54 found	    1 ms | 54 found	    0 ms | 54 found	    1 ms | 54 found	    41 ms | 54 found	1 ms | 54 found
             */
        }
    }
}
