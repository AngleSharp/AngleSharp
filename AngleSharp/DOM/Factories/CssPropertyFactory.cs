namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using PropertyCreator = System.Func<CSSStyleDeclaration, CSSProperty>;

    /// <summary>
    /// Provides string to CSSProperty instance creation mappings.
    /// </summary>
    static class CssPropertyFactory
    {
        #region Fields

        static readonly Dictionary<String, PropertyCreator> longhands = new Dictionary<String, PropertyCreator>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, PropertyCreator> shorthands = new Dictionary<String, PropertyCreator>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, String[]> mappings = new Dictionary<String, String[]>();

        #endregion

        #region Initialization

        static CssPropertyFactory()
        {
            AddShorthand(PropertyNames.Animation, style => new CSSAnimationProperty(style), 
                PropertyNames.AnimationDelay, 
                PropertyNames.AnimationDirection, 
                PropertyNames.AnimationDuration, 
                PropertyNames.AnimationFillMode, 
                PropertyNames.AnimationIterationCount, 
                PropertyNames.AnimationName, 
                PropertyNames.AnimationTimingFunction);
            AddLonghand(PropertyNames.AnimationDelay, style => new CSSAnimationDelayProperty(style));
            AddLonghand(PropertyNames.AnimationDirection, style => new CSSAnimationDirectionProperty(style));
            AddLonghand(PropertyNames.AnimationDuration, style => new CSSAnimationDurationProperty(style));
            AddLonghand(PropertyNames.AnimationFillMode, style => new CSSAnimationFillModeProperty(style));
            AddLonghand(PropertyNames.AnimationIterationCount, style => new CSSAnimationIterationCountProperty(style));
            AddLonghand(PropertyNames.AnimationName, style => new CSSAnimationNameProperty(style));
            AddLonghand(PropertyNames.AnimationPlayState, style => new CSSAnimationPlayStateProperty(style));
            AddLonghand(PropertyNames.AnimationTimingFunction, style => new CSSAnimationTimingFunctionProperty(style));
            AddLonghand(PropertyNames.BackgroundAttachment, style => new CSSBackgroundAttachmentProperty(style));
            AddLonghand(PropertyNames.BackgroundColor, style => new CSSBackgroundColorProperty(style));
            AddLonghand(PropertyNames.BackgroundClip, style => new CSSBackgroundClipProperty(style));
            AddLonghand(PropertyNames.BackgroundOrigin, style => new CSSBackgroundOriginProperty(style));
            AddLonghand(PropertyNames.BackgroundSize, style => new CSSBackgroundSizeProperty(style));
            AddLonghand(PropertyNames.BackgroundImage, style => new CSSBackgroundImageProperty(style));
            AddLonghand(PropertyNames.BackgroundPosition, style => new CSSBackgroundPositionProperty(style));
            AddLonghand(PropertyNames.BackgroundRepeat, style => new CSSBackgroundRepeatProperty(style));
            AddShorthand(PropertyNames.Background, style => new CSSBackgroundProperty(style),
                PropertyNames.BackgroundAttachment,
                PropertyNames.BackgroundClip,
                PropertyNames.BackgroundColor,
                PropertyNames.BackgroundImage,
                PropertyNames.BackgroundOrigin,
                PropertyNames.BackgroundPosition,
                PropertyNames.BackgroundRepeat,
                PropertyNames.BackgroundSize);
            AddShorthand(PropertyNames.BorderColor, style => new CSSBorderColorProperty(style),
                PropertyNames.BorderLeftColor,
                PropertyNames.BorderRightColor,
                PropertyNames.BorderTopColor,
                PropertyNames.BorderBottomColor);
            AddLonghand(PropertyNames.BorderSpacing, style => new CSSBorderSpacingProperty(style));
            AddLonghand(PropertyNames.BorderCollapse, style => new CSSBorderCollapseProperty(style));
            AddShorthand(PropertyNames.BorderStyle, style => new CSSBorderStyleProperty(style),
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderBottomStyle);
            AddLonghand(PropertyNames.BoxShadow, style => new CSSBoxShadowProperty(style));
            AddLonghand(PropertyNames.BoxDecorationBreak, style => new CSSBoxDecorationBreak(style));
            AddLonghand(PropertyNames.BreakAfter, style => new CSSBreakAfterProperty(style));
            AddLonghand(PropertyNames.BreakBefore, style => new CSSBreakBeforeProperty(style));
            AddLonghand(PropertyNames.BreakInside, style => new CSSBreakInsideProperty(style));
            AddLonghand(PropertyNames.BackfaceVisibility, style => new CSSBackfaceVisibilityProperty(style));
            AddLonghand(PropertyNames.BorderTopLeftRadius, style => new CSSBorderTopLeftRadiusProperty(style));
            AddLonghand(PropertyNames.BorderTopRightRadius, style => new CSSBorderTopRightRadiusProperty(style));
            AddLonghand(PropertyNames.BorderBottomLeftRadius, style => new CSSBorderBottomLeftRadiusProperty(style));
            AddLonghand(PropertyNames.BorderBottomRightRadius, style => new CSSBorderBottomRightRadiusProperty(style));
            AddLonghand(PropertyNames.BorderRadius, style => new CSSBorderRadiusProperty(style));
            AddLonghand(PropertyNames.BorderImage, style => new CSSBorderImageProperty(style));
            AddLonghand(PropertyNames.BorderImageOutset, style => new CSSBorderImageOutsetProperty(style));
            AddLonghand(PropertyNames.BorderImageRepeat, style => new CSSBorderImageRepeatProperty(style));
            AddLonghand(PropertyNames.BorderImageSource, style => new CSSBorderImageSourceProperty(style));
            AddLonghand(PropertyNames.BorderImageSlice, style => new CSSBorderImageSliceProperty(style));
            AddLonghand(PropertyNames.BorderImageWidth, style => new CSSBorderImageWidthProperty(style));
            AddLonghand(PropertyNames.BorderTopColor, style => new CSSBorderTopColorProperty(style));
            AddLonghand(PropertyNames.BorderLeftColor, style => new CSSBorderLeftColorProperty(style));
            AddLonghand(PropertyNames.BorderRightColor, style => new CSSBorderRightColorProperty(style));
            AddLonghand(PropertyNames.BorderBottomColor, style => new CSSBorderBottomColorProperty(style));
            AddLonghand(PropertyNames.BorderTopStyle, style => new CSSBorderTopStyleProperty(style));
            AddLonghand(PropertyNames.BorderLeftStyle, style => new CSSBorderLeftStyleProperty(style));
            AddLonghand(PropertyNames.BorderRightStyle, style => new CSSBorderRightStyleProperty(style));
            AddLonghand(PropertyNames.BorderBottomStyle, style => new CSSBorderBottomStyleProperty(style));
            AddLonghand(PropertyNames.BorderTopWidth, style => new CSSBorderTopWidthProperty(style));
            AddLonghand(PropertyNames.BorderLeftWidth, style => new CSSBorderLeftWidthProperty(style));
            AddLonghand(PropertyNames.BorderRightWidth, style => new CSSBorderRightWidthProperty(style));
            AddLonghand(PropertyNames.BorderBottomWidth, style => new CSSBorderBottomWidthProperty(style));
            AddShorthand(PropertyNames.BorderWidth, style => new CSSBorderWidthProperty(style),
                PropertyNames.BorderLeftWidth,
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderBottomWidth);
            AddShorthand(PropertyNames.BorderTop, style => new CSSBorderTopProperty(style),
                PropertyNames.BorderTopColor,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderTopWidth);
            AddShorthand(PropertyNames.BorderRight, style => new CSSBorderRightProperty(style),
                PropertyNames.BorderRightColor,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderRightWidth);
            AddShorthand(PropertyNames.BorderBottom, style => new CSSBorderBottomProperty(style),
                PropertyNames.BorderBottomColor,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderBottomWidth);
            AddShorthand(PropertyNames.BorderLeft, style => new CSSBorderLeftProperty(style),
                PropertyNames.BorderLeftColor,
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderLeftWidth);
            AddShorthand(PropertyNames.Border, style => new CSSBorderProperty(style),
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderLeftColor,
                PropertyNames.BorderLeftWidth,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderRightColor,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderTopColor,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderBottomColor);
            AddLonghand(PropertyNames.Bottom, style => new CSSBottomProperty(style));
            AddShorthand(PropertyNames.Columns, style => new CSSColumnsProperty(style),
                PropertyNames.ColumnCount,
                PropertyNames.ColumnWidth);
            AddLonghand(PropertyNames.ColumnCount, style => new CSSColumnCountProperty(style));
            AddLonghand(PropertyNames.ColumnWidth, style => new CSSColumnWidthProperty(style));
            AddLonghand(PropertyNames.ColumnFill, style => new CSSColumnFillProperty(style));
            AddLonghand(PropertyNames.ColumnGap, style => new CSSColumnGapProperty(style));
            AddLonghand(PropertyNames.ColumnSpan, style => new CSSColumnSpanProperty(style));
            AddShorthand(PropertyNames.ColumnRule, style => new CSSColumnRuleProperty(style),
                PropertyNames.ColumnRuleColor,
                PropertyNames.ColumnRuleStyle,
                PropertyNames.ColumnRuleWidth);
            AddLonghand(PropertyNames.ColumnRuleColor, style => new CSSColumnRuleColorProperty(style));
            AddLonghand(PropertyNames.ColumnRuleStyle, style => new CSSColumnRuleStyleProperty(style));
            AddLonghand(PropertyNames.ColumnRuleWidth, style => new CSSColumnRuleWidthProperty(style));
            AddLonghand(PropertyNames.CaptionSide, style => new CSSCaptionSideProperty(style));
            AddLonghand(PropertyNames.Clear, style => new CSSClearProperty(style));
            AddLonghand(PropertyNames.Clip, style => new CSSClipProperty(style));
            AddLonghand(PropertyNames.Color, style => new CSSColorProperty(style));
            AddLonghand(PropertyNames.Content, style => new CSSContentProperty(style));
            AddLonghand(PropertyNames.CounterIncrement, style => new CSSCounterIncrementProperty(style));
            AddLonghand(PropertyNames.CounterReset, style => new CSSCounterResetProperty(style));
            AddLonghand(PropertyNames.Cursor, style => new CSSCursorProperty(style));
            AddLonghand(PropertyNames.Direction, style => new CSSDirectionProperty(style));
            AddLonghand(PropertyNames.Display, style => new CSSDisplayProperty(style));
            AddLonghand(PropertyNames.EmptyCells, style => new CSSEmptyCellsProperty(style));
            AddLonghand(PropertyNames.Float, style => new CSSFloatProperty(style));
            AddLonghand(PropertyNames.FontFamily, style => new CSSFontFamilyProperty(style));
            AddLonghand(PropertyNames.FontSize, style => new CSSFontSizeProperty(style));
            AddLonghand(PropertyNames.FontSizeAdjust, style => new CSSFontSizeAdjustProperty(style));
            AddLonghand(PropertyNames.FontStyle, style => new CSSFontStyleProperty(style));
            AddLonghand(PropertyNames.FontVariant, style => new CSSFontVariantProperty(style));
            AddLonghand(PropertyNames.FontWeight, style => new CSSFontWeightProperty(style));
            AddLonghand(PropertyNames.FontStretch, style => new CSSFontStretchProperty(style));
            AddShorthand(PropertyNames.Font, style => new CSSFontProperty(style),
                PropertyNames.FontFamily,
                PropertyNames.FontSize,
                PropertyNames.FontStretch,
                PropertyNames.FontStyle,
                PropertyNames.FontVariant,
                PropertyNames.FontWeight,
                PropertyNames.LineHeight);
            AddLonghand(PropertyNames.Height, style => new CSSHeightProperty(style));
            AddLonghand(PropertyNames.Left, style => new CSSLeftProperty(style));
            AddLonghand(PropertyNames.LetterSpacing, style => new CSSLetterSpacingProperty(style));
            AddLonghand(PropertyNames.LineHeight, style => new CSSLineHeightProperty(style));
            AddLonghand(PropertyNames.ListStyleImage, style => new CSSListStyleImageProperty(style));
            AddLonghand(PropertyNames.ListStylePosition, style => new CSSListStylePositionProperty(style));
            AddLonghand(PropertyNames.ListStyleType, style => new CSSListStyleTypeProperty(style));
            AddShorthand(PropertyNames.ListStyle, style => new CSSListStyleProperty(style),
                PropertyNames.ListStyleImage,
                PropertyNames.ListStylePosition,
                PropertyNames.ListStyleType);
            AddLonghand(PropertyNames.MarginRight, style => new CSSMarginRightProperty(style));
            AddLonghand(PropertyNames.MarginLeft, style => new CSSMarginLeftProperty(style));
            AddLonghand(PropertyNames.MarginTop, style => new CSSMarginTopProperty(style));
            AddLonghand(PropertyNames.MarginBottom, style => new CSSMarginBottomProperty(style));
            AddShorthand(PropertyNames.Margin, style => new CSSMarginProperty(style),
                PropertyNames.MarginBottom,
                PropertyNames.MarginLeft,
                PropertyNames.MarginRight,
                PropertyNames.MarginTop);
            AddLonghand(PropertyNames.MaxHeight, style => new CSSMaxHeightProperty(style));
            AddLonghand(PropertyNames.MaxWidth, style => new CSSMaxWidthProperty(style));
            AddLonghand(PropertyNames.MinHeight, style => new CSSMinHeightProperty(style));
            AddLonghand(PropertyNames.MinWidth, style => new CSSMinWidthProperty(style));
            AddLonghand(PropertyNames.Opacity, style => new CSSOpacityProperty(style));
            AddLonghand(PropertyNames.Orphans, style => new CSSOrphansProperty(style));
            AddShorthand(PropertyNames.Outline, style => new CSSOutlineProperty(style),
                PropertyNames.OutlineColor,
                PropertyNames.OutlineStyle,
                PropertyNames.OutlineWidth);
            AddLonghand(PropertyNames.OutlineColor, style => new CSSOutlineColorProperty(style));
            AddLonghand(PropertyNames.OutlineStyle, style => new CSSOutlineStyleProperty(style));
            AddLonghand(PropertyNames.OutlineWidth, style => new CSSOutlineWidthProperty(style));
            AddLonghand(PropertyNames.Overflow, style => new CSSOverflowProperty(style));
            AddLonghand(PropertyNames.PaddingTop, style => new CSSPaddingTopProperty(style));
            AddLonghand(PropertyNames.PaddingRight, style => new CSSPaddingRightProperty(style));
            AddLonghand(PropertyNames.PaddingLeft, style => new CSSPaddingLeftProperty(style));
            AddLonghand(PropertyNames.PaddingBottom, style => new CSSPaddingBottomProperty(style));
            AddShorthand(PropertyNames.Padding, style => new CSSPaddingProperty(style),
                PropertyNames.PaddingBottom,
                PropertyNames.PaddingLeft,
                PropertyNames.PaddingRight,
                PropertyNames.PaddingTop);
            AddLonghand(PropertyNames.PageBreakAfter, style => new CSSPageBreakAfterProperty(style));
            AddLonghand(PropertyNames.PageBreakBefore, style => new CSSPageBreakBeforeProperty(style));
            AddLonghand(PropertyNames.PageBreakInside, style => new CSSPageBreakInsideProperty(style));
            AddLonghand(PropertyNames.Perspective, style => new CSSPerspectiveProperty(style));
            AddLonghand(PropertyNames.PerspectiveOrigin, style => new CSSPerspectiveOriginProperty(style));
            AddLonghand(PropertyNames.Position, style => new CSSPositionProperty(style));
            AddLonghand(PropertyNames.Quotes, style => new CSSQuotesProperty(style));
            AddLonghand(PropertyNames.Right, style => new CSSRightProperty(style));
            AddLonghand(PropertyNames.TableLayout, style => new CSSTableLayoutProperty(style));
            AddLonghand(PropertyNames.TextAlign, style => new CSSTextAlignProperty(style));
            AddShorthand(PropertyNames.TextDecoration, style => new CSSTextDecorationProperty(style),
                PropertyNames.TextDecorationColor,
                PropertyNames.TextDecorationLine,
                PropertyNames.TextDecorationStyle);
            AddLonghand(PropertyNames.TextDecorationStyle, style => new CSSTextDecorationStyleProperty(style));
            AddLonghand(PropertyNames.TextDecorationLine, style => new CSSTextDecorationLineProperty(style));
            AddLonghand(PropertyNames.TextDecorationColor, style => new CSSTextDecorationColorProperty(style));
            AddLonghand(PropertyNames.TextIndent, style => new CSSTextIndentProperty(style));
            AddLonghand(PropertyNames.TextTransform, style => new CSSTextTransformProperty(style));
            AddLonghand(PropertyNames.TextShadow, style => new CSSTextShadowProperty(style));
            AddLonghand(PropertyNames.Transform, style => new CSSTransformProperty(style));
            AddLonghand(PropertyNames.TransformOrigin, style => new CSSTransformOriginProperty(style));
            AddLonghand(PropertyNames.TransformStyle, style => new CSSTransformStyleProperty(style));
            AddShorthand(PropertyNames.Transition, style => new CSSTransitionProperty(style),
                PropertyNames.TransitionDelay,
                PropertyNames.TransitionDuration,
                PropertyNames.TransitionProperty,
                PropertyNames.TransitionTimingFunction);
            AddLonghand(PropertyNames.TransitionDelay, style => new CSSTransitionDelayProperty(style));
            AddLonghand(PropertyNames.TransitionDuration, style => new CSSTransitionDurationProperty(style));
            AddLonghand(PropertyNames.TransitionTimingFunction, style => new CSSTransitionTimingFunctionProperty(style));
            AddLonghand(PropertyNames.TransitionProperty, style => new CSSTransitionPropertyProperty(style));
            AddLonghand(PropertyNames.Top, style => new CSSTopProperty(style));
            AddLonghand(PropertyNames.UnicodeBidi, style => new CSSUnicodeBidiProperty(style));
            AddLonghand(PropertyNames.VerticalAlign, style => new CSSVerticalAlignProperty(style));
            AddLonghand(PropertyNames.Visibility, style => new CSSVisibilityProperty(style));
            AddLonghand(PropertyNames.WhiteSpace, style => new CSSWhiteSpaceProperty(style));
            AddLonghand(PropertyNames.Widows, style => new CSSWidowsProperty(style));
            AddLonghand(PropertyNames.Width, style => new CSSWidthProperty(style));
            AddLonghand(PropertyNames.WordSpacing, style => new CSSWordSpacingProperty(style));
            AddLonghand(PropertyNames.ZIndex, style => new CSSZIndexProperty(style));
        }

        static void AddShorthand(String name, PropertyCreator creator, params String[] longhands)
        {
            shorthands.Add(name, creator);
            mappings.Add(name, longhands);
        }

        static void AddLonghand(String name, PropertyCreator creator)
        {
            longhands.Add(name, creator);
        }

        #endregion

        #region Creation

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created property.</returns>
        public static CSSProperty Create(String name, CSSStyleDeclaration style)
        {
            PropertyCreator propertyCreator;
            var property = style.GetProperty(name);

            if (property != null)
                return property;

            if (longhands.TryGetValue(name, out propertyCreator) || shorthands.TryGetValue(name, out propertyCreator))
                property = propertyCreator(style);

            return property;
        }

        /// <summary>
        /// Creates a new longhand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created longhand property.</returns>
        public static CSSProperty CreateLonghand(String name, CSSStyleDeclaration style)
        {
            PropertyCreator propertyCreator;
            var property = style.GetProperty(name);

            if (property != null)
                return property;

            if (longhands.TryGetValue(name, out propertyCreator))
                return propertyCreator(style);

            return null;
        }

        /// <summary>
        /// Creates a new shorthand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created shorthand property.</returns>
        public static CSSProperty CreateShorthand(String name, CSSStyleDeclaration style)
        {
            PropertyCreator propertyCreator;

            if (shorthands.TryGetValue(name, out propertyCreator))
                return propertyCreator(style);

            return null;
        }

        /// <summary>
        /// Creates a series of longhand properties for the provided shorthand.
        /// </summary>
        /// <param name="name">The name of the corresponding shorthand property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created longhand properties.</returns>
        public static IEnumerable<CSSProperty> CreateLonghandsFor(String name, CSSStyleDeclaration style)
        {
            return GetLonghands(name).Select(m => CreateLonghand(m, style));
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Checks if the given property name is a longhand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property is a longhand, otherwise false.</returns>
        public static Boolean IsLonghand(String name)
        {
            return longhands.ContainsKey(name);
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

        /// <summary>
        /// Checks if the given property name if generally supported.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property name is supported, otherwise false.</returns>
        public static Boolean IsSupported(String name)
        {
            return IsLonghand(name) || IsShorthand(name);
        }

        /// <summary>
        /// Gets the longhands that map to the specified shorthand property.
        /// </summary>
        /// <param name="name">The name of the shorthand property.</param>
        /// <returns>An enumeration over all longhand properties.</returns>
        public static IEnumerable<String> GetLonghands(String name)
        {
            if (mappings.ContainsKey(name))
                return mappings[name];
            
            return Enumerable.Empty<String>();
        }

        /// <summary>
        /// Gets the shorthands that map to the specified longhand property.
        /// </summary>
        /// <param name="name">The name of the longhand property.</param>
        /// <returns>An enumeration over all shorthand properties.</returns>
        public static IEnumerable<String> GetShorthands(String name)
        {
            foreach (var mapping in mappings)
            {
                if (mapping.Value.Contains(name, StringComparison.OrdinalIgnoreCase))
                    yield return mapping.Key;
            }
        }

        #endregion
    }
}
