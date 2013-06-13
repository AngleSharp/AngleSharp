using System;

namespace AngleSharp
{
    /// <summary>
    /// A collection of useful helpers when working with SVG.
    /// </summary>
    static class MathMLHelpers
    {
        /// <summary>
        /// Adjusts the attribute name to the correct capitalization.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static string AdjustAttributeName(string attributeName)
        {
            switch (attributeName)
            {
                case "definitionurl": return "definitionURL";
                default: return attributeName;
            }
        }
    }
}
