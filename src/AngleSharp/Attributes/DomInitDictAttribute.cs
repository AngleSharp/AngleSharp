namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// This attribute decorates official DOM methods as specified by the W3C.
    /// It tells scripting engines that bags with objects should be provided,
    /// which have to be expanded to be used as arguments.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor, Inherited = false)]
    public sealed class DomInitDictAttribute : Attribute
    {
        /// <summary>
        /// Creates a new DomInitDict attribute.
        /// </summary>
        /// <param name="offset">The start index of the dictionary.</param>
        /// <param name="optional">Has a dictionary to be present?</param>
        public DomInitDictAttribute(Int32 offset = 0, Boolean optional = false)
        {
            Offset = offset;
            IsOptional = optional;
        }

        /// <summary>
        /// Gets the offset of the passed arguments. Arguments before the offset
        /// will be skipped and are not part of the dictionary.
        /// </summary>
        public Int32 Offset
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets if the dictionary is completely optional and does not have to
        /// be present.
        /// </summary>
        public Boolean IsOptional
        {
            get;
            private set;
        }
    }
}
