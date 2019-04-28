namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Text;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A simple list of tokens that is immutable.
    /// </summary>
    class TokenList : ITokenList, IBindable
    {
        #region Fields

        private readonly List<String> _tokens;

        #endregion

        #region Events

        public event Action<String> Changed;

        #endregion

        #region ctor

        internal TokenList(String value)
        {
            _tokens = new List<String>();
            Update(value);
        }

        #endregion

        #region Index

        public String this[Int32 index] => _tokens[index];

        #endregion

        #region Properties

        public Int32 Length => _tokens.Count;

        #endregion

        #region Methods

        public void Update(String value)
        {
            _tokens.Clear();

            if (!String.IsNullOrEmpty(value))
            {
                var elements = value.SplitSpaces();

                for (var i = 0; i < elements.Length; i++)
                {
                    if (!_tokens.Contains(elements[i]))
                    {
                        _tokens.Add(elements[i]);
                    }
                }
            }
        }

        public Boolean Contains(String token) => _tokens.Contains(token);

        public void Remove(params String[] tokens)
        {
            var changed = false;

            foreach (var token in tokens)
            {
                if (_tokens.Contains(token))
                {
                    _tokens.Remove(token);
                    changed = true;
                }
            }

            if (changed)
            {
                RaiseChanged();
            }
        }

        public void Add(params String[] tokens)
        {
            var changed = false;

            foreach (var token in tokens)
            {
                if (!_tokens.Contains(token))
                {
                    _tokens.Add(token);
                    changed = true;
                }
            }

            if (changed)
            {
                RaiseChanged();
            }
        }

        public Boolean Toggle(String token, Boolean force = false)
        {
            var contains = _tokens.Contains(token);

            if (contains && force)
            {
                return true;
            }

            if (contains)
            {
                _tokens.Remove(token);
            }
            else
            {
                _tokens.Add(token);
            }

            RaiseChanged();
            return !contains;
        }

        #endregion

        #region Helper

        private void RaiseChanged() => Changed?.Invoke(ToString());

        #endregion

        #region IEnumerable Implementation

        public IEnumerator<String> GetEnumerator() => _tokens.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion

        #region String representation

        public override String ToString() => String.Join(" ", _tokens);

        #endregion
    }
}
