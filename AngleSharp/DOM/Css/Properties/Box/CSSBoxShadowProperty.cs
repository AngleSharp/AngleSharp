namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// </summary>
    public sealed class CSSBoxShadowProperty : CSSProperty
    {
        #region Fields

        List<BoxShadow> _shadows;

        #endregion

        #region ctor

        internal CSSBoxShadowProperty()
            : base(PropertyNames.BoxShadow)
        {
            _shadows = new List<BoxShadow>();
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("none"))
                _shadows.Clear();
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        Boolean Evaluate(CSSValueList values)
        {
            var shadows = new List<BoxShadow>();
            var items = values.ToList();
            
            foreach (var item in items)
            {
                if (item.Length < 2)
                    return false;

                var inset = item[0].Is("inset");
                var offset = inset ? 1 : 0;

                if (inset && item.Length < 3)
                    return false;

                var offsetX = item[offset++].ToLength();
                var offsetY = item[offset++].ToLength();

                if (!offsetX.HasValue || !offsetY.HasValue)
                    return false;

                var blurRadius = Length.Zero;
                var spreadRadius = Length.Zero;
                var color = Color.Black;

                if (item.Length > offset)
                {
                    var blur = item[offset].ToLength();

                    if (blur.HasValue)
                    {
                        blurRadius = blur.Value;
                        offset++;
                    }
                }

                if (item.Length > offset)
                {
                    var spread = item[offset].ToLength();

                    if (spread.HasValue)
                    {
                        spreadRadius = spread.Value;
                        offset++;
                    }
                }

                if (item.Length > offset)
                {
                    var col = item[offset].ToColor();

                    if (col.HasValue)
                    {
                        color = col.Value;
                        offset++;
                    }
                }

                if (item.Length > offset)
                    return false;

                shadows.Add(new BoxShadow(inset, offsetX.Value, offsetY.Value, blurRadius, spreadRadius, color));
            }

            _shadows = shadows;
            return true;
        }

        #endregion

        #region Modes

        sealed class BoxShadow
        {
            Boolean _inset;
            Length _offsetX;
            Length _offsetY;
            Length _blurRadius;
            Length _spreadRadius;
            Color _color;

            public BoxShadow(Boolean inset, Length offsetX, Length offsetY, Length blurRadius, Length spreadRadius, Color color)
            {
                _inset = inset;
                _offsetX = offsetX;
                _offsetY = offsetY;
                _blurRadius = blurRadius;
                _spreadRadius = spreadRadius;
                _color = color;
            }

            public Boolean IsInset
            {
                get { return _inset; }
            }
        }

        #endregion
    }
}
