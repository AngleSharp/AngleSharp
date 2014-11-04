namespace AngleSharp.DOM.Css
{
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
        public IEnumerable<Object> Images
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

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var items = (value as CSSValueList ?? new CSSValueList(value)).ToList();
            var images = new CSSValueList();
            var positions = new CSSValueList();
            var sizes = new CSSValueList();
            var repeats = new CSSValueList();
            var attachments = new CSSValueList();
            var origins = new CSSValueList();
            var clips = new CSSValueList();
            CSSValue color = null;

            foreach (var list in items)
            {
                CSSValue image = null, position = null, size = null, repeat = null,
                         attachment = null, origin = null, clip = null;

                if (color != null)
                    return false;

                if (images.Length > 0)
                {
                    images.Add(CSSValue.Separator);
                    positions.Add(CSSValue.Separator);
                    sizes.Add(CSSValue.Separator);
                    repeats.Add(CSSValue.Separator);
                    attachments.Add(CSSValue.Separator);
                    origins.Add(CSSValue.Separator);
                    clips.Add(CSSValue.Separator);
                }

                for (int j = 0; j < list.Length; j++)
                {
                    var item = list[j];

                    if (_position.CanStore(item, ref position))
                    {
                        if (j + 1 == list.Length)
                            continue;

                        var pack = new CSSValueList();
                        pack.Add(position);
                        pack.Add(list[j + 1]);

                        if (_position.CanTake(pack))
                        {
                            positions.Add(position);
                            position = list[++j];
                        }

                        if (j + 1 < list.Length && list[j + 1] == CSSValue.Delimiter)
                        {
                            j += 2;

                            if (j < list.Length && _size.CanStore(list[j], ref size))
                            {
                                if (j + 1 == list.Length)
                                    continue;

                                pack = new CSSValueList();
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

                        var pack = new CSSValueList();
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

                images.Add(image ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.None)));
                positions.Add(position ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.Center)));
                sizes.Add(size ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.Auto)));
                repeats.Add(repeat ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.Repeat)));
                attachments.Add(attachment ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.Scroll)));
                origins.Add(origin ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.BorderBox)));
                clips.Add(clip ?? new CSSPrimitiveValue(new CssIdentifier(Keywords.BorderBox)));
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
