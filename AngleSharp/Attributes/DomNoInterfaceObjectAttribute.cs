namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute appears on an interfaces, which must not be available in the ECMAScript binding.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface | AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class DomNoInterfaceObjectAttribute : Attribute
    {
        internal DomNoInterfaceObjectAttribute()
        { }
    }
}
