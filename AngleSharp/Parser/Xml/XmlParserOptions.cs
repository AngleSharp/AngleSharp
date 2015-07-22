namespace AngleSharp.Parser.Xml
{
    using System;

    /// <summary>
    /// Contains a number of options for the XML parser.
    /// </summary>
    public struct XmlParserOptions
    {
        /// <summary>
        /// Gets or sets if errors should be surpressed. This way the document
        /// will be parsed for sure, but may look weird.
        /// </summary>
        public Boolean IsSuppressingErrors 
        { 
            get; 
            set; 
        }
    }
}
