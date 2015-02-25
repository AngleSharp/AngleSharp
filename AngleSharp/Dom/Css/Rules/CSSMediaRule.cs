namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    sealed class CssMediaRule : CssConditionRule, ICssMediaRule
    {
        #region Fields

        readonly MediaList _media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS @media rule with a new media list.
        /// </summary>
        internal CssMediaRule()
            : this(new MediaList())
        {
        }

        /// <summary>
        /// Creates a new CSS @media rule with the given media list.
        /// </summary>
        /// <param name="media">The media list.</param>
        internal CssMediaRule(MediaList media)
            : base(CssRuleType.Media)
        {
            _media = media;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the media condition.
        /// </summary>
        public String ConditionText
        {
            get { return _media.MediaText; }
            set { _media.MediaText = value; }
        }

        /// <summary>
        /// Gets a list of media types for this rule.
        /// </summary>
        public IMediaList Media
        {
            get { return _media; }
        }

        #endregion

        #region Internal Methods

        protected override void ReplaceWith(ICssRule rule)
        {
            base.ReplaceWith(rule);
            var newRule = rule as CssMediaRule;
            _media.Import(newRule._media);
        }

        internal override Boolean IsValid(RenderDevice device)
        {
            return _media.Validate(device);
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        protected override String ToCss()
        {
            return String.Concat("@media ", _media.MediaText, " ", Rules.ToCssBlock());
        }

        #endregion
    }
}
