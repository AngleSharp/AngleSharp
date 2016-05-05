namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the counter-style CSS rule.
    /// </summary>
    [DomName("CSSCounterStyleRule")]
    public interface ICssCounterStyleRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the name of the counter.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets or sets the counter system string.
        /// </summary>
        [DomName("system")]
        String System { get; set; }

        /// <summary>
        /// Gets or sets the used symbols.
        /// </summary>
        [DomName("symbols")]
        String Symbols { get; set; }

        /// <summary>
        /// Gets or sets the additional symbols.
        /// </summary>
        [DomName("additiveSymbols")]
        String AdditiveSymbols { get; set; }

        /// <summary>
        /// Gets or sets the prefix for negative values.
        /// </summary>
        [DomName("negative")]
        String Negative { get; set; }

        /// <summary>
        /// Gets or sets the general prefix.
        /// </summary>
        [DomName("prefix")]
        String Prefix { get; set; }

        /// <summary>
        /// Gets or sets the suffix to use.
        /// </summary>
        [DomName("suffix")]
        String Suffix { get; set; }

        /// <summary>
        /// Gets or sets the range setting.
        /// </summary>
        [DomName("range")]
        String Range { get; set; }

        /// <summary>
        /// Gets or sets the padding of a value.
        /// </summary>
        [DomName("pad")]
        String Pad { get; set; }

        /// <summary>
        /// Gets or sets the spoken value.
        /// </summary>
        [DomName("speakAs")]
        String SpeakAs { get; set; }

        /// <summary>
        /// Gets or sets the fallback option.
        /// </summary>
        [DomName("fallback")]
        String Fallback { get; set; }
    }
}
