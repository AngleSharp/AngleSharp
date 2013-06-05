using System;
using System.Collections.Generic;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents the common base for all rules.
    /// </summary>
    abstract class CssRule
    {
        /// <summary>
        /// Creates a new base rule.
        /// </summary>
        public CssRule()
	    {
            Prelude = new List<CssComponentValue>();
	    }

        /// <summary>
        /// Gets or sets the prelude (or sometimes value) of the rule.
        /// </summary>
        public List<CssComponentValue> Prelude 
        { 
            get; 
            set; 
        }
    }
}
