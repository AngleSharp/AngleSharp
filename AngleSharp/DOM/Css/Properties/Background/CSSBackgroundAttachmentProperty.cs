namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// </summary>
    sealed class CSSBackgroundAttachmentProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, Attachment> _modes = new Dictionary<String, Attachment>(StringComparer.OrdinalIgnoreCase);
        List<Attachment> _attachments;

        #endregion

        #region ctor

        static CSSBackgroundAttachmentProperty()
        {
            _modes.Add("fixed", Attachment.Fixed);
            _modes.Add("local", Attachment.Local);
            _modes.Add("scroll", Attachment.Scroll);
        }

        public CSSBackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
            _attachments = new List<Attachment>();
            _attachments.Add(Attachment.Scroll);
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList ?? new CSSValueList(value);
            var attachments = new List<Attachment>();

            for (int i = 0; i < list.Length; i+=2)
            {
                Attachment attachment;

                if (list[i] is CSSIdentifierValue && !_modes.TryGetValue(((CSSIdentifierValue)list[i]).Value, out attachment))
                    attachments.Add(attachment);
                else
                    return false;

                if (i + 1 < list.Length && list[i + 1] != CSSValue.Separator)
                    return false;
            }

            _attachments = attachments;
            return true;
        }

        #endregion

        #region Attachment

        enum Attachment
        {
            /// <summary>
            /// This keyword means that the background is fixed with regard to the viewport.
            /// Even if an element has a scrolling mechanism, a ‘fixed’ background doesn't
            /// move with the element.
            /// </summary>
            Fixed,
            /// <summary>
            /// This keyword means that the background is fixed with regard to the element's
            /// contents: if the element has a scrolling mechanism, the background scrolls
            /// with the element's contents, and the background painting area and background
            /// positioning area are relative to the scrollable area of the element rather
            /// than to the border framing them.
            /// </summary>
            Local,
            /// <summary>
            /// This keyword means that the background is fixed with regard to the element
            /// itself and does not scroll with its contents. (It is effectively attached
            /// to the element's border.)
            /// </summary>
            Scroll
        }

        #endregion
    }
}
