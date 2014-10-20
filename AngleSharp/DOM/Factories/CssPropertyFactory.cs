namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSSProperty instance creation mappings.
    /// </summary>
    static class CssPropertyFactory
    {
        static readonly Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>> properties = new Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>>(StringComparer.OrdinalIgnoreCase);

        static CssPropertyFactory()
        {
            properties.Add(PropertyNames.Animation, style => new CSSAnimationProperty(style));
            properties.Add(PropertyNames.AnimationDelay, style => new CSSAnimationDelayProperty(style));
            properties.Add(PropertyNames.AnimationDirection, style => new CSSAnimationDirectionProperty(style));
            properties.Add(PropertyNames.AnimationDuration, style => new CSSAnimationDurationProperty(style));
            properties.Add(PropertyNames.AnimationFillMode, style => new CSSAnimationFillModeProperty(style));
            properties.Add(PropertyNames.AnimationIterationCount, style => new CSSAnimationIterationCountProperty(style));
            properties.Add(PropertyNames.AnimationName, style => new CSSAnimationNameProperty(style));
            properties.Add(PropertyNames.AnimationPlayState, style => new CSSAnimationPlayStateProperty(style));
            properties.Add(PropertyNames.AnimationTimingFunction, style => new CSSAnimationTimingFunctionProperty(style));
            properties.Add(PropertyNames.BackgroundAttachment, style => new CSSBackgroundAttachmentProperty(style));
            properties.Add(PropertyNames.BackgroundColor, style => new CSSBackgroundColorProperty(style));
            properties.Add(PropertyNames.BackgroundClip, style => new CSSBackgroundClipProperty(style));
            properties.Add(PropertyNames.BackgroundOrigin, style => new CSSBackgroundOriginProperty(style));
            properties.Add(PropertyNames.BackgroundSize, style => new CSSBackgroundSizeProperty(style));
            properties.Add(PropertyNames.BackgroundImage, style => new CSSBackgroundImageProperty(style));
            properties.Add(PropertyNames.BackgroundPosition, style => new CSSBackgroundPositionProperty(style));
            properties.Add(PropertyNames.BackgroundRepeat, style => new CSSBackgroundRepeatProperty(style));
            properties.Add(PropertyNames.Background, style => new CSSBackgroundProperty(style));
            properties.Add(PropertyNames.BorderColor, style => new CSSBorderColorProperty(style));
            properties.Add(PropertyNames.BorderSpacing, style => new CSSBorderSpacingProperty(style));
            properties.Add(PropertyNames.BorderCollapse, style => new CSSBorderCollapseProperty(style));
            properties.Add(PropertyNames.BorderStyle, style => new CSSBorderStyleProperty(style));
            properties.Add(PropertyNames.BoxShadow, style => new CSSBoxShadowProperty(style));
            properties.Add(PropertyNames.BoxDecorationBreak, style => new CSSBoxDecorationBreak(style));
            properties.Add(PropertyNames.BreakAfter, style => new CSSBreakAfterProperty(style));
            properties.Add(PropertyNames.BreakBefore, style => new CSSBreakBeforeProperty(style));
            properties.Add(PropertyNames.BreakInside, style => new CSSBreakInsideProperty(style));
            properties.Add(PropertyNames.BackfaceVisibility, style => new CSSBackfaceVisibilityProperty(style));
            properties.Add(PropertyNames.BorderTopLeftRadius, style => new CSSBorderTopLeftRadiusProperty(style));
            properties.Add(PropertyNames.BorderTopRightRadius, style => new CSSBorderTopRightRadiusProperty(style));
            properties.Add(PropertyNames.BorderBottomLeftRadius, style => new CSSBorderBottomLeftRadiusProperty(style));
            properties.Add(PropertyNames.BorderBottomRightRadius, style => new CSSBorderBottomRightRadiusProperty(style));
            properties.Add(PropertyNames.BorderRadius, style => new CSSBorderRadiusProperty(style));
            properties.Add(PropertyNames.BorderImage, style => new CSSBorderImageProperty(style));
            properties.Add(PropertyNames.BorderImageOutset, style => new CSSBorderImageOutsetProperty(style));
            properties.Add(PropertyNames.BorderImageRepeat, style => new CSSBorderImageRepeatProperty(style));
            properties.Add(PropertyNames.BorderImageSource, style => new CSSBorderImageSourceProperty(style));
            properties.Add(PropertyNames.BorderImageSlice, style => new CSSBorderImageSliceProperty(style));
            properties.Add(PropertyNames.BorderImageWidth, style => new CSSBorderImageWidthProperty(style));
            properties.Add(PropertyNames.BorderTopColor, style => new CSSBorderTopColorProperty(style));
            properties.Add(PropertyNames.BorderLeftColor, style => new CSSBorderLeftColorProperty(style));
            properties.Add(PropertyNames.BorderRightColor, style => new CSSBorderRightColorProperty(style));
            properties.Add(PropertyNames.BorderBottomColor, style => new CSSBorderBottomColorProperty(style));
            properties.Add(PropertyNames.BorderTopStyle, style => new CSSBorderTopStyleProperty(style));
            properties.Add(PropertyNames.BorderLeftStyle, style => new CSSBorderLeftStyleProperty(style));
            properties.Add(PropertyNames.BorderRightStyle, style => new CSSBorderRightStyleProperty(style));
            properties.Add(PropertyNames.BorderBottomStyle, style => new CSSBorderBottomStyleProperty(style));
            properties.Add(PropertyNames.BorderTopWidth, style => new CSSBorderTopWidthProperty(style));
            properties.Add(PropertyNames.BorderLeftWidth, style => new CSSBorderLeftWidthProperty(style));
            properties.Add(PropertyNames.BorderRightWidth, style => new CSSBorderRightWidthProperty(style));
            properties.Add(PropertyNames.BorderBottomWidth, style => new CSSBorderBottomWidthProperty(style));
            properties.Add(PropertyNames.BorderWidth, style => new CSSBorderWidthProperty(style));
            properties.Add(PropertyNames.BorderTop, style => new CSSBorderTopProperty(style));
            properties.Add(PropertyNames.BorderRight, style => new CSSBorderRightProperty(style));
            properties.Add(PropertyNames.BorderBottom, style => new CSSBorderBottomProperty(style));
            properties.Add(PropertyNames.BorderLeft, style => new CSSBorderLeftProperty(style));
            properties.Add(PropertyNames.Border, style => new CSSBorderProperty(style));
            properties.Add(PropertyNames.Bottom, style => new CSSBottomProperty(style));
            properties.Add(PropertyNames.Columns, style => new CSSColumnsProperty(style));
            properties.Add(PropertyNames.ColumnCount, style => new CSSColumnCountProperty(style));
            properties.Add(PropertyNames.ColumnWidth, style => new CSSColumnWidthProperty(style));
            properties.Add(PropertyNames.ColumnFill, style => new CSSColumnFillProperty(style));
            properties.Add(PropertyNames.ColumnGap, style => new CSSColumnGapProperty(style));
            properties.Add(PropertyNames.ColumnSpan, style => new CSSColumnSpanProperty(style));
            properties.Add(PropertyNames.ColumnRule, style => new CSSColumnRuleProperty(style));
            properties.Add(PropertyNames.ColumnRuleColor, style => new CSSColumnRuleColorProperty(style));
            properties.Add(PropertyNames.ColumnRuleStyle, style => new CSSColumnRuleStyleProperty(style));
            properties.Add(PropertyNames.ColumnRuleWidth, style => new CSSColumnRuleWidthProperty(style));
            properties.Add(PropertyNames.CaptionSide, style => new CSSCaptionSideProperty(style));
            properties.Add(PropertyNames.Clear, style => new CSSClearProperty(style));
            properties.Add(PropertyNames.Clip, style => new CSSClipProperty(style));
            properties.Add(PropertyNames.Color, style => new CSSColorProperty(style));
            properties.Add(PropertyNames.Content, style => new CSSContentProperty(style));
            properties.Add(PropertyNames.CounterIncrement, style => new CSSCounterIncrementProperty(style));
            properties.Add(PropertyNames.CounterReset, style => new CSSCounterResetProperty(style));
            properties.Add(PropertyNames.Cursor, style => new CSSCursorProperty(style));
            properties.Add(PropertyNames.Direction, style => new CSSDirectionProperty(style));
            properties.Add(PropertyNames.Display, style => new CSSDisplayProperty(style));
            properties.Add(PropertyNames.EmptyCells, style => new CSSEmptyCellsProperty(style));
            properties.Add(PropertyNames.Float, style => new CSSFloatProperty(style));
            properties.Add(PropertyNames.FontFamily, style => new CSSFontFamilyProperty(style));
            properties.Add(PropertyNames.FontSize, style => new CSSFontSizeProperty(style));
            properties.Add(PropertyNames.FontSizeAdjust, style => new CSSFontSizeAdjustProperty(style));
            properties.Add(PropertyNames.FontStyle, style => new CSSFontStyleProperty(style));
            properties.Add(PropertyNames.FontVariant, style => new CSSFontVariantProperty(style));
            properties.Add(PropertyNames.FontWeight, style => new CSSFontWeightProperty(style));
            properties.Add(PropertyNames.FontStretch, style => new CSSFontStretchProperty(style));
            properties.Add(PropertyNames.Font, style => new CSSFontProperty(style));
            properties.Add(PropertyNames.Height, style => new CSSHeightProperty(style));
            properties.Add(PropertyNames.Left, style => new CSSLeftProperty(style));
            properties.Add(PropertyNames.LetterSpacing, style => new CSSLetterSpacingProperty(style));
            properties.Add(PropertyNames.LineHeight, style => new CSSLineHeightProperty(style));
            properties.Add(PropertyNames.ListStyleImage, style => new CSSListStyleImageProperty(style));
            properties.Add(PropertyNames.ListStylePosition, style => new CSSListStylePositionProperty(style));
            properties.Add(PropertyNames.ListStyleType, style => new CSSListStyleTypeProperty(style));
            properties.Add(PropertyNames.ListStyle, style => new CSSListStyleProperty(style));
            properties.Add(PropertyNames.MarginRight, style => new CSSMarginRightProperty(style));
            properties.Add(PropertyNames.MarginLeft, style => new CSSMarginLeftProperty(style));
            properties.Add(PropertyNames.MarginTop, style => new CSSMarginTopProperty(style));
            properties.Add(PropertyNames.MarginBottom, style => new CSSMarginBottomProperty(style));
            properties.Add(PropertyNames.Margin, style => new CSSMarginProperty(style));
            properties.Add(PropertyNames.MaxHeight, style => new CSSMaxHeightProperty(style));
            properties.Add(PropertyNames.MaxWidth, style => new CSSMaxWidthProperty(style));
            properties.Add(PropertyNames.MinHeight, style => new CSSMinHeightProperty(style));
            properties.Add(PropertyNames.MinWidth, style => new CSSMinWidthProperty(style));
            properties.Add(PropertyNames.Opacity, style => new CSSOpacityProperty(style));
            properties.Add(PropertyNames.Orphans, style => new CSSOrphansProperty(style));
            properties.Add(PropertyNames.OutlineColor, style => new CSSOutlineColorProperty(style));
            properties.Add(PropertyNames.OutlineStyle, style => new CSSOutlineStyleProperty(style));
            properties.Add(PropertyNames.OutlineWidth, style => new CSSOutlineWidthProperty(style));
            properties.Add(PropertyNames.Outline, style => new CSSOutlineProperty(style));
            properties.Add(PropertyNames.Overflow, style => new CSSOverflowProperty(style));
            properties.Add(PropertyNames.PaddingTop, style => new CSSPaddingTopProperty(style));
            properties.Add(PropertyNames.PaddingRight, style => new CSSPaddingRightProperty(style));
            properties.Add(PropertyNames.PaddingLeft, style => new CSSPaddingLeftProperty(style));
            properties.Add(PropertyNames.PaddingBottom, style => new CSSPaddingBottomProperty(style));
            properties.Add(PropertyNames.Padding, style => new CSSPaddingProperty(style));
            properties.Add(PropertyNames.PageBreakAfter, style => new CSSPageBreakAfterProperty(style));
            properties.Add(PropertyNames.PageBreakBefore, style => new CSSPageBreakBeforeProperty(style));
            properties.Add(PropertyNames.PageBreakInside, style => new CSSPageBreakInsideProperty(style));
            properties.Add(PropertyNames.Perspective, style => new CSSPerspectiveProperty(style));
            properties.Add(PropertyNames.PerspectiveOrigin, style => new CSSPerspectiveOriginProperty(style));
            properties.Add(PropertyNames.Position, style => new CSSPositionProperty(style));
            properties.Add(PropertyNames.Quotes, style => new CSSQuotesProperty(style));
            properties.Add(PropertyNames.Right, style => new CSSRightProperty(style));
            properties.Add(PropertyNames.TableLayout, style => new CSSTableLayoutProperty(style));
            properties.Add(PropertyNames.TextAlign, style => new CSSTextAlignProperty(style));
            properties.Add(PropertyNames.TextDecoration, style => new CSSTextDecorationProperty(style));
            properties.Add(PropertyNames.TextDecorationStyle, style => new CSSTextDecorationStyleProperty(style));
            properties.Add(PropertyNames.TextDecorationLine, style => new CSSTextDecorationLineProperty(style));
            properties.Add(PropertyNames.TextDecorationColor, style => new CSSTextDecorationColorProperty(style));
            properties.Add(PropertyNames.TextIndent, style => new CSSTextIndentProperty(style));
            properties.Add(PropertyNames.TextTransform, style => new CSSTextTransformProperty(style));
            properties.Add(PropertyNames.TextShadow, style => new CSSTextShadowProperty(style));
            properties.Add(PropertyNames.Transform, style => new CSSTransformProperty(style));
            properties.Add(PropertyNames.TransformOrigin, style => new CSSTransformOriginProperty(style));
            properties.Add(PropertyNames.TransformStyle, style => new CSSTransformStyleProperty(style));
            properties.Add(PropertyNames.Transition, style => new CSSTransitionProperty(style));
            properties.Add(PropertyNames.TransitionDelay, style => new CSSTransitionDelayProperty(style));
            properties.Add(PropertyNames.TransitionDuration, style => new CSSTransitionDurationProperty(style));
            properties.Add(PropertyNames.TransitionTimingFunction, style => new CSSTransitionTimingFunctionProperty(style));
            properties.Add(PropertyNames.TransitionProperty, style => new CSSTransitionPropertyProperty(style));
            properties.Add(PropertyNames.Top, style => new CSSTopProperty(style));
            properties.Add(PropertyNames.UnicodeBidi, style => new CSSUnicodeBidiProperty(style));
            properties.Add(PropertyNames.VerticalAlign, style => new CSSVerticalAlignProperty(style));
            properties.Add(PropertyNames.Visibility, style => new CSSVisibilityProperty(style));
            properties.Add(PropertyNames.WhiteSpace, style => new CSSWhiteSpaceProperty(style));
            properties.Add(PropertyNames.Widows, style => new CSSWidowsProperty(style));
            properties.Add(PropertyNames.Width, style => new CSSWidthProperty(style));
            properties.Add(PropertyNames.WordSpacing, style => new CSSWordSpacingProperty(style));
            properties.Add(PropertyNames.ZIndex, style => new CSSZIndexProperty(style));
        }

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created property</returns>
        public static CSSProperty Create(String name, CSSStyleDeclaration style)
        {
            Func<CSSStyleDeclaration, CSSProperty> propertyCreator;
            var property = style.GetProperty(name);

            if (property == null && properties.TryGetValue(name, out propertyCreator))
                property = propertyCreator(style);

            return property;
        }
    }
}
