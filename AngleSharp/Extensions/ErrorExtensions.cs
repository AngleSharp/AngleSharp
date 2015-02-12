namespace AngleSharp.Extensions
{
    using AngleSharp.Attributes;
    using System;
    using System.Diagnostics;
    using System.Reflection;

    /// <summary>
    /// A set of useful helpers concerning errors.
    /// </summary>
    [DebuggerStepThrough]
    static class ErrorExtensions
    {
        /// <summary>
        /// Retrieves a string describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The description of the error.</returns>
        public static String GetMessage(this ErrorCode code)
        {
            var attr = typeof(ErrorCode).GetTypeInfo().GetDeclaredField(code.ToString()).GetCustomAttribute<DomDescriptionAttribute>();

            if (attr != null)
                return attr.Description;

            return "An unknown error occurred.";
        }

        /// <summary>
        /// Retrieves a number describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The code of the error.</returns>
        public static Int32 GetCode(this ErrorCode code)
        {
            return (Int32)code;
        }
    }
}
