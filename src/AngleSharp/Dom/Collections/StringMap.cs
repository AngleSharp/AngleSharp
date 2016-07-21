namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a list of DOMTokens.
    /// </summary>
    sealed class StringMap : IStringMap
    {
        #region Fields

        readonly String _prefix;
        readonly Element _parent;

        #endregion

        #region ctor

        internal StringMap(String prefix, Element parent)
        {
            _prefix = prefix;
            _parent = parent;
        }

        #endregion

        #region Index

        public String this[String name]
        {
            get { return _parent.GetOwnAttribute(_prefix + Check(name)); }
            set { _parent.SetOwnAttribute(_prefix + Check(name), value); }
        }

        #endregion

        #region Methods

        public void Remove(String name)
        {
            if (Contains(name))
            {
                this[name] = null;
            }
        }

        public Boolean Contains(String name)
        {
            return _parent.HasOwnAttribute(_prefix + Check(name));
        }

        #endregion

        #region Helper

        static String Check(String name)
        {
            if (name.StartsWith(TagNames.Xml, StringComparison.OrdinalIgnoreCase))
                throw new DomException(DomError.Syntax);

            if (name.IndexOf(Symbols.Semicolon) >= 0)
                throw new DomException(DomError.Syntax);

            for (var i = 0; i < name.Length; i++)
            {
                if (name[i].IsUppercaseAscii())
                    throw new DomException(DomError.Syntax);
            }

            return name;
        }

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<KeyValuePair<String, String>> GetEnumerator()
        {
            foreach (var attr in _parent.Attributes)
            {
                if (attr.NamespaceUri == null && attr.Name.StartsWith(_prefix, StringComparison.OrdinalIgnoreCase))
                {
                    var name = attr.Name.Remove(0, _prefix.Length);
                    var value = attr.Value;
                    yield return new KeyValuePair<String, String>(name, value);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
