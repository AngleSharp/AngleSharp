using AngleSharp.Css;
using AngleSharp.DOM.Css;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    [DOM("CSSStyleDeclaration")]
    public sealed class CSSStyleDeclaration : IEnumerable<CSSProperty>
    {
        #region Members

        List<CSSProperty> _rules;
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
            String text = String.Empty;
            _getter = () => text;
            _setter = value => text = value;
            _rules = new List<CSSProperty>();
        }

        /// <summary>
        /// Creates a new CSS style declaration with pre-defined getter
        /// and setter functions (use-case: HTML element).
        /// </summary>
        /// <param name="host">The element to host this representation.</param>
        internal CSSStyleDeclaration(Element host)
        {
            _getter = () => host.GetAttribute("style");
            _setter = value => host.SetAttribute("style", value);
            _rules = new List<CSSProperty>();
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
            get { return index >= 0 && index < Length ? _rules[index].Name : null; }
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
            get { return GetPropertyValue("align-content") ?? String.Empty; }
            set { SetProperty("align-content", value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the object represents a
        /// keyboard shortcut.
        /// </summary>
        [DOM("accelerator")]
        public String Accelerator
        {
            get { return GetPropertyValue("accelerator") ?? String.Empty; }
            set { SetProperty("accelerator", value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex container.
        /// </summary>
        [DOM("alignItems")]
        public String AlignItems
        {
            get { return GetPropertyValue("align-items") ?? String.Empty; }
            set { SetProperty("align-items", value); }
        }

        /// <summary>
        /// Gets or sets which baseline of this element is to be aligned
        /// with the corresponding baseline of the parent.
        /// </summary>
        [DOM("alignmentBaseline")]
        public String AlignmentBaseline
        {
            get { return GetPropertyValue("alignment-baseline") ?? String.Empty; }
            set { SetProperty("alignment-baseline", value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout
        /// axis defined by the flex-direction property) of flex items of
        /// the flex container.
        /// </summary>
        [DOM("alignSelf")]
        public String AlignSelf
        {
            get { return GetPropertyValue("align-self") ?? String.Empty; }
            set { SetProperty("align-self", value); }
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
            get { return GetPropertyValue("animation") ?? String.Empty; }
            set { SetProperty("animation", value); }
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
            get { return GetPropertyValue("animation-delay") ?? String.Empty; }
            set { SetProperty("animation-delay", value); }
        }

        /// <summary>
        /// Gets or sets the direction of play for an animation cycle.
        /// </summary>
        [DOM("animationDirection")]
        public String AnimationDirection
        {
            get { return GetPropertyValue("animation-direction") ?? String.Empty; }
            set { SetProperty("animation-direction", value); }
        }

        /// <summary>
        /// Gets or sets the length of time to complete one cycle of the animation.
        /// </summary>
        [DOM("animationDuration")]
        public String AnimationDuration
        {
            get { return GetPropertyValue("animation-duration") ?? String.Empty; }
            set { SetProperty("animation-duration", value); }
        }

        /// <summary>
        /// Gets or sets whether the effects of an animation are visible before or after it plays.
        /// </summary>
        [DOM("animationFillMode")]
        public String AnimationFillMode
        {
            get { return GetPropertyValue("animation-fill-mode") ?? String.Empty; }
            set { SetProperty("animation-fill-mode", value); }
        }

        /// <summary>
        /// Gets or sets the number of times an animation cycle is played.
        /// </summary>
        [DOM("animationIterationCount")]
        public String AnimationIterationCount
        {
            get { return GetPropertyValue("animation-iteration-count") ?? String.Empty; }
            set { SetProperty("animation-iteration-count", value); }
        }

        /// <summary>
        /// Gets or sets one or more animation names. An animation name
        /// selects a CSS @keyframes at-rule.
        /// </summary>
        [DOM("animationName")]
        public String AnimationName
        {
            get { return GetPropertyValue("animation-name") ?? String.Empty; }
            set { SetProperty("animation-name", value); }
        }

        /// <summary>
        /// Gets or sets whether an animation is playing or paused.
        /// </summary>
        [DOM("animationPlayState")]
        public String AnimationPlayState
        {
            get { return GetPropertyValue("animation-play-state") ?? String.Empty; }
            set { SetProperty("animation-play-state", value); }
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
            get { return GetPropertyValue("animation-timing-function") ?? String.Empty; }
            set { SetProperty("animation-timing-function", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the back face
        /// (reverse side) of an object is visible.
        /// </summary>
        [DOM("backfaceVisibility")]
        public String BackfaceVisibility
        {
            get { return GetPropertyValue("backface-visibility") ?? String.Empty; }
            set { SetProperty("backface-visibility", value); }
        }

        /// <summary>
        /// Gets or sets up to five separate background properties of an object.
        /// </summary>
        [DOM("background")]
        public String Background
        {
            get { return GetPropertyValue("background") ?? String.Empty; }
            set { SetProperty("background", value); }
        }

        /// <summary>
        /// Gets or sets how the background image (or images) is attached
        /// to the object within the document.
        /// </summary>
        [DOM("backgroundAttachment")]
        public String BackgroundAttachment
        {
            get { return GetPropertyValue("background-attachment") ?? String.Empty; }
            set { SetProperty("background-attachment", value); }
        }

        /// <summary>
        /// Gets or sets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DOM("backgroundClip")]
        public String BackgroundClip
        {
            get { return GetPropertyValue("background-clip") ?? String.Empty; }
            set { SetProperty("background-clip", value); }
        }

        /// <summary>
        /// Gets or sets the color behind the content of the object.
        /// </summary>
        [DOM("backgroundColor")]
        public String BackgroundColor
        {
            get { return GetPropertyValue("background-color") ?? String.Empty; }
            set { SetProperty("background-color", value); }
        }

        /// <summary>
        /// Gets or sets the background image or images of the object.
        /// </summary>
        [DOM("backgroundImage")]
        public String BackgroundImage
        {
            get { return GetPropertyValue("background-image") ?? String.Empty; }
            set { SetProperty("background-image", value); }
        }

        /// <summary>
        /// Gets or sets the positioning area of an element or multiple elements.
        /// </summary>
        [DOM("backgroundOrigin")]
        public String BackgroundOrigin
        {
            get { return GetPropertyValue("background-origin") ?? String.Empty; }
            set { SetProperty("background-origin", value); }
        }

        /// <summary>
        /// Gets or sets the position of the background of the object.
        /// </summary>
        [DOM("backgroundPosition")]
        public String BackgroundPosition
        {
            get { return GetPropertyValue("background-position") ?? String.Empty; }
            set { SetProperty("background-position", value); }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the background-position property.
        /// </summary>
        [DOM("backgroundPositionX")]
        public String BackgroundPositionX
        {
            get { return GetPropertyValue("background-position-x") ?? String.Empty; }
            set { SetProperty("background-position-x", value); }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the background-position property.
        /// </summary>
        [DOM("backgroundPositionY")]
        public String BackgroundPositionY
        {
            get { return GetPropertyValue("background-position-y") ?? String.Empty; }
            set { SetProperty("background-position-y", value); }
        }

        /// <summary>
        /// Gets or sets the size of the background images.
        /// </summary>
        [DOM("backgroundSize")]
        public String BackgroundSize
        {
            get { return GetPropertyValue("background-size") ?? String.Empty; }
            set { SetProperty("background-size", value); }
        }

        /// <summary>
        /// Gets or sets whether and how the background image (or images) is tiled.
        /// </summary>
        [DOM("backgroundRepeat")]
        public String BackgroundRepeat
        {
            get { return GetPropertyValue("background-repeat") ?? String.Empty; }
            set { SetProperty("background-repeat", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DOM("baselineShift")]
        public String BaselineShift
        {
            get { return GetPropertyValue("baseline-shift") ?? String.Empty; }
            set { SetProperty("baseline-shift", value); }
        }

        /// <summary>
        /// Gets or sets the location of the Dynamic HTML (DHTML) behaviorDHTML Behaviors.
        /// </summary>
        [DOM("behavior")]
        public String Behavior
        {
            get { return GetPropertyValue("behavior") ?? String.Empty; }
            set { SetProperty("behavior", value); }
        }

        /// <summary>
        /// Gets or sets the properties of a border drawn around an object.
        /// </summary>
        [DOM("border")]
        public String Border
        {
            get { return GetPropertyValue("border") ?? String.Empty; }
            set { SetProperty("border", value); }
        }

        /// <summary>
        /// Gets or sets the properties of the bottom border of the object.
        /// </summary>
        [DOM("borderBottom")]
        public String BorderBottom
        {
            get { return GetPropertyValue("border-bottom") ?? String.Empty; }
            set { SetProperty("border-bottom", value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the bottom border of an object.
        /// </summary>
        [DOM("borderBottomColor")]
        public String BorderBottomColor
        {
            get { return GetPropertyValue("border-bottom-color") ?? String.Empty; }
            set { SetProperty("border-bottom-color", value); }
        }

        /// <summary>
        /// Gets or sets the radii of the quarter ellipse that defines
        /// the shape of the lower-left corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderBottomLeftRadius")]
        public String BorderBottomLeftRadius
        {
            get { return GetPropertyValue("border-bottom-left-radius") ?? String.Empty; }
            set { SetProperty("border-bottom-left-radius", value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the lower-right corner
        /// for the outer border edge of the current box.
        /// </summary>
        [DOM("borderBottomRightRadius")]
        public String BorderBottomRightRadius
        {
            get { return GetPropertyValue("border-bottom-right-radius") ?? String.Empty; }
            set { SetProperty("border-bottom-radius", value); }
        }

        /// <summary>
        /// Gets or sets the style of the bottom border of the object.
        /// </summary>
        [DOM("borderBottomStyle")]
        public String BorderBottomStyle
        {
            get { return GetPropertyValue("border-bottom-style") ?? String.Empty; }
            set { SetProperty("border-bottom-style", value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the bottom border of the object.
        /// </summary>
        [DOM("borderBottomWidth")]
        public String BorderBottomWidth
        {
            get { return GetPropertyValue("border-bottom-width") ?? String.Empty; }
            set { SetProperty("border-bottom-width", value); }
        }

        /// <summary>
        /// Gets or sets whether the row and cell borders of a table are joined in a
        /// single border or detached as in standard HTML.
        /// </summary>
        [DOM("borderCollapse")]
        public String BorderCollapse
        {
            get { return GetPropertyValue("border-collapse") ?? String.Empty; }
            set { SetProperty("border-collapse", value); }
        }

        /// <summary>
        /// Gets or sets the border color of the object.
        /// </summary>
        [DOM("borderColor")]
        public String BorderColor
        {
            get { return GetPropertyValue("border-color") ?? String.Empty; }
            set { SetProperty("border-color", value); }
        }

        /// <summary>
        /// Gets or sets an image to be used in place of the border styles.
        /// </summary>
        [DOM("borderImage")]
        public String BorderImage
        {
            get { return GetPropertyValue("border-image") ?? String.Empty; }
            set { SetProperty("border-image", value); }
        }

        /// <summary>
        /// Gets or sets the amount by which the border image area extends beyond the border box.
        /// </summary>
        [DOM("borderImageOutset")]
        public String BorderImageOutset
        {
            get { return GetPropertyValue("border-image-outset") ?? String.Empty; }
            set { SetProperty("border-image-outset", value); }
        }

        /// <summary>
        /// Gets or sets ow the image is scaled and tiled.
        /// </summary>
        [DOM("borderImageRepeat")]
        public String BorderImageRepeat
        {
            get { return GetPropertyValue("border-image-repeat") ?? String.Empty; }
            set { SetProperty("border-image-repeat", value); }
        }

        /// <summary>
        /// Gets or sets four inward offsets, this property slices the specified
        /// border image into a three by three grid: four corners, four edges, and a central region.
        /// </summary>
        [DOM("borderImageSlice")]
        public String BorderImageSlice
        {
            get { return GetPropertyValue("border-image-slice") ?? String.Empty; }
            set { SetProperty("border-image-slice", value); }
        }

        /// <summary>
        /// Gets or sets the path of the image to be used for the border.
        /// </summary>
        [DOM("borderImageSource")]
        public String BorderImageSource
        {
            get { return GetPropertyValue("border-image-source") ?? String.Empty; }
            set { SetProperty("border-image-source", value); }
        }

        /// <summary>
        /// Gets or sets the inward offsets from the outer border edge.
        /// </summary>
        [DOM("borderImageWidth")]
        public String BorderImageWidth
        {
            get { return GetPropertyValue("border-image-width") ?? String.Empty; }
            set { SetProperty("border-image-width", value); }
        }

        /// <summary>
        /// Gets or sets the properties of the left border of the object.
        /// </summary>
        [DOM("borderLeft")]
        public String BorderLeft
        {
            get { return GetPropertyValue("border-left") ?? String.Empty; }
            set { SetProperty("border-left", value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the left border of an object.
        /// </summary>
        [DOM("borderLeftColor")]
        public String BorderLeftColor
        {
            get { return GetPropertyValue("border-left-color") ?? String.Empty; }
            set { SetProperty("border-left-color", value); }
        }

        /// <summary>
        /// Gets or sets the style of the left border of the object.
        /// </summary>
        [DOM("borderLeftStyle")]
        public String BorderLeftStyle
        {
            get { return GetPropertyValue("border-left-style") ?? String.Empty; }
            set { SetProperty("border-left-style", value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the left border of the object.
        /// </summary>
        [DOM("borderLeftWidth")]
        public String BorderLeftWidth
        {
            get { return GetPropertyValue("border-left-width") ?? String.Empty; }
            set { SetProperty("border-left-width", value); }
        }

        /// <summary>
        /// Gets or sets the radii of a quarter ellipse that defines the shape of
        /// the corners for the outer border edge of the current box.
        /// </summary>
        [DOM("borderRadius")]
        public String BorderRadius
        {
            get { return GetPropertyValue("border-radius") ?? String.Empty; }
            set { SetProperty("border-radius", value); }
        }

        /// <summary>
        /// Gets or sets the properties of the right border of the object.
        /// </summary>
        [DOM("borderRight")]
        public String BorderRight
        {
            get { return GetPropertyValue("border-right") ?? String.Empty; }
            set { SetProperty("border-right", value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the right border of an object.
        /// </summary>
        [DOM("borderRightColor")]
        public String BorderRightColor
        {
            get { return GetPropertyValue("border-right-color") ?? String.Empty; }
            set { SetProperty("border-right-color", value); }
        }

        /// <summary>
        /// Gets or sets the style of the right border of the object.
        /// </summary>
        [DOM("borderRightStyle")]
        public String BorderRightStyle
        {
            get { return GetPropertyValue("border-right-style") ?? String.Empty; }
            set { SetProperty("border-right-style", value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the right border of the object.
        /// </summary>
        [DOM("borderRightWidth")]
        public String BorderRightWidth
        {
            get { return GetPropertyValue("border-right-width") ?? String.Empty; }
            set { SetProperty("border-right-width", value); }
        }

        /// <summary>
        /// Gets or sets the distance between the borders of adjoining cells in a table.
        /// </summary>
        [DOM("borderSpacing")]
        public String BorderSpacing
        {
            get { return GetPropertyValue("border-spacing") ?? String.Empty; }
            set { SetProperty("border-spacing", value); }
        }

        /// <summary>
        /// Gets or sets the style of the left, right, top, and bottom borders of the object.
        /// </summary>
        [DOM("borderStyle")]
        public String BorderStyle
        {
            get { return GetPropertyValue("border-style") ?? String.Empty; }
            set { SetProperty("border-style", value); }
        }

        /// <summary>
        /// Gets or sets the properties of the top border of the object.
        /// </summary>
        [DOM("borderTop")]
        public String BorderTop
        {
            get { return GetPropertyValue("border-top") ?? String.Empty; }
            set { SetProperty("border-top", value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the top border of an object.
        /// </summary>
        [DOM("borderTopColor")]
        public String BorderTopColor
        {
            get { return GetPropertyValue("border-top-color") ?? String.Empty; }
            set { SetProperty("border-top-color", value); }
        }

        /// <summary>
        /// Gets or sets  one or two values that define the radii of the quarter ellipse
        /// that defines the shape of the upper-left corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderTopLeftRadius")]
        public String BorderTopLeftRadius
        {
            get { return GetPropertyValue("border-top-left-radius") ?? String.Empty; }
            set { SetProperty("border-top-left-radius", value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-right
        /// corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderTopRightRadius")]
        public String BorderTopRightRadius
        {
            get { return GetPropertyValue("border-top-right-radius") ?? String.Empty; }
            set { SetProperty("border-top-right-radius", value); }
        }

        /// <summary>
        /// Gets or sets  the style of the top border of the object.
        /// </summary>
        [DOM("borderTopStyle")]
        public String BorderTopStyle
        {
            get { return GetPropertyValue("border-top-style") ?? String.Empty; }
            set { SetProperty("border-top-style", value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the top border of the object.
        /// </summary>
        [DOM("borderTopWidth")]
        public String BorderTopWidth
        {
            get { return GetPropertyValue("border-top-width") ?? String.Empty; }
            set { SetProperty("border-top-width", value); }
        }

        /// <summary>
        /// Gets or sets the thicknesses of the left, right, top, and bottom borders of an object.
        /// </summary>
        [DOM("borderWidth")]
        public String BorderWidth
        {
            get { return GetPropertyValue("border-width") ?? String.Empty; }
            set { SetProperty("border-width", value); }
        }

        /// <summary>
        /// Gets or sets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DOM("boxShadow")]
        public String BoxShadow
        {
            get { return GetPropertyValue("box-shadow") ?? String.Empty; }
            set { SetProperty("box-shadow", value); }
        }

        /// <summary>
        /// Gets or sets the box model to use for object sizing.
        /// </summary>
        [DOM("boxSizing")]
        public String BoxSizing
        {
            get { return GetPropertyValue("box-sizing") ?? String.Empty; }
            set { SetProperty("box-sizing", value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that follows a content
        /// block in a multi-column element.
        /// </summary>
        [DOM("breakAfter")]
        public String BreakAfter
        {
            get { return GetPropertyValue("break-after") ?? String.Empty; }
            set { SetProperty("break-after", value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DOM("breakBefore")]
        public String BreakBefore
        {
            get { return GetPropertyValue("break-before") ?? String.Empty; }
            set { SetProperty("break-before", value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DOM("breakInside")]
        public String BreakInside
        {
            get { return GetPropertyValue("break-inside") ?? String.Empty; }
            set { SetProperty("break-inside", value); }
        }

        /// <summary>
        /// Gets or sets where the caption of a table is located.
        /// </summary>
        [DOM("captionSide")]
        public String CaptionSide
        {
            get { return GetPropertyValue("caption-side") ?? String.Empty; }
            set { SetProperty("caption-side", value); }
        }

        /// <summary>
        /// Gets or sets whether the object allows floating objects on its left side,
        /// right side, or both, so that the next text displays past the floating objects.
        /// </summary>
        [DOM("clear")]
        public String Clear
        {
            get { return GetPropertyValue("clear") ?? String.Empty; }
            set { SetProperty("clear", value); }
        }

        /// <summary>
        /// Gets or sets which part of a positioned object is visible.
        /// </summary>
        [DOM("clip")]
        public String Clip
        {
            get { return GetPropertyValue("clip") ?? String.Empty; }
            set { SetProperty("clip", value); }
        }

        /// <summary>
        /// Gets or sets the bottom coordinate of the object clipping region.
        /// </summary>
        [DOM("clipBottom")]
        public String ClipBottom
        {
            get { return GetPropertyValue("clip-bottom") ?? String.Empty; }
            set { SetProperty("clip-bottom", value); }
        }

        /// <summary>
        /// Gets or sets the left coordinate of the object clipping region.
        /// </summary>
        [DOM("clipLeft")]
        public String ClipLeft
        {
            get { return GetPropertyValue("clip-left") ?? String.Empty; }
            set { SetProperty("clip-left", value); }
        }

        /// <summary>
        /// Gets or sets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DOM("clipPath")]
        public String ClipPath
        {
            get { return GetPropertyValue("clip-path") ?? String.Empty; }
            set { SetProperty("clip-path", value); }
        }

        /// <summary>
        /// Gets or sets the right coordinate of the object clipping region.
        /// </summary>
        [DOM("clipRight")]
        public String ClipRight
        {
            get { return GetPropertyValue("clip-right") ?? String.Empty; }
            set { SetProperty("clip-right", value); }
        }

        /// <summary>
        /// Gets or sets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DOM("clipRule")]
        public String ClipRule
        {
            get { return GetPropertyValue("clip-rule") ?? String.Empty; }
            set { SetProperty("clip-rule", value); }
        }

        /// <summary>
        /// Gets or sets the top coordinate of the object clipping region.
        /// </summary>
        [DOM("clipTop")]
        public String ClipTop
        {
            get { return GetPropertyValue("clip-top") ?? String.Empty; }
            set { SetProperty("clip-top", value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the text of an object.
        /// </summary>
        [DOM("color")]
        public String Color
        {
            get { return GetPropertyValue("color") ?? String.Empty; }
            set { SetProperty("color", value); }
        }

        /// <summary>
        /// Gets or sets which color space to use for filter effects.
        /// </summary>
        [DOM("colorInterpolationFilters")]
        public String ColorInterpolationFilters
        {
            get { return GetPropertyValue("color-interpolation-filters") ?? String.Empty; }
            set { SetProperty("color-interpolation-filters", value); }
        }

        /// <summary>
        /// Gets or sets the optimal number of columns in a multi-column element.
        /// </summary>
        [DOM("columnCount")]
        public String ColumnCount
        {
            get { return GetPropertyValue("column-count") ?? String.Empty; }
            set { SetProperty("column-count", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DOM("columnFill")]
        public String ColumnFill
        {
            get { return GetPropertyValue("column-fill") ?? String.Empty; }
            set { SetProperty("column-fill", value); }
        }

        /// <summary>
        /// Gets or sets the width of the gap between columns in a multi-column element.
        /// </summary>
        [DOM("columnGap")]
        public String ColumnGap
        {
            get { return GetPropertyValue("column-gap") ?? String.Empty; }
            set { SetProperty("column-gap", value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value  that specifies values for the columnRuleWidth, 
        /// columnRuleStyle, and the columnRuleColor of a multi-column element.
        /// </summary>
        [DOM("columnRule")]
        public String ColumnRule
        {
            get { return GetPropertyValue("column-rule") ?? String.Empty; }
            set { SetProperty("column-rule", value); }
        }

        /// <summary>
        /// Gets or sets the color for all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleColor")]
        public String ColumnRuleColor
        {
            get { return GetPropertyValue("column-rule-color") ?? String.Empty; }
            set { SetProperty("column-rule-color", value); }
        }

        /// <summary>
        /// Gets or sets the style for all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleStyle")]
        public String ColumnRuleStyle
        {
            get { return GetPropertyValue("column-rule-style") ?? String.Empty; }
            set { SetProperty("column-rule-style", value); }
        }

        /// <summary>
        /// Gets or sets the width of all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleWidth")]
        public String ColumnRuleWidth
        {
            get { return GetPropertyValue("column-rule-width") ?? String.Empty; }
            set { SetProperty("column-rule-width", value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value that specifies values for the column-width
        /// and the column-count of a multi-column element.
        /// </summary>
        [DOM("columns")]
        public String Columns
        {
            get { return GetPropertyValue("columns") ?? String.Empty; }
            set { SetProperty("columns", value); }
        }

        /// <summary>
        /// Gets or sets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DOM("columnSpan")]
        public String ColumnSpan
        {
            get { return GetPropertyValue("column-span") ?? String.Empty; }
            set { SetProperty("column-span", value); }
        }

        /// <summary>
        /// Gets or sets the optimal width of the columns in a multi-column element.
        /// </summary>
        [DOM("columnWidth")]
        public String ColumnWidth
        {
            get { return GetPropertyValue("column-width") ?? String.Empty; }
            set { SetProperty("column-width", value); }
        }

        /// <summary>
        /// Gets or sets generated content to insert before or after an element.
        /// </summary>
        [DOM("content")]
        public String Content
        {
            get { return GetPropertyValue("content") ?? String.Empty; }
            set { SetProperty("content", value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to increment.
        /// </summary>
        [DOM("counterIncrement")]
        public String CounterIncrement
        {
            get { return GetPropertyValue("counter-increment") ?? String.Empty; }
            set { SetProperty("counter-increment", value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to create or reset to zero.
        /// </summary>
        [DOM("counterReset")]
        public String CounterReset
        {
            get { return GetPropertyValue("counter-reset") ?? String.Empty; }
            set { SetProperty("counter-reset", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether a box should float
        /// to the left, right, or not at all.
        /// </summary>
        [DOM("cssFloat")]
        public String CssFloat
        {
            get { return GetPropertyValue("css-float") ?? String.Empty; }
            set { SetProperty("css-float", value); }
        }

        /// <summary>
        /// Gets or sets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DOM("cursor")]
        public String Cursor
        {
            get { return GetPropertyValue("cursor") ?? String.Empty; }
            set { SetProperty("cursor", value); }
        }

        /// <summary>
        /// Gets or sets the reading order of the object.
        /// </summary>
        [DOM("direction")]
        public String Direction
        {
            get { return GetPropertyValue("direction") ?? String.Empty; }
            set { SetProperty("direction", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether and how the object is rendered.
        /// </summary>
        [DOM("display")]
        public String Display
        {
            get { return GetPropertyValue("display") ?? String.Empty; }
            set { SetProperty("display", value); }
        }

        /// <summary>
        /// Gets or sets a value that determines or redetermines a scaled-baseline table.
        /// </summary>
        [DOM("dominantBaseline")]
        public String DominantBaseline
        {
            get { return GetPropertyValue("dominant-baseline") ?? String.Empty; }
            set { SetProperty("dominant-baseline", value); }
        }

        /// <summary>
        /// Determines whether to show or hide a cell without content.
        /// </summary>
        [DOM("emptyCells")]
        public String EmptyCells
        {
            get { return GetPropertyValue("empty-cells") ?? String.Empty; }
            set { SetProperty("empty-cells", value); }
        }

        /// <summary>
        /// Allocate a shared background image all graphic elements within a container.
        /// </summary>
        [DOM("enableBackground")]
        public String EnableBackground
        {
            get { return GetPropertyValue("enable-background") ?? String.Empty; }
            set { SetProperty("enable-background", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DOM("fill")]
        public String Fill
        {
            get { return GetPropertyValue("fill") ?? String.Empty; }
            set { SetProperty("fill", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation that is used to paint the interior of the current object.
        /// </summary>
        [DOM("fillOpacity")]
        public String FillOpacity
        {
            get { return GetPropertyValue("fill-opacity") ?? String.Empty; }
            set { SetProperty("fill-opacity", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the algorithm that is to be used to determine
        /// what parts of the canvas are included inside the shape.
        /// </summary>
        [DOM("fillRule")]
        public String FillRule
        {
            get { return GetPropertyValue("fill-rule") ?? String.Empty; }
            set { SetProperty("fill-rule", value); }
        }

        /// <summary>
        /// The filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DOM("filter")]
        public String Filter
        {
            get { return GetPropertyValue("filter") ?? String.Empty; }
            set { SetProperty("filter", value); }
        }

        /// <summary>
        /// Gets or sets the parameter values of a flexible length, the positive and
        /// negative flexibility, and the preferred size.
        /// </summary>
        [DOM("flex")]
        public String Flex
        {
            get { return GetPropertyValue("flex") ?? String.Empty; }
            set { SetProperty("flex", value); }
        }

        /// <summary>
        /// Gets or sets the initial main size of the flex item.
        /// </summary>
        [DOM("flexBasis")]
        public String FlexBasis
        {
            get { return GetPropertyValue("flex-basis") ?? String.Empty; }
            set { SetProperty("flex-basis", value); }
        }

        /// <summary>
        /// Gets or sets the direction of the main axis which specifies how
        /// the flex items are displayed in the flex container.
        /// </summary>
        [DOM("flexDirection")]
        public String FlexDirection
        {
            get { return GetPropertyValue("flex-direction") ?? String.Empty; }
            set { SetProperty("flex-direction", value); }
        }

        /// <summary>
        /// Gets or sets the shorthand property to set both the flex-direction and flex-wrap
        /// properties of a flex container.
        /// </summary>
        [DOM("flexFlow")]
        public String FlexFlow
        {
            get { return GetPropertyValue("flex-flow") ?? String.Empty; }
            set { SetProperty("flex-flow", value); }
        }

        /// <summary>
        /// Gets or sets the flex grow factor for the flex item.
        /// </summary>
        [DOM("flexGrow")]
        public String FlexGrow
        {
            get { return GetPropertyValue("flex-grow") ?? String.Empty; }
            set { SetProperty("flex-grow", value); }
        }

        /// <summary>
        /// Gets or sets the flex shrink factor for the flex item.
        /// </summary>
        [DOM("flexShrink")]
        public String FlexShrink
        {
            get { return GetPropertyValue("flex-shrink") ?? String.Empty; }
            set { SetProperty("flex-shrink", value); }
        }

        /// <summary>
        /// Gets or sets whether flex items wrap and the direction they
        /// wrap onto multiple lines or columns based on the spac
        /// available in the flex container. 
        /// </summary>
        [DOM("flexWrap")]
        public String FlexWrap
        {
            get { return GetPropertyValue("flex-wrap") ?? String.Empty; }
            set { SetProperty("flex-wrap", value); }
        }

        /// <summary>
        /// Gets or sets the color used to flood the current filter-primitive subregion.
        /// </summary>
        [DOM("floodColor")]
        public String FloodColor
        {
            get { return GetPropertyValue("flood-color") ?? String.Empty; }
            set { SetProperty("flood-color", value); }
        }

        /// <summary>
        /// Gets or sets the opacity value to use with feFlood elements.
        /// </summary>
        [DOM("floodOpacity")]
        public String FloodOpacity
        {
            get { return GetPropertyValue("flood-opacity") ?? String.Empty; }
            set { SetProperty("flood-opacity", value); }
        }

        /// <summary>
        /// Gets or sets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of
        /// six user-preference fonts.
        /// </summary>
        [DOM("font")]
        public String Font
        {
            get { return GetPropertyValue("font") ?? String.Empty; }
            set { SetProperty("font", value); }
        }

        /// <summary>
        /// Gets or sets the name of the font used for text in the object.
        /// </summary>
        [DOM("fontFamily")]
        public String FontFamily
        {
            get { return GetPropertyValue("font-family") ?? String.Empty; }
            set { SetProperty("font-family", value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DOM("fontFeatureSettings")]
        public String FontFeatureSettings
        {
            get { return GetPropertyValue("font-feature-settings") ?? String.Empty; }
            set { SetProperty("font-feature-settings", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the font size used for text in the object.
        /// </summary>
        [DOM("fontSize")]
        public String FontSize
        {
            get { return GetPropertyValue("font-size") ?? String.Empty; }
            set { SetProperty("font-size", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies an aspect value for an element that
        /// will effectively preserve the x-height of the first choice font, whether
        /// it is substituted or not.
        /// </summary>
        [DOM("fontSizeAdjust")]
        public String FontSizeAdjust
        {
            get { return GetPropertyValue("font-size-adjust") ?? String.Empty; }
            set { SetProperty("font-size-adjust", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a normal, condensed,
        /// or expanded face of a font family.
        /// </summary>
        [DOM("fontStretch")]
        public String FontStretch
        {
            get { return GetPropertyValue("font-stretch") ?? String.Empty; }
            set { SetProperty("font-stretch", value); }
        }

        /// <summary>
        /// Gets or sets the font style of the object as italic, normal, or oblique.
        /// </summary>
        [DOM("fontStyle")]
        public String FontStyle
        {
            get { return GetPropertyValue("font-style") ?? String.Empty; }
            set { SetProperty("font-style", value); }
        }

        /// <summary>
        /// Gets or sets whether the text of the object is in small capital letters.
        /// </summary>
        [DOM("fontVariant")]
        public String FontVariant
        {
            get { return GetPropertyValue("font-variant") ?? String.Empty; }
            set { SetProperty("font-variant", value); }
        }

        /// <summary>
        /// Gets of sets the weight of the font of the object.
        /// </summary>
        [DOM("fontWeight")]
        public String FontWeight
        {
            get { return GetPropertyValue("font-weight") ?? String.Empty; }
            set { SetProperty("font-weight", value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence of characters relative to an inline-progression-direction  of horizontal.
        /// </summary>
        [DOM("glyphOrientationHorizontal")]
        public String GlyphOrientationHorizontal
        {
            get { return GetPropertyValue("glyph-orientation-horizontal") ?? String.Empty; }
            set { SetProperty("glyph-orientation-horizontal", value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of vertical.
        /// </summary>
        [DOM("glyphOrientationVertical")]
        public String GlyphOrientationVertical
        {
            get { return GetPropertyValue("glyph-orientation-vertical") ?? String.Empty; }
            set { SetProperty("glyph-orientation-vertical", value); }
        }

        /// <summary>
        /// Gets or sets the height of the object.
        /// </summary>
        [DOM("height")]
        public String Height
        {
            get { return GetPropertyValue("height") ?? String.Empty; }
            set { SetProperty("height", value); }
        }

        /// <summary>
        /// Gets or sets the state of an IME.
        /// </summary>
        [DOM("imeMode")]
        public String ImeMode
        {
            get { return GetPropertyValue("ime-mode") ?? String.Empty; }
            set { SetProperty("ime-mode", value); }
        }

        /// <summary>
        /// Gets or sets a how flex items are aligned along the main axis of the flex
        /// container after any flexible lengths and auto margins are resolved.
        /// </summary>
        [DOM("justifyContent")]
        public String JustifyContent
        {
            get { return GetPropertyValue("justify-content") ?? String.Empty; }
            set { SetProperty("justify-content", value); }
        }

        /// <summary>
        /// Gets or sets the composite document grid properties
        /// that specify the layout of text characters.
        /// </summary>
        [DOM("layoutGrid")]
        public String LayoutGrid
        {
            get { return GetPropertyValue("layout-grid") ?? String.Empty; }
            set { SetProperty("layout-grid", value); }
        }

        /// <summary>
        /// Gets or sets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DOM("layoutGridChar")]
        public String LayoutGridChar
        {
            get { return GetPropertyValue("layout-grid-char") ?? String.Empty; }
            set { SetProperty("layout-grid-char", value); }
        }

        /// <summary>
        /// Gets or sets the gridline value used for rendering the
        /// text content of an element.
        /// </summary>
        [DOM("layoutGridLine")]
        public String LayoutGridLine
        {
            get { return GetPropertyValue("layout-grid-line") ?? String.Empty; }
            set { SetProperty("layout-grid-line", value); }
        }

        /// <summary>
        /// Gets or sets whether the text layout grid uses two dimensions.
        /// </summary>
        [DOM("layoutGridMode")]
        public String LayoutGridMode
        {
            get { return GetPropertyValue("layout-grid-mode") ?? String.Empty; }
            set { SetProperty("layout-grid-mode", value); }
        }

        /// <summary>
        /// Gets or sets the type of grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DOM("layoutGridType")]
        public String LayoutGridType
        {
            get { return GetPropertyValue("layout-grid-type") ?? String.Empty; }
            set { SetProperty("layout-grid-type", value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("left")]
        public String Left
        {
            get { return GetPropertyValue("left") ?? String.Empty; }
            set { SetProperty("left", value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between letters in the object.
        /// </summary>
        [DOM("letterSpacing")]
        public String LetterSpacing
        {
            get { return GetPropertyValue("letter-spacing") ?? String.Empty; }
            set { SetProperty("letter-spacing", value); }
        }

        /// <summary>
        /// Defines the color of the light source for filter
        /// primitives feDiffuseLighting and feSpecularLighting.
        /// </summary>
        [DOM("lightingColor")]
        public String LightingColor
        {
            get { return GetPropertyValue("lighting-color") ?? String.Empty; }
            set { SetProperty("lighting-color", value); }
        }

        /// <summary>
        /// Gets or sets the distance between lines in the object.
        /// </summary>
        [DOM("lineHeight")]
        public String LineHeight
        {
            get { return GetPropertyValue("line-height") ?? String.Empty; }
            set { SetProperty("line-height", value); }
        }

        /// <summary>
        /// Gets or sets up to three separate list-style properties of the object.
        /// </summary>
        [DOM("listStyle")]
        public String ListStyle
        {
            get { return GetPropertyValue("list-style") ?? String.Empty; }
            set { SetProperty("list-style", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates which image to use as
        /// a list-item marker for the object.
        /// </summary>
        [DOM("listStyleImage")]
        public String ListStyleImage
        {
            get { return GetPropertyValue("list-style-image") ?? String.Empty; }
            set { SetProperty("list-style-image", value); }
        }

        /// <summary>
        /// Gets or sets a variable that indicates how the list-item marker
        /// is drawn relative to the content of the object.
        /// </summary>
        [DOM("listStylePosition")]
        public String ListStylePosition
        {
            get { return GetPropertyValue("list-style-position") ?? String.Empty; }
            set { SetProperty("list-style-position", value); }
        }

        /// <summary>
        /// Gets or sets the predefined type of the line-item marker for the object.
        /// </summary>
        [DOM("listStyleType")]
        public String ListStyleType
        {
            get { return GetPropertyValue("list-style-type") ?? String.Empty; }
            set { SetProperty("list-style-type", value); }
        }

        /// <summary>
        /// Gets or sets the width of the top, right, bottom, and left margins of the object.
        /// </summary>
        [DOM("margin")]
        public String Margin
        {
            get { return GetPropertyValue("margin") ?? String.Empty; }
            set { SetProperty("margin", value); }
        }

        /// <summary>
        /// Gets or sets the height of the bottom margin of the object.
        /// </summary>
        [DOM("marginBottom")]
        public String MarginBottom
        {
            get { return GetPropertyValue("margin-bottom") ?? String.Empty; }
            set { SetProperty("margin-bottom", value); }
        }

        /// <summary>
        /// Gets or sets the width of the left margin of the object.
        /// </summary>
        [DOM("marginLeft")]
        public String MarginLeft
        {
            get { return GetPropertyValue("margin-left") ?? String.Empty; }
            set { SetProperty("margin-left", value); }
        }

        /// <summary>
        /// Gets or sets the width of the right margin of the object.
        /// </summary>
        [DOM("marginRight")]
        public String MarginRight
        {
            get { return GetPropertyValue("margin-right") ?? String.Empty; }
            set { SetProperty("margin-right", value); }
        }

        /// <summary>
        /// Gets or sets the height of the top margin of the object.
        /// </summary>
        [DOM("marginTop")]
        public String MarginTop
        {
            get { return GetPropertyValue("margin-top") ?? String.Empty; }
            set { SetProperty("margin-top", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DOM("marker")]
        public String Marker
        {
            get { return GetPropertyValue("marker") ?? String.Empty; }
            set { SetProperty("marker", value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the final vertex of a given path element or
        /// basic shape.
        /// </summary>
        [DOM("markerEnd")]
        public String MarkerEnd
        {
            get { return GetPropertyValue("marker-end") ?? String.Empty; }
            set { SetProperty("marker-end", value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DOM("markerMid")]
        public String MarkerMid
        {
            get { return GetPropertyValue("marker-mid") ?? String.Empty; }
            set { SetProperty("marker-mid", value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the first vertex of a given path element or
        /// basic shape.
        /// </summary>
        [DOM("markerStart")]
        public String MarkerStart
        {
            get { return GetPropertyValue("marker-start") ?? String.Empty; }
            set { SetProperty("marker-start", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a SVG mask.
        /// </summary>
        [DOM("mask")]
        public String Mask
        {
            get { return GetPropertyValue("mask") ?? String.Empty; }
            set { SetProperty("mask", value); }
        }

        /// <summary>
        /// Gets or sets the maximum height for an element.
        /// </summary>
        [DOM("maxHeight")]
        public String MaxHeight
        {
            get { return GetPropertyValue("max-height") ?? String.Empty; }
            set { SetProperty("max-height", value); }
        }

        /// <summary>
        /// Gets or sets the maximum width for an element.
        /// </summary>
        [DOM("maxWidth")]
        public String MaxWidth
        {
            get { return GetPropertyValue("max-width") ?? String.Empty; }
            set { SetProperty("max-width", value); }
        }

        /// <summary>
        /// Gets or sets the minimum height for an element.
        /// </summary>
        [DOM("minHeight")]
        public String MinHeight
        {
            get { return GetPropertyValue("min-height") ?? String.Empty; }
            set { SetProperty("min-height", value); }
        }

        /// <summary>
        /// Gets or sets the minimum width for an element.
        /// </summary>
        [DOM("minWidth")]
        public String MinWidth
        {
            get { return GetPropertyValue("min-width") ?? String.Empty; }
            set { SetProperty("min-width", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies object or group opacity in CSS or SVG.
        /// </summary>
        [DOM("opacity")]
        public String Opacity
        {
            get { return GetPropertyValue("opacity") ?? String.Empty; }
            set { SetProperty("opacity", value); }
        }

        /// <summary>
        /// Gets or sets the order in which a flex item
        /// within a flex container is displayed.
        /// </summary>
        [DOM("order")]
        public String Order
        {
            get { return GetPropertyValue("order") ?? String.Empty; }
            set { SetProperty("order", value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph
        /// that must appear at the bottom of a page.
        /// </summary>
        [DOM("orphans")]
        public String Orphans
        {
            get { return GetPropertyValue("orphans") ?? String.Empty; }
            set { SetProperty("orphans", value); }
        }

        /// <summary>
        /// Gets or sets the style of the outline frame.
        /// </summary>
        [DOM("outlineStyle")]
        public String OutlineStyle
        {
            get { return GetPropertyValue("outline-style") ?? String.Empty; }
            set { SetProperty("outline-style", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DOM("overflow")]
        public String Overflow
        {
            get { return GetPropertyValue("overflow") ?? String.Empty; }
            set { SetProperty("overflow", value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DOM("overflowX")]
        public String OverflowX
        {
            get { return GetPropertyValue("overflow-x") ?? String.Empty; }
            set { SetProperty("overflow-x", value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when
        /// the content exceeds the height of the object.
        /// </summary>
        [DOM("overflowY")]
        public String OverflowY
        {
            get { return GetPropertyValue("overflow-y") ?? String.Empty; }
            set { SetProperty("overflow-y", value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its border.
        /// </summary>
        [DOM("padding")]
        public String Padding
        {
            get { return GetPropertyValue("padding") ?? String.Empty; }
            set { SetProperty("padding", value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingBottom")]
        public String PaddingBottom
        {
            get { return GetPropertyValue("padding-bottom") ?? String.Empty; }
            set { SetProperty("padding-bottom", value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingLeft")]
        public String PaddingLeft
        {
            get { return GetPropertyValue("padding-left") ?? String.Empty; }
            set { SetProperty("padding-left", value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between
        /// the right border of the object and the content.
        /// </summary>
        [DOM("paddingRight")]
        public String PaddingRight
        {
            get { return GetPropertyValue("padding-right") ?? String.Empty; }
            set { SetProperty("padding-right", value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the top
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingTop")]
        public String PaddingTop
        {
            get { return GetPropertyValue("padding-top") ?? String.Empty; }
            set { SetProperty("padding-top", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a page break occurs after the object.
        /// </summary>
        [DOM("pageBreakAfter")]
        public String PageBreakAfter
        {
            get { return GetPropertyValue("page-break-after") ?? String.Empty; }
            set { SetProperty("page-break-after", value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break occurs before the object.
        /// </summary>
        [DOM("pageBreakBefore")]
        public String PageBreakBefore
        {
            get { return GetPropertyValue("page-break-before") ?? String.Empty; }
            set { SetProperty("page-break-before", value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break is
        /// allowed to occur inside the object.
        /// </summary>
        [DOM("pageBreakInside")]
        public String PageBreakInside
        {
            get { return GetPropertyValue("page-break-inside") ?? String.Empty; }
            set { SetProperty("page-break-inside", value); }
        }

        /// <summary>
        /// Gets or sets a value that represents the perspective from which all child elements of the object are viewed.
        /// </summary>
        [DOM("perspective")]
        public String Perspective
        {
            get { return GetPropertyValue("perspective") ?? String.Empty; }
            set { SetProperty("perspective", value); }
        }

        /// <summary>
        /// Gets or sets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DOM("perspectiveOrigin")]
        public String PerspectiveOrigin
        {
            get { return GetPropertyValue("perspective-origin") ?? String.Empty; }
            set { SetProperty("perspective-origin", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies under what circumstances a given graphics
        /// element can be the target element for a pointer event in SVG.
        /// </summary>
        [DOM("pointerEvents")]
        public String PointerEvents
        {
            get { return GetPropertyValue("pointer-events") ?? String.Empty; }
            set { SetProperty("pointer-events", value); }
        }

        /// <summary>
        /// Gets or sets the type of positioning used for the object.
        /// </summary>
        [DOM("position")]
        public String Position
        {
            get { return GetPropertyValue("position") ?? String.Empty; }
            set { SetProperty("position", value); }
        }

        /// <summary>
        /// Gets or sets the pairs of strings to be used as quotes in generated content.
        /// </summary>
        [DOM("quotes")]
        public String Quotes
        {
            get { return GetPropertyValue("quotes") ?? String.Empty; }
            set { SetProperty("quotes", value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the right edge of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("right")]
        public String Right
        {
            get { return GetPropertyValue("right") ?? String.Empty; }
            set { SetProperty("right", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the ruby text content.
        /// </summary>
        [DOM("rubyAlign")]
        public String RubyAlign
        {
            get { return GetPropertyValue("ruby-align") ?? String.Empty; }
            set { SetProperty("ruby-align", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether, and on which side, ruby
        /// text is allowed to partially overhang any adjacent text in addition
        /// to its own base, when the ruby text is wider than the ruby base
        /// </summary>
        [DOM("rubyOverhang")]
        public String RubyOverhang
        {
            get { return GetPropertyValue("ruby-overhang") ?? String.Empty; }
            set { SetProperty("ruby-overhang", value); }
        }

        /// <summary>
        /// Gets or sets a value that controls the position of the ruby text with respect to its base.
        /// </summary>
        [DOM("rubyPosition")]
        public String RubyPosition
        {
            get { return GetPropertyValue("ruby-position") ?? String.Empty; }
            set { SetProperty("ruby-position", value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbar3dLightColor")]
        public String Scrollbar3dLightColor
        {
            get { return GetPropertyValue("scrollbar3d-light-color") ?? String.Empty; }
            set { SetProperty("scrollbar3d-light-color", value); }
        }

        /// <summary>
        /// Gets or sets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DOM("scrollbarArrowColor")]
        public String ScrollbarArrowColor
        {
            get { return GetPropertyValue("scrollbar-arrow-color") ?? String.Empty; }
            set { SetProperty("scrollbar-arrow-color", value); }
        }

        /// <summary>
        /// Gets or sets the color of the gutter of a scroll bar.
        /// </summary>
        [DOM("scrollbarDarkShadowColor")]
        public String ScrollbarDarkShadowColor
        {
            get { return GetPropertyValue("scrollbar-dark-shadow-color") ?? String.Empty; }
            set { SetProperty("scrollbar-dark-shadow-color", value); }
        }

        /// <summary>
        /// Gets or sets the color of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarFaceColor")]
        public String ScrollbarFaceColor
        {
            get { return GetPropertyValue("scrollbar-face-color") ?? String.Empty; }
            set { SetProperty("scrollbar-face-color", value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarHighlightColor")]
        public String ScrollbarHighlightColor
        {
            get { return GetPropertyValue("scrollbar-highlight-color") ?? String.Empty; }
            set { SetProperty("scrollbar-highlight-color", value); }
        }

        /// <summary>
        /// Gets or sets the color of the bottom and right edges of the
        /// scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarShadowColor")]
        public String ScrollbarShadowColor
        {
            get { return GetPropertyValue("scrollbar-shadow-color") ?? String.Empty; }
            set { SetProperty("scrollbar-shadow-color", value); }
        }

        /// <summary>
        /// Gets or sets the color of the track element of a scroll bar.
        /// </summary>
        [DOM("scrollbarTrackColor")]
        public String ScrollbarTrackColor
        {
            get { return GetPropertyValue("scrollbar-track-color") ?? String.Empty; }
            set { SetProperty("scrollbar-track-color", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates what color to use at the current gradient stop.
        /// </summary>
        [DOM("stopColor")]
        public String StopColor
        {
            get { return GetPropertyValue("stop-color") ?? String.Empty; }
            set { SetProperty("stop-color", value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the opacity of the current gradient stop.
        /// </summary>
        [DOM("stopOpacity")]
        public String StopOpacity
        {
            get { return GetPropertyValue("stop-opacity") ?? String.Empty; }
            set { SetProperty("stop-opacity", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint along
        /// the outline of a given graphical element.
        /// </summary>
        [DOM("stroke")]
        public String Stroke
        {
            get { return GetPropertyValue("stroke") ?? String.Empty; }
            set { SetProperty("stroke", value); }
        }

        /// <summary>
        /// Gets or sets one or more values that indicate the pattern of
        /// dashes and gaps used to stroke paths.
        /// </summary>
        [DOM("strokeDasharray")]
        public String StrokeDasharray
        {
            get { return GetPropertyValue("stroke-dasharray") ?? String.Empty; }
            set { SetProperty("stroke-dasharray", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the distance into the
        /// dash pattern to start the dash.
        /// </summary>
        [DOM("strokeDashoffset")]
        public String StrokeDashoffset
        {
            get { return GetPropertyValue("stroke-dashoffset") ?? String.Empty; }
            set { SetProperty("stroke-dashoffset", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the
        /// end of open subpaths when they are stroked.
        /// </summary>
        [DOM("strokeLinecap")]
        public String StrokeLinecap
        {
            get { return GetPropertyValue("stroke-linecap") ?? String.Empty; }
            set { SetProperty("stroke-linecap", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the corners of
        /// paths or basic shapes when they are stroked.
        /// </summary>
        [DOM("strokeLinejoin")]
        public String StrokeLinejoin
        {
            get { return GetPropertyValue("stroke-linejoin") ?? String.Empty; }
            set { SetProperty("stroke-linejoin", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin property).
        /// </summary>
        [DOM("strokeMiterlimit")]
        public String StrokeMiterlimit
        {
            get { return GetPropertyValue("stroke-miterlimit") ?? String.Empty; }
            set { SetProperty("stroke-miterlimit", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation
        /// that is used to stroke the current object.
        /// </summary>
        [DOM("strokeOpacity")]
        public String StrokeOpacity
        {
            get { return GetPropertyValue("stroke-opacity") ?? String.Empty; }
            set { SetProperty("stroke-opacity", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the width of the stroke on the current object.
        /// </summary>
        [DOM("strokeWidth")]
        public String StrokeWidth
        {
            get { return GetPropertyValue("stroke-width") ?? String.Empty; }
            set { SetProperty("stroke-width", value); }
        }

        /// <summary>
        /// Gets or sets on which side of the object the text will flow.
        /// </summary>
        [DOM("styleFloat")]
        public String StyleFloat
        {
            get { return GetPropertyValue("style-float") ?? String.Empty; }
            set { SetProperty("style-float", value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the table layout is fixed.
        /// </summary>
        [DOM("tableLayout")]
        public String TableLayout
        {
            get { return GetPropertyValue("table-layout") ?? String.Empty; }
            set { SetProperty("table-layout", value); }
        }

        /// <summary>
        /// Gets or sets whether the text in the object is left-aligned, right-aligned, 
        /// centered, or justified.
        /// </summary>
        [DOM("textAlign")]
        public String TextAlign
        {
            get { return GetPropertyValue("text-align") ?? String.Empty; }
            set { SetProperty("text-align", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the last line or only
        /// line of text in the specified object.
        /// </summary>
        [DOM("textAlignLast")]
        public String TextAlignLast
        {
            get { return GetPropertyValue("text-align-last") ?? String.Empty; }
            set { SetProperty("text-align-last", value); }
        }

        /// <summary>
        /// Aligns a string of text relative to the specified point.
        /// </summary>
        [DOM("textAnchor")]
        public String TextAnchor
        {
            get { return GetPropertyValue("text-anchor") ?? String.Empty; }
            set { SetProperty("text-anchor", value); }
        }

        /// <summary>
        /// Gets or sets the autospacing and narrow space width adjustment of text.
        /// </summary>
        [DOM("textAutospace")]
        public String TextAutospace
        {
            get { return GetPropertyValue("text-autospace") ?? String.Empty; }
            set { SetProperty("text-autospace", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DOM("textDecoration")]
        public String TextDecoration
        {
            get { return GetPropertyValue("text-decoration") ?? String.Empty; }
            set { SetProperty("text-decoration", value); }
        }

        /// <summary>
        /// Gets or sets the indentation of the first line of text in the object.
        /// </summary>
        [DOM("textIndent")]
        public String TextIndent
        {
            get { return GetPropertyValue("text-indent") ?? String.Empty; }
            set { SetProperty("text-indent", value); }
        }

        /// <summary>
        /// Gets or sets the type of alignment used to justify text in the object.
        /// </summary>
        [DOM("textJustify")]
        public String TextJustify
        {
            get { return GetPropertyValue("text-justify") ?? String.Empty; }
            set { SetProperty("text-justify", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether to render
        /// ellipses (...) to indicate text overflow.
        /// </summary>
        [DOM("textOverflow")]
        public String TextOverflow
        {
            get { return GetPropertyValue("text-overflow") ?? String.Empty; }
            set { SetProperty("text-overflow", value); }
        }

        /// <summary>
        /// Gets or sets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DOM("textShadow")]
        public String TextShadow
        {
            get { return GetPropertyValue("text-shadow") ?? String.Empty; }
            set { SetProperty("text-shadow", value); }
        }

        /// <summary>
        /// Gets or sets the rendering of the text in the object.
        /// </summary>
        [DOM("textTransform")]
        public String TextTransform
        {
            get { return GetPropertyValue("text-transform") ?? String.Empty; }
            set { SetProperty("text-transform", value); }
        }

        /// <summary>
        /// Gets or sets the position of the underline decoration that is set through the
        /// text-decoration property of the object.
        /// </summary>
        [DOM("textUnderlinePosition")]
        public String TextUnderlinePosition
        {
            get { return GetPropertyValue("text-underline-position") ?? String.Empty; }
            set { SetProperty("text-underline-position", value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("top")]
        public String Top
        {
            get { return GetPropertyValue("top") ?? String.Empty; }
            set { SetProperty("top", value); }
        }

        /// <summary>
        /// Gets or sets a list of one or more transform functions that specify how
        /// to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DOM("transform")]
        public String Transform
        {
            get { return GetPropertyValue("transform") ?? String.Empty; }
            set { SetProperty("transform", value); }
        }

        /// <summary>
        /// Gets or sets one or two values that establish the origin of transformation for an element.
        /// </summary>
        [DOM("transformOrigin")]
        public String TransformOrigin
        {
            get { return GetPropertyValue("transform-origin") ?? String.Empty; }
            set { SetProperty("transform-origin", value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DOM("transformStyle")]
        public String TransformStyle
        {
            get { return GetPropertyValue("transform-style") ?? String.Empty; }
            set { SetProperty("transform-style", value); }
        }

        /// <summary>
        /// Gets or sets one or more shorthand values that specify the transition properties
        /// for a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        [DOM("transition")]
        public String Transition
        {
            get { return GetPropertyValue("transition") ?? String.Empty; }
            set { SetProperty("transition", value); }
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
            get { return GetPropertyValue("transition-delay") ?? String.Empty; }
            set { SetProperty("transition-delay", value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the durations of transitions on
        /// a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        [DOM("transitionDuration")]
        public String TransitionDuration
        {
            get { return GetPropertyValue("transition-duration") ?? String.Empty; }
            set { SetProperty("transition-duration", value); }
        }

        /// <summary>
        /// Gets or sets a value that identifies the CSS property name or names to which
        /// the transition effect (defined by the transition-duration, transition-timing-function,
        /// and transition-delay properties) is applied when a new property value is specified.
        /// </summary>
        [DOM("transitionProperty")]
        public String TransitionProperty
        {
            get { return GetPropertyValue("transition-property") ?? String.Empty; }
            set { SetProperty("transition-property", value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the intermediate property values to be
        /// used during a transition on a set of corresponding object properties identified
        /// in the transition-property property.
        /// </summary>
        [DOM("transitionTimingFunction")]
        public String TransitionTimingFunction
        {
            get { return GetPropertyValue("transition-timing-function") ?? String.Empty; }
            set { SetProperty("transition-timing-function", value); }
        }

        /// <summary>
        /// Gets or sets the level of embedding with respect to the bidirectional algorithm.
        /// </summary>
        [DOM("unicodeBidi")]
        public String UnicodeBidi
        {
            get { return GetPropertyValue("unicode-bidi") ?? String.Empty; }
            set { SetProperty("unicode-bidi", value); }
        }

        /// <summary>
        /// Gets or sets the vertical alignment of the object.
        /// </summary>
        [DOM("verticalAlign")]
        public String VerticalAlign
        {
            get { return GetPropertyValue("vertical-align") ?? String.Empty; }
            set { SetProperty("vertical-align", value); }
        }

        /// <summary>
        /// Gets or sets whether the content of the object is displayed.
        /// </summary>
        [DOM("visibility")]
        public String Visibility
        {
            get { return GetPropertyValue("visibility") ?? String.Empty; }
            set { SetProperty("visibility", value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether lines are automatically broken inside the object.
        /// </summary>
        [DOM("whiteSpace")]
        public String WhiteSpace
        {
            get { return GetPropertyValue("white-space") ?? String.Empty; }
            set { SetProperty("white-space", value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph that must appear at the top of a document.
        /// </summary>
        [DOM("widows")]
        public String Widows
        {
            get { return GetPropertyValue("widows") ?? String.Empty; }
            set { SetProperty("widows", value); }
        }

        /// <summary>
        /// Gets or sets the width of the object.
        /// </summary>
        [DOM("width")]
        public String Width
        {
            get { return GetPropertyValue("width") ?? String.Empty; }
            set { SetProperty("width", value); }
        }

        /// <summary>
        /// Gets or sets line-breaking behavior within words, particularly where multiple languages appear in the object.
        /// </summary>
        [DOM("wordBreak")]
        public String WordBreak
        {
            get { return GetPropertyValue("word-break") ?? String.Empty; }
            set { SetProperty("word-break", value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between words in the object.
        /// </summary>
        [DOM("wordSpacing")]
        public String WordSpacing
        {
            get { return GetPropertyValue("word-spacing") ?? String.Empty; }
            set { SetProperty("word-spacing", value); }
        }

        /// <summary>
        /// Gets or sets whether to break words when the content exceeds the boundaries of its container.
        /// </summary>
        [DOM("wordWrap")]
        public String WordWrap
        {
            get { return GetPropertyValue("word-wrap") ?? String.Empty; }
            set { SetProperty("word-wrap", value); }
        }

        /// <summary>
        /// Gets or sets the direction and flow of the content in the object.
        /// </summary>
        [DOM("writingMode")]
        public String WritingMode
        {
            get { return GetPropertyValue("writing-mode") ?? String.Empty; }
            set { SetProperty("writing-mode", value); }
        }

        /// <summary>
        /// Gets or sets the stacking order of positioned objects.
        /// </summary>
        [DOM("zIndex")]
        public String ZIndex
        {
            get { return GetPropertyValue("z-index") ?? String.Empty; }
            set { SetProperty("z-index", value); }
        }

        /// <summary>
        /// Gets or sets the magnification scale of the object.
        /// </summary>
        [DOM("zoom")]
        public String Zoom
        {
            get { return GetPropertyValue("zoom") ?? String.Empty; }
            set { SetProperty("zoom", value); }
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
            for (int i = 0; i < _rules.Count; i++)
            {
                if (_rules[i].Name.Equals(propertyName))
                {
                    var value = _rules[i].Value;
                    _rules.RemoveAt(i);
                    Propagate();
                    return value.CssText;
                }
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
            for (int i = 0; i < _rules.Count; i++)
            {
                if (_rules[i].Name.Equals(propertyName))
                    return _rules[i].Important ? "important" : null;
            }

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
            for (int i = 0; i < _rules.Count; i++)
            {
                if (_rules[i].Name.Equals(propertyName))
                    return _rules[i].Value.CssText;
            }

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
                _rules.Add(decl);
                Propagate();
            }

            return this;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the list of CSS declarations.
        /// </summary>
        internal List<CSSProperty> List
        {
            get { return _rules; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Updates the CSSStyleDeclaration with the given value.
        /// </summary>
        /// <param name="value">The new value.</param>
        internal void Update(String value)
        {
            if (!_blocking)
            {
                var rules = CssParser.ParseDeclarations(value ?? String.Empty)._rules;
                _rules.Clear();
                _rules.AddRange(rules);
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
        /// <returns></returns>
        public String ToCss()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _rules.Count; i++)
                sb.Append(_rules[i].ToCss()).Append(';');

            return sb.ToString();
        }

        #endregion

        #region Interface implementation

        /// <summary>
        /// Returns an ienumerator that enumerates over all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        public IEnumerator<CSSProperty> GetEnumerator()
        {
            return _rules.GetEnumerator();
        }

        /// <summary>
        /// Returns a common ienumerator to enumerate all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_rules).GetEnumerator();
        }

        #endregion
    }
}
