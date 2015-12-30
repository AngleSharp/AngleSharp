namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS medium.
    /// </summary>
    public interface ICssMedium : ICssNode
    {
        /// <summary>
        /// Gets the type of medium that is represented.
        /// </summary>
        String Type { get; }

        /// <summary>
        /// Gets if the medium has been created using the only keyword.
        /// </summary>
        Boolean IsExclusive { get; }

        /// <summary>
        /// Gets if the medium has been created using the not keyword.
        /// </summary>
        Boolean IsInverse { get; }

        /// <summary>
        /// Gets a string describing the covered constraints.
        /// </summary>
        String Constraints { get; }

        /// <summary>
        /// Gets an enumerable of contained features.
        /// </summary>
        IEnumerable<IMediaFeature> Features { get; }
    }
}
