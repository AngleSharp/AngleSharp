namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Token list extensions exposed to DOM.
    /// </summary>
    [DomExposed("DOMTokenList")]
    public static class DomTokenListExtensions
    {
        /// <summary>
        /// Toggles the token like the DOM's toggle(token, force): without force,
        /// the token is removed if present and added otherwise; with force, it is
        /// only added (true) or only removed (false).
        /// </summary>
        /// <remarks>
        /// <see cref="ITokenList.Toggle(String, Boolean)"/> cannot tell an omitted
        /// force from false, so it toggles in both cases.
        /// </remarks>
        /// <param name="list">The list of tokens to change.</param>
        /// <param name="token">The token to toggle.</param>
        /// <param name="force">True to only add, false to only remove, null to toggle.</param>
        /// <returns>True if the token is present afterwards, otherwise false.</returns>
        [DomName("toggle")]
        public static Boolean ToggleToken(this ITokenList list, String token, Boolean? force = null)
        {
            var isPresent = force ?? !list.Contains(token);

            if (isPresent)
            {
                list.Add(token);
            }
            else
            {
                list.Remove(token);
            }

            return isPresent;
        }
    }
}
