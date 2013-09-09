using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Fore more information about CSS properties
    /// see http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    [DOM("CSSProperty")]
    public sealed class CSSProperty : ICSSObject
    {
        #region Members

        String _name;
        CSSValue _value;
        Boolean _important;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS property.
        /// </summary>
        /// <param name="name"></param>
        internal CSSProperty(String name)
        {
            _name = name;
        }

        #endregion

		#region Factory

		/// <summary>
		/// Creates a new property.
		/// </summary>
		/// <param name="name">The name of the property.</param>
		/// <returns>The created property</returns>
		static internal CSSProperty Create(String name)
		{
			switch (name.ToLower())
			{
				case "azimuth":
				case "animation":
				case "animation-delay":
				case "animation-direction":
				case "animation-duration":
				case "animation-fill-mode":
				case "animation-iteration-count":
				case "animation-name":
				case "animation-play-state":
				case "animation-timing-function":
				case "background-attachment":
				case "background-color":
				case "background-clip":
				case "background-origin":
				case "background-size":
				case "background-image":
				case "background-position":
				case "background-repeat":
				case "background":
				case "border-color":
				case "border-spacing":
				case "border-collapse":
				case "border-style":
				case "border-radius":
				case "box-shadow":
				case "box-decoration-break":
				case "break-after":
				case "break-before":
				case "break-inside":
				case "backface-visibility":
				case "border-top-left-radius":
				case "border-top-right-radius":
				case "border-bottom-left-radius":
				case "border-bottom-right-radius":
				case "border-image":
				case "border-image-outset":
				case "border-image-repeat":
				case "border-image-source":
				case "border-image-slice":
				case "border-image-width":
				case "border-top":
				case "border-right":
				case "border-bottom":
				case "border-left":
				case "border-top-color":
				case "border-left-color":
				case "border-right-color":
				case "border-bottom-color":
				case "border-top-style":
				case "border-left-style":
				case "border-right-style":
				case "border-bottom-style":
				case "border-top-width":
				case "border-left-width":
				case "border-right-width":
				case "border-bottom-width":
				case "border-width":
				case "border":
				case "bottom":
				case "columns":
				case "column-count":
				case "column-fill":
				case "column-gap":
				case "column-rule-color":
				case "column-rule-style":
				case "column-rule-width":
				case "column-span":
				case "column-width":
				case "caption-side":
				case "clear":
				case "clip":
				case "color":
				case "content":
				case "counter-increment":
				case "counter-reset":
				case "cue-after":
				case "cue-before":
				case "cue":
				case "cursor":
				case "direction":
				case "display":
				case "elevation":
				case "empty-cells":
				case "float":
				case "font-family":
				case "font-size":
				case "font-style":
				case "font-variant":
				case "font-weight":
				case "font":
				case "height":
				case "left":
				case "letter-spacing":
				case "line-height":
				case "list-style-image":
				case "list-style-position":
				case "list-style-type":
				case "list-style":
				case "marquee-direction":
				case "marquee-play-count":
				case "marquee-speed":
				case "marquee-style":
				case "margin-right":
				case "margin-left":
				case "margin-top":
				case "margin-bottom":
				case "margin":
				case "max-height":
				case "max-width":
				case "min-height":
				case "min-width":
				case "opacity":
				case "orphans":
				case "outline-color":
				case "outline-style":
				case "outline-width":
				case "outline":
				case "overflow":
				case "padding-top":
				case "padding-right":
				case "padding-left":
				case "padding-bottom":
				case "padding":
				case "page-break-after":
				case "page-break-before":
				case "page-break-inside":
				case "pause-after":
				case "pause-before":
				case "pause":
				case "perspective":
				case "perspective-origin":
				case "pitch-range":
				case "pitch":
				case "play-during":
				case "position":
				case "quotes":
				case "richness":
				case "right":
				case "speak-header":
				case "speak-numeral":
				case "speak-punctuation":
				case "speak":
				case "speech-rate":
				case "stress":
				case "table-layout":
				case "text-align":
				case "text-decoration":
				case "text-indent":
				case "text-transform":
				case "transform":
				case "transform-origin":
				case "transform-style":
				case "transition":
				case "transition-delay":
				case "transition-duration":
				case "transition-timing-function":
				case "transition-property":
				case "top":
				case "unicode-bidi":
				case "vertical-align":
				case "visibility":
				case "voice-family":
				case "volume":
				case "white-space":
				case "widows":
				case "width":
				case "word-spacing":
				case "z-index":
				default:
					return new CSSProperty(name);
			}
		}

		#endregion

		#region Properties

		/// <summary>
        /// Gets the name of the property.
        /// </summary>
        [DOM("name")]
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets or sets the value of the property.
        /// </summary>
        [DOM("value")]
        public CSSValue Value
        {
            get { return _value ?? CSSValue.Inherit; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets or sets if the !important flag has been set.
        /// </summary>
        [DOM("important")]
        public Boolean Important
        {
            get { return _important; }
            set { _important = value; }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns a CSS code representation of the property.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var value = _name + ":" + _value.ToCss();

            if (_important)
                value += " !important";

            return value;
        }

        #endregion
    }
}
