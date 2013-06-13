using System;

namespace AngleSharp
{
    /// <summary>
    /// A collection of useful helpers when working with SVG.
    /// </summary>
    static class ForeignHelpers
    {
        /// <summary>
        /// Adjusts the attribute name to the correct prefix.
        /// </summary>
        /// <param name="attributeName">The name of adjust.</param>
        /// <returns>The name with the correct capitalization.</returns>
        public static string AdjustAttributeName(string attributeName)
        {
            switch (attributeName)
            {
                case "xlink:actuate":
                case "xlink:arcrole":
                case "xlink:href":
                case "xlink:role":
                case "xlink:show":
                case "xlink:title":
                case "xlink:type":
                case "xml:base":
                case "xml:lang":
                case "xml:space":
                case "xmlns":
                case "xmlns:xlink":
                    return attributeName;

                default:
                    if (attributeName.Contains(':'))
                        return attributeName.Substring(attributeName.IndexOf(':') + 1);
                    return attributeName;
            }
        }
    }
}
