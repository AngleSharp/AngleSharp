using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a list of media elements.
    /// </summary>
    [DOM("MediaList")]
    public sealed class MediaList : IEnumerable<String>
    {
        #region Static

        readonly static string[] ALLOWED = {
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
            // Intended for speech synthesizers.
            "aural",
            // Suitable for all devices.
            "all"
        };

        #endregion

        #region Members

        StringCollection _media;
        String _buffer;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new MediaList.
        /// </summary>
        internal MediaList()
        {
            _buffer = String.Empty;
            _media = new StringCollection();
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

                return _media[index];
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
            get { return _buffer; }
            set
            {
                _buffer = string.Empty;
                _media.Clear();

                if (!string.IsNullOrEmpty(value))
                {
                    var entries = value.SplitCommas();

                    for (int i = 0; i < entries.Length; i++)
                    {
                        if (!CheckSyntax(entries[i]))
                            throw new DOMException(ErrorCode.SyntaxError);   
                    }
                    
                    for (int i = 0; i < entries.Length; i++)
                        AppendMedium(entries[i]);
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
        /// <returns>The current list of media.</returns>
        [DOM("appendMedium")]
        public MediaList AppendMedium(String newMedium)
        {
            if (!CheckSyntax(newMedium))
                throw new DOMException(ErrorCode.SyntaxError);

            if (!_media.Contains(newMedium))
            {
                _media.Add(newMedium);
                _buffer += (String.IsNullOrEmpty(_buffer) ? String.Empty : ",") + newMedium;
            }

            return this;
        }

        /// <summary>
        /// Deletes the medium indicated by oldMedium from the list.
        /// </summary>
        /// <param name="oldMedium">The medium to delete in the media list.</param>
        /// <returns>The current list of media.</returns>
        [DOM("deleteMedium")]
        public MediaList DeleteMedium(String oldMedium)
        {
            if (!_media.Contains(oldMedium))
                throw new DOMException(ErrorCode.ItemNotFound);

            _media.Remove(oldMedium);

            if (_buffer.StartsWith(oldMedium))
                _buffer.Remove(0, oldMedium.Length + 1);
            else
                _buffer.Replace("," + oldMedium, String.Empty);

            return this;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks the syntax of the medium in the given language.
        /// </summary>
        /// <param name="medium">The medium string to check.</param>
        /// <returns>True if the syntax is OK, otherwise false.</returns>
        Boolean CheckSyntax(String medium)
        {
            if (string.IsNullOrEmpty(medium))
                return false;

            return true;
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
                yield return medium;
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
