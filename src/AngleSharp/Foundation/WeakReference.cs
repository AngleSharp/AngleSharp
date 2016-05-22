#if NET40 || SL50
namespace AngleSharp
{
    using System;

    sealed class WeakReference<T>
        where T : class
    {
        WeakReference wr;

        public WeakReference(T value)
        {
            wr = new WeakReference(value);
        }

        public Boolean TryGetTarget(out T value)
        {
            value = wr.Target as T;
            return value != null;
        }
    }
}
#endif