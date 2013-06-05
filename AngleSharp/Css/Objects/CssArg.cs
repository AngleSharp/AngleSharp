using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents an argument for a CSS function.
    /// </summary>
    class CssArg
    {
        /// <summary>
        /// Creates the argument object.
        /// </summary>
        public CssArg()
        {
            Values = new List<CssComponentValue>();
        }

        /// <summary>
        /// Gets or sets the list of component values for this argument.
        /// </summary>
        public List<CssComponentValue> Values 
        {
            get; 
            set;
        }

        /// <summary>
        /// Transforms the argument into a string.
        /// </summary>
        /// <returns>The serialized content.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < Values.Count; i++)
                sb.Append(Values[i]);

            return sb.ToString();
        }
    }
}
