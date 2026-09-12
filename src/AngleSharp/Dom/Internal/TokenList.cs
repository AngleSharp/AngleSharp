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

        // Set while our own write to the content attribute is being dispatched, so that the
        // attribute change steps coming back around do not re-parse what we just serialized.
        private Boolean _writing;

        #endregion

        #region Events

        public event Action<String>? Changed;

        #endregion

        #region ctor

        internal TokenList(String? value)
        {
            _tokens = [];
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

        public void Update(String? value)
        {
            if (_writing)
            {
                // DOM 7.1: the attribute change steps set the token set from the parsed attribute
                // value. This one is our own serialization coming back, so the set already matches
                // it - re-parsing would only cost an allocation and throw away the identity of the
                // tokens the caller just handed us.
                return;
            }

            _tokens.Clear();

            if (value is { Length: > 0 })
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

        private void RaiseChanged()
        {
            var handler = Changed;

            if (handler is null)
            {
                return;
            }

            // The write runs the full attribute change steps, so observers and mutation records see
            // it exactly as they see a setAttribute. Only the sync back into this list is skipped.
            var wasWriting = _writing;
            _writing = true;

            try
            {
                handler.Invoke(ToString());
            }
            finally
            {
                // Restore rather than clear: an observer is free to write this very list again, and
                // the outer write must stay guarded once the inner one has unwound.
                _writing = wasWriting;
            }
        }

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
