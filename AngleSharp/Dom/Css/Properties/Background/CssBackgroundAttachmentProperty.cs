namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background-attachment
    /// </summary>
    sealed class CssBackgroundAttachmentProperty : CssProperty
    {
        #region Fields

        internal static readonly BackgroundAttachment Default = BackgroundAttachment.Scroll;
        internal static readonly IValueConverter<BackgroundAttachment> SingleConverter = Map.BackgroundAttachments.ToConverter();
        internal static readonly IValueConverter<BackgroundAttachment[]> Converter = SingleConverter.FromList();
        readonly List<BackgroundAttachment> _attachments;

        #endregion

        #region ctor

        internal CssBackgroundAttachmentProperty(CssStyleDeclaration rule)
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
            _attachments.Add(Default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetAttachments);
        }

        #endregion
    }
}
