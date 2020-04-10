namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSS attribute selector instance mappings.
    /// </summary>
    public class DefaultAttributeSelectorFactory : IAttributeSelectorFactory
    {
        // see https://html.spec.whatwg.org/multipage/semantics-other.html#case-sensitivity-of-selectors
        private static readonly HashSet<String> insensitiveAttributes = new HashSet<String>
        {
            AttributeNames.Accept,
            AttributeNames.AcceptCharset,
            AttributeNames.Align,
            AttributeNames.Alink,
            AttributeNames.Axis,
            AttributeNames.BgColor,
            AttributeNames.Charset,
            AttributeNames.Checked,
            AttributeNames.Clear,
            AttributeNames.Codetype,
            AttributeNames.Color,
            AttributeNames.Compact,
            AttributeNames.Declare,
            AttributeNames.Defer,
            AttributeNames.Dir,
            AttributeNames.Direction,
            AttributeNames.Disabled,
            AttributeNames.Enctype,
            AttributeNames.Face,
            AttributeNames.Frame,
            AttributeNames.HrefLang,
            AttributeNames.HttpEquiv,
            AttributeNames.Lang,
            AttributeNames.Language,
            AttributeNames.Link,
            AttributeNames.Media,
            AttributeNames.Method,
            AttributeNames.Multiple,
            AttributeNames.NoHref,
            AttributeNames.NoResize,
            AttributeNames.NoShade,
            AttributeNames.NoWrap,
            AttributeNames.Readonly,
            AttributeNames.Rel,
            AttributeNames.Rev,
            AttributeNames.Rules,
            AttributeNames.Scope,
            AttributeNames.Scrolling,
            AttributeNames.Selected,
            AttributeNames.Shape,
            AttributeNames.Target,
            AttributeNames.Text,
            AttributeNames.Type,
            AttributeNames.Valign,
            AttributeNames.ValueType,
            AttributeNames.Vlink,
        };

        private readonly Dictionary<String, Creator> _creators = new Dictionary<String, Creator>
        {
            { CombinatorSymbols.Exactly, (name, value, prefix, mode) => new AttrMatchSelector(name, value, prefix, mode) },
            { CombinatorSymbols.InList, (name, value, prefix, mode) => new AttrInListSelector(name, value, prefix, mode) },
            { CombinatorSymbols.InToken, (name, value, prefix, mode) => new AttrInTokenSelector(name, value, prefix, mode) },
            { CombinatorSymbols.Begins, (name, value, prefix, mode) => new AttrStartsWithSelector(name, value, prefix, mode) },
            { CombinatorSymbols.Ends, (name, value, prefix, mode) => new AttrEndsWithSelector(name, value, prefix, mode) },
            { CombinatorSymbols.InText, (name, value, prefix, mode) => new AttrContainsSelector(name, value, prefix, mode) },
            { CombinatorSymbols.Unlike, (name, value, prefix, mode) => new AttrNotMatchSelector(name, value, prefix, mode) },
        };

        /// <summary>
        /// Represents a creator delegate for creating an attribute selector.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <param name="prefix">The prefix for the attribute.</param>
        /// <param name="insensitive">Sets the evaluation mode.</param>
        /// <returns></returns>
        public delegate ISelector Creator(String name, String value, String prefix, Boolean insensitive);

        /// <summary>
        /// Registers a new creator for the specified combinator.
        /// Throws an exception if another creator for the given
        /// combinator is already added.
        /// </summary>
        /// <param name="combinator">The used CSS combinator.</param>
        /// <param name="creator">The creator to invoke.</param>
        public void Register(String combinator, Creator creator) => _creators.Add(combinator, creator);

        /// <summary>
        /// Unregisters an existing creator for the given combinator.
        /// </summary>
        /// <param name="combinator">The used CSS combinator.</param>
        /// <returns>The registered creator, if any.</returns>
        public Creator Unregister(String combinator)
        {
            if (_creators.TryGetValue(combinator, out var creator))
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
        /// <param name="insensitive">Should be evaluated insensitive.</param>
        /// <returns>The selector with the given options.</returns>
        protected virtual ISelector CreateDefault(String name, String value, String prefix, Boolean insensitive) => new AttrAvailableSelector(name, prefix);

        /// <summary>
        /// Creates the associated CSS attribute selector.
        /// </summary>
        /// <param name="combinator">The used CSS combinator.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The used value, if any.</param>
        /// <param name="prefix">The given prefix, if any.</param>
        /// <param name="insensitive">Should be evaluated insensitive.</param>
        /// <returns>The associated selector.</returns>
        public ISelector Create(String combinator, String name, String value, String prefix, Boolean insensitive)
        {
            if (!insensitive && insensitiveAttributes.Contains(name))
            {
                // for some known attributes we need to force insensitive
                insensitive = true;
            }

            if (_creators.TryGetValue(combinator, out var creator))
            {
                return creator.Invoke(name, value, prefix, insensitive);
            }

            return CreateDefault(name, value, prefix, insensitive);
        }
    }
}
