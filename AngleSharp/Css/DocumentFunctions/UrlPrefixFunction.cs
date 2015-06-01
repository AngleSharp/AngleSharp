namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as a url prefix function.
    /// </summary>
    sealed class UrlPrefixFunction : IDocumentFunction
    {
        public UrlPrefixFunction(String url)
        {
            Data = url;
        }

        public String Name
        {
            get { return FunctionNames.Url_Prefix; }
        }

        public String Data
        {
            get;
            private set;
        }
    }
}
