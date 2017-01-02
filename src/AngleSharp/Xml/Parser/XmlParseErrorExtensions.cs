﻿namespace AngleSharp.Xml.Parser
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Extensions for the parse error enumeration.
    /// </summary>
    static class XmlParseErrorExtensions
    {
        /// <summary>
        /// Creates the XmlParseException at the given position.
        /// </summary>
        /// <param name="code">The code for the exception.</param>
        /// <param name="position">The position of the error.</param>
        /// <returns>The new exception object.</returns>
        public static XmlParseException At(this XmlParseError code, TextPosition position)
        {
            var message = "Error while parsing the provided XML document.";
            return new XmlParseException(code.GetCode(), message, position);
        }

        /// <summary>
        /// Retrieves a number describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The code of the error.</returns>
        public static Int32 GetCode(this XmlParseError code)
        {
            return (Int32)code;
        }
    }
}
