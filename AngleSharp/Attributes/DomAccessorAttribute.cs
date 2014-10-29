namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute decorates official DOM objects as specified by the W3C. You could
    /// use it to check if the given property or method should be placed on special locations,
    /// e.g. as a getter, setter or handled by a delete call.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class DomAccessorAttribute : Attribute
    {
        internal DomAccessorAttribute(Accessors type)
        {
            Type = type;
        }

        /// <summary>
        /// Gets the accessor types for the given property or method.
        /// </summary>
        public Accessors Type
        {
            get;
            private set;
        }
    }
}
