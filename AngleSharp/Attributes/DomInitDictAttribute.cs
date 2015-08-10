namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute decorates official DOM methods as specified by the W3C.
    /// It tells scripting engines that bags with objects should be provided,
    /// which have to be expanded to be used as arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class DomInitDictAttribute : Attribute
    {
    }
}
