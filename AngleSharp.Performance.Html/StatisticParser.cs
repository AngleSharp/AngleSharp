namespace AngleSharp.Performance.Html
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp;
    using AngleSharp.Parser.Html;

    class StatisticParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration();

        readonly Dictionary<String, Int32> _bins = new Dictionary<String, Int32>();

        public String Name
        {
            get { return "AngleSharp-Statistics"; }
        }

        public Type Library
        {
            get { return typeof(HtmlParser); }
        }

        public void Run(String source)
        {
            var parser = new HtmlParser(source, configuration);
            var document = parser.Parse();

            foreach (var element in document.All)
            {
                var tag = element.NodeName;
                var count = 0;
                _bins.TryGetValue(tag, out count);
                count++;
                _bins[tag] = count;
            }
        }

        public void Print()
        {
            var index = 1;
            Console.WriteLine("Most used items");
            Console.WriteLine("---------------");

            foreach (var element in _bins.OrderByDescending(m => m.Value))
                Console.WriteLine("{0}. {1} ( {2} )", index++, element.Key, element.Value);
        }
    }
}
