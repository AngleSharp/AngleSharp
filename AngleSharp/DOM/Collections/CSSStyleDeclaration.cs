namespace AngleSharp.DOM.Collections
{
    using AngleSharp.DOM.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    [DOM("CSSStyleDeclaration")]
    public sealed class CSSStyleDeclaration : IEnumerable<CSSProperty>
    {
        #region Fields

        Dictionary<String, CSSProperty> _rules;
        CSSRule _parent;
        Func<String> _getter;
        Action<String> _setter;
        Boolean _blocking;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS style declaration. Here a local string
        /// variable is used to cache the text representation.
        /// </summary>
        internal CSSStyleDeclaration()
        {
            var text = String.Empty;
            _getter = () => text;
            _setter = value => text = value;
            _rules = new Dictionary<String, CSSProperty>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Creates a new CSS style declaration with pre-defined getter
        /// and setter functions (use-case: HTML element).
        /// </summary>
        /// <param name="host">The element to host this representation.</param>
        internal CSSStyleDeclaration(Element host)
        {
            _getter = () => (host.OwnerDocument == null || host.OwnerDocument.Options.IsStyling) ? host.GetAttribute(AttributeNames.Style) : String.Empty;
            _setter = value => host.SetAttribute(AttributeNames.Style, value);
            _rules = new Dictionary<String, CSSProperty>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region General Properties

        /// <summary>
        /// Gets or sets the textual representation of the declaration block.
        /// </summary>
        [DOM("cssText")]
        public String CssText
        {
            get { return _getter(); }
            set
            {
                Update(value);
                _setter(value);
            }
        }

        /// <summary>
        /// Gets the number of properties in the declaration.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return _rules.Count; }
        }

        /// <summary>
        /// Gets the containing CSSRule.
        /// </summary>
        [DOM("parentRule")]
        public CSSRule ParentRule
        {
            get { return _parent; }
            internal set { _parent = value; }
        }

        /// <summary>
        /// Returns a property name.
        /// </summary>
        /// <param name="index">The index of the property to retrieve.</param>
        /// <returns>The name of the property at the given index.</returns>
        [DOM("item")]
        public String this[Int32 index]
        {
            get { return Get(index).Name; }
        }

        #endregion

        #region CSS Properties

        /// <summary>
        /// Gets or sets how a flex item's lines align within the flex container when there
        /// is extra space along the axis that is perpendicular to the axis defined by the
        /// flex-direction property.
        /// </summary>
        [DOM("alignContent")]
        public String AlignContent
        {
            get { return GetPropertyValue(PropertyNames.AlignContent) ?? String.Empty; }
            set { SetProperty(PropertyNames.AlignContent, value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex container.
        /// </summary>
        [DOM("alignItems")]
        public String AlignItems
        {
            get { return GetPropertyValue(PropertyNames.AlignItems) ?? String.Empty; }
            set { SetProperty(PropertyNames.AlignItems, value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout
        /// axis defined by the flex-direction property) of flex items of
        /// the flex container.
        /// </summary>
        [DOM("alignSelf")]
        public String AlignSelf
        {
            get { return GetPropertyValue(PropertyNames.AlignSelf) ?? String.Empty; }
            set { SetProperty(PropertyNames.AlignSelf, value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the object represents a
        /// keyboard shortcut.
        /// </summary>
        [DOM("accelerator")]
        public String Accelerator
        {
            get { return GetPropertyValue(PropertyNames.Accelerator) ?? String.Empty; }
            set { SetProperty(PropertyNames.Accelerator, value); }
        }

        /// <summary>
        /// Gets or sets which baseline of this element is to be aligned
        /// with the corresponding baseline of the parent.
        /// </summary>
        [DOM("alignmentBaseline")]
        public String AlignmentBaseline
        {
            get { return GetPropertyValue(PropertyNames.AlignBaseline) ?? String.Empty; }
            set { SetProperty(PropertyNames.AlignBaseline, value); }
        }

        /// <summary>
        /// Gets or sets one or more shorthand values that define all animation
        /// properties (except animation-play-state) for a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by the
        /// animations-name property.
        /// </summary>
        [DOM("animation")]
        public String Animation
        {
            get { return GetPropertyValue(PropertyNames.Animation) ?? String.Empty; }
            set { SetProperty(PropertyNames.Animation, value); }
        }

        /// <summary>
        /// Gets or sets the offset within an animation cycle
        /// (the amount of time from the start of a cycle) before
        /// the animation is displayed for a set of corresponding
        /// object properties identified in the CSS @keyframes at-rule
        /// specified by the animation-name property.
        /// </summary>
        [DOM("animationDelay")]
        public String AnimationDelay
        {
            get { return GetPropertyValue(PropertyNames.AnimationDelay) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationDelay, value); }
        }

        /// <summary>
        /// Gets or sets the direction of play for an animation cycle.
        /// </summary>
        [DOM("animationDirection")]
        public String AnimationDirection
        {
            get { return GetPropertyValue(PropertyNames.AnimationDirection) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationDirection, value); }
        }

        /// <summary>
        /// Gets or sets the length of time to complete one cycle of the animation.
        /// </summary>
        [DOM("animationDuration")]
        public String AnimationDuration
        {
            get { return GetPropertyValue(PropertyNames.AnimationDuration) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationDuration, value); }
        }

        /// <summary>
        /// Gets or sets whether the effects of an animation are visible before or after it plays.
        /// </summary>
        [DOM("animationFillMode")]
        public String AnimationFillMode
        {
            get { return GetPropertyValue(PropertyNames.AnimationFillMode) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationFillMode, value); }
        }

        /// <summary>
        /// Gets or sets the number of times an animation cycle is played.
        /// </summary>
        [DOM("animationIterationCount")]
        public String AnimationIterationCount
        {
            get { return GetPropertyValue(PropertyNames.AnimationIterationCount) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationIterationCount, value); }
        }

        /// <summary>
        /// Gets or sets one or more animation names. An animation name
        /// selects a CSS @keyframes at-rule.
        /// </summary>
        [DOM("animationName")]
        public String AnimationName
        {
            get { return GetPropertyValue(PropertyNames.AnimationName) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationName, value); }
        }

        /// <summary>
        /// Gets or sets whether an animation is playing or paused.
        /// </summary>
        [DOM("animationPlayState")]
        public String AnimationPlayState
        {
            get { return GetPropertyValue(PropertyNames.AnimationPlayState) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationPlayState, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate property values to be used during a
        /// single cycle of an animation on a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by
        /// the animation-name property.
        /// </summary>
        [DOM("animationTimingFunction")]
        public String AnimationTimingFunction
        {
            get { return GetPropertyValue(PropertyNames.AnimationTimingFunction) ?? String.Empty; }
            set { SetProperty(PropertyNames.AnimationTimingFunction, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the back face
        /// (reverse side) of an object is visible.
        /// </summary>
        [DOM("backfaceVisibility")]
        public String BackfaceVisibility
        {
            get { return GetPropertyValue(PropertyNames.BackfaceVisibility) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackfaceVisibility, value); }
        }

        /// <summary>
        /// Gets or sets up to five separate background properties of an object.
        /// </summary>
        [DOM("background")]
        public String Background
        {
            get { return GetPropertyValue(PropertyNames.Background) ?? String.Empty; }
            set { SetProperty(PropertyNames.Background, value); }
        }

        /// <summary>
        /// Gets or sets how the background image (or images) is attached
        /// to the object within the document.
        /// </summary>
        [DOM("backgroundAttachment")]
        public String BackgroundAttachment
        {
            get { return GetPropertyValue(PropertyNames.BackgroundAttachment) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundAttachment, value); }
        }

        /// <summary>
        /// Gets or sets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DOM("backgroundClip")]
        public String BackgroundClip
        {
            get { return GetPropertyValue(PropertyNames.BackgroundClip) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundClip, value); }
        }

        /// <summary>
        /// Gets or sets the color behind the content of the object.
        /// </summary>
        [DOM("backgroundColor")]
        public String BackgroundColor
        {
            get { return GetPropertyValue(PropertyNames.BackgroundColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundColor, value); }
        }

        /// <summary>
        /// Gets or sets the background image or images of the object.
        /// </summary>
        [DOM("backgroundImage")]
        public String BackgroundImage
        {
            get { return GetPropertyValue(PropertyNames.BackgroundImage) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundImage, value); }
        }

        /// <summary>
        /// Gets or sets the positioning area of an element or multiple elements.
        /// </summary>
        [DOM("backgroundOrigin")]
        public String BackgroundOrigin
        {
            get { return GetPropertyValue(PropertyNames.BackgroundOrigin) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundOrigin, value); }
        }

        /// <summary>
        /// Gets or sets the position of the background of the object.
        /// </summary>
        [DOM("backgroundPosition")]
        public String BackgroundPosition
        {
            get { return GetPropertyValue(PropertyNames.BackgroundPosition) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundPosition, value); }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the background-position property.
        /// </summary>
        [DOM("backgroundPositionX")]
        public String BackgroundPositionX
        {
            get { return GetPropertyValue(PropertyNames.BackgroundPositionX) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundPositionX, value); }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the background-position property.
        /// </summary>
        [DOM("backgroundPositionY")]
        public String BackgroundPositionY
        {
            get { return GetPropertyValue(PropertyNames.BackgroundPositionY) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundPositionY, value); }
        }

        /// <summary>
        /// Gets or sets whether and how the background image (or images) is tiled.
        /// </summary>
        [DOM("backgroundRepeat")]
        public String BackgroundRepeat
        {
            get { return GetPropertyValue(PropertyNames.BackgroundRepeat) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundRepeat, value); }
        }

        /// <summary>
        /// Gets or sets the size of the background images.
        /// </summary>
        [DOM("backgroundSize")]
        public String BackgroundSize
        {
            get { return GetPropertyValue(PropertyNames.BackgroundSize) ?? String.Empty; }
            set { SetProperty(PropertyNames.BackgroundSize, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DOM("baselineShift")]
        public String BaselineShift
        {
            get { return GetPropertyValue(PropertyNames.BaselineShift) ?? String.Empty; }
            set { SetProperty(PropertyNames.BaselineShift, value); }
        }

        /// <summary>
        /// Gets or sets the location of the Dynamic HTML (DHTML) behaviorDHTML Behaviors.
        /// </summary>
        [DOM("behavior")]
        public String Behavior
        {
            get { return GetPropertyValue(PropertyNames.Behavior) ?? String.Empty; }
            set { SetProperty(PropertyNames.Behavior, value); }
        }

        /// <summary>
        /// Gets or sets the properties of a border drawn around an object.
        /// </summary>
        [DOM("border")]
        public String Border
        {
            get { return GetPropertyValue(PropertyNames.Border) ?? String.Empty; }
            set { SetProperty(PropertyNames.Border, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the bottom border of the object.
        /// </summary>
        [DOM("borderBottom")]
        public String BorderBottom
        {
            get { return GetPropertyValue(PropertyNames.BorderBottom) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderBottom, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the bottom border of an object.
        /// </summary>
        [DOM("borderBottomColor")]
        public String BorderBottomColor
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderBottomColor, value); }
        }

        /// <summary>
        /// Gets or sets the radii of the quarter ellipse that defines
        /// the shape of the lower-left corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderBottomLeftRadius")]
        public String BorderBottomLeftRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomLeftRadius) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderBottomLeftRadius, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the lower-right corner
        /// for the outer border edge of the current box.
        /// </summary>
        [DOM("borderBottomRightRadius")]
        public String BorderBottomRightRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomRightRadius) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderBottomRightRadius, value); }
        }

        /// <summary>
        /// Gets or sets the style of the bottom border of the object.
        /// </summary>
        [DOM("borderBottomStyle")]
        public String BorderBottomStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderBottomStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the bottom border of the object.
        /// </summary>
        [DOM("borderBottomWidth")]
        public String BorderBottomWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderBottomWidth, value); }
        }

        /// <summary>
        /// Gets or sets whether the row and cell borders of a table are joined in a
        /// single border or detached as in standard HTML.
        /// </summary>
        [DOM("borderCollapse")]
        public String BorderCollapse
        {
            get { return GetPropertyValue(PropertyNames.BorderCollapse) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderCollapse, value); }
        }

        /// <summary>
        /// Gets or sets the border color of the object.
        /// </summary>
        [DOM("borderColor")]
        public String BorderColor
        {
            get { return GetPropertyValue(PropertyNames.BorderColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderColor, value); }
        }

        /// <summary>
        /// Gets or sets an image to be used in place of the border styles.
        /// </summary>
        [DOM("borderImage")]
        public String BorderImage
        {
            get { return GetPropertyValue(PropertyNames.BorderImage) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderImage, value); }
        }

        /// <summary>
        /// Gets or sets the amount by which the border image area extends beyond the border box.
        /// </summary>
        [DOM("borderImageOutset")]
        public String BorderImageOutset
        {
            get { return GetPropertyValue(PropertyNames.BorderImageOutset) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderImageOutset, value); }
        }

        /// <summary>
        /// Gets or sets ow the image is scaled and tiled.
        /// </summary>
        [DOM("borderImageRepeat")]
        public String BorderImageRepeat
        {
            get { return GetPropertyValue(PropertyNames.BorderImageRepeat) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderImageRepeat, value); }
        }

        /// <summary>
        /// Gets or sets four inward offsets, this property slices the specified
        /// border image into a three by three grid: four corners, four edges, and a central region.
        /// </summary>
        [DOM("borderImageSlice")]
        public String BorderImageSlice
        {
            get { return GetPropertyValue(PropertyNames.BorderImageSlice) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderImageSlice, value); }
        }

        /// <summary>
        /// Gets or sets the path of the image to be used for the border.
        /// </summary>
        [DOM("borderImageSource")]
        public String BorderImageSource
        {
            get { return GetPropertyValue(PropertyNames.BorderImageSource) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderImageSource, value); }
        }

        /// <summary>
        /// Gets or sets the inward offsets from the outer border edge.
        /// </summary>
        [DOM("borderImageWidth")]
        public String BorderImageWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderImageWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderImageWidth, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the left border of the object.
        /// </summary>
        [DOM("borderLeft")]
        public String BorderLeft
        {
            get { return GetPropertyValue(PropertyNames.BorderLeft) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderLeft, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the left border of an object.
        /// </summary>
        [DOM("borderLeftColor")]
        public String BorderLeftColor
        {
            get { return GetPropertyValue(PropertyNames.BorderLeftColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderLeftColor, value); }
        }

        /// <summary>
        /// Gets or sets the style of the left border of the object.
        /// </summary>
        [DOM("borderLeftStyle")]
        public String BorderLeftStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderLeftStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderLeftStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the left border of the object.
        /// </summary>
        [DOM("borderLeftWidth")]
        public String BorderLeftWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderLeftWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderLeftWidth, value); }
        }

        /// <summary>
        /// Gets or sets the radii of a quarter ellipse that defines the shape of
        /// the corners for the outer border edge of the current box.
        /// </summary>
        [DOM("borderRadius")]
        public String BorderRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderRadius) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderRadius, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the right border of the object.
        /// </summary>
        [DOM("borderRight")]
        public String BorderRight
        {
            get { return GetPropertyValue(PropertyNames.BorderRight) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderRight, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the right border of an object.
        /// </summary>
        [DOM("borderRightColor")]
        public String BorderRightColor
        {
            get { return GetPropertyValue(PropertyNames.BorderRightColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderRightColor, value); }
        }

        /// <summary>
        /// Gets or sets the style of the right border of the object.
        /// </summary>
        [DOM("borderRightStyle")]
        public String BorderRightStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderRightStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderRightStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the right border of the object.
        /// </summary>
        [DOM("borderRightWidth")]
        public String BorderRightWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderRightWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderRightWidth, value); }
        }

        /// <summary>
        /// Gets or sets the distance between the borders of adjoining cells in a table.
        /// </summary>
        [DOM("borderSpacing")]
        public String BorderSpacing
        {
            get { return GetPropertyValue(PropertyNames.BorderSpacing) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderSpacing, value); }
        }

        /// <summary>
        /// Gets or sets the style of the left, right, top, and bottom borders of the object.
        /// </summary>
        [DOM("borderStyle")]
        public String BorderStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderStyle, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the top border of the object.
        /// </summary>
        [DOM("borderTop")]
        public String BorderTop
        {
            get { return GetPropertyValue(PropertyNames.BorderTop) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderTop, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the top border of an object.
        /// </summary>
        [DOM("borderTopColor")]
        public String BorderTopColor
        {
            get { return GetPropertyValue(PropertyNames.BorderTopColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderTopColor, value); }
        }

        /// <summary>
        /// Gets or sets  one or two values that define the radii of the quarter ellipse
        /// that defines the shape of the upper-left corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderTopLeftRadius")]
        public String BorderTopLeftRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderTopLeftRadius) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderTopLeftRadius, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-right
        /// corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderTopRightRadius")]
        public String BorderTopRightRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderTopRightRadius) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderTopRightRadius, value); }
        }

        /// <summary>
        /// Gets or sets  the style of the top border of the object.
        /// </summary>
        [DOM("borderTopStyle")]
        public String BorderTopStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderTopStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderTopStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the top border of the object.
        /// </summary>
        [DOM("borderTopWidth")]
        public String BorderTopWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderTopWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderTopWidth, value); }
        }

        /// <summary>
        /// Gets or sets the thicknesses of the left, right, top, and bottom borders of an object.
        /// </summary>
        [DOM("borderWidth")]
        public String BorderWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.BorderWidth, value); }
        }

        /// <summary>
        /// Gets or sets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DOM("boxShadow")]
        public String BoxShadow
        {
            get { return GetPropertyValue(PropertyNames.BoxShadow) ?? String.Empty; }
            set { SetProperty(PropertyNames.BoxShadow, value); }
        }

        /// <summary>
        /// Gets or sets the box model to use for object sizing.
        /// </summary>
        [DOM("boxSizing")]
        public String BoxSizing
        {
            get { return GetPropertyValue(PropertyNames.BoxSizing) ?? String.Empty; }
            set { SetProperty(PropertyNames.BoxSizing, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that follows a content
        /// block in a multi-column element.
        /// </summary>
        [DOM("breakAfter")]
        public String BreakAfter
        {
            get { return GetPropertyValue(PropertyNames.BreakAfter) ?? String.Empty; }
            set { SetProperty(PropertyNames.BreakAfter, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DOM("breakBefore")]
        public String BreakBefore
        {
            get { return GetPropertyValue(PropertyNames.BreakBefore) ?? String.Empty; }
            set { SetProperty(PropertyNames.BreakBefore, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DOM("breakInside")]
        public String BreakInside
        {
            get { return GetPropertyValue(PropertyNames.BreakInside) ?? String.Empty; }
            set { SetProperty(PropertyNames.BreakInside, value); }
        }

        /// <summary>
        /// Gets or sets where the caption of a table is located.
        /// </summary>
        [DOM("captionSide")]
        public String CaptionSide
        {
            get { return GetPropertyValue(PropertyNames.CaptionSide) ?? String.Empty; }
            set { SetProperty(PropertyNames.CaptionSide, value); }
        }

        /// <summary>
        /// Gets or sets whether the object allows floating objects on its left side,
        /// right side, or both, so that the next text displays past the floating objects.
        /// </summary>
        [DOM("clear")]
        public String Clear
        {
            get { return GetPropertyValue(PropertyNames.Clear) ?? String.Empty; }
            set { SetProperty(PropertyNames.Clear, value); }
        }

        /// <summary>
        /// Gets or sets which part of a positioned object is visible.
        /// </summary>
        [DOM("clip")]
        public String Clip
        {
            get { return GetPropertyValue(PropertyNames.Clip) ?? String.Empty; }
            set { SetProperty(PropertyNames.Clip, value); }
        }

        /// <summary>
        /// Gets or sets the bottom coordinate of the object clipping region.
        /// </summary>
        [DOM("clipBottom")]
        public String ClipBottom
        {
            get { return GetPropertyValue(PropertyNames.ClipBottom) ?? String.Empty; }
            set { SetProperty(PropertyNames.ClipBottom, value); }
        }

        /// <summary>
        /// Gets or sets the left coordinate of the object clipping region.
        /// </summary>
        [DOM("clipLeft")]
        public String ClipLeft
        {
            get { return GetPropertyValue(PropertyNames.ClipLeft) ?? String.Empty; }
            set { SetProperty(PropertyNames.ClipLeft, value); }
        }

        /// <summary>
        /// Gets or sets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DOM("clipPath")]
        public String ClipPath
        {
            get { return GetPropertyValue(PropertyNames.ClipPath) ?? String.Empty; }
            set { SetProperty(PropertyNames.ClipPath, value); }
        }

        /// <summary>
        /// Gets or sets the right coordinate of the object clipping region.
        /// </summary>
        [DOM("clipRight")]
        public String ClipRight
        {
            get { return GetPropertyValue(PropertyNames.ClipRight) ?? String.Empty; }
            set { SetProperty(PropertyNames.ClipRight, value); }
        }

        /// <summary>
        /// Gets or sets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DOM("clipRule")]
        public String ClipRule
        {
            get { return GetPropertyValue(PropertyNames.ClipRule) ?? String.Empty; }
            set { SetProperty(PropertyNames.ClipRule, value); }
        }

        /// <summary>
        /// Gets or sets the top coordinate of the object clipping region.
        /// </summary>
        [DOM("clipTop")]
        public String ClipTop
        {
            get { return GetPropertyValue(PropertyNames.ClipTop) ?? String.Empty; }
            set { SetProperty(PropertyNames.ClipTop, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the text of an object.
        /// </summary>
        [DOM("color")]
        public String Color
        {
            get { return GetPropertyValue(PropertyNames.Color) ?? String.Empty; }
            set { SetProperty(PropertyNames.Color, value); }
        }

        /// <summary>
        /// Gets or sets which color space to use for filter effects.
        /// </summary>
        [DOM("colorInterpolationFilters")]
        public String ColorInterpolationFilters
        {
            get { return GetPropertyValue(PropertyNames.ColorInterpolationFilters) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColorInterpolationFilters, value); }
        }

        /// <summary>
        /// Gets or sets the optimal number of columns in a multi-column element.
        /// </summary>
        [DOM("columnCount")]
        public String ColumnCount
        {
            get { return GetPropertyValue(PropertyNames.ColumnCount) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnCount, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DOM("columnFill")]
        public String ColumnFill
        {
            get { return GetPropertyValue(PropertyNames.ColumnFill) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnFill, value); }
        }

        /// <summary>
        /// Gets or sets the width of the gap between columns in a multi-column element.
        /// </summary>
        [DOM("columnGap")]
        public String ColumnGap
        {
            get { return GetPropertyValue(PropertyNames.ColumnGap) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnGap, value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value  that specifies values for the columnRuleWidth, 
        /// columnRuleStyle, and the columnRuleColor of a multi-column element.
        /// </summary>
        [DOM("columnRule")]
        public String ColumnRule
        {
            get { return GetPropertyValue(PropertyNames.ColumnRule) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnRule, value); }
        }

        /// <summary>
        /// Gets or sets the color for all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleColor")]
        public String ColumnRuleColor
        {
            get { return GetPropertyValue(PropertyNames.ColumnRuleColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnRuleColor, value); }
        }

        /// <summary>
        /// Gets or sets the style for all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleStyle")]
        public String ColumnRuleStyle
        {
            get { return GetPropertyValue(PropertyNames.ColumnRuleStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnRuleStyle, value); }
        }

        /// <summary>
        /// Gets or sets the width of all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleWidth")]
        public String ColumnRuleWidth
        {
            get { return GetPropertyValue(PropertyNames.ColumnRuleWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnRuleWidth, value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value that specifies values for the column-width
        /// and the column-count of a multi-column element.
        /// </summary>
        [DOM("columns")]
        public String Columns
        {
            get { return GetPropertyValue(PropertyNames.Columns) ?? String.Empty; }
            set { SetProperty(PropertyNames.Columns, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DOM("columnSpan")]
        public String ColumnSpan
        {
            get { return GetPropertyValue(PropertyNames.ColumnSpan) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnSpan, value); }
        }

        /// <summary>
        /// Gets or sets the optimal width of the columns in a multi-column element.
        /// </summary>
        [DOM("columnWidth")]
        public String ColumnWidth
        {
            get { return GetPropertyValue(PropertyNames.ColumnWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.ColumnWidth, value); }
        }

        /// <summary>
        /// Gets or sets generated content to insert before or after an element.
        /// </summary>
        [DOM("content")]
        public String Content
        {
            get { return GetPropertyValue(PropertyNames.Content) ?? String.Empty; }
            set { SetProperty(PropertyNames.Content, value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to increment.
        /// </summary>
        [DOM("counterIncrement")]
        public String CounterIncrement
        {
            get { return GetPropertyValue(PropertyNames.CounterIncrement) ?? String.Empty; }
            set { SetProperty(PropertyNames.CounterIncrement, value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to create or reset to zero.
        /// </summary>
        [DOM("counterReset")]
        public String CounterReset
        {
            get { return GetPropertyValue(PropertyNames.CounterReset) ?? String.Empty; }
            set { SetProperty(PropertyNames.CounterReset, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether a box should float
        /// to the left, right, or not at all.
        /// </summary>
        [DOM("cssFloat")]
        public String Float
        {
            get { return GetPropertyValue(PropertyNames.Float) ?? String.Empty; }
            set { SetProperty(PropertyNames.Float, value); }
        }

        /// <summary>
        /// Gets or sets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DOM("cursor")]
        public String Cursor
        {
            get { return GetPropertyValue(PropertyNames.Cursor) ?? String.Empty; }
            set { SetProperty(PropertyNames.Cursor, value); }
        }

        /// <summary>
        /// Gets or sets the reading order of the object.
        /// </summary>
        [DOM("direction")]
        public String Direction
        {
            get { return GetPropertyValue(PropertyNames.Direction) ?? String.Empty; }
            set { SetProperty(PropertyNames.Direction, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether and how the object is rendered.
        /// </summary>
        [DOM("display")]
        public String Display
        {
            get { return GetPropertyValue(PropertyNames.Display) ?? String.Empty; }
            set { SetProperty(PropertyNames.Display, value); }
        }

        /// <summary>
        /// Gets or sets a value that determines or redetermines a scaled-baseline table.
        /// </summary>
        [DOM("dominantBaseline")]
        public String DominantBaseline
        {
            get { return GetPropertyValue(PropertyNames.DominantBaseline) ?? String.Empty; }
            set { SetProperty(PropertyNames.DominantBaseline, value); }
        }

        /// <summary>
        /// Determines whether to show or hide a cell without content.
        /// </summary>
        [DOM("emptyCells")]
        public String EmptyCells
        {
            get { return GetPropertyValue(PropertyNames.EmptyCells) ?? String.Empty; }
            set { SetProperty(PropertyNames.EmptyCells, value); }
        }

        /// <summary>
        /// Allocate a shared background image all graphic elements within a container.
        /// </summary>
        [DOM("enableBackground")]
        public String EnableBackground
        {
            get { return GetPropertyValue(PropertyNames.EnableBackground) ?? String.Empty; }
            set { SetProperty(PropertyNames.EnableBackground, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DOM("fill")]
        public String Fill
        {
            get { return GetPropertyValue(PropertyNames.Fill) ?? String.Empty; }
            set { SetProperty(PropertyNames.Fill, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation that
        /// is used to paint the interior of the current object.
        /// </summary>
        [DOM("fillOpacity")]
        public String FillOpacity
        {
            get { return GetPropertyValue(PropertyNames.FillOpacity) ?? String.Empty; }
            set { SetProperty(PropertyNames.FillOpacity, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the algorithm that is to be used to determine
        /// what parts of the canvas are included inside the shape.
        /// </summary>
        [DOM("fillRule")]
        public String FillRule
        {
            get { return GetPropertyValue(PropertyNames.FillRule) ?? String.Empty; }
            set { SetProperty(PropertyNames.FillRule, value); }
        }

        /// <summary>
        /// The filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DOM("filter")]
        public String Filter
        {
            get { return GetPropertyValue(PropertyNames.Filter) ?? String.Empty; }
            set { SetProperty(PropertyNames.Filter, value); }
        }

        /// <summary>
        /// Gets or sets the parameter values of a flexible length, the positive and
        /// negative flexibility, and the preferred size.
        /// </summary>
        [DOM("flex")]
        public String Flex
        {
            get { return GetPropertyValue(PropertyNames.Flex) ?? String.Empty; }
            set { SetProperty(PropertyNames.Flex, value); }
        }

        /// <summary>
        /// Gets or sets the initial main size of the flex item.
        /// </summary>
        [DOM("flexBasis")]
        public String FlexBasis
        {
            get { return GetPropertyValue(PropertyNames.FlexBasis) ?? String.Empty; }
            set { SetProperty(PropertyNames.FlexBasis, value); }
        }

        /// <summary>
        /// Gets or sets the direction of the main axis which specifies how
        /// the flex items are displayed in the flex container.
        /// </summary>
        [DOM("flexDirection")]
        public String FlexDirection
        {
            get { return GetPropertyValue(PropertyNames.FlexDirection) ?? String.Empty; }
            set { SetProperty(PropertyNames.FlexDirection, value); }
        }

        /// <summary>
        /// Gets or sets the shorthand property to set both the flex-direction and flex-wrap
        /// properties of a flex container.
        /// </summary>
        [DOM("flexFlow")]
        public String FlexFlow
        {
            get { return GetPropertyValue(PropertyNames.FlexFlow) ?? String.Empty; }
            set { SetProperty(PropertyNames.FlexFlow, value); }
        }

        /// <summary>
        /// Gets or sets the flex grow factor for the flex item.
        /// </summary>
        [DOM("flexGrow")]
        public String FlexGrow
        {
            get { return GetPropertyValue(PropertyNames.FlexGrow) ?? String.Empty; }
            set { SetProperty(PropertyNames.FlexGrow, value); }
        }

        /// <summary>
        /// Gets or sets the flex shrink factor for the flex item.
        /// </summary>
        [DOM("flexShrink")]
        public String FlexShrink
        {
            get { return GetPropertyValue(PropertyNames.FlexShrink) ?? String.Empty; }
            set { SetProperty(PropertyNames.FlexShrink, value); }
        }

        /// <summary>
        /// Gets or sets whether flex items wrap and the direction they
        /// wrap onto multiple lines or columns based on the spac
        /// available in the flex container. 
        /// </summary>
        [DOM("flexWrap")]
        public String FlexWrap
        {
            get { return GetPropertyValue(PropertyNames.FlexWrap) ?? String.Empty; }
            set { SetProperty(PropertyNames.FlexWrap, value); }
        }

        /// <summary>
        /// Gets or sets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of
        /// six user-preference fonts.
        /// </summary>
        [DOM("font")]
        public String Font
        {
            get { return GetPropertyValue(PropertyNames.Font) ?? String.Empty; }
            set { SetProperty(PropertyNames.Font, value); }
        }

        /// <summary>
        /// Gets or sets the name of the font used for text in the object.
        /// </summary>
        [DOM("fontFamily")]
        public String FontFamily
        {
            get { return GetPropertyValue(PropertyNames.FontFamily) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontFamily, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DOM("fontFeatureSettings")]
        public String FontFeatureSettings
        {
            get { return GetPropertyValue(PropertyNames.FontFeatureSettings) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontFeatureSettings, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the font size used for text in the object.
        /// </summary>
        [DOM("fontSize")]
        public String FontSize
        {
            get { return GetPropertyValue(PropertyNames.FontSize) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontSize, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies an aspect value for an element that
        /// will effectively preserve the x-height of the first choice font, whether
        /// it is substituted or not.
        /// </summary>
        [DOM("fontSizeAdjust")]
        public String FontSizeAdjust
        {
            get { return GetPropertyValue(PropertyNames.FontSizeAdjust) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontSizeAdjust, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a normal, condensed,
        /// or expanded face of a font family.
        /// </summary>
        [DOM("fontStretch")]
        public String FontStretch
        {
            get { return GetPropertyValue(PropertyNames.FontStretch) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontStretch, value); }
        }

        /// <summary>
        /// Gets or sets the font style of the object as italic, normal, or oblique.
        /// </summary>
        [DOM("fontStyle")]
        public String FontStyle
        {
            get { return GetPropertyValue(PropertyNames.FontStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontStyle, value); }
        }

        /// <summary>
        /// Gets or sets whether the text of the object is in small capital letters.
        /// </summary>
        [DOM("fontVariant")]
        public String FontVariant
        {
            get { return GetPropertyValue(PropertyNames.FontVariant) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontVariant, value); }
        }

        /// <summary>
        /// Gets of sets the weight of the font of the object.
        /// </summary>
        [DOM("fontWeight")]
        public String FontWeight
        {
            get { return GetPropertyValue(PropertyNames.FontWeight) ?? String.Empty; }
            set { SetProperty(PropertyNames.FontWeight, value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence of characters
        /// relative to an inline-progression-direction of horizontal.
        /// </summary>
        [DOM("glyphOrientationHorizontal")]
        public String GlyphOrientationHorizontal
        {
            get { return GetPropertyValue(PropertyNames.GlyphOrientationHorizontal) ?? String.Empty; }
            set { SetProperty(PropertyNames.GlyphOrientationHorizontal, value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of vertical.
        /// </summary>
        [DOM("glyphOrientationVertical")]
        public String GlyphOrientationVertical
        {
            get { return GetPropertyValue(PropertyNames.GlyphOrientationVertical) ?? String.Empty; }
            set { SetProperty(PropertyNames.GlyphOrientationVertical, value); }
        }

        /// <summary>
        /// Gets or sets the height of the object.
        /// </summary>
        [DOM("height")]
        public String Height
        {
            get { return GetPropertyValue(PropertyNames.Height) ?? String.Empty; }
            set { SetProperty(PropertyNames.Height, value); }
        }

        /// <summary>
        /// Gets or sets the state of an IME.
        /// </summary>
        [DOM("imeMode")]
        public String ImeMode
        {
            get { return GetPropertyValue(PropertyNames.ImeMode) ?? String.Empty; }
            set { SetProperty(PropertyNames.ImeMode, value); }
        }

        /// <summary>
        /// Gets or sets a how flex items are aligned along the main axis of the flex
        /// container after any flexible lengths and auto margins are resolved.
        /// </summary>
        [DOM("justifyContent")]
        public String JustifyContent
        {
            get { return GetPropertyValue(PropertyNames.JustifyContent) ?? String.Empty; }
            set { SetProperty(PropertyNames.JustifyContent, value); }
        }

        /// <summary>
        /// Gets or sets the composite document grid properties
        /// that specify the layout of text characters.
        /// </summary>
        [DOM("layoutGrid")]
        public String LayoutGrid
        {
            get { return GetPropertyValue(PropertyNames.LayoutGrid) ?? String.Empty; }
            set { SetProperty(PropertyNames.LayoutGrid, value); }
        }

        /// <summary>
        /// Gets or sets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DOM("layoutGridChar")]
        public String LayoutGridChar
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridChar) ?? String.Empty; }
            set { SetProperty(PropertyNames.LayoutGridChar, value); }
        }

        /// <summary>
        /// Gets or sets the gridline value used for rendering the
        /// text content of an element.
        /// </summary>
        [DOM("layoutGridLine")]
        public String LayoutGridLine
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridLine) ?? String.Empty; }
            set { SetProperty(PropertyNames.LayoutGridLine, value); }
        }

        /// <summary>
        /// Gets or sets whether the text layout grid uses two dimensions.
        /// </summary>
        [DOM("layoutGridMode")]
        public String LayoutGridMode
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridMode) ?? String.Empty; }
            set { SetProperty(PropertyNames.LayoutGridMode, value); }
        }

        /// <summary>
        /// Gets or sets the type of grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DOM("layoutGridType")]
        public String LayoutGridType
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridType) ?? String.Empty; }
            set { SetProperty(PropertyNames.LayoutGridType, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("left")]
        public String Left
        {
            get { return GetPropertyValue(PropertyNames.Left) ?? String.Empty; }
            set { SetProperty(PropertyNames.Left, value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between letters in the object.
        /// </summary>
        [DOM("letterSpacing")]
        public String LetterSpacing
        {
            get { return GetPropertyValue(PropertyNames.LetterSpacing) ?? String.Empty; }
            set { SetProperty(PropertyNames.LetterSpacing, value); }
        }

        /// <summary>
        /// Gets or sets the distance between lines in the object.
        /// </summary>
        [DOM("lineHeight")]
        public String LineHeight
        {
            get { return GetPropertyValue(PropertyNames.LineHeight) ?? String.Empty; }
            set { SetProperty(PropertyNames.LineHeight, value); }
        }

        /// <summary>
        /// Gets or sets up to three separate list-style properties of the object.
        /// </summary>
        [DOM("listStyle")]
        public String ListStyle
        {
            get { return GetPropertyValue(PropertyNames.ListStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.ListStyle, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates which image to use as
        /// a list-item marker for the object.
        /// </summary>
        [DOM("listStyleImage")]
        public String ListStyleImage
        {
            get { return GetPropertyValue(PropertyNames.ListStyleImage) ?? String.Empty; }
            set { SetProperty(PropertyNames.ListStyleImage, value); }
        }

        /// <summary>
        /// Gets or sets a variable that indicates how the list-item marker
        /// is drawn relative to the content of the object.
        /// </summary>
        [DOM("listStylePosition")]
        public String ListStylePosition
        {
            get { return GetPropertyValue(PropertyNames.ListStylePosition) ?? String.Empty; }
            set { SetProperty(PropertyNames.ListStylePosition, value); }
        }

        /// <summary>
        /// Gets or sets the predefined type of the line-item marker for the object.
        /// </summary>
        [DOM("listStyleType")]
        public String ListStyleType
        {
            get { return GetPropertyValue(PropertyNames.ListStyleType) ?? String.Empty; }
            set { SetProperty(PropertyNames.ListStyleType, value); }
        }

        /// <summary>
        /// Gets or sets the width of the top, right, bottom, and left margins of the object.
        /// </summary>
        [DOM("margin")]
        public String Margin
        {
            get { return GetPropertyValue(PropertyNames.Margin) ?? String.Empty; }
            set { SetProperty(PropertyNames.Margin, value); }
        }

        /// <summary>
        /// Gets or sets the height of the bottom margin of the object.
        /// </summary>
        [DOM("marginBottom")]
        public String MarginBottom
        {
            get { return GetPropertyValue(PropertyNames.MarginBottom) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarginBottom, value); }
        }

        /// <summary>
        /// Gets or sets the width of the left margin of the object.
        /// </summary>
        [DOM("marginLeft")]
        public String MarginLeft
        {
            get { return GetPropertyValue(PropertyNames.MarginLeft) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarginLeft, value); }
        }

        /// <summary>
        /// Gets or sets the width of the right margin of the object.
        /// </summary>
        [DOM("marginRight")]
        public String MarginRight
        {
            get { return GetPropertyValue(PropertyNames.MarginRight) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarginRight, value); }
        }

        /// <summary>
        /// Gets or sets the height of the top margin of the object.
        /// </summary>
        [DOM("marginTop")]
        public String MarginTop
        {
            get { return GetPropertyValue(PropertyNames.MarginTop) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarginTop, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DOM("marker")]
        public String Marker
        {
            get { return GetPropertyValue(PropertyNames.Marker) ?? String.Empty; }
            set { SetProperty(PropertyNames.Marker, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the final vertex of a given path element or
        /// basic shape.
        /// </summary>
        [DOM("markerEnd")]
        public String MarkerEnd
        {
            get { return GetPropertyValue(PropertyNames.MarkerEnd) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarkerEnd, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DOM("markerMid")]
        public String MarkerMid
        {
            get { return GetPropertyValue(PropertyNames.MarkerMid) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarkerMid, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the first vertex of a given path element or
        /// basic shape.
        /// </summary>
        [DOM("markerStart")]
        public String MarkerStart
        {
            get { return GetPropertyValue(PropertyNames.MarkerStart) ?? String.Empty; }
            set { SetProperty(PropertyNames.MarkerStart, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a SVG mask.
        /// </summary>
        [DOM("mask")]
        public String Mask
        {
            get { return GetPropertyValue(PropertyNames.Mask) ?? String.Empty; }
            set { SetProperty(PropertyNames.Mask, value); }
        }

        /// <summary>
        /// Gets or sets the maximum height for an element.
        /// </summary>
        [DOM("maxHeight")]
        public String MaxHeight
        {
            get { return GetPropertyValue(PropertyNames.MaxHeight) ?? String.Empty; }
            set { SetProperty(PropertyNames.MaxHeight, value); }
        }

        /// <summary>
        /// Gets or sets the maximum width for an element.
        /// </summary>
        [DOM("maxWidth")]
        public String MaxWidth
        {
            get { return GetPropertyValue(PropertyNames.MaxWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.MaxWidth, value); }
        }

        /// <summary>
        /// Gets or sets the minimum height for an element.
        /// </summary>
        [DOM("minHeight")]
        public String MinHeight
        {
            get { return GetPropertyValue(PropertyNames.MinHeight) ?? String.Empty; }
            set { SetProperty(PropertyNames.MinHeight, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width for an element.
        /// </summary>
        [DOM("minWidth")]
        public String MinWidth
        {
            get { return GetPropertyValue(PropertyNames.MinWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.MinWidth, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies object or group opacity in CSS or SVG.
        /// </summary>
        [DOM("opacity")]
        public String Opacity
        {
            get { return GetPropertyValue(PropertyNames.Opacity) ?? String.Empty; }
            set { SetProperty(PropertyNames.Opacity, value); }
        }

        /// <summary>
        /// Gets or sets the order, which property specifies the order used to lay out
        /// flex items in their flex container. Elements are laid out by ascending order
        /// of the order value. Elements with the same order value are laid out in the
        /// order they appear in the source code.
        /// </summary>
        [DOM("order")]
        public String Order
        {
            get { return GetPropertyValue(PropertyNames.Order) ?? String.Empty; }
            set { SetProperty(PropertyNames.Order, value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph
        /// that must appear at the bottom of a page.
        /// </summary>
        [DOM("orphans")]
        public String Orphans
        {
            get { return GetPropertyValue(PropertyNames.Orphans) ?? String.Empty; }
            set { SetProperty(PropertyNames.Orphans, value); }
        }

        /// <summary>
        /// Gets or sets the outline frame.
        /// </summary>
        [DOM("outline")]
        public String Outline
        {
            get { return GetPropertyValue(PropertyNames.Outline) ?? String.Empty; }
            set { SetProperty(PropertyNames.Outline, value); }
        }

        /// <summary>
        /// Gets or sets the color of the outline frame.
        /// </summary>
        [DOM("outlineColor")]
        public String OutlineColor
        {
            get { return GetPropertyValue(PropertyNames.OutlineColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.OutlineColor, value); }
        }

        /// <summary>
        /// Gets or sets the style of the outline frame.
        /// </summary>
        [DOM("outlineStyle")]
        public String OutlineStyle
        {
            get { return GetPropertyValue(PropertyNames.OutlineStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.OutlineStyle, value); }
        }

        /// <summary>
        /// Gets or sets the width of the outline frame.
        /// </summary>
        [DOM("outlineWidth")]
        public String OutlineWidth
        {
            get { return GetPropertyValue(PropertyNames.OutlineWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.OutlineWidth, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DOM("overflow")]
        public String Overflow
        {
            get { return GetPropertyValue(PropertyNames.Overflow) ?? String.Empty; }
            set { SetProperty(PropertyNames.Overflow, value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DOM("overflowX")]
        public String OverflowX
        {
            get { return GetPropertyValue(PropertyNames.OverflowX) ?? String.Empty; }
            set { SetProperty(PropertyNames.OverflowX, value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when
        /// the content exceeds the height of the object.
        /// </summary>
        [DOM("overflowY")]
        public String OverflowY
        {
            get { return GetPropertyValue(PropertyNames.OverflowY) ?? String.Empty; }
            set { SetProperty(PropertyNames.OverflowY, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its border.
        /// </summary>
        [DOM("padding")]
        public String Padding
        {
            get { return GetPropertyValue(PropertyNames.Padding) ?? String.Empty; }
            set { SetProperty(PropertyNames.Padding, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingBottom")]
        public String PaddingBottom
        {
            get { return GetPropertyValue(PropertyNames.PaddingBottom) ?? String.Empty; }
            set { SetProperty(PropertyNames.PaddingBottom, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingLeft")]
        public String PaddingLeft
        {
            get { return GetPropertyValue(PropertyNames.PaddingLeft) ?? String.Empty; }
            set { SetProperty(PropertyNames.PaddingLeft, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between
        /// the right border of the object and the content.
        /// </summary>
        [DOM("paddingRight")]
        public String PaddingRight
        {
            get { return GetPropertyValue(PropertyNames.PaddingRight) ?? String.Empty; }
            set { SetProperty(PropertyNames.PaddingRight, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the top
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingTop")]
        public String PaddingTop
        {
            get { return GetPropertyValue(PropertyNames.PaddingTop) ?? String.Empty; }
            set { SetProperty(PropertyNames.PaddingTop, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a page break occurs after the object.
        /// </summary>
        [DOM("pageBreakAfter")]
        public String PageBreakAfter
        {
            get { return GetPropertyValue(PropertyNames.PageBreakAfter) ?? String.Empty; }
            set { SetProperty(PropertyNames.PageBreakAfter, value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break occurs before the object.
        /// </summary>
        [DOM("pageBreakBefore")]
        public String PageBreakBefore
        {
            get { return GetPropertyValue(PropertyNames.PageBreakBefore) ?? String.Empty; }
            set { SetProperty(PropertyNames.PageBreakBefore, value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break is
        /// allowed to occur inside the object.
        /// </summary>
        [DOM("pageBreakInside")]
        public String PageBreakInside
        {
            get { return GetPropertyValue(PropertyNames.PageBreakInside) ?? String.Empty; }
            set { SetProperty(PropertyNames.PageBreakInside, value); }
        }

        /// <summary>
        /// Gets or sets a value that represents the perspective from which all child
        /// elements of the object are viewed.
        /// </summary>
        [DOM("perspective")]
        public String Perspective
        {
            get { return GetPropertyValue(PropertyNames.Perspective) ?? String.Empty; }
            set { SetProperty(PropertyNames.Perspective, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DOM("perspectiveOrigin")]
        public String PerspectiveOrigin
        {
            get { return GetPropertyValue(PropertyNames.PerspectiveOrigin) ?? String.Empty; }
            set { SetProperty(PropertyNames.PerspectiveOrigin, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies under what circumstances a given graphics
        /// element can be the target element for a pointer event in SVG.
        /// </summary>
        [DOM("pointerEvents")]
        public String PointerEvents
        {
            get { return GetPropertyValue(PropertyNames.PointerEvents) ?? String.Empty; }
            set { SetProperty(PropertyNames.PointerEvents, value); }
        }

        /// <summary>
        /// Gets or sets the type of positioning used for the object.
        /// </summary>
        [DOM("position")]
        public String Position
        {
            get { return GetPropertyValue(PropertyNames.Position) ?? String.Empty; }
            set { SetProperty(PropertyNames.Position, value); }
        }

        /// <summary>
        /// Gets or sets the pairs of strings to be used as quotes in generated content.
        /// </summary>
        [DOM("quotes")]
        public String Quotes
        {
            get { return GetPropertyValue(PropertyNames.Quotes) ?? String.Empty; }
            set { SetProperty(PropertyNames.Quotes, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the right edge of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("right")]
        public String Right
        {
            get { return GetPropertyValue(PropertyNames.Right) ?? String.Empty; }
            set { SetProperty(PropertyNames.Right, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the ruby text content.
        /// </summary>
        [DOM("rubyAlign")]
        public String RubyAlign
        {
            get { return GetPropertyValue(PropertyNames.RubyAlign) ?? String.Empty; }
            set { SetProperty(PropertyNames.RubyAlign, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether, and on which side, ruby
        /// text is allowed to partially overhang any adjacent text in addition
        /// to its own base, when the ruby text is wider than the ruby base
        /// </summary>
        [DOM("rubyOverhang")]
        public String RubyOverhang
        {
            get { return GetPropertyValue(PropertyNames.RubyOverhang) ?? String.Empty; }
            set { SetProperty(PropertyNames.RubyOverhang, value); }
        }

        /// <summary>
        /// Gets or sets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        [DOM("rubyPosition")]
        public String RubyPosition
        {
            get { return GetPropertyValue(PropertyNames.RubyPosition) ?? String.Empty; }
            set { SetProperty(PropertyNames.RubyPosition, value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbar3dLightColor")]
        public String Scrollbar3dLightColor
        {
            get { return GetPropertyValue(PropertyNames.Scrollbar3dLightColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.Scrollbar3dLightColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DOM("scrollbarArrowColor")]
        public String ScrollbarArrowColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarArrowColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ScrollbarArrowColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the gutter of a scroll bar.
        /// </summary>
        [DOM("scrollbarDarkShadowColor")]
        public String ScrollbarDarkShadowColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarDarkShadowColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ScrollbarDarkShadowColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarFaceColor")]
        public String ScrollbarFaceColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarFaceColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ScrollbarFaceColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarHighlightColor")]
        public String ScrollbarHighlightColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarHighlightColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ScrollbarHighlightColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the bottom and right edges of the
        /// scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarShadowColor")]
        public String ScrollbarShadowColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarShadowColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ScrollbarShadowColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the track element of a scroll bar.
        /// </summary>
        [DOM("scrollbarTrackColor")]
        public String ScrollbarTrackColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarTrackColor) ?? String.Empty; }
            set { SetProperty(PropertyNames.ScrollbarTrackColor, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint along
        /// the outline of a given graphical element.
        /// </summary>
        [DOM("stroke")]
        public String Stroke
        {
            get { return GetPropertyValue(PropertyNames.Stroke) ?? String.Empty; }
            set { SetProperty(PropertyNames.Stroke, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that indicate the pattern of
        /// dashes and gaps used to stroke paths.
        /// </summary>
        [DOM("strokeDasharray")]
        public String StrokeDasharray
        {
            get { return GetPropertyValue(PropertyNames.StrokeDasharray) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeDasharray, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the distance into the
        /// dash pattern to start the dash.
        /// </summary>
        [DOM("strokeDashoffset")]
        public String StrokeDashoffset
        {
            get { return GetPropertyValue(PropertyNames.StrokeDashoffset) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeDashoffset, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the
        /// end of open subpaths when they are stroked.
        /// </summary>
        [DOM("strokeLinecap")]
        public String StrokeLinecap
        {
            get { return GetPropertyValue(PropertyNames.StrokeLinecap) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeLinecap, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the corners of
        /// paths or basic shapes when they are stroked.
        /// </summary>
        [DOM("strokeLinejoin")]
        public String StrokeLinejoin
        {
            get { return GetPropertyValue(PropertyNames.StrokeLinejoin) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeLinejoin, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin property).
        /// </summary>
        [DOM("strokeMiterlimit")]
        public String StrokeMiterlimit
        {
            get { return GetPropertyValue(PropertyNames.StrokeMiterlimit) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeMiterlimit, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation
        /// that is used to stroke the current object.
        /// </summary>
        [DOM("strokeOpacity")]
        public String StrokeOpacity
        {
            get { return GetPropertyValue(PropertyNames.StrokeOpacity) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeOpacity, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the width of the stroke on the current object.
        /// </summary>
        [DOM("strokeWidth")]
        public String StrokeWidth
        {
            get { return GetPropertyValue(PropertyNames.StrokeWidth) ?? String.Empty; }
            set { SetProperty(PropertyNames.StrokeWidth, value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the table layout is fixed.
        /// </summary>
        [DOM("tableLayout")]
        public String TableLayout
        {
            get { return GetPropertyValue(PropertyNames.TableLayout) ?? String.Empty; }
            set { SetProperty(PropertyNames.TableLayout, value); }
        }

        /// <summary>
        /// Gets or sets whether the text in the object is left-aligned, right-aligned, 
        /// centered, or justified.
        /// </summary>
        [DOM("textAlign")]
        public String TextAlign
        {
            get { return GetPropertyValue(PropertyNames.TextAlign) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextAlign, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the last line or only
        /// line of text in the specified object.
        /// </summary>
        [DOM("textAlignLast")]
        public String TextAlignLast
        {
            get { return GetPropertyValue(PropertyNames.TextAlignLast) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextAlignLast, value); }
        }

        /// <summary>
        /// Aligns a string of text relative to the specified point.
        /// </summary>
        [DOM("textAnchor")]
        public String TextAnchor
        {
            get { return GetPropertyValue(PropertyNames.TextAnchor) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextAnchor, value); }
        }

        /// <summary>
        /// Gets or sets the autospacing and narrow space width adjustment of text.
        /// </summary>
        [DOM("textAutospace")]
        public String TextAutospace
        {
            get { return GetPropertyValue(PropertyNames.TextAutospace) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextAutospace, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DOM("textDecoration")]
        public String TextDecoration
        {
            get { return GetPropertyValue(PropertyNames.TextDecoration) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextDecoration, value); }
        }

        /// <summary>
        /// Gets or sets the indentation of the first line of text in the object.
        /// </summary>
        [DOM("textIndent")]
        public String TextIndent
        {
            get { return GetPropertyValue(PropertyNames.TextIndent) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextIndent, value); }
        }

        /// <summary>
        /// Gets or sets the type of alignment used to justify text in the object.
        /// </summary>
        [DOM("textJustify")]
        public String TextJustify
        {
            get { return GetPropertyValue(PropertyNames.TextJustify) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextJustify, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether to render
        /// ellipses (...) to indicate text overflow.
        /// </summary>
        [DOM("textOverflow")]
        public String TextOverflow
        {
            get { return GetPropertyValue(PropertyNames.TextOverflow) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextOverflow, value); }
        }

        /// <summary>
        /// Gets or sets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DOM("textShadow")]
        public String TextShadow
        {
            get { return GetPropertyValue(PropertyNames.TextShadow) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextShadow, value); }
        }

        /// <summary>
        /// Gets or sets the rendering of the text in the object.
        /// </summary>
        [DOM("textTransform")]
        public String TextTransform
        {
            get { return GetPropertyValue(PropertyNames.TextTransform) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextTransform, value); }
        }

        /// <summary>
        /// Gets or sets the position of the underline decoration that is set through the
        /// text-decoration property of the object.
        /// </summary>
        [DOM("textUnderlinePosition")]
        public String TextUnderlinePosition
        {
            get { return GetPropertyValue(PropertyNames.TextUnderlinePosition) ?? String.Empty; }
            set { SetProperty(PropertyNames.TextUnderlinePosition, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("top")]
        public String Top
        {
            get { return GetPropertyValue(PropertyNames.Top) ?? String.Empty; }
            set { SetProperty(PropertyNames.Top, value); }
        }

        /// <summary>
        /// Gets or sets a list of one or more transform functions that specify how
        /// to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DOM("transform")]
        public String Transform
        {
            get { return GetPropertyValue(PropertyNames.Transform) ?? String.Empty; }
            set { SetProperty(PropertyNames.Transform, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that establish the origin of transformation for an element.
        /// </summary>
        [DOM("transformOrigin")]
        public String TransformOrigin
        {
            get { return GetPropertyValue(PropertyNames.TransformOrigin) ?? String.Empty; }
            set { SetProperty(PropertyNames.TransformOrigin, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DOM("transformStyle")]
        public String TransformStyle
        {
            get { return GetPropertyValue(PropertyNames.TransformStyle) ?? String.Empty; }
            set { SetProperty(PropertyNames.TransformStyle, value); }
        }

        /// <summary>
        /// Gets or sets one or more shorthand values that specify the transition properties
        /// for a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        [DOM("transition")]
        public String Transition
        {
            get { return GetPropertyValue(PropertyNames.Transition) ?? String.Empty; }
            set { SetProperty(PropertyNames.Transition, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the offset within a
        /// transition (the amount of time from the start of a transition) before
        /// the transition is displayed  for a set of corresponding object properties 
        /// identified in the transition property.
        /// </summary>
        [DOM("transitionDelay")]
        public String TransitionDelay
        {
            get { return GetPropertyValue(PropertyNames.TransitionDelay) ?? String.Empty; }
            set { SetProperty(PropertyNames.TransitionDelay, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the durations of transitions on
        /// a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        [DOM("transitionDuration")]
        public String TransitionDuration
        {
            get { return GetPropertyValue(PropertyNames.TransitionDuration) ?? String.Empty; }
            set { SetProperty(PropertyNames.TransitionDuration, value); }
        }

        /// <summary>
        /// Gets or sets a value that identifies the CSS property name or names to which
        /// the transition effect (defined by the transition-duration, transition-timing-function,
        /// and transition-delay properties) is applied when a new property value is specified.
        /// </summary>
        [DOM("transitionProperty")]
        public String TransitionProperty
        {
            get { return GetPropertyValue(PropertyNames.TransitionProperty) ?? String.Empty; }
            set { SetProperty(PropertyNames.TransitionProperty, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the intermediate property values to be
        /// used during a transition on a set of corresponding object properties identified
        /// in the transition-property property.
        /// </summary>
        [DOM("transitionTimingFunction")]
        public String TransitionTimingFunction
        {
            get { return GetPropertyValue(PropertyNames.TransitionTimingFunction) ?? String.Empty; }
            set { SetProperty(PropertyNames.TransitionTimingFunction, value); }
        }

        /// <summary>
        /// Gets or sets the level of embedding with respect to the bidirectional algorithm.
        /// </summary>
        [DOM("unicodeBidi")]
        public String UnicodeBidi
        {
            get { return GetPropertyValue(PropertyNames.UnicodeBidi) ?? String.Empty; }
            set { SetProperty(PropertyNames.UnicodeBidi, value); }
        }

        /// <summary>
        /// Gets or sets the vertical alignment of the object.
        /// </summary>
        [DOM("verticalAlign")]
        public String VerticalAlign
        {
            get { return GetPropertyValue(PropertyNames.VerticalAlign) ?? String.Empty; }
            set { SetProperty(PropertyNames.VerticalAlign, value); }
        }

        /// <summary>
        /// Gets or sets whether the content of the object is displayed.
        /// </summary>
        [DOM("visibility")]
        public String Visibility
        {
            get { return GetPropertyValue(PropertyNames.Visibility) ?? String.Empty; }
            set { SetProperty(PropertyNames.Visibility, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        [DOM("whiteSpace")]
        public String WhiteSpace
        {
            get { return GetPropertyValue(PropertyNames.WhiteSpace) ?? String.Empty; }
            set { SetProperty(PropertyNames.WhiteSpace, value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        [DOM("widows")]
        public String Widows
        {
            get { return GetPropertyValue(PropertyNames.Widows) ?? String.Empty; }
            set { SetProperty(PropertyNames.Widows, value); }
        }

        /// <summary>
        /// Gets or sets the width of the object.
        /// </summary>
        [DOM("width")]
        public String Width
        {
            get { return GetPropertyValue(PropertyNames.Width) ?? String.Empty; }
            set { SetProperty(PropertyNames.Width, value); }
        }

        /// <summary>
        /// Gets or sets line-breaking behavior within words, particularly where
        /// multiple languages appear in the object.
        /// </summary>
        [DOM("wordBreak")]
        public String WordBreak
        {
            get { return GetPropertyValue(PropertyNames.WordBreak) ?? String.Empty; }
            set { SetProperty(PropertyNames.WordBreak, value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between words in the object.
        /// </summary>
        [DOM("wordSpacing")]
        public String WordSpacing
        {
            get { return GetPropertyValue(PropertyNames.WordSpacing) ?? String.Empty; }
            set { SetProperty(PropertyNames.WordSpacing, value); }
        }

        /// <summary>
        /// Gets or sets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        [DOM("wordWrap")]
        public String WordWrap
        {
            get { return GetPropertyValue(PropertyNames.WordWrap) ?? String.Empty; }
            set { SetProperty(PropertyNames.WordWrap, value); }
        }

        /// <summary>
        /// Gets or sets the direction and flow of the content in the object.
        /// </summary>
        [DOM("writingMode")]
        public String WritingMode
        {
            get { return GetPropertyValue(PropertyNames.WritingMode) ?? String.Empty; }
            set { SetProperty(PropertyNames.WritingMode, value); }
        }

        /// <summary>
        /// Gets or sets the stacking order of positioned objects.
        /// </summary>
        [DOM("zIndex")]
        public String ZIndex
        {
            get { return GetPropertyValue(PropertyNames.ZIndex) ?? String.Empty; }
            set { SetProperty(PropertyNames.ZIndex, value); }
        }

        /// <summary>
        /// Gets or sets the magnification scale of the object.
        /// </summary>
        [DOM("zoom")]
        public String Zoom
        {
            get { return GetPropertyValue(PropertyNames.Zoom) ?? String.Empty; }
            set { SetProperty(PropertyNames.Zoom, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the value deleted.
        /// </summary>
        /// <param name="propertyName">The name of the property to be removed.</param>
        /// <returns>The value of the deleted property.</returns>
        [DOM("removeProperty")]
        public String RemoveProperty(String propertyName)
        {
            CSSProperty property;

            if (_rules.TryGetValue(propertyName, out property))
            {
                _rules.Remove(propertyName);
                Propagate();
                return property.Value.CssText;
            }

            return null;
        }

        /// <summary>
        /// Returns the optional priority, "important".
        /// </summary>
        /// <param name="propertyName">The name of the property to get the priority of.</param>
        /// <returns>A priority or null.</returns>
        [DOM("getPropertyPriority")]
        public String GetPropertyPriority(String propertyName)
        {
            CSSProperty property;

            if (_rules.TryGetValue(propertyName, out property) && property.Important)
                return "important";

            return null;
        }

        /// <summary>
        /// Returns the value of a property.
        /// </summary>
        /// <param name="propertyName">The name of the property to get the value of.</param>
        /// <returns>A value or null if nothing has been set.</returns>
        [DOM("getPropertyValue")]
        public String GetPropertyValue(String propertyName)
        {
            CSSProperty property;

            if (_rules.TryGetValue(propertyName, out property))
                return property.Value.CssText;

            return null;
        }

        /// <summary>
        /// Sets a property with the given name and value.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <returns>The current style declaration.</returns>
        [DOM("setProperty")]
        public CSSStyleDeclaration SetProperty(String propertyName, String propertyValue)
        {
            var decl = CssParser.ParseDeclaration(propertyName + ":" + propertyValue);

            if (decl != null)
            {
                _rules[propertyName] = decl;
                Propagate();
            }

            return this;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Gets the given CSS property.
        /// </summary>
        /// <param name="index">The index of the property to get.</param>
        /// <returns>The property at the specified position or null.</returns>
        internal CSSProperty Get(Int32 index)
        {
            var i = 0;

            foreach (var rule in _rules)
            {
                if (i++ == index)
                    return rule.Value;
            }

            return null;
        }

        /// <summary>
        /// Gets the given CSS property.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The property with the specified name or null.</returns>
        internal CSSProperty Get(String name)
        {
            CSSProperty prop;

            if (_rules.TryGetValue(name, out prop))
                return prop;

            return null;
        }

        /// <summary>
        /// Sets the given CSS property.
        /// </summary>
        /// <param name="property">The property to set.</param>
        internal void Set(CSSProperty property)
        {
            if (property != null)
                _rules[property.Name] = property;
        }

        /// <summary>
        /// Updates the CSSStyleDeclaration with the given value.
        /// </summary>
        /// <param name="value">The new value.</param>
        internal void Update(String value)
        {
            if (!_blocking)
            {
                _rules.Clear();
                CssParser.AppendDeclarations(this, value ?? String.Empty);
            }
        }

        #endregion

        #region Helpers

        void Propagate()
        {
            _blocking = true;
            _setter(ToCss());
            _blocking = false;
        }

        #endregion

        #region String Representation

        /// <summary>
        /// Returns the CSS representation of the list of rules.
        /// </summary>
        /// <returns>A string containing the CSS code of the declarations.</returns>
        public String ToCss()
        {
            var sb = Pool.NewStringBuilder();

            foreach (var rule in _rules)
            {
                if (rule.Value.IsLegal)
                    sb.Append(rule.Value.ToCss()).Append(Specification.Semicolon);
            }

            return sb.ToPool();
        }

        #endregion

        #region Interface implementation

        /// <summary>
        /// Returns an ienumerator that enumerates over all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        public IEnumerator<CSSProperty> GetEnumerator()
        {
            return _rules.Values.GetEnumerator();
        }

        /// <summary>
        /// Returns a common ienumerator to enumerate all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
