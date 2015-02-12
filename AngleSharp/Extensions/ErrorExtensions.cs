namespace AngleSharp.Extensions
{
#if SILVERLIGHT
    using System.Linq;
#endif
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
#if !SILVERLIGHT
            var attr = typeof(ErrorCode).GetTypeInfo().GetDeclaredField(code.ToString()).GetCustomAttribute<DomDescriptionAttribute>();
#else
            var attr = typeof(ErrorCode).GetField(code.ToString()).GetCustomAttributes(typeof(DomDescriptionAttribute), false).OfType<DomDescriptionAttribute>().FirstOrDefault();
#endif
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
