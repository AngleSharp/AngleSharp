using System;

namespace AngleSharp
{
    /// <summary>
    /// This attribute decorates official DOM objects as specified by the W3C. You could
    /// use it to detect all DOM types or get the correct spelling (PascalCase to camelCase).
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method)]
    public sealed class DOMAttribute : Attribute
    {
        internal DOMAttribute(String officialName)
        {
            OfficialName = officialName;
        }

        /// <summary>
        /// Gets the official name of the given class,
        /// method or property.
        /// </summary>
        public String OfficialName
        {
            get;
            private set;
        }
    }
}
