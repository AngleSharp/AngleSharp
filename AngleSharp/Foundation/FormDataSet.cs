using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AngleSharp
{
    /// <summary>
    /// Bundles information stored in HTML forms.
    /// </summary>
    sealed class FormDataSet : IEnumerable<String>
    {
        #region Members

        List<FormDataSetEntry> _entries;

        #endregion

        #region ctor

        public FormDataSet()
        {
            _entries = new List<FormDataSetEntry>();
        }

        #endregion

        #region Properties

        public Object this[String name]
        {
            get { return _entries.Where(m => m.Name == name).Select(m => m.Value).FirstOrDefault(); }
            set { Append(name, value, "Text"); }
        }

        public Object this[String name, String type]
        {
            get { return _entries.Where(m => m.Name == name && m.Type == type).Select(m => m.Value).FirstOrDefault(); }
            set { Append(name, value, type); }
        }

        #endregion

        #region Methods

        public void Append(String name, Object value, String type)
        {
            if (String.Compare(type, "File", StringComparison.OrdinalIgnoreCase) == 0 || String.Compare(type, "Textarea", StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);

                if(value is String)
                    value = Normalize((String)value);
            }

            _entries.Add(new FormDataSetEntry { Name = name, Value = value, Type = type });
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Replaces every occurrence of a "CR" (U+000D) character not followed by a "LF" (U+000A)
        /// character, and every occurrence of a "LF" (U+000A) character not preceded by a "CR"
        /// (U+000D) character, by a two-character string consisting of a U+000D CARRIAGE RETURN
        /// "CRLF" (U+000A) character pair.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized string.</returns>
        String Normalize(String value)
        {
            var lines = value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return String.Join("\r\n", lines);
        }

        #endregion

        #region Entry Class

        /// <summary>
        /// Encapsulates the data contained in an entry.
        /// </summary>
        public class FormDataSetEntry
        {
            /// <summary>
            /// Gets or sets the entry's name.
            /// </summary>
            public String Name
            {
                get;
                set;
            }
            
            /// <summary>
            /// Gets or sets the entry's value.
            /// </summary>
            public Object Value
            {
                get;
                set;
            }

            /// <summary>
            /// Gets or sets the entry's type.
            /// </summary>
            public String Type
            {
                get;
                set;
            }
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over all entry names.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return _entries.Select(m => m.Name).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
