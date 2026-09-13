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

        // The exact string our own write to the content attribute is dispatching and whose echo
        // has not arrived yet, or null when no write of ours is still in flight. Correctness does
        // not rest on this: the sync hands us the attribute's current value, so parsing is always
        // right. What the field buys is skipping that parse for the one value we know reproduces
        // the set we already hold - and only while it is still the one in flight, so that a later
        // write repeating our text is not mistaken for the echo.
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
            if (_writtenValue is not null)
            {
                var isEcho = String.Equals(value, _writtenValue, StringComparison.Ordinal);

                // Our write has stopped being in flight either way: this is its echo, or another
                // value reached the attribute first and the echo can never arrive. Anything from
                // here on that repeats our text is somebody else's write and has to be parsed.
                _writtenValue = null;

                if (isEcho)
                {
                    // DOM 7.1: the attribute change steps set the token set from the parsed
                    // attribute value. This one is our own serialization coming back, so the set
                    // already matches it. Skipping is only an optimisation, never a recursion guard
                    // - Update does not raise Changed, so an unguarded re-entry terminates on its
                    // own. What it buys is the split plus its substrings, and keeping the token
                    // instances the caller handed us instead of replacing them with equal copies.
                    return;
                }
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
            // it exactly as they see a setAttribute. Only the echo of this very value is skipped,
            // and only while it is still the one in flight.
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
                // an outer write whose echo is still outstanding must go on recognizing it once the
                // inner one has unwound. If that echo had already arrived, previous is null and the
                // outer write stays finished, which is what keeps the case above from reopening.
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
