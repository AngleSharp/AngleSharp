using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Represents a syntax error while parsing XML documents.
    /// </summary>
    public sealed class XmlSyntaxException : Exception
    {
        /// <summary>
        /// Creates a new XmlSyntaxException object.
        /// </summary>
        /// <param name="message">The message of the error.</param>
        /// <param name="url">The URL of the error.</param>
        /// <param name="rule">The rule number.</param>
        public XmlSyntaxException(String message, String url, String rule)
            : base(message)
        {
            Url = url;
            Rule = rule;
        }

        /// <summary>
        /// Gets the URL with a description of the error.
        /// </summary>
        public String Url
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rule number of the error.
        /// </summary>
        public String Rule
        {
            get;
            private set;
        }
    }
}
