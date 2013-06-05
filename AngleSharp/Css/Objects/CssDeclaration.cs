using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS declaration.
    /// </summary>
    class CssDeclaration : CssNamedRule
    {
        /// <summary>
        /// Gets or sets if the declaration has been marked as important.
        /// </summary>
        public bool Important 
        { 
            get;
            set; 
        }

        /// <summary>
        /// Gets the list of component values.
        /// </summary>
        public List<CssComponentValue> Value
        {
            get { return Prelude; }
        }

        /// <summary>
        /// Transforms the given declaration into a string.
        /// </summary>
        /// <returns>The serialized content.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name).Append(':');

            for (int i = 0; i < Prelude.Count; i++)
                sb.Append(Prelude[i]);

            if (Important)
                sb.Append("!important");

            sb.Append(';');
            return sb.ToString();
        }
    }
}
