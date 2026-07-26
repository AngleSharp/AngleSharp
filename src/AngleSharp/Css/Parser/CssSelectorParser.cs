namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Allows the simply creation of CSS selectors.
    /// </summary>
    public class CssSelectorParser : ICssSelectorParser
    {
        private const Int32 MaxCachedSelectors = 256;

        private readonly IAttributeSelectorFactory _attribute;
        private readonly IPseudoClassSelectorFactory _pseudoClass;
        private readonly IPseudoElementSelectorFactory _pseudoElement;
        private readonly Dictionary<String, LinkedListNode<CachedSelector>> _selectorCache = new(StringComparer.Ordinal);
        private readonly LinkedList<CachedSelector> _selectorCacheOrder = [];
        private readonly Object _cacheLock = new();

        /// <summary>
        /// Creates a new selector parser.
        /// </summary>
        public CssSelectorParser()
            : this(default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new selector parser using the different factories.
        /// </summary>
        internal CssSelectorParser(IBrowsingContext? context)
        {
            context ??= BrowsingContext.NewFrom<ICssSelectorParser>(this);

            _attribute = context.GetFactory<IAttributeSelectorFactory>();
            _pseudoClass = context.GetFactory<IPseudoClassSelectorFactory>();
            _pseudoElement = context.GetFactory<IPseudoElementSelectorFactory>();
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public ISelector? ParseSelector(String selectorText)
        {
            if (TryGetCached(selectorText, out var cachedSelector))
            {
                return cachedSelector;
            }

            var source = new StringSource(selectorText);
            var tokenizer = new CssTokenizer(source);
            var constructor = new CssSelectorConstructor(tokenizer, _attribute, _pseudoClass, _pseudoElement);
            var selector = constructor.Parse();

            if (selector is not null)
            {
                AddCached(selectorText, selector);
            }

            return selector;
        }

        private Boolean TryGetCached(String selectorText, out ISelector selector)
        {
            lock (_cacheLock)
            {
                if (_selectorCache.TryGetValue(selectorText, out var node))
                {
                    _selectorCacheOrder.Remove(node);
                    _selectorCacheOrder.AddFirst(node);
                    selector = node.Value.Selector;
                    return true;
                }
            }

            selector = null!;
            return false;
        }

        private void AddCached(String selectorText, ISelector selector)
        {
            lock (_cacheLock)
            {
                if (_selectorCache.TryGetValue(selectorText, out var existingNode))
                {
                    existingNode.Value = new CachedSelector(selectorText, selector);
                    _selectorCacheOrder.Remove(existingNode);
                    _selectorCacheOrder.AddFirst(existingNode);
                    return;
                }

                var node = new LinkedListNode<CachedSelector>(new CachedSelector(selectorText, selector));
                _selectorCacheOrder.AddFirst(node);
                _selectorCache[selectorText] = node;

                if (_selectorCache.Count > MaxCachedSelectors)
                {
                    var last = _selectorCacheOrder.Last!;
                    _selectorCache.Remove(last.Value.Key);
                    _selectorCacheOrder.RemoveLast();
                }
            }
        }

        private readonly struct CachedSelector(String key, ISelector selector)
        {
            public String Key { get; } = key;

            public ISelector Selector { get; } = selector;
        }
    }
}
