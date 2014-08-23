namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-origins
    /// </summary>
    public sealed class CSSBackgroundOriginProperty : CSSProperty
    {
        #region Fields

        List<BoxModel> _origins;

        #endregion

        #region ctor

        internal CSSBackgroundOriginProperty()
            : base(PropertyNames.BackgroundOrigin)
        {
            _origins = new List<BoxModel>();
            _origins.Add(BoxModel.PaddingBox);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the desired origin settings.
        /// </summary>
        public IEnumerable<BoxModel> Origins
        {
            get { return _origins; }
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
            if (value == CSSValue.Inherit)
                return true;

            var values = value as CSSValueList ?? new CSSValueList(value);
            var origins = new List<BoxModel>();

            for (int i = 0; i < values.Length; i++)
            {
                var origin = values[i].ToBoxModel();

                if (!origin.HasValue)
                    return false;

                origins.Add(origin.Value);

                if (++i < values.Length && values[i] != CSSValue.Separator)
                    return false;
            }

            _origins = origins;
            return true;
        }

        #endregion
    }
}
