using System;
using System.Diagnostics;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Useful helpers for the XML parser.
    /// </summary>
    static class XMLHelpers
    {
        /// <summary>
        /// Determines if the given character is a legal character for the public id field:
        /// http://www.w3.org/TR/REC-xml/#NT-PubidChar
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsPubidChar(this Char c)
        {
            return c.IsAlphanumericAscii() || c == Specification.MINUS || c == Specification.SQ || c == Specification.PLUS ||
                   c == Specification.COMMA || c == Specification.DOT || c == Specification.SOLIDUS || c == Specification.COLON ||
                   c == Specification.QM || c == Specification.EQ || c == Specification.EM || c == Specification.ASTERISK ||
                   c == Specification.NUM || c == Specification.AT || c == Specification.DOLLAR || c == Specification.UNDERSCORE ||
                   c == Specification.RBO || c == Specification.RBC || c == Specification.SC || c == Specification.PERCENT ||
                   c.IsSpaceCharacter();
        }

        /// <summary>
        /// Determines if the given character is a legal name start character for XML.
        /// http://www.w3.org/TR/REC-xml/#NT-NameStartChar
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsXmlNameStart(this Char c)
        {
            return c.IsLetter() || c == Specification.COLON || c == Specification.UNDERSCORE || c.IsInRange(0xC0, 0xD6) || 
                   c.IsInRange(0xD8, 0xF6) || c.IsInRange(0xF8, 0x2FF) || c.IsInRange(0x370, 0x37D) || c.IsInRange(0x37F, 0x1FFF) || 
                   c.IsInRange(0x200C, 0x200D) || c.IsInRange(0x2070, 0x218F) || c.IsInRange(0x2C00, 0x2FEF) || 
                   c.IsInRange(0x3001, 0xD7FF) || c.IsInRange(0xF900, 0xFDCF) || c.IsInRange(0xFDF0, 0xFFFD) || 
                   c.IsInRange(0x10000, 0xEFFFF);
        }

        /// <summary>
        /// Determines if the given character is a name character for XML.
        /// http://www.w3.org/TR/REC-xml/#NT-NameChar
        /// </summary>
        /// <param name="c">The character to examine.</param>
        /// <returns>The result of the test.</returns>
        [DebuggerStepThrough]
        public static Boolean IsXmlName(this Char c)
        {
            return c.IsXmlNameStart() || c.IsDigit() || c == Specification.MINUS || c == Specification.DOT || c == 0xB7 ||
                   c.IsInRange(0x300, 0x36F) || c.IsInRange(0x203F, 0x2040);
        }
    }
}
