namespace AngleSharp.Services
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Provides a spell correction service.
    /// </summary>
    public interface ISpellCheckService : IService
    {
        /// <summary>
        /// Gets the culture for spell check service.
        /// </summary>
        CultureInfo Culture { get; }

        /// <summary>
        /// Ignores the word.
        /// </summary>
        /// <param name="word">The word to ignore.</param>
        /// <param name="persistent">If true, should be added to dictionary. Otherwise false.</param>
        void Ignore(String word, Boolean persistent);

        /// <summary>
        /// Checks if the given word is correct.
        /// </summary>
        /// <param name="word">The word to check.</param>
        /// <returns>True if the word is correctly spelled, otherwise false.</returns>
        Boolean IsCorrect(String word);

        /// <summary>
        /// Suggests correct spellings for the given word.
        /// </summary>
        /// <param name="word">The base word.</param>
        /// <returns>An enumeration over possibly right variants.</returns>
        IEnumerable<String> SuggestFor(String word);
    }
}
