namespace AngleSharp.Performance.Css
{
    using System;
    using ExCSS;

    class ExCssParser : ITestee
    {
        public String Name
        {
            get { return "ExCSS"; }
        }

        public Type Library
        {
            get { return typeof(Parser); }
        }

        public void Run(String source)
        {
            var parser = new Parser();
            parser.Parse(source);
        }
    }
}
