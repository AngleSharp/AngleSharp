namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/background
    /// </summary>
    sealed class CSSBackgroundProperty : CSSShorthandProperty, ICssBackgroundProperty
    {
        #region Fields

        readonly CSSBackgroundImageProperty _image;
        readonly CSSBackgroundPositionProperty _position;
        readonly CSSBackgroundSizeProperty _size;
        readonly CSSBackgroundRepeatProperty _repeat;
        readonly CSSBackgroundAttachmentProperty _attachment;
        readonly CSSBackgroundOriginProperty _origin;
        readonly CSSBackgroundClipProperty _clip;
        readonly CSSBackgroundColorProperty _color;

        #endregion

        #region ctor

        internal CSSBackgroundProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Background, rule, PropertyFlags.Animatable)
        {
            _image = Get<CSSBackgroundImageProperty>();
            _position = Get<CSSBackgroundPositionProperty>();
            _size = Get<CSSBackgroundSizeProperty>();
            _repeat = Get<CSSBackgroundRepeatProperty>();
            _attachment = Get<CSSBackgroundAttachmentProperty>();
            _origin = Get<CSSBackgroundOriginProperty>();
            _clip = Get<CSSBackgroundClipProperty>();
            _color = Get<CSSBackgroundColorProperty>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the background image property.
        /// </summary>
        public IEnumerable<IImageSource> Images
        {
            get { return _image.Images; }
        }

        /// <summary>
        /// Gets the value of the background position property.
        /// </summary>
        public IEnumerable<Point> Positions
        {
            get { return _position.Positions; }
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

        /// <summary>
        /// Gets if the background image should be covered, i.e. min(100%).
        /// </summary>
        public IEnumerable<Boolean> IsCovered
        {
            get { return _size.IsCovered; }
        }

        /// <summary>
        /// Gets if the background image should be contained, i.e. max(100%).
        /// </summary>
        public IEnumerable<Boolean> IsContained
        {
            get { return _size.IsContained; }
        }

        /// <summary>
        /// Gets the widths and heights of the background image, if specified.
        /// </summary>
        public IEnumerable<Point> Sizes
        {
            get { return _size.Sizes; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            var items = (value as CssValueList ?? new CssValueList(value)).ToList();
            var images = new CssValueList();
            var positions = new CssValueList();
            var sizes = new CssValueList();
            var repeats = new CssValueList();
            var attachments = new CssValueList();
            var origins = new CssValueList();
            var clips = new CssValueList();
            ICssValue color = null;

            foreach (var list in items)
            {
                ICssValue image = null, position = null, size = null, repeat = null,
                         attachment = null, origin = null, clip = null;

                if (color != null)
                    return false;

                if (images.Length > 0)
                {
                    images.Add(CssValue.Separator);
                    positions.Add(CssValue.Separator);
                    sizes.Add(CssValue.Separator);
                    repeats.Add(CssValue.Separator);
                    attachments.Add(CssValue.Separator);
                    origins.Add(CssValue.Separator);
                    clips.Add(CssValue.Separator);
                }

                for (int j = 0; j < list.Length; j++)
                {
                    var item = list[j];

                    if (_position.CanStore(item, ref position))
                    {
                        if (j + 1 == list.Length)
                            continue;

                        var pack = new CssValueList();
                        pack.Add(position);
                        pack.Add(list[j + 1]);

                        if (_position.CanTake(pack))
                        {
                            positions.Add(position);
                            position = list[++j];
                        }

                        if (j + 1 < list.Length && list[j + 1] == CssValue.Delimiter)
                        {
                            j += 2;

                            if (j < list.Length && _size.CanStore(list[j], ref size))
                            {
                                if (j + 1 == list.Length)
                                    continue;

                                pack = new CssValueList();
                                pack.Add(size);
                                pack.Add(list[j + 1]);

                                if (_size.CanTake(pack))
                                {
                                    sizes.Add(size);
                                    size = list[++j];
                                }
                            }
                            else
                                return false;
                        }
                    }
                    else if (_repeat.CanStore(item, ref repeat))
                    {
                        if (j + 1 == list.Length)
                            continue;

                        var pack = new CssValueList();
                        pack.Add(repeat);
                        pack.Add(list[j + 1]);

                        if (_repeat.CanTake(pack))
                        {
                            repeats.Add(repeat);
                            repeat = list[++j];
                        }
                    }
                    else if (!_image.CanStore(item, ref image) && !_attachment.CanStore(item, ref attachment) && 
                             !_origin.CanStore(item, ref origin) && !_clip.CanStore(item, ref clip) && !_color.CanStore(item, ref color))
                        return false;
                }

                images.Add(image ?? new CssIdentifier(Keywords.None));
                positions.Add(position ?? new CssIdentifier(Keywords.Center));
                sizes.Add(size ?? new CssIdentifier(Keywords.Auto));
                repeats.Add(repeat ?? new CssIdentifier(Keywords.Repeat));
                attachments.Add(attachment ?? new CssIdentifier(Keywords.Scroll));
                origins.Add(origin ?? new CssIdentifier(Keywords.BorderBox));
                clips.Add(clip ?? new CssIdentifier(Keywords.BorderBox));
            }

            return _image.TrySetValue(images.Reduce()) && _position.TrySetValue(positions.Reduce()) &&
                   _repeat.TrySetValue(repeats.Reduce()) && _attachment.TrySetValue(attachments.Reduce()) &&
                   _origin.TrySetValue(origins.Reduce()) && _size.TrySetValue(sizes.Reduce()) &&
                   _clip.TrySetValue(clips) && _color.TrySetValue(color);
        }

        internal override String SerializeValue(IEnumerable<CSSProperty> properties)
        {
            if (!IsComplete(properties))
                return String.Empty;

            var values = new List<String>();
            values.Add(_image.SerializeValue());
            values.Add(_position.SerializeValue());

            if (_size.HasValue)
            {
                values.Add("/");
                values.Add(_size.SerializeValue());
            }

            values.Add(_repeat.SerializeValue());
            values.Add(_attachment.SerializeValue());
            values.Add(_clip.SerializeValue());
            values.Add(_origin.SerializeValue());
            values.Add(_color.SerializeValue());
            values.RemoveAll(m => String.IsNullOrEmpty(m));

            return String.Join(" ", values);
        }

        #endregion
    }
}
