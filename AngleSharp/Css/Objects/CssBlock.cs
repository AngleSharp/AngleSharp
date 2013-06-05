using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a simple block of rules in CSS.
    /// </summary>
    class CssBlock
    {
        /// <summary>
        /// Creates a simple block of rules.
        /// </summary>
        public CssBlock()
        {
            Value = new List<CssComponentValue>();
            Tokens = new List<CssToken>();
        }

        /// <summary>
        /// Gets or sets the associated token for this block of rules.
        /// </summary>
        public CssBracketToken AssociatedToken
        { 
            get; 
            set; 
        }

        /// <summary>
        /// Gets the used tokens.
        /// </summary>
        public List<CssToken> Tokens
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the contained values.
        /// </summary>
        public List<CssComponentValue> Value 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Transforms the simple block of rules to a string.
        /// </summary>
        /// <returns>The serialized version.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(AssociatedToken.Open);

            for (int i = 0; i < Value.Count; i++)
                sb.Append(Value[i]);

            sb.Append(AssociatedToken.Close);
            return sb.ToString();
        }
    }
}
