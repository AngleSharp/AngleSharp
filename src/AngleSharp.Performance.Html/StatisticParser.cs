namespace AngleSharp.Performance.Html
{
    using AngleSharp;
    using AngleSharp.Parser.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class StatisticParser : ITestee
    {
        static readonly IConfiguration configuration = new Configuration();
        static readonly HtmlParser parser = new HtmlParser(configuration);

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
            var document = parser.Parse(source);

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
