namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-clip
    /// </summary>
    sealed class CSSBackgroundClipProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Clip> _modes = new Dictionary<String, Clip>(StringComparer.OrdinalIgnoreCase);
        List<Clip> _clips;

        #endregion

        #region ctor

        static CSSBackgroundClipProperty()
        {
            _modes.Add("border-box", Clip.BorderBox);
            _modes.Add("padding-box", Clip.PaddingBox);
            _modes.Add("content-box", Clip.ContentBox);
        }

        public CSSBackgroundClipProperty()
            : base(PropertyNames.BackgroundClip)
        {
            _inherited = false;
            _clips = new List<Clip>();
            _clips.Add(Clip.BorderBox);
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList ?? new CSSValueList(value);
            var clips = new List<Clip>();

            for (int i = 0; i < list.Length; i++)
            {
                Clip clip;

                if (list[i] is CSSIdentifierValue && !_modes.TryGetValue(((CSSIdentifierValue)list[i]).Value, out clip))
                    clips.Add(clip);
                else
                    return false;

                if (++i < list.Length && list[i] != CSSValue.Separator)
                    return false;
            }

            _clips = clips;
            return true;
        }

        #endregion

        #region Modes

        enum Clip
        {
            /// <summary>
            /// The background extends to the outside edge of the border (but underneath the border in z-ordering).
            /// </summary>
            BorderBox,
            /// <summary>
            /// No background is drawn below the border (background extends to the outside edge of the padding).
            /// </summary>
            PaddingBox,
            /// <summary>
            /// The background is painted within (clipped to) the content box.
            /// </summary>
            ContentBox,
        }

        #endregion
    }
}
