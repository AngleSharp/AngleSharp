namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    /// <summary>
    /// A set of useful extension methods for the Window class.
    /// </summary>
    [DebuggerStepThrough]
    static class WindowExtensions
    {
        #region Methods

        public static CssStyleDeclaration ComputeDefaultStyle(this IWindow window, IElement element)
        {
            // Ignores transitions and animations
            // Ignores author-level style
            // Ignores presentational hints (e.g. bgColor)
            // Ignores inline styles
            // --> computed
            throw new NotImplementedException();
        }

        public static CssStyleDeclaration ComputeRawStyle(this IWindow window, IElement element)
        {
            // Computes the cascaded style first
            // Places current device info
            // Replaces the relative values with absolute ones
            // --> computed
            throw new NotImplementedException();
        }

        public static CssStyleDeclaration ComputeUsedStyle(this IWindow window, IElement element)
        {
            // Is this somewhere implemented ? I don't know what that should be.
            // --> used (?)
            throw new NotImplementedException();
        }

        /// <summary>
        /// Computes the cascaded style, i.e. resolves the cascade by ordering after specifity.
        /// Two rules with the same specifity are ordered according to their appearance. The more
        /// recent declaration wins. Inheritance is not taken into account.
        /// </summary>
        /// <param name="styleCollection">The style rules to apply.</param>
        /// <param name="element">The element to compute the cascade for.</param>
        /// <returns>Returns the cascaded read-only style declaration.</returns>
        public static CssStyleDeclaration ComputeCascadedStyle(this StyleCollection styleCollection, IElement element)
        {
            var style = new CssStyleDeclaration();
            var rules = styleCollection.SortBySpecifity(element);

            foreach (var rule in rules)
                style.SetDeclarations(rule.Style);

            return new CssStyleDeclaration(style.Declarations);
        }

        /// <summary>
        /// Generates the style collection for the given window object.
        /// </summary>
        /// <param name="window">The window to host the style collection.</param>
        /// <returns>The device-bound style collection.</returns>
        public static StyleCollection GetStyleCollection(this IWindow window)
        {
            var device = new RenderDevice(window.OuterWidth, window.OuterHeight);
            var stylesheets = window.Document.GetStyleSheets().OfType<CssStyleSheet>();
            return new StyleCollection(stylesheets, device);
        }

        #endregion

        #region Helpers

        static IEnumerable<CssStyleRule> SortBySpecifity(this IEnumerable<CssStyleRule> rules, IElement element)
        {
            return rules.Where(m => m.Selector.Match(element)).OrderBy(m => m.Selector.Specifity);
        }

        #endregion
    }
}
