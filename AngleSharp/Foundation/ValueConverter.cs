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
        /// Gets the static value entry resolved with the given trigger.
        /// </summary>
        /// <param name="trigger">The trigger that is used for value resolution.</param>
        /// <returns>The instance or null, if no such instance could be found.</returns>
        public T GetStatic(String trigger)
        {
            foreach (var call in _calls)
            {
                var entry = call as StaticValueFactoryEntry;

                if (entry != null && entry.Trigger.Equals(trigger, StringComparison.OrdinalIgnoreCase))
                    return entry.Instance;
            }

            return null;
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
        /// Adds a delegate to a construction function that is invoked
        /// to construct the result. If the given argument is not suited,
        /// then null should be returned.
        /// </summary>
        /// <param name="creator">The creator function.</param>
        public void AddDelegate(Func<CSSValue, T> creator)
        {
            _calls.Add(new DelegateValueFactoryEntry(creator));
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

        /// <summary>
        /// Adds the possibility of constructing an object based on
        /// all other possible results. The arguments have to be a list.
        /// The key difference is that only chunks will be tested.
        /// </summary>
        /// <typeparam name="TType">The type of the result.</typeparam>
        /// <param name="chunkSize">The optional chunk size to use.</param>
        public void AddEnumerable<TType>(Int32 chunkSize = 1)
            where TType : T
        {
            var type = typeof(TType);
            var constructorInfo = type.GetDeclaredConstructor();
            var entry = new EnumerableValueFactoryEntry(_calls, list => (TType)constructorInfo.Invoke(new Object[] { list }));
            entry.ChunkSize = chunkSize;
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

            public String Trigger
            {
                get { return _trigger; }
            }

            public T Instance
            {
                get { return _instance; }
            }

            public override T Create(CSSValue argument)
            {
                if (argument is CSSIdentifierValue && ((CSSIdentifierValue)argument).Value.Equals(_trigger, StringComparison.OrdinalIgnoreCase))
                    return _instance;

                return null;
            }
        }

        sealed class DelegateValueFactoryEntry : ValueFactoryEntry
        {
            Func<CSSValue, T> _creator;

            public DelegateValueFactoryEntry(Func<CSSValue, T> creator)
            {
                _creator = creator;
            }

            public override T Create(CSSValue argument)
            {
                return _creator(argument);
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
                else if (type == typeof(Int32) || type == typeof(Int32?))
                    _validators.Add(value => Validate(value.ToInteger()));
                else if (type == typeof(Byte) || type == typeof(Byte?))
                    _validators.Add(value => Validate(value.ToByte()));
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

                if (arguments != null)
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
            }
            
            public override T Create(CSSValue argument)
            {
                var arguments = argument as CSSValueList;

                if (arguments != null)
                {
                    var found = false;

                    foreach (var arg in arguments)
                        if (found = (arg == CSSValue.Separator))
                            break;

                    if (found)
                    {
                        var parameters = new List<T>();
                        var bag = new List<CSSValueList>();
                        var temp = new CSSValueList();

                        foreach (var arg in arguments)
                        {
                            if (arg == CSSValue.Separator)
                            {
                                bag.Add(temp);
                                temp = new CSSValueList();
                            }
                            else
                                temp.Add(arg);
                        }

                        bag.Add(temp);
                        temp = null;

                        foreach (var element in bag)
                        {
                            for (int i = 0; i < _source.Count; i++)
                            {
                                if (_source[i] == this || _source[i].IsExclusive)
                                    continue;

                                var result = _source[i].Create(element);

                                if (found = (result != null))
                                {
                                    parameters.Add(result);
                                    break;
                                }
                            }

                            if (!found)
                                return null;
                        }

                        return _constructor(parameters);
                    }
                }

                return null;
            }
        }

        sealed class EnumerableValueFactoryEntry : ValueFactoryEntry
        {
            List<ValueFactoryEntry> _source;
            Func<List<T>, T> _constructor;

            public EnumerableValueFactoryEntry(List<ValueFactoryEntry> source, Func<List<T>, T> constructor)
            {
                _source = source;
                _constructor = constructor;
            }

            public Int32 ChunkSize
            {
                get;
                set;
            }

            public override T Create(CSSValue argument)
            {
                if (ChunkSize < 1)
                    return null;

                var arguments = argument as CSSValueList;

                if (arguments != null && arguments.Length % ChunkSize == 0)
                {
                    var parameters = new List<T>();

                    if (ChunkSize > 1)
                    {
                        var testList = new CSSValueList();

                        for (var j = 0; j < arguments.Length; j += ChunkSize)
                        {
                            for (var i = 0; i < ChunkSize; i++)
                                testList.Add(arguments[i]);

                            for (var i = 0; i < _source.Count; i++)
                            {
                                if (_source[i] == this || _source[i].IsExclusive)
                                    continue;

                                var result = _source[i].Create(testList);

                                if (result != null)
                                {
                                    parameters.Add(result);
                                    testList.Clear();
                                    break;
                                }
                            }

                            if (testList.Length != 0)
                                return null;
                        }
                    }
                    else
                    {
                        for (var j = 0; j < arguments.Length; j++)
                        {
                            for (var i = 0; i < _source.Count; i++)
                            {
                                if (_source[i] == this || _source[i].IsExclusive)
                                    continue;

                                var result = _source[i].Create(arguments[j]);

                                if (result != null)
                                {
                                    parameters.Add(result);
                                    break;
                                }
                            }
                        }

                        if (parameters.Count != arguments.Length)
                            return null;
                    }
                    
                    return _constructor(parameters);
                }

                return null;
            }
        }

        #endregion
    }
}
