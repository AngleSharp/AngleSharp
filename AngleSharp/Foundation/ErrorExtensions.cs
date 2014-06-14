namespace AngleSharp
{
    using System;
    using System.Reflection;

    /// <summary>
    /// A set of useful helpers concerning errors.
    /// </summary>
    static class ErrorExtensions
    {
        /// <summary>
        /// Retrieves a string describing the error of a given error code.
        /// </summary>
        /// <param name="code">A specific error code.</param>
        /// <returns>The description of the error.</returns>
        public static String GetErrorMessage(this ErrorCode code)
        {
            var attr = typeof(ErrorCode).GetTypeInfo().GetDeclaredField(code.ToString()).GetCustomAttribute<DomDescriptionAttribute>();

            if (attr != null)
                return attr.Description;

            return "An unknown error occurred.";
        }
    }
}
