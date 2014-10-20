namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSSProperty instance creation mappings.
    /// </summary>
    static class CssPropertyFactory
    {
        static readonly Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>> longhands = new Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>> shorthands = new Dictionary<String, Func<CSSStyleDeclaration, CSSProperty>>(StringComparer.OrdinalIgnoreCase);

        static CssPropertyFactory()
        {
            shorthands.Add(PropertyNames.Animation, style => new CSSAnimationProperty(style));
            longhands.Add(PropertyNames.AnimationDelay, style => new CSSAnimationDelayProperty(style));
            longhands.Add(PropertyNames.AnimationDirection, style => new CSSAnimationDirectionProperty(style));
            longhands.Add(PropertyNames.AnimationDuration, style => new CSSAnimationDurationProperty(style));
            longhands.Add(PropertyNames.AnimationFillMode, style => new CSSAnimationFillModeProperty(style));
            longhands.Add(PropertyNames.AnimationIterationCount, style => new CSSAnimationIterationCountProperty(style));
            longhands.Add(PropertyNames.AnimationName, style => new CSSAnimationNameProperty(style));
            longhands.Add(PropertyNames.AnimationPlayState, style => new CSSAnimationPlayStateProperty(style));
            longhands.Add(PropertyNames.AnimationTimingFunction, style => new CSSAnimationTimingFunctionProperty(style));
            longhands.Add(PropertyNames.BackgroundAttachment, style => new CSSBackgroundAttachmentProperty(style));
            longhands.Add(PropertyNames.BackgroundColor, style => new CSSBackgroundColorProperty(style));
            longhands.Add(PropertyNames.BackgroundClip, style => new CSSBackgroundClipProperty(style));
            longhands.Add(PropertyNames.BackgroundOrigin, style => new CSSBackgroundOriginProperty(style));
            longhands.Add(PropertyNames.BackgroundSize, style => new CSSBackgroundSizeProperty(style));
            longhands.Add(PropertyNames.BackgroundImage, style => new CSSBackgroundImageProperty(style));
            longhands.Add(PropertyNames.BackgroundPosition, style => new CSSBackgroundPositionProperty(style));
            longhands.Add(PropertyNames.BackgroundRepeat, style => new CSSBackgroundRepeatProperty(style));
            shorthands.Add(PropertyNames.Background, style => new CSSBackgroundProperty(style));
            shorthands.Add(PropertyNames.BorderColor, style => new CSSBorderColorProperty(style));
            longhands.Add(PropertyNames.BorderSpacing, style => new CSSBorderSpacingProperty(style));
            longhands.Add(PropertyNames.BorderCollapse, style => new CSSBorderCollapseProperty(style));
            shorthands.Add(PropertyNames.BorderStyle, style => new CSSBorderStyleProperty(style));
            longhands.Add(PropertyNames.BoxShadow, style => new CSSBoxShadowProperty(style));
            longhands.Add(PropertyNames.BoxDecorationBreak, style => new CSSBoxDecorationBreak(style));
            longhands.Add(PropertyNames.BreakAfter, style => new CSSBreakAfterProperty(style));
            longhands.Add(PropertyNames.BreakBefore, style => new CSSBreakBeforeProperty(style));
            longhands.Add(PropertyNames.BreakInside, style => new CSSBreakInsideProperty(style));
            longhands.Add(PropertyNames.BackfaceVisibility, style => new CSSBackfaceVisibilityProperty(style));
            longhands.Add(PropertyNames.BorderTopLeftRadius, style => new CSSBorderTopLeftRadiusProperty(style));
            longhands.Add(PropertyNames.BorderTopRightRadius, style => new CSSBorderTopRightRadiusProperty(style));
            longhands.Add(PropertyNames.BorderBottomLeftRadius, style => new CSSBorderBottomLeftRadiusProperty(style));
            longhands.Add(PropertyNames.BorderBottomRightRadius, style => new CSSBorderBottomRightRadiusProperty(style));
            longhands.Add(PropertyNames.BorderRadius, style => new CSSBorderRadiusProperty(style));
            longhands.Add(PropertyNames.BorderImage, style => new CSSBorderImageProperty(style));
            longhands.Add(PropertyNames.BorderImageOutset, style => new CSSBorderImageOutsetProperty(style));
            longhands.Add(PropertyNames.BorderImageRepeat, style => new CSSBorderImageRepeatProperty(style));
            longhands.Add(PropertyNames.BorderImageSource, style => new CSSBorderImageSourceProperty(style));
            longhands.Add(PropertyNames.BorderImageSlice, style => new CSSBorderImageSliceProperty(style));
            longhands.Add(PropertyNames.BorderImageWidth, style => new CSSBorderImageWidthProperty(style));
            longhands.Add(PropertyNames.BorderTopColor, style => new CSSBorderTopColorProperty(style));
            longhands.Add(PropertyNames.BorderLeftColor, style => new CSSBorderLeftColorProperty(style));
            longhands.Add(PropertyNames.BorderRightColor, style => new CSSBorderRightColorProperty(style));
            longhands.Add(PropertyNames.BorderBottomColor, style => new CSSBorderBottomColorProperty(style));
            longhands.Add(PropertyNames.BorderTopStyle, style => new CSSBorderTopStyleProperty(style));
            longhands.Add(PropertyNames.BorderLeftStyle, style => new CSSBorderLeftStyleProperty(style));
            longhands.Add(PropertyNames.BorderRightStyle, style => new CSSBorderRightStyleProperty(style));
            longhands.Add(PropertyNames.BorderBottomStyle, style => new CSSBorderBottomStyleProperty(style));
            longhands.Add(PropertyNames.BorderTopWidth, style => new CSSBorderTopWidthProperty(style));
            longhands.Add(PropertyNames.BorderLeftWidth, style => new CSSBorderLeftWidthProperty(style));
            longhands.Add(PropertyNames.BorderRightWidth, style => new CSSBorderRightWidthProperty(style));
            longhands.Add(PropertyNames.BorderBottomWidth, style => new CSSBorderBottomWidthProperty(style));
            shorthands.Add(PropertyNames.BorderWidth, style => new CSSBorderWidthProperty(style));
            shorthands.Add(PropertyNames.BorderTop, style => new CSSBorderTopProperty(style));
            shorthands.Add(PropertyNames.BorderRight, style => new CSSBorderRightProperty(style));
            shorthands.Add(PropertyNames.BorderBottom, style => new CSSBorderBottomProperty(style));
            shorthands.Add(PropertyNames.BorderLeft, style => new CSSBorderLeftProperty(style));
            shorthands.Add(PropertyNames.Border, style => new CSSBorderProperty(style));
            longhands.Add(PropertyNames.Bottom, style => new CSSBottomProperty(style));
            shorthands.Add(PropertyNames.Columns, style => new CSSColumnsProperty(style));
            longhands.Add(PropertyNames.ColumnCount, style => new CSSColumnCountProperty(style));
            longhands.Add(PropertyNames.ColumnWidth, style => new CSSColumnWidthProperty(style));
            longhands.Add(PropertyNames.ColumnFill, style => new CSSColumnFillProperty(style));
            longhands.Add(PropertyNames.ColumnGap, style => new CSSColumnGapProperty(style));
            longhands.Add(PropertyNames.ColumnSpan, style => new CSSColumnSpanProperty(style));
            shorthands.Add(PropertyNames.ColumnRule, style => new CSSColumnRuleProperty(style));
            longhands.Add(PropertyNames.ColumnRuleColor, style => new CSSColumnRuleColorProperty(style));
            longhands.Add(PropertyNames.ColumnRuleStyle, style => new CSSColumnRuleStyleProperty(style));
            longhands.Add(PropertyNames.ColumnRuleWidth, style => new CSSColumnRuleWidthProperty(style));
            longhands.Add(PropertyNames.CaptionSide, style => new CSSCaptionSideProperty(style));
            longhands.Add(PropertyNames.Clear, style => new CSSClearProperty(style));
            longhands.Add(PropertyNames.Clip, style => new CSSClipProperty(style));
            longhands.Add(PropertyNames.Color, style => new CSSColorProperty(style));
            longhands.Add(PropertyNames.Content, style => new CSSContentProperty(style));
            longhands.Add(PropertyNames.CounterIncrement, style => new CSSCounterIncrementProperty(style));
            longhands.Add(PropertyNames.CounterReset, style => new CSSCounterResetProperty(style));
            longhands.Add(PropertyNames.Cursor, style => new CSSCursorProperty(style));
            longhands.Add(PropertyNames.Direction, style => new CSSDirectionProperty(style));
            longhands.Add(PropertyNames.Display, style => new CSSDisplayProperty(style));
            longhands.Add(PropertyNames.EmptyCells, style => new CSSEmptyCellsProperty(style));
            longhands.Add(PropertyNames.Float, style => new CSSFloatProperty(style));
            longhands.Add(PropertyNames.FontFamily, style => new CSSFontFamilyProperty(style));
            longhands.Add(PropertyNames.FontSize, style => new CSSFontSizeProperty(style));
            longhands.Add(PropertyNames.FontSizeAdjust, style => new CSSFontSizeAdjustProperty(style));
            longhands.Add(PropertyNames.FontStyle, style => new CSSFontStyleProperty(style));
            longhands.Add(PropertyNames.FontVariant, style => new CSSFontVariantProperty(style));
            longhands.Add(PropertyNames.FontWeight, style => new CSSFontWeightProperty(style));
            longhands.Add(PropertyNames.FontStretch, style => new CSSFontStretchProperty(style));
            shorthands.Add(PropertyNames.Font, style => new CSSFontProperty(style));
            longhands.Add(PropertyNames.Height, style => new CSSHeightProperty(style));
            longhands.Add(PropertyNames.Left, style => new CSSLeftProperty(style));
            longhands.Add(PropertyNames.LetterSpacing, style => new CSSLetterSpacingProperty(style));
            longhands.Add(PropertyNames.LineHeight, style => new CSSLineHeightProperty(style));
            longhands.Add(PropertyNames.ListStyleImage, style => new CSSListStyleImageProperty(style));
            longhands.Add(PropertyNames.ListStylePosition, style => new CSSListStylePositionProperty(style));
            longhands.Add(PropertyNames.ListStyleType, style => new CSSListStyleTypeProperty(style));
            shorthands.Add(PropertyNames.ListStyle, style => new CSSListStyleProperty(style));
            longhands.Add(PropertyNames.MarginRight, style => new CSSMarginRightProperty(style));
            longhands.Add(PropertyNames.MarginLeft, style => new CSSMarginLeftProperty(style));
            longhands.Add(PropertyNames.MarginTop, style => new CSSMarginTopProperty(style));
            longhands.Add(PropertyNames.MarginBottom, style => new CSSMarginBottomProperty(style));
            shorthands.Add(PropertyNames.Margin, style => new CSSMarginProperty(style));
            longhands.Add(PropertyNames.MaxHeight, style => new CSSMaxHeightProperty(style));
            longhands.Add(PropertyNames.MaxWidth, style => new CSSMaxWidthProperty(style));
            longhands.Add(PropertyNames.MinHeight, style => new CSSMinHeightProperty(style));
            longhands.Add(PropertyNames.MinWidth, style => new CSSMinWidthProperty(style));
            longhands.Add(PropertyNames.Opacity, style => new CSSOpacityProperty(style));
            longhands.Add(PropertyNames.Orphans, style => new CSSOrphansProperty(style));
            longhands.Add(PropertyNames.OutlineColor, style => new CSSOutlineColorProperty(style));
            longhands.Add(PropertyNames.OutlineStyle, style => new CSSOutlineStyleProperty(style));
            longhands.Add(PropertyNames.OutlineWidth, style => new CSSOutlineWidthProperty(style));
            shorthands.Add(PropertyNames.Outline, style => new CSSOutlineProperty(style));
            longhands.Add(PropertyNames.Overflow, style => new CSSOverflowProperty(style));
            longhands.Add(PropertyNames.PaddingTop, style => new CSSPaddingTopProperty(style));
            longhands.Add(PropertyNames.PaddingRight, style => new CSSPaddingRightProperty(style));
            longhands.Add(PropertyNames.PaddingLeft, style => new CSSPaddingLeftProperty(style));
            longhands.Add(PropertyNames.PaddingBottom, style => new CSSPaddingBottomProperty(style));
            shorthands.Add(PropertyNames.Padding, style => new CSSPaddingProperty(style));
            longhands.Add(PropertyNames.PageBreakAfter, style => new CSSPageBreakAfterProperty(style));
            longhands.Add(PropertyNames.PageBreakBefore, style => new CSSPageBreakBeforeProperty(style));
            longhands.Add(PropertyNames.PageBreakInside, style => new CSSPageBreakInsideProperty(style));
            longhands.Add(PropertyNames.Perspective, style => new CSSPerspectiveProperty(style));
            longhands.Add(PropertyNames.PerspectiveOrigin, style => new CSSPerspectiveOriginProperty(style));
            longhands.Add(PropertyNames.Position, style => new CSSPositionProperty(style));
            longhands.Add(PropertyNames.Quotes, style => new CSSQuotesProperty(style));
            longhands.Add(PropertyNames.Right, style => new CSSRightProperty(style));
            longhands.Add(PropertyNames.TableLayout, style => new CSSTableLayoutProperty(style));
            longhands.Add(PropertyNames.TextAlign, style => new CSSTextAlignProperty(style));
            shorthands.Add(PropertyNames.TextDecoration, style => new CSSTextDecorationProperty(style));
            longhands.Add(PropertyNames.TextDecorationStyle, style => new CSSTextDecorationStyleProperty(style));
            longhands.Add(PropertyNames.TextDecorationLine, style => new CSSTextDecorationLineProperty(style));
            longhands.Add(PropertyNames.TextDecorationColor, style => new CSSTextDecorationColorProperty(style));
            longhands.Add(PropertyNames.TextIndent, style => new CSSTextIndentProperty(style));
            longhands.Add(PropertyNames.TextTransform, style => new CSSTextTransformProperty(style));
            longhands.Add(PropertyNames.TextShadow, style => new CSSTextShadowProperty(style));
            longhands.Add(PropertyNames.Transform, style => new CSSTransformProperty(style));
            longhands.Add(PropertyNames.TransformOrigin, style => new CSSTransformOriginProperty(style));
            longhands.Add(PropertyNames.TransformStyle, style => new CSSTransformStyleProperty(style));
            shorthands.Add(PropertyNames.Transition, style => new CSSTransitionProperty(style));
            longhands.Add(PropertyNames.TransitionDelay, style => new CSSTransitionDelayProperty(style));
            longhands.Add(PropertyNames.TransitionDuration, style => new CSSTransitionDurationProperty(style));
            longhands.Add(PropertyNames.TransitionTimingFunction, style => new CSSTransitionTimingFunctionProperty(style));
            longhands.Add(PropertyNames.TransitionProperty, style => new CSSTransitionPropertyProperty(style));
            longhands.Add(PropertyNames.Top, style => new CSSTopProperty(style));
            longhands.Add(PropertyNames.UnicodeBidi, style => new CSSUnicodeBidiProperty(style));
            longhands.Add(PropertyNames.VerticalAlign, style => new CSSVerticalAlignProperty(style));
            longhands.Add(PropertyNames.Visibility, style => new CSSVisibilityProperty(style));
            longhands.Add(PropertyNames.WhiteSpace, style => new CSSWhiteSpaceProperty(style));
            longhands.Add(PropertyNames.Widows, style => new CSSWidowsProperty(style));
            longhands.Add(PropertyNames.Width, style => new CSSWidthProperty(style));
            longhands.Add(PropertyNames.WordSpacing, style => new CSSWordSpacingProperty(style));
            longhands.Add(PropertyNames.ZIndex, style => new CSSZIndexProperty(style));
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

            if (property != null)
                return property;

            if (longhands.TryGetValue(name, out propertyCreator) || shorthands.TryGetValue(name, out propertyCreator))
                property = propertyCreator(style);

            return property;
        }

        /// <summary>
        /// Checks if the given property name is a shorthand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property is a shorthand, otherwise false.</returns>
        public static Boolean IsShorthand(String name)
        {
            return shorthands.ContainsKey(name);
        }
    }
}
