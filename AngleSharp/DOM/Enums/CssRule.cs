using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// The set of possible CSS rules.
    /// </summary>
    public enum CssRule : ushort
    {  
        /// <summary>
        /// The rule is not known and cannot be used.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// A standard style rule.
        /// </summary>
        Style = 1,
        /// <summary>
        /// Defines a @charset rule.
        /// </summary>
        Charset = 2,
        /// <summary>
        /// The @import rule.
        /// </summary>
        Import = 3,
        /// <summary>
        /// An @media rule.
        /// </summary>
        Media = 4,
        /// <summary>
        /// This is for definining @font-face rule.
        /// </summary>
        FontFace = 5,
        /// <summary>
        /// In printing we require the @page rule.
        /// </summary>
        Page = 6,
        /// <summary>
        /// For animations @keyframes is very important.
        /// </summary>
        Keyframes = 7,
        /// <summary>
        /// Keyframes require one or more @keyframe rule(s) to be used.
        /// </summary>
        Keyframe = 8,
        /// <summary>
        /// Declaring namespaces is possible @namespace.
        /// </summary>
        Namespace = 10,
        /// <summary>
        /// The @counter-style rule for styling counters.
        /// </summary>
        CounterStyle = 11,
        /// <summary>
        /// Checking for CSS support using @support.
        /// </summary>
        Supports = 12,
        /// <summary>
        /// Changing document (location) specific rules with @document.
        /// </summary>
        Document = 13,
        /// <summary>
        /// This @font-feature-values is still very complicated.
        /// </summary>
        FontFeatureValues = 14,
        /// <summary>
        /// Defines the @viewport rule for responsive design.
        /// </summary>
        Viewport = 15,
        /// <summary>
        /// Creating a CSS region with @region.
        /// </summary>
        RegionStyle = 16
    }
}
