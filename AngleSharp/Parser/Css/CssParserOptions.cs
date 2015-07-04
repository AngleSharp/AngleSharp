namespace AngleSharp.Parser.Css
{
    using System;

    /// <summary>
    /// Contains a number of options for the CSS parser.
    /// </summary>
    public struct CssParserOptions
    {
        /// <summary>
        /// Gets or sets if unknown (@-) rules are dropped.
        /// </summary>
        public Boolean IsDroppingUnknownRules
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if unknown declarations are dropped.
        /// </summary>
        public Boolean IsDroppingUnknownDeclarations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if invalid values for declarations should be ignored.
        /// </summary>
        public Boolean IsIgnoringInvalidValues
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if invalid constraints should be ignored.
        /// </summary>
        public Boolean IsIgnoringInvalidConstraints
        {
            get;
            set;
        }
    }
}
