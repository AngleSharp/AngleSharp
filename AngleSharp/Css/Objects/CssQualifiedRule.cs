using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Gets or sets the qualified rule (most qualified rules are style rules).
    /// </summary>
    class CssQualifiedRule : CssRule
    {
        /// <summary>
        /// Creates a new qualified rule.
        /// </summary>
        public CssQualifiedRule()
	    {
            Value = new List<CssNamedRule>();
	    }

        /// <summary>
        /// Gets or sets the value of the qualified rule.
        /// </summary>
        public List<CssNamedRule> Value 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Transforms the qualified rule to a string.
        /// </summary>
        /// <returns>A serialized version.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Prelude.Count; i++)
                sb.Append(Prelude[i]);

            sb.Append('{');

            for (int i = 0; i < Value.Count; i++)
                sb.Append(Value[i]);

            sb.Append('}');

            return sb.ToString();
        }
    }
}
