using System;
using System.Collections.Generic;
using System.Text;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS function.
    /// </summary>
    class CssFunction
    {
        /// <summary>
        /// Creates a new CSS function.
        /// </summary>
        public CssFunction()
        {
            Arguments = new List<CssArg>();
            IsValid = true;
            Tokens = new List<CssToken>();
        }

        /// <summary>
        /// Gets or sets the name of the function.
        /// </summary>
        public string Name 
        { 
            get; 
            set;
        }

        /// <summary>
        /// Gets the list of tokens for the function.
        /// </summary>
        public List<CssToken> Tokens
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of arguments for the function.
        /// </summary>
        public List<CssArg> Arguments 
        { 
            get; 
            private set;
        }

        /// <summary>
        /// Gets or sets if the syntax for the function has been invalid.
        /// </summary>
        public bool IsValid 
        {
            get; 
            set; 
        }

        /// <summary>
        /// Transforms the function to a string.
        /// </summary>
        /// <returns>The serialized content.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Name);
            sb.Append('(');

            if (Arguments.Count != 0)
            {
                sb.Append(Arguments[0]);

                for (int i = 1; i < Arguments.Count; i++)
                    sb.Append(',').Append(Arguments[i]);
            }

            sb.Append(')');
            return sb.ToString();
        }
    }
}
