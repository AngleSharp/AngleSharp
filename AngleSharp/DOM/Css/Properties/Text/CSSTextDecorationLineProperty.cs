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

        public CSSTextDecorationLineProperty()
            : base(PropertyNames.TextDecorationLine)
        {
            _line = new List<TextDecorationLine>();
            _inherited = false;
        }

        #endregion

        #region Methods

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

        #region Mode

        enum TextDecorationLine
        {
            /// <summary>
            /// Each line of text is underlined.
            /// </summary>
            Underline,
            /// <summary>
            /// Each line of text has a line above it.
            /// </summary>
            Overline,
            /// <summary>
            /// Each line of text has a line through the middle.
            /// </summary>
            LineThrough,
            /// <summary>
            /// The text blinks (alternates between visible and invisible). Conforming user agents may simply not blink the text.
            /// </summary>
            Blink
        }

        #endregion
    }
}
