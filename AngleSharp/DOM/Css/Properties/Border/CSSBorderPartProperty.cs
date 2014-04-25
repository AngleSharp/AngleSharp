namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// The abstract base class for border properties.
    /// </summary>
    public abstract class CSSBorderPartProperty : CSSProperty
    {
        #region Fields

        Length _width;
        Color _color;
        LineStyle _style;

        #endregion

        #region ctor

        protected CSSBorderPartProperty(String name)
            : base(name)
        {
            _inherited = false;
            _width = Length.Medium;
            _color = Color.Transparent;
            _style = LineStyle.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the width of the given border property.
        /// </summary>
        public Length Width
        {
            get { return _width; }
        }

        /// <summary>
        /// Gets the color of the given border property.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        /// <summary>
        /// Gets the style of the given border property.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var width = Length.Medium;
            var color = Color.Transparent;
            var style = LineStyle.None;

            if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length > 3)
                    return false;

                Length? w = null;
                Color? c = null;
                LineStyle? s = null;

                foreach (var v in values)
                {
                    if (!w.HasValue)
                    {
                        w = value.ToBorderWidth();

                        if (w.HasValue)
                        {
                            width = w.Value;
                            continue;
                        }
                    }

                    if (!c.HasValue)
                    {
                        c = value.ToColor();

                        if (c.HasValue)
                        {
                            color = c.Value;
                            continue;
                        }
                    }

                    if (!s.HasValue)
                    {
                        s = value.ToLineStyle();

                        if (s.HasValue)
                        {
                            style = s.Value;
                            continue;
                        }
                    }

                    return false;
                }
            }
            else
            {
                var w = value.ToBorderWidth();
                var c = value.ToColor();
                var s = value.ToLineStyle();

                if (w.HasValue)
                    width = w.Value;
                else if (c.HasValue)
                    color = c.Value;
                else if (s.HasValue)
                    style = s.Value;
                else
                    return false;
            }

            _width = width;
            _color = color;
            _style = style;
            return true;
        }

        #endregion
    }
}
