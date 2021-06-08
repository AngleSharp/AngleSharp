namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a list of query parameters.
    /// </summary>
    [DomName("URLSearchParams")]
    [DomExposed("Window")]
    [DomExposed("Worker")]
    public class UrlSearchParams
    {
        #region Fields

        private readonly List<KeyValuePair<String, String>> _values;
        private Boolean _reflow;

        #endregion

        #region Events

        internal event Action<String>? Changed;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        [DomConstructor]
        public UrlSearchParams() => _values = new();

        internal UrlSearchParams(Url parent) : this(parent.Query ?? String.Empty) => Changed += qs =>
        {
            _reflow = true;
            parent.Query = qs;
            _reflow = false;
        };

        /// <summary>
        /// Creates a new instance filled from the provided string.
        /// </summary>
        [DomConstructor]
        public UrlSearchParams(String init) : this() => ChangeTo(init);

        #endregion

        #region Methods

        internal void Reset() => _values.Clear();

        internal void ChangeTo(String query)
        {
            if (!_reflow)
            {
                Reset();

                foreach (var pair in query.Split('&'))
                {
                    var kvp = pair.Split('=');

                    if (kvp.Length > 1)
                    {
                        Append(Decode(kvp[0]), Decode(kvp[1]));
                    }
                    else
                    {
                        Append(Decode(pair), String.Empty);
                    }
                }
            }
        }

        /// <summary>
        /// Appends another value for the given search param name.
        /// </summary>
        /// <param name="name">The name of the param.</param>
        /// <param name="value">The value of the param.</param>
        [DomName("append")]
        public void Append(String name, String value)
        {
            _values.Add(new KeyValuePair<String, String>(name, value));
            RaiseChanged();
        }

        /// <summary>
        /// Deletes the values of the search param name.
        /// </summary>
        /// <param name="name">The name of the param.</param>
        [DomName("delete")]
        public void Delete(String name)
        {
            _values.RemoveAll(p => p.Key == name);
            RaiseChanged();
        }

        /// <summary>
        /// Gets the first value of the given search param name, if any.
        /// </summary>
        /// <param name="name">The name of the param.</param>
        /// <returns>The value of the param, if any.</returns>
        [DomName("get")]
        public String? Get(String name) => _values.Find(p => p.Key == name).Value;

        /// <summary>
        /// Gets all values for the given search param name.
        /// </summary>
        /// <param name="name">The name of the param.</param>
        /// <returns>The list with all stored values.</returns>
        [DomName("getAll")]
        public String[] GetAll(String name) => _values.FindAll(p => p.Key == name).Select(m => m.Value).ToArray();

        /// <summary>
        /// Checks if a search param with the given name exists.
        /// </summary>
        /// <param name="name">The name of the param.</param>
        /// <returns>True if such a param exists, otherwise false.</returns>
        [DomName("has")]
        public Boolean Has(String name) => _values.Any(p => p.Key == name);

        /// <summary>
        /// Sets the given search param.
        /// </summary>
        /// <param name="name">The name of the param.</param>
        /// <param name="value">The value of the param.</param>
        [DomName("set")]
        public void Set(String name, String value)
        {
            if (Has(name))
            {
                var index = _values.FindIndex(p => p.Key == name);
                Delete(name);
                _values.Insert(index, new KeyValuePair<String, String>(name, value));
            }
            else
            {
                Append(name, value);
            }

            RaiseChanged();
        }

        /// <summary>
        /// Sorts the underlying list.
        /// </summary>
        [DomName("sort")]
        public void Sort()
        {
            _values.Sort((a, b) => a.Key.CompareTo(b.Key));
            RaiseChanged();
        }

        /// <inheritdoc />
        public override String ToString() => String.Join("&", _values.Select(p => $"{Encode(p.Key)}={Encode(p.Value)}"));

        #endregion

        #region Helpers

        private static String Encode(String value)
        {
            var sb = StringBuilderPool.Obtain();

            foreach (var chr in value)
            {
                if (chr.IsAlphanumericAscii() || chr is Symbols.Minus or Symbols.Underscore or Symbols.ExclamationMark or Symbols.Asterisk or Symbols.ReverseSolidus or Symbols.RoundBracketOpen or Symbols.RoundBracketClose or Symbols.Tilde)
                {
                    sb.Append(chr);
                }
                else
                {
                    sb.Append(Symbols.Percent);
                    sb.Append(((Int32)chr).ToString("X2"));
                }
            }

            return sb.ToPool();
        }

        private static String Decode(String value)
        {
            var sb = StringBuilderPool.Obtain();

            for (var i = 0; i < value.Length; i++)
            {
                var chr = value[i];

                if (chr is Symbols.Percent && i + 2 < value.Length)
                {
                    var count = value.Substring(i + 1, 2).FromHex();
                    sb.Append((Char)count);
                    i += 2;
                }
                else
                {
                    sb.Append(chr);
                }
            }

            return sb.ToPool();
        }

        private void RaiseChanged() => Changed?.Invoke(ToString());

        #endregion
    }
}
