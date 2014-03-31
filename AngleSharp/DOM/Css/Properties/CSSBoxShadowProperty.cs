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
            _creator.AddStatic("none", _none);
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

        class NoneBoxShadowMode : BoxShadowMode
        {
        }

        class InsetBoxShadowMode : BoxShadowMode
        {
            Length offsetX;
            Length offsetY;
            Length blurRadius;
            Length spreadRadius;
            Color color;

            public InsetBoxShadowMode(Length offsetX, Length offsetY, Length? blurRadius = null, Length? spreadRadius = null, Color? color = null)
            {
                this.offsetX = offsetX;
                this.offsetY = offsetY;
                this.blurRadius = blurRadius ?? Length.Zero;
                this.spreadRadius = spreadRadius ?? Length.Zero;
                this.color = color ?? Color.Black;
            }
        }

        class NormalBoxShadowMode : BoxShadowMode
        {
            Length offsetX;
            Length offsetY;
            Length blurRadius;
            Length spreadRadius;
            Color color;

            public NormalBoxShadowMode(Length offsetX, Length offsetY, Length? blurRadius = null, Length? spreadRadius = null, Color? color = null)
            {
                this.offsetX = offsetX;
                this.offsetY = offsetY;
                this.blurRadius = blurRadius ?? Length.Zero;
                this.spreadRadius = spreadRadius ?? Length.Zero;
                this.color = color ?? Color.Black;
            }
        }

        class MultiBoxShadowMode : BoxShadowMode
        {
            BoxShadowMode top;
            BoxShadowMode right;
            BoxShadowMode bottom;
            BoxShadowMode left;

            public MultiBoxShadowMode(List<BoxShadowMode> modes)
            {
                var count = modes.Count;
                top = modes[0];
                right = modes[1 % count];
                bottom = modes[2 % count];
                left = modes[3 % count];
            }

        }

        #endregion
    }
}
