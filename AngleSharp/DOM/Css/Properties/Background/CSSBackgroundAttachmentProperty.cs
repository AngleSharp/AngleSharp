namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// </summary>
    sealed class CSSBackgroundAttachmentProperty : CSSProperty, ICssBackgroundAttachmentProperty
    {
        #region Fields

        readonly List<BackgroundAttachment> _attachments;

        #endregion

        #region ctor

        internal CSSBackgroundAttachmentProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BackgroundAttachment, rule)
        {
            _attachments = new List<BackgroundAttachment>();
            Reset();
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

        public void SetAttachments(IEnumerable<BackgroundAttachment> attachments)
        {
            _attachments.Clear();
            _attachments.AddRange(attachments);
        }

        internal override void Reset()
        {
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
            return this.TakeList(this.WithBackgroundAttachment()).TryConvert(value, SetAttachments);
        }

        #endregion
    }
}
