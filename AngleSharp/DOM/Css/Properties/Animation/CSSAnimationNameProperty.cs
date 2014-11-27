namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/CSS/animation-name
    /// </summary>
    sealed class CSSAnimationNameProperty : CSSProperty, ICssAnimationNameProperty
    {
        #region Fields

        readonly List<String> _names;

        #endregion

        #region ctor

        internal CSSAnimationNameProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.AnimationName, rule)
        {
            _names = new List<String>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the names of the animations to trigger.
        /// </summary>
        public IEnumerable<String> Names
        {
            get { return _names; }
        }

        #endregion

        #region Methods

        public void SetNames(IEnumerable<String> names)
        {
            _names.Clear();
            _names.AddRange(names);
        }

        internal override void Reset()
        {
            _names.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeOne(Keywords.None, new String[0]).Or(this.TakeList(this.WithIdentifier())).TryConvert(value, SetNames);
        }

        #endregion
    }
}
