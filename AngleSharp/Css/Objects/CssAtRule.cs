using System;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS @rule.
    /// </summary>
    class CssAtRule : CssNamedRule
    {
        /// <summary>
        /// Gets or sets the value block.
        /// </summary>
        public CssBlock Value 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Transforms the @rule into a string.
        /// </summary>
        /// <returns>The serialized content.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append('@').Append(Name);

            for (int i = 0; i < Prelude.Count; i++)
                sb.Append(Prelude[i]);

            sb.Append(Value);
            return sb.ToString();
        }
    }
}
