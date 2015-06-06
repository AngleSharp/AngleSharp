namespace AngleSharp.Factories
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Provides string to CSSProperty instance creation mappings.
    /// </summary>
    sealed class CssPropertyFactory
    {
        #region Delegates

        delegate CssProperty LonghandCreator(CssStyleDeclaration style);
        delegate CssShorthandProperty ShorthandCreator(CssStyleDeclaration style);

        #endregion

        #region Fields

        readonly Dictionary<String, LonghandCreator> longhands = new Dictionary<String, LonghandCreator>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<String, ShorthandCreator> shorthands = new Dictionary<String, ShorthandCreator>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<String, String[]> mappings = new Dictionary<String, String[]>();
        readonly List<String> animatables = new List<String>();

        #endregion

        #region Initialization

        public CssPropertyFactory()
        {
            AddShorthand(PropertyNames.Animation, style => new CssAnimationProperty(style),
                PropertyNames.AnimationName,
                PropertyNames.AnimationDuration,
                PropertyNames.AnimationTimingFunction,
                PropertyNames.AnimationDelay, 
                PropertyNames.AnimationDirection, 
                PropertyNames.AnimationFillMode, 
                PropertyNames.AnimationIterationCount, 
                PropertyNames.AnimationPlayState);
            AddLonghand(PropertyNames.AnimationDelay, style => new CssAnimationDelayProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationDirection, style => new CssAnimationDirectionProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationDuration, style => new CssAnimationDurationProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationFillMode, style => new CssAnimationFillModeProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationIterationCount, style => new CssAnimationIterationCountProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationName, style => new CssAnimationNameProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationPlayState, style => new CssAnimationPlayStateProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationTimingFunction, style => new CssAnimationTimingFunctionProperty(), animatable: false);

            AddShorthand(PropertyNames.Background, style => new CssBackgroundProperty(style),
                PropertyNames.BackgroundAttachment,
                PropertyNames.BackgroundClip,
                PropertyNames.BackgroundColor,
                PropertyNames.BackgroundImage,
                PropertyNames.BackgroundOrigin,
                PropertyNames.BackgroundPosition,
                PropertyNames.BackgroundRepeat,
                PropertyNames.BackgroundSize);
            AddLonghand(PropertyNames.BackgroundAttachment, style => new CssBackgroundAttachmentProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundColor, style => new CssBackgroundColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BackgroundClip, style => new CssBackgroundClipProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundOrigin, style => new CssBackgroundOriginProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundSize, style => new CssBackgroundSizeProperty(), animatable: true);
            AddLonghand(PropertyNames.BackgroundImage, style => new CssBackgroundImageProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundPosition, style => new CssBackgroundPositionProperty(), animatable: true);
            AddLonghand(PropertyNames.BackgroundRepeat, style => new CssBackgroundRepeatProperty(), animatable: false);

            AddLonghand(PropertyNames.BorderSpacing, style => new CssBorderSpacingProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderCollapse, style => new CssBorderCollapseProperty(), animatable: false);
            AddLonghand(PropertyNames.BoxShadow, style => new CssBoxShadowProperty(), animatable: true);
            AddLonghand(PropertyNames.BoxDecorationBreak, style => new CssBoxDecorationBreak(), animatable: false);
            AddLonghand(PropertyNames.BreakAfter, style => new CssBreakAfterProperty(), animatable: false);
            AddLonghand(PropertyNames.BreakBefore, style => new CssBreakBeforeProperty(), animatable: false);
            AddLonghand(PropertyNames.BreakInside, style => new CssBreakInsideProperty(), animatable: false);
            AddLonghand(PropertyNames.BackfaceVisibility, style => new CssBackfaceVisibilityProperty(), animatable: false);

            AddShorthand(PropertyNames.BorderRadius, style => new CssBorderRadiusProperty(style),
                PropertyNames.BorderTopLeftRadius,
                PropertyNames.BorderTopRightRadius,
                PropertyNames.BorderBottomRightRadius,
                PropertyNames.BorderBottomLeftRadius);
            AddLonghand(PropertyNames.BorderTopLeftRadius, style => new CssBorderTopLeftRadiusProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderTopRightRadius, style => new CssBorderTopRightRadiusProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomLeftRadius, style => new CssBorderBottomLeftRadiusProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomRightRadius, style => new CssBorderBottomRightRadiusProperty(), animatable: true);

            AddShorthand(PropertyNames.BorderImage, style => new CssBorderImageProperty(style),
                PropertyNames.BorderImageOutset,
                PropertyNames.BorderImageRepeat,
                PropertyNames.BorderImageSlice,
                PropertyNames.BorderImageSource,
                PropertyNames.BorderImageWidth);
            AddLonghand(PropertyNames.BorderImageOutset, style => new CssBorderImageOutsetProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageRepeat, style => new CssBorderImageRepeatProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageSource, style => new CssBorderImageSourceProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageSlice, style => new CssBorderImageSliceProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageWidth, style => new CssBorderImageWidthProperty(), animatable: false);

            AddShorthand(PropertyNames.BorderColor, style => new CssBorderColorProperty(style),
                PropertyNames.BorderTopColor,
                PropertyNames.BorderRightColor,
                PropertyNames.BorderBottomColor,
                PropertyNames.BorderLeftColor);
            AddShorthand(PropertyNames.BorderStyle, style => new CssBorderStyleProperty(style),
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderLeftStyle);
            AddShorthand(PropertyNames.BorderWidth, style => new CssBorderWidthProperty(style),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderLeftWidth);
            AddShorthand(PropertyNames.BorderTop, style => new CssBorderTopProperty(style),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderTopColor);
            AddShorthand(PropertyNames.BorderRight, style => new CssBorderRightProperty(style),
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderRightColor);
            AddShorthand(PropertyNames.BorderBottom, style => new CssBorderBottomProperty(style),
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderBottomColor);
            AddShorthand(PropertyNames.BorderLeft, style => new CssBorderLeftProperty(style),
                PropertyNames.BorderLeftWidth,
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderLeftColor);

            AddShorthand(PropertyNames.Border, style => new CssBorderProperty(style),
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
            AddLonghand(PropertyNames.BorderTopColor, style => new CssBorderTopColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderLeftColor, style => new CssBorderLeftColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderRightColor, style => new CssBorderRightColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomColor, style => new CssBorderBottomColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderTopStyle, style => new CssBorderTopStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderLeftStyle, style => new CssBorderLeftStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderRightStyle, style => new CssBorderRightStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderBottomStyle, style => new CssBorderBottomStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderTopWidth, style => new CssBorderTopWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderLeftWidth, style => new CssBorderLeftWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderRightWidth, style => new CssBorderRightWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomWidth, style => new CssBorderBottomWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.Bottom, style => new CssBottomProperty(), animatable: true);

            AddShorthand(PropertyNames.Columns, style => new CssColumnsProperty(style),
                PropertyNames.ColumnWidth,
                PropertyNames.ColumnCount);
            AddLonghand(PropertyNames.ColumnCount, style => new CssColumnCountProperty(), animatable: true);
            AddLonghand(PropertyNames.ColumnWidth, style => new CssColumnWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.ColumnFill, style => new CssColumnFillProperty(), animatable: false);
            AddLonghand(PropertyNames.ColumnGap, style => new CssColumnGapProperty(), animatable: true);
            AddLonghand(PropertyNames.ColumnSpan, style => new CssColumnSpanProperty(), animatable: false);

            AddShorthand(PropertyNames.ColumnRule, style => new CssColumnRuleProperty(style),
                PropertyNames.ColumnRuleWidth,
                PropertyNames.ColumnRuleStyle,
                PropertyNames.ColumnRuleColor);
            AddLonghand(PropertyNames.ColumnRuleColor, style => new CssColumnRuleColorProperty(), animatable: true);
            AddLonghand(PropertyNames.ColumnRuleStyle, style => new CssColumnRuleStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.ColumnRuleWidth, style => new CssColumnRuleWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.CaptionSide, style => new CssCaptionSideProperty(), animatable: false);
            AddLonghand(PropertyNames.Clear, style => new CssClearProperty(), animatable: false);
            AddLonghand(PropertyNames.Clip, style => new CssClipProperty(), animatable: true);
            AddLonghand(PropertyNames.Color, style => new CssColorProperty(), animatable: true);
            AddLonghand(PropertyNames.Content, style => new CssContentProperty(), animatable: false);
            AddLonghand(PropertyNames.CounterIncrement, style => new CssCounterIncrementProperty());
            AddLonghand(PropertyNames.CounterReset, style => new CssCounterResetProperty(), animatable: false);
            AddLonghand(PropertyNames.Cursor, style => new CssCursorProperty(), animatable: false);
            AddLonghand(PropertyNames.Direction, style => new CssDirectionProperty(), animatable: false);
            AddLonghand(PropertyNames.Display, style => new CssDisplayProperty(), animatable: false);
            AddLonghand(PropertyNames.EmptyCells, style => new CssEmptyCellsProperty(), animatable: false);
            AddLonghand(PropertyNames.Float, style => new CssFloatProperty(), animatable: false);

            AddShorthand(PropertyNames.Font, style => new CssFontProperty(style),
                PropertyNames.FontFamily,
                PropertyNames.FontSize,
                PropertyNames.FontStretch,
                PropertyNames.FontStyle,
                PropertyNames.FontVariant,
                PropertyNames.FontWeight,
                PropertyNames.LineHeight);
            AddLonghand(PropertyNames.FontFamily, style => new CssFontFamilyProperty(), animatable: false);
            AddLonghand(PropertyNames.FontSize, style => new CssFontSizeProperty(), animatable: true);
            AddLonghand(PropertyNames.FontSizeAdjust, style => new CssFontSizeAdjustProperty(), animatable: true);
            AddLonghand(PropertyNames.FontStyle, style => new CssFontStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.FontVariant, style => new CssFontVariantProperty(), animatable: false);
            AddLonghand(PropertyNames.FontWeight, style => new CssFontWeightProperty(), animatable: true);
            AddLonghand(PropertyNames.FontStretch, style => new CssFontStretchProperty(), animatable: true);
            AddLonghand(PropertyNames.LineHeight, style => new CssLineHeightProperty(), animatable: true);

            AddLonghand(PropertyNames.Height, style => new CssHeightProperty(), animatable: true);
            AddLonghand(PropertyNames.Left, style => new CssLeftProperty(), animatable: true);
            AddLonghand(PropertyNames.LetterSpacing, style => new CssLetterSpacingProperty(), animatable: false);

            AddShorthand(PropertyNames.ListStyle, style => new CssListStyleProperty(style),
                PropertyNames.ListStyleType,
                PropertyNames.ListStyleImage,
                PropertyNames.ListStylePosition);
            AddLonghand(PropertyNames.ListStyleImage, style => new CssListStyleImageProperty(), animatable: false);
            AddLonghand(PropertyNames.ListStylePosition, style => new CssListStylePositionProperty(), animatable: false);
            AddLonghand(PropertyNames.ListStyleType, style => new CssListStyleTypeProperty(), animatable: false);

            AddShorthand(PropertyNames.Margin, style => new CssMarginProperty(style),
                PropertyNames.MarginTop,
                PropertyNames.MarginRight,
                PropertyNames.MarginBottom,
                PropertyNames.MarginLeft);
            AddLonghand(PropertyNames.MarginRight, style => new CssMarginRightProperty(), animatable: true);
            AddLonghand(PropertyNames.MarginLeft, style => new CssMarginLeftProperty(), animatable: true);
            AddLonghand(PropertyNames.MarginTop, style => new CssMarginTopProperty(), animatable: true);
            AddLonghand(PropertyNames.MarginBottom, style => new CssMarginBottomProperty(), animatable: true);

            AddLonghand(PropertyNames.MaxHeight, style => new CssMaxHeightProperty(), animatable: true);
            AddLonghand(PropertyNames.MaxWidth, style => new CssMaxWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.MinHeight, style => new CssMinHeightProperty(), animatable: true);
            AddLonghand(PropertyNames.MinWidth, style => new CssMinWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.Opacity, style => new CssOpacityProperty(), animatable: true);
            AddLonghand(PropertyNames.Orphans, style => new CssOrphansProperty(), animatable: false);

            AddShorthand(PropertyNames.Outline, style => new CssOutlineProperty(style),
                PropertyNames.OutlineWidth,
                PropertyNames.OutlineStyle,
                PropertyNames.OutlineColor);
            AddLonghand(PropertyNames.OutlineColor, style => new CssOutlineColorProperty(), animatable: true);
            AddLonghand(PropertyNames.OutlineStyle, style => new CssOutlineStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.OutlineWidth, style => new CssOutlineWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.Overflow, style => new CssOverflowProperty(), animatable: false);

            AddShorthand(PropertyNames.Padding, style => new CssPaddingProperty(style),
                PropertyNames.PaddingTop,
                PropertyNames.PaddingRight,
                PropertyNames.PaddingBottom,
                PropertyNames.PaddingLeft);
            AddLonghand(PropertyNames.PaddingTop, style => new CssPaddingTopProperty(), animatable: true);
            AddLonghand(PropertyNames.PaddingRight, style => new CssPaddingRightProperty(), animatable: true);
            AddLonghand(PropertyNames.PaddingLeft, style => new CssPaddingLeftProperty(), animatable: true);
            AddLonghand(PropertyNames.PaddingBottom, style => new CssPaddingBottomProperty(), animatable: true);

            AddLonghand(PropertyNames.PageBreakAfter, style => new CssPageBreakAfterProperty(), animatable: false);
            AddLonghand(PropertyNames.PageBreakBefore, style => new CssPageBreakBeforeProperty(), animatable: false);
            AddLonghand(PropertyNames.PageBreakInside, style => new CssPageBreakInsideProperty(), animatable: false);
            AddLonghand(PropertyNames.Perspective, style => new CssPerspectiveProperty(), animatable: true);
            AddLonghand(PropertyNames.PerspectiveOrigin, style => new CssPerspectiveOriginProperty(), animatable: true);
            AddLonghand(PropertyNames.Position, style => new CssPositionProperty(), animatable: false);
            AddLonghand(PropertyNames.Quotes, style => new CssQuotesProperty(), animatable: false);
            AddLonghand(PropertyNames.Right, style => new CssRightProperty(), animatable: true);
            AddLonghand(PropertyNames.TableLayout, style => new CssTableLayoutProperty(), animatable: false);
            AddLonghand(PropertyNames.TextAlign, style => new CssTextAlignProperty(), animatable: false);

            AddShorthand(PropertyNames.TextDecoration, style => new CssTextDecorationProperty(style),
                PropertyNames.TextDecorationLine,
                PropertyNames.TextDecorationStyle,
                PropertyNames.TextDecorationColor);
            AddLonghand(PropertyNames.TextDecorationStyle, style => new CssTextDecorationStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.TextDecorationLine, style => new CssTextDecorationLineProperty(), animatable: false);
            AddLonghand(PropertyNames.TextDecorationColor, style => new CssTextDecorationColorProperty(), animatable: true);

            AddLonghand(PropertyNames.TextIndent, style => new CssTextIndentProperty(), animatable: true);
            AddLonghand(PropertyNames.TextTransform, style => new CssTextTransformProperty(), animatable: false);
            AddLonghand(PropertyNames.TextShadow, style => new CssTextShadowProperty(), animatable: true);
            AddLonghand(PropertyNames.Transform, style => new CssTransformProperty(), animatable: true);
            AddLonghand(PropertyNames.TransformOrigin, style => new CssTransformOriginProperty(), animatable: true);
            AddLonghand(PropertyNames.TransformStyle, style => new CssTransformStyleProperty(), animatable: false);

            AddShorthand(PropertyNames.Transition, style => new CssTransitionProperty(style),
                PropertyNames.TransitionProperty,
                PropertyNames.TransitionDuration,
                PropertyNames.TransitionTimingFunction,
                PropertyNames.TransitionDelay);
            AddLonghand(PropertyNames.TransitionDelay, style => new CssTransitionDelayProperty());
            AddLonghand(PropertyNames.TransitionDuration, style => new CssTransitionDurationProperty());
            AddLonghand(PropertyNames.TransitionTimingFunction, style => new CssTransitionTimingFunctionProperty());
            AddLonghand(PropertyNames.TransitionProperty, style => new CssTransitionPropertyProperty());

            AddLonghand(PropertyNames.Top, style => new CssTopProperty(), animatable: true);
            AddLonghand(PropertyNames.UnicodeBidi, style => new CssUnicodeBidiProperty(), animatable: false);
            AddLonghand(PropertyNames.VerticalAlign, style => new CssVerticalAlignProperty(), animatable: true);
            AddLonghand(PropertyNames.Visibility, style => new CssVisibilityProperty(), animatable: true);
            AddLonghand(PropertyNames.WhiteSpace, style => new CssWhiteSpaceProperty(), animatable: false);
            AddLonghand(PropertyNames.Widows, style => new CssWidowsProperty(), animatable: false);
            AddLonghand(PropertyNames.Width, style => new CssWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.WordSpacing, style => new CssWordSpacingProperty(), animatable: true);
            AddLonghand(PropertyNames.ZIndex, style => new CssZIndexProperty(), animatable: true);
            AddLonghand(PropertyNames.ObjectFit, style => new CssObjectFitProperty(), animatable: false);
            AddLonghand(PropertyNames.ObjectPosition, style => new CssObjectPositionProperty(), animatable: true);
        }

        void AddShorthand(String name, ShorthandCreator creator, params String[] longhands)
        {
            shorthands.Add(name, creator);
            mappings.Add(name, longhands);
        }

        void AddLonghand(String name, LonghandCreator creator, Boolean animatable = false)
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
        public CssProperty Create(String name, CssStyleDeclaration style)
        {
            return CreateLonghand(name, style) ?? CreateShorthand(name, style);
        }

        /// <summary>
        /// Creates a new longhand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="style">The given style set.</param>
        /// <returns>The created longhand property.</returns>
        public CssProperty CreateLonghand(String name, CssStyleDeclaration style)
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
        public CssShorthandProperty CreateShorthand(String name, CssStyleDeclaration style)
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
        public IEnumerable<CssProperty> CreateLonghandsFor(String name, CssStyleDeclaration style)
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
        public Boolean IsLonghand(String name)
        {
            return longhands.ContainsKey(name);
        }

        /// <summary>
        /// Checks if the given property name is a shorthand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property is a shorthand, otherwise false.</returns>
        public Boolean IsShorthand(String name)
        {
            return shorthands.ContainsKey(name);
        }

        /// <summary>
        /// Checks if the given property name is actually animatable.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>True if the property is animatable, or has a longhand that is animatable.</returns>
        public Boolean IsAnimatable(String name)
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
        public Boolean IsSupported(String name)
        {
            return IsLonghand(name) || IsShorthand(name);
        }

        /// <summary>
        /// Gets the longhands that map to the specified shorthand property.
        /// </summary>
        /// <param name="name">The name of the shorthand property.</param>
        /// <returns>An enumeration over all longhand properties.</returns>
        public IEnumerable<String> GetLonghands(String name)
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
        public IEnumerable<String> GetShorthands(String name)
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
