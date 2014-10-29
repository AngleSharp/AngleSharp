namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute is used to place a description on some object.
    /// The description can then be read out at runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Method | AttributeTargets.Field | AttributeTargets.Delegate)]
    public sealed class DomDescriptionAttribute : Attribute
    {
        internal DomDescriptionAttribute(String description)
        {
            Description = description;
        }

        /// <summary>
        /// Gets the official name of the given class,
        /// method or property.
        /// </summary>
        public String Description
        {
            get;
            private set;
        }
    }
}
