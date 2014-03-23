namespace AngleSharp.Css
{
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// The class that is responsible for book-keeping information
    /// about the current CSS value that is been build.
    /// </summary>
    [DebuggerStepThrough]
    sealed class CssValueBuilder
    {
        #region Fields

        Boolean _fraction;
        Stack<FunctionBuffer> _functions;
        List<CSSValue> _values;

        static readonly CSSValue separator = new CSSValue();

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS value builder instance.
        /// </summary>
        public CssValueBuilder()
        {
            _functions = new Stack<FunctionBuffer>();
            _values = new List<CSSValue>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if the current value is in fraction mode.
        /// </summary>
        public Boolean IsFraction
        {
            get { return _fraction; }
            set { _fraction = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Adds a function to the current value with the given name.
        /// </summary>
        /// <param name="name">The name of the function.</param>
        public void AddFunction(String name)
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
        public void AddValue(CSSValue value)
        {
            if (_fraction)
            {
                _fraction = false;

                if (_values.Count != 0)
                {
                    if (_values[_values.Count - 1] is CSSValueList)
                    {
                        if (((CSSValueList)_values[_values.Count - 1]).Separator == ValueListSeparator.Slash)
                        {
                            var list = (CSSValueList)_values[_values.Count - 1];
                            list.List.Add(value);
                        }

                        return;
                    }
                    else
                    {
                        var list = new CSSValueList { Separator = ValueListSeparator.Slash };
                        list.List.Add(_values[_values.Count - 1]);
                        _values.RemoveAt(_values.Count - 1);
                        list.List.Add(value);
                        value = list;
                    }
                }
            }

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
            else
                _values.Add(separator);
        }

        /// <summary>
        /// Resets the builder for reprocessing.
        /// </summary>
        public void Reset()
        {
            _fraction = false;
            _functions.Clear();
            _values.Clear();
        }

        /// <summary>
        /// Converts the current stage to a CSSValue.
        /// </summary>
        /// <returns>The instance of a value.</returns>
        public CSSValue ToValue()
        {
            while (_functions.Count > 0)
                CloseFunction();

            while (_values.Count != 0 && _values[_values.Count - 1] == separator)
                _values.RemoveAt(_values.Count - 1);

            while (_values.Count != 0 && _values[0] == separator)
                _values.RemoveAt(0);

            if (IsList())
                return CreateList();
            else if (_values.Count != 0)
                return Create();

            return null;
        }

        #endregion

        #region Helpers

        Boolean IsList()
        {
            for (int i = 1; i < _values.Count - 1; i++)
            {
                if (_values[i] == separator)
                    return true;
            }

            return false;
        }

        CSSValueList CreateList()
        {
            var value = new CSSValueList { Separator = ValueListSeparator.Comma };
            var start = 0;

            for (int i = 0; i <= _values.Count; i++)
            {
                if (i == _values.Count || _values[i] == separator)
                {
                    if (i != start)
                        value.List.Add(Create(start, i));

                    start = i + 1;
                }
            }

            return value;
        }

        /// <summary>
        /// Creates a value from the given index.
        /// </summary>
        /// <param name="start">The inclusive start index.</param>
        /// <returns>The created value (primitive or list).</returns>
        CSSValue Create(Int32 start = 0)
        {
            return Create(start, _values.Count);
        }

        /// <summary>
        /// Creates a value from the given span.
        /// </summary>
        /// <param name="start">The inclusive start index.</param>
        /// <param name="end">The exclusive end index.</param>
        /// <returns>The created value (primitive or list).</returns>
        CSSValue Create(Int32 start, Int32 end)
        {
            if (end - start != 1)
            {
                var list = new CSSValueList { Separator = ValueListSeparator.Space };

                for (var i = start; i < end; i++)
                    list.List.Add(_values[i]);

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

            String _name;
            List<CSSValue> _arguments;

            #endregion

            #region ctor

            public FunctionBuffer(String name)
            {
                _arguments = new List<CSSValue>();
                _name = name;
            }

            #endregion

            #region Properties

            public Int32 StartIndex
            {
                get;
                set;
            }

            public List<CSSValue> Arguments
            {
                get { return _arguments; }
            }

            #endregion

            #region Methods

            public CSSValue ToValue()
            {
                return CSSFunction.Create(_name, _arguments);
            }

            #endregion
        }

        #endregion
    }
}
