namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Use regular expression function.
    /// </summary>
    public class RegexpFunction : IDocumentFunction
    {
        public RegexpFunction(String url)
        {
            Data = url;
        }

        public String Name
        {
            get { return FunctionNames.Regexp; }
        }

        public String Data
        {
            get;
            private set;
        }
    }
}
