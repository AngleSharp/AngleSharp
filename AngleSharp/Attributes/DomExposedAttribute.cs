namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute is used to determine the hosting interface.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, 
        AllowMultiple = true, Inherited = false)]
    public sealed class DomExposedAttribute : Attribute
    {
        /// <summary>
        /// Creates a new DomExposedAttribute.
        /// </summary>
        /// <param name="target">
        /// The official name of the target interface.
        /// </param>
        public DomExposedAttribute(String target)
        {
            Target = target;
        }

        /// <summary>
        /// Gets the official name of the target interface.
        /// </summary>
        public String Target
        {
            get;
            private set;
        }
    }
}
