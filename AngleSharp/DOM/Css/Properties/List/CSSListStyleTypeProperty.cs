namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// </summary>
    public sealed class CSSListStyleTypeProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, ListStyle> styles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase);
        ListStyle _style;

        #endregion

        #region ctor

        static CSSListStyleTypeProperty()
        {
            styles.Add("disc", ListStyle.Disc);
            styles.Add("circle", ListStyle.Circle);
            styles.Add("square", ListStyle.Square);
            styles.Add("decimal", ListStyle.Decimal);
            styles.Add("decimal-leading-zero", ListStyle.DecimalLeadingZero);
            styles.Add("lower-roman", ListStyle.LowerRoman);
            styles.Add("upper-roman", ListStyle.UpperRoman);
            styles.Add("lower-greek", ListStyle.LowerGreek);
            styles.Add("lower-latin", ListStyle.LowerLatin);
            styles.Add("upper-latin", ListStyle.UpperLatin);
            styles.Add("armenian", ListStyle.Armenian);
            styles.Add("georgian", ListStyle.Georgian);
            styles.Add("lower-alpha", ListStyle.LowerLatin);
            styles.Add("upper-alpha", ListStyle.UpperLatin);
            styles.Add("none", ListStyle.None);
        }

        internal CSSListStyleTypeProperty()
            : base(PropertyNames.ListStyleType)
        {
            _inherited = true;
            _style = ListStyle.Disc;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected style for the list.
        /// </summary>
        public ListStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            ListStyle position;

            if (value is CSSIdentifierValue && styles.TryGetValue(((CSSIdentifierValue)value).Value, out position))
                _style = position;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
