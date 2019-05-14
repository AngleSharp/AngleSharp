namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// Represents a single instance object.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class DomInstanceAttribute : Attribute
    {
        /// <summary>
        /// Creates a new instance.
        /// </summary>
        /// <param name="name">The name to use.</param>
        public DomInstanceAttribute(String name)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the name of the variable.
        /// </summary>
        public String Name
        {
            get;
            private set;
        }
    }
}
