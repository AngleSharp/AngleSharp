namespace AngleSharp.Services.Default
{
	using AngleSharp.Css;
	using AngleSharp.Dom.Css;
	using AngleSharp.Extensions;
	using System;
	using System.Collections.Generic;

	/// <summary>
	/// Provides string to CSSProperty instance creation mappings.
	/// </summary>
	sealed class CssPropertyFactory : ICssPropertyFactory
    {
        #region Delegates

        delegate CssProperty LonghandCreator();
        delegate CssShorthandProperty ShorthandCreator();

        #endregion

        #region Fields

        readonly Dictionary<String, LonghandCreator> longhands = new Dictionary<String, LonghandCreator>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<String, ShorthandCreator> shorthands = new Dictionary<String, ShorthandCreator>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<String, LonghandCreator> fonts = new Dictionary<String, LonghandCreator>(StringComparer.OrdinalIgnoreCase);
        readonly Dictionary<String, String[]> mappings = new Dictionary<String, String[]>();
        readonly List<String> animatables = new List<String>();

        #endregion

        #region Initialization

        public CssPropertyFactory()
        {
            AddShorthand(PropertyNames.Animation, () => new CssAnimationProperty(),
                PropertyNames.AnimationName,
                PropertyNames.AnimationDuration,
                PropertyNames.AnimationTimingFunction,
                PropertyNames.AnimationDelay, 
                PropertyNames.AnimationDirection, 
                PropertyNames.AnimationFillMode, 
                PropertyNames.AnimationIterationCount, 
                PropertyNames.AnimationPlayState);
            AddLonghand(PropertyNames.AnimationDelay, () => new CssAnimationDelayProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationDirection, () => new CssAnimationDirectionProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationDuration, () => new CssAnimationDurationProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationFillMode, () => new CssAnimationFillModeProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationIterationCount, () => new CssAnimationIterationCountProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationName, () => new CssAnimationNameProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationPlayState, () => new CssAnimationPlayStateProperty(), animatable: false);
            AddLonghand(PropertyNames.AnimationTimingFunction, () => new CssAnimationTimingFunctionProperty(), animatable: false);

            AddShorthand(PropertyNames.Background, () => new CssBackgroundProperty(),
                PropertyNames.BackgroundAttachment,
                PropertyNames.BackgroundClip,
                PropertyNames.BackgroundColor,
                PropertyNames.BackgroundImage,
                PropertyNames.BackgroundOrigin,
                PropertyNames.BackgroundPosition,
                PropertyNames.BackgroundRepeat,
                PropertyNames.BackgroundSize);
            AddLonghand(PropertyNames.BackgroundAttachment, () => new CssBackgroundAttachmentProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundColor, () => new CssBackgroundColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BackgroundClip, () => new CssBackgroundClipProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundOrigin, () => new CssBackgroundOriginProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundSize, () => new CssBackgroundSizeProperty(), animatable: true);
            AddLonghand(PropertyNames.BackgroundImage, () => new CssBackgroundImageProperty(), animatable: false);
            AddLonghand(PropertyNames.BackgroundPosition, () => new CssBackgroundPositionProperty(), animatable: true);
            AddLonghand(PropertyNames.BackgroundRepeat, () => new CssBackgroundRepeatProperty(), animatable: false);

            AddLonghand(PropertyNames.BorderSpacing, () => new CssBorderSpacingProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderCollapse, () => new CssBorderCollapseProperty(), animatable: false);
            AddLonghand(PropertyNames.BoxShadow, () => new CssBoxShadowProperty(), animatable: true);
            AddLonghand(PropertyNames.BoxDecorationBreak, () => new CssBoxDecorationBreak(), animatable: false);
            AddLonghand(PropertyNames.BreakAfter, () => new CssBreakAfterProperty(), animatable: false);
            AddLonghand(PropertyNames.BreakBefore, () => new CssBreakBeforeProperty(), animatable: false);
            AddLonghand(PropertyNames.BreakInside, () => new CssBreakInsideProperty(), animatable: false);
            AddLonghand(PropertyNames.BackfaceVisibility, () => new CssBackfaceVisibilityProperty(), animatable: false);

            AddShorthand(PropertyNames.BorderRadius, () => new CssBorderRadiusProperty(),
                PropertyNames.BorderTopLeftRadius,
                PropertyNames.BorderTopRightRadius,
                PropertyNames.BorderBottomRightRadius,
                PropertyNames.BorderBottomLeftRadius);
            AddLonghand(PropertyNames.BorderTopLeftRadius, () => new CssBorderTopLeftRadiusProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderTopRightRadius, () => new CssBorderTopRightRadiusProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomLeftRadius, () => new CssBorderBottomLeftRadiusProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomRightRadius, () => new CssBorderBottomRightRadiusProperty(), animatable: true);

            AddShorthand(PropertyNames.BorderImage, () => new CssBorderImageProperty(),
                PropertyNames.BorderImageOutset,
                PropertyNames.BorderImageRepeat,
                PropertyNames.BorderImageSlice,
                PropertyNames.BorderImageSource,
                PropertyNames.BorderImageWidth);
            AddLonghand(PropertyNames.BorderImageOutset, () => new CssBorderImageOutsetProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageRepeat, () => new CssBorderImageRepeatProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageSource, () => new CssBorderImageSourceProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageSlice, () => new CssBorderImageSliceProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderImageWidth, () => new CssBorderImageWidthProperty(), animatable: false);

            AddShorthand(PropertyNames.BorderColor, () => new CssBorderColorProperty(),
                PropertyNames.BorderTopColor,
                PropertyNames.BorderRightColor,
                PropertyNames.BorderBottomColor,
                PropertyNames.BorderLeftColor);
            AddShorthand(PropertyNames.BorderStyle, () => new CssBorderStyleProperty(),
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderLeftStyle);
            AddShorthand(PropertyNames.BorderWidth, () => new CssBorderWidthProperty(),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderLeftWidth);
            AddShorthand(PropertyNames.BorderTop, () => new CssBorderTopProperty(),
                PropertyNames.BorderTopWidth,
                PropertyNames.BorderTopStyle,
                PropertyNames.BorderTopColor);
            AddShorthand(PropertyNames.BorderRight, () => new CssBorderRightProperty(),
                PropertyNames.BorderRightWidth,
                PropertyNames.BorderRightStyle,
                PropertyNames.BorderRightColor);
            AddShorthand(PropertyNames.BorderBottom, () => new CssBorderBottomProperty(),
                PropertyNames.BorderBottomWidth,
                PropertyNames.BorderBottomStyle,
                PropertyNames.BorderBottomColor);
            AddShorthand(PropertyNames.BorderLeft, () => new CssBorderLeftProperty(),
                PropertyNames.BorderLeftWidth,
                PropertyNames.BorderLeftStyle,
                PropertyNames.BorderLeftColor);

            AddShorthand(PropertyNames.Border, () => new CssBorderProperty(),
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
            AddLonghand(PropertyNames.BorderTopColor, () => new CssBorderTopColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderLeftColor, () => new CssBorderLeftColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderRightColor, () => new CssBorderRightColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomColor, () => new CssBorderBottomColorProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderTopStyle, () => new CssBorderTopStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderLeftStyle, () => new CssBorderLeftStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderRightStyle, () => new CssBorderRightStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderBottomStyle, () => new CssBorderBottomStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.BorderTopWidth, () => new CssBorderTopWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderLeftWidth, () => new CssBorderLeftWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderRightWidth, () => new CssBorderRightWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.BorderBottomWidth, () => new CssBorderBottomWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.Bottom, () => new CssBottomProperty(), animatable: true);

            AddShorthand(PropertyNames.Columns, () => new CssColumnsProperty(),
                PropertyNames.ColumnWidth,
                PropertyNames.ColumnCount);
            AddLonghand(PropertyNames.ColumnCount, () => new CssColumnCountProperty(), animatable: true);
            AddLonghand(PropertyNames.ColumnWidth, () => new CssColumnWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.ColumnFill, () => new CssColumnFillProperty(), animatable: false);
            AddLonghand(PropertyNames.ColumnGap, () => new CssColumnGapProperty(), animatable: true);
            AddLonghand(PropertyNames.ColumnSpan, () => new CssColumnSpanProperty(), animatable: false);

            AddShorthand(PropertyNames.ColumnRule, () => new CssColumnRuleProperty(),
                PropertyNames.ColumnRuleWidth,
                PropertyNames.ColumnRuleStyle,
                PropertyNames.ColumnRuleColor);
            AddLonghand(PropertyNames.ColumnRuleColor, () => new CssColumnRuleColorProperty(), animatable: true);
            AddLonghand(PropertyNames.ColumnRuleStyle, () => new CssColumnRuleStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.ColumnRuleWidth, () => new CssColumnRuleWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.CaptionSide, () => new CssCaptionSideProperty(), animatable: false);
            AddLonghand(PropertyNames.Clear, () => new CssClearProperty(), animatable: false);
            AddLonghand(PropertyNames.Clip, () => new CssClipProperty(), animatable: true);
            AddLonghand(PropertyNames.Color, () => new CssColorProperty(), animatable: true);
            AddLonghand(PropertyNames.Content, () => new CssContentProperty(), animatable: false);
            AddLonghand(PropertyNames.CounterIncrement, () => new CssCounterIncrementProperty());
            AddLonghand(PropertyNames.CounterReset, () => new CssCounterResetProperty(), animatable: false);
            AddLonghand(PropertyNames.Cursor, () => new CssCursorProperty(), animatable: false);
            AddLonghand(PropertyNames.Direction, () => new CssDirectionProperty(), animatable: false);
            AddLonghand(PropertyNames.Display, () => new CssDisplayProperty(), animatable: false);
            AddLonghand(PropertyNames.EmptyCells, () => new CssEmptyCellsProperty(), animatable: false);
            AddLonghand(PropertyNames.Float, () => new CssFloatProperty(), animatable: false);

            AddShorthand(PropertyNames.Font, () => new CssFontProperty(),
                PropertyNames.FontFamily,
                PropertyNames.FontSize,
                PropertyNames.FontStretch,
                PropertyNames.FontStyle,
                PropertyNames.FontVariant,
                PropertyNames.FontWeight,
                PropertyNames.LineHeight);
            AddLonghand(PropertyNames.FontFamily, () => new CssFontFamilyProperty(), animatable: false, font: true);
            AddLonghand(PropertyNames.FontSize, () => new CssFontSizeProperty(), animatable: true);
            AddLonghand(PropertyNames.FontSizeAdjust, () => new CssFontSizeAdjustProperty(), animatable: true);
            AddLonghand(PropertyNames.FontStyle, () => new CssFontStyleProperty(), animatable: false, font: true);
            AddLonghand(PropertyNames.FontVariant, () => new CssFontVariantProperty(), animatable: false, font: true);
            AddLonghand(PropertyNames.FontWeight, () => new CssFontWeightProperty(), animatable: true, font: true);
            AddLonghand(PropertyNames.FontStretch, () => new CssFontStretchProperty(), animatable: true, font: true);
            AddLonghand(PropertyNames.LineHeight, () => new CssLineHeightProperty(), animatable: true);

            AddLonghand(PropertyNames.Height, () => new CssHeightProperty(), animatable: true);
            AddLonghand(PropertyNames.Left, () => new CssLeftProperty(), animatable: true);
            AddLonghand(PropertyNames.LetterSpacing, () => new CssLetterSpacingProperty(), animatable: false);

            AddShorthand(PropertyNames.ListStyle, () => new CssListStyleProperty(),
                PropertyNames.ListStyleType,
                PropertyNames.ListStyleImage,
                PropertyNames.ListStylePosition);
            AddLonghand(PropertyNames.ListStyleImage, () => new CssListStyleImageProperty(), animatable: false);
            AddLonghand(PropertyNames.ListStylePosition, () => new CssListStylePositionProperty(), animatable: false);
            AddLonghand(PropertyNames.ListStyleType, () => new CssListStyleTypeProperty(), animatable: false);

            AddShorthand(PropertyNames.Margin, () => new CssMarginProperty(),
                PropertyNames.MarginTop,
                PropertyNames.MarginRight,
                PropertyNames.MarginBottom,
                PropertyNames.MarginLeft);
            AddLonghand(PropertyNames.MarginRight, () => new CssMarginRightProperty(), animatable: true);
            AddLonghand(PropertyNames.MarginLeft, () => new CssMarginLeftProperty(), animatable: true);
            AddLonghand(PropertyNames.MarginTop, () => new CssMarginTopProperty(), animatable: true);
            AddLonghand(PropertyNames.MarginBottom, () => new CssMarginBottomProperty(), animatable: true);

            AddLonghand(PropertyNames.MaxHeight, () => new CssMaxHeightProperty(), animatable: true);
            AddLonghand(PropertyNames.MaxWidth, () => new CssMaxWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.MinHeight, () => new CssMinHeightProperty(), animatable: true);
            AddLonghand(PropertyNames.MinWidth, () => new CssMinWidthProperty(), animatable: true);
            AddLonghand(PropertyNames.Opacity, () => new CssOpacityProperty(), animatable: true);
            AddLonghand(PropertyNames.Orphans, () => new CssOrphansProperty(), animatable: false);

            AddShorthand(PropertyNames.Outline, () => new CssOutlineProperty(),
                PropertyNames.OutlineWidth,
                PropertyNames.OutlineStyle,
                PropertyNames.OutlineColor);
            AddLonghand(PropertyNames.OutlineColor, () => new CssOutlineColorProperty(), animatable: true);
            AddLonghand(PropertyNames.OutlineStyle, () => new CssOutlineStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.OutlineWidth, () => new CssOutlineWidthProperty(), animatable: true);

            AddLonghand(PropertyNames.Overflow, () => new CssOverflowProperty(), animatable: false);
			AddLonghand(PropertyNames.OverflowWrap, () => new CssOverflowWrapProperty(), animatable: false);

			AddShorthand(PropertyNames.Padding, () => new CssPaddingProperty(),
                PropertyNames.PaddingTop,
                PropertyNames.PaddingRight,
                PropertyNames.PaddingBottom,
                PropertyNames.PaddingLeft);
            AddLonghand(PropertyNames.PaddingTop, () => new CssPaddingTopProperty(), animatable: true);
            AddLonghand(PropertyNames.PaddingRight, () => new CssPaddingRightProperty(), animatable: true);
            AddLonghand(PropertyNames.PaddingLeft, () => new CssPaddingLeftProperty(), animatable: true);
            AddLonghand(PropertyNames.PaddingBottom, () => new CssPaddingBottomProperty(), animatable: true);

            AddLonghand(PropertyNames.PageBreakAfter, () => new CssPageBreakAfterProperty(), animatable: false);
            AddLonghand(PropertyNames.PageBreakBefore, () => new CssPageBreakBeforeProperty(), animatable: false);
            AddLonghand(PropertyNames.PageBreakInside, () => new CssPageBreakInsideProperty(), animatable: false);
            AddLonghand(PropertyNames.Perspective, () => new CssPerspectiveProperty(), animatable: true);
            AddLonghand(PropertyNames.PerspectiveOrigin, () => new CssPerspectiveOriginProperty(), animatable: true);
            AddLonghand(PropertyNames.Position, () => new CssPositionProperty(), animatable: false);
            AddLonghand(PropertyNames.Quotes, () => new CssQuotesProperty(), animatable: false);
            AddLonghand(PropertyNames.Right, () => new CssRightProperty(), animatable: true);
			AddLonghand(PropertyNames.Stroke, () => new CssStrokeProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeDasharray, () => new CssStrokeDasharrayProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeDashoffset, () => new CssStrokeDashoffsetProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeLinecap, () => new CssStrokeLinecapProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeLinejoin, () => new CssStrokeLinejoinProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeMiterlimit, () => new CssStrokeMiterlimitProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeOpacity, () => new CssStrokeOpacityProperty(), animatable: true);
			AddLonghand(PropertyNames.StrokeWidth, () => new CssStrokeWidthProperty(), animatable: true);
			AddLonghand(PropertyNames.TableLayout, () => new CssTableLayoutProperty(), animatable: false);
            AddLonghand(PropertyNames.TextAlign, () => new CssTextAlignProperty(), animatable: false);
			AddLonghand(PropertyNames.TextAlignLast, () => new CssTextAlignLastProperty(), animatable: false);
			AddLonghand(PropertyNames.TextAnchor, () => new CssTextAnchorProperty(), animatable: false);

			AddShorthand(PropertyNames.TextDecoration, () => new CssTextDecorationProperty(),
                PropertyNames.TextDecorationLine,
                PropertyNames.TextDecorationStyle,
                PropertyNames.TextDecorationColor);
            AddLonghand(PropertyNames.TextDecorationStyle, () => new CssTextDecorationStyleProperty(), animatable: false);
            AddLonghand(PropertyNames.TextDecorationLine, () => new CssTextDecorationLineProperty(), animatable: false);
            AddLonghand(PropertyNames.TextDecorationColor, () => new CssTextDecorationColorProperty(), animatable: true);

            AddLonghand(PropertyNames.TextIndent, () => new CssTextIndentProperty(), animatable: true);
			AddLonghand(PropertyNames.TextJustify, () => new CssTextJustifyProperty(), animatable: false);
			AddLonghand(PropertyNames.TextTransform, () => new CssTextTransformProperty(), animatable: false);
            AddLonghand(PropertyNames.TextShadow, () => new CssTextShadowProperty(), animatable: true);
            AddLonghand(PropertyNames.Transform, () => new CssTransformProperty(), animatable: true);
            AddLonghand(PropertyNames.TransformOrigin, () => new CssTransformOriginProperty(), animatable: true);
            AddLonghand(PropertyNames.TransformStyle, () => new CssTransformStyleProperty(), animatable: false);

            AddShorthand(PropertyNames.Transition, () => new CssTransitionProperty(),
                PropertyNames.TransitionProperty,
                PropertyNames.TransitionDuration,
                PropertyNames.TransitionTimingFunction,
                PropertyNames.TransitionDelay);
            AddLonghand(PropertyNames.TransitionDelay, () => new CssTransitionDelayProperty());
            AddLonghand(PropertyNames.TransitionDuration, () => new CssTransitionDurationProperty());
            AddLonghand(PropertyNames.TransitionTimingFunction, () => new CssTransitionTimingFunctionProperty());
            AddLonghand(PropertyNames.TransitionProperty, () => new CssTransitionPropertyProperty());

            AddLonghand(PropertyNames.Top, () => new CssTopProperty(), animatable: true);
            AddLonghand(PropertyNames.UnicodeBidi, () => new CssUnicodeBidiProperty(), animatable: false);
            AddLonghand(PropertyNames.VerticalAlign, () => new CssVerticalAlignProperty(), animatable: true);
            AddLonghand(PropertyNames.Visibility, () => new CssVisibilityProperty(), animatable: true);
            AddLonghand(PropertyNames.WhiteSpace, () => new CssWhiteSpaceProperty(), animatable: false);
            AddLonghand(PropertyNames.Widows, () => new CssWidowsProperty(), animatable: false);
            AddLonghand(PropertyNames.Width, () => new CssWidthProperty(), animatable: true);
			AddLonghand(PropertyNames.WordBreak, () => new CssWordBreakProperty(), animatable: true);
			AddLonghand(PropertyNames.WordSpacing, () => new CssWordSpacingProperty(), animatable: true);
			AddLonghand(PropertyNames.WordWrap, () => new CssOverflowWrapProperty(), animatable: false);
			AddLonghand(PropertyNames.ZIndex, () => new CssZIndexProperty(), animatable: true);
            AddLonghand(PropertyNames.ObjectFit, () => new CssObjectFitProperty(), animatable: false);
            AddLonghand(PropertyNames.ObjectPosition, () => new CssObjectPositionProperty(), animatable: true);

            fonts.Add(PropertyNames.Src, () => new CssSrcProperty());
            fonts.Add(PropertyNames.UnicodeRange, () => new CssUnicodeRangeProperty());
        }

        void AddShorthand(String name, ShorthandCreator creator, params String[] longhands)
        {
            shorthands.Add(name, creator);
            mappings.Add(name, longhands);
        }

        void AddLonghand(String name, LonghandCreator creator, Boolean animatable = false, Boolean font = false)
        {
            longhands.Add(name, creator);

            if (animatable)
            {
                animatables.Add(name);
            }

            if (font)
            {
                fonts.Add(name, creator);
            }
        }

        #endregion

        #region Creation

        /// <summary>
        /// Creates a new property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created property.</returns>
        public CssProperty Create(String name)
        {
            return CreateLonghand(name) ?? CreateShorthand(name);
        }

        /// <summary>
        /// Creates a new property for @font-face.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created property.</returns>
        public CssProperty CreateFont(String name)
        {
            var propertyCreator = default(LonghandCreator);

            if (fonts.TryGetValue(name, out propertyCreator))
            {
                return propertyCreator();
            }

            return null;
        }

        /// <summary>
        /// Creates a new property for @viewport.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created property.</returns>
        public CssProperty CreateViewport(String name)
        {
            var feature = Factory.MediaFeatures.Create(name);
            return feature != null ? new CssFeatureProperty(feature) : null;
        }

        /// <summary>
        /// Creates a new longhand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created longhand property.</returns>
        public CssProperty CreateLonghand(String name)
        {
            var createProperty = default(LonghandCreator);

            if (longhands.TryGetValue(name, out createProperty))
            {
                return createProperty();
            }

            return null;
        }

        /// <summary>
        /// Creates a new shorthand property.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <returns>The created shorthand property.</returns>
        public CssShorthandProperty CreateShorthand(String name)
        {
            var propertyCreator = default(ShorthandCreator);

            if (shorthands.TryGetValue(name, out propertyCreator))
            {
                return propertyCreator();
            }

            return null;
        }

        /// <summary>
        /// Creates a series of longhand properties for the provided shorthand.
        /// </summary>
        /// <param name="name">The name of the corresponding shorthand property.</param>
        /// <returns>The created longhand properties.</returns>
        public CssProperty[] CreateLonghandsFor(String name)
        {
            var propertyNames = GetLonghands(name);
            var properties = new List<CssProperty>();

            foreach (var propertyName in propertyNames)
            {
                properties.Add(CreateLonghand(propertyName));
            }

            return properties.ToArray();
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
            {
                return animatables.Contains(name);
            }

            foreach (var longhand in GetLonghands(name))
            {
                if (animatables.Contains(name))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the longhands that map to the specified shorthand property.
        /// </summary>
        /// <param name="name">The name of the shorthand property.</param>
        /// <returns>An array with all longhand property names.</returns>
        public String[] GetLonghands(String name)
        {
            if (mappings.ContainsKey(name))
            {
                return mappings[name];
            }
            
            return new String[0];
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
                {
                    yield return mapping.Key;
                }
            }
        }

        #endregion
    }
}
