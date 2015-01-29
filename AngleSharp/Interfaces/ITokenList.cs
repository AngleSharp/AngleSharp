namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
	using System;
    using System.Collections.Generic;

    /// <summary>
    /// This type represents a set of space-separated tokens. 
    /// </summary>
	[DomName("DOMTokenList")]
	public interface ITokenList : IEnumerable<String>
	{
        /// <summary>
        /// Gets the number of contained tokens.
        /// </summary>
		[DomName("length")]
		Int32 Length { get; }

        /// <summary>
        /// Gets an item in the list by its index.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The item at the specified index.</returns>
		[DomName("item")]
        [DomAccessor(Accessors.Getter)]
		String this[Int32 index] { get; }

        /// <summary>
        /// Returns true if the underlying string contains a token, otherwise
        /// false.
        /// </summary>
        /// <param name="token">The token to search for.</param>
        /// <returns>The result of the search.</returns>
		[DomName("contains")]
		Boolean Contains(String token);

        /// <summary>
        /// Adds some tokens to the underlying string.
        /// </summary>
        /// <param name="tokens">A list of tokens to add.</param>
		[DomName("add")]
		void Add(params String[] tokens);

        /// <summary>
        /// Remove some tokens from the underlying string.
        /// </summary>
        /// <param name="tokens">A list of tokens to remove.</param>
		[DomName("remove")]
		void Remove(params String[] tokens);

        /// <summary>
        /// Removes the specified token from string and returns false.
        /// If token doesn't exist it's added and the function returns true.
        /// </summary>
        /// <param name="token">The token to toggle.</param>
        /// <param name="force"></param>
        /// <returns>
        /// True if the token has been added, otherwise false.
        /// </returns>
		[DomName("toggle")]
		Boolean Toggle(String token, Boolean force = false);
	}
}