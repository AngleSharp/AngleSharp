namespace System.Collections.Concurrent
{
    using System.Collections.Generic;
    using System.Linq;

    class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Fields

        readonly IDictionary<TKey, TValue> _values;
        readonly Object _sync = new Object();

        #endregion

        #region ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeDictionary{TKey,TValue}"/> class. 
        /// </summary>
        public ConcurrentDictionary()
        {
            _values = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadSafeDictionary{TKey,TValue}"/> class using the 
        /// given <see cref="IEqualityComparer{T}"/>.
        /// </summary>
        /// <param name="comparer">The <see cref="IEqualityComparer{T}"/> implementation to use when comparing keys</param>
        public ConcurrentDictionary(IEqualityComparer<TKey> comparer)
        {
            _values = new Dictionary<TKey, TValue>(comparer);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the collection is readonly.
        /// </summary>
        public Boolean IsReadOnly
        {
            get { return _values.IsReadOnly; }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value of the key/value pair at the specified index.</returns>
        public TValue this[TKey key]
        {
            get { return _values[key]; }
            set { lock (_sync) _values[key] = value; }
        }

        /// <summary>
        /// Gets the number of key/value pairs contained in the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        public Int32 Count
        {
            get { return _values.Count; }
        }

        /// <summary>
        /// Gets a collection that contains the values in the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                lock (_sync)
                {
                    return _values.Values;
                }
            }
        }

        /// <summary>
        /// Gets a collection containing the keys in the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                lock (_sync)
                {
                    return _values.Keys;
                }
            }
        }

        #endregion

        #region Methods

        public void Add(TKey key, TValue value)
        {
            lock (_sync)
                Add(key, value);
        }

        public Boolean ContainsKey(TKey key)
        {
            return _values.ContainsKey(key);
        }

        public Boolean Remove(TKey key)
        {
            lock (_sync)
                return _values.Remove(key);
        }

        Boolean IDictionary<TKey, TValue>.TryGetValue(TKey key, out TValue value)
        {
            lock (_sync)
                return _values.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            lock (_sync)
                _values.Add(item);
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return _values.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            _values.CopyTo(array, arrayIndex);
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (_sync)
                return _values.Remove(item);
        }

        /// <summary>
        /// Adds a key/value pair to the <see cref="ThreadSafeDictionary{TKey,TValue}"/> by using the specified function, if the key does not already exist.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="valuefactory">The function used to generate a value for the key</param>
        /// <returns>The value for the key.</returns>
        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valuefactory)
        {
            lock (_sync)
            {
                TValue value;
                if (!_values.TryGetValue(key, out value))
                {
                    value = valuefactory(key);
                    _values.Add(key, value);
                }

                return value;
            }
        }    
        
        /// <summary>
        /// Uses the specified functions to add a key/value pair to the <see cref="ThreadSafeDictionary{TKey,TValue}"/> if the key does not already exist, 
        /// or to update a key/value pair in the <see cref="ThreadSafeDictionary{TKey,TValue}"/> if the key already exists.
        /// </summary>
        /// <param name="key">The key to be added or whose value should be updated</param>
        /// <param name="addValueFactory">The function used to generate a value for an absent key</param>
        /// <param name="updateValueFactory">The function used to generate a new value for an existing key based on the key's existing value</param>
        public void AddOrUpdate(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            lock (_sync)
            {
                TValue value;
                if (!_values.TryGetValue(key, out value))
                {
                    _values.Add(key, addValueFactory(key));
                }
                else
                {
                    _values[key] = updateValueFactory(key, value);
                }
            }
        }

        /// <summary>
        /// Attempts to get the value associated with the specified key from the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the object from the <see cref="ThreadSafeDictionary{TKey,TValue}"/> that has the specified key, 
        /// or the default value of <typeparamref name="TValue"/>, if the operation failed.</param>
        public void TryGetValue(TKey key, out TValue value)
        {
            lock (_sync)
            {
                _values.TryGetValue(key, out value);
            }
        }

        /// <summary>
        /// Attempts to remove and return the value that has the specified key from the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to remove and return.</param>
        /// <param name="value">When this method returns, contains the object removed from the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.</param>
        /// <returns>When this method returns, contains the object removed from the <see cref="ThreadSafeDictionary{TKey,TValue}"/>, 
        /// or the default value of the <typeparamref name="TValue"/> type if key does not exist.</returns>
        public Boolean TryRemove(TKey key, out TValue value)
        {
            lock (_sync)
            {                    
                if (_values.TryGetValue(key, out value))
                {                 
                    return true;
                }
                
                value = default(TValue);
                return false;                
            }
        }

        /// <summary>
        /// Attempts to add the specified key and value to the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>true if the key/value pair was added to the <see cref="ThreadSafeDictionary{TKey,TValue}"/> successfully; false if the key already exists.</returns>
        public Boolean TryAdd(TKey key, TValue value)
        {
            if (_values.ContainsKey(key))
            {
                return false;
            }

            lock (_sync)
            {
                if (_values.ContainsKey(key))
                {
                    return false;
                }
              
                _values.Add(key, value);
                return true;              
            }
        }

        /// <summary>
        /// Removes all keys and values from the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>        
        public void Clear()
        {
            lock (_sync)
            {
                _values.Clear();    
            }                
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>An enumerator for the <see cref="ThreadSafeDictionary{TKey,TValue}"/></returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            lock (_sync)
            {
                return _values.ToDictionary(kvp => kvp.Key, kvp => kvp.Value).GetEnumerator();
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="ThreadSafeDictionary{TKey,TValue}"/>.
        /// </summary>
        /// <returns>An enumerator for the <see cref="ThreadSafeDictionary{TKey,TValue}"/></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
