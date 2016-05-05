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
                "http://cdn.sstatic.net/stackoverflow/all.css?v=5a386bc7d85d",
                "http://fbstatic-a.akamaihd.net/rsrc.php/v2/yX/r/fr81HMP3WaY.css",
                "https://s.yimg.com/zz/combo?/os/stencil/3.1.0/styles-ltr.css&/os/fp/atomic-css.2c9008c2.css",
                "http://static.licdn.com/sc/h/59ugqor0mlj9qjt7ohmi1kpko,cy00fqioblh011agl8l3n36sc",
                "http://style.aliunicorn.com/css/6v/??apollo/core/core-ws-responsive.css,run/wholesale/font-v1.css,run/site/en/common/header/header-v150213.css,run/site/en/common/bottombar/bottom-bar.css?t=145f12b3e_104c763f9a",
                "http://www.florian-rappl.de/Content/style?v=o2O40dFmfq2JG0tQyfQctozyaA9IcUQxq9b6x16JOKw1").Wait();

            var parsers = new List<ITestee>
            {
                new AngleSharpParser(),
                new ExCssParser(),
                new CsCssParser()
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
