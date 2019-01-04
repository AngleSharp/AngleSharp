namespace AngleSharp.Common
{
    using System.Runtime.CompilerServices;

    sealed class AttachedProperty<TObj, TProp>
        where TObj : class
        where TProp : class
    {
        private readonly ConditionalWeakTable<TObj, TProp> _properties = new ConditionalWeakTable<TObj, TProp>();

        public TProp Get(TObj item)
        {
            _properties.TryGetValue(item, out var value);
            return value;
        }

        public void Set(TObj item, TProp value)
        {
            _properties.Add(item, value);
        }
    }
}
