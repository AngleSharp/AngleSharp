namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/box-shadow
    /// </summary>
    sealed class CSSBoxShadowProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<BoxShadowMode> _creator;
        static readonly NoneBoxShadowMode _none = new NoneBoxShadowMode();
        BoxShadowMode _mode;

        #endregion

        #region ctor

        static CSSBoxShadowProperty()
        {
            _creator = new ValueConverter<BoxShadowMode>();
            _creator.AddStatic("none", _none, exclusive: true);
            _creator.AddConstructed<NormalBoxShadowMode>();
            _creator.AddConstructed<InsetBoxShadowMode>("inset");
            _creator.AddMultiple<MultiBoxShadowMode>();
        }

        public CSSBoxShadowProperty()
            : base(PropertyNames.BOX_SHADOW)
        {
            _mode = _none;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            BoxShadowMode mode;

            if (_creator.TryCreate(value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class BoxShadowMode
        {
            //TODO Add members that make sense
        }

        sealed class NoneBoxShadowMode : BoxShadowMode
        {
        }

        sealed class InsetBoxShadowMode : BoxShadowMode
        {
            Length _offsetX;
            Length _offsetY;
            Length _blurRadius;
            Length _spreadRadius;
            Color _color;

            public InsetBoxShadowMode(Length offsetX, Length offsetY, Length? blurRadius = null, Length? spreadRadius = null, Color? color = null)
            {
                _offsetX = offsetX;
                _offsetY = offsetY;
                _blurRadius = blurRadius ?? Length.Zero;
                _spreadRadius = spreadRadius ?? Length.Zero;
                _color = color ?? Color.Black;
            }
        }

        sealed class NormalBoxShadowMode : BoxShadowMode
        {
            Length _offsetX;
            Length _offsetY;
            Length _blurRadius;
            Length _spreadRadius;
            Color _color;

            public NormalBoxShadowMode(Length offsetX, Length offsetY, Length? blurRadius = null, Length? spreadRadius = null, Color? color = null)
            {
                _offsetX = offsetX;
                _offsetY = offsetY;
                _blurRadius = blurRadius ?? Length.Zero;
                _spreadRadius = spreadRadius ?? Length.Zero;
                _color = color ?? Color.Black;
            }
        }

        sealed class MultiBoxShadowMode : BoxShadowMode
        {
            List<BoxShadowMode> _shadows;

            public MultiBoxShadowMode(List<BoxShadowMode> shadows)
            {
                _shadows = shadows;
            }

        }

        #endregion
    }
}
