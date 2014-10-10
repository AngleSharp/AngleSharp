namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CSSColumnsProperty : CSSProperty, ICssColumnsProperty
    {
        #region Fields

        Int32? _count;
        Length? _width;

        #endregion

        #region ctor

        internal CSSColumnsProperty()
            : base(PropertyNames.Columns, PropertyFlags.Animatable)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width for the columns, if set.
        /// </summary>
        public Length? Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the count for the columns, if set.
        /// </summary>
        public Int32? Count
        {
            get { return _count; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _count = null;
            _width = null;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Auto))
            {
                _width = null;
                _count = null;
            }
            else
            {
                var n = 0;
                var list = value.AsEnumeration();
                Int32? count = null;
                Length? width = null;

                foreach (var entry in list)
                {
                    if (n++ < 2)
                    {
                        if (count == null && (count = entry.ToInteger()).HasValue)
                            continue;
                        else if (width == null && (width = entry.ToLength()).HasValue)
                            continue;
                        else if (entry.Is(Keywords.Auto))
                            continue;
                    }

                    return false;
                }

                _width = width;
                _count = count;
            }

            return true;
        }

        #endregion
    }
}
