namespace AngleSharp.DOM.Css.Media
{
    using System;

    abstract class MediaFeature : ICssObject
    {
        String _name;
        CSSValue _value;

        public MediaFeature(String name)
        {
            _name = name;
        }

        /// <summary>
        /// Gets the name of the feature.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the value of the feature.
        /// </summary>
        public CSSValue Value
        {
            get { return _value; }
            protected set { _value = value; }
        }

        public abstract Boolean SetDefaultValue();

        public abstract Boolean SetValue(CSSValue value);

        /// <summary>
        /// Validates the given feature.
        /// </summary>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public abstract Boolean Validate();

        /// <summary>
        /// Returns a CSS code representation of the medium.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            if (_value == null)
                return String.Concat("(", _name, ")");

            return String.Concat("(", _name, ": ", _value.ToCss(), ")");
        }
    }
}
