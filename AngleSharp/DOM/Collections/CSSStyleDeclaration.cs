using AngleSharp.Css;
using AngleSharp.DOM.Css;
using System;
using System.Collections;
using System.Collections.Generic;

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
            var text = String.Empty;
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
            _getter = () => (host.OwnerDocument == null || host.OwnerDocument.Options.IsStyling) ? host.GetAttribute(AttributeNames.STYLE) : String.Empty;
            _setter = value => host.SetAttribute(AttributeNames.STYLE, value);
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
            get { return GetPropertyValue(PropertyNames.ALIGN_CONTENT) ?? String.Empty; }
            set { SetProperty(PropertyNames.ALIGN_CONTENT, value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex container.
        /// </summary>
        [DOM("alignItems")]
        public String AlignItems
        {
            get { return GetPropertyValue(PropertyNames.ALIGN_ITEMS) ?? String.Empty; }
            set { SetProperty(PropertyNames.ALIGN_ITEMS, value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout
        /// axis defined by the flex-direction property) of flex items of
        /// the flex container.
        /// </summary>
        [DOM("alignSelf")]
        public String AlignSelf
        {
            get { return GetPropertyValue(PropertyNames.ALIGN_SELF) ?? String.Empty; }
            set { SetProperty(PropertyNames.ALIGN_SELF, value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the object represents a
        /// keyboard shortcut.
        /// </summary>
        [DOM("accelerator")]
        public String Accelerator
        {
            get { return GetPropertyValue(PropertyNames.ACCELERATOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.ACCELERATOR, value); }
        }

        /// <summary>
        /// Gets or sets which baseline of this element is to be aligned
        /// with the corresponding baseline of the parent.
        /// </summary>
        [DOM("alignmentBaseline")]
        public String AlignmentBaseline
        {
            get { return GetPropertyValue(PropertyNames.ALIGN_BASELINE) ?? String.Empty; }
            set { SetProperty(PropertyNames.ALIGN_BASELINE, value); }
        }

        /// <summary>
        /// Gets or sets the azimuth value, which enables different audio sources to
        /// be positioned spatially for aural presentation. This is important in that
        /// it provides a natural way to tell several voices apart, as each can be
        /// positioned to originate at a different location on the sound stage.
        /// </summary>
        [DOM("azimuth")]
        public String Azimuth
        {
            get { return GetPropertyValue(PropertyNames.AZIMUTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.AZIMUTH, value); }
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
            get { return GetPropertyValue(PropertyNames.ANIMATION) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION, value); }
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
            get { return GetPropertyValue(PropertyNames.ANIMATION_DELAY) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_DELAY, value); }
        }

        /// <summary>
        /// Gets or sets the direction of play for an animation cycle.
        /// </summary>
        [DOM("animationDirection")]
        public String AnimationDirection
        {
            get { return GetPropertyValue(PropertyNames.ANIMATION_DIRECTION) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_DIRECTION, value); }
        }

        /// <summary>
        /// Gets or sets the length of time to complete one cycle of the animation.
        /// </summary>
        [DOM("animationDuration")]
        public String AnimationDuration
        {
            get { return GetPropertyValue(PropertyNames.ANIMATION_DURATION) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_DURATION, value); }
        }

        /// <summary>
        /// Gets or sets whether the effects of an animation are visible before or after it plays.
        /// </summary>
        [DOM("animationFillMode")]
        public String AnimationFillMode
        {
            get { return GetPropertyValue(PropertyNames.ANIMATION_FILL_MODE) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_FILL_MODE, value); }
        }

        /// <summary>
        /// Gets or sets the number of times an animation cycle is played.
        /// </summary>
        [DOM("animationIterationCount")]
        public String AnimationIterationCount
        {
            get { return GetPropertyValue(PropertyNames.ANIMATION_ITERATION_COUNT) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_ITERATION_COUNT, value); }
        }

        /// <summary>
        /// Gets or sets one or more animation names. An animation name
        /// selects a CSS @keyframes at-rule.
        /// </summary>
        [DOM("animationName")]
        public String AnimationName
        {
            get { return GetPropertyValue(PropertyNames.ANIMATION_NAME) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_NAME, value); }
        }

        /// <summary>
        /// Gets or sets whether an animation is playing or paused.
        /// </summary>
        [DOM("animationPlayState")]
        public String AnimationPlayState
        {
            get { return GetPropertyValue(PropertyNames.ANIMATION_PLAY_STATE) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_PLAY_STATE, value); }
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
            get { return GetPropertyValue(PropertyNames.ANIMATION_TIMING_FUNCTION) ?? String.Empty; }
            set { SetProperty(PropertyNames.ANIMATION_TIMING_FUNCTION, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the back face
        /// (reverse side) of an object is visible.
        /// </summary>
        [DOM("backfaceVisibility")]
        public String BackfaceVisibility
        {
            get { return GetPropertyValue(PropertyNames.BACKFACE_VISIBILITY) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKFACE_VISIBILITY, value); }
        }

        /// <summary>
        /// Gets or sets up to five separate background properties of an object.
        /// </summary>
        [DOM("background")]
        public String Background
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND, value); }
        }

        /// <summary>
        /// Gets or sets how the background image (or images) is attached
        /// to the object within the document.
        /// </summary>
        [DOM("backgroundAttachment")]
        public String BackgroundAttachment
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_ATTACHMENT) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_ATTACHMENT, value); }
        }

        /// <summary>
        /// Gets or sets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        [DOM("backgroundClip")]
        public String BackgroundClip
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_CLIP) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_CLIP, value); }
        }

        /// <summary>
        /// Gets or sets the color behind the content of the object.
        /// </summary>
        [DOM("backgroundColor")]
        public String BackgroundColor
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the background image or images of the object.
        /// </summary>
        [DOM("backgroundImage")]
        public String BackgroundImage
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_IMAGE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_IMAGE, value); }
        }

        /// <summary>
        /// Gets or sets the positioning area of an element or multiple elements.
        /// </summary>
        [DOM("backgroundOrigin")]
        public String BackgroundOrigin
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_ORIGIN) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_ORIGIN, value); }
        }

        /// <summary>
        /// Gets or sets the position of the background of the object.
        /// </summary>
        [DOM("backgroundPosition")]
        public String BackgroundPosition
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_POSITION) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_POSITION, value); }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the background-position property.
        /// </summary>
        [DOM("backgroundPositionX")]
        public String BackgroundPositionX
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_POSITION_X) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_POSITION_X, value); }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the background-position property.
        /// </summary>
        [DOM("backgroundPositionY")]
        public String BackgroundPositionY
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_POSITION_Y) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_POSITION_Y, value); }
        }

        /// <summary>
        /// Gets or sets whether and how the background image (or images) is tiled.
        /// </summary>
        [DOM("backgroundRepeat")]
        public String BackgroundRepeat
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_REPEAT) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_REPEAT, value); }
        }

        /// <summary>
        /// Gets or sets the size of the background images.
        /// </summary>
        [DOM("backgroundSize")]
        public String BackgroundSize
        {
            get { return GetPropertyValue(PropertyNames.BACKGROUND_SIZE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BACKGROUND_SIZE, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        [DOM("baselineShift")]
        public String BaselineShift
        {
            get { return GetPropertyValue(PropertyNames.BASELINE_SHIFT) ?? String.Empty; }
            set { SetProperty(PropertyNames.BASELINE_SHIFT, value); }
        }

        /// <summary>
        /// Gets or sets the location of the Dynamic HTML (DHTML) behaviorDHTML Behaviors.
        /// </summary>
        [DOM("behavior")]
        public String Behavior
        {
            get { return GetPropertyValue(PropertyNames.BEHAVIOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BEHAVIOR, value); }
        }

        /// <summary>
        /// Gets or sets the properties of a border drawn around an object.
        /// </summary>
        [DOM("border")]
        public String Border
        {
            get { return GetPropertyValue(PropertyNames.BORDER) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the bottom border of the object.
        /// </summary>
        [DOM("borderBottom")]
        public String BorderBottom
        {
            get { return GetPropertyValue(PropertyNames.BORDER_BOTTOM) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_BOTTOM, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the bottom border of an object.
        /// </summary>
        [DOM("borderBottomColor")]
        public String BorderBottomColor
        {
            get { return GetPropertyValue(PropertyNames.BORDER_BOTTOM_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_BOTTOM_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the radii of the quarter ellipse that defines
        /// the shape of the lower-left corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderBottomLeftRadius")]
        public String BorderBottomLeftRadius
        {
            get { return GetPropertyValue(PropertyNames.BORDER_BOTTOM_LEFT_RADIUS) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_BOTTOM_LEFT_RADIUS, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the lower-right corner
        /// for the outer border edge of the current box.
        /// </summary>
        [DOM("borderBottomRightRadius")]
        public String BorderBottomRightRadius
        {
            get { return GetPropertyValue(PropertyNames.BORDER_BOTTOM_RIGHT_RADIUS) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_BOTTOM_RIGHT_RADIUS, value); }
        }

        /// <summary>
        /// Gets or sets the style of the bottom border of the object.
        /// </summary>
        [DOM("borderBottomStyle")]
        public String BorderBottomStyle
        {
            get { return GetPropertyValue(PropertyNames.BORDER_BOTTOM_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_BOTTOM_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the bottom border of the object.
        /// </summary>
        [DOM("borderBottomWidth")]
        public String BorderBottomWidth
        {
            get { return GetPropertyValue(PropertyNames.BORDER_BOTTOM_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_BOTTOM_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets whether the row and cell borders of a table are joined in a
        /// single border or detached as in standard HTML.
        /// </summary>
        [DOM("borderCollapse")]
        public String BorderCollapse
        {
            get { return GetPropertyValue(PropertyNames.BORDER_COLLAPSE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_COLLAPSE, value); }
        }

        /// <summary>
        /// Gets or sets the border color of the object.
        /// </summary>
        [DOM("borderColor")]
        public String BorderColor
        {
            get { return GetPropertyValue(PropertyNames.BORDER_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets an image to be used in place of the border styles.
        /// </summary>
        [DOM("borderImage")]
        public String BorderImage
        {
            get { return GetPropertyValue(PropertyNames.BORDER_IMAGE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_IMAGE, value); }
        }

        /// <summary>
        /// Gets or sets the amount by which the border image area extends beyond the border box.
        /// </summary>
        [DOM("borderImageOutset")]
        public String BorderImageOutset
        {
            get { return GetPropertyValue(PropertyNames.BORDER_IMAGE_OUTSET) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_IMAGE_OUTSET, value); }
        }

        /// <summary>
        /// Gets or sets ow the image is scaled and tiled.
        /// </summary>
        [DOM("borderImageRepeat")]
        public String BorderImageRepeat
        {
            get { return GetPropertyValue(PropertyNames.BORDER_IMAGE_REPEAT) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_IMAGE_REPEAT, value); }
        }

        /// <summary>
        /// Gets or sets four inward offsets, this property slices the specified
        /// border image into a three by three grid: four corners, four edges, and a central region.
        /// </summary>
        [DOM("borderImageSlice")]
        public String BorderImageSlice
        {
            get { return GetPropertyValue(PropertyNames.BORDER_IMAGE_SLICE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_IMAGE_SLICE, value); }
        }

        /// <summary>
        /// Gets or sets the path of the image to be used for the border.
        /// </summary>
        [DOM("borderImageSource")]
        public String BorderImageSource
        {
            get { return GetPropertyValue(PropertyNames.BORDER_IMAGE_SOURCE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_IMAGE_SOURCE, value); }
        }

        /// <summary>
        /// Gets or sets the inward offsets from the outer border edge.
        /// </summary>
        [DOM("borderImageWidth")]
        public String BorderImageWidth
        {
            get { return GetPropertyValue(PropertyNames.BORDER_IMAGE_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_IMAGE_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the left border of the object.
        /// </summary>
        [DOM("borderLeft")]
        public String BorderLeft
        {
            get { return GetPropertyValue(PropertyNames.BORDER_LEFT) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_LEFT, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the left border of an object.
        /// </summary>
        [DOM("borderLeftColor")]
        public String BorderLeftColor
        {
            get { return GetPropertyValue(PropertyNames.BORDER_LEFT_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_LEFT_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the style of the left border of the object.
        /// </summary>
        [DOM("borderLeftStyle")]
        public String BorderLeftStyle
        {
            get { return GetPropertyValue(PropertyNames.BORDER_LEFT_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_LEFT_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the left border of the object.
        /// </summary>
        [DOM("borderLeftWidth")]
        public String BorderLeftWidth
        {
            get { return GetPropertyValue(PropertyNames.BORDER_LEFT_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_LEFT_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets the radii of a quarter ellipse that defines the shape of
        /// the corners for the outer border edge of the current box.
        /// </summary>
        [DOM("borderRadius")]
        public String BorderRadius
        {
            get { return GetPropertyValue(PropertyNames.BORDER_RADIUS) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_RADIUS, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the right border of the object.
        /// </summary>
        [DOM("borderRight")]
        public String BorderRight
        {
            get { return GetPropertyValue(PropertyNames.BORDER_RIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_RIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the right border of an object.
        /// </summary>
        [DOM("borderRightColor")]
        public String BorderRightColor
        {
            get { return GetPropertyValue(PropertyNames.BORDER_RIGHT_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_RIGHT_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the style of the right border of the object.
        /// </summary>
        [DOM("borderRightStyle")]
        public String BorderRightStyle
        {
            get { return GetPropertyValue(PropertyNames.BORDER_RIGHT_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_RIGHT_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the right border of the object.
        /// </summary>
        [DOM("borderRightWidth")]
        public String BorderRightWidth
        {
            get { return GetPropertyValue(PropertyNames.BORDER_RIGHT_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_RIGHT_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets the distance between the borders of adjoining cells in a table.
        /// </summary>
        [DOM("borderSpacing")]
        public String BorderSpacing
        {
            get { return GetPropertyValue(PropertyNames.BORDER_SPACING) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_SPACING, value); }
        }

        /// <summary>
        /// Gets or sets the style of the left, right, top, and bottom borders of the object.
        /// </summary>
        [DOM("borderStyle")]
        public String BorderStyle
        {
            get { return GetPropertyValue(PropertyNames.BORDER_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the top border of the object.
        /// </summary>
        [DOM("borderTop")]
        public String BorderTop
        {
            get { return GetPropertyValue(PropertyNames.BORDER_TOP) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_TOP, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the top border of an object.
        /// </summary>
        [DOM("borderTopColor")]
        public String BorderTopColor
        {
            get { return GetPropertyValue(PropertyNames.BORDER_TOP_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_TOP_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets  one or two values that define the radii of the quarter ellipse
        /// that defines the shape of the upper-left corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderTopLeftRadius")]
        public String BorderTopLeftRadius
        {
            get { return GetPropertyValue(PropertyNames.BORDER_TOP_LEFT_RADIUS) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_TOP_LEFT_RADIUS, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-right
        /// corner for the outer border edge of the current box.
        /// </summary>
        [DOM("borderTopRightRadius")]
        public String BorderTopRightRadius
        {
            get { return GetPropertyValue(PropertyNames.BORDER_TOP_RIGHT_RADIUS) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_TOP_RIGHT_RADIUS, value); }
        }

        /// <summary>
        /// Gets or sets  the style of the top border of the object.
        /// </summary>
        [DOM("borderTopStyle")]
        public String BorderTopStyle
        {
            get { return GetPropertyValue(PropertyNames.BORDER_TOP_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_TOP_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the top border of the object.
        /// </summary>
        [DOM("borderTopWidth")]
        public String BorderTopWidth
        {
            get { return GetPropertyValue(PropertyNames.BORDER_TOP_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_TOP_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets the thicknesses of the left, right, top, and bottom borders of an object.
        /// </summary>
        [DOM("borderWidth")]
        public String BorderWidth
        {
            get { return GetPropertyValue(PropertyNames.BORDER_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.BORDER_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        [DOM("boxShadow")]
        public String BoxShadow
        {
            get { return GetPropertyValue(PropertyNames.BOX_SHADOW) ?? String.Empty; }
            set { SetProperty(PropertyNames.BOX_SHADOW, value); }
        }

        /// <summary>
        /// Gets or sets the box model to use for object sizing.
        /// </summary>
        [DOM("boxSizing")]
        public String BoxSizing
        {
            get { return GetPropertyValue(PropertyNames.BOX_SIZING) ?? String.Empty; }
            set { SetProperty(PropertyNames.BOX_SIZING, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that follows a content
        /// block in a multi-column element.
        /// </summary>
        [DOM("breakAfter")]
        public String BreakAfter
        {
            get { return GetPropertyValue(PropertyNames.BREAK_AFTER) ?? String.Empty; }
            set { SetProperty(PropertyNames.BREAK_AFTER, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        [DOM("breakBefore")]
        public String BreakBefore
        {
            get { return GetPropertyValue(PropertyNames.BREAK_BEFORE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BREAK_BEFORE, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        [DOM("breakInside")]
        public String BreakInside
        {
            get { return GetPropertyValue(PropertyNames.BREAK_INSIDE) ?? String.Empty; }
            set { SetProperty(PropertyNames.BREAK_INSIDE, value); }
        }

        /// <summary>
        /// Gets or sets where the caption of a table is located.
        /// </summary>
        [DOM("captionSide")]
        public String CaptionSide
        {
            get { return GetPropertyValue(PropertyNames.CAPTION_SIDE) ?? String.Empty; }
            set { SetProperty(PropertyNames.CAPTION_SIDE, value); }
        }

        /// <summary>
        /// Gets or sets whether the object allows floating objects on its left side,
        /// right side, or both, so that the next text displays past the floating objects.
        /// </summary>
        [DOM("clear")]
        public String Clear
        {
            get { return GetPropertyValue(PropertyNames.CLEAR) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLEAR, value); }
        }

        /// <summary>
        /// Gets or sets which part of a positioned object is visible.
        /// </summary>
        [DOM("clip")]
        public String Clip
        {
            get { return GetPropertyValue(PropertyNames.CLIP) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP, value); }
        }

        /// <summary>
        /// Gets or sets the bottom coordinate of the object clipping region.
        /// </summary>
        [DOM("clipBottom")]
        public String ClipBottom
        {
            get { return GetPropertyValue(PropertyNames.CLIP_BOTTOM) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP_BOTTOM, value); }
        }

        /// <summary>
        /// Gets or sets the left coordinate of the object clipping region.
        /// </summary>
        [DOM("clipLeft")]
        public String ClipLeft
        {
            get { return GetPropertyValue(PropertyNames.CLIP_LEFT) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP_LEFT, value); }
        }

        /// <summary>
        /// Gets or sets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        [DOM("clipPath")]
        public String ClipPath
        {
            get { return GetPropertyValue(PropertyNames.CLIP_PATH) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP_PATH, value); }
        }

        /// <summary>
        /// Gets or sets the right coordinate of the object clipping region.
        /// </summary>
        [DOM("clipRight")]
        public String ClipRight
        {
            get { return GetPropertyValue(PropertyNames.CLIP_RIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP_RIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        [DOM("clipRule")]
        public String ClipRule
        {
            get { return GetPropertyValue(PropertyNames.CLIP_RULE) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP_RULE, value); }
        }

        /// <summary>
        /// Gets or sets the top coordinate of the object clipping region.
        /// </summary>
        [DOM("clipTop")]
        public String ClipTop
        {
            get { return GetPropertyValue(PropertyNames.CLIP_TOP) ?? String.Empty; }
            set { SetProperty(PropertyNames.CLIP_TOP, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the text of an object.
        /// </summary>
        [DOM("color")]
        public String Color
        {
            get { return GetPropertyValue(PropertyNames.COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLOR, value); }
        }

        /// <summary>
        /// Gets or sets which color space to use for filter effects.
        /// </summary>
        [DOM("colorInterpolationFilters")]
        public String ColorInterpolationFilters
        {
            get { return GetPropertyValue(PropertyNames.COLOR_INTERPOLATION_FILTERS) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLOR_INTERPOLATION_FILTERS, value); }
        }

        /// <summary>
        /// Gets or sets the optimal number of columns in a multi-column element.
        /// </summary>
        [DOM("columnCount")]
        public String ColumnCount
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_COUNT) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_COUNT, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        [DOM("columnFill")]
        public String ColumnFill
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_FILL) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_FILL, value); }
        }

        /// <summary>
        /// Gets or sets the width of the gap between columns in a multi-column element.
        /// </summary>
        [DOM("columnGap")]
        public String ColumnGap
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_GAP) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_GAP, value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value  that specifies values for the columnRuleWidth, 
        /// columnRuleStyle, and the columnRuleColor of a multi-column element.
        /// </summary>
        [DOM("columnRule")]
        public String ColumnRule
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_RULE) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_RULE, value); }
        }

        /// <summary>
        /// Gets or sets the color for all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleColor")]
        public String ColumnRuleColor
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_RULE_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_RULE_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the style for all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleStyle")]
        public String ColumnRuleStyle
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_RULE_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_RULE_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the width of all column rules in a multi-column element.
        /// </summary>
        [DOM("columnRuleWidth")]
        public String ColumnRuleWidth
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_RULE_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_RULE_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value that specifies values for the column-width
        /// and the column-count of a multi-column element.
        /// </summary>
        [DOM("columns")]
        public String Columns
        {
            get { return GetPropertyValue(PropertyNames.COLUMNS) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMNS, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        [DOM("columnSpan")]
        public String ColumnSpan
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_SPAN) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_SPAN, value); }
        }

        /// <summary>
        /// Gets or sets the optimal width of the columns in a multi-column element.
        /// </summary>
        [DOM("columnWidth")]
        public String ColumnWidth
        {
            get { return GetPropertyValue(PropertyNames.COLUMN_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.COLUMN_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets generated content to insert before or after an element.
        /// </summary>
        [DOM("content")]
        public String Content
        {
            get { return GetPropertyValue(PropertyNames.CONTENT) ?? String.Empty; }
            set { SetProperty(PropertyNames.CONTENT, value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to increment.
        /// </summary>
        [DOM("counterIncrement")]
        public String CounterIncrement
        {
            get { return GetPropertyValue(PropertyNames.COUNTER_INCREMENT) ?? String.Empty; }
            set { SetProperty(PropertyNames.COUNTER_INCREMENT, value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to create or reset to zero.
        /// </summary>
        [DOM("counterReset")]
        public String CounterReset
        {
            get { return GetPropertyValue(PropertyNames.COUNTER_RESET) ?? String.Empty; }
            set { SetProperty(PropertyNames.COUNTER_RESET, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether a box should float
        /// to the left, right, or not at all.
        /// </summary>
        [DOM("cssFloat")]
        public String Float
        {
            get { return GetPropertyValue(PropertyNames.FLOAT) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLOAT, value); }
        }

        /// <summary>
        /// Gets or sets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        [DOM("cursor")]
        public String Cursor
        {
            get { return GetPropertyValue(PropertyNames.CURSOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.CURSOR, value); }
        }

        /// <summary>
        /// Gets or sets the reading order of the object.
        /// </summary>
        [DOM("direction")]
        public String Direction
        {
            get { return GetPropertyValue(PropertyNames.DIRECTION) ?? String.Empty; }
            set { SetProperty(PropertyNames.DIRECTION, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether and how the object is rendered.
        /// </summary>
        [DOM("display")]
        public String Display
        {
            get { return GetPropertyValue(PropertyNames.DISPLAY) ?? String.Empty; }
            set { SetProperty(PropertyNames.DISPLAY, value); }
        }

        /// <summary>
        /// Gets or sets a value that determines or redetermines a scaled-baseline table.
        /// </summary>
        [DOM("dominantBaseline")]
        public String DominantBaseline
        {
            get { return GetPropertyValue(PropertyNames.DOMINANT_BASELINE) ?? String.Empty; }
            set { SetProperty(PropertyNames.DOMINANT_BASELINE, value); }
        }

        /// <summary>
        /// Determines whether to show or hide a cell without content.
        /// </summary>
        [DOM("emptyCells")]
        public String EmptyCells
        {
            get { return GetPropertyValue(PropertyNames.EMPTY_CELLS) ?? String.Empty; }
            set { SetProperty(PropertyNames.EMPTY_CELLS, value); }
        }

        /// <summary>
        /// Allocate a shared background image all graphic elements within a container.
        /// </summary>
        [DOM("enableBackground")]
        public String EnableBackground
        {
            get { return GetPropertyValue(PropertyNames.ENABLE_BACKGROUND) ?? String.Empty; }
            set { SetProperty(PropertyNames.ENABLE_BACKGROUND, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        [DOM("fill")]
        public String Fill
        {
            get { return GetPropertyValue(PropertyNames.FILL) ?? String.Empty; }
            set { SetProperty(PropertyNames.FILL, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation that
        /// is used to paint the interior of the current object.
        /// </summary>
        [DOM("fillOpacity")]
        public String FillOpacity
        {
            get { return GetPropertyValue(PropertyNames.FILL_OPACITY) ?? String.Empty; }
            set { SetProperty(PropertyNames.FILL_OPACITY, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the algorithm that is to be used to determine
        /// what parts of the canvas are included inside the shape.
        /// </summary>
        [DOM("fillRule")]
        public String FillRule
        {
            get { return GetPropertyValue(PropertyNames.FILL_RULE) ?? String.Empty; }
            set { SetProperty(PropertyNames.FILL_RULE, value); }
        }

        /// <summary>
        /// The filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        [DOM("filter")]
        public String Filter
        {
            get { return GetPropertyValue(PropertyNames.FILTER) ?? String.Empty; }
            set { SetProperty(PropertyNames.FILTER, value); }
        }

        /// <summary>
        /// Gets or sets the parameter values of a flexible length, the positive and
        /// negative flexibility, and the preferred size.
        /// </summary>
        [DOM("flex")]
        public String Flex
        {
            get { return GetPropertyValue(PropertyNames.FLEX) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX, value); }
        }

        /// <summary>
        /// Gets or sets the initial main size of the flex item.
        /// </summary>
        [DOM("flexBasis")]
        public String FlexBasis
        {
            get { return GetPropertyValue(PropertyNames.FLEX_BASIS) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX_BASIS, value); }
        }

        /// <summary>
        /// Gets or sets the direction of the main axis which specifies how
        /// the flex items are displayed in the flex container.
        /// </summary>
        [DOM("flexDirection")]
        public String FlexDirection
        {
            get { return GetPropertyValue(PropertyNames.FLEX_DIRECTION) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX_DIRECTION, value); }
        }

        /// <summary>
        /// Gets or sets the shorthand property to set both the flex-direction and flex-wrap
        /// properties of a flex container.
        /// </summary>
        [DOM("flexFlow")]
        public String FlexFlow
        {
            get { return GetPropertyValue(PropertyNames.FLEX_FLOW) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX_FLOW, value); }
        }

        /// <summary>
        /// Gets or sets the flex grow factor for the flex item.
        /// </summary>
        [DOM("flexGrow")]
        public String FlexGrow
        {
            get { return GetPropertyValue(PropertyNames.FLEX_GROW) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX_GROW, value); }
        }

        /// <summary>
        /// Gets or sets the flex shrink factor for the flex item.
        /// </summary>
        [DOM("flexShrink")]
        public String FlexShrink
        {
            get { return GetPropertyValue(PropertyNames.FLEX_SHRINK) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX_SHRINK, value); }
        }

        /// <summary>
        /// Gets or sets whether flex items wrap and the direction they
        /// wrap onto multiple lines or columns based on the spac
        /// available in the flex container. 
        /// </summary>
        [DOM("flexWrap")]
        public String FlexWrap
        {
            get { return GetPropertyValue(PropertyNames.FLEX_WRAP) ?? String.Empty; }
            set { SetProperty(PropertyNames.FLEX_WRAP, value); }
        }

        /// <summary>
        /// Gets or sets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of
        /// six user-preference fonts.
        /// </summary>
        [DOM("font")]
        public String Font
        {
            get { return GetPropertyValue(PropertyNames.FONT) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT, value); }
        }

        /// <summary>
        /// Gets or sets the name of the font used for text in the object.
        /// </summary>
        [DOM("fontFamily")]
        public String FontFamily
        {
            get { return GetPropertyValue(PropertyNames.FONT_FAMILY) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_FAMILY, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        [DOM("fontFeatureSettings")]
        public String FontFeatureSettings
        {
            get { return GetPropertyValue(PropertyNames.FONT_FEATURE_SETTINGS) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_FEATURE_SETTINGS, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the font size used for text in the object.
        /// </summary>
        [DOM("fontSize")]
        public String FontSize
        {
            get { return GetPropertyValue(PropertyNames.FONT_SIZE) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_SIZE, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies an aspect value for an element that
        /// will effectively preserve the x-height of the first choice font, whether
        /// it is substituted or not.
        /// </summary>
        [DOM("fontSizeAdjust")]
        public String FontSizeAdjust
        {
            get { return GetPropertyValue(PropertyNames.FONT_SIZE_ADJUST) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_SIZE_ADJUST, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a normal, condensed,
        /// or expanded face of a font family.
        /// </summary>
        [DOM("fontStretch")]
        public String FontStretch
        {
            get { return GetPropertyValue(PropertyNames.FONT_STRETCH) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_STRETCH, value); }
        }

        /// <summary>
        /// Gets or sets the font style of the object as italic, normal, or oblique.
        /// </summary>
        [DOM("fontStyle")]
        public String FontStyle
        {
            get { return GetPropertyValue(PropertyNames.FONT_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets whether the text of the object is in small capital letters.
        /// </summary>
        [DOM("fontVariant")]
        public String FontVariant
        {
            get { return GetPropertyValue(PropertyNames.FONT_VARIANT) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_VARIANT, value); }
        }

        /// <summary>
        /// Gets of sets the weight of the font of the object.
        /// </summary>
        [DOM("fontWeight")]
        public String FontWeight
        {
            get { return GetPropertyValue(PropertyNames.FONT_WEIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.FONT_WEIGHT, value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence of characters
        /// relative to an inline-progression-direction of horizontal.
        /// </summary>
        [DOM("glyphOrientationHorizontal")]
        public String GlyphOrientationHorizontal
        {
            get { return GetPropertyValue(PropertyNames.GLYPH_ORIENTATION_HORIZONTAL) ?? String.Empty; }
            set { SetProperty(PropertyNames.GLYPH_ORIENTATION_HORIZONTAL, value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of vertical.
        /// </summary>
        [DOM("glyphOrientationVertical")]
        public String GlyphOrientationVertical
        {
            get { return GetPropertyValue(PropertyNames.GLYPH_ORIENTATION_VERTICAL) ?? String.Empty; }
            set { SetProperty(PropertyNames.GLYPH_ORIENTATION_VERTICAL, value); }
        }

        /// <summary>
        /// Gets or sets the height of the object.
        /// </summary>
        [DOM("height")]
        public String Height
        {
            get { return GetPropertyValue(PropertyNames.HEIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.HEIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the state of an IME.
        /// </summary>
        [DOM("imeMode")]
        public String ImeMode
        {
            get { return GetPropertyValue(PropertyNames.IME_MODE) ?? String.Empty; }
            set { SetProperty(PropertyNames.IME_MODE, value); }
        }

        /// <summary>
        /// Gets or sets a how flex items are aligned along the main axis of the flex
        /// container after any flexible lengths and auto margins are resolved.
        /// </summary>
        [DOM("justifyContent")]
        public String JustifyContent
        {
            get { return GetPropertyValue(PropertyNames.JUSTIFY_CONTENT) ?? String.Empty; }
            set { SetProperty(PropertyNames.JUSTIFY_CONTENT, value); }
        }

        /// <summary>
        /// Gets or sets the composite document grid properties
        /// that specify the layout of text characters.
        /// </summary>
        [DOM("layoutGrid")]
        public String LayoutGrid
        {
            get { return GetPropertyValue(PropertyNames.LAYOUT_GRID) ?? String.Empty; }
            set { SetProperty(PropertyNames.LAYOUT_GRID, value); }
        }

        /// <summary>
        /// Gets or sets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DOM("layoutGridChar")]
        public String LayoutGridChar
        {
            get { return GetPropertyValue(PropertyNames.LAYOUT_GRID_CHAR) ?? String.Empty; }
            set { SetProperty(PropertyNames.LAYOUT_GRID_CHAR, value); }
        }

        /// <summary>
        /// Gets or sets the gridline value used for rendering the
        /// text content of an element.
        /// </summary>
        [DOM("layoutGridLine")]
        public String LayoutGridLine
        {
            get { return GetPropertyValue(PropertyNames.LAYOUT_GRID_LINE) ?? String.Empty; }
            set { SetProperty(PropertyNames.LAYOUT_GRID_LINE, value); }
        }

        /// <summary>
        /// Gets or sets whether the text layout grid uses two dimensions.
        /// </summary>
        [DOM("layoutGridMode")]
        public String LayoutGridMode
        {
            get { return GetPropertyValue(PropertyNames.LAYOUT_GRID_MODE) ?? String.Empty; }
            set { SetProperty(PropertyNames.LAYOUT_GRID_MODE, value); }
        }

        /// <summary>
        /// Gets or sets the type of grid used for rendering
        /// the text content of an element.
        /// </summary>
        [DOM("layoutGridType")]
        public String LayoutGridType
        {
            get { return GetPropertyValue(PropertyNames.LAYOUT_GRID_TYPE) ?? String.Empty; }
            set { SetProperty(PropertyNames.LAYOUT_GRID_TYPE, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("left")]
        public String Left
        {
            get { return GetPropertyValue(PropertyNames.LEFT) ?? String.Empty; }
            set { SetProperty(PropertyNames.LEFT, value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between letters in the object.
        /// </summary>
        [DOM("letterSpacing")]
        public String LetterSpacing
        {
            get { return GetPropertyValue(PropertyNames.LETTER_SPACING) ?? String.Empty; }
            set { SetProperty(PropertyNames.LETTER_SPACING, value); }
        }

        /// <summary>
        /// Gets or sets the distance between lines in the object.
        /// </summary>
        [DOM("lineHeight")]
        public String LineHeight
        {
            get { return GetPropertyValue(PropertyNames.LINE_HEIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.LINE_HEIGHT, value); }
        }

        /// <summary>
        /// Gets or sets up to three separate list-style properties of the object.
        /// </summary>
        [DOM("listStyle")]
        public String ListStyle
        {
            get { return GetPropertyValue(PropertyNames.LIST_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.LIST_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates which image to use as
        /// a list-item marker for the object.
        /// </summary>
        [DOM("listStyleImage")]
        public String ListStyleImage
        {
            get { return GetPropertyValue(PropertyNames.LIST_STYLE_IMAGE) ?? String.Empty; }
            set { SetProperty(PropertyNames.LIST_STYLE_IMAGE, value); }
        }

        /// <summary>
        /// Gets or sets a variable that indicates how the list-item marker
        /// is drawn relative to the content of the object.
        /// </summary>
        [DOM("listStylePosition")]
        public String ListStylePosition
        {
            get { return GetPropertyValue(PropertyNames.LIST_STYLE_POSITION) ?? String.Empty; }
            set { SetProperty(PropertyNames.LIST_STYLE_POSITION, value); }
        }

        /// <summary>
        /// Gets or sets the predefined type of the line-item marker for the object.
        /// </summary>
        [DOM("listStyleType")]
        public String ListStyleType
        {
            get { return GetPropertyValue(PropertyNames.LIST_STYLE_TYPE) ?? String.Empty; }
            set { SetProperty(PropertyNames.LIST_STYLE_TYPE, value); }
        }

        /// <summary>
        /// Gets or sets the width of the top, right, bottom, and left margins of the object.
        /// </summary>
        [DOM("margin")]
        public String Margin
        {
            get { return GetPropertyValue(PropertyNames.MARGIN) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARGIN, value); }
        }

        /// <summary>
        /// Gets or sets the height of the bottom margin of the object.
        /// </summary>
        [DOM("marginBottom")]
        public String MarginBottom
        {
            get { return GetPropertyValue(PropertyNames.MARGIN_BOTTOM) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARGIN_BOTTOM, value); }
        }

        /// <summary>
        /// Gets or sets the width of the left margin of the object.
        /// </summary>
        [DOM("marginLeft")]
        public String MarginLeft
        {
            get { return GetPropertyValue(PropertyNames.MARGIN_LEFT) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARGIN_LEFT, value); }
        }

        /// <summary>
        /// Gets or sets the width of the right margin of the object.
        /// </summary>
        [DOM("marginRight")]
        public String MarginRight
        {
            get { return GetPropertyValue(PropertyNames.MARGIN_RIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARGIN_RIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the height of the top margin of the object.
        /// </summary>
        [DOM("marginTop")]
        public String MarginTop
        {
            get { return GetPropertyValue(PropertyNames.MARGIN_TOP) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARGIN_TOP, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        [DOM("marker")]
        public String Marker
        {
            get { return GetPropertyValue(PropertyNames.MARKER) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARKER, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the final vertex of a given path element or
        /// basic shape.
        /// </summary>
        [DOM("markerEnd")]
        public String MarkerEnd
        {
            get { return GetPropertyValue(PropertyNames.MARKER_END) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARKER_END, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        [DOM("markerMid")]
        public String MarkerMid
        {
            get { return GetPropertyValue(PropertyNames.MARKER_MID) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARKER_MID, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the first vertex of a given path element or
        /// basic shape.
        /// </summary>
        [DOM("markerStart")]
        public String MarkerStart
        {
            get { return GetPropertyValue(PropertyNames.MARKER_START) ?? String.Empty; }
            set { SetProperty(PropertyNames.MARKER_START, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a SVG mask.
        /// </summary>
        [DOM("mask")]
        public String Mask
        {
            get { return GetPropertyValue(PropertyNames.MASK) ?? String.Empty; }
            set { SetProperty(PropertyNames.MASK, value); }
        }

        /// <summary>
        /// Gets or sets the maximum height for an element.
        /// </summary>
        [DOM("maxHeight")]
        public String MaxHeight
        {
            get { return GetPropertyValue(PropertyNames.MAX_HEIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.MAX_HEIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the maximum width for an element.
        /// </summary>
        [DOM("maxWidth")]
        public String MaxWidth
        {
            get { return GetPropertyValue(PropertyNames.MAX_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.MAX_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets the minimum height for an element.
        /// </summary>
        [DOM("minHeight")]
        public String MinHeight
        {
            get { return GetPropertyValue(PropertyNames.MIN_HEIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.MIN_HEIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width for an element.
        /// </summary>
        [DOM("minWidth")]
        public String MinWidth
        {
            get { return GetPropertyValue(PropertyNames.MIN_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.MIN_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies object or group opacity in CSS or SVG.
        /// </summary>
        [DOM("opacity")]
        public String Opacity
        {
            get { return GetPropertyValue(PropertyNames.OPACITY) ?? String.Empty; }
            set { SetProperty(PropertyNames.OPACITY, value); }
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
            get { return GetPropertyValue(PropertyNames.ORDER) ?? String.Empty; }
            set { SetProperty(PropertyNames.ORDER, value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph
        /// that must appear at the bottom of a page.
        /// </summary>
        [DOM("orphans")]
        public String Orphans
        {
            get { return GetPropertyValue(PropertyNames.ORPHANS) ?? String.Empty; }
            set { SetProperty(PropertyNames.ORPHANS, value); }
        }

        /// <summary>
        /// Gets or sets the outline frame.
        /// </summary>
        [DOM("outline")]
        public String Outline
        {
            get { return GetPropertyValue(PropertyNames.OUTLINE) ?? String.Empty; }
            set { SetProperty(PropertyNames.OUTLINE, value); }
        }

        /// <summary>
        /// Gets or sets the color of the outline frame.
        /// </summary>
        [DOM("outlineColor")]
        public String OutlineColor
        {
            get { return GetPropertyValue(PropertyNames.OUTLINE_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.OUTLINE_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the style of the outline frame.
        /// </summary>
        [DOM("outlineStyle")]
        public String OutlineStyle
        {
            get { return GetPropertyValue(PropertyNames.OUTLINE_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.OUTLINE_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets the width of the outline frame.
        /// </summary>
        [DOM("outlineWidth")]
        public String OutlineWidth
        {
            get { return GetPropertyValue(PropertyNames.OUTLINE_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.OUTLINE_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        [DOM("overflow")]
        public String Overflow
        {
            get { return GetPropertyValue(PropertyNames.OVERFLOW) ?? String.Empty; }
            set { SetProperty(PropertyNames.OVERFLOW, value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        [DOM("overflowX")]
        public String OverflowX
        {
            get { return GetPropertyValue(PropertyNames.OVERFLOW_X) ?? String.Empty; }
            set { SetProperty(PropertyNames.OVERFLOW_X, value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when
        /// the content exceeds the height of the object.
        /// </summary>
        [DOM("overflowY")]
        public String OverflowY
        {
            get { return GetPropertyValue(PropertyNames.OVERFLOW_Y) ?? String.Empty; }
            set { SetProperty(PropertyNames.OVERFLOW_Y, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its border.
        /// </summary>
        [DOM("padding")]
        public String Padding
        {
            get { return GetPropertyValue(PropertyNames.PADDING) ?? String.Empty; }
            set { SetProperty(PropertyNames.PADDING, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingBottom")]
        public String PaddingBottom
        {
            get { return GetPropertyValue(PropertyNames.PADDING_BOTTOM) ?? String.Empty; }
            set { SetProperty(PropertyNames.PADDING_BOTTOM, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingLeft")]
        public String PaddingLeft
        {
            get { return GetPropertyValue(PropertyNames.PADDING_LEFT) ?? String.Empty; }
            set { SetProperty(PropertyNames.PADDING_LEFT, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between
        /// the right border of the object and the content.
        /// </summary>
        [DOM("paddingRight")]
        public String PaddingRight
        {
            get { return GetPropertyValue(PropertyNames.PADDING_RIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.PADDING_RIGHT, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the top
        /// border of the object and the content.
        /// </summary>
        [DOM("paddingTop")]
        public String PaddingTop
        {
            get { return GetPropertyValue(PropertyNames.PADDING_TOP) ?? String.Empty; }
            set { SetProperty(PropertyNames.PADDING_TOP, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a page break occurs after the object.
        /// </summary>
        [DOM("pageBreakAfter")]
        public String PageBreakAfter
        {
            get { return GetPropertyValue(PropertyNames.PAGE_BREAK_AFTER) ?? String.Empty; }
            set { SetProperty(PropertyNames.PAGE_BREAK_AFTER, value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break occurs before the object.
        /// </summary>
        [DOM("pageBreakBefore")]
        public String PageBreakBefore
        {
            get { return GetPropertyValue(PropertyNames.PAGE_BREAK_BEFORE) ?? String.Empty; }
            set { SetProperty(PropertyNames.PAGE_BREAK_BEFORE, value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break is
        /// allowed to occur inside the object.
        /// </summary>
        [DOM("pageBreakInside")]
        public String PageBreakInside
        {
            get { return GetPropertyValue(PropertyNames.PAGE_BREAK_INSIDE) ?? String.Empty; }
            set { SetProperty(PropertyNames.PAGE_BREAK_INSIDE, value); }
        }

        /// <summary>
        /// Gets or sets a value that represents the perspective from which all child
        /// elements of the object are viewed.
        /// </summary>
        [DOM("perspective")]
        public String Perspective
        {
            get { return GetPropertyValue(PropertyNames.PERSPECTIVE) ?? String.Empty; }
            set { SetProperty(PropertyNames.PERSPECTIVE, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        [DOM("perspectiveOrigin")]
        public String PerspectiveOrigin
        {
            get { return GetPropertyValue(PropertyNames.PERSPECTIVE_ORIGIN) ?? String.Empty; }
            set { SetProperty(PropertyNames.PERSPECTIVE_ORIGIN, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies under what circumstances a given graphics
        /// element can be the target element for a pointer event in SVG.
        /// </summary>
        [DOM("pointerEvents")]
        public String PointerEvents
        {
            get { return GetPropertyValue(PropertyNames.POINTER_EVENTS) ?? String.Empty; }
            set { SetProperty(PropertyNames.POINTER_EVENTS, value); }
        }

        /// <summary>
        /// Gets or sets the type of positioning used for the object.
        /// </summary>
        [DOM("position")]
        public String Position
        {
            get { return GetPropertyValue(PropertyNames.POSITION) ?? String.Empty; }
            set { SetProperty(PropertyNames.POSITION, value); }
        }

        /// <summary>
        /// Gets or sets the pairs of strings to be used as quotes in generated content.
        /// </summary>
        [DOM("quotes")]
        public String Quotes
        {
            get { return GetPropertyValue(PropertyNames.QUOTES) ?? String.Empty; }
            set { SetProperty(PropertyNames.QUOTES, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the right edge of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("right")]
        public String Right
        {
            get { return GetPropertyValue(PropertyNames.RIGHT) ?? String.Empty; }
            set { SetProperty(PropertyNames.RIGHT, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the ruby text content.
        /// </summary>
        [DOM("rubyAlign")]
        public String RubyAlign
        {
            get { return GetPropertyValue(PropertyNames.RUBY_ALIGN) ?? String.Empty; }
            set { SetProperty(PropertyNames.RUBY_ALIGN, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether, and on which side, ruby
        /// text is allowed to partially overhang any adjacent text in addition
        /// to its own base, when the ruby text is wider than the ruby base
        /// </summary>
        [DOM("rubyOverhang")]
        public String RubyOverhang
        {
            get { return GetPropertyValue(PropertyNames.RUBY_OVERHANG) ?? String.Empty; }
            set { SetProperty(PropertyNames.RUBY_OVERHANG, value); }
        }

        /// <summary>
        /// Gets or sets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        [DOM("rubyPosition")]
        public String RubyPosition
        {
            get { return GetPropertyValue(PropertyNames.RUBY_POSITION) ?? String.Empty; }
            set { SetProperty(PropertyNames.RUBY_POSITION, value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbar3dLightColor")]
        public String Scrollbar3dLightColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR3D_LIGHT_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR3D_LIGHT_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the color of the arrow elements of a scroll arrow.
        /// </summary>
        [DOM("scrollbarArrowColor")]
        public String ScrollbarArrowColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR_ARROW_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR_ARROW_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the color of the gutter of a scroll bar.
        /// </summary>
        [DOM("scrollbarDarkShadowColor")]
        public String ScrollbarDarkShadowColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR_DARK_SHADOW_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR_DARK_SHADOW_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the color of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarFaceColor")]
        public String ScrollbarFaceColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR_FACE_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR_FACE_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarHighlightColor")]
        public String ScrollbarHighlightColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR_HIGHLIGHT_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR_HIGHLIGHT_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the color of the bottom and right edges of the
        /// scroll box and scroll arrows of a scroll bar.
        /// </summary>
        [DOM("scrollbarShadowColor")]
        public String ScrollbarShadowColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR_SHADOW_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR_SHADOW_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets the color of the track element of a scroll bar.
        /// </summary>
        [DOM("scrollbarTrackColor")]
        public String ScrollbarTrackColor
        {
            get { return GetPropertyValue(PropertyNames.SCROLLBAR_TRACK_COLOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.SCROLLBAR_TRACK_COLOR, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint along
        /// the outline of a given graphical element.
        /// </summary>
        [DOM("stroke")]
        public String Stroke
        {
            get { return GetPropertyValue(PropertyNames.STROKE) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that indicate the pattern of
        /// dashes and gaps used to stroke paths.
        /// </summary>
        [DOM("strokeDasharray")]
        public String StrokeDasharray
        {
            get { return GetPropertyValue(PropertyNames.STROKE_DASHARRAY) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_DASHARRAY, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the distance into the
        /// dash pattern to start the dash.
        /// </summary>
        [DOM("strokeDashoffset")]
        public String StrokeDashoffset
        {
            get { return GetPropertyValue(PropertyNames.STROKE_DASHOFFSET) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_DASHOFFSET, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the
        /// end of open subpaths when they are stroked.
        /// </summary>
        [DOM("strokeLinecap")]
        public String StrokeLinecap
        {
            get { return GetPropertyValue(PropertyNames.STROKE_LINECAP) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_LINECAP, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the corners of
        /// paths or basic shapes when they are stroked.
        /// </summary>
        [DOM("strokeLinejoin")]
        public String StrokeLinejoin
        {
            get { return GetPropertyValue(PropertyNames.STROKE_LINEJOIN) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_LINEJOIN, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin property).
        /// </summary>
        [DOM("strokeMiterlimit")]
        public String StrokeMiterlimit
        {
            get { return GetPropertyValue(PropertyNames.STROKE_MITERLIMIT) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_MITERLIMIT, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation
        /// that is used to stroke the current object.
        /// </summary>
        [DOM("strokeOpacity")]
        public String StrokeOpacity
        {
            get { return GetPropertyValue(PropertyNames.STROKE_OPACITY) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_OPACITY, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the width of the stroke on the current object.
        /// </summary>
        [DOM("strokeWidth")]
        public String StrokeWidth
        {
            get { return GetPropertyValue(PropertyNames.STROKE_WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.STROKE_WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the table layout is fixed.
        /// </summary>
        [DOM("tableLayout")]
        public String TableLayout
        {
            get { return GetPropertyValue(PropertyNames.TABLE_LAYOUT) ?? String.Empty; }
            set { SetProperty(PropertyNames.TABLE_LAYOUT, value); }
        }

        /// <summary>
        /// Gets or sets whether the text in the object is left-aligned, right-aligned, 
        /// centered, or justified.
        /// </summary>
        [DOM("textAlign")]
        public String TextAlign
        {
            get { return GetPropertyValue(PropertyNames.TEXT_ALIGN) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_ALIGN, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the last line or only
        /// line of text in the specified object.
        /// </summary>
        [DOM("textAlignLast")]
        public String TextAlignLast
        {
            get { return GetPropertyValue(PropertyNames.TEXT_ALIGN_LAST) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_ALIGN_LAST, value); }
        }

        /// <summary>
        /// Aligns a string of text relative to the specified point.
        /// </summary>
        [DOM("textAnchor")]
        public String TextAnchor
        {
            get { return GetPropertyValue(PropertyNames.TEXT_ANCHOR) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_ANCHOR, value); }
        }

        /// <summary>
        /// Gets or sets the autospacing and narrow space width adjustment of text.
        /// </summary>
        [DOM("textAutospace")]
        public String TextAutospace
        {
            get { return GetPropertyValue(PropertyNames.TEXT_AUTOSPACE) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_AUTOSPACE, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        [DOM("textDecoration")]
        public String TextDecoration
        {
            get { return GetPropertyValue(PropertyNames.TEXT_DECORATION) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_DECORATION, value); }
        }

        /// <summary>
        /// Gets or sets the indentation of the first line of text in the object.
        /// </summary>
        [DOM("textIndent")]
        public String TextIndent
        {
            get { return GetPropertyValue(PropertyNames.TEXT_INDENT) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_INDENT, value); }
        }

        /// <summary>
        /// Gets or sets the type of alignment used to justify text in the object.
        /// </summary>
        [DOM("textJustify")]
        public String TextJustify
        {
            get { return GetPropertyValue(PropertyNames.TEXT_JUSTIFY) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_JUSTIFY, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether to render
        /// ellipses (...) to indicate text overflow.
        /// </summary>
        [DOM("textOverflow")]
        public String TextOverflow
        {
            get { return GetPropertyValue(PropertyNames.TEXT_OVERFLOW) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_OVERFLOW, value); }
        }

        /// <summary>
        /// Gets or sets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        [DOM("textShadow")]
        public String TextShadow
        {
            get { return GetPropertyValue(PropertyNames.TEXT_SHADOW) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_SHADOW, value); }
        }

        /// <summary>
        /// Gets or sets the rendering of the text in the object.
        /// </summary>
        [DOM("textTransform")]
        public String TextTransform
        {
            get { return GetPropertyValue(PropertyNames.TEXT_TRANSFORM) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_TRANSFORM, value); }
        }

        /// <summary>
        /// Gets or sets the position of the underline decoration that is set through the
        /// text-decoration property of the object.
        /// </summary>
        [DOM("textUnderlinePosition")]
        public String TextUnderlinePosition
        {
            get { return GetPropertyValue(PropertyNames.TEXT_UNDERLINE_POSITION) ?? String.Empty; }
            set { SetProperty(PropertyNames.TEXT_UNDERLINE_POSITION, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        [DOM("top")]
        public String Top
        {
            get { return GetPropertyValue(PropertyNames.TOP) ?? String.Empty; }
            set { SetProperty(PropertyNames.TOP, value); }
        }

        /// <summary>
        /// Gets or sets a list of one or more transform functions that specify how
        /// to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        [DOM("transform")]
        public String Transform
        {
            get { return GetPropertyValue(PropertyNames.TRANSFORM) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSFORM, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that establish the origin of transformation for an element.
        /// </summary>
        [DOM("transformOrigin")]
        public String TransformOrigin
        {
            get { return GetPropertyValue(PropertyNames.TRANSFORM_ORIGIN) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSFORM_ORIGIN, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        [DOM("transformStyle")]
        public String TransformStyle
        {
            get { return GetPropertyValue(PropertyNames.TRANSFORM_STYLE) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSFORM_STYLE, value); }
        }

        /// <summary>
        /// Gets or sets one or more shorthand values that specify the transition properties
        /// for a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        [DOM("transition")]
        public String Transition
        {
            get { return GetPropertyValue(PropertyNames.TRANSITION) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSITION, value); }
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
            get { return GetPropertyValue(PropertyNames.TRANSITION_DELAY) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSITION_DELAY, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the durations of transitions on
        /// a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        [DOM("transitionDuration")]
        public String TransitionDuration
        {
            get { return GetPropertyValue(PropertyNames.TRANSITION_DURATION) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSITION_DURATION, value); }
        }

        /// <summary>
        /// Gets or sets a value that identifies the CSS property name or names to which
        /// the transition effect (defined by the transition-duration, transition-timing-function,
        /// and transition-delay properties) is applied when a new property value is specified.
        /// </summary>
        [DOM("transitionProperty")]
        public String TransitionProperty
        {
            get { return GetPropertyValue(PropertyNames.TRANSITION_PROPERTY) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSITION_PROPERTY, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the intermediate property values to be
        /// used during a transition on a set of corresponding object properties identified
        /// in the transition-property property.
        /// </summary>
        [DOM("transitionTimingFunction")]
        public String TransitionTimingFunction
        {
            get { return GetPropertyValue(PropertyNames.TRANSITION_TIMING_FUNCTION) ?? String.Empty; }
            set { SetProperty(PropertyNames.TRANSITION_TIMING_FUNCTION, value); }
        }

        /// <summary>
        /// Gets or sets the level of embedding with respect to the bidirectional algorithm.
        /// </summary>
        [DOM("unicodeBidi")]
        public String UnicodeBidi
        {
            get { return GetPropertyValue(PropertyNames.UNICODE_BIDI) ?? String.Empty; }
            set { SetProperty(PropertyNames.UNICODE_BIDI, value); }
        }

        /// <summary>
        /// Gets or sets the vertical alignment of the object.
        /// </summary>
        [DOM("verticalAlign")]
        public String VerticalAlign
        {
            get { return GetPropertyValue(PropertyNames.VERTICAL_ALIGN) ?? String.Empty; }
            set { SetProperty(PropertyNames.VERTICAL_ALIGN, value); }
        }

        /// <summary>
        /// Gets or sets whether the content of the object is displayed.
        /// </summary>
        [DOM("visibility")]
        public String Visibility
        {
            get { return GetPropertyValue(PropertyNames.VISIBILITY) ?? String.Empty; }
            set { SetProperty(PropertyNames.VISIBILITY, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        [DOM("whiteSpace")]
        public String WhiteSpace
        {
            get { return GetPropertyValue(PropertyNames.WHITE_SPACE) ?? String.Empty; }
            set { SetProperty(PropertyNames.WHITE_SPACE, value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        [DOM("widows")]
        public String Widows
        {
            get { return GetPropertyValue(PropertyNames.WIDOWS) ?? String.Empty; }
            set { SetProperty(PropertyNames.WIDOWS, value); }
        }

        /// <summary>
        /// Gets or sets the width of the object.
        /// </summary>
        [DOM("width")]
        public String Width
        {
            get { return GetPropertyValue(PropertyNames.WIDTH) ?? String.Empty; }
            set { SetProperty(PropertyNames.WIDTH, value); }
        }

        /// <summary>
        /// Gets or sets line-breaking behavior within words, particularly where
        /// multiple languages appear in the object.
        /// </summary>
        [DOM("wordBreak")]
        public String WordBreak
        {
            get { return GetPropertyValue(PropertyNames.WORD_BREAK) ?? String.Empty; }
            set { SetProperty(PropertyNames.WORD_BREAK, value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between words in the object.
        /// </summary>
        [DOM("wordSpacing")]
        public String WordSpacing
        {
            get { return GetPropertyValue(PropertyNames.WORD_SPACING) ?? String.Empty; }
            set { SetProperty(PropertyNames.WORD_SPACING, value); }
        }

        /// <summary>
        /// Gets or sets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        [DOM("wordWrap")]
        public String WordWrap
        {
            get { return GetPropertyValue(PropertyNames.WORD_WRAP) ?? String.Empty; }
            set { SetProperty(PropertyNames.WORD_WRAP, value); }
        }

        /// <summary>
        /// Gets or sets the direction and flow of the content in the object.
        /// </summary>
        [DOM("writingMode")]
        public String WritingMode
        {
            get { return GetPropertyValue(PropertyNames.WRITING_MODE) ?? String.Empty; }
            set { SetProperty(PropertyNames.WRITING_MODE, value); }
        }

        /// <summary>
        /// Gets or sets the stacking order of positioned objects.
        /// </summary>
        [DOM("zIndex")]
        public String ZIndex
        {
            get { return GetPropertyValue(PropertyNames.Z_INDEX) ?? String.Empty; }
            set { SetProperty(PropertyNames.Z_INDEX, value); }
        }

        /// <summary>
        /// Gets or sets the magnification scale of the object.
        /// </summary>
        [DOM("zoom")]
        public String Zoom
        {
            get { return GetPropertyValue(PropertyNames.ZOOM) ?? String.Empty; }
            set { SetProperty(PropertyNames.ZOOM, value); }
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
            var value = RemovePropertyFromList(_rules, propertyName);

            if (value != null)
            {
                Propagate();
                return value.CssText;
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
                RemovePropertyFromList(_rules, propertyName);
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
                _rules.Clear();
                CssParser.AppendDeclarations(this, value ?? String.Empty);
            }
        }

        #endregion

        #region Helpers

        static CSSValue RemovePropertyFromList(List<CSSProperty> rules, String propertyName)
        {
            for (int i = 0; i < rules.Count; i++)
            {
                if (rules[i].Name.Equals(propertyName))
                {
                    var value = rules[i].Value;
                    rules.RemoveAt(i);
                    return value;
                }
            }

            return null;
        }

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

            for (int i = 0; i < _rules.Count; i++)
                sb.Append(_rules[i].ToCss()).Append(Specification.SC);

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
