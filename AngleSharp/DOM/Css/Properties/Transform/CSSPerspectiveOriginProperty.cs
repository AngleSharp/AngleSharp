namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/perspective-origin
    /// </summary>
    sealed class CSSPerspectiveOriginProperty : CSSProperty
    {
        #region Fields

        static readonly RelativePositionMode _center = new RelativePositionMode(0.5f);
        PositionMode _x;
        PositionMode _y;

        #endregion

        #region ctor

        public CSSPerspectiveOriginProperty()
            : base(PropertyNames.PerspectiveOrigin)
        {
            _inherited = false;
            _x = _center;
            _y = _center;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;
            else if (value is CSSValueList)
                return SetXY((CSSValueList)value);
            
            return SetSingle(value);
        }

        Boolean SetSingle(CSSValue value)
        {
            var length = value.ToLength();

            if (length.HasValue)
            {
                _x = new AbsolutePositionMode(length.Value);
                _y = new AbsolutePositionMode(length.Value);
                return true;
            }
            else if (value is CSSPercentValue)
            {
                var scale = ((CSSPercentValue)value).Value;
                _x = new RelativePositionMode(scale);
                _y = new RelativePositionMode(scale);
                return true;
            }
            else if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                switch (ident.ToLower())
                {
                    case "left":
                        _x = new RelativePositionMode(0f);
                        _y = _center;
                        return true;

                    case "center":
                        _x = _center;
                        _y = _center;
                        return true;

                    case "right":
                        _x = new RelativePositionMode(1f);
                        _y = _center;
                        return true;

                    case "top":
                        _x = _center;
                        _y = new RelativePositionMode(0f);
                        return true;

                    case "bottom":
                        _x = _center;
                        _y = new RelativePositionMode(1f);
                        return true;
                }
            }

            return false;
        }

        Boolean SetXY(CSSValueList list)
        {
            if (list.Length == 2)
            {
                var x = GetMode(list[0], "left", "right");
                var index = 1;

                if (x == null)
                    index--;

                var y = GetMode(list[index], "top", "bottom");

                if (y != null && x == null)
                    x = GetMode(list[1], "left", "right");

                if (x != null && y != null)
                {
                    _x = x;
                    _y = y;
                }
            }

            return false;
        }

        static PositionMode GetMode(CSSValue value, String minIdentifier, String maxIdentifier)
        {
            var length = value.ToLength();

            if (length.HasValue)
                return new AbsolutePositionMode(length.Value);
            else if (value is CSSPercentValue)
                return new RelativePositionMode(((CSSPercentValue)value).Value);
            else if (value is CSSIdentifierValue)
            {
                var ident = ((CSSIdentifierValue)value).Value;

                if (minIdentifier.Equals(ident, StringComparison.OrdinalIgnoreCase))
                    return new RelativePositionMode(0f);
                else if (maxIdentifier.Equals(ident, StringComparison.OrdinalIgnoreCase))
                    return new RelativePositionMode(1f);
                else if ("center".Equals(ident, StringComparison.OrdinalIgnoreCase))
                    return _center;
            }

            return null;
        }

        #endregion

        #region Modes

        abstract class PositionMode
        {
            //TODO Add Members here
        }

        sealed class AbsolutePositionMode : PositionMode
        {
            Length _position;

            public AbsolutePositionMode(Length position)
            {
                _position = position;
            }
        }

        sealed class RelativePositionMode : PositionMode
        {
            Single _scale;

            public RelativePositionMode(Single scale)
            {
                _scale = scale;
            }
        }

        #endregion

    }
}
