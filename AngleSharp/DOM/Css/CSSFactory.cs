namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css.Properties;
    using System;

    internal class CSSFactory
    {
        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created property</returns>
        public static CSSProperty Create(String name, CSSStyleDeclaration style)
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
                case PropertyNames.BoxShadow:                   return new CSSBoxShadowProperty { Rule = style };
                case PropertyNames.BoxDecorationBreak:          return new CSSBoxDecorationBreak { Rule = style };
                case PropertyNames.BreakAfter:                  return new CSSBreakAfterProperty { Rule = style };
                case PropertyNames.BreakBefore:                 return new CSSBreakBeforeProperty { Rule = style };
                case PropertyNames.BreakInside:                 return new CSSBreakInsideProperty { Rule = style };
                case PropertyNames.BackfaceVisibility:          return new CSSBackfaceVisibility { Rule = style };
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
                case PropertyNames.BorderCollapse:              return new CSSBorderCollapseProperty { Rule = style };
                case PropertyNames.Bottom:                      return new CSSBottomProperty { Rule = style };
                //case PropertyNames.COLUMNS:
                //case PropertyNames.COLUMN_COUNT:
                //case PropertyNames.COLUMN_FILL:
                //case PropertyNames.COLUMN_GAP:
                //case PropertyNames.COLUMN_RULE_COLOR:
                //case PropertyNames.COLUMN_RULE_STYLE:
                //case PropertyNames.COLUMN_RULE_WIDTH:
                //case PropertyNames.COLUMN_SPAN:
                //case PropertyNames.COLUMN_WIDTH:
                case PropertyNames.CaptionSide:                 return new CSSCaptionSideProperty { Rule = style };
                case PropertyNames.Clear:                       return new CSSClearProperty { Rule = style };
                case PropertyNames.Clip:                        return new CSSClipProperty { Rule = style };
                case PropertyNames.Color:                       return new CSSColorProperty { Rule = style };
                case PropertyNames.Content:                     return new CSSContentProperty { Rule = style };
                //case PropertyNames.COUNTER_INCREMENT:
                //case PropertyNames.COUNTER_RESET:
                //case PropertyNames.CUE_AFTER:
                //case PropertyNames.CUE_BEFORE:
                //case PropertyNames.CUE:
                case PropertyNames.Cursor:                      return new CSSCursorProperty { Rule = style };
                case PropertyNames.Direction:                   return new CSSDirectionProperty { Rule = style };
                case PropertyNames.Display:                     return new CSSDisplayProperty { Rule = style };
                //case PropertyNames.ELEVATION:
                case PropertyNames.EmptyCells:                  return new CSSEmptyCellsProperty { Rule = style };
                case PropertyNames.Float:                       return new CSSFloatProperty { Rule = style };
                case PropertyNames.FontFamily:                  return new CSSFontFamilyProperty { Rule = style };
                case PropertyNames.FontSize:                    return new CSSFontSizeProperty { Rule = style };
                case PropertyNames.FontStyle:                   return new CSSFontStyleProperty { Rule = style };
                case PropertyNames.FontVariant:                 return new CSSFontVariantProperty { Rule = style };
                case PropertyNames.FontWeight:                  return new CSSFontWeightProperty { Rule = style };
                case PropertyNames.FontStretch:                 return new CSSFontStretchProperty { Rule = style };
                case PropertyNames.Font:                        return new CSSFontProperty { Rule = style };
                case PropertyNames.Height:                      return new CSSHeightProperty { Rule = style };
                case PropertyNames.Left:                        return new CSSLeftProperty { Rule = style };
                case PropertyNames.LetterSpacing:               return new CSSLetterSpacingProperty { Rule = style };
                case PropertyNames.LineHeight:                  return new CSSLineHeightProperty { Rule = style };
                //case PropertyNames.LIST_STYLE_IMAGE:
                //case PropertyNames.LIST_STYLE_POSITION:
                //case PropertyNames.LIST_STYLE_TYPE:
                //case PropertyNames.LIST_STYLE:
                //case PropertyNames.MARQUEE_DIRECTION:
                //case PropertyNames.MARQUEE_PLAY_COUNT:
                //case PropertyNames.MARQUEE_SPEED:
                //case PropertyNames.MARQUEE_STYLE:
                case PropertyNames.MarginRight:                 return new CSSMarginRightProperty { Rule = style };
                case PropertyNames.MarginLeft:                  return new CSSMarginLeftProperty { Rule = style };
                case PropertyNames.MarginTop:                   return new CSSMarginTopProperty { Rule = style };
                case PropertyNames.MarginBottom:                return new CSSMarginBottomProperty { Rule = style };
                case PropertyNames.Margin:                      return new CSSMarginProperty { Rule = style };
                //case PropertyNames.MAX_HEIGHT:
                //case PropertyNames.MAX_WIDTH:
                //case PropertyNames.MIN_HEIGHT:
                //case PropertyNames.MIN_WIDTH:
                case PropertyNames.Opacity:                     return new CSSOpacityProperty { Rule = style };
                case PropertyNames.Orphans:                     return new CSSOrphansProperty { Rule = style };
                case PropertyNames.OutlineColor:                return new CSSOutlineColorProperty { Rule = style };
                case PropertyNames.OutlineStyle:                return new CSSOutlineStyleProperty { Rule = style };
                case PropertyNames.OutlineWidth:                return new CSSOutlineWidthProperty { Rule = style };
                case PropertyNames.Outline:                     return new CSSOutlineProperty { Rule = style };
                case PropertyNames.Overflow:                    return new CSSOverflowProperty { Rule = style };
                case PropertyNames.PaddingTop:                  return new CSSPaddingTopProperty { Rule = style };
                case PropertyNames.PaddingRight:                return new CSSPaddingRightProperty { Rule = style };
                case PropertyNames.PaddingLeft:                 return new CSSPaddingLeftProperty { Rule = style };
                case PropertyNames.PaddingBottom:               return new CSSPaddingBottomProperty { Rule = style };
                case PropertyNames.Padding:                     return new CSSPaddingProperty { Rule = style };
                //case PropertyNames.PAGE_BREAK_AFTER:
                //case PropertyNames.PAGE_BREAK_BEFORE:
                //case PropertyNames.PAGE_BREAK_INSIDE:
                //case PropertyNames.PERSPECTIVE:
                //case PropertyNames.PERSPECTIVE_ORIGIN:
                case PropertyNames.Position:                    return new CSSPositionProperty { Rule = style };
                case PropertyNames.Quotes:                      return new CSSQuotesProperty { Rule = style };
                case PropertyNames.Right:                       return new CSSRightProperty { Rule = style };
                case PropertyNames.TableLayout:                 return new CSSTableLayoutProperty { Rule = style };
                case PropertyNames.TextAlign:                   return new CSSTextAlignProperty { Rule = style };
                case PropertyNames.TextDecoration:              return new CSSTextDecorationProperty { Rule = style };
                case PropertyNames.TextDecorationStyle:         return new CSSTextDecorationStyleProperty { Rule = style };
                case PropertyNames.TextDecorationLine:          return new CSSTextDecorationLineProperty { Rule = style };
                case PropertyNames.TextDecorationColor:         return new CSSTextDecorationColorProperty { Rule = style };
                //case PropertyNames.TEXT_INDENT:
                case PropertyNames.TextTransform:               return new CSSTextTransformProperty { Rule = style };
                //case PropertyNames.TRANSFORM:
                //case PropertyNames.TRANSFORM_ORIGIN:
                //case PropertyNames.TRANSFORM_STYLE:
                //case PropertyNames.TRANSITION:
                //case PropertyNames.TRANSITION_DELAY:
                //case PropertyNames.TRANSITION_DURATION:
                //case PropertyNames.TRANSITION_TIMING_FUNCTION:
                //case PropertyNames.TRANSITION_PROPERTY:
                case PropertyNames.Top:                         return new CSSTopProperty { Rule = style };
                case PropertyNames.UnicodeBidi:                 return new CSSUnicodeBidiProperty { Rule = style };
                case PropertyNames.VerticalAlign:               return new CSSVerticalAlignProperty { Rule = style };
                case PropertyNames.Visibility:                  return new CSSVisibilityProperty { Rule = style };
                case PropertyNames.WhiteSpace:                  return new CSSWhiteSpaceProperty { Rule = style };
                case PropertyNames.Widows:                      return new CSSWidowsProperty { Rule = style };
                case PropertyNames.Width:                       return new CSSWidthProperty { Rule = style };
                case PropertyNames.WordSpacing:                 return new CSSWordSpacingProperty { Rule = style };
                case PropertyNames.ZIndex:                      return new CSSZIndexProperty { Rule = style };
                default:                                        return new CSSProperty(name) { Rule = style };
            }
        }
    }
}
