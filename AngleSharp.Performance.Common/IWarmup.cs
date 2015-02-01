namespace AngleSharp.Performance
{
    using System;

    public interface IWarmup
    {
        void ForceJit(Type type);
    }
}
