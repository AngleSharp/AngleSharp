namespace AngleSharp.Performance.Css
{
    using Alba.CsCss.Style;
    using System;

    class CsCssParser : ITestee
    {
        public String Name
        {
            get { return "CsCss"; }
        }

        public Type Library
        {
            get { return typeof(CssLoader); }
        }

        public void Run(String source)
        {
            var parser = new CssLoader { Compatibility = Alba.CsCss.BrowserCompatibility.FullStandards };

            try
            {
                parser.ParseSheet(source, new Uri("http://localhost/foo.css"), new Uri("http://localhost"));
            }
            catch 
            { 
                // May crash otherwise if invalid input detected ...
            }
        }
    }
}
