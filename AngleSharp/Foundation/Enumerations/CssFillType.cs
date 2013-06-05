using System;

namespace AngleSharp
{
    /// <summary>
    /// Determines the fill type of @ rules.
    /// </summary>
    enum CssFillType : ushort
    {
        /// <summary>
        /// This is just a simple statement.
        /// </summary>
        None,
        /// <summary>
        /// The type is filled with rules.
        /// </summary>
        Rule,
        /// <summary>
        /// The type is filled with declarations.
        /// </summary>
        Declaration
    }
}
