namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute decorates official DOM objects as specified by the W3C. You could
    /// use it to detect all DOM types or get the correct spelling (PascalCase to camelCase).
    /// Multiple usages are allowed and should be checked.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Delegate | AttributeTargets.Enum, AllowMultiple = true)]
    public sealed class DomNameAttribute : Attribute
    {
        internal DomNameAttribute(String officialName)
        {
            OfficialName = officialName;
        }

        /// <summary>
        /// Gets the official name of the given class, method or property.
        /// </summary>
        public String OfficialName
        {
            get;
            private set;
        }
    }
}
