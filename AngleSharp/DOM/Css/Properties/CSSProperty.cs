using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Fore more information about CSS properties
    /// see http://www.w3.org/TR/CSS21/propidx.html.
    /// </summary>
    [DOM("CSSProperty")]
    public sealed class CSSProperty : ICssObject
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
                //case PropertyNames.AZIMUTH:
                //case PropertyNames.ANIMATION:
                //case PropertyNames.ANIMATION_DELAY:
                //case PropertyNames.ANIMATION_DIRECTION:
                //case PropertyNames.ANIMATION_DURATION:
                //case PropertyNames.ANIMATION_FILL_MODE:
                //case PropertyNames.ANIMATION_ITERATION_COUNT:
                //case PropertyNames.ANIMATION_NAME:
                //case PropertyNames.ANIMATION_PLAY_STATE:
                //case PropertyNames.ANIMATION_TIMING_FUNCTION:
                //case PropertyNames.BACKGROUND_ATTACHMENT:
                //case PropertyNames.BACKGROUND_COLOR:
                //case PropertyNames.BACKGROUND_CLIP:
                //case PropertyNames.BACKGROUND_ORIGIN:
                //case PropertyNames.BACKGROUND_SIZE:
                //case PropertyNames.BACKGROUND_IMAGE:
                //case PropertyNames.BACKGROUND_POSITION:
                //case PropertyNames.BACKGROUND_REPEAT:
                //case PropertyNames.BACKGROUND:
                //case PropertyNames.BORDER_COLOR:
                //case PropertyNames.BORDER_SPACING:
                //case PropertyNames.BORDER_COLLAPSE:
                //case PropertyNames.BORDER_STYLE:
                //case PropertyNames.BORDER_RADIUS:
                //case PropertyNames.BOX_SHADOW:
                //case PropertyNames.BOX_DECORATION_BREAK:
                //case PropertyNames.BREAK_AFTER:
                //case PropertyNames.BREAK_BEFORE:
                //case PropertyNames.BREAK_INSIDE:
                //case PropertyNames.BACKFACE_VISIBILITY:
                //case PropertyNames.BORDER_TOP_LEFT_RADIUS:
                //case PropertyNames.BORDER_TOP_RIGHT_RADIUS:
                //case PropertyNames.BORDER_BOTTOM_LEFT_RADIUS:
                //case PropertyNames.BORDER_BOTTOM_RIGHT_RADIUS:
                //case PropertyNames.BORDER_IMAGE:
                //case PropertyNames.BORDER_IMAGE_OUTSET:
                //case PropertyNames.BORDER_IMAGE_REPEAT:
                //case PropertyNames.BORDER_IMAGE_SOURCE:
                //case PropertyNames.BORDER_IMAGE_SLICE:
                //case PropertyNames.BORDER_IMAGE_WIDTH:
                //case PropertyNames.BORDER_TOP:
                //case PropertyNames.BORDER_RIGHT:
                //case PropertyNames.BORDER_BOTTOM:
                //case PropertyNames.BORDER_LEFT:
                //case PropertyNames.BORDER_TOP_COLOR:
                //case PropertyNames.BORDER_LEFT_COLOR:
                //case PropertyNames.BORDER_RIGHT_COLOR:
                //case PropertyNames.BORDER_BOTTOM_COLOR:
                //case PropertyNames.BORDER_TOP_STYLE:
                //case PropertyNames.BORDER_LEFT_STYLE:
                //case PropertyNames.BORDER_RIGHT_STYLE:
                //case PropertyNames.BORDER_BOTTOM_STYLE:
                //case PropertyNames.BORDER_TOP_WIDTH:
                //case PropertyNames.BORDER_LEFT_WIDTH:
                //case PropertyNames.BORDER_RIGHT_WIDTH:
                //case PropertyNames.BORDER_BOTTOM_WIDTH:
                //case PropertyNames.BORDER_WIDTH:
                //case PropertyNames.BORDER:
                //case PropertyNames.BOTTOM:
                //case PropertyNames.COLUMNS:
                //case PropertyNames.COLUMN_COUNT:
                //case PropertyNames.COLUMN_FILL:
                //case PropertyNames.COLUMN_GAP:
                //case PropertyNames.COLUMN_RULE_COLOR:
                //case PropertyNames.COLUMN_RULE_STYLE:
                //case PropertyNames.COLUMN_RULE_WIDTH:
                //case PropertyNames.COLUMN_SPAN:
                //case PropertyNames.COLUMN_WIDTH:
                //case PropertyNames.CAPTION_SIDE:
                //case PropertyNames.CLEAR:
                //case PropertyNames.CLIP:
                //case PropertyNames.COLOR:
                //case PropertyNames.CONTENT:
                //case PropertyNames.COUNTER_INCREMENT:
                //case PropertyNames.COUNTER_RESET:
                //case PropertyNames.CUE_AFTER:
                //case PropertyNames.CUE_BEFORE:
                //case PropertyNames.CUE:
                //case PropertyNames.CURSOR:
                //case PropertyNames.DIRECTION:
                //case PropertyNames.DISPLAY:
                //case PropertyNames.ELEVATION:
                //case PropertyNames.EMPTY_CELLS:
                //case PropertyNames.FLOAT:
                //case PropertyNames.FONT_FAMILY:
                //case PropertyNames.FONT_SIZE:
                //case PropertyNames.FONT_STYLE:
                //case PropertyNames.FONT_VARIANT:
                //case PropertyNames.FONT_WEIGHT:
                //case PropertyNames.FONT:
                //case PropertyNames.HEIGHT:
                //case PropertyNames.LEFT:
                //case PropertyNames.LETTER_SPACING:
                //case PropertyNames.LINE_HEIGHT:
                //case PropertyNames.LIST_STYLE_IMAGE:
                //case PropertyNames.LIST_STYLE_POSITION:
                //case PropertyNames.LIST_STYLE_TYPE:
                //case PropertyNames.LIST_STYLE:
                //case PropertyNames.MARQUEE_DIRECTION:
                //case PropertyNames.MARQUEE_PLAY_COUNT:
                //case PropertyNames.MARQUEE_SPEED:
                //case PropertyNames.MARQUEE_STYLE:
                //case PropertyNames.MARGIN_RIGHT:
                //case PropertyNames.MARGIN_LEFT:
                //case PropertyNames.MARGIN_TOP:
                //case PropertyNames.MARGIN_BOTTOM:
                //case PropertyNames.MARGIN:
                //case PropertyNames.MAX_HEIGHT:
                //case PropertyNames.MAX_WIDTH:
                //case PropertyNames.MIN_HEIGHT:
                //case PropertyNames.MIN_WIDTH:
                //case PropertyNames.OPACITY:
                //case PropertyNames.ORPHANS:
                //case PropertyNames.OUTLINE_COLOR:
                //case PropertyNames.OUTLINE_STYLE:
                //case PropertyNames.OUTLINE_WIDTH:
                //case PropertyNames.OUTLINE:
                //case PropertyNames.OVERFLOW:
                //case PropertyNames.PADDING_TOP:
                //case PropertyNames.PADDING_RIGHT:
                //case PropertyNames.PADDING_LEFT:
                //case PropertyNames.PADDING_BOTTOM:
                //case PropertyNames.PADDING:
                //case PropertyNames.PAGE_BREAK_AFTER:
                //case PropertyNames.PAGE_BREAK_BEFORE:
                //case PropertyNames.PAGE_BREAK_INSIDE:
                //case PropertyNames.PERSPECTIVE:
                //case PropertyNames.PERSPECTIVE_ORIGIN:
                //case PropertyNames.POSITION:
                //case PropertyNames.QUOTES:
                //case PropertyNames.RIGHT:
                //case PropertyNames.TABLE_LAYOUT:
                //case PropertyNames.TEXT_ALIGN:
                //case PropertyNames.TEXT_DECORATION:
                //case PropertyNames.TEXT_INDENT:
                //case PropertyNames.TEXT_TRANSFORM:
                //case PropertyNames.TRANSFORM:
                //case PropertyNames.TRANSFORM_ORIGIN:
                //case PropertyNames.TRANSFORM_STYLE:
                //case PropertyNames.TRANSITION:
                //case PropertyNames.TRANSITION_DELAY:
                //case PropertyNames.TRANSITION_DURATION:
                //case PropertyNames.TRANSITION_TIMING_FUNCTION:
                //case PropertyNames.TRANSITION_PROPERTY:
                //case PropertyNames.TOP:
                //case PropertyNames.UNICODE_BIDI:
                //case PropertyNames.VERTICAL_ALIGN:
                //case PropertyNames.VISIBILITY:
                //case PropertyNames.WHITE_SPACE:
                //case PropertyNames.WIDOWS:
                //case PropertyNames.WIDTH:
                //case PropertyNames.WORD_SPACING:
                //case PropertyNames.Z_INDEX:

				default:
					return new CSSProperty(name);
			}
		}

		#endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the property hsa a value.
        /// </summary>
        internal Boolean HasValue
        {
            get { return _value != null; }
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
