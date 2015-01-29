namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The set of possible CSS rules.
    /// </summary>
    public enum CssRuleType : ushort
    {  
        /// <summary>
        /// The rule is not known and cannot be used.
        /// </summary>
        Unknown = 0,
        /// <summary>
        /// A standard style rule.
        /// </summary>
        [DomName("STYLE_RULE")]
        Style = 1,
        /// <summary>
        /// Defines a @charset rule.
        /// </summary>
        [DomName("CHARSET_RULE")]
        Charset = 2,
        /// <summary>
        /// The @import rule.
        /// </summary>
        [DomName("IMPORT_RULE")]
        Import = 3,
        /// <summary>
        /// An @media rule.
        /// </summary>
        [DomName("MEDIA_RULE")]
        Media = 4,
        /// <summary>
        /// This is for definining @font-face rule.
        /// </summary>
        [DomName("FONT_FACE_RULE")]
        FontFace = 5,
        /// <summary>
        /// In printing we require the @page rule.
        /// </summary>
        [DomName("PAGE_RULE")]
        Page = 6,
        /// <summary>
        /// For animations @keyframes is very important.
        /// </summary>
        [DomName("KEYFRAMES_RULE")]
        Keyframes = 7,
        /// <summary>
        /// Keyframes require one or more @keyframe rule(s) to be used.
        /// </summary>
        [DomName("KEYFRAME_RULE")]
        Keyframe = 8,
        /// <summary>
        /// Declaring namespaces is possible @namespace.
        /// </summary>
        [DomName("NAMESPACE_RULE")]
        Namespace = 10,
        /// <summary>
        /// The @counter-style rule for styling counters.
        /// </summary>
        [DomName("COUNTER_STYLE_RULE")]
        CounterStyle = 11,
        /// <summary>
        /// Checking for CSS support using @support.
        /// </summary>
        [DomName("SUPPORTS_RULE")]
        Supports = 12,
        /// <summary>
        /// Changing document (location) specific rules with @document.
        /// </summary>
        [DomName("DOCUMENT_RULE")]
        Document = 13,
        /// <summary>
        /// This @font-feature-values is still very complicated.
        /// </summary>
        [DomName("FONT_FEATURE_VALUES_RULE")]
        FontFeatureValues = 14,
        /// <summary>
        /// Defines the @viewport rule for responsive design.
        /// </summary>
        [DomName("VIEWPORT_RULE")]
        Viewport = 15,
        /// <summary>
        /// Creating a CSS region with @region.
        /// </summary>
        [DomName("REGION_STYLE_RULE")]
        RegionStyle = 16
    }
}
