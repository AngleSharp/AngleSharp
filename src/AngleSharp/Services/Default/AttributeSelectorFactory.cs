namespace AngleSharp.Services.Default
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSS attribute selector instance mappings.
    /// </summary>
    public class AttributeSelectorFactory : IAttributeSelectorFactory
    {
        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>
        {
            { CombinatorSymbols.Exactly, SimpleSelector.AttrMatch },
            { CombinatorSymbols.InList, SimpleSelector.AttrList },
            { CombinatorSymbols.InToken, SimpleSelector.AttrHyphen },
            { CombinatorSymbols.Begins, SimpleSelector.AttrBegins },
            { CombinatorSymbols.Ends, SimpleSelector.AttrEnds },
            { CombinatorSymbols.InText, SimpleSelector.AttrContains },
            { CombinatorSymbols.Unlike, SimpleSelector.AttrNotMatch },
        };

        /// <summary>
        /// Represents a creator delegate for creating an attribute selector.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="prefix">The prefix for the attribute.</param>
        /// <returns></returns>
        public delegate ISelector Creator(String name, String value, String prefix);

        /// <summary>
        /// Registers a new creator for the specified combinator.
        /// Throws an exception if another creator for the given
        /// combinator is already added.
        /// </summary>
        /// <param name="combinator">The used CSS combinator.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String combinator, Creator creator)
        {
            _creators.Add(combinator, creator);
        }

        /// <summary>
        /// Unregisters an existing creator for the given combinator.
        /// </summary>
        /// <param name="combinator">The used CSS combinator.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String combinator)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(combinator, out creator))
            {
                _creators.Remove(combinator);
            }

            return creator;
        }

        /// <summary>
        /// Creates the default CSS attribute selector for the given options.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The used value, if any.</param>
        /// <param name="prefix">The given prefix, if any.</param>
        /// <returns>The selector with the given options.</returns>
        protected virtual ISelector CreateDefault(String name, String value, String prefix)
        {
            return SimpleSelector.AttrAvailable(name, value);
        }

        /// <summary>
        /// Creates the associated CSS attribute selector.
        /// </summary>
        /// <param name="combinator">The used CSS combinator.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The used value, if any.</param>
        /// <param name="prefix">The given prefix, if any.</param>
        /// <returns>The associated selector.</returns>
        public ISelector Create(String combinator, String name, String value, String prefix)
        {
            var creator = default(Creator);

            if (_creators.TryGetValue(combinator, out creator))
            {
                return creator.Invoke(name, value, prefix);
            }

            return CreateDefault(name, value, prefix);
        }
    }
}
