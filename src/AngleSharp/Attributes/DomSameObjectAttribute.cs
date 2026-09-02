namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// Decorates a read only attribute declaration whose type is an interface
    /// type. It indicates that reading the attribute always yields the same
    /// object, i.e., the value is not re-created on every access.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class DomSameObjectAttribute : Attribute
    {
    }
}
