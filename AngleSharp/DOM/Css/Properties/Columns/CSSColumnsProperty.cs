namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    sealed class CSSColumnsProperty : CSSProperty, ICssColumnsProperty
    {
        #region Fields

        CSSColumnWidthProperty _width;
        CSSColumnCountProperty _count;

        #endregion

        #region ctor

        internal CSSColumnsProperty()
            : base(PropertyNames.Columns)
        {
            _count = new CSSColumnCountProperty();
            _width = new CSSColumnWidthProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if width should be used.
        /// </summary>
        public Boolean UseWidth
        {
            get { return _width.IsUsed; }
        }

        /// <summary>
        /// Gets if count should be used.
        /// </summary>
        public Boolean UseCount
        {
            get { return _count.IsUsed; }
        }

        /// <summary>
        /// Gets the width for the columns, if set.
        /// </summary>
        public Length Width
        {
            get { return _width.Width; }
        }

        /// <summary>
        /// Gets the count for the columns, if set.
        /// </summary>
        public Int32 Count
        {
            get { return _count.Count; }
        }

        Boolean ICssColumnCountProperty.IsUsed
        {
            get { return _count.IsUsed; }
        }

        Boolean ICssColumnWidthProperty.IsUsed
        {
            get { return _width.IsUsed; }
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

            var index = 0;
            var list = value as CSSValueList ?? new CSSValueList(value);
            var startGroup = new List<CSSProperty>(2);
            var width = new CSSColumnWidthProperty();
            var count = new CSSColumnCountProperty();
            startGroup.Add(width);
            startGroup.Add(count);

            while (true)
            {
                var length = startGroup.Count;

                for (int i = 0; i < length; i++)
                {
                    if (CheckSingleProperty(startGroup[i], index, list))
                    {
                        startGroup.RemoveAt(i);
                        index++;
                        break;
                    }
                }

                if (length == startGroup.Count)
                    break;
            }

            if (index == list.Length)
            {
                _width = width;
                _count = count;
                return true;
            }

            return false;
        }

        #endregion
    }
}
