namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// Decorates a read only attribute declaration whose type is an interface type.
    /// It indicates that assigning to the attribute will have specific behavior.
    /// Namely, the assignment is "forwarded" to the named attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class DomPutForwardsAttribute : Attribute
    {
        internal DomPutForwardsAttribute(String propertyName)
        {
            PropertyName = propertyName;
        }

        /// <summary>
        /// Gets the official name of the property to forward assignments to.
        /// </summary>
        public String PropertyName
        {
            get;
            private set;
        }
    }
}
