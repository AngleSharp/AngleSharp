namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Css.Properties;
    using System;

    internal class CSSFactory
    {
        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created property</returns>
        public static CSSProperty Create(String name)
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
                case PropertyNames.BOX_SHADOW:                   return new CSSBoxShadowProperty();
                case PropertyNames.BOX_DECORATION_BREAK:         return new CSSBoxDecorationBreak();
                case PropertyNames.BREAK_AFTER:                  return new CSSBreakAfterProperty();
                case PropertyNames.BREAK_BEFORE:                 return new CSSBreakBeforeProperty();
                case PropertyNames.BREAK_INSIDE:                 return new CSSBreakInsideProperty();
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
                case PropertyNames.BORDER_COLLAPSE:              return new CSSBorderCollapseProperty();
                case PropertyNames.BOTTOM:                       return new CSSBottomProperty();
                //case PropertyNames.COLUMNS:
                //case PropertyNames.COLUMN_COUNT:
                //case PropertyNames.COLUMN_FILL:
                //case PropertyNames.COLUMN_GAP:
                //case PropertyNames.COLUMN_RULE_COLOR:
                //case PropertyNames.COLUMN_RULE_STYLE:
                //case PropertyNames.COLUMN_RULE_WIDTH:
                //case PropertyNames.COLUMN_SPAN:
                //case PropertyNames.COLUMN_WIDTH:
                case PropertyNames.CAPTION_SIDE:                 return new CSSCaptionSideProperty();
                case PropertyNames.CLEAR:                        return new CSSClearProperty();
                //case PropertyNames.CLIP:
                //case PropertyNames.COLOR:
                //case PropertyNames.CONTENT:
                //case PropertyNames.COUNTER_INCREMENT:
                //case PropertyNames.COUNTER_RESET:
                //case PropertyNames.CUE_AFTER:
                //case PropertyNames.CUE_BEFORE:
                //case PropertyNames.CUE:
                //case PropertyNames.CURSOR:
                case PropertyNames.DIRECTION:                    return new CSSDirectionProperty();
                case PropertyNames.DISPLAY:                      return new CSSDisplayProperty();
                //case PropertyNames.ELEVATION:
                case PropertyNames.EMPTY_CELLS:                  return new CSSEmptyCellsProperty();
                case PropertyNames.FLOAT:                        return new CSSFloatProperty();
                //case PropertyNames.FONT_FAMILY:
                //case PropertyNames.FONT_SIZE:
                //case PropertyNames.FONT_STYLE:
                //case PropertyNames.FONT_VARIANT:
                //case PropertyNames.FONT_WEIGHT:
                //case PropertyNames.FONT:
                case PropertyNames.HEIGHT:                       return new CSSHeightProperty();
                case PropertyNames.LEFT:                         return new CSSLeftProperty();
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
                case PropertyNames.OPACITY:                      return new CSSOpacityProperty();
                //case PropertyNames.ORPHANS:
                //case PropertyNames.OUTLINE_COLOR:
                //case PropertyNames.OUTLINE_STYLE:
                //case PropertyNames.OUTLINE_WIDTH:
                //case PropertyNames.OUTLINE:
                case PropertyNames.OVERFLOW:                     return new CSSOverflowProperty();
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
                case PropertyNames.POSITION:                     return new CSSPositionProperty();
                //case PropertyNames.QUOTES:
                case PropertyNames.RIGHT:                        return new CSSRightProperty();
                case PropertyNames.TABLE_LAYOUT:                 return new CSSTableLayoutProperty();
                case PropertyNames.TEXT_ALIGN:                   return new CSSTextAlignProperty();
                //case PropertyNames.TEXT_DECORATION:
                //case PropertyNames.TEXT_INDENT:
                case PropertyNames.TEXT_TRANSFORM:               return new CSSTextTransformProperty();
                //case PropertyNames.TRANSFORM:
                //case PropertyNames.TRANSFORM_ORIGIN:
                //case PropertyNames.TRANSFORM_STYLE:
                //case PropertyNames.TRANSITION:
                //case PropertyNames.TRANSITION_DELAY:
                //case PropertyNames.TRANSITION_DURATION:
                //case PropertyNames.TRANSITION_TIMING_FUNCTION:
                //case PropertyNames.TRANSITION_PROPERTY:
                case PropertyNames.TOP:                          return new CSSTopProperty();
                case PropertyNames.UNICODE_BIDI:                 return new CSSUnicodeBidiProperty();
                case PropertyNames.VERTICAL_ALIGN:               return new CSSVerticalAlignProperty();
                case PropertyNames.VISIBILITY:                   return new CSSVisibilityProperty();
                //case PropertyNames.WHITE_SPACE:
                //case PropertyNames.WIDOWS:
                case PropertyNames.WIDTH:                        return new CSSWidthProperty();
                //case PropertyNames.WORD_SPACING:
                case PropertyNames.Z_INDEX:                      return new CSSZIndexProperty();
                default:                                         return new CSSProperty(name);
            }
        }
    }
}
