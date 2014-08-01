namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    sealed class CSSMediaRule : CSSConditionRule, ICssMediaRule
    {
        #region Fields

        readonly MediaList _media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS @media rule with a new media list.
        /// </summary>
        internal CSSMediaRule()
            : this(new MediaList())
        {
        }

        /// <summary>
        /// Creates a new CSS @media rule with the given media list.
        /// </summary>
        /// <param name="media">The media list.</param>
        internal CSSMediaRule(MediaList media)
        {
            _media = media;
            _type = CssRuleType.Media;
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
            var newRule = rule as CSSMediaRule;
            _media.Import(newRule._media);
        }

        internal override Boolean IsValid(IWindow window)
        {
            //TODO
            return true;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@media {0} {{{1}{2}}}", _media.MediaText, Environment.NewLine, Rules.ToCss());
        }

        #endregion
    }
}
