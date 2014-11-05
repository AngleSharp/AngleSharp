namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-clip
    /// </summary>
    sealed class CSSBackgroundClipProperty : CSSProperty, ICssBackgroundClipProperty
    {
        #region Fields

        List<BoxModel> _clips;

        #endregion

        #region ctor

        internal CSSBackgroundClipProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundClip, rule)
        {
            _clips = new List<BoxModel>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the desired clip settings.
        /// </summary>
        public IEnumerable<BoxModel> Clips
        {
            get { return _clips; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _clips.Clear();
            _clips.Add(BoxModel.BorderBox);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var list = value as CSSValueList ?? new CSSValueList(value);
            var clips = new List<BoxModel>();

            for (int i = 0; i < list.Length; i++)
            {
                var clip = list[i].ToBoxModel();

                if (!clip.HasValue)
                    return false;

                clips.Add(clip.Value);

                if (++i < list.Length && list[i] != CSSValue.Separator)
                    return false;
            }

            _clips = clips;
            return true;
        }

        #endregion
    }
}
