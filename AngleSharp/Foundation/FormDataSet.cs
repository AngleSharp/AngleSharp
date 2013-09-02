using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AngleSharp
{
    class FormDataSet : IEnumerable<String>
    {
        List<FormDataSetEntry> _entries;

        public FormDataSet()
        {
            _entries = new List<FormDataSetEntry>();
        }

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
            var sb = new StringBuilder(value);
            sb.Replace("\r\n", "\n").Replace("\r", "\n").Replace("\n", "\r\n");
            return sb.ToString();
        }

        public class FormDataSetEntry
        {
            public String Name
            {
                get;
                set;
            }

            public Object Value
            {
                get;
                set;
            }

            public String Type
            {
                get;
                set;
            }
        }

        public IEnumerator<String> GetEnumerator()
        {
            return _entries.Select(m => m.Name).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
