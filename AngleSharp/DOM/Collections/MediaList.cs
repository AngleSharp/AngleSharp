namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of media elements.
    /// </summary>
    [DOM("MediaList")]
    public sealed class MediaList : IEnumerable<String>
    {
        #region Static

        readonly static String[] Allowed = {
            // Intended for television-type devices (low resolution, color, limited scrollability).
            "tv",
            // Intended for non-paged computer screens.
            "screen",
            // Intended for media using a fixed-pitch character grid, such as teletypes, terminals, or portable devices with limited display capabilities.
            "tty",
            // Intended for projectors.
            "projection",
            // Intended for handheld devices (small screen, monochrome, bitmapped graphics, limited bandwidth).
            "handheld",
            // Intended for paged, opaque material and for documents viewed on screen in print preview mode.
            "print",
            // Intended for braille tactile feedback devices.
            "braille",
            // Suitable for all devices.
            "all"
        };

        #endregion

        #region Fields

        /// <summary>
        /// Represents an empty media list.
        /// </summary>
        public static readonly MediaList Empty = new MediaList();

        List<CSSMedium> _media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new MediaList.
        /// </summary>
        internal MediaList()
        {
            _media = new List<CSSMedium>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the medium item at the specified index.
        /// </summary>
        /// <param name="index">Index into the collection.</param>
        /// <returns>The medium at the index-th position in the MediaList, or null if that is not a valid index.</returns>
        [DOM("item")]
        public String this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= _media.Count)
                    return null;

                return _media[index].ToCss();
            }
        }

        /// <summary>
        /// Gets the number of media in the list. 
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return _media.Count; }
        }

        /// <summary>
        /// Gets or sets the parsable textual representation of the media list. This is a comma-separated list of media.
        /// </summary>
        [DOM("mediaText")]
        public String MediaText
        {
            get 
            {
                var parts = new String[_media.Count];

                for (int i = 0; i < _media.Count; i++)
                    parts[i] = _media[i].ToCss();

                return String.Join(", ", parts); 
            }
            set
            {
                _media.Clear();

                foreach (var medium in CssParser.ParseMediaList(value))
                {
                    if (medium == null)
                        throw new DOMException(ErrorCode.SyntaxError);

                    _media.Add(medium);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds the medium newMedium to the end of the list.
        /// If the newMedium is already used, it is first removed.
        /// </summary>
        /// <param name="newMedium">The new medium to add.</param>
        [DOM("appendMedium")]
        public void AppendMedium(String newMedium)
        {
            var medium = CssParser.ParseMedium(newMedium);

            if (medium == null)
                throw new DOMException(ErrorCode.SyntaxError);

            _media.Add(medium);
        }

        /// <summary>
        /// Deletes the medium indicated by oldMedium from the list.
        /// </summary>
        /// <param name="oldMedium">The medium to delete in the media list.</param>
        [DOM("deleteMedium")]
        public void DeleteMedium(String oldMedium)
        {
            var medium = CssParser.ParseMedium(oldMedium);

            if (medium == null)
                throw new DOMException(ErrorCode.SyntaxError);

            foreach (var item in _media)
            {
                if (item.Equals(medium))
                {
                    _media.Remove(item);
                    return;
                }
            }

            throw new DOMException(ErrorCode.ItemNotFound);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Adds the given medium to the list of media.
        /// </summary>
        /// <param name="medium">The medium to add.</param>
        internal void Add(CSSMedium medium)
        {
            _media.Add(medium);
        }

        #endregion

        #region IEnumerable implementation

        /// <summary>
        /// Returns an enumerator that iterates through the media in the list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the mediums.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            foreach (var medium in _media)
                yield return medium.ToCss();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the media in the list.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the mediums.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
