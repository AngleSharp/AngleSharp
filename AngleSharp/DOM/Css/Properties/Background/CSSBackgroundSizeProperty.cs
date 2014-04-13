namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-size
    /// </summary>
    sealed class CSSBackgroundSizeProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, SizeMode> _modes = new Dictionary<String, SizeMode>(StringComparer.OrdinalIgnoreCase);
        SizeMode _size;

        #endregion

        #region ctor

        static CSSBackgroundSizeProperty()
        {
            _modes.Add("auto", new AutoSizeMode());
            _modes.Add("cover", new CoverSizeMode());
            _modes.Add("contain", new ContainSizeMode());
        }

        public CSSBackgroundSizeProperty()
            : base(PropertyNames.BackgroundSize)
        {
            _inherited = false;
            _size = _modes["auto"];
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSValueList)
                return CheckList((CSSValueList)value);
            else if (!CheckSingle(value) && value != CSSValue.Inherit)
                return false;

            return true;
        }

        static SizeMode Check(CSSValue value)
        {
            var calc = value.ToCalc();

            if (calc != null)
                return new CalcSizeMode(calc);
            else if (value is CSSIdentifierValue)
            {
                SizeMode size;

                if (_modes.TryGetValue(((CSSIdentifierValue)value).Value, out size))
                    return size;
            }

            return null;
        }

        Boolean CheckSingle(CSSValue value)
        {
            var size = Check(value);

            if (size == null)
                return false;

            _size = size;
            return true;
        }

        Boolean CheckList(CSSValueList values)
        {
            var sizes = new List<SizeMode>();

            for (int i = 0; i < values.Length; i++)
            {
                var size = Check(values[i]);

                if (size == null)
                    return false;

                sizes.Add(size);

                if (++i < values.Length && values[i] != CSSValue.Separator)
                    return false;
            }

            _size = new MultipleSizeMode(sizes);
            return true;
        }

        #endregion

        #region Modes

        abstract class SizeMode
        {
            //TODO Add Members that make sense
        }

        /// <summary>
        /// A list of sizes defining the background-size properties of every
        /// given image.
        /// </summary>
        sealed class MultipleSizeMode : SizeMode
        {
            List<SizeMode> _sizes;

            public MultipleSizeMode(List<SizeMode> sizes)
            {
                _sizes =_sizes;
            }
        }

        /// <summary>
        /// A value that scales the background image to the specified value in
        /// the corresponding dimension. Negative values are not allowed.
        /// </summary>
        sealed class CalcSizeMode : SizeMode
        {
            CSSCalcValue _calc;

            public CalcSizeMode(CSSCalcValue calc)
            {
                _calc = calc;
            }
        }

        /// <summary>
        /// The auto keyword that scales the background image in the corresponding
        /// direction such that its intrinsic proportion is maintained.
        /// </summary>
        sealed class AutoSizeMode : SizeMode
        {

        }

        /// <summary>
        /// This keyword specifies that the background image should be scaled to
        /// be as small as possible while ensuring both its dimensions are greater
        /// than or equal to the corresponding dimensions of the background
        /// positioning area.
        /// </summary>
        sealed class CoverSizeMode : SizeMode
        {

        }

        /// <summary>
        /// This keyword specifies that the background image should be scaled to
        /// be as large as possible while ensuring both its dimensions are less
        /// than or equal to the corresponding dimensions of the background
        /// positioning area.
        /// </summary>
        sealed class ContainSizeMode : SizeMode
        {

        }

        #endregion
    }
}
