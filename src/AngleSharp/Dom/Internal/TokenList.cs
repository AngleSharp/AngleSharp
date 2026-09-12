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

        // The exact string our own write to the content attribute is currently dispatching, or
        // null when we are not writing. Guarding on the value rather than on being inside the
        // dispatch matters: an observer is free to write a different value to the same attribute
        // while our write is being delivered, and that one has to be parsed like anyone else's.
        private String? _writtenValue;

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
            if (_writtenValue is not null && String.Equals(value, _writtenValue, StringComparison.Ordinal))
            {
                // DOM 7.1: the attribute change steps set the token set from the parsed attribute
                // value. This one is our own serialization coming back, so the set already matches
                // it. Skipping is only an optimisation, not a recursion guard - Update never raises
                // Changed, so an unguarded re-entry terminates on its own. What it buys is the
                // split plus its substrings, and keeping the token instances the caller handed us
                // instead of replacing them with equal copies.
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
            // it exactly as they see a setAttribute. Only the sync of this very value back into
            // this list is skipped; a different value arriving meanwhile is somebody else's write.
            var value = ToString();
            var previous = _writtenValue;
            _writtenValue = value;

            try
            {
                handler.Invoke(value);
            }
            finally
            {
                // Restore rather than clear: an observer is free to write this very list again, and
                // the outer write must still recognize its own value once the inner one has unwound.
                _writtenValue = previous;
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
