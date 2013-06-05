using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS name rule, i.e. a base object for all rules with names.
    /// </summary>
    abstract class CssNamedRule : CssRule
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name 
        {
            get; 
            set;
        }
    }
}
