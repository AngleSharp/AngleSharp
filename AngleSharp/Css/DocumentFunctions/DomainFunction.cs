namespace AngleSharp.Css.DocumentFunctions
{
    using System;

    /// <summary>
    /// Take as domain.
    /// </summary>
    sealed class DomainFunction : IDocumentFunction
    {
        public DomainFunction(String url)
        {
            Data = url;
        }

        public String Name
        {
            get { return FunctionNames.Domain; }
        }

        public String Data
        {
            get;
            private set;
        }
    }
}
