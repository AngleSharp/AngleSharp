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
    public class MediaList : DOMCollection, IEnumerable<string>
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

        StringCollection media;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new MediaList.
        /// </summary>
        public MediaList()
        {
            media = new StringCollection();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the medium item at the specified index.
        /// </summary>
        /// <param name="index">Index into the collection.</param>
        /// <returns>The medium at the index-th position in the MediaList, or null if that is not a valid index.</returns>
        public string this[int index]
        {
            get
            {
                if (index < 0 || index >= media.Count)
                    return null;

                return media[index];
            }
        }

        /// <summary>
        /// Gets the number of media in the list. 
        /// </summary>
        public int Length
        {
            get { return media.Count; }
        }

        /// <summary>
        /// Gets or sets the parsable textual representation of the media list. This is a comma-separated list of media.
        /// </summary>
        public string MediaText
        {
            get
            {
                var sb = new StringBuilder();

                if (media.Count > 0)
                {
                    sb.Append(media[0]);

                    for (int i = 1; i < media.Count; i++)
                        sb.Append(',').Append(media[i]);
                }

                return sb.ToString();
            }
            set
            {
                var entries = value.SplitCommas();

                foreach(var entry in entries)
                    if (!CheckSyntax(entry))
                        throw new DOMException(ErrorCode.SyntaxError);

                media.Clear();

                foreach (var entry in entries)
                    AppendMedium(entry);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks the syntax of the medium in the given language.
        /// </summary>
        /// <param name="medium">The medium string to check.</param>
        /// <returns>True if the syntax is OK, otherwise false.</returns>
        protected virtual bool CheckSyntax(string medium)
        {
            if (string.IsNullOrEmpty(medium))
                return false;

            return true;
        }

        /// <summary>
        /// Adds the medium newMedium to the end of the list.
        /// If the newMedium is already used, it is first removed.
        /// </summary>
        /// <param name="newMedium">The new medium to add.</param>
        /// <returns>The current list of media.</returns>
        public MediaList AppendMedium(string newMedium)
        {
            if (!CheckSyntax(newMedium))
                throw new DOMException(ErrorCode.SyntaxError);

            if(media.Contains(newMedium))
                media.Remove(newMedium);

            media.Add(newMedium);
            return this;
        }

        /// <summary>
        /// Deletes the medium indicated by oldMedium from the list.
        /// </summary>
        /// <param name="oldMedium">The medium to delete in the media list.</param>
        /// <returns>The current list of media.</returns>
        public MediaList DeleteMedium(string oldMedium)
        {
            if (!media.Contains(oldMedium))
                throw new DOMException(ErrorCode.ItemNotFound);

            media.Remove(oldMedium);
            return this;
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<string> GetEnumerator()
        {
            foreach (var medium in media)
                yield return medium;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)media).GetEnumerator();
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an HTML-code representation of the medialist.
        /// </summary>
        /// <returns>There is no HTML code to return.</returns>
        public override string ToHtml()
        {
            return string.Empty;
        }

        #endregion
    }
}
