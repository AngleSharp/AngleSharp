namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using AngleSharp.Dom.Css;

    /// <summary>
    /// The class that is responsible for book-keeping information
    /// about the current CSS value that is been build.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssValueBuilder
    {
        #region Fields

        readonly Stack<FunctionBuffer> _functions;
        readonly List<ICssValue> _values;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value builder instance.
        /// </summary>
        public CssValueBuilder()
        {
            _functions = new Stack<FunctionBuffer>();
            _values = new List<ICssValue>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the current value contains syntax errors.
        /// </summary>
        public Boolean IsFaulted
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a function to the current value with the given name.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        public void OpenFunction(String name)
        {
            var function = new FunctionBuffer(name);
            function.StartIndex = _values.Count;
            _functions.Push(function);
        }

        /// <summary>
        /// Adds the new value to the current value (or replaces it).
        /// </summary>
        /// <param name="value">The value to add.</param>
        /// <returns>The status.</returns>
        public void AddValue(ICssValue value)
        {
            _values.Add(value);
        }

        /// <summary>
        /// Closes the current function.
        /// </summary>
        public void CloseFunction()
        {
            NextArgument();
            AddValue(_functions.Pop().ToValue());
        }

        /// <summary>
        /// Inserts a delimiter / into the current value.
        /// </summary>
        public void InsertDelimiter()
        {
            _values.Add(CssValue.Delimiter);
        }

        /// <summary>
        /// Closes the current argument and appends another.
        /// </summary>
        public void NextArgument()
        {
            if (_functions.Count > 0)
            {
                var function = _functions.Peek();
                var value = Create(function.StartIndex);
                _values.RemoveRange(function.StartIndex, _values.Count - function.StartIndex);
                function.Arguments.Add(value);
                function.StartIndex = _values.Count;
            }
            else if (_values.Count == 0 || _values[_values.Count - 1] == CssValue.Separator)
                IsFaulted = true;
            else
                _values.Add(CssValue.Separator);
        }

        /// <summary>
        /// Resets the builder for reprocessing.
        /// </summary>
        public void Reset()
        {
            IsFaulted = false;
            _functions.Clear();
            _values.Clear();
        }

        /// <summary>
        /// Converts the current stage to a CSSValue.
        /// </summary>
        /// <returns>The instance of a value.</returns>
        public ICssValue ToValue()
        {
            if (IsFaulted == false)
            {
                while (_functions.Count > 0)
                    CloseFunction();

                return Create();
            }

            return null;
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Creates a value from the given index.
        /// </summary>
        /// <param name="start">The inclusive start index.</param>
        /// <returns>The created value (primitive or list).</returns>
        ICssValue Create(Int32 start = 0)
        {
            return Create(start, _values.Count);
        }

        /// <summary>
        /// Creates a value from the given span.
        /// </summary>
        /// <param name="start">The inclusive start index.</param>
        /// <param name="end">The exclusive end index.</param>
        /// <returns>The created value (primitive or list).</returns>
        ICssValue Create(Int32 start, Int32 end)
        {
            if (end - start != 1)
            {
                var list = new CssValueList();

                for (var i = start; i < end; i++)
                    list.Add(_values[i]);

                return list;
            }

            return _values[start];
        }

        #endregion

        #region Function Buffer

        /// <summary>
        /// A buffer for functions.
        /// </summary>
        sealed class FunctionBuffer
        {
            #region Fields

            readonly String _name;
            readonly List<ICssValue> _arguments;

            #endregion

            #region ctor

            public FunctionBuffer(String name)
            {
                _arguments = new List<ICssValue>();
                _name = name;
            }

            #endregion

            #region Properties

            public Int32 StartIndex
            {
                get;
                set;
            }

            public List<ICssValue> Arguments
            {
                get { return _arguments; }
            }

            #endregion

            #region Methods

            public ICssValue ToValue()
            {
                return new CssFunction(_name, _arguments);
            }

            #endregion
        }

        #endregion
    }
}
