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
        public void AddStatic(String trigger, T instance)
        {
            _calls.Add(new StaticValueFactoryEntry(trigger, instance));
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
        public void AddMultiple<TType>()
            where TType : T
        {
            var type = typeof(TType);
            var constructorInfo = type.GetDeclaredConstructor();
            var entry = new MultipleValueFactoryEntry(_calls, list => (TType)constructorInfo.Invoke(new Object[] { list }));
            _calls.Add(entry);
        }

        #endregion

        #region Nested

        abstract class ValueFactoryEntry
        {
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
                    _validators.Add(value => Validate(ToLength(value)));
                else if (type == typeof(Color) || type == typeof(Color?))
                    _validators.Add(value => Validate(ToColor(value)));
            }
            
            public override T Create(CSSValue argument)
            {
                var arguments = argument as CSSValueList;

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

            static Length? ToLength(CSSValue value)
            {
                if (value is CSSLengthValue)
                    return ((CSSLengthValue)value).Length;
                else if (value == CSSNumberValue.Zero)
                    return Length.Zero;

                return null;
            }

            static Color? ToColor(CSSValue value)
            {
                if (value is CSSColorValue)
                    return ((CSSColorValue)value).Color;

                return null;
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
            }
            
            public override T Create(CSSValue argument)
            {
                var arguments = argument as CSSValueList;

                if (arguments != null && arguments.Separator == CssValueListSeparator.Comma)
                {
                    var parameters = new List<T>();

                    foreach (var arg in arguments.List)
                    {
                        for (int i = 0; i < _source.Count; i++)
                        {
                            if (_source[i] == this)
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
