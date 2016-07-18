namespace AngleSharp.Parser.Html
{
    using System;

    /// <summary>
    /// Contains a number of options for the HTML parser.
    /// </summary>
    public struct HtmlParserOptions
    {
        /// <summary>
        /// Gets or sets if the document is embedded.
        /// </summary>
        public Boolean IsEmbedded
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if scripting is allowed.
        /// </summary>
        public Boolean IsScripting
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if errors should be treated as exceptions.
        /// </summary>
        public Boolean IsStrictMode
        {
            get;
            set;
        }
    }
}
