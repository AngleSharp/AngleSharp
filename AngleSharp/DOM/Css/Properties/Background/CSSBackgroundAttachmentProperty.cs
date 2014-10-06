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

        List<BackgroundAttachment> _attachments;

        #endregion

        #region ctor

        internal CSSBackgroundAttachmentProperty()
            : base(PropertyNames.BackgroundAttachment)
        {
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

        protected override void Reset()
        {
            if (_attachments == null)
                _attachments = new List<BackgroundAttachment>();
            else
                _attachments.Clear();

            _attachments.Add(BackgroundAttachment.Scroll);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var list = value as CSSValueList ?? new CSSValueList(value);
            var attachments = new List<BackgroundAttachment>();

            for (int i = 0; i < list.Length; i++)
            {
                var attachment = list[i].ToBackgroundAttachment();

                if (attachment == null)
                    return false;

                attachments.Add(attachment.Value);

                if (++i < list.Length && list[i] != CSSValue.Separator)
                    return false;
            }

            _attachments = attachments;
            return true;
        }

        #endregion
    }
}
