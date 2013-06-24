using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    [DOM("CSSMediaRule")]
    public sealed class CSSMediaRule : CSSConditionRule
    {
        #region Constants

        internal const String RuleName = "media";

        #endregion

        #region Members

        MediaList media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS @media rule.
        /// </summary>
        internal CSSMediaRule()
        {
            media = new MediaList();
            _type = CssRule.Media;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the text of the media condition.
        /// </summary>
        [DOM("conditionText")]
        public override String ConditionText
        {
            get { return media.MediaText; }
            set { media.MediaText = value; }
        }

        /// <summary>
        /// Gets a list of media types for this rule.
        /// </summary>
        [DOM("media")]
        public MediaList Media
        {
            get { return media; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Format("@media {0} {{{1}{2}}}", media.MediaText, Environment.NewLine, CssRules.ToCss());
        }

        #endregion
    }
}
