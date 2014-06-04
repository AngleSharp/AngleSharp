namespace AngleSharp
{
    using AngleSharp.DOM;
    using System;

    /// <summary>
    /// A set of useful extensions for the IDL.
    /// </summary>
    static class IdlExtensions
    {
        /// <summary>
        /// Returns true if the underlying string contains all of the tokens, otherwise false.
        /// </summary>
        /// <param name="list">The list that is considered.</param>
        /// <param name="tokens">The tokens to consider.</param>
        /// <returns>True if the string contained all tokens, otherwise false.</returns>
        public static Boolean Contains(this ITokenList list, String[] tokens)
        {
            for (int i = 0; i < tokens.Length; i++)
            {
                if (!list.Contains(tokens[i]))
                    return false;
            }

            return true;
        }
    }
}
