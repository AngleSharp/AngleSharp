namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// The attribute indicates that invocations of the attribute's getter
    /// or setter with a this value that is not an object that implements
    /// the interface on which the attribute appears will be ignored.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Event)]
    public sealed class DomLenientThisAttribute : Attribute
    {
        internal DomLenientThisAttribute()
        {
        }
    }
}
