namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides string to CSSProperty instance creation mappings.
    /// </summary>
    static class CssPropertyFactory
    {
        #region Delegates

        delegate CSSProperty LonghandCreator(CssStyleDeclaration style);
        delegate CSSShorthandProperty ShorthandCreator(CssStyleDeclaration style);

        #endregion

        #region Fields

        static readonly Dictionary<String, LonghandCreator> longhands = new Dictionary<String, LonghandCreator>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, ShorthandCreator> shorthands = new Dictionary<String, ShorthandCreator>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, String[]> mappings = new Dictionary<String, String[]>();
        static readonly List<String> animatables = new List<String>();

        #endregion

        #region Initialization

        static CssPropertyFactory()
        {
            AddShorthand(PropertyNames.Animation, style => new CSSAnimationProperty(style),
                PropertyNames.AnimationName,
                PropertyNames.AnimationDuration,
                PropertyNames.AnimationTimingFunction,
                PropertyNames.AnimationDelay, 
                PropertyNames.AnimationDirection, 
                PropertyNames.AnimationFillMode, 
                PropertyNames.AnimationIterationCount, 
                PropertyNames.AnimationPlayState);
            AddLonghand(PropertyNames.AnimationDelay, style => new CSSAnimationDelayProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationDirection, style => new CSSAnimationDirectionProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationDuration, style => new CSSAnimationDurationProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationFillMode, style => new CSSAnimationFillModeProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationIterationCount, style => new CSSAnimationIterationCountProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationName, style => new CSSAnimationNameProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationPlayState, style => new CSSAnimationPlayStateProperty(style), animatable: false);
            AddLonghand(PropertyNames.AnimationTimingFunction, style => new CSSAnimationTimingFunctionProperty(style), animatable: false);

            AddShorthand(PropertyNames.Background, style => new CSSBackgroundProperty(style),
                PropertyNames.BackgroundAttachment,
                PropertyNames.BackgroundClip,
                PropertyNames.BackgroundColor,
                PropertyNames.BackgroundImage,
                PropertyNames.BackgroundOrigin,
                PropertyNames.BackgroundPosition,
                PropertyNames.BackgroundRepeat,
                PropertyNames.BackgroundSize);
            AddLonghand(PropertyNames.BackgroundAttachment, style => new CSSBackgroundAttachmentProperty(style), animatable: false);
            AddLonghand(PropertyNames.BackgroundColor, style => new CSSBackgroundColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.BackgroundClip, style => new CSSBackgroundClipProperty(style), animatable: false);
            AddLonghand(PropertyNames.BackgroundOrigin, style => new CSSBackgroundOriginProperty(style), animatable: false);
            AddLonghand(PropertyNames.BackgroundSize, style => new CSSBackgroundSizeProperty(style), animatable: true);
            AddLonghand(PropertyNames.BackgroundImage, style => new CSSBackgroundImageProperty(style), animatable: false);
            AddLonghand(PropertyNames.BackgroundPosition, style => new CSSBackgroundPositionProperty(style), animatable: true);
            AddLonghand(PropertyNames.BackgroundRepeat, style => new CSSBackgroundRepeatProperty(style), animatable: false);

            AddLonghand(PropertyNames.BorderSpacing, style => new CSSBorderSpacingProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderCollapse, style => new CSSBorderCollapseProperty(style), animatable: false);
            AddLonghand(PropertyNames.BoxShadow, style => new CSSBoxShadowProperty(style), animatable: true);
            AddLonghand(PropertyNames.BoxDecorationBreak, style => new CSSBoxDecorationBreak(style), animatable: false);
            AddLonghand(PropertyNames.BreakAfter, style => new CSSBreakAfterProperty(style), animatable: false);
            AddLonghand(PropertyNames.BreakBefore, style => new CSSBreakBeforeProperty(style), animatable: false);
            AddLonghand(PropertyNames.BreakInside, style => new CSSBreakInsideProperty(style), animatable: false);
            AddLonghand(PropertyNames.BackfaceVisibility, style => new CSSBackfaceVisibilityProperty(style), animatable: false);

            AddShorthand(PropertyNames.BorderRadius, style => new CSSBorderRadiusProperty(style),
                PropertyNames.BorderTopLeftRadius,
                PropertyNames.BorderTopRightRadius,
                PropertyNames.BorderBottomRightRadius,
                PropertyNames.BorderBottomLeftRadius);
            AddLonghand(PropertyNames.BorderTopLeftRadius, style => new CSSBorderTopLeftRadiusProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderTopRightRadius, style => new CSSBorderTopRightRadiusProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderBottomLeftRadius, style => new CSSBorderBottomLeftRadiusProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderBottomRightRadius, style => new CSSBorderBottomRightRadiusProperty(style), animatable: true);

            AddShorthand(PropertyNames.BorderImage, style => new CSSBorderImageProperty(style),
                PropertyNames.BorderImageOutset,
                PropertyNames.BorderImageRepeat,
                PropertyNames.BorderImageSlice,
                PropertyNames.BorderImageSource,
                PropertyNames.BorderImageWidth);
            AddLonghand(PropertyNames.BorderImageOutset, style => new CSSBorderImageOutsetProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderImageRepeat, style => new CSSBorderImageRepeatProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderImageSource, style => new CSSBorderImageSourceProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderImageSlice, style => new CSSBorderImageSliceProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderImageWidth, style => new CSSBorderImageWidthProperty(style), animatable: false);

            AddShorthand(PropertyNames.BorderColor, style => new CSSBorderColorProperty(style),
                PropertyNames.BorderTopColor,
                PropertyNames.BorderRightColor,
                PropertyNames.BorderBottomColor,
                PropertyNames.BorderLeftColor);
            AddShorthand(PropertyNames.BorderStyle, style => new CSSBorderStyleProperty(style),
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderLeftStyle);
            AddShorthand(PropertyNames.BorderWidth, style => new CSSBorderWidthProperty(style),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderLeftWidth);
            AddShorthand(PropertyNames.BorderTop, style => new CSSBorderTopProperty(style),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderTopColor);
            AddShorthand(PropertyNames.BorderRight, style => new CSSBorderRightProperty(style),
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderRightColor);
            AddShorthand(PropertyNames.BorderBottom, style => new CSSBorderBottomProperty(style),
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderBottomColor);
            AddShorthand(PropertyNames.BorderLeft, style => new CSSBorderLeftProperty(style),
                PropertyNames.BorderLeftWidth,
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderLeftColor);

            AddShorthand(PropertyNames.Border, style => new CSSBorderProperty(style),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderTopColor,
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderRightColor,
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderBottomColor,
                PropertyNames.BorderLeftWidth,
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderLeftColor);
            AddLonghand(PropertyNames.BorderTopColor, style => new CSSBorderTopColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderLeftColor, style => new CSSBorderLeftColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderRightColor, style => new CSSBorderRightColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderBottomColor, style => new CSSBorderBottomColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderTopStyle, style => new CSSBorderTopStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderLeftStyle, style => new CSSBorderLeftStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderRightStyle, style => new CSSBorderRightStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderBottomStyle, style => new CSSBorderBottomStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.BorderTopWidth, style => new CSSBorderTopWidthProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderLeftWidth, style => new CSSBorderLeftWidthProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderRightWidth, style => new CSSBorderRightWidthProperty(style), animatable: true);
            AddLonghand(PropertyNames.BorderBottomWidth, style => new CSSBorderBottomWidthProperty(style), animatable: true);

            AddLonghand(PropertyNames.Bottom, style => new CSSBottomProperty(style), animatable: true);

            AddShorthand(PropertyNames.Columns, style => new CSSColumnsProperty(style),
                PropertyNames.ColumnWidth,
                PropertyNames.ColumnCount);
            AddLonghand(PropertyNames.ColumnCount, style => new CSSColumnCountProperty(style), animatable: true);
            AddLonghand(PropertyNames.ColumnWidth, style => new CSSColumnWidthProperty(style), animatable: true);

            AddLonghand(PropertyNames.ColumnFill, style => new CSSColumnFillProperty(style), animatable: false);
            AddLonghand(PropertyNames.ColumnGap, style => new CSSColumnGapProperty(style), animatable: true);
            AddLonghand(PropertyNames.ColumnSpan, style => new CSSColumnSpanProperty(style), animatable: false);

            AddShorthand(PropertyNames.ColumnRule, style => new CSSColumnRuleProperty(style),
                PropertyNames.ColumnRuleWidth,
                PropertyNames.ColumnRuleStyle,
                PropertyNames.ColumnRuleColor);
            AddLonghand(PropertyNames.ColumnRuleColor, style => new CSSColumnRuleColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.ColumnRuleStyle, style => new CSSColumnRuleStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.ColumnRuleWidth, style => new CSSColumnRuleWidthProperty(style), animatable: true);

            AddLonghand(PropertyNames.CaptionSide, style => new CSSCaptionSideProperty(style), animatable: false);
            AddLonghand(PropertyNames.Clear, style => new CSSClearProperty(style), animatable: false);
            AddLonghand(PropertyNames.Clip, style => new CSSClipProperty(style), animatable: true);
            AddLonghand(PropertyNames.Color, style => new CSSColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.Content, style => new CSSContentProperty(style), animatable: false);
            AddLonghand(PropertyNames.CounterIncrement, style => new CSSCounterIncrementProperty(style));
            AddLonghand(PropertyNames.CounterReset, style => new CSSCounterResetProperty(style), animatable: false);
            AddLonghand(PropertyNames.Cursor, style => new CSSCursorProperty(style), animatable: false);
            AddLonghand(PropertyNames.Direction, style => new CSSDirectionProperty(style), animatable: false);
            AddLonghand(PropertyNames.Display, style => new CSSDisplayProperty(style), animatable: false);
            AddLonghand(PropertyNames.EmptyCells, style => new CSSEmptyCellsProperty(style), animatable: false);
            AddLonghand(PropertyNames.Float, style => new CSSFloatProperty(style), animatable: false);

            AddShorthand(PropertyNames.Font, style => new CSSFontProperty(style),
                PropertyNames.FontFamily,
                PropertyNames.FontSize,
                PropertyNames.FontStretch,
                PropertyNames.FontStyle,
                PropertyNames.FontVariant,
                PropertyNames.FontWeight,
                PropertyNames.LineHeight);
            AddLonghand(PropertyNames.FontFamily, style => new CSSFontFamilyProperty(style), animatable: false);
            AddLonghand(PropertyNames.FontSize, style => new CSSFontSizeProperty(style), animatable: true);
            AddLonghand(PropertyNames.FontSizeAdjust, style => new CSSFontSizeAdjustProperty(style), animatable: true);
            AddLonghand(PropertyNames.FontStyle, style => new CSSFontStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.FontVariant, style => new CSSFontVariantProperty(style), animatable: false);
            AddLonghand(PropertyNames.FontWeight, style => new CSSFontWeightProperty(style), animatable: true);
            AddLonghand(PropertyNames.FontStretch, style => new CSSFontStretchProperty(style), animatable: true);
            AddLonghand(PropertyNames.LineHeight, style => new CSSLineHeightProperty(style), animatable: true);

            AddLonghand(PropertyNames.Height, style => new CSSHeightProperty(style), animatable: true);
            AddLonghand(PropertyNames.Left, style => new CSSLeftProperty(style), animatable: true);
            AddLonghand(PropertyNames.LetterSpacing, style => new CSSLetterSpacingProperty(style), animatable: false);

            AddShorthand(PropertyNames.ListStyle, style => new CSSListStyleProperty(style),
                PropertyNames.ListStyleType,
                PropertyNames.ListStyleImage,
                PropertyNames.ListStylePosition);
            AddLonghand(PropertyNames.ListStyleImage, style => new CSSListStyleImageProperty(style), animatable: false);
            AddLonghand(PropertyNames.ListStylePosition, style => new CSSListStylePositionProperty(style), animatable: false);
            AddLonghand(PropertyNames.ListStyleType, style => new CSSListStyleTypeProperty(style), animatable: false);

            AddShorthand(PropertyNames.Margin, style => new CSSMarginProperty(style),
                PropertyNames.MarginTop,
                PropertyNames.MarginRight,
                PropertyNames.MarginBottom,
                PropertyNames.MarginLeft);
            AddLonghand(PropertyNames.MarginRight, style => new CSSMarginRightProperty(style), animatable: true);
            AddLonghand(PropertyNames.MarginLeft, style => new CSSMarginLeftProperty(style), animatable: true);
            AddLonghand(PropertyNames.MarginTop, style => new CSSMarginTopProperty(style), animatable: true);
            AddLonghand(PropertyNames.MarginBottom, style => new CSSMarginBottomProperty(style), animatable: true);

            AddLonghand(PropertyNames.MaxHeight, style => new CSSMaxHeightProperty(style), animatable: true);
            AddLonghand(PropertyNames.MaxWidth, style => new CSSMaxWidthProperty(style), animatable: true);
            AddLonghand(PropertyNames.MinHeight, style => new CSSMinHeightProperty(style), animatable: true);
            AddLonghand(PropertyNames.MinWidth, style => new CSSMinWidthProperty(style), animatable: true);
            AddLonghand(PropertyNames.Opacity, style => new CSSOpacityProperty(style), animatable: true);
            AddLonghand(PropertyNames.Orphans, style => new CSSOrphansProperty(style), animatable: false);

            AddShorthand(PropertyNames.Outline, style => new CSSOutlineProperty(style),
                PropertyNames.OutlineWidth,
                PropertyNames.OutlineStyle,
                PropertyNames.OutlineColor);
            AddLonghand(PropertyNames.OutlineColor, style => new CSSOutlineColorProperty(style), animatable: true);
            AddLonghand(PropertyNames.OutlineStyle, style => new CSSOutlineStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.OutlineWidth, style => new CSSOutlineWidthProperty(style), animatable: true);

            AddLonghand(PropertyNames.Overflow, style => new CSSOverflowProperty(style), animatable: false);

            AddShorthand(PropertyNames.Padding, style => new CSSPaddingProperty(style),
                PropertyNames.PaddingTop,
                PropertyNames.PaddingRight,
                PropertyNames.PaddingBottom,
                PropertyNames.PaddingLeft);
            AddLonghand(PropertyNames.PaddingTop, style => new CSSPaddingTopProperty(style), animatable: true);
            AddLonghand(PropertyNames.PaddingRight, style => new CSSPaddingRightProperty(style), animatable: true);
            AddLonghand(PropertyNames.PaddingLeft, style => new CSSPaddingLeftProperty(style), animatable: true);
            AddLonghand(PropertyNames.PaddingBottom, style => new CSSPaddingBottomProperty(style), animatable: true);

            AddLonghand(PropertyNames.PageBreakAfter, style => new CSSPageBreakAfterProperty(style), animatable: false);
            AddLonghand(PropertyNames.PageBreakBefore, style => new CSSPageBreakBeforeProperty(style), animatable: false);
            AddLonghand(PropertyNames.PageBreakInside, style => new CSSPageBreakInsideProperty(style), animatable: false);
            AddLonghand(PropertyNames.Perspective, style => new CSSPerspectiveProperty(style), animatable: true);
            AddLonghand(PropertyNames.PerspectiveOrigin, style => new CSSPerspectiveOriginProperty(style), animatable: true);
            AddLonghand(PropertyNames.Position, style => new CSSPositionProperty(style), animatable: false);
            AddLonghand(PropertyNames.Quotes, style => new CSSQuotesProperty(style), animatable: false);
            AddLonghand(PropertyNames.Right, style => new CSSRightProperty(style), animatable: true);
            AddLonghand(PropertyNames.TableLayout, style => new CSSTableLayoutProperty(style), animatable: false);
            AddLonghand(PropertyNames.TextAlign, style => new CSSTextAlignProperty(style), animatable: false);

            AddShorthand(PropertyNames.TextDecoration, style => new CSSTextDecorationProperty(style),
                PropertyNames.TextDecorationLine,
                PropertyNames.TextDecorationStyle,
                PropertyNames.TextDecorationColor);
            AddLonghand(PropertyNames.TextDecorationStyle, style => new CSSTextDecorationStyleProperty(style), animatable: false);
            AddLonghand(PropertyNames.TextDecorationLine, style => new CSSTextDecorationLineProperty(style), animatable: false);
            AddLonghand(PropertyNames.TextDecorationColor, style => new CSSTextDecorationColorProperty(style), animatable: true);

            AddLonghand(PropertyNames.TextIndent, style => new CSSTextIndentProperty(style), animatable: true);
            AddLonghand(PropertyNames.TextTransform, style => new CSSTextTransformProperty(style), animatable: false);
            AddLonghand(PropertyNames.TextShadow, style => new CSSTextShadowProperty(style), animatable: true);
            AddLonghand(PropertyNames.Transform, style => new CSSTransformProperty(style), animatable: true);
            AddLonghand(PropertyNames.TransformOrigin, style => new CSSTransformOriginProperty(style), animatable: true);
            AddLonghand(PropertyNames.TransformStyle, style => new CSSTransformStyleProperty(style), animatable: false);

            AddShorthand(PropertyNames.Transition, style => new CSSTransitionProperty(style),
                PropertyNames.TransitionProperty,
                PropertyNames.TransitionDuration,
                PropertyNames.TransitionTimingFunction,
                PropertyNames.TransitionDelay);
            AddLonghand(PropertyNames.TransitionDelay, style => new CSSTransitionDelayProperty(style));
            AddLonghand(PropertyNames.TransitionDuration, style => new CSSTransitionDurationProperty(style));
            AddLonghand(PropertyNames.TransitionTimingFunction, style => new CSSTransitionTimingFunctionProperty(style));
            AddLonghand(PropertyNames.TransitionProperty, style => new CSSTransitionPropertyProperty(style));

            AddLonghand(PropertyNames.Top, style => new CSSTopProperty(style), animatable: true);
            AddLonghand(PropertyNames.UnicodeBidi, style => new CSSUnicodeBidiProperty(style), animatable: false);
            AddLonghand(PropertyNames.VerticalAlign, style => new CSSVerticalAlignProperty(style), animatable: true);
            AddLonghand(PropertyNames.Visibility, style => new CSSVisibilityProperty(style), animatable: true);
            AddLonghand(PropertyNames.WhiteSpace, style => new CSSWhiteSpaceProperty(style), animatable: false);
            AddLonghand(PropertyNames.Widows, style => new CSSWidowsProperty(style), animatable: false);
            AddLonghand(PropertyNames.Width, style => new CSSWidthProperty(style), animatable: true);
            AddLonghand(PropertyNames.WordSpacing, style => new CSSWordSpacingProperty(style), animatable: true);
            AddLonghand(PropertyNames.ZIndex, style => new CSSZIndexProperty(style), animatable: true);
            AddLonghand(PropertyNames.ObjectFit, style => new CSSObjectFitProperty(style), animatable: false);
            AddLonghand(PropertyNames.ObjectPosition, style => new CSSObjectPositionProperty(style), animatable: true);
        }

        static void AddShorthand(String name, ShorthandCreator creator, params String[] longhands)
        {
            shorthands.Add(name, creator);
            mappings.Add(name, longhands);
        }

        static void AddLonghand(String name, LonghandCreator creator, Boolean animatable = false)
        {
            longhands.Add(name, creator);

            if (animatable)
                animatables.Add(name);
        }

        #endregion

        #region Creation

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created property.</returns>
        public static CSSProperty Create(String name, CssStyleDeclaration style)
        {
            return CreateLonghand(name, style) ?? CreateShorthand(name, style);
        }

        /// <summary>
        /// Creates a new longhand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created longhand property.</returns>
        public static CSSProperty CreateLonghand(String name, CssStyleDeclaration style)
        {
            LonghandCreator longhand;
            var property = style.GetProperty(name);

            if (property == null && longhands.TryGetValue(name, out longhand))
                property = longhand(style);

            return property;
        }

        /// <summary>
        /// Creates a new shorthand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created shorthand property.</returns>
        public static CSSShorthandProperty CreateShorthand(String name, CssStyleDeclaration style)
        {
            ShorthandCreator shorthand;

            if (shorthands.TryGetValue(name, out shorthand))
                return shorthand(style);

            return null;
        }

        /// <summary>
        /// Creates a series of longhand properties for the provided shorthand.
        /// </summary>
        /// <param name="name">The name of the corresponding shorthand property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created longhand properties.</returns>
        public static IEnumerable<CSSProperty> CreateLonghandsFor(String name, CssStyleDeclaration style)
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
        /// Checks if the given property name is actually animatable.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property is animatable, or has a longhand that is animatable.</returns>
        public static Boolean IsAnimatable(String name)
        {
            if (IsLonghand(name))
                return animatables.Contains(name);

            foreach (var longhand in GetLonghands(name))
            {
                if (animatables.Contains(name))
                    return true;
            }

            return false;
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
