namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of media elements.
    /// </summary>
    sealed class MediaList : IMediaList
    {
        #region Fields

        readonly List<CssMedium> _media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new MediaList.
        /// </summary>
        internal MediaList()
        {
            _media = new List<CssMedium>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the medium item at the specified index.
        /// </summary>
        /// <param name="index">Index into the collection.</param>
        /// <returns>The medium at the index-th position in the MediaList, or null if that is not a valid index.</returns>
        public String this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= _media.Count)
                    return null;

                return _media[index].CssText;
            }
        }

        /// <summary>
        /// Gets the number of media in the list. 
        /// </summary>
        public Int32 Length
        {
            get { return _media.Count; }
        }

        /// <summary>
        /// Gets or sets the parsable textual representation of the media list. This is a comma-separated list of media.
        /// </summary>
        public String MediaText
        {
            get 
            {
                var parts = new String[_media.Count];

                for (int i = 0; i < _media.Count; i++)
                    parts[i] = _media[i].CssText;

                return String.Join(", ", parts); 
            }
            set
            {
                _media.Clear();

                foreach (var medium in CssParser.ParseMediaList(value))
                {
                    if (medium == null)
                        throw new DomException(DomError.Syntax);

                    _media.Add(medium);
                }
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates the list of contained media against the provided rendering device.
        /// </summary>
        /// <param name="device">The current rendering device.</param>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public Boolean Validate(RenderDevice device)
        {
            foreach (var media in _media)
            {
                if (media.Validate(device) == false)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Adds the medium newMedium to the end of the list.
        /// If the newMedium is already used, it is first removed.
        /// </summary>
        /// <param name="newMedium">The new medium to add.</param>
        public void Add(String newMedium)
        {
            var medium = CssParser.ParseMedium(newMedium);

            if (medium == null)
                throw new DomException(DomError.Syntax);

            _media.Add(medium);
        }

        /// <summary>
        /// Deletes the medium indicated by oldMedium from the list.
        /// </summary>
        /// <param name="oldMedium">The medium to delete in the media list.</param>
        public void Remove(String oldMedium)
        {
            var medium = CssParser.ParseMedium(oldMedium);

            if (medium == null)
                throw new DomException(DomError.Syntax);

            foreach (var item in _media)
            {
                if (item.Equals(medium))
                {
                    _media.Remove(item);
                    return;
                }
            }

            throw new DomException(DomError.NotFound);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Adds the given medium to the list of media.
        /// </summary>
        /// <param name="medium">The medium to add.</param>
        internal void Add(CssMedium medium)
        {
            _media.Add(medium);
        }

        /// <summary>
        /// Imports the media from the given list.
        /// Clears the existing media.
        /// </summary>
        /// <param name="list">The list to import.</param>
        internal void Import(MediaList list)
        {
            _media.Clear();
            _media.AddRange(list._media);
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
                yield return medium.CssText;
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
