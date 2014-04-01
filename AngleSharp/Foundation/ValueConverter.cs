namespace AngleSharp
{
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class ValueConverter<T>
        where T : class
    {
        #region Fields

        List<ValueFactoryEntry> _calls;

        #endregion

        #region ctor

        public ValueConverter()
        {
            _calls = new List<ValueFactoryEntry>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to create an instance of T with the given value.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <param name="mode">The mode to create.</param>
        /// <returns>True if a mode could be created, otherwise false.</returns>
        public Boolean TryCreate(CSSValue value, out T mode)
        {
            foreach (var call in _calls)
            {
                mode = call.Create(value);

                if (mode != null)
                    return true;
            }

            mode = default(T);
            return false;
        }

        /// <summary>
        /// Adds a static object to the list of possible constructed
        /// results. The object is returned if the given trigger is found.
        /// </summary>
        /// <param name="trigger">The identifier to search.</param>
        /// <param name="instance">The instance to keep.</param>
        /// <param name="exclusive">Optional: Shall this element be exclusive (can only appear single).</param>
        public void AddStatic(String trigger, T instance, Boolean exclusive = false)
        {
            var entry = new StaticValueFactoryEntry(trigger, instance);
            entry.IsExclusive = exclusive;
            _calls.Add(entry);
        }

        /// <summary>
        /// Adds a type that is constructed to the list of possible
        /// results. The object is returned if it can be constructed.
        /// Optionally an identifier has to be found before the list of
        /// constructor arguments.
        /// </summary>
        /// <typeparam name="TType">The type of the result.</typeparam>
        /// <param name="trigger">The optional identifier.</param>
        public void AddConstructed<TType>(String trigger = null)
            where TType : T
        {
            var type = typeof(TType);
            var constructorInfo = type.GetDeclaredConstructor();
            var parameters = constructorInfo.GetParameters();
            var entry = new ConstructedValueFactoryEntry(trigger, objs => (TType)constructorInfo.Invoke(objs));
            entry.MaxParameters = parameters.Length;
            entry.MinParameters = parameters.Count(m => !m.IsOptional);

            foreach (var parameter in parameters)
                entry.Consider(parameter.ParameterType);

            _calls.Add(entry);
        }

        /// <summary>
        /// Adds the possibility of constructing an object based on
        /// all other possible results. The arguments have to be a list.
        /// </summary>
        /// <typeparam name="TType">The type of the result.</typeparam>
        /// <param name="separator">The optional list separator.</param>
        public void AddMultiple<TType>(CssValueListSeparator separator = CssValueListSeparator.Comma)
            where TType : T
        {
            var type = typeof(TType);
            var constructorInfo = type.GetDeclaredConstructor();
            var entry = new MultipleValueFactoryEntry(_calls, list => (TType)constructorInfo.Invoke(new Object[] { list }));
            entry.Separator = separator;
            _calls.Add(entry);
        }

        #endregion

        #region Nested

        abstract class ValueFactoryEntry
        {
            public Boolean IsExclusive
            {
                get;
                set;
            }

            public abstract T Create(CSSValue argument);
        }

        sealed class StaticValueFactoryEntry : ValueFactoryEntry
        {
            String _trigger;
            T _instance;

            public StaticValueFactoryEntry(String trigger, T instance)
            {
                _trigger = trigger;
                _instance = instance;
            }

            public override T Create(CSSValue argument)
            {
                if (argument is CSSIdentifierValue && ((CSSIdentifierValue)argument).Value.Equals(_trigger, StringComparison.OrdinalIgnoreCase))
                    return _instance;

                return null;
            }
        }

        sealed class ConstructedValueFactoryEntry : ValueFactoryEntry
        {
            String _trigger;
            Func<Object[], T> _constructor;
            List<Func<CSSValue, Object>> _validators;

            public ConstructedValueFactoryEntry(String trigger, Func<Object[], T> constructor)
            {
                _trigger = trigger;
                _constructor = constructor;
                _validators = new List<Func<CSSValue, Object>>();
                MinParameters = 0;
                MaxParameters = Int32.MaxValue;
            }

            public Int32 MinParameters
            {
                get;
                set;
            }

            public Int32 MaxParameters
            {
                get;
                set;
            }

            public void Consider(Type type)
            {
                if (type == typeof(Length) || type == typeof(Length?))
                    _validators.Add(value => Validate(value.ToLength()));
                else if (type == typeof(Color) || type == typeof(Color?))
                    _validators.Add(value => Validate(value.ToColor()));
                else if (type == typeof(Single) || type == typeof(Single?))
                    _validators.Add(value => Validate(value.ToNumber()));
                else if (type == typeof(Uri))
                    _validators.Add(value => value.ToUri());
                else if (type == typeof(String))
                    _validators.Add(value => value.ToContent());
                else
                    _validators.Add(value => value.GetType() == type ? value : null);
            }
            
            public override T Create(CSSValue argument)
            {
                var arguments = argument as CSSValueList;

                if (arguments == null && MinParameters == 1)
                {
                    var parameter = _validators[0](argument);

                    if (parameter != null)
                    {
                        var parameters = new Object[MaxParameters];
                        parameters[0] = parameter;
                        return _constructor(parameters);
                    }
                }

                if (arguments != null && arguments.Separator == CssValueListSeparator.Space)
                {
                    int index = 0;

                    if (!String.IsNullOrEmpty(_trigger) && (arguments[0] is CSSIdentifierValue == false || !((CSSIdentifierValue)arguments[index++]).Value.Equals(_trigger, StringComparison.OrdinalIgnoreCase)))
                        return null;

                    if (arguments.Length - index < MinParameters || arguments.Length - index > MaxParameters)
                        return null;

                    var parameters = new Object[MaxParameters];

                    for (int i = index, j = 0; i < arguments.Length; j++)
                    {
                        if (j == _validators.Count)
                            return null;

                        if ((parameters[j] = _validators[j](arguments[i])) == null)
                        {
                            if (j < MinParameters)
                                return null;

                            continue;
                        }

                        i++;
                    }

                    return _constructor(parameters);
                }

                return null;
            }

            Object Validate<TTarget>(TTarget? result)
                where TTarget : struct
            {
                return result.HasValue ? (Object)result.Value : null;
            }
        }

        sealed class MultipleValueFactoryEntry : ValueFactoryEntry
        {
            List<ValueFactoryEntry> _source;
            Func<List<T>, T> _constructor;

            public MultipleValueFactoryEntry(List<ValueFactoryEntry> source, Func<List<T>, T> constructor)
            {
                _source = source;
                _constructor = constructor;
                Separator = CssValueListSeparator.Comma;
            }

            public CssValueListSeparator Separator
            {
                get;
                set;
            }
            
            public override T Create(CSSValue argument)
            {
                var arguments = argument as CSSValueList;

                if (arguments != null && arguments.Separator == Separator)
                {
                    var parameters = new List<T>();

                    foreach (var arg in arguments.List)
                    {
                        for (int i = 0; i < _source.Count; i++)
                        {
                            if (_source[i] == this || _source[i].IsExclusive)
                                continue;

                            var result = _source[i].Create(arg);

                            if (result != null)
                            {
                                parameters.Add(result);
                                break;
                            }
                        }
                    }

                    if (parameters.Count != arguments.Length)
                        return null;

                    return _constructor(parameters);
                }

                return null;
            }
        }

        #endregion
    }
}
