namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/columns
    /// </summary>
    public sealed class CSSColumnsProperty : CSSProperty
    {
        #region Fields

        CSSColumnWidthProperty _width;
        CSSColumnCountProperty _count;

        #endregion

        #region ctor

        internal CSSColumnsProperty()
            : base(PropertyNames.Columns)
        {
            _inherited = false;
            _count = new CSSColumnCountProperty();
            _width = new CSSColumnWidthProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width for the columns, if set.
        /// </summary>
        public CSSColumnWidthProperty Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the count for the columns, if set.
        /// </summary>
        public CSSColumnCountProperty Count
        {
            get { return _count; }
        }

        #endregion

        #region Methods

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
