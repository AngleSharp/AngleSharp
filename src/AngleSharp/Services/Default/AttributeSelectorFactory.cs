namespace AngleSharp.Services.Default
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSS attribute selector instance mappings.
    /// </summary>
    public sealed class AttributeSelectorFactory : IAttributeSelectorFactory
    {
        delegate ISelector Creator(String name, String value, String prefix);

        readonly Dictionary<String, Creator> creators = new Dictionary<String, Creator>
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

            if (creators.TryGetValue(combinator, out creator))
            {
                return creator(name, value, prefix);
            }

            return SimpleSelector.AttrAvailable(name, value);
        }
    }
}
