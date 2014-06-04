namespace AngleSharp.DOM
{
	using System;

    /// <summary>
    /// This type represents a set of space-separated tokens. 
    /// </summary>
	[DOM("DOMTokenList")]
	public interface ITokenList
	{
        /// <summary>
        /// Gets the number of contained tokens.
        /// </summary>
		[DOM("length")]
		Int32 Length { get; }

        /// <summary>
        /// Gets an item in the list by its index.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The item at the specified index.</returns>
		[DOM("item")]
		String this[Int32 index] { get; }

        /// <summary>
        /// Returns true if the underlying string contains a token, otherwise false.
        /// </summary>
        /// <param name="token">The token to search for.</param>
        /// <returns>The result of the search.</returns>
		[DOM("contains")]
		Boolean Contains(String token);

        /// <summary>
        /// Adds some tokens to the underlying string.
        /// </summary>
        /// <param name="tokens">A list of tokens to add.</param>
		[DOM("add")]
		void Add(params String[] tokens);

        /// <summary>
        /// Remove some tokens from the underlying string.
        /// </summary>
        /// <param name="tokens">A list of tokens to remove.</param>
		[DOM("remove")]
		void Remove(params String[] tokens);

        /// <summary>
        /// Removes the specified token from string and returns false.
        /// If token doesn't exist it's added and the function returns true.
        /// </summary>
        /// <param name="token">The token to toggle.</param>
        /// <param name="force"></param>
        /// <returns>True if the token has been added, otherwise false.</returns>
		[DOM("toggle")]
		Boolean Toggle(String token, Boolean force = false);
	}
}