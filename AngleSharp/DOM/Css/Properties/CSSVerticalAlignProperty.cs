namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/vertical-align
    /// </summary>
    sealed class CSSVerticalAlignProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, VerticalAlignMode> modes = new Dictionary<String, VerticalAlignMode>(StringComparer.OrdinalIgnoreCase);
        VerticalAlignMode _mode;

        #endregion

        #region ctor

        static CSSVerticalAlignProperty()
        {
            modes.Add("baseline", new BaselineCoordinateMode());
            modes.Add("sub", new SubCoordinateMode());
            modes.Add("super", new SuperCoordinateMode());
            modes.Add("text-top", new TextTopCoordinateMode());
            modes.Add("text-bottom", new TextBottomCoordinateMode());
            modes.Add("middle", new MiddleCoordinateMode());
            modes.Add("top", new TopCoordinateMode());
            modes.Add("bottom", new BottomCoordinateMode());
        }

        public CSSVerticalAlignProperty()
            : base(PropertyNames.VERTICAL_ALIGN)
        {
            _inherited = false;
            _mode = modes["baseline"];
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSLengthValue)
            {
                var length = (CSSLengthValue)value;
                _mode = new AbsoluteVerticalAlignMode(length.Length);
            }
            else if (value is CSSPercentValue)
                _mode = new RelativeVerticalAlignMode(((CSSPercentValue)value).Value);
            else if (value == CSSNumberValue.Zero)
                _mode = new AbsoluteVerticalAlignMode(Length.Zero);
            else if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                VerticalAlignMode mode;

                if (!modes.TryGetValue(ident.Value, out mode))
                    return false;

                _mode = mode;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes
        
        abstract class VerticalAlignMode
        {
            //TODO Add members that make sense
        }

        class BaselineCoordinateMode : VerticalAlignMode
        {
        }

        class SubCoordinateMode : VerticalAlignMode
        {
        }

        class SuperCoordinateMode : VerticalAlignMode
        {
        }

        class TextTopCoordinateMode : VerticalAlignMode
        {
        }

        class TextBottomCoordinateMode : VerticalAlignMode
        {
        }

        class MiddleCoordinateMode : VerticalAlignMode
        {
        }

        class TopCoordinateMode : VerticalAlignMode
        {
        }

        class BottomCoordinateMode : VerticalAlignMode
        {
        }

        class RelativeVerticalAlignMode : VerticalAlignMode
        {
            Single _value;

            public RelativeVerticalAlignMode(Single value)
            {
                _value = value;
            }
        }

        class AbsoluteVerticalAlignMode : VerticalAlignMode
        {
            Length _value;

            public AbsoluteVerticalAlignMode(Length value)
            {
                _value = value;
            }
        }

        #endregion
    }
}
