namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines a set of extension methods for style sheets.
    /// </summary>
    public static class StyleSheetExtensions
    {
        #region Adding Rules

        private static readonly Dictionary<String, CssRuleType> RuleMapping = new Dictionary<String, CssRuleType>
        {
            { typeof(ICssCharsetRule).FullName, CssRuleType.Charset },
            { typeof(ICssCounterStyleRule).FullName, CssRuleType.CounterStyle },
            { typeof(ICssDocumentRule).FullName, CssRuleType.Document },
            { typeof(ICssFontFaceRule).FullName, CssRuleType.FontFace },
            { typeof(ICssFontFeatureValuesRule).FullName, CssRuleType.FontFeatureValues },
            { typeof(ICssImportRule).FullName, CssRuleType.Import },
            { typeof(ICssKeyframeRule).FullName, CssRuleType.Keyframe },
            { typeof(ICssKeyframesRule).FullName, CssRuleType.Keyframes },
            { typeof(ICssMediaRule).FullName, CssRuleType.Media },
            { typeof(ICssNamespaceRule).FullName, CssRuleType.Namespace },
            { typeof(ICssPageRule).FullName, CssRuleType.Page },
            { typeof(ICssStyleRule).FullName, CssRuleType.Style },
            { typeof(ICssSupportsRule).FullName, CssRuleType.Supports },
        };

        /// <summary>
        /// Creates a new CSS rule and appends it to the current node.
        /// </summary>
        /// <typeparam name="TRule">The type of rule to create.</typeparam>
        /// <param name="creator">The host of the rule.</param>
        /// <returns>The created rule.</returns>
        public static TRule AddNewRule<TRule>(this ICssRuleCreator creator)
            where TRule : ICssRule
        {
            var ruleFullName = typeof(TRule).FullName;
            var ruleType = CssRuleType.Unknown;
            
            if (RuleMapping.TryGetValue(ruleFullName, out ruleType))
            {
                var rule = creator.AddNewRule(ruleType);

                if (rule is TRule)
                {
                    return (TRule)rule;
                }
            }

            return default(TRule);
        }

        /// <summary>
        /// Creates a new CSS style rule and appends it to the current node. The
        /// given selector and declarations are set in the beginning.
        /// </summary>
        /// <param name="creator">The host of the rule.</param>
        /// <param name="selector">The selector to use, if any.</param>
        /// <param name="declarations">The declarations to set, if any.</param>
        /// <returns>The created style rule.</returns>
        public static ICssStyleRule AddNewStyle(this ICssRuleCreator creator, String selector = null, IDictionary<String, String> declarations = null)
        {
            var rule = creator.AddNewRule<ICssStyleRule>();

            if (!String.IsNullOrEmpty(selector))
            {
                rule.SelectorText = selector;
            }

            if (declarations != null)
            {
                foreach (var declaration in declarations)
                {
                    rule.Style.SetProperty(declaration.Key, declaration.Value);
                }
            }

            return rule;
        }

        /// <summary>
        /// Creates a new CSS style rule and appends it to the current node.
        /// The given selector and declarations from an anonymous object are set
        /// in the beginning.
        /// </summary>
        /// <param name="creator">The host of the rule.</param>
        /// <param name="selector">The selector to use.</param>
        /// <param name="declarations">The declarations to set.</param>
        /// <returns>The created style rule.</returns>
        public static ICssStyleRule AddNewStyle(this ICssRuleCreator creator, String selector, Object declarations)
        {
            return creator.AddNewStyle(selector, declarations.ToDictionary());
        }

        #endregion

        #region Obtaining Rules

        /// <summary>
        /// Gets all rules that are of the provided type.
        /// </summary>
        /// <typeparam name="TRule">The type of rules to get.</typeparam>
        /// <param name="sheets">The list of stylesheets to consider.</param>
        /// <returns>The list of rules.</returns>
        public static IEnumerable<TRule> RulesOf<TRule>(this IEnumerable<IStyleSheet> sheets)
            where TRule : ICssRule
        {
            if (sheets == null)
                throw new ArgumentNullException(nameof(sheets));

            return sheets.Where(m => !m.IsDisabled).OfType<ICssStyleSheet>().SelectMany(m => m.Rules).OfType<TRule>();
        }

        /// <summary>
        /// Gets all style rules that have the same selector text.
        /// </summary>
        /// <param name="sheets">The list of stylesheets to consider.</param>
        /// <param name="selector">The selector to compare to.</param>
        /// <returns>The list of style rules.</returns>
        public static IEnumerable<ICssStyleRule> StylesWith(this IEnumerable<IStyleSheet> sheets, ISelector selector)
        {
            if (selector == null)
                throw new ArgumentNullException(nameof(selector));

            var selectorText = selector.Text;
            return sheets.RulesOf<ICssStyleRule>().Where(m => m.SelectorText == selectorText);
        }

        /// <summary>
        /// Gets the associated document of the sheet if any.
        /// </summary>
        /// <param name="sheet">The sheet.</param>
        /// <returns>The associated document, if any.</returns>
        public static IDocument GetDocument(this IStyleSheet sheet)
        {
            return sheet?.OwnerNode?.Owner;
        }

        /// <summary>
        /// Gets the comments contained in the sheet, if any.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all comments.</returns>
        public static IEnumerable<ICssComment> GetComments(this ICssNode node)
        {
            return node.GetAll<ICssComment>();
        }

        /// <summary>
        /// Gets all descendents of the provided node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all contained nodes.</returns>
        public static IEnumerable<ICssNode> GetAllDescendents(this ICssNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            return node.Children.SelectMany(m => m.GetAllDescendents());
        }

        /// <summary>
        /// Gets all descendents of the provided node.
        /// </summary>
        /// <param name="node">The node to examine.</param>
        /// <returns>An iterator over all contained nodes.</returns>
        public static IEnumerable<T> GetAll<T>(this ICssNode node)
            where T : IStyleFormattable
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node is T)
            {
                yield return (T)node;
            }

            foreach (var entity in node.Children.SelectMany(m => m.GetAll<T>()))
            {
                yield return entity;
            }
        }

        #endregion

        #region Linked Stylesheet States

        /// <summary>
        /// Gets if the link contains a stylesheet that is regarded persistent.
        /// </summary>
        /// <param name="link">The link to examine.</param>
        /// <returns>True if the link hosts a persistent stylesheet.</returns>
        public static Boolean IsPersistent(this IHtmlLinkElement link)
        {
            return link.Relation.Isi(LinkRelNames.StyleSheet) && link.Title == null;
        }

        /// <summary>
        /// Gets if the link contains a stylesheet that is regarded preferred.
        /// </summary>
        /// <param name="link">The link to examine.</param>
        /// <returns>True if the link hosts a preferred stylesheet.</returns>
        public static Boolean IsPreferred(this IHtmlLinkElement link)
        {
            return link.Relation.Isi(LinkRelNames.StyleSheet) && link.Title != null;
        }

        /// <summary>
        /// Gets if the link contains a stylesheet that is regarded alternate.
        /// </summary>
        /// <param name="link">The link to examine.</param>
        /// <returns>True if the link hosts an alternate stylesheet.</returns>
        public static Boolean IsAlternate(this IHtmlLinkElement link)
        {
            var relation = link.RelationList;
            return relation.Contains(LinkRelNames.StyleSheet) && relation.Contains(LinkRelNames.Alternate) && link.Title != null;
        }

        #endregion
    }
}
