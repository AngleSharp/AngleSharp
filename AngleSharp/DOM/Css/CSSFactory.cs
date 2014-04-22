namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css.Properties;
    using System;
    using System.Collections.Generic;

    internal class CSSFactory
    {
        static readonly Dictionary<String, Func<CSSProperty>> properties = new Dictionary<String, Func<CSSProperty>>(StringComparer.OrdinalIgnoreCase);

        static CSSFactory()
        {
            //properties.Add(PropertyNames.ANIMATION, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_DELAY, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_DIRECTION, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_DURATION, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_FILL_MODE, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_ITERATION_COUNT, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_NAME, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_PLAY_STATE, () => new CSSProperty());
            //properties.Add(PropertyNames.ANIMATION_TIMING_FUNCTION, () => new CSSProperty());
            properties.Add(PropertyNames.BackgroundAttachment, () => new CSSBackgroundAttachmentProperty());
            properties.Add(PropertyNames.BackgroundColor, () => new CSSBackgroundColorProperty());
            properties.Add(PropertyNames.BackgroundClip, () => new CSSBackgroundClipProperty());
            properties.Add(PropertyNames.BackgroundOrigin, () => new CSSBackgroundOriginProperty());
            properties.Add(PropertyNames.BackgroundSize, () => new CSSBackgroundSizeProperty());
            properties.Add(PropertyNames.BackgroundImage, () => new CSSBackgroundImageProperty());
            properties.Add(PropertyNames.BackgroundPosition, () => new CSSBackgroundPositionProperty());
            properties.Add(PropertyNames.BackgroundRepeat, () => new CSSBackgroundRepeatProperty());
            properties.Add(PropertyNames.Background, () => new CSSBackgroundProperty());
            properties.Add(PropertyNames.BorderColor, () => new CSSBorderColorProperty());
            properties.Add(PropertyNames.BorderSpacing, () => new CSSBorderSpacingProperty());
            properties.Add(PropertyNames.BorderCollapse, () => new CSSBorderCollapseProperty());
            properties.Add(PropertyNames.BorderColor, () => new CSSBorderColorProperty());
            properties.Add(PropertyNames.BoxShadow, () => new CSSBoxShadowProperty());
            properties.Add(PropertyNames.BoxDecorationBreak, () => new CSSBoxDecorationBreak());
            properties.Add(PropertyNames.BreakAfter, () => new CSSBreakAfterProperty());
            properties.Add(PropertyNames.BreakBefore, () => new CSSBreakBeforeProperty());
            properties.Add(PropertyNames.BreakInside, () => new CSSBreakInsideProperty());
            properties.Add(PropertyNames.BackfaceVisibility, () => new CSSBackfaceVisibility());
            properties.Add(PropertyNames.BorderTopLeftRadius, () => new CSSBorderTopLeftRadiusProperty());
            properties.Add(PropertyNames.BorderTopRightRadius, () => new CSSBorderTopRightRadiusProperty());
            properties.Add(PropertyNames.BorderBottomLeftRadius, () => new CSSBorderBottomLeftRadiusProperty());
            properties.Add(PropertyNames.BorderBottomRightRadius, () => new CSSBorderBottomRightRadiusProperty());
            properties.Add(PropertyNames.BorderRadius, () => new CSSBorderRadiusProperty());
            //properties.Add(PropertyNames.BORDER_IMAGE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_IMAGE_OUTSET, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_IMAGE_REPEAT, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_IMAGE_SOURCE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_IMAGE_SLICE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_IMAGE_WIDTH, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_TOP, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_RIGHT, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_BOTTOM, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_LEFT, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_TOP_COLOR, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_LEFT_COLOR, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_RIGHT_COLOR, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_BOTTOM_COLOR, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_TOP_STYLE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_LEFT_STYLE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_RIGHT_STYLE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_BOTTOM_STYLE, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_TOP_WIDTH, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_LEFT_WIDTH, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_RIGHT_WIDTH, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_BOTTOM_WIDTH, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER_WIDTH, () => new CSSProperty());
            //properties.Add(PropertyNames.BORDER, () => new CSSProperty());
            properties.Add(PropertyNames.Bottom, () => new CSSBottomProperty());
            properties.Add(PropertyNames.Columns, () => new CSSColumnsProperty());
            properties.Add(PropertyNames.ColumnCount, () => new CSSColumnCountProperty());
            properties.Add(PropertyNames.ColumnWidth, () => new CSSColumnWidthProperty());
            properties.Add(PropertyNames.ColumnFill, () => new CSSColumnFillProperty());
            properties.Add(PropertyNames.ColumnGap, () => new CSSColumnGapProperty());
            properties.Add(PropertyNames.ColumnSpan, () => new CSSColumnSpanProperty());
            properties.Add(PropertyNames.ColumnRule, () => new CSSColumnRuleProperty());
            properties.Add(PropertyNames.ColumnRuleColor, () => new CSSColumnRuleColorProperty());
            properties.Add(PropertyNames.ColumnRuleStyle, () => new CSSColumnRuleStyleProperty());
            properties.Add(PropertyNames.ColumnRuleWidth, () => new CSSColumnRuleWidthProperty());
            properties.Add(PropertyNames.CaptionSide, () => new CSSCaptionSideProperty());
            properties.Add(PropertyNames.Clear, () => new CSSClearProperty());
            properties.Add(PropertyNames.Clip, () => new CSSClipProperty());
            properties.Add(PropertyNames.Color, () => new CSSColorProperty());
            properties.Add(PropertyNames.Content, () => new CSSContentProperty());
            properties.Add(PropertyNames.CounterIncrement, () => new CSSCounterIncrementProperty());
            properties.Add(PropertyNames.CounterReset, () => new CSSCounterResetProperty());
            properties.Add(PropertyNames.Cursor, () => new CSSCursorProperty());
            properties.Add(PropertyNames.Direction, () => new CSSDirectionProperty());
            properties.Add(PropertyNames.Display, () => new CSSDisplayProperty());
            properties.Add(PropertyNames.EmptyCells, () => new CSSEmptyCellsProperty());
            properties.Add(PropertyNames.Float, () => new CSSFloatProperty());
            properties.Add(PropertyNames.FontFamily, () => new CSSFontFamilyProperty());
            properties.Add(PropertyNames.FontSize, () => new CSSFontSizeProperty());
            properties.Add(PropertyNames.FontStyle, () => new CSSFontStyleProperty());
            properties.Add(PropertyNames.FontVariant, () => new CSSFontVariantProperty());
            properties.Add(PropertyNames.FontWeight, () => new CSSFontWeightProperty());
            properties.Add(PropertyNames.FontStretch, () => new CSSFontStretchProperty());
            properties.Add(PropertyNames.Font, () => new CSSFontProperty());
            properties.Add(PropertyNames.Height, () => new CSSHeightProperty());
            properties.Add(PropertyNames.Left, () => new CSSLeftProperty());
            properties.Add(PropertyNames.LetterSpacing, () => new CSSLetterSpacingProperty());
            properties.Add(PropertyNames.LineHeight, () => new CSSLineHeightProperty());
            properties.Add(PropertyNames.ListStyleImage, () => new CSSListStyleImageProperty());
            properties.Add(PropertyNames.ListStylePosition, () => new CSSListStylePositionProperty());
            properties.Add(PropertyNames.ListStyleType, () => new CSSListStyleTypeProperty());
            properties.Add(PropertyNames.ListStyle, () => new CSSListStyleProperty());
            //properties.Add(PropertyNames.MARQUEE_DIRECTION, () => new CSSProperty());
            //properties.Add(PropertyNames.MARQUEE_PLAY_COUNT, () => new CSSProperty());
            //properties.Add(PropertyNames.MARQUEE_SPEED, () => new CSSProperty());
            //properties.Add(PropertyNames.MARQUEE_STYLE, () => new CSSProperty());
            properties.Add(PropertyNames.MarginRight, () => new CSSMarginRightProperty());
            properties.Add(PropertyNames.MarginLeft, () => new CSSMarginLeftProperty());
            properties.Add(PropertyNames.MarginTop, () => new CSSMarginTopProperty());
            properties.Add(PropertyNames.MarginBottom, () => new CSSMarginBottomProperty());
            properties.Add(PropertyNames.Margin, () => new CSSMarginProperty());
            properties.Add(PropertyNames.MaxHeight, () => new CSSMaxHeightProperty());
            properties.Add(PropertyNames.MaxWidth, () => new CSSMaxWidthProperty());
            properties.Add(PropertyNames.MinHeight, () => new CSSMinHeightProperty());
            properties.Add(PropertyNames.MinWidth, () => new CSSMinWidthProperty());
            properties.Add(PropertyNames.Opacity, () => new CSSOpacityProperty());
            properties.Add(PropertyNames.Orphans, () => new CSSOrphansProperty());
            properties.Add(PropertyNames.OutlineColor, () => new CSSOutlineColorProperty());
            properties.Add(PropertyNames.OutlineStyle, () => new CSSOutlineStyleProperty());
            properties.Add(PropertyNames.OutlineWidth, () => new CSSOutlineWidthProperty());
            properties.Add(PropertyNames.Outline, () => new CSSOutlineProperty());
            properties.Add(PropertyNames.Overflow, () => new CSSOverflowProperty());
            properties.Add(PropertyNames.PaddingTop, () => new CSSPaddingTopProperty());
            properties.Add(PropertyNames.PaddingRight, () => new CSSPaddingRightProperty());
            properties.Add(PropertyNames.PaddingLeft, () => new CSSPaddingLeftProperty());
            properties.Add(PropertyNames.PaddingBottom, () => new CSSPaddingBottomProperty());
            properties.Add(PropertyNames.Padding, () => new CSSPaddingProperty());
            properties.Add(PropertyNames.PageBreakAfter, () => new CSSPageBreakAfterProperty());
            properties.Add(PropertyNames.PageBreakBefore, () => new CSSPageBreakBeforeProperty());
            properties.Add(PropertyNames.PageBreakInside, () => new CSSPageBreakInsideProperty());
            properties.Add(PropertyNames.Perspective, () => new CSSPerspectiveProperty());
            properties.Add(PropertyNames.PerspectiveOrigin, () => new CSSPerspectiveOriginProperty());
            properties.Add(PropertyNames.Position, () => new CSSPositionProperty());
            properties.Add(PropertyNames.Quotes, () => new CSSQuotesProperty());
            properties.Add(PropertyNames.Right, () => new CSSRightProperty());
            properties.Add(PropertyNames.TableLayout, () => new CSSTableLayoutProperty());
            properties.Add(PropertyNames.TextAlign, () => new CSSTextAlignProperty());
            properties.Add(PropertyNames.TextDecoration, () => new CSSTextDecorationProperty());
            properties.Add(PropertyNames.TextDecorationStyle, () => new CSSTextDecorationStyleProperty());
            properties.Add(PropertyNames.TextDecorationLine, () => new CSSTextDecorationLineProperty());
            properties.Add(PropertyNames.TextDecorationColor, () => new CSSTextDecorationColorProperty());
            properties.Add(PropertyNames.TextIndent, () => new CSSTextIndentProperty());
            properties.Add(PropertyNames.TextTransform, () => new CSSTextTransformProperty());
            properties.Add(PropertyNames.Transform, () => new CSSTransformProperty());
            properties.Add(PropertyNames.TransformOrigin, () => new CSSTransformOriginProperty());
            properties.Add(PropertyNames.TransformStyle, () => new CSSTransformStyleProperty());
            //properties.Add(PropertyNames.TRANSITION, () => new CSSProperty());
            //properties.Add(PropertyNames.TRANSITION_DELAY, () => new CSSProperty());
            //properties.Add(PropertyNames.TRANSITION_DURATION, () => new CSSProperty());
            //properties.Add(PropertyNames.TRANSITION_TIMING_FUNCTION, () => new CSSProperty());
            //properties.Add(PropertyNames.TRANSITION_PROPERTY, () => new CSSProperty());
            properties.Add(PropertyNames.Top, () => new CSSTopProperty());
            properties.Add(PropertyNames.UnicodeBidi, () => new CSSUnicodeBidiProperty());
            properties.Add(PropertyNames.VerticalAlign, () => new CSSVerticalAlignProperty());
            properties.Add(PropertyNames.Visibility, () => new CSSVisibilityProperty());
            properties.Add(PropertyNames.WhiteSpace, () => new CSSWhiteSpaceProperty());
            properties.Add(PropertyNames.Widows, () => new CSSWidowsProperty());
            properties.Add(PropertyNames.Width, () => new CSSWidthProperty());
            properties.Add(PropertyNames.WordSpacing, () => new CSSWordSpacingProperty());
            properties.Add(PropertyNames.ZIndex, () => new CSSZIndexProperty());
        }

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created property</returns>
        public static CSSProperty Create(String name, CSSStyleDeclaration style)
        {
            Func<CSSProperty> propertyCreator;

            if (properties.TryGetValue(name, out propertyCreator))
            {
                var property = propertyCreator();
                property.Rule = style;
                return property;
            }

            return new CSSProperty(name) { Rule = style };
        }
    }
}
