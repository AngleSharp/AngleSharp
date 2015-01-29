namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a CSS declaration block, including its underlying state, where
    /// this underlying state depends upon the source of the CSSStyleDeclaration instance.
    /// </summary>
    [DomName("CSSStyleDeclaration")]
    public interface ICssStyleDeclaration : IEnumerable<ICssProperty>
    {
        #region API

        /// <summary>
        /// Gets the name of the property with the specified index.
        /// </summary>
        /// <param name="index">The index of the property to retrieve.</param>
        /// <returns>The name of the property at the given index.</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        String this[Int32 index] { get; }

        /// <summary>
        /// Gets or sets the textual representation of the declaration block.
        /// </summary>
        [DomName("cssText")]
        String CssText { get; set; }

        /// <summary>
        /// Gets the number of properties in the declaration.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets the value of a property with the given name.
        /// </summary>
        /// <param name="propertyName">The name of the property to get the value of.</param>
        /// <returns>A string or null if nothing has been set.</returns>
        [DomName("getPropertyValue")]
        String GetPropertyValue(String propertyName);

        /// <summary>
        /// Returns the optional priority, "important" or null, if no priority has been set.
        /// </summary>
        /// <param name="propertyName">The name of the property to get the priority of.</param>
        /// <returns>A priority or null.</returns>
        [DomName("getPropertyPriority")]
        String GetPropertyPriority(String propertyName);

        /// <summary>
        /// Sets a property with the given name and value. Optionally the priority can be
        /// set to "important" or left empty.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <param name="priority">The optional priority.</param>
        [DomName("setProperty")]
        void SetProperty(String propertyName, String propertyValue, String priority = null);

        /// <summary>
        /// Removes the property with the given name and returns its value.
        /// </summary>
        /// <param name="propertyName">The name of the property to be removed.</param>
        /// <returns>The value of the deleted property.</returns>
        [DomName("removeProperty")]
        String RemoveProperty(String propertyName);

        /// <summary>
        /// Gets the containing rule.
        /// </summary>
        [DomName("parentRule")]
        ICssRule Parent { get; }

        #endregion

        #region CSS Property Values

        /// <summary>
        /// Gets or sets the accelerator value.
        /// </summary>
        [DomName("accelerator")]
        String Accelerator { get; set; }

        /// <summary>
        /// Gets or sets the align-content value.
        /// </summary>
        [DomName("alignContent")]
        String AlignContent { get; set; }

        /// <summary>
        /// Gets or sets the align-items value.
        /// </summary>
        [DomName("alignItems")]
        String AlignItems { get; set; }

        /// <summary>
        /// Gets or sets the alignment-baseline value.
        /// </summary>
        [DomName("alignmentBaseline")]
        String AlignmentBaseline { get; set; }

        /// <summary>
        /// Gets or sets the align-self value.
        /// </summary>
        [DomName("alignSelf")]
        String AlignSelf { get; set; }

        /// <summary>
        /// Gets or sets the animation value.
        /// </summary>
        [DomName("animation")]
        String Animation { get; set; }

        /// <summary>
        /// Gets or sets the animation-delay value.
        /// </summary>
        [DomName("animationDelay")]
        String AnimationDelay { get; set; }

        /// <summary>
        /// Gets or sets the animation-direction value.
        /// </summary>
        [DomName("animationDirection")]
        String AnimationDirection { get; set; }

        /// <summary>
        /// Gets or sets the animation-duration value.
        /// </summary>
        [DomName("animationDuration")]
        String AnimationDuration { get; set; }

        /// <summary>
        /// Gets or sets the animation-fill-mode value.
        /// </summary>
        [DomName("animationFillMode")]
        String AnimationFillMode { get; set; }

        /// <summary>
        /// Gets or sets the animation-iteration-count value.
        /// </summary>
        [DomName("animationIterationCount")]
        String AnimationIterationCount { get; set; }

        /// <summary>
        /// Gets or sets the animation-name value.
        /// </summary>
        [DomName("animationName")]
        String AnimationName { get; set; }

        /// <summary>
        /// Gets or sets the animation-play-state value.
        /// </summary>
        [DomName("animationPlayState")]
        String AnimationPlayState { get; set; }

        /// <summary>
        /// Gets or sets the animation-timing-function value.
        /// </summary>
        [DomName("animationTimingFunction")]
        String AnimationTimingFunction { get; set; }

        /// <summary>
        /// Gets or sets the backface-visibility value.
        /// </summary>
        [DomName("backfaceVisibility")]
        String BackfaceVisibility { get; set; }

        /// <summary>
        /// Gets or sets the background value.
        /// </summary>
        [DomName("background")]
        String Background { get; set; }

        /// <summary>
        /// Gets or sets the background-attachment value.
        /// </summary>
        [DomName("backgroundAttachment")]
        String BackgroundAttachment { get; set; }

        /// <summary>
        /// Gets or sets the background-clip value.
        /// </summary>
        [DomName("backgroundClip")]
        String BackgroundClip { get; set; }

        /// <summary>
        /// Gets or sets the background-color value.
        /// </summary>
        [DomName("backgroundColor")]
        String BackgroundColor { get; set; }

        /// <summary>
        /// Gets or sets the background-image value.
        /// </summary>
        [DomName("backgroundImage")]
        String BackgroundImage { get; set; }

        /// <summary>
        /// Gets or sets the background-origin value.
        /// </summary>
        [DomName("backgroundOrigin")]
        String BackgroundOrigin { get; set; }

        /// <summary>
        /// Gets or sets the background-position value.
        /// </summary>
        [DomName("backgroundPosition")]
        String BackgroundPosition { get; set; }

        /// <summary>
        /// Gets or sets the background-position-x value.
        /// </summary>
        [DomName("backgroundPositionX")]
        String BackgroundPositionX { get; set; }

        /// <summary>
        /// Gets or sets the background-position-y value.
        /// </summary>
        [DomName("backgroundPositionY")]
        String BackgroundPositionY { get; set; }

        /// <summary>
        /// Gets or sets the background-repeat value.
        /// </summary>
        [DomName("backgroundRepeat")]
        String BackgroundRepeat { get; set; }

        /// <summary>
        /// Gets or sets the background-size value.
        /// </summary>
        [DomName("backgroundSize")]
        String BackgroundSize { get; set; }

        /// <summary>
        /// Gets or sets the baseline-shift value.
        /// </summary>
        [DomName("baselineShift")]
        String BaselineShift { get; set; }

        /// <summary>
        /// Gets or sets the behavior value.
        /// </summary>
        [DomName("behavior")]
        String Behavior { get; set; }

        /// <summary>
        /// Gets or sets the border value.
        /// </summary>
        [DomName("border")]
        String Border { get; set; }

        /// <summary>
        /// Gets or sets the border-bottom value.
        /// </summary>
        [DomName("borderBottom")]
        String BorderBottom { get; set; }

        /// <summary>
        /// Gets or sets the border-bottom-color value.
        /// </summary>
        [DomName("borderBottomColor")]
        String BorderBottomColor { get; set; }

        /// <summary>
        /// Gets or sets the border-bottom-left-radius value.
        /// </summary>
        [DomName("borderBottomLeftRadius")]
        String BorderBottomLeftRadius { get; set; }

        /// <summary>
        /// Gets or sets the border-bottom-right-radius value.
        /// </summary>
        [DomName("borderBottomRightRadius")]
        String BorderBottomRightRadius { get; set; }

        /// <summary>
        /// Gets or sets the border-bottom-style value.
        /// </summary>
        [DomName("borderBottomStyle")]
        String BorderBottomStyle { get; set; }

        /// <summary>
        /// Gets or sets the border-bottom-width value.
        /// </summary>
        [DomName("borderBottomWidth")]
        String BorderBottomWidth { get; set; }

        /// <summary>
        /// Gets or sets the border-collapse value.
        /// </summary>
        [DomName("borderCollapse")]
        String BorderCollapse { get; set; }

        /// <summary>
        /// Gets or sets the border-color value.
        /// </summary>
        [DomName("borderColor")]
        String BorderColor { get; set; }

        /// <summary>
        /// Gets or sets the border-image value.
        /// </summary>
        [DomName("borderImage")]
        String BorderImage { get; set; }

        /// <summary>
        /// Gets or sets the border-image-outset value.
        /// </summary>
        [DomName("borderImageOutset")]
        String BorderImageOutset { get; set; }

        /// <summary>
        /// Gets or sets the border-image-repeat value.
        /// </summary>
        [DomName("borderImageRepeat")]
        String BorderImageRepeat { get; set; }

        /// <summary>
        /// Gets or sets the border-image-slice value.
        /// </summary>
        [DomName("borderImageSlice")]
        String BorderImageSlice { get; set; }

        /// <summary>
        /// Gets or sets the border-image-source value.
        /// </summary>
        [DomName("borderImageSource")]
        String BorderImageSource { get; set; }

        /// <summary>
        /// Gets or sets the border-image-width value.
        /// </summary>
        [DomName("borderImageWidth")]
        String BorderImageWidth { get; set; }

        /// <summary>
        /// Gets or sets the border-left value.
        /// </summary>
        [DomName("borderLeft")]
        String BorderLeft { get; set; }

        /// <summary>
        /// Gets or sets the border-left-color value.
        /// </summary>
        [DomName("borderLeftColor")]
        String BorderLeftColor { get; set; }

        /// <summary>
        /// Gets or sets the border-left-style value.
        /// </summary>
        [DomName("borderLeftStyle")]
        String BorderLeftStyle { get; set; }

        /// <summary>
        /// Gets or sets the border-left-width value.
        /// </summary>
        [DomName("borderLeftWidth")]
        String BorderLeftWidth { get; set; }

        /// <summary>
        /// Gets or sets the border-radius value.
        /// </summary>
        [DomName("borderRadius")]
        String BorderRadius { get; set; }

        /// <summary>
        /// Gets or sets the border-right value.
        /// </summary>
        [DomName("borderRight")]
        String BorderRight { get; set; }

        /// <summary>
        /// Gets or sets the border-right-color value.
        /// </summary>
        [DomName("borderRightColor")]
        String BorderRightColor { get; set; }

        /// <summary>
        /// Gets or sets the border-right-style value.
        /// </summary>
        [DomName("borderRightStyle")]
        String BorderRightStyle { get; set; }

        /// <summary>
        /// Gets or sets the border-right-width value.
        /// </summary>
        [DomName("borderRightWidth")]
        String BorderRightWidth { get; set; }

        /// <summary>
        /// Gets or sets the border-spacing value.
        /// </summary>
        [DomName("borderSpacing")]
        String BorderSpacing { get; set; }

        /// <summary>
        /// Gets or sets the border-style value.
        /// </summary>
        [DomName("borderStyle")]
        String BorderStyle { get; set; }

        /// <summary>
        /// Gets or sets the border-top value.
        /// </summary>
        [DomName("borderTop")]
        String BorderTop { get; set; }

        /// <summary>
        /// Gets or sets the border-top-color value.
        /// </summary>
        [DomName("borderTopColor")]
        String BorderTopColor { get; set; }

        /// <summary>
        /// Gets or sets the border-top-left-radius value.
        /// </summary>
        [DomName("borderTopLeftRadius")]
        String BorderTopLeftRadius { get; set; }

        /// <summary>
        /// Gets or sets the border-top-right-radius value.
        /// </summary>
        [DomName("borderTopRightRadius")]
        String BorderTopRightRadius { get; set; }

        /// <summary>
        /// Gets or sets the border-top-style value.
        /// </summary>
        [DomName("borderTopStyle")]
        String BorderTopStyle { get; set; }

        /// <summary>
        /// Gets or sets the border-top-width value.
        /// </summary>
        [DomName("borderTopWidth")]
        String BorderTopWidth { get; set; }

        /// <summary>
        /// Gets or sets the border-width value.
        /// </summary>
        [DomName("borderWidth")]
        String BorderWidth { get; set; }

        /// <summary>
        /// Gets or sets the box-shadow value.
        /// </summary>
        [DomName("boxShadow")]
        String BoxShadow { get; set; }

        /// <summary>
        /// Gets or sets the box-sizing value.
        /// </summary>
        [DomName("boxSizing")]
        String BoxSizing { get; set; }

        /// <summary>
        /// Gets or sets the break-after value.
        /// </summary>
        [DomName("breakAfter")]
        String BreakAfter { get; set; }

        /// <summary>
        /// Gets or sets the break-before value.
        /// </summary>
        [DomName("breakBefore")]
        String BreakBefore { get; set; }

        /// <summary>
        /// Gets or sets the break-inside value.
        /// </summary>
        [DomName("breakInside")]
        String BreakInside { get; set; }

        /// <summary>
        /// Gets or sets the caption-side value.
        /// </summary>
        [DomName("captionSide")]
        String CaptionSide { get; set; }

        /// <summary>
        /// Gets or sets the clear value.
        /// </summary>
        [DomName("clear")]
        String Clear { get; set; }

        /// <summary>
        /// Gets or sets the clip value.
        /// </summary>
        [DomName("clip")]
        String Clip { get; set; }

        /// <summary>
        /// Gets or sets the clip-bottom value.
        /// </summary>
        [DomName("clipBottom")]
        String ClipBottom { get; set; }

        /// <summary>
        /// Gets or sets the clip-left value.
        /// </summary>
        [DomName("clipLeft")]
        String ClipLeft { get; set; }

        /// <summary>
        /// Gets or sets the clip-path value.
        /// </summary>
        [DomName("clipPath")]
        String ClipPath { get; set; }

        /// <summary>
        /// Gets or sets the clip-right value.
        /// </summary>
        [DomName("clipRight")]
        String ClipRight { get; set; }

        /// <summary>
        /// Gets or sets the clip-rule value.
        /// </summary>
        [DomName("clipRule")]
        String ClipRule { get; set; }

        /// <summary>
        /// Gets or sets the clip-top value.
        /// </summary>
        [DomName("clipTop")]
        String ClipTop { get; set; }

        /// <summary>
        /// Gets or sets the color value.
        /// </summary>
        [DomName("color")]
        String Color { get; set; }

        /// <summary>
        /// Gets or sets the color-interpolation-filters value.
        /// </summary>
        [DomName("colorInterpolationFilters")]
        String ColorInterpolationFilters { get; set; }

        /// <summary>
        /// Gets or sets the column-count value.
        /// </summary>
        [DomName("columnCount")]
        String ColumnCount { get; set; }

        /// <summary>
        /// Gets or sets the column-fill value.
        /// </summary>
        [DomName("columnFill")]
        String ColumnFill { get; set; }

        /// <summary>
        /// Gets or sets the column-gap value.
        /// </summary>
        [DomName("columnGap")]
        String ColumnGap { get; set; }

        /// <summary>
        /// Gets or sets the column-rule value.
        /// </summary>
        [DomName("columnRule")]
        String ColumnRule { get; set; }

        /// <summary>
        /// Gets or sets the column-rule-color value.
        /// </summary>
        [DomName("columnRuleColor")]
        String ColumnRuleColor { get; set; }

        /// <summary>
        /// Gets or sets the column-rule-style value.
        /// </summary>
        [DomName("columnRuleStyle")]
        String ColumnRuleStyle { get; set; }

        /// <summary>
        /// Gets or sets the column-rule-width value.
        /// </summary>
        [DomName("columnRuleWidth")]
        String ColumnRuleWidth { get; set; }

        /// <summary>
        /// Gets or sets the columnsv
        /// </summary>
        [DomName("columns")]
        String Columns { get; set; }

        /// <summary>
        /// Gets or sets the column-span value.
        /// </summary>
        [DomName("columnSpan")]
        String ColumnSpan { get; set; }

        /// <summary>
        /// Gets or sets the column-width value.
        /// </summary>
        [DomName("columnWidth")]
        String ColumnWidth { get; set; }

        /// <summary>
        /// Gets or sets the content value.
        /// </summary>
        [DomName("content")]
        String Content { get; set; }

        /// <summary>
        /// Gets or sets the counter-increment value.
        /// </summary>
        [DomName("counterIncrement")]
        String CounterIncrement { get; set; }
        /// <summary>
        /// Gets or sets the counter-reset value.
        /// </summary>
        [DomName("counterReset")]
        String CounterReset { get; set; }

        /// <summary>
        /// Gets or sets the cursor value.
        /// </summary>
        [DomName("cursor")]
        String Cursor { get; set; }

        /// <summary>
        /// Gets or sets the direction value.
        /// </summary>
        [DomName("direction")]
        String Direction { get; set; }

        /// <summary>
        /// Gets or sets the display value.
        /// </summary>
        [DomName("display")]
        String Display { get; set; }

        /// <summary>
        /// Gets or sets the dominant-baseline value.
        /// </summary>
        [DomName("dominantBaseline")]
        String DominantBaseline { get; set; }

        /// <summary>
        /// Gets or sets the empty-cells value.
        /// </summary>
        [DomName("emptyCells")]
        String EmptyCells { get; set; }

        /// <summary>
        /// Gets or sets the enable-background value.
        /// </summary>
        [DomName("enableBackground")]
        String EnableBackground { get; set; }

        /// <summary>
        /// Gets or sets the fill value.
        /// </summary>
        [DomName("fill")]
        String Fill { get; set; }

        /// <summary>
        /// Gets or sets the fill-opacity value.
        /// </summary>
        [DomName("fillOpacity")]
        String FillOpacity { get; set; }

        /// <summary>
        /// Gets or sets the fill-rule value.
        /// </summary>
        [DomName("fillRule")]
        String FillRule { get; set; }

        /// <summary>
        /// Gets or sets the filter value.
        /// </summary>
        [DomName("filter")]
        String Filter { get; set; }

        /// <summary>
        /// Gets or sets the flex value.
        /// </summary>
        [DomName("flex")]
        String Flex { get; set; }

        /// <summary>
        /// Gets or sets the flex-basis value.
        /// </summary>
        [DomName("flexBasis")]
        String FlexBasis { get; set; }

        /// <summary>
        /// Gets or sets the flex-direction value.
        /// </summary>
        [DomName("flexDirection")]
        String FlexDirection { get; set; }

        /// <summary>
        /// Gets or sets the flex-flow value.
        /// </summary>
        [DomName("flexFlow")]
        String FlexFlow { get; set; }

        /// <summary>
        /// Gets or sets the flex-grow value.
        /// </summary>
        [DomName("flexGrow")]
        String FlexGrow { get; set; }

        /// <summary>
        /// Gets or sets the flex-shrink value. 
        /// </summary>
        [DomName("flexShrink")]
        String FlexShrink { get; set; }

        /// <summary>
        /// Gets or sets the flex-wrap value.
        /// </summary>
        [DomName("flexWrap")]
        String FlexWrap { get; set; }

        /// <summary>
        /// Gets or sets the float value.
        /// </summary>
        [DomName("cssFloat")]
        String Float { get; set; }

        /// <summary>
        /// Gets or sets the font value.
        /// </summary>
        [DomName("font")]
        String Font { get; set; }

        /// <summary>
        /// Gets or sets the font-family value.
        /// </summary>
        [DomName("fontFamily")]
        String FontFamily { get; set; }

        /// <summary>
        /// Gets or sets the font-feature-settings value.
        /// </summary>
        [DomName("fontFeatureSettings")]
        String FontFeatureSettings { get; set; }

        /// <summary>
        /// Gets or sets the font-size value.
        /// </summary>
        [DomName("fontSize")]
        String FontSize { get; set; }

        /// <summary>
        /// Gets or sets the font-size-adjust value.
        /// </summary>
        [DomName("fontSizeAdjust")]
        String FontSizeAdjust { get; set; }

        /// <summary>
        /// Gets or sets the font-stretch value.
        /// </summary>
        [DomName("fontStretch")]
        String FontStretch { get; set; }

        /// <summary>
        /// Gets or sets the font-style value.
        /// </summary>
        [DomName("fontStyle")]
        String FontStyle { get; set; }

        /// <summary>
        /// Gets or sets the font-variant value.
        /// </summary>
        [DomName("fontVariant")]
        String FontVariant { get; set; }

        /// <summary>
        /// Gets or sets the font-weight value.
        /// </summary>
        [DomName("fontWeight")]
        String FontWeight { get; set; }

        /// <summary>
        /// Gets or sets the glyph-orientation-horizontal value.
        /// </summary>
        [DomName("glyphOrientationHorizontal")]
        String GlyphOrientationHorizontal { get; set; }

        /// <summary>
        /// Gets or sets the glyph-orientation-vertical value.
        /// </summary>
        [DomName("glyphOrientationVertical")]
        String GlyphOrientationVertical { get; set; }

        /// <summary>
        /// Gets or sets the height value.
        /// </summary>
        [DomName("height")]
        String Height { get; set; }

        /// <summary>
        /// Gets or sets the ime-mode value.
        /// </summary>
        [DomName("imeMode")]
        String ImeMode { get; set; }

        /// <summary>
        /// Gets or sets the justify-content value.
        /// </summary>
        [DomName("justifyContent")]
        String JustifyContent { get; set; }

        /// <summary>
        /// Gets or sets the layout-grid value.
        /// </summary>
        [DomName("layoutGrid")]
        String LayoutGrid { get; set; }

        /// <summary>
        /// Gets or sets the layout-grid-char value.
        /// </summary>
        [DomName("layoutGridChar")]
        String LayoutGridChar { get; set; }

        /// <summary>
        /// Gets or sets the layout-grid-line value.
        /// </summary>
        [DomName("layoutGridLine")]
        String LayoutGridLine { get; set; }

        /// <summary>
        /// Gets or sets the layout-grid-mode value.
        /// </summary>
        [DomName("layoutGridMode")]
        String LayoutGridMode { get; set; }

        /// <summary>
        /// Gets or sets the layout-grid-type value.
        /// </summary>
        [DomName("layoutGridType")]
        String LayoutGridType { get; set; }

        /// <summary>
        /// Gets or sets the left value.
        /// </summary>
        [DomName("left")]
        String Left { get; set; }

        /// <summary>
        /// Gets or sets the letter-spacing value.
        /// </summary>
        [DomName("letterSpacing")]
        String LetterSpacing { get; set; }

        /// <summary>
        /// Gets or sets the line-height value.
        /// </summary>
        [DomName("lineHeight")]
        String LineHeight { get; set; }

        /// <summary>
        /// Gets or sets the list-style value.
        /// </summary>
        [DomName("listStyle")]
        String ListStyle { get; set; }

        /// <summary>
        /// Gets or sets the list-style-image value.
        /// </summary>
        [DomName("listStyleImage")]
        String ListStyleImage { get; set; }

        /// <summary>
        /// Gets or sets the list-style-position value.
        /// </summary>
        [DomName("listStylePosition")]
        String ListStylePosition { get; set; }

        /// <summary>
        /// Gets or sets the list-style-type value.
        /// </summary>
        [DomName("listStyleType")]
        String ListStyleType { get; set; }

        /// <summary>
        /// Gets or sets the margin value.
        /// </summary>
        [DomName("margin")]
        String Margin { get; set; }

        /// <summary>
        /// Gets or sets the margin-bottom value.
        /// </summary>
        [DomName("marginBottom")]
        String MarginBottom { get; set; }

        /// <summary>
        /// Gets or sets the margin-left value.
        /// </summary>
        [DomName("marginLeft")]
        String MarginLeft { get; set; }

        /// <summary>
        /// Gets or sets the margin-right value.
        /// </summary>
        [DomName("marginRight")]
        String MarginRight { get; set; }

        /// <summary>
        /// Gets or sets the margin-top value.
        /// </summary>
        [DomName("marginTop")]
        String MarginTop { get; set; }

        /// <summary>
        /// Gets or sets the marker value.
        /// </summary>
        [DomName("marker")]
        String Marker { get; set; }

        /// <summary>
        /// Gets or sets the marker-end value.
        /// </summary>
        [DomName("markerEnd")]
        String MarkerEnd { get; set; }

        /// <summary>
        /// Gets or sets the marker-mid value.
        /// </summary>
        [DomName("markerMid")]
        String MarkerMid { get; set; }

        /// <summary>
        /// Gets or sets the marker-start value.
        /// </summary>
        [DomName("markerStart")]
        String MarkerStart { get; set; }

        /// <summary>
        /// Gets or sets the mask value.
        /// </summary>
        [DomName("mask")]
        String Mask { get; set; }

        /// <summary>
        /// Gets or sets the max-height value.
        /// </summary>
        [DomName("maxHeight")]
        String MaxHeight { get; set; }

        /// <summary>
        /// Gets or sets the max-width value.
        /// </summary>
        [DomName("maxWidth")]
        String MaxWidth { get; set; }

        /// <summary>
        /// Gets or sets the min-height value.
        /// </summary>
        [DomName("minHeight")]
        String MinHeight { get; set; }

        /// <summary>
        /// Gets or sets the min-width value.
        /// </summary>
        [DomName("minWidth")]
        String MinWidth { get; set; }

        /// <summary>
        /// Gets or sets the opacity value.
        /// </summary>
        [DomName("opacity")]
        String Opacity { get; set; }

        /// <summary>
        /// Gets or sets the order value.
        /// </summary>
        [DomName("order")]
        String Order { get; set; }

        /// <summary>
        /// Gets or sets the orphans value.
        /// </summary>
        [DomName("orphans")]
        String Orphans { get; set; }

        /// <summary>
        /// Gets or sets the outline value.
        /// </summary>
        [DomName("outline")]
        String Outline { get; set; }

        /// <summary>
        /// Gets or sets the outline-color value.
        /// </summary>
        [DomName("outlineColor")]
        String OutlineColor { get; set; }

        /// <summary>
        /// Gets or sets the outline-style value.
        /// </summary>
        [DomName("outlineStyle")]
        String OutlineStyle { get; set; }

        /// <summary>
        /// Gets or sets the outline-width value.
        /// </summary>
        [DomName("outlineWidth")]
        String OutlineWidth { get; set; }

        /// <summary>
        /// Gets or sets the overflow value.
        /// </summary>
        [DomName("overflow")]
        String Overflow { get; set; }

        /// <summary>
        /// Gets or sets the overflow-x value.
        /// </summary>
        [DomName("overflowX")]
        String OverflowX { get; set; }

        /// <summary>
        /// Gets or sets the overflow-y value.
        /// </summary>
        [DomName("overflowY")]
        String OverflowY { get; set; }

        /// <summary>
        /// Gets or sets the padding value.
        /// </summary>
        [DomName("padding")]
        String Padding { get; set; }

        /// <summary>
        /// Gets or sets the padding-bottom value.
        /// </summary>
        [DomName("paddingBottom")]
        String PaddingBottom { get; set; }

        /// <summary>
        /// Gets or sets the padding-left value.
        /// </summary>
        [DomName("paddingLeft")]
        String PaddingLeft { get; set; }

        /// <summary>
        /// Gets or sets the padding-right value.
        /// </summary>
        [DomName("paddingRight")]
        String PaddingRight { get; set; }

        /// <summary>
        /// Gets or sets the padding-top value.
        /// </summary>
        [DomName("paddingTop")]
        String PaddingTop { get; set; }

        /// <summary>
        /// Gets or sets the page-break-after value.
        /// </summary>
        [DomName("pageBreakAfter")]
        String PageBreakAfter { get; set; }

        /// <summary>
        /// Gets or sets the page-break-before value.
        /// </summary>
        [DomName("pageBreakBefore")]
        String PageBreakBefore { get; set; }

        /// <summary>
        /// Gets or sets the page-break-inside value.
        /// </summary>
        [DomName("pageBreakInside")]
        String PageBreakInside { get; set; }

        /// <summary>
        /// Gets or sets the perspective value.
        /// </summary>
        [DomName("perspective")]
        String Perspective { get; set; }

        /// <summary>
        /// Gets or sets the perspective-origin value.
        /// </summary>
        [DomName("perspectiveOrigin")]
        String PerspectiveOrigin { get; set; }

        /// <summary>
        /// Gets or sets the pointer-events value.
        /// </summary>
        [DomName("pointerEvents")]
        String PointerEvents { get; set; }

        /// <summary>
        /// Gets or sets the position value.
        /// </summary>
        [DomName("position")]
        String Position { get; set; }

        /// <summary>
        /// Gets or sets the quotes value.
        /// </summary>
        [DomName("quotes")]
        String Quotes { get; set; }

        /// <summary>
        /// Gets or sets the right value.
        /// </summary>
        [DomName("right")]
        String Right { get; set; }

        /// <summary>
        /// Gets or sets the ruby-align value.
        /// </summary>
        [DomName("rubyAlign")]
        String RubyAlign { get; set; }

        /// <summary>
        /// Gets or sets the ruby-overhang value.
        /// </summary>
        [DomName("rubyOverhang")]
        String RubyOverhang { get; set; }

        /// <summary>
        /// Gets or sets the ruby-position value.
        /// </summary>
        [DomName("rubyPosition")]
        String RubyPosition { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar3d-light-color value.
        /// </summary>
        [DomName("scrollbar3dLightColor")]
        String Scrollbar3dLightColor { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar-arrow-color value.
        /// </summary>
        [DomName("scrollbarArrowColor")]
        String ScrollbarArrowColor { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar-dark-shadow-color value.
        /// </summary>
        [DomName("scrollbarDarkShadowColor")]
        String ScrollbarDarkShadowColor { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar-face-color value.
        /// </summary>
        [DomName("scrollbarFaceColor")]
        String ScrollbarFaceColor { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar-highlight-color value.
        /// </summary>
        [DomName("scrollbarHighlightColor")]
        String ScrollbarHighlightColor { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar-shadow-color value.
        /// </summary>
        [DomName("scrollbarShadowColor")]
        String ScrollbarShadowColor { get; set; }

        /// <summary>
        /// Gets or sets the scrollbar-track-color value.
        /// </summary>
        [DomName("scrollbarTrackColor")]
        String ScrollbarTrackColor { get; set; }

        /// <summary>
        /// Gets or sets the stroke value.
        /// </summary>
        [DomName("stroke")]
        String Stroke { get; set; }

        /// <summary>
        /// Gets or sets the stroke-dasharray value.
        /// </summary>
        [DomName("strokeDasharray")]
        String StrokeDasharray { get; set; }

        /// <summary>
        /// Gets or sets the stroke-dashoffset value.
        /// </summary>
        [DomName("strokeDashoffset")]
        String StrokeDashoffset { get; set; }

        /// <summary>
        /// Gets or sets the stroke-linecap value.
        /// </summary>
        [DomName("strokeLinecap")]
        String StrokeLinecap { get; set; }

        /// <summary>
        /// Gets or sets the stroke-line-join value.
        /// </summary>
        [DomName("strokeLinejoin")]
        String StrokeLinejoin { get; set; }

        /// <summary>
        /// Gets or sets the stroke-miterlimit value.
        /// </summary>
        [DomName("strokeMiterlimit")]
        String StrokeMiterlimit { get; set; }

        /// <summary>
        /// Gets or sets the stroke-opacity value.
        /// </summary>
        [DomName("strokeOpacity")]
        String StrokeOpacity { get; set; }

        /// <summary>
        /// Gets or sets the stroke-width value.
        /// </summary>
        [DomName("strokeWidth")]
        String StrokeWidth { get; set; }
        /// <summary>
        /// Gets or sets the table-layout value.
        /// </summary>
        [DomName("tableLayout")]
        String TableLayout { get; set; }

        /// <summary>
        /// Gets or sets the text-align value.
        /// </summary>
        [DomName("textAlign")]
        String TextAlign { get; set; }

        /// <summary>
        /// Gets or sets the text-align-last value.
        /// </summary>
        [DomName("textAlignLast")]
        String TextAlignLast { get; set; }

        /// <summary>
        /// Gets or sets the text-anchor value.
        /// </summary>
        [DomName("textAnchor")]
        String TextAnchor { get; set; }

        /// <summary>
        /// Gets or sets the text-autospace value.
        /// </summary>
        [DomName("textAutospace")]
        String TextAutospace { get; set; }

        /// <summary>
        /// Gets or sets the text-decoration value.
        /// </summary>
        [DomName("textDecoration")]
        String TextDecoration { get; set; }

        /// <summary>
        /// Gets or sets the text-indent value.
        /// </summary>
        [DomName("textIndent")]
        String TextIndent { get; set; }

        /// <summary>
        /// Gets or sets the text-justify value.
        /// </summary>
        [DomName("textJustify")]
        String TextJustify { get; set; }

        /// <summary>
        /// Gets or sets the text-overflow value.
        /// </summary>
        [DomName("textOverflow")]
        String TextOverflow { get; set; }

        /// <summary>
        /// Gets or sets the text-shadow value.
        /// </summary>
        [DomName("textShadow")]
        String TextShadow { get; set; }

        /// <summary>
        /// Gets or sets the text-transform value.
        /// </summary>
        [DomName("textTransform")]
        String TextTransform { get; set; }

        /// <summary>
        /// Gets or sets the text-underline-position value.
        /// </summary>
        [DomName("textUnderlinePosition")]
        String TextUnderlinePosition { get; set; }

        /// <summary>
        /// Gets or sets the top value.
        /// </summary>
        [DomName("top")]
        String Top { get; set; }

        /// <summary>
        /// Gets or sets the transform value.
        /// </summary>
        [DomName("transform")]
        String Transform { get; set; }

        /// <summary>
        /// Gets or sets the transform-origin value.
        /// </summary>
        [DomName("transformOrigin")]
        String TransformOrigin { get; set; }

        /// <summary>
        /// Gets or sets the transform-style value.
        /// </summary>
        [DomName("transformStyle")]
        String TransformStyle { get; set; }

        /// <summary>
        /// Gets or sets the  value.
        /// </summary>
        [DomName("transition")]
        String Transition { get; set; }

        /// <summary>
        /// Gets or sets the transition-delay value.
        /// </summary>
        [DomName("transitionDelay")]
        String TransitionDelay { get; set; }

        /// <summary>
        /// Gets or sets the transition-duration value.
        /// </summary>
        [DomName("transitionDuration")]
        String TransitionDuration { get; set; }

        /// <summary>
        /// Gets or sets the transition-property value.
        /// </summary>
        [DomName("transitionProperty")]
        String TransitionProperty { get; set; }

        /// <summary>
        /// Gets or sets the transition-timing-function value.
        /// </summary>
        [DomName("transitionTimingFunction")]
        String TransitionTimingFunction { get; set; }

        /// <summary>
        /// Gets or sets the unicode-bidi value.
        /// </summary>
        [DomName("unicodeBidi")]
        String UnicodeBidi { get; set; }

        /// <summary>
        /// Gets or sets the vertical-align value.
        /// </summary>
        [DomName("verticalAlign")]
        String VerticalAlign { get; set; }

        /// <summary>
        /// Gets or sets the visibility value.
        /// </summary>
        [DomName("visibility")]
        String Visibility { get; set; }

        /// <summary>
        /// Gets or sets the white-space value.
        /// </summary>
        [DomName("whiteSpace")]
        String WhiteSpace { get; set; }

        /// <summary>
        /// Gets or sets the widows value.
        /// </summary>
        [DomName("widows")]
        String Widows { get; set; }

        /// <summary>
        /// Gets or sets the width value.
        /// </summary>
        [DomName("width")]
        String Width { get; set; }

        /// <summary>
        /// Gets or sets the word-break value.
        /// </summary>
        [DomName("wordBreak")]
        String WordBreak { get; set; }

        /// <summary>
        /// Gets or sets the word-spacing value.
        /// </summary>
        [DomName("wordSpacing")]
        String WordSpacing { get; set; }

        /// <summary>
        /// Gets or sets the word-wrap value.
        /// </summary>
        [DomName("wordWrap")]
        String WordWrap { get; set; }

        /// <summary>
        /// Gets or sets the writing-mode value.
        /// </summary>
        [DomName("writingMode")]
        String WritingMode { get; set; }

        /// <summary>
        /// Gets or sets the z-index value.
        /// </summary>
        [DomName("zIndex")]
        String ZIndex { get; set; }

        /// <summary>
        /// Gets or sets the zoom value.
        /// </summary>
        [DomName("zoom")]
        String Zoom { get; set; }

        #endregion
    }
}
