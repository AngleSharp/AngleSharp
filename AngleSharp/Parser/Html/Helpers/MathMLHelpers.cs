namespace AngleSharp.Parser.Html
{
    using System;

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
        public static String AdjustMathMLAttributeName(this String attributeName)
        {
            if (attributeName.Equals("definitionurl"))
                return "definitionURL";

            return attributeName;
        }
    }
}
