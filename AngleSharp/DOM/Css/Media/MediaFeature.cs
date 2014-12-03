namespace AngleSharp.DOM.Css.Media
{
    using System;

    /// <summary>
    /// Represents a feature expression within
    /// a media query.
    /// </summary>
    public abstract class MediaFeature
    {
        #region Fields

        readonly String _name;
        ICssValue _value;

        #endregion

        #region ctor

        internal MediaFeature(String name)
        {
            _name = name;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the feature.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the value of the feature, if any.
        /// </summary>
        public ICssValue Value
        {
            get { return _value; }
        }

        /// <summary>
        /// Gets a CSS code representation of the medium.
        /// </summary>
        public String CssText
        {
            get
            {
                var ending = _value != null ? ": " + _value.CssText : String.Empty;
                return String.Concat("(", _name, ending, ")");
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to set the default value.
        /// </summary>
        /// <returns>True if the default value is acceptable, otherwise false.</returns>
        protected abstract Boolean TrySetDefault();

        /// <summary>
        /// Tries to set the given value.
        /// </summary>
        /// <param name="value">The value that should be used.</param>
        /// <returns>True if the given value is valid, otherwise false.</returns>
        protected abstract Boolean TrySetCustom(ICssValue value);

        /// <summary>
        /// Tries to set the given value.
        /// </summary>
        /// <param name="value">The value that should be used.</param>
        /// <returns>True if the given value is accepted, otherwise false.</returns>
        internal Boolean TrySetValue(ICssValue value)
        {
            var result = false;

            if (value == null)
                result = TrySetDefault();
            else
                result = TrySetCustom(value);

            if (result)
                _value = value;

            return result;
        }

        /// <summary>
        /// Validates the given feature.
        /// </summary>
        /// <param name="window">The current browsing window.</param>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public abstract Boolean Validate(IWindow window);

        #endregion
    }
}
