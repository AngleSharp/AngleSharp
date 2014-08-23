namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// </summary>
    public sealed class CSSFloatProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Floating> modes = new Dictionary<String, Floating>(StringComparer.OrdinalIgnoreCase);
        Floating _mode;

        #endregion

        #region ctor

        static CSSFloatProperty()
        {
            modes.Add(Keywords.None, Floating.None);
            modes.Add(Keywords.Left, Floating.Left);
            modes.Add(Keywords.Right, Floating.Right);
        }

        internal CSSFloatProperty()
            : base(PropertyNames.Float)
        {
            _mode = Floating.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the floating property.
        /// </summary>
        public Floating State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            Floating mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
