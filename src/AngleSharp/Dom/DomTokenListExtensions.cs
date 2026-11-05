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
        /// Replaces a token with another one, which keeps its position.
        /// </summary>
        /// <param name="list">The list of tokens to change.</param>
        /// <param name="token">The token to replace.</param>
        /// <param name="newToken">The token to have instead.</param>
        /// <returns>True if the token was there, otherwise false.</returns>
        [DomName("replace")]
        public static Boolean ReplaceToken(this ITokenList list, String token, String newToken)
        {
            var tokens = System.Linq.Enumerable.ToArray(list);
            var index = System.Array.IndexOf(tokens, token);

            if (index == -1)
            {
                return false;
            }

            tokens[index] = newToken;
            list.Remove(tokens.Length == 0 ? System.Array.Empty<String>() : System.Linq.Enumerable.ToArray(list));
            list.Add(System.Linq.Enumerable.ToArray(System.Linq.Enumerable.Distinct(tokens)));
            return true;
        }

        /// <summary>
        /// Gets the tokens as one text.
        /// </summary>
        /// <param name="list">The list of tokens.</param>
        /// <returns>The tokens separated by spaces.</returns>
        [DomName("value")]
        [DomAccessor(Accessors.Getter)]
        public static String GetValue(this ITokenList list) => String.Join(" ", list);

        /// <summary>
        /// Sets the tokens from one text.
        /// </summary>
        /// <param name="list">The list of tokens to change.</param>
        /// <param name="value">The tokens separated by spaces.</param>
        [DomName("value")]
        [DomAccessor(Accessors.Setter)]
        public static void SetValue(this ITokenList list, String value)
        {
            list.Remove(System.Linq.Enumerable.ToArray(list));
            list.Add((value ?? String.Empty).Split(new[] { ' ', '\t', '\n', '\f', '\r' }, StringSplitOptions.RemoveEmptyEntries));
        }

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
