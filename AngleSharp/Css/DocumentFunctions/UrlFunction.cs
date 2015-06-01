namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as url function.
    /// </summary>
    sealed class UrlFunction : IDocumentFunction
    {
        public UrlFunction(String url)
        {
            Data = url;
        }

        public String Name
        {
            get { return FunctionNames.Url; }
        }

        public String Data
        {
            get;
            private set;
        }
    }
}
