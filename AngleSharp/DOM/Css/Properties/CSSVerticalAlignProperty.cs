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

        /// <summary>
        /// Aligns the baseline of the element with the baseline of its parent.
        /// The baseline of some replaced elements, like textarea is not specified
        /// by the HTML specification, meaning that their behavior with this keyword
        /// may change from one browser to the other.
        /// </summary>
        sealed class BaselineCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Aligns the baseline of the element with the subscript-baseline
        /// of its parent.
        /// </summary>
        sealed class SubCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Aligns the baseline of the element with the superscript-baseline
        /// of its parent.
        /// </summary>
        sealed class SuperCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Aligns the top of the element with the top of the parent
        /// element's font.
        /// </summary>
        sealed class TextTopCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Aligns the bottom of the element with the bottom of the parent
        /// element's font.
        /// </summary>
        sealed class TextBottomCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Aligns the middle of the element with the middle of lowercase
        /// letters in the parent.
        /// </summary>
        sealed class MiddleCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Align the top of the element and its descendants with the top
        /// of the entire line.
        /// </summary>
        sealed class TopCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Align the bottom of the element and its descendants with the
        /// bottom of the entire line.
        /// </summary>
        sealed class BottomCoordinateMode : VerticalAlignMode
        {
        }

        /// <summary>
        /// Like absolute values, with the percentage being a percent of the
        /// line-height property.
        /// </summary>
        sealed class RelativeVerticalAlignMode : VerticalAlignMode
        {
            Single _value;

            public RelativeVerticalAlignMode(Single value)
            {
                _value = value;
            }
        }

        /// <summary>
        /// Aligns the baseline of the element at the given length above
        /// the baseline of its parent.
        /// </summary>
        sealed class AbsoluteVerticalAlignMode : VerticalAlignMode
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
