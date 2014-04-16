namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform-style
    /// </summary>
    sealed class CSSTransformStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TransformStyle> modes = new Dictionary<String, TransformStyle>(StringComparer.OrdinalIgnoreCase);
        TransformStyle _mode;

        #endregion

        #region ctor

        static CSSTransformStyleProperty()
        {
            modes.Add("flat", TransformStyle.Flat);
            modes.Add("preserve-3d", TransformStyle.Preserve3d);
        }

        public CSSTransformStyleProperty()
            : base(PropertyNames.TransformStyle)
        {
            _mode = TransformStyle.Flat;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            TransformStyle mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum TransformStyle
        {
            /// <summary>
            /// Indicates that the children of the element are lying
            /// in the plane of the element itself.
            /// </summary>
            Flat,
            /// <summary>
            /// Indicates that the children of the element should
            /// be positioned in the 3D-space.
            /// </summary>
            Preserve3d
        }

        #endregion
    }
}
