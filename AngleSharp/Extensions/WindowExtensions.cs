namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using System;
    using System.Linq;

    /// <summary>
    /// A set of useful extension methods for the Window class.
    /// </summary>
    static class WindowExtensions
    {
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

        public static CssStyleDeclaration ComputeCascadedStyle(this IWindow window, IElement element)
        {
            // Resolves the cascade by ordering after specifity [same specifity = more recent specification wins]
            // --> cascaded
            throw new NotImplementedException();
        }

        public static CssStyleDeclaration ComputeUsedStyle(this IWindow window, IElement element)
        {
            // Is this somewhere implemented ? I don't know what that should be.
            // --> used (?)
            throw new NotImplementedException();
        }

        public static StyleCollection GetStyleCollection(this IWindow window)
        {
            var device = new RenderDevice(window.OuterWidth, window.OuterHeight);
            var stylesheets = window.Document.GetStyleSheets().OfType<CssStyleSheet>();
            return new StyleCollection(stylesheets, device);
        }
    }
}
