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
            if (wr.IsAlive)
            {
                value = wr.Target as T;
                return true;
            }

            value = null;
            return false;
        }
    }
}
