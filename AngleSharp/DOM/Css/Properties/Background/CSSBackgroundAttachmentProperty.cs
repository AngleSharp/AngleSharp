namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// </summary>
    sealed class CSSBackgroundAttachmentProperty : CSSProperty, ICssBackgroundAttachmentProperty
    {
        #region Fields

        static readonly Dictionary<String, BackgroundAttachment> _modes = new Dictionary<String, BackgroundAttachment>(StringComparer.OrdinalIgnoreCase);
        List<BackgroundAttachment> _attachments;

        #endregion

        #region ctor

        static CSSBackgroundAttachmentProperty()
        {
            _modes.Add(Keywords.Fixed, BackgroundAttachment.Fixed);
            _modes.Add(Keywords.Local, BackgroundAttachment.Local);
            _modes.Add(Keywords.Scroll, BackgroundAttachment.Scroll);
        }

        internal CSSBackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
            _attachments = new List<BackgroundAttachment>();
            _attachments.Add(BackgroundAttachment.Scroll);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration with the desired attachment settings.
        /// </summary>
        public IEnumerable<BackgroundAttachment> Attachments
        {
            get { return _attachments; }
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
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList ?? new CSSValueList(value);
            var attachments = new List<BackgroundAttachment>();

            for (int i = 0; i < list.Length; i++)
            {
                BackgroundAttachment attachment;

                if (list[i] is CSSIdentifierValue && _modes.TryGetValue(((CSSIdentifierValue)list[i]).Value, out attachment))
                    attachments.Add(attachment);
                else
                    return false;

                if (++i < list.Length && list[i] != CSSValue.Separator)
                    return false;
            }

            _attachments = attachments;
            return true;
        }

        #endregion
    }
}
