namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CSSBackgroundProperty : CSSProperty, ICssBackgroundProperty
    {
        #region Fields

        CSSBackgroundImageProperty _image;
        CSSBackgroundPositionProperty _position;
        CSSBackgroundSizeProperty _size;
        CSSBackgroundRepeatProperty _repeat;
        CSSBackgroundAttachmentProperty _attachment;
        CSSBackgroundOriginProperty _origin;
        CSSBackgroundClipProperty _clip;
        CSSBackgroundColorProperty _color;

        #endregion

        #region ctor

        internal CSSBackgroundProperty()
            : base(PropertyNames.Background)
        {
            _image = new CSSBackgroundImageProperty();
            _position = new CSSBackgroundPositionProperty();
            _size = new CSSBackgroundSizeProperty();
            _repeat = new CSSBackgroundRepeatProperty();
            _attachment = new CSSBackgroundAttachmentProperty();
            _origin = new CSSBackgroundOriginProperty();
            _clip = new CSSBackgroundClipProperty();
            _color = new CSSBackgroundColorProperty();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the background image property.
        /// </summary>
        internal IEnumerable<CSSImageValue> Images
        {
            get { return _image.Images; }
        }

        /// <summary>
        /// Gets the value of the background position property.
        /// </summary>
        internal IEnumerable<Point2d> Position
        {
            get { return _position.Positions; }
        }

        /// <summary>
        /// Gets the value of the background size property.
        /// </summary>
        public CSSBackgroundSizeProperty Size
        {
            get { return _size; }
        }

        /// <summary>
        /// Gets the value of the horizontal repeat property.
        /// </summary>
        public IEnumerable<BackgroundRepeat> HorizontalRepeats
        {
            get { return _repeat.HorizontalRepeats; }
        }

        /// <summary>
        /// Gets the value of the vertical repeat property.
        /// </summary>
        public IEnumerable<BackgroundRepeat> VerticalRepeats
        {
            get { return _repeat.VerticalRepeats; }
        }

        /// <summary>
        /// Gets the value of the background attachment property.
        /// </summary>
        public IEnumerable<BackgroundAttachment> Attachments
        {
            get { return _attachment.Attachments; }
        }

        /// <summary>
        /// Gets the value of the background origin property.
        /// </summary>
        public IEnumerable<BoxModel> Origins
        {
            get { return _origin.Origins; }
        }

        /// <summary>
        /// Gets the value of the background clip property.
        /// </summary>
        public IEnumerable<BoxModel> Clips
        {
            get { return _clip.Clips; }
        }

        /// <summary>
        /// Gets the value of the background color property.
        /// </summary>
        public Color Color
        {
            get { return _color.Color; }
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
            if (value != CSSValue.Inherit)
            {
                var values = value as CSSValueList ?? new CSSValueList(value);
                var image = new CSSValueList();
                var position = new CSSValueList();
                var size = new CSSValueList();
                var repeat = new CSSValueList();
                var attachment = new CSSValueList();
                var origin = new CSSValueList();
                var clip = new CSSValueList();
                Color? color = null;
                var list = values.ToList();

                for (int i = 0; i < list.Count; i++)
                {
                    var entry = list[i];
                    var hasImage = false;
                    var hasPosition = false;
                    var hasRepeat = false;
                    var hasAttachment = false;
                    var hasBox = false;
                    var hasColor = i + 1 != list.Count;

                    for (int j = 0; j < entry.Length; j++)
                    {
                        if (!hasPosition && (entry[j].IsOneOf(Keywords.Top, Keywords.Left, Keywords.Center, Keywords.Bottom, Keywords.Right) || entry[j].AsCalc() != null))
                        {
                            hasPosition = true;
                            position.Add(entry[j]);

                            while (j + 1 < entry.Length && (entry[j + 1].IsOneOf(Keywords.Top, Keywords.Left, Keywords.Center, Keywords.Bottom, Keywords.Right) || entry[j + 1].AsCalc() != null))
                                position.Add(entry[++j]);

                            if (j + 1 < entry.Length && entry[j + 1] == CSSValue.Delimiter)
                            {
                                j += 2;

                                if (j < entry.Length && (entry[j].IsOneOf(Keywords.Auto, Keywords.Contain, Keywords.Cover) || entry[j].AsCalc() != null))
                                {
                                    size.Add(entry[j]);

                                    if (j + 1 < entry.Length && (entry[j + 1].Is(Keywords.Auto) || entry[j + 1].AsCalc() != null))
                                        size.Add(entry[++j]);
                                }
                                else
                                    return false;
                            }
                            else
                                size.Add(new CSSIdentifierValue(Keywords.Auto));

                            continue;
                        }

                        if (!hasImage && entry[j].AsImage() != null)
                        {
                            hasImage = true;
                            image.Add(entry[j]);
                        }
                        else if (!hasRepeat && entry[j].IsOneOf(Keywords.RepeatX, Keywords.RepeatY, Keywords.Repeat, Keywords.Space, Keywords.Round, Keywords.NoRepeat))
                        {
                            hasRepeat = true;
                            repeat.Add(entry[j]);

                            if (j + 1 < entry.Length && entry[j + 1].IsOneOf(Keywords.Repeat, Keywords.Space, Keywords.Round, Keywords.NoRepeat))
                                repeat.Add(entry[++j]);
                        }
                        else if (!hasAttachment && entry[j].IsOneOf(Keywords.Local, Keywords.Fixed, Keywords.Scroll))
                        {
                            hasAttachment = true;
                            attachment.Add(entry[j]);
                        }
                        else if (!hasBox && entry[j].ToBoxModel().HasValue)
                        {
                            hasBox = true;
                            origin.Add(entry[j]);

                            if (j + 1 < entry.Length && entry[j + 1].ToBoxModel().HasValue)
                                clip.Add(entry[++j]);
                            else
                                clip.Add(new CSSIdentifierValue(Keywords.BorderBox));
                        }
                        else
                        {
                            if (hasColor)
                                return false;

                            hasColor = true;
                            color = entry[j].ToColor();

                            if (color == null)
                                return false;
                        }
                    }

                    if (!hasImage)
                        image.Add(new CSSIdentifierValue(Keywords.None));

                    if (!hasPosition)
                    {
                        position.Add(new CSSIdentifierValue(Keywords.Center));
                        size.Add(new CSSIdentifierValue(Keywords.Auto));
                    }

                    if (!hasRepeat)
                        repeat.Add(new CSSIdentifierValue(Keywords.Repeat));

                    if (!hasAttachment)
                        attachment.Add(new CSSIdentifierValue(Keywords.Scroll));

                    if (!hasBox)
                    {
                        origin.Add(new CSSIdentifierValue(Keywords.BorderBox));
                        clip.Add(new CSSIdentifierValue(Keywords.BorderBox));
                    }

                    if (i + 1 < list.Count)
                    {
                        image.Add(CSSValue.Separator);
                        position.Add(CSSValue.Separator);
                        size.Add(CSSValue.Separator);
                        repeat.Add(CSSValue.Separator);
                        attachment.Add(CSSValue.Separator);
                        origin.Add(CSSValue.Separator);
                        clip.Add(CSSValue.Separator);
                    }
                }

                _image.Value = image;
                _position.Value = position;
                _repeat.Value = repeat;
                _attachment.Value = attachment;
                _origin.Value = origin;
                _size.Value = size;
                _clip.Value = clip;
                _color.Value = new CSSPrimitiveValue(color ?? Color.Transparent);
            }

            return true;
        }

        #endregion
    }
}
