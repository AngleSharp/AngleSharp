namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute is used to mark a constructor as being
    /// accessible from scripts.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor, Inherited = false)]
    public sealed class DomConstructorAttribute : Attribute
    {
    }
}
