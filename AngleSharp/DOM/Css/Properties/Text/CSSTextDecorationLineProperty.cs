namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-line
    /// </summary>
    public sealed class CSSTextDecorationLineProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TextDecorationLine> modes = new Dictionary<String, TextDecorationLine>();
        List<TextDecorationLine> _line;

        #endregion

        #region ctor

        static CSSTextDecorationLineProperty()
        {
            modes.Add("underline", TextDecorationLine.Underline);
            modes.Add("overline", TextDecorationLine.Overline);
            modes.Add("line-through", TextDecorationLine.LineThrough);
            modes.Add("blink", TextDecorationLine.Blink);
        }

        internal CSSTextDecorationLineProperty()
            : base(PropertyNames.TextDecorationLine)
        {
            _line = new List<TextDecorationLine>();
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all selected styles
        /// for text decoration lines.
        /// </summary>
        public IEnumerable<TextDecorationLine> Line
        {
            get { return _line; }
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
            TextDecorationLine mode;

            if (value.Is("none"))
                _line.Clear();
            else if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
            {
                _line.Clear();
                _line.Add(mode);
            }
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;
                var list = new List<TextDecorationLine>();

                foreach (var item in values)
                {
                    if (item is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                        list.Add(mode);
                    else
                        return false;
                }

                _line = list;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
