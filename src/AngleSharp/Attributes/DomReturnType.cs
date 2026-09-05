namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// Decorates a method whose actual IDL return type is different
    /// to the declared return type. This needs to be casted to the
    /// given type in scripting engines.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class DomReturnTypeAttribute : Attribute
    {
        /// <summary>
        /// Creates a new DomReturnTypeAttribute.
        /// </summary>
        /// <param name="returnType">
        /// The actual type of the returned object.
        /// </param>
        public DomReturnTypeAttribute(Type returnType)
        {
            ReturnType = returnType;
        }

        /// <summary>
        /// Gets the actual IDL type of the returned object.
        /// </summary>
        public Type ReturnType { get; }
    }
}
