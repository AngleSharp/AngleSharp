using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents a CSS @media rule.
    /// </summary>
    public sealed class CSSMediaRule : CSSConditionRule
    {
        #region Members

        MediaList media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS @media rule.
        /// </summary>
        public CSSMediaRule()
        {
            media = new MediaList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a list of media types for this rule.
        /// </summary>
        public MediaList Media
        {
            get { return media; }
        }

        #endregion
    }
}
