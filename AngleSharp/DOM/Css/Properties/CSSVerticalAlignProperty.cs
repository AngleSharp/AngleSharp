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
            if (value is CSSLength)
            {
                var length = (CSSLength)value;
                _mode = new AbsoluteVerticalAlignMode(length.Value, length.PrimitiveType);
            }
            else if (value is CSSIdentifier)
            {
                var ident = (CSSIdentifier)value;
                VerticalAlignMode mode;

                if (!modes.TryGetValue(ident.Identifier, out mode))
                    return false;

                _mode = mode;
            }
            else if (value is CSSPercent)
                _mode = new RelativeVerticalAlignMode(((CSSPercent)value).Value);
            else if (value is CSSNumber && ((CSSNumber)value).Value == 0f)
                _mode = new AbsoluteVerticalAlignMode(0f, CssUnit.Px);
            else if (value == CSSValue.Inherit)
                return true;
            else
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
            Single _value;
            CssUnit _unit;

            public AbsoluteVerticalAlignMode(Single value, CssUnit unit)
            {
                _value = value;
                _unit = unit;
            }
        }

        #endregion
    }
}
