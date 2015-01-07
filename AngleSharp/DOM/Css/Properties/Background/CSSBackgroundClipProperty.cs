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

        internal static readonly BoxModel Default = BoxModel.BorderBox;
        internal static readonly IValueConverter<BoxModel> SingleConverter = Map.BoxModels.ToConverter();
        internal static readonly IValueConverter<BoxModel[]> Converter = SingleConverter.FromList();
        readonly List<BoxModel> _clips;

        #endregion

        #region ctor

        internal CSSBackgroundClipProperty(CssStyleDeclaration rule)
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

        public void SetClips(IEnumerable<BoxModel> clips)
        {
            _clips.Clear();
            _clips.AddRange(clips);
        }

        internal override void Reset()
        {
            _clips.Clear();
            _clips.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetClips);
        }

        #endregion
    }
}
