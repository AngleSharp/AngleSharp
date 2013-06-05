using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a singular value.
    /// </summary>
    class CssComponentValue
    {
        object value;

        /// <summary>
        /// Gets or sets the preserved token (one of three values).
        /// </summary>
        public CssToken Preserved
        {
            get { return value as CssToken; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets the simple block (one of three values).
        /// </summary>
        public CssBlock Block
        {
            get { return value as CssBlock; }
            set { this.value = value; }
        }

        /// <summary>
        /// Gets or sets the function (one of three values).
        /// </summary>
        public CssFunction Function
        {
            get { return value as CssFunction; }
            set { this.value = value; }
        }

        /// <summary>
        /// Transforms the component value to a string.
        /// </summary>
        /// <returns>The serialized version.</returns>
        public override string ToString()
        {
            if (value is CssToken)
                return Preserved.ToValue();

            return value.ToString();
        }
    }
}
