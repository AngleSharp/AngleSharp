namespace AngleSharp.Performance.Css
{
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(String[] args)
        {
            var stylesheets = new UrlTests(
                extension: ".css", 
                withBuffer: true);

            stylesheets.Include(
                "http://www.csszengarden.com/215/215.css?v=8may2013",
                "http://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css",
                "http://cdnjs.cloudflare.com/ajax/libs/jqueryui/1.11.4/jquery-ui.css",
                "http://www.florian-rappl.de/Content/style?v=o2O40dFmfq2JG0tQyfQctozyaA9IcUQxq9b6x16JOKw1",
                "http://z-ecx.images-amazon.com/images/G/01/AUIClients/AmazonUI-a607978b4c59f6c52279818077903ce9a01d14e2.rendering_engine-not-trident.min._V2_.css",
                "http://assets-cdn.github.com/assets/github2/index-0d569068eabc0305b93b246950eb913fc9d2948b952824895172164835b351dd.css",
                "http://codeproject.cachefly.net/App_Themes/CodeProject/Css/Main.min.css?dt=2.8.150616.1",
                "http://fbstatic-a.akamaihd.net/rsrc.php/v2/yX/r/fr81HMP3WaY.css").Wait();

            var parsers = new List<ITestee>
            {
                new AngleSharpParser()
            };

            var testsuite = new TestSuite(parsers, stylesheets.Tests, new Output(), new Warmup())
            {
                NumberOfRepeats = 5,
                NumberOfReRuns = 1
            };

            testsuite.Run();
        }
    }
}
