namespace System.Collections.Concurrent
{
    using System.Collections.Generic;

    sealed class ConcurrentDictionary<TKey, TValue> : IDictionary<TKey, TValue>
    {
        #region Fields

        IDictionary<TKey, TValue> values;

        #endregion

        #region ctor

        public ConcurrentDictionary()
        {
            values = new Dictionary<TKey, TValue>();
        }

        #endregion

        #region Properties

        public ICollection<TKey> Keys
        {
            get { return values.Keys; }
        }

        public ICollection<TValue> Values
        {
            get { return values.Values; }
        }

        public TValue this[TKey key]
        {
            get
            {
                return values[key];
            }
            set
            {
                lock (values)
                    values[key] = value;
            }
        }

        public Int32 Count
        {
            get { return values.Count; }
        }

        public Boolean IsReadOnly
        {
            get { return values.IsReadOnly; }
        }

        #endregion

        #region Methods

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueGenerator)
        {
            lock (values)
            {
                if (values.ContainsKey(key))
                    return values[key];

                var value = valueGenerator(key);
                values.Add(key, value);
                return value;
            }
        }

        public TValue GetOrAdd(TKey key, TValue value)
        {
            lock (values)
            {
                if (values.ContainsKey(key))
                    return values[key];

                values.Add(key, value);
            }

            return value;
        }

        public void Add(TKey key, TValue value)
        {
            lock (values)
                values.Add(key, value);
        }

        public Boolean ContainsKey(TKey key)
        {
            return values.ContainsKey(key);
        }

        public Boolean Remove(TKey key)
        {
            lock (values)
                return values.Remove(key);
        }

        public Boolean TryGetValue(TKey key, out TValue value)
        {
            lock (values)
                return values.TryGetValue(key, out value);
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            lock (values)
                values.Add(item);
        }

        public void Clear()
        {
            lock (values)
                values.Clear();
        }

        public Boolean Contains(KeyValuePair<TKey, TValue> item)
        {
            return values.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, Int32 arrayIndex)
        {
            lock (values)
                values.CopyTo(array, arrayIndex);
        }

        public Boolean Remove(KeyValuePair<TKey, TValue> item)
        {
            lock (values)
                return values.Remove(item);
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
