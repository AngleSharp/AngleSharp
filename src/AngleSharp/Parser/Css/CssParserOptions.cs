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
        public Boolean IsIncludingUnknownRules
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if unknown declarations are dropped.
        /// </summary>
        public Boolean IsIncludingUnknownDeclarations
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if style rules with invalid selectors should included.
        /// </summary>
        public Boolean IsToleratingInvalidSelectors
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if invalid values for declarations should be ignored.
        /// </summary>
        public Boolean IsToleratingInvalidValues
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if invalid constraints should be ignored.
        /// </summary>
        public Boolean IsToleratingInvalidConstraints
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if CSS trivia (whitespace, tabs, comments, ...) should
        /// be stored.
        /// </summary>
        public Boolean IsStoringTrivia
        {
            get;
            set;
        }
    }
}
