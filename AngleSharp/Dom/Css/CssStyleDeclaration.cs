namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    sealed class CssStyleDeclaration : ICssStyleDeclaration, IPropertyCreator
    {
        #region Fields

        readonly List<CssProperty> _declarations;
        readonly Boolean _readOnly;
        readonly CssRule _parent;
        readonly IPropertyCreator _creator;

        #endregion

        #region Events

        public event EventHandler Changed;

        #endregion

        #region ctor

        CssStyleDeclaration(Boolean readOnly, CssRule parent)
        {
            _readOnly = readOnly;
            _parent = parent;
            _creator = parent as IPropertyCreator ?? this;
            _declarations = new List<CssProperty>();
        }

        /// <summary>
        /// Creates a new CSS style declaration with no parent.
        /// </summary>
        /// <param name="source">The source to start with.</param>
        internal CssStyleDeclaration(String source = null)
            : this(false, null)
        {
            Update(source);
        }

        /// <summary>
        /// Creates a new CSS style declaration.
        /// </summary>
        /// <param name="parent">The parent of the style declaration.</param>
        internal CssStyleDeclaration(CssRule parent)
            : this(false, parent)
        {
        }

        /// <summary>
        /// Creates a new read-only CSS style declaration.
        /// </summary>
        /// <param name="properties">The properties to show.</param>
        internal CssStyleDeclaration(IEnumerable<CssProperty> properties)
            : this(true, null)
        {
            foreach (var property in properties)
                _declarations.Add(property);
        }

        #endregion

        #region General Properties

        /// <summary>
        /// Gets or sets the textual representation of the declaration block.
        /// </summary>
        public String CssText
        {
            get
            {
                var list = new List<String>();
                var serialized = new List<String>();

                foreach (var declaration in _declarations)
                {
                    var property = declaration.Name;

                    if (serialized.Contains(property))
                        continue;

                    var shorthands = Factory.Properties.GetShorthands(property);

                    if (shorthands.Any())
                    {
                        var longhands = _declarations.Where(m => !serialized.Contains(m.Name)).ToList();

                        foreach (var shorthand in shorthands.OrderByDescending(m => Factory.Properties.GetLonghands(m).Count()))
                        {
                            var properties = Factory.Properties.GetLonghands(shorthand);
                            var currentLonghands = longhands.Where(m => properties.Contains(m.Name)).ToList();

                            if (currentLonghands.Count == 0)
                                continue;

                            var important = currentLonghands.Count(m => m.IsImportant);

                            if (important > 0 && important != currentLonghands.Count)
                                continue;

                            var rule = Factory.Properties.CreateShorthand(shorthand, this);
                            var value = rule.SerializeValue(currentLonghands);

                            if (String.IsNullOrEmpty(value))
                                continue;

                            value = CssProperty.Serialize(shorthand, value, important != 0);
                            list.Add(value);

                            foreach (var longhand in currentLonghands)
                            {
                                serialized.Add(longhand.Name);
                                longhands.Remove(longhand);
                            }
                        }
                    }

                    if (serialized.Contains(property))
                        continue;

                    list.Add(declaration.CssText);
                    serialized.Add(property);
                }

                return String.Join(" ", list);
            }
            set
            {
                if (_readOnly)
                    throw new DomException(DomError.NoModificationAllowed);

                Update(value);
                RaiseChanged();
            }
        }

        /// <summary>
        /// Gets if the style declaration is read-only and must not be modified.
        /// </summary>
        public Boolean IsReadOnly
        {
            get { return _readOnly; }
        }

        /// <summary>
        /// Gets the number of properties in the declaration.
        /// </summary>
        public Int32 Length
        {
            get { return _declarations.Count; }
        }

        /// <summary>
        /// Gets the containing CSSRule.
        /// </summary>
        public ICssRule Parent
        {
            get { return _parent; }
        }

        /// <summary>
        /// Gets the name of the property at the given index.
        /// </summary>
        /// <param name="index">The index of the property to get.</param>
        /// <returns>The name of the property at the given index or null.</returns>
        public String this[Int32 index]
        {
            get
            {
                if (index >= 0 && index < _declarations.Count)
                    return _declarations[index].Name;

                return null;
            }
        }

        internal IEnumerable<CssProperty> Declarations 
        {
            get { return _declarations; }
        }

        #endregion

        #region CSS Properties

        /// <summary>
        /// Gets or sets how a flex item's lines align within the flex container when there
        /// is extra space along the axis that is perpendicular to the axis defined by the
        /// flex-direction property.
        /// </summary>
        String ICssStyleDeclaration.AlignContent
        {
            get { return GetPropertyValue(PropertyNames.AlignContent); }
            set { SetProperty(PropertyNames.AlignContent, value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout axis
        /// defined by the flex-direction property) of flex items of the flex container.
        /// </summary>
        String ICssStyleDeclaration.AlignItems
        {
            get { return GetPropertyValue(PropertyNames.AlignItems); }
            set { SetProperty(PropertyNames.AlignItems, value); }
        }

        /// <summary>
        /// Gets or sets the alignment value (perpendicular to the layout
        /// axis defined by the flex-direction property) of flex items of
        /// the flex container.
        /// </summary>
        String ICssStyleDeclaration.AlignSelf
        {
            get { return GetPropertyValue(PropertyNames.AlignSelf); }
            set { SetProperty(PropertyNames.AlignSelf, value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the object represents a
        /// keyboard shortcut.
        /// </summary>
        String ICssStyleDeclaration.Accelerator
        {
            get { return GetPropertyValue(PropertyNames.Accelerator); }
            set { SetProperty(PropertyNames.Accelerator, value); }
        }

        /// <summary>
        /// Gets or sets which baseline of this element is to be aligned
        /// with the corresponding baseline of the parent.
        /// </summary>
        String ICssStyleDeclaration.AlignmentBaseline
        {
            get { return GetPropertyValue(PropertyNames.AlignBaseline); }
            set { SetProperty(PropertyNames.AlignBaseline, value); }
        }

        /// <summary>
        /// Gets or sets one or more shorthand values that define all animation
        /// properties (except animation-play-state) for a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by the
        /// animations-name property.
        /// </summary>
        String ICssStyleDeclaration.Animation
        {
            get { return GetPropertyValue(PropertyNames.Animation); }
            set { SetProperty(PropertyNames.Animation, value); }
        }

        /// <summary>
        /// Gets or sets the offset within an animation cycle
        /// (the amount of time from the start of a cycle) before
        /// the animation is displayed for a set of corresponding
        /// object properties identified in the CSS @keyframes at-rule
        /// specified by the animation-name property.
        /// </summary>
        String ICssStyleDeclaration.AnimationDelay
        {
            get { return GetPropertyValue(PropertyNames.AnimationDelay); }
            set { SetProperty(PropertyNames.AnimationDelay, value); }
        }

        /// <summary>
        /// Gets or sets the direction of play for an animation cycle.
        /// </summary>
        String ICssStyleDeclaration.AnimationDirection
        {
            get { return GetPropertyValue(PropertyNames.AnimationDirection); }
            set { SetProperty(PropertyNames.AnimationDirection, value); }
        }

        /// <summary>
        /// Gets or sets the length of time to complete one cycle of the animation.
        /// </summary>
        String ICssStyleDeclaration.AnimationDuration
        {
            get { return GetPropertyValue(PropertyNames.AnimationDuration); }
            set { SetProperty(PropertyNames.AnimationDuration, value); }
        }

        /// <summary>
        /// Gets or sets whether the effects of an animation are visible before or after it plays.
        /// </summary>
        String ICssStyleDeclaration.AnimationFillMode
        {
            get { return GetPropertyValue(PropertyNames.AnimationFillMode); }
            set { SetProperty(PropertyNames.AnimationFillMode, value); }
        }

        /// <summary>
        /// Gets or sets the number of times an animation cycle is played.
        /// </summary>
        String ICssStyleDeclaration.AnimationIterationCount
        {
            get { return GetPropertyValue(PropertyNames.AnimationIterationCount); }
            set { SetProperty(PropertyNames.AnimationIterationCount, value); }
        }

        /// <summary>
        /// Gets or sets one or more animation names. An animation name
        /// selects a CSS @keyframes at-rule.
        /// </summary>
        String ICssStyleDeclaration.AnimationName
        {
            get { return GetPropertyValue(PropertyNames.AnimationName); }
            set { SetProperty(PropertyNames.AnimationName, value); }
        }

        /// <summary>
        /// Gets or sets whether an animation is playing or paused.
        /// </summary>
        String ICssStyleDeclaration.AnimationPlayState
        {
            get { return GetPropertyValue(PropertyNames.AnimationPlayState); }
            set { SetProperty(PropertyNames.AnimationPlayState, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate property values to be used during a
        /// single cycle of an animation on a set of corresponding object
        /// properties identified in the CSS @keyframes at-rule specified by
        /// the animation-name property.
        /// </summary>
        String ICssStyleDeclaration.AnimationTimingFunction
        {
            get { return GetPropertyValue(PropertyNames.AnimationTimingFunction); }
            set { SetProperty(PropertyNames.AnimationTimingFunction, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether the back face
        /// (reverse side) of an object is visible.
        /// </summary>
        String ICssStyleDeclaration.BackfaceVisibility
        {
            get { return GetPropertyValue(PropertyNames.BackfaceVisibility); }
            set { SetProperty(PropertyNames.BackfaceVisibility, value); }
        }

        /// <summary>
        /// Gets or sets up to five separate background properties of an object.
        /// </summary>
        String ICssStyleDeclaration.Background
        {
            get { return GetPropertyValue(PropertyNames.Background); }
            set { SetProperty(PropertyNames.Background, value); }
        }

        /// <summary>
        /// Gets or sets how the background image (or images) is attached
        /// to the object within the document.
        /// </summary>
        String ICssStyleDeclaration.BackgroundAttachment
        {
            get { return GetPropertyValue(PropertyNames.BackgroundAttachment); }
            set { SetProperty(PropertyNames.BackgroundAttachment, value); }
        }

        /// <summary>
        /// Gets or sets the background painting area or areas relative to the
        /// element's bounding boxes.
        /// </summary>
        String ICssStyleDeclaration.BackgroundClip
        {
            get { return GetPropertyValue(PropertyNames.BackgroundClip); }
            set { SetProperty(PropertyNames.BackgroundClip, value); }
        }

        /// <summary>
        /// Gets or sets the color behind the content of the object.
        /// </summary>
        String ICssStyleDeclaration.BackgroundColor
        {
            get { return GetPropertyValue(PropertyNames.BackgroundColor); }
            set { SetProperty(PropertyNames.BackgroundColor, value); }
        }

        /// <summary>
        /// Gets or sets the background image or images of the object.
        /// </summary>
        String ICssStyleDeclaration.BackgroundImage
        {
            get { return GetPropertyValue(PropertyNames.BackgroundImage); }
            set { SetProperty(PropertyNames.BackgroundImage, value); }
        }

        /// <summary>
        /// Gets or sets the positioning area of an element or multiple elements.
        /// </summary>
        String ICssStyleDeclaration.BackgroundOrigin
        {
            get { return GetPropertyValue(PropertyNames.BackgroundOrigin); }
            set { SetProperty(PropertyNames.BackgroundOrigin, value); }
        }

        /// <summary>
        /// Gets or sets the position of the background of the object.
        /// </summary>
        String ICssStyleDeclaration.BackgroundPosition
        {
            get { return GetPropertyValue(PropertyNames.BackgroundPosition); }
            set { SetProperty(PropertyNames.BackgroundPosition, value); }
        }

        /// <summary>
        /// Gets or sets the x-coordinate of the background-position property.
        /// </summary>
        String ICssStyleDeclaration.BackgroundPositionX
        {
            get { return GetPropertyValue(PropertyNames.BackgroundPositionX); }
            set { SetProperty(PropertyNames.BackgroundPositionX, value); }
        }

        /// <summary>
        /// Gets or sets the y-coordinate of the background-position property.
        /// </summary>
        String ICssStyleDeclaration.BackgroundPositionY
        {
            get { return GetPropertyValue(PropertyNames.BackgroundPositionY); }
            set { SetProperty(PropertyNames.BackgroundPositionY, value); }
        }

        /// <summary>
        /// Gets or sets whether and how the background image (or images) is tiled.
        /// </summary>
        String ICssStyleDeclaration.BackgroundRepeat
        {
            get { return GetPropertyValue(PropertyNames.BackgroundRepeat); }
            set { SetProperty(PropertyNames.BackgroundRepeat, value); }
        }

        /// <summary>
        /// Gets or sets the size of the background images.
        /// </summary>
        String ICssStyleDeclaration.BackgroundSize
        {
            get { return GetPropertyValue(PropertyNames.BackgroundSize); }
            set { SetProperty(PropertyNames.BackgroundSize, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the dominant baseline
        /// should be repositioned relative to the dominant baseline of the
        /// parent text content element.
        /// </summary>
        String ICssStyleDeclaration.BaselineShift
        {
            get { return GetPropertyValue(PropertyNames.BaselineShift); }
            set { SetProperty(PropertyNames.BaselineShift, value); }
        }

        /// <summary>
        /// Gets or sets the location of the Dynamic HTML (DHTML) behaviorDHTML Behaviors.
        /// </summary>
        String ICssStyleDeclaration.Behavior
        {
            get { return GetPropertyValue(PropertyNames.Behavior); }
            set { SetProperty(PropertyNames.Behavior, value); }
        }

        /// <summary>
        /// Gets or sets the properties of a border drawn around an object.
        /// </summary>
        String ICssStyleDeclaration.Border
        {
            get { return GetPropertyValue(PropertyNames.Border); }
            set { SetProperty(PropertyNames.Border, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the bottom border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderBottom
        {
            get { return GetPropertyValue(PropertyNames.BorderBottom); }
            set { SetProperty(PropertyNames.BorderBottom, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the bottom border of an object.
        /// </summary>
        String ICssStyleDeclaration.BorderBottomColor
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomColor); }
            set { SetProperty(PropertyNames.BorderBottomColor, value); }
        }

        /// <summary>
        /// Gets or sets the radii of the quarter ellipse that defines
        /// the shape of the lower-left corner for the outer border edge of the current box.
        /// </summary>
        String ICssStyleDeclaration.BorderBottomLeftRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomLeftRadius); }
            set { SetProperty(PropertyNames.BorderBottomLeftRadius, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the lower-right corner
        /// for the outer border edge of the current box.
        /// </summary>
        String ICssStyleDeclaration.BorderBottomRightRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomRightRadius); }
            set { SetProperty(PropertyNames.BorderBottomRightRadius, value); }
        }

        /// <summary>
        /// Gets or sets the style of the bottom border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderBottomStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomStyle); }
            set { SetProperty(PropertyNames.BorderBottomStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the bottom border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderBottomWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderBottomWidth); }
            set { SetProperty(PropertyNames.BorderBottomWidth, value); }
        }

        /// <summary>
        /// Gets or sets whether the row and cell borders of a table are joined in a
        /// single border or detached as in standard HTML.
        /// </summary>
        String ICssStyleDeclaration.BorderCollapse
        {
            get { return GetPropertyValue(PropertyNames.BorderCollapse); }
            set { SetProperty(PropertyNames.BorderCollapse, value); }
        }

        /// <summary>
        /// Gets or sets the border color of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderColor
        {
            get { return GetPropertyValue(PropertyNames.BorderColor); }
            set { SetProperty(PropertyNames.BorderColor, value); }
        }

        /// <summary>
        /// Gets or sets an image to be used in place of the border styles.
        /// </summary>
        String ICssStyleDeclaration.BorderImage
        {
            get { return GetPropertyValue(PropertyNames.BorderImage); }
            set { SetProperty(PropertyNames.BorderImage, value); }
        }

        /// <summary>
        /// Gets or sets the amount by which the border image area extends beyond the border box.
        /// </summary>
        String ICssStyleDeclaration.BorderImageOutset
        {
            get { return GetPropertyValue(PropertyNames.BorderImageOutset); }
            set { SetProperty(PropertyNames.BorderImageOutset, value); }
        }

        /// <summary>
        /// Gets or sets ow the image is scaled and tiled.
        /// </summary>
        String ICssStyleDeclaration.BorderImageRepeat
        {
            get { return GetPropertyValue(PropertyNames.BorderImageRepeat); }
            set { SetProperty(PropertyNames.BorderImageRepeat, value); }
        }

        /// <summary>
        /// Gets or sets four inward offsets, this property slices the specified
        /// border image into a three by three grid: four corners, four edges, and a central region.
        /// </summary>
        String ICssStyleDeclaration.BorderImageSlice
        {
            get { return GetPropertyValue(PropertyNames.BorderImageSlice); }
            set { SetProperty(PropertyNames.BorderImageSlice, value); }
        }

        /// <summary>
        /// Gets or sets the path of the image to be used for the border.
        /// </summary>
        String ICssStyleDeclaration.BorderImageSource
        {
            get { return GetPropertyValue(PropertyNames.BorderImageSource); }
            set { SetProperty(PropertyNames.BorderImageSource, value); }
        }

        /// <summary>
        /// Gets or sets the inward offsets from the outer border edge.
        /// </summary>
        String ICssStyleDeclaration.BorderImageWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderImageWidth); }
            set { SetProperty(PropertyNames.BorderImageWidth, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the left border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderLeft
        {
            get { return GetPropertyValue(PropertyNames.BorderLeft); }
            set { SetProperty(PropertyNames.BorderLeft, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the left border of an object.
        /// </summary>
        String ICssStyleDeclaration.BorderLeftColor
        {
            get { return GetPropertyValue(PropertyNames.BorderLeftColor); }
            set { SetProperty(PropertyNames.BorderLeftColor, value); }
        }

        /// <summary>
        /// Gets or sets the style of the left border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderLeftStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderLeftStyle); }
            set { SetProperty(PropertyNames.BorderLeftStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the left border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderLeftWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderLeftWidth); }
            set { SetProperty(PropertyNames.BorderLeftWidth, value); }
        }

        /// <summary>
        /// Gets or sets the radii of a quarter ellipse that defines the shape of
        /// the corners for the outer border edge of the current box.
        /// </summary>
        String ICssStyleDeclaration.BorderRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderRadius); }
            set { SetProperty(PropertyNames.BorderRadius, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the right border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderRight
        {
            get { return GetPropertyValue(PropertyNames.BorderRight); }
            set { SetProperty(PropertyNames.BorderRight, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the right border of an object.
        /// </summary>
        String ICssStyleDeclaration.BorderRightColor
        {
            get { return GetPropertyValue(PropertyNames.BorderRightColor); }
            set { SetProperty(PropertyNames.BorderRightColor, value); }
        }

        /// <summary>
        /// Gets or sets the style of the right border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderRightStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderRightStyle); }
            set { SetProperty(PropertyNames.BorderRightStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the right border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderRightWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderRightWidth); }
            set { SetProperty(PropertyNames.BorderRightWidth, value); }
        }

        /// <summary>
        /// Gets or sets the distance between the borders of adjoining cells in a table.
        /// </summary>
        String ICssStyleDeclaration.BorderSpacing
        {
            get { return GetPropertyValue(PropertyNames.BorderSpacing); }
            set { SetProperty(PropertyNames.BorderSpacing, value); }
        }

        /// <summary>
        /// Gets or sets the style of the left, right, top, and bottom borders of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderStyle); }
            set { SetProperty(PropertyNames.BorderStyle, value); }
        }

        /// <summary>
        /// Gets or sets the properties of the top border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderTop
        {
            get { return GetPropertyValue(PropertyNames.BorderTop); }
            set { SetProperty(PropertyNames.BorderTop, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the top border of an object.
        /// </summary>
        String ICssStyleDeclaration.BorderTopColor
        {
            get { return GetPropertyValue(PropertyNames.BorderTopColor); }
            set { SetProperty(PropertyNames.BorderTopColor, value); }
        }

        /// <summary>
        /// Gets or sets  one or two values that define the radii of the quarter ellipse
        /// that defines the shape of the upper-left corner for the outer border edge of the current box.
        /// </summary>
        String ICssStyleDeclaration.BorderTopLeftRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderTopLeftRadius); }
            set { SetProperty(PropertyNames.BorderTopLeftRadius, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that define the radii of the
        /// quarter ellipse that defines the shape of the upper-right
        /// corner for the outer border edge of the current box.
        /// </summary>
        String ICssStyleDeclaration.BorderTopRightRadius
        {
            get { return GetPropertyValue(PropertyNames.BorderTopRightRadius); }
            set { SetProperty(PropertyNames.BorderTopRightRadius, value); }
        }

        /// <summary>
        /// Gets or sets  the style of the top border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderTopStyle
        {
            get { return GetPropertyValue(PropertyNames.BorderTopStyle); }
            set { SetProperty(PropertyNames.BorderTopStyle, value); }
        }

        /// <summary>
        /// Gets or sets the thickness of the top border of the object.
        /// </summary>
        String ICssStyleDeclaration.BorderTopWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderTopWidth); }
            set { SetProperty(PropertyNames.BorderTopWidth, value); }
        }

        /// <summary>
        /// Gets or sets the thicknesses of the left, right, top, and bottom borders of an object.
        /// </summary>
        String ICssStyleDeclaration.BorderWidth
        {
            get { return GetPropertyValue(PropertyNames.BorderWidth); }
            set { SetProperty(PropertyNames.BorderWidth, value); }
        }

        /// <summary>
        /// Gets or sets one or more set of shadow values that attaches one or
        /// more drop shadows to the current box.
        /// </summary>
        String ICssStyleDeclaration.BoxShadow
        {
            get { return GetPropertyValue(PropertyNames.BoxShadow); }
            set { SetProperty(PropertyNames.BoxShadow, value); }
        }

        /// <summary>
        /// Gets or sets the box model to use for object sizing.
        /// </summary>
        String ICssStyleDeclaration.BoxSizing
        {
            get { return GetPropertyValue(PropertyNames.BoxSizing); }
            set { SetProperty(PropertyNames.BoxSizing, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that follows a content
        /// block in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.BreakAfter
        {
            get { return GetPropertyValue(PropertyNames.BreakAfter); }
            set { SetProperty(PropertyNames.BreakAfter, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that precedes a content
        /// block in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.BreakBefore
        {
            get { return GetPropertyValue(PropertyNames.BreakBefore); }
            set { SetProperty(PropertyNames.BreakBefore, value); }
        }

        /// <summary>
        /// Gets or sets the column-break behavior that occurs within a
        /// content block in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.BreakInside
        {
            get { return GetPropertyValue(PropertyNames.BreakInside); }
            set { SetProperty(PropertyNames.BreakInside, value); }
        }

        /// <summary>
        /// Gets or sets where the caption of a table is located.
        /// </summary>
        String ICssStyleDeclaration.CaptionSide
        {
            get { return GetPropertyValue(PropertyNames.CaptionSide); }
            set { SetProperty(PropertyNames.CaptionSide, value); }
        }

        /// <summary>
        /// Gets or sets whether the object allows floating objects on its left side,
        /// right side, or both, so that the next text displays past the floating objects.
        /// </summary>
        String ICssStyleDeclaration.Clear
        {
            get { return GetPropertyValue(PropertyNames.Clear); }
            set { SetProperty(PropertyNames.Clear, value); }
        }

        /// <summary>
        /// Gets or sets which part of a positioned object is visible.
        /// </summary>
        String ICssStyleDeclaration.Clip
        {
            get { return GetPropertyValue(PropertyNames.Clip); }
            set { SetProperty(PropertyNames.Clip, value); }
        }

        /// <summary>
        /// Gets or sets the bottom coordinate of the object clipping region.
        /// </summary>
        String ICssStyleDeclaration.ClipBottom
        {
            get { return GetPropertyValue(PropertyNames.ClipBottom); }
            set { SetProperty(PropertyNames.ClipBottom, value); }
        }

        /// <summary>
        /// Gets or sets the left coordinate of the object clipping region.
        /// </summary>
        String ICssStyleDeclaration.ClipLeft
        {
            get { return GetPropertyValue(PropertyNames.ClipLeft); }
            set { SetProperty(PropertyNames.ClipLeft, value); }
        }

        /// <summary>
        /// Gets or sets a reference to the SVG graphical object
        /// that will be used as the clipping path.
        /// </summary>
        String ICssStyleDeclaration.ClipPath
        {
            get { return GetPropertyValue(PropertyNames.ClipPath); }
            set { SetProperty(PropertyNames.ClipPath, value); }
        }

        /// <summary>
        /// Gets or sets the right coordinate of the object clipping region.
        /// </summary>
        String ICssStyleDeclaration.ClipRight
        {
            get { return GetPropertyValue(PropertyNames.ClipRight); }
            set { SetProperty(PropertyNames.ClipRight, value); }
        }

        /// <summary>
        /// Gets or sets the algorithm used to determine what parts of the
        /// canvas are affected by the fill operation.
        /// </summary>
        String ICssStyleDeclaration.ClipRule
        {
            get { return GetPropertyValue(PropertyNames.ClipRule); }
            set { SetProperty(PropertyNames.ClipRule, value); }
        }

        /// <summary>
        /// Gets or sets the top coordinate of the object clipping region.
        /// </summary>
        String ICssStyleDeclaration.ClipTop
        {
            get { return GetPropertyValue(PropertyNames.ClipTop); }
            set { SetProperty(PropertyNames.ClipTop, value); }
        }

        /// <summary>
        /// Gets or sets the foreground color of the text of an object.
        /// </summary>
        String ICssStyleDeclaration.Color
        {
            get { return GetPropertyValue(PropertyNames.Color); }
            set { SetProperty(PropertyNames.Color, value); }
        }

        /// <summary>
        /// Gets or sets which color space to use for filter effects.
        /// </summary>
        String ICssStyleDeclaration.ColorInterpolationFilters
        {
            get { return GetPropertyValue(PropertyNames.ColorInterpolationFilters); }
            set { SetProperty(PropertyNames.ColorInterpolationFilters, value); }
        }

        /// <summary>
        /// Gets or sets the optimal number of columns in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnCount
        {
            get { return GetPropertyValue(PropertyNames.ColumnCount); }
            set { SetProperty(PropertyNames.ColumnCount, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the column lengths in a
        /// multi-column element are affected by the content flow.
        /// </summary>
        String ICssStyleDeclaration.ColumnFill
        {
            get { return GetPropertyValue(PropertyNames.ColumnFill); }
            set { SetProperty(PropertyNames.ColumnFill, value); }
        }

        /// <summary>
        /// Gets or sets the width of the gap between columns in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnGap
        {
            get { return GetPropertyValue(PropertyNames.ColumnGap); }
            set { SetProperty(PropertyNames.ColumnGap, value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value  that specifies values for the columnRuleWidth, 
        /// columnRuleStyle, and the columnRuleColor of a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnRule
        {
            get { return GetPropertyValue(PropertyNames.ColumnRule); }
            set { SetProperty(PropertyNames.ColumnRule, value); }
        }

        /// <summary>
        /// Gets or sets the color for all column rules in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnRuleColor
        {
            get { return GetPropertyValue(PropertyNames.ColumnRuleColor); }
            set { SetProperty(PropertyNames.ColumnRuleColor, value); }
        }

        /// <summary>
        /// Gets or sets the style for all column rules in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnRuleStyle
        {
            get { return GetPropertyValue(PropertyNames.ColumnRuleStyle); }
            set { SetProperty(PropertyNames.ColumnRuleStyle, value); }
        }

        /// <summary>
        /// Gets or sets the width of all column rules in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnRuleWidth
        {
            get { return GetPropertyValue(PropertyNames.ColumnRuleWidth); }
            set { SetProperty(PropertyNames.ColumnRuleWidth, value); }
        }

        /// <summary>
        /// Gets or sets a shorthand value that specifies values for the column-width
        /// and the column-count of a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.Columns
        {
            get { return GetPropertyValue(PropertyNames.Columns); }
            set { SetProperty(PropertyNames.Columns, value); }
        }

        /// <summary>
        /// Gets or sets the number of columns that a content block
        /// spans in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnSpan
        {
            get { return GetPropertyValue(PropertyNames.ColumnSpan); }
            set { SetProperty(PropertyNames.ColumnSpan, value); }
        }

        /// <summary>
        /// Gets or sets the optimal width of the columns in a multi-column element.
        /// </summary>
        String ICssStyleDeclaration.ColumnWidth
        {
            get { return GetPropertyValue(PropertyNames.ColumnWidth); }
            set { SetProperty(PropertyNames.ColumnWidth, value); }
        }

        /// <summary>
        /// Gets or sets generated content to insert before or after an element.
        /// </summary>
        String ICssStyleDeclaration.Content
        {
            get { return GetPropertyValue(PropertyNames.Content); }
            set { SetProperty(PropertyNames.Content, value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to increment.
        /// </summary>
        String ICssStyleDeclaration.CounterIncrement
        {
            get { return GetPropertyValue(PropertyNames.CounterIncrement); }
            set { SetProperty(PropertyNames.CounterIncrement, value); }
        }

        /// <summary>
        /// Gets or sets a list of counters to create or reset to zero.
        /// </summary>
        String ICssStyleDeclaration.CounterReset
        {
            get { return GetPropertyValue(PropertyNames.CounterReset); }
            set { SetProperty(PropertyNames.CounterReset, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies whether a box should float
        /// to the left, right, or not at all.
        /// </summary>
        String ICssStyleDeclaration.Float
        {
            get { return GetPropertyValue(PropertyNames.Float); }
            set { SetProperty(PropertyNames.Float, value); }
        }

        /// <summary>
        /// Gets or sets the type of cursor to display as the mouse pointer
        /// moves over the object.
        /// </summary>
        String ICssStyleDeclaration.Cursor
        {
            get { return GetPropertyValue(PropertyNames.Cursor); }
            set { SetProperty(PropertyNames.Cursor, value); }
        }

        /// <summary>
        /// Gets or sets the reading order of the object.
        /// </summary>
        String ICssStyleDeclaration.Direction
        {
            get { return GetPropertyValue(PropertyNames.Direction); }
            set { SetProperty(PropertyNames.Direction, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether and how the object is rendered.
        /// </summary>
        String ICssStyleDeclaration.Display
        {
            get { return GetPropertyValue(PropertyNames.Display); }
            set { SetProperty(PropertyNames.Display, value); }
        }

        /// <summary>
        /// Gets or sets a value that determines or redetermines a scaled-baseline table.
        /// </summary>
        String ICssStyleDeclaration.DominantBaseline
        {
            get { return GetPropertyValue(PropertyNames.DominantBaseline); }
            set { SetProperty(PropertyNames.DominantBaseline, value); }
        }

        /// <summary>
        /// Determines whether to show or hide a cell without content.
        /// </summary>
        String ICssStyleDeclaration.EmptyCells
        {
            get { return GetPropertyValue(PropertyNames.EmptyCells); }
            set { SetProperty(PropertyNames.EmptyCells, value); }
        }

        /// <summary>
        /// Allocate a shared background image all graphic elements within a container.
        /// </summary>
        String ICssStyleDeclaration.EnableBackground
        {
            get { return GetPropertyValue(PropertyNames.EnableBackground); }
            set { SetProperty(PropertyNames.EnableBackground, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint the
        /// interior of the given graphical element.
        /// </summary>
        String ICssStyleDeclaration.Fill
        {
            get { return GetPropertyValue(PropertyNames.Fill); }
            set { SetProperty(PropertyNames.Fill, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation that
        /// is used to paint the interior of the current object.
        /// </summary>
        String ICssStyleDeclaration.FillOpacity
        {
            get { return GetPropertyValue(PropertyNames.FillOpacity); }
            set { SetProperty(PropertyNames.FillOpacity, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the algorithm that is to be used to determine
        /// what parts of the canvas are included inside the shape.
        /// </summary>
        String ICssStyleDeclaration.FillRule
        {
            get { return GetPropertyValue(PropertyNames.FillRule); }
            set { SetProperty(PropertyNames.FillRule, value); }
        }

        /// <summary>
        /// The filter property is generally used to apply a previously
        /// define filter to an applicable element.
        /// </summary>
        String ICssStyleDeclaration.Filter
        {
            get { return GetPropertyValue(PropertyNames.Filter); }
            set { SetProperty(PropertyNames.Filter, value); }
        }

        /// <summary>
        /// Gets or sets the parameter values of a flexible length, the positive and
        /// negative flexibility, and the preferred size.
        /// </summary>
        String ICssStyleDeclaration.Flex
        {
            get { return GetPropertyValue(PropertyNames.Flex); }
            set { SetProperty(PropertyNames.Flex, value); }
        }

        /// <summary>
        /// Gets or sets the initial main size of the flex item.
        /// </summary>
        String ICssStyleDeclaration.FlexBasis
        {
            get { return GetPropertyValue(PropertyNames.FlexBasis); }
            set { SetProperty(PropertyNames.FlexBasis, value); }
        }

        /// <summary>
        /// Gets or sets the direction of the main axis which specifies how
        /// the flex items are displayed in the flex container.
        /// </summary>
        String ICssStyleDeclaration.FlexDirection
        {
            get { return GetPropertyValue(PropertyNames.FlexDirection); }
            set { SetProperty(PropertyNames.FlexDirection, value); }
        }

        /// <summary>
        /// Gets or sets the shorthand property to set both the flex-direction and flex-wrap
        /// properties of a flex container.
        /// </summary>
        String ICssStyleDeclaration.FlexFlow
        {
            get { return GetPropertyValue(PropertyNames.FlexFlow); }
            set { SetProperty(PropertyNames.FlexFlow, value); }
        }

        /// <summary>
        /// Gets or sets the flex grow factor for the flex item.
        /// </summary>
        String ICssStyleDeclaration.FlexGrow
        {
            get { return GetPropertyValue(PropertyNames.FlexGrow); }
            set { SetProperty(PropertyNames.FlexGrow, value); }
        }

        /// <summary>
        /// Gets or sets the flex shrink factor for the flex item.
        /// </summary>
        String ICssStyleDeclaration.FlexShrink
        {
            get { return GetPropertyValue(PropertyNames.FlexShrink); }
            set { SetProperty(PropertyNames.FlexShrink, value); }
        }

        /// <summary>
        /// Gets or sets whether flex items wrap and the direction they
        /// wrap onto multiple lines or columns based on the spac
        /// available in the flex container. 
        /// </summary>
        String ICssStyleDeclaration.FlexWrap
        {
            get { return GetPropertyValue(PropertyNames.FlexWrap); }
            set { SetProperty(PropertyNames.FlexWrap, value); }
        }

        /// <summary>
        /// Gets or sets a combination of separate font properties of the
        /// object. Alternatively, sets or retrieves one or more of
        /// six user-preference fonts.
        /// </summary>
        String ICssStyleDeclaration.Font
        {
            get { return GetPropertyValue(PropertyNames.Font); }
            set { SetProperty(PropertyNames.Font, value); }
        }

        /// <summary>
        /// Gets or sets the name of the font used for text in the object.
        /// </summary>
        String ICssStyleDeclaration.FontFamily
        {
            get { return GetPropertyValue(PropertyNames.FontFamily); }
            set { SetProperty(PropertyNames.FontFamily, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify glyph substitution and
        /// positioning in fonts that include OpenType layout features.
        /// </summary>
        String ICssStyleDeclaration.FontFeatureSettings
        {
            get { return GetPropertyValue(PropertyNames.FontFeatureSettings); }
            set { SetProperty(PropertyNames.FontFeatureSettings, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the font size used for text in the object.
        /// </summary>
        String ICssStyleDeclaration.FontSize
        {
            get { return GetPropertyValue(PropertyNames.FontSize); }
            set { SetProperty(PropertyNames.FontSize, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies an aspect value for an element that
        /// will effectively preserve the x-height of the first choice font, whether
        /// it is substituted or not.
        /// </summary>
        String ICssStyleDeclaration.FontSizeAdjust
        {
            get { return GetPropertyValue(PropertyNames.FontSizeAdjust); }
            set { SetProperty(PropertyNames.FontSizeAdjust, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a normal, condensed,
        /// or expanded face of a font family.
        /// </summary>
        String ICssStyleDeclaration.FontStretch
        {
            get { return GetPropertyValue(PropertyNames.FontStretch); }
            set { SetProperty(PropertyNames.FontStretch, value); }
        }

        /// <summary>
        /// Gets or sets the font style of the object as italic, normal, or oblique.
        /// </summary>
        String ICssStyleDeclaration.FontStyle
        {
            get { return GetPropertyValue(PropertyNames.FontStyle); }
            set { SetProperty(PropertyNames.FontStyle, value); }
        }

        /// <summary>
        /// Gets or sets whether the text of the object is in small capital letters.
        /// </summary>
        String ICssStyleDeclaration.FontVariant
        {
            get { return GetPropertyValue(PropertyNames.FontVariant); }
            set { SetProperty(PropertyNames.FontVariant, value); }
        }

        /// <summary>
        /// Gets of sets the weight of the font of the object.
        /// </summary>
        String ICssStyleDeclaration.FontWeight
        {
            get { return GetPropertyValue(PropertyNames.FontWeight); }
            set { SetProperty(PropertyNames.FontWeight, value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence of characters
        /// relative to an inline-progression-direction of horizontal.
        /// </summary>
        String ICssStyleDeclaration.GlyphOrientationHorizontal
        {
            get { return GetPropertyValue(PropertyNames.GlyphOrientationHorizontal); }
            set { SetProperty(PropertyNames.GlyphOrientationHorizontal, value); }
        }

        /// <summary>
        /// Gets or sets a value that alters the orientation of a sequence
        /// of characters relative to an inline-progression-direction of vertical.
        /// </summary>
        String ICssStyleDeclaration.GlyphOrientationVertical
        {
            get { return GetPropertyValue(PropertyNames.GlyphOrientationVertical); }
            set { SetProperty(PropertyNames.GlyphOrientationVertical, value); }
        }

        /// <summary>
        /// Gets or sets the height of the object.
        /// </summary>
        String ICssStyleDeclaration.Height
        {
            get { return GetPropertyValue(PropertyNames.Height); }
            set { SetProperty(PropertyNames.Height, value); }
        }

        /// <summary>
        /// Gets or sets the state of an IME.
        /// </summary>
        String ICssStyleDeclaration.ImeMode
        {
            get { return GetPropertyValue(PropertyNames.ImeMode); }
            set { SetProperty(PropertyNames.ImeMode, value); }
        }

        /// <summary>
        /// Gets or sets a how flex items are aligned along the main axis of the flex
        /// container after any flexible lengths and auto margins are resolved.
        /// </summary>
        String ICssStyleDeclaration.JustifyContent
        {
            get { return GetPropertyValue(PropertyNames.JustifyContent); }
            set { SetProperty(PropertyNames.JustifyContent, value); }
        }

        /// <summary>
        /// Gets or sets the composite document grid properties
        /// that specify the layout of text characters.
        /// </summary>
        String ICssStyleDeclaration.LayoutGrid
        {
            get { return GetPropertyValue(PropertyNames.LayoutGrid); }
            set { SetProperty(PropertyNames.LayoutGrid, value); }
        }

        /// <summary>
        /// Gets or sets the size of the character grid used for rendering
        /// the text content of an element.
        /// </summary>
        String ICssStyleDeclaration.LayoutGridChar
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridChar); }
            set { SetProperty(PropertyNames.LayoutGridChar, value); }
        }

        /// <summary>
        /// Gets or sets the gridline value used for rendering the
        /// text content of an element.
        /// </summary>
        String ICssStyleDeclaration.LayoutGridLine
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridLine); }
            set { SetProperty(PropertyNames.LayoutGridLine, value); }
        }

        /// <summary>
        /// Gets or sets whether the text layout grid uses two dimensions.
        /// </summary>
        String ICssStyleDeclaration.LayoutGridMode
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridMode); }
            set { SetProperty(PropertyNames.LayoutGridMode, value); }
        }

        /// <summary>
        /// Gets or sets the type of grid used for rendering
        /// the text content of an element.
        /// </summary>
        String ICssStyleDeclaration.LayoutGridType
        {
            get { return GetPropertyValue(PropertyNames.LayoutGridType); }
            set { SetProperty(PropertyNames.LayoutGridType, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the left edge
        /// of the next positioned object in the document hierarchy.
        /// </summary>
        String ICssStyleDeclaration.Left
        {
            get { return GetPropertyValue(PropertyNames.Left); }
            set { SetProperty(PropertyNames.Left, value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between letters in the object.
        /// </summary>
        String ICssStyleDeclaration.LetterSpacing
        {
            get { return GetPropertyValue(PropertyNames.LetterSpacing); }
            set { SetProperty(PropertyNames.LetterSpacing, value); }
        }

        /// <summary>
        /// Gets or sets the distance between lines in the object.
        /// </summary>
        String ICssStyleDeclaration.LineHeight
        {
            get { return GetPropertyValue(PropertyNames.LineHeight); }
            set { SetProperty(PropertyNames.LineHeight, value); }
        }

        /// <summary>
        /// Gets or sets up to three separate list-style properties of the object.
        /// </summary>
        String ICssStyleDeclaration.ListStyle
        {
            get { return GetPropertyValue(PropertyNames.ListStyle); }
            set { SetProperty(PropertyNames.ListStyle, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates which image to use as
        /// a list-item marker for the object.
        /// </summary>
        String ICssStyleDeclaration.ListStyleImage
        {
            get { return GetPropertyValue(PropertyNames.ListStyleImage); }
            set { SetProperty(PropertyNames.ListStyleImage, value); }
        }

        /// <summary>
        /// Gets or sets a variable that indicates how the list-item marker
        /// is drawn relative to the content of the object.
        /// </summary>
        String ICssStyleDeclaration.ListStylePosition
        {
            get { return GetPropertyValue(PropertyNames.ListStylePosition); }
            set { SetProperty(PropertyNames.ListStylePosition, value); }
        }

        /// <summary>
        /// Gets or sets the predefined type of the line-item marker for the object.
        /// </summary>
        String ICssStyleDeclaration.ListStyleType
        {
            get { return GetPropertyValue(PropertyNames.ListStyleType); }
            set { SetProperty(PropertyNames.ListStyleType, value); }
        }

        /// <summary>
        /// Gets or sets the width of the top, right, bottom, and left margins of the object.
        /// </summary>
        String ICssStyleDeclaration.Margin
        {
            get { return GetPropertyValue(PropertyNames.Margin); }
            set { SetProperty(PropertyNames.Margin, value); }
        }

        /// <summary>
        /// Gets or sets the height of the bottom margin of the object.
        /// </summary>
        String ICssStyleDeclaration.MarginBottom
        {
            get { return GetPropertyValue(PropertyNames.MarginBottom); }
            set { SetProperty(PropertyNames.MarginBottom, value); }
        }

        /// <summary>
        /// Gets or sets the width of the left margin of the object.
        /// </summary>
        String ICssStyleDeclaration.MarginLeft
        {
            get { return GetPropertyValue(PropertyNames.MarginLeft); }
            set { SetProperty(PropertyNames.MarginLeft, value); }
        }

        /// <summary>
        /// Gets or sets the width of the right margin of the object.
        /// </summary>
        String ICssStyleDeclaration.MarginRight
        {
            get { return GetPropertyValue(PropertyNames.MarginRight); }
            set { SetProperty(PropertyNames.MarginRight, value); }
        }

        /// <summary>
        /// Gets or sets the height of the top margin of the object.
        /// </summary>
        String ICssStyleDeclaration.MarginTop
        {
            get { return GetPropertyValue(PropertyNames.MarginTop); }
            set { SetProperty(PropertyNames.MarginTop, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the marker symbol that is
        /// used for all vertices on the given path element or basic shape.
        /// </summary>
        String ICssStyleDeclaration.Marker
        {
            get { return GetPropertyValue(PropertyNames.Marker); }
            set { SetProperty(PropertyNames.Marker, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the final vertex of a given path element or
        /// basic shape.
        /// </summary>
        String ICssStyleDeclaration.MarkerEnd
        {
            get { return GetPropertyValue(PropertyNames.MarkerEnd); }
            set { SetProperty(PropertyNames.MarkerEnd, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker that
        /// is drawn at every other vertex (that is, every vertex except the
        /// first and last) of a given path element or basic shape.
        /// </summary>
        String ICssStyleDeclaration.MarkerMid
        {
            get { return GetPropertyValue(PropertyNames.MarkerMid); }
            set { SetProperty(PropertyNames.MarkerMid, value); }
        }

        /// <summary>
        /// Gets or sets a value that defines the arrowhead or polymarker
        /// that is drawn at the first vertex of a given path element or
        /// basic shape.
        /// </summary>
        String ICssStyleDeclaration.MarkerStart
        {
            get { return GetPropertyValue(PropertyNames.MarkerStart); }
            set { SetProperty(PropertyNames.MarkerStart, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates a SVG mask.
        /// </summary>
        String ICssStyleDeclaration.Mask
        {
            get { return GetPropertyValue(PropertyNames.Mask); }
            set { SetProperty(PropertyNames.Mask, value); }
        }

        /// <summary>
        /// Gets or sets the maximum height for an element.
        /// </summary>
        String ICssStyleDeclaration.MaxHeight
        {
            get { return GetPropertyValue(PropertyNames.MaxHeight); }
            set { SetProperty(PropertyNames.MaxHeight, value); }
        }

        /// <summary>
        /// Gets or sets the maximum width for an element.
        /// </summary>
        String ICssStyleDeclaration.MaxWidth
        {
            get { return GetPropertyValue(PropertyNames.MaxWidth); }
            set { SetProperty(PropertyNames.MaxWidth, value); }
        }

        /// <summary>
        /// Gets or sets the minimum height for an element.
        /// </summary>
        String ICssStyleDeclaration.MinHeight
        {
            get { return GetPropertyValue(PropertyNames.MinHeight); }
            set { SetProperty(PropertyNames.MinHeight, value); }
        }

        /// <summary>
        /// Gets or sets the minimum width for an element.
        /// </summary>
        String ICssStyleDeclaration.MinWidth
        {
            get { return GetPropertyValue(PropertyNames.MinWidth); }
            set { SetProperty(PropertyNames.MinWidth, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies object or group opacity in CSS or SVG.
        /// </summary>
        String ICssStyleDeclaration.Opacity
        {
            get { return GetPropertyValue(PropertyNames.Opacity); }
            set { SetProperty(PropertyNames.Opacity, value); }
        }

        /// <summary>
        /// Gets or sets the order, which property specifies the order used to lay out
        /// flex items in their flex container. Elements are laid out by ascending order
        /// of the order value. Elements with the same order value are laid out in the
        /// order they appear in the source code.
        /// </summary>
        String ICssStyleDeclaration.Order
        {
            get { return GetPropertyValue(PropertyNames.Order); }
            set { SetProperty(PropertyNames.Order, value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph
        /// that must appear at the bottom of a page.
        /// </summary>
        String ICssStyleDeclaration.Orphans
        {
            get { return GetPropertyValue(PropertyNames.Orphans); }
            set { SetProperty(PropertyNames.Orphans, value); }
        }

        /// <summary>
        /// Gets or sets the outline frame.
        /// </summary>
        String ICssStyleDeclaration.Outline
        {
            get { return GetPropertyValue(PropertyNames.Outline); }
            set { SetProperty(PropertyNames.Outline, value); }
        }

        /// <summary>
        /// Gets or sets the color of the outline frame.
        /// </summary>
        String ICssStyleDeclaration.OutlineColor
        {
            get { return GetPropertyValue(PropertyNames.OutlineColor); }
            set { SetProperty(PropertyNames.OutlineColor, value); }
        }

        /// <summary>
        /// Gets or sets the style of the outline frame.
        /// </summary>
        String ICssStyleDeclaration.OutlineStyle
        {
            get { return GetPropertyValue(PropertyNames.OutlineStyle); }
            set { SetProperty(PropertyNames.OutlineStyle, value); }
        }

        /// <summary>
        /// Gets or sets the width of the outline frame.
        /// </summary>
        String ICssStyleDeclaration.OutlineWidth
        {
            get { return GetPropertyValue(PropertyNames.OutlineWidth); }
            set { SetProperty(PropertyNames.OutlineWidth, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating how to manage the content of the
        /// object when the content exceeds the height or width of the object.
        /// </summary>
        String ICssStyleDeclaration.Overflow
        {
            get { return GetPropertyValue(PropertyNames.Overflow); }
            set { SetProperty(PropertyNames.Overflow, value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when the
        /// content exceeds the width of the object.
        /// </summary>
        String ICssStyleDeclaration.OverflowX
        {
            get { return GetPropertyValue(PropertyNames.OverflowX); }
            set { SetProperty(PropertyNames.OverflowX, value); }
        }

        /// <summary>
        /// Gets or sets how to manage the content of the object when
        /// the content exceeds the height of the object.
        /// </summary>
        String ICssStyleDeclaration.OverflowY
        {
            get { return GetPropertyValue(PropertyNames.OverflowY); }
            set { SetProperty(PropertyNames.OverflowY, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the object and
        /// its margin or, if there is a border, between the object and its border.
        /// </summary>
        String ICssStyleDeclaration.Padding
        {
            get { return GetPropertyValue(PropertyNames.Padding); }
            set { SetProperty(PropertyNames.Padding, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the bottom
        /// border of the object and the content.
        /// </summary>
        String ICssStyleDeclaration.PaddingBottom
        {
            get { return GetPropertyValue(PropertyNames.PaddingBottom); }
            set { SetProperty(PropertyNames.PaddingBottom, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the left
        /// border of the object and the content.
        /// </summary>
        String ICssStyleDeclaration.PaddingLeft
        {
            get { return GetPropertyValue(PropertyNames.PaddingLeft); }
            set { SetProperty(PropertyNames.PaddingLeft, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between
        /// the right border of the object and the content.
        /// </summary>
        String ICssStyleDeclaration.PaddingRight
        {
            get { return GetPropertyValue(PropertyNames.PaddingRight); }
            set { SetProperty(PropertyNames.PaddingRight, value); }
        }

        /// <summary>
        /// Gets or sets the amount of space to insert between the top
        /// border of the object and the content.
        /// </summary>
        String ICssStyleDeclaration.PaddingTop
        {
            get { return GetPropertyValue(PropertyNames.PaddingTop); }
            set { SetProperty(PropertyNames.PaddingTop, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether a page break occurs after the object.
        /// </summary>
        String ICssStyleDeclaration.PageBreakAfter
        {
            get { return GetPropertyValue(PropertyNames.PageBreakAfter); }
            set { SetProperty(PropertyNames.PageBreakAfter, value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break occurs before the object.
        /// </summary>
        String ICssStyleDeclaration.PageBreakBefore
        {
            get { return GetPropertyValue(PropertyNames.PageBreakBefore); }
            set { SetProperty(PropertyNames.PageBreakBefore, value); }
        }

        /// <summary>
        /// Gets or sets a string indicating whether a page break is
        /// allowed to occur inside the object.
        /// </summary>
        String ICssStyleDeclaration.PageBreakInside
        {
            get { return GetPropertyValue(PropertyNames.PageBreakInside); }
            set { SetProperty(PropertyNames.PageBreakInside, value); }
        }

        /// <summary>
        /// Gets or sets a value that represents the perspective from which all child
        /// elements of the object are viewed.
        /// </summary>
        String ICssStyleDeclaration.Perspective
        {
            get { return GetPropertyValue(PropertyNames.Perspective); }
            set { SetProperty(PropertyNames.Perspective, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that represent the origin (the
        /// vanishing point for the 3-D space) of an object with an perspective
        /// property declaration.
        /// </summary>
        String ICssStyleDeclaration.PerspectiveOrigin
        {
            get { return GetPropertyValue(PropertyNames.PerspectiveOrigin); }
            set { SetProperty(PropertyNames.PerspectiveOrigin, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies under what circumstances a given graphics
        /// element can be the target element for a pointer event in SVG.
        /// </summary>
        String ICssStyleDeclaration.PointerEvents
        {
            get { return GetPropertyValue(PropertyNames.PointerEvents); }
            set { SetProperty(PropertyNames.PointerEvents, value); }
        }

        /// <summary>
        /// Gets or sets the pairs of strings to be used as quotes in generated content.
        /// </summary>
        String ICssStyleDeclaration.Quotes
        {
            get { return GetPropertyValue(PropertyNames.Quotes); }
            set { SetProperty(PropertyNames.Quotes, value); }
        }

        /// <summary>
        /// Gets or sets the type of positioning used for the object.
        /// </summary>
        String ICssStyleDeclaration.Position
        {
            get { return GetPropertyValue(PropertyNames.Position); }
            set { SetProperty(PropertyNames.Position, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the right edge of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        String ICssStyleDeclaration.Right
        {
            get { return GetPropertyValue(PropertyNames.Right); }
            set { SetProperty(PropertyNames.Right, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the ruby text content.
        /// </summary>
        String ICssStyleDeclaration.RubyAlign
        {
            get { return GetPropertyValue(PropertyNames.RubyAlign); }
            set { SetProperty(PropertyNames.RubyAlign, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether, and on which side, ruby
        /// text is allowed to partially overhang any adjacent text in addition
        /// to its own base, when the ruby text is wider than the ruby base
        /// </summary>
        String ICssStyleDeclaration.RubyOverhang
        {
            get { return GetPropertyValue(PropertyNames.RubyOverhang); }
            set { SetProperty(PropertyNames.RubyOverhang, value); }
        }

        /// <summary>
        /// Gets or sets a value that controls the position of the ruby text
        /// with respect to its base.
        /// </summary>
        String ICssStyleDeclaration.RubyPosition
        {
            get { return GetPropertyValue(PropertyNames.RubyPosition); }
            set { SetProperty(PropertyNames.RubyPosition, value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll
        /// box and scroll arrows of a scroll bar.
        /// </summary>
        String ICssStyleDeclaration.Scrollbar3dLightColor
        {
            get { return GetPropertyValue(PropertyNames.Scrollbar3dLightColor); }
            set { SetProperty(PropertyNames.Scrollbar3dLightColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the arrow elements of a scroll arrow.
        /// </summary>
        String ICssStyleDeclaration.ScrollbarArrowColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarArrowColor); }
            set { SetProperty(PropertyNames.ScrollbarArrowColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the gutter of a scroll bar.
        /// </summary>
        String ICssStyleDeclaration.ScrollbarDarkShadowColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarDarkShadowColor); }
            set { SetProperty(PropertyNames.ScrollbarDarkShadowColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        String ICssStyleDeclaration.ScrollbarFaceColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarFaceColor); }
            set { SetProperty(PropertyNames.ScrollbarFaceColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the top and left edges of the scroll box and scroll arrows of a scroll bar.
        /// </summary>
        String ICssStyleDeclaration.ScrollbarHighlightColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarHighlightColor); }
            set { SetProperty(PropertyNames.ScrollbarHighlightColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the bottom and right edges of the
        /// scroll box and scroll arrows of a scroll bar.
        /// </summary>
        String ICssStyleDeclaration.ScrollbarShadowColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarShadowColor); }
            set { SetProperty(PropertyNames.ScrollbarShadowColor, value); }
        }

        /// <summary>
        /// Gets or sets the color of the track element of a scroll bar.
        /// </summary>
        String ICssStyleDeclaration.ScrollbarTrackColor
        {
            get { return GetPropertyValue(PropertyNames.ScrollbarTrackColor); }
            set { SetProperty(PropertyNames.ScrollbarTrackColor, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the color to paint along
        /// the outline of a given graphical element.
        /// </summary>
        String ICssStyleDeclaration.Stroke
        {
            get { return GetPropertyValue(PropertyNames.Stroke); }
            set { SetProperty(PropertyNames.Stroke, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that indicate the pattern of
        /// dashes and gaps used to stroke paths.
        /// </summary>
        String ICssStyleDeclaration.StrokeDasharray
        {
            get { return GetPropertyValue(PropertyNames.StrokeDasharray); }
            set { SetProperty(PropertyNames.StrokeDasharray, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the distance into the
        /// dash pattern to start the dash.
        /// </summary>
        String ICssStyleDeclaration.StrokeDashoffset
        {
            get { return GetPropertyValue(PropertyNames.StrokeDashoffset); }
            set { SetProperty(PropertyNames.StrokeDashoffset, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the
        /// end of open subpaths when they are stroked.
        /// </summary>
        String ICssStyleDeclaration.StrokeLinecap
        {
            get { return GetPropertyValue(PropertyNames.StrokeLinecap); }
            set { SetProperty(PropertyNames.StrokeLinecap, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the shape to be used at the corners of
        /// paths or basic shapes when they are stroked.
        /// </summary>
        String ICssStyleDeclaration.StrokeLinejoin
        {
            get { return GetPropertyValue(PropertyNames.StrokeLinejoin); }
            set { SetProperty(PropertyNames.StrokeLinejoin, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates the limit on the ratio of the
        /// length of miter joins (as specified in the StrokeLinejoin property).
        /// </summary>
        String ICssStyleDeclaration.StrokeMiterlimit
        {
            get { return GetPropertyValue(PropertyNames.StrokeMiterlimit); }
            set { SetProperty(PropertyNames.StrokeMiterlimit, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the opacity of the painting operation
        /// that is used to stroke the current object.
        /// </summary>
        String ICssStyleDeclaration.StrokeOpacity
        {
            get { return GetPropertyValue(PropertyNames.StrokeOpacity); }
            set { SetProperty(PropertyNames.StrokeOpacity, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies the width of the stroke on the current object.
        /// </summary>
        String ICssStyleDeclaration.StrokeWidth
        {
            get { return GetPropertyValue(PropertyNames.StrokeWidth); }
            set { SetProperty(PropertyNames.StrokeWidth, value); }
        }

        /// <summary>
        /// Gets or sets a string that indicates whether the table layout is fixed.
        /// </summary>
        String ICssStyleDeclaration.TableLayout
        {
            get { return GetPropertyValue(PropertyNames.TableLayout); }
            set { SetProperty(PropertyNames.TableLayout, value); }
        }

        /// <summary>
        /// Gets or sets whether the text in the object is left-aligned, right-aligned, 
        /// centered, or justified.
        /// </summary>
        String ICssStyleDeclaration.TextAlign
        {
            get { return GetPropertyValue(PropertyNames.TextAlign); }
            set { SetProperty(PropertyNames.TextAlign, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates how to align the last line or only
        /// line of text in the specified object.
        /// </summary>
        String ICssStyleDeclaration.TextAlignLast
        {
            get { return GetPropertyValue(PropertyNames.TextAlignLast); }
            set { SetProperty(PropertyNames.TextAlignLast, value); }
        }

        /// <summary>
        /// Aligns a string of text relative to the specified point.
        /// </summary>
        String ICssStyleDeclaration.TextAnchor
        {
            get { return GetPropertyValue(PropertyNames.TextAnchor); }
            set { SetProperty(PropertyNames.TextAnchor, value); }
        }

        /// <summary>
        /// Gets or sets the autospacing and narrow space width adjustment of text.
        /// </summary>
        String ICssStyleDeclaration.TextAutospace
        {
            get { return GetPropertyValue(PropertyNames.TextAutospace); }
            set { SetProperty(PropertyNames.TextAutospace, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the text in the object
        /// has blink, line-through, overline, or underline decorations.
        /// </summary>
        String ICssStyleDeclaration.TextDecoration
        {
            get { return GetPropertyValue(PropertyNames.TextDecoration); }
            set { SetProperty(PropertyNames.TextDecoration, value); }
        }

        /// <summary>
        /// Gets or sets the indentation of the first line of text in the object.
        /// </summary>
        String ICssStyleDeclaration.TextIndent
        {
            get { return GetPropertyValue(PropertyNames.TextIndent); }
            set { SetProperty(PropertyNames.TextIndent, value); }
        }

        /// <summary>
        /// Gets or sets the type of alignment used to justify text in the object.
        /// </summary>
        String ICssStyleDeclaration.TextJustify
        {
            get { return GetPropertyValue(PropertyNames.TextJustify); }
            set { SetProperty(PropertyNames.TextJustify, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether to render
        /// ellipses (...) to indicate text overflow.
        /// </summary>
        String ICssStyleDeclaration.TextOverflow
        {
            get { return GetPropertyValue(PropertyNames.TextOverflow); }
            set { SetProperty(PropertyNames.TextOverflow, value); }
        }

        /// <summary>
        /// Gets or sets a comma-separated list of shadows that attaches one or
        /// more drop shadows to the specified text.
        /// </summary>
        String ICssStyleDeclaration.TextShadow
        {
            get { return GetPropertyValue(PropertyNames.TextShadow); }
            set { SetProperty(PropertyNames.TextShadow, value); }
        }

        /// <summary>
        /// Gets or sets the rendering of the text in the object.
        /// </summary>
        String ICssStyleDeclaration.TextTransform
        {
            get { return GetPropertyValue(PropertyNames.TextTransform); }
            set { SetProperty(PropertyNames.TextTransform, value); }
        }

        /// <summary>
        /// Gets or sets the position of the underline decoration that is set through the
        /// text-decoration property of the object.
        /// </summary>
        String ICssStyleDeclaration.TextUnderlinePosition
        {
            get { return GetPropertyValue(PropertyNames.TextUnderlinePosition); }
            set { SetProperty(PropertyNames.TextUnderlinePosition, value); }
        }

        /// <summary>
        /// Gets or sets the position of the object relative to the top of
        /// the next positioned object in the document hierarchy.
        /// </summary>
        String ICssStyleDeclaration.Top
        {
            get { return GetPropertyValue(PropertyNames.Top); }
            set { SetProperty(PropertyNames.Top, value); }
        }

        /// <summary>
        /// Gets or sets a list of one or more transform functions that specify how
        /// to translate, rotate, or scale an element in 2-D or 3-D space.
        /// </summary>
        String ICssStyleDeclaration.Transform
        {
            get { return GetPropertyValue(PropertyNames.Transform); }
            set { SetProperty(PropertyNames.Transform, value); }
        }

        /// <summary>
        /// Gets or sets one or two values that establish the origin of transformation for an element.
        /// </summary>
        String ICssStyleDeclaration.TransformOrigin
        {
            get { return GetPropertyValue(PropertyNames.TransformOrigin); }
            set { SetProperty(PropertyNames.TransformOrigin, value); }
        }

        /// <summary>
        /// Gets or sets a value that specifies how child elements of the
        /// object are rendered in 3-D space.
        /// </summary>
        String ICssStyleDeclaration.TransformStyle
        {
            get { return GetPropertyValue(PropertyNames.TransformStyle); }
            set { SetProperty(PropertyNames.TransformStyle, value); }
        }

        /// <summary>
        /// Gets or sets one or more shorthand values that specify the transition properties
        /// for a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        String ICssStyleDeclaration.Transition
        {
            get { return GetPropertyValue(PropertyNames.Transition); }
            set { SetProperty(PropertyNames.Transition, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the offset within a
        /// transition (the amount of time from the start of a transition) before
        /// the transition is displayed  for a set of corresponding object properties 
        /// identified in the transition property.
        /// </summary>
        String ICssStyleDeclaration.TransitionDelay
        {
            get { return GetPropertyValue(PropertyNames.TransitionDelay); }
            set { SetProperty(PropertyNames.TransitionDelay, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the durations of transitions on
        /// a set of corresponding object properties identified in the transition-property
        /// property.
        /// </summary>
        String ICssStyleDeclaration.TransitionDuration
        {
            get { return GetPropertyValue(PropertyNames.TransitionDuration); }
            set { SetProperty(PropertyNames.TransitionDuration, value); }
        }

        /// <summary>
        /// Gets or sets a value that identifies the CSS property name or names to which
        /// the transition effect (defined by the transition-duration, transition-timing-function,
        /// and transition-delay properties) is applied when a new property value is specified.
        /// </summary>
        String ICssStyleDeclaration.TransitionProperty
        {
            get { return GetPropertyValue(PropertyNames.TransitionProperty); }
            set { SetProperty(PropertyNames.TransitionProperty, value); }
        }

        /// <summary>
        /// Gets or sets one or more values that specify the intermediate property values to be
        /// used during a transition on a set of corresponding object properties identified
        /// in the transition-property property.
        /// </summary>
        String ICssStyleDeclaration.TransitionTimingFunction
        {
            get { return GetPropertyValue(PropertyNames.TransitionTimingFunction); }
            set { SetProperty(PropertyNames.TransitionTimingFunction, value); }
        }

        /// <summary>
        /// Gets or sets the level of embedding with respect to the bidirectional algorithm.
        /// </summary>
        String ICssStyleDeclaration.UnicodeBidi
        {
            get { return GetPropertyValue(PropertyNames.UnicodeBidi); }
            set { SetProperty(PropertyNames.UnicodeBidi, value); }
        }

        /// <summary>
        /// Gets or sets the vertical alignment of the object.
        /// </summary>
        String ICssStyleDeclaration.VerticalAlign
        {
            get { return GetPropertyValue(PropertyNames.VerticalAlign); }
            set { SetProperty(PropertyNames.VerticalAlign, value); }
        }

        /// <summary>
        /// Gets or sets whether the content of the object is displayed.
        /// </summary>
        String ICssStyleDeclaration.Visibility
        {
            get { return GetPropertyValue(PropertyNames.Visibility); }
            set { SetProperty(PropertyNames.Visibility, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether lines are automatically
        /// broken inside the object.
        /// </summary>
        String ICssStyleDeclaration.WhiteSpace
        {
            get { return GetPropertyValue(PropertyNames.WhiteSpace); }
            set { SetProperty(PropertyNames.WhiteSpace, value); }
        }

        /// <summary>
        /// Gets or sets the minimum number of lines of a paragraph that must
        /// appear at the top of a document.
        /// </summary>
        String ICssStyleDeclaration.Widows
        {
            get { return GetPropertyValue(PropertyNames.Widows); }
            set { SetProperty(PropertyNames.Widows, value); }
        }

        /// <summary>
        /// Gets or sets the width of the object.
        /// </summary>
        String ICssStyleDeclaration.Width
        {
            get { return GetPropertyValue(PropertyNames.Width); }
            set { SetProperty(PropertyNames.Width, value); }
        }

        /// <summary>
        /// Gets or sets line-breaking behavior within words, particularly where
        /// multiple languages appear in the object.
        /// </summary>
        String ICssStyleDeclaration.WordBreak
        {
            get { return GetPropertyValue(PropertyNames.WordBreak); }
            set { SetProperty(PropertyNames.WordBreak, value); }
        }

        /// <summary>
        /// Gets or sets the amount of additional space between words in the object.
        /// </summary>
        String ICssStyleDeclaration.WordSpacing
        {
            get { return GetPropertyValue(PropertyNames.WordSpacing); }
            set { SetProperty(PropertyNames.WordSpacing, value); }
        }

        /// <summary>
        /// Gets or sets whether to break words when the content exceeds the
        /// boundaries of its container.
        /// </summary>
        String ICssStyleDeclaration.WordWrap
        {
            get { return GetPropertyValue(PropertyNames.WordWrap); }
            set { SetProperty(PropertyNames.WordWrap, value); }
        }

        /// <summary>
        /// Gets or sets the direction and flow of the content in the object.
        /// </summary>
        String ICssStyleDeclaration.WritingMode
        {
            get { return GetPropertyValue(PropertyNames.WritingMode); }
            set { SetProperty(PropertyNames.WritingMode, value); }
        }

        /// <summary>
        /// Gets or sets the stacking order of positioned objects.
        /// </summary>
        String ICssStyleDeclaration.ZIndex
        {
            get { return GetPropertyValue(PropertyNames.ZIndex); }
            set { SetProperty(PropertyNames.ZIndex, value); }
        }

        /// <summary>
        /// Gets or sets the magnification scale of the object.
        /// </summary>
        String ICssStyleDeclaration.Zoom
        {
            get { return GetPropertyValue(PropertyNames.Zoom); }
            set { SetProperty(PropertyNames.Zoom, value); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Removes the given property and returns its value.
        /// </summary>
        /// <param name="propertyName">The name of the property to be removed.</param>
        /// <returns>The value of the deleted property, if any.</returns>
        public String RemoveProperty(String propertyName)
        {
            if (_readOnly)
                throw new DomException(DomError.NoModificationAllowed);

            var value = GetPropertyValue(propertyName);

            if (Factory.Properties.IsShorthand(propertyName))
            {
                var longhands = Factory.Properties.GetLonghands(propertyName);

                foreach (var longhand in longhands)
                    RemoveProperty(longhand);
            }
            else
            {
                _declarations.RemoveAll(rule => rule.Name == propertyName);
                RaiseChanged();
            }

            return value;
        }

        /// <summary>
        /// Returns the optional priority, "important".
        /// </summary>
        /// <param name="propertyName">The name of the property to get the priority of.</param>
        /// <returns>A priority or the empty string.</returns>
        public String GetPropertyPriority(String propertyName)
        {
            var property = GetProperty(propertyName);

            if (Factory.Properties.IsShorthand(propertyName))
            {
                var longhands = Factory.Properties.GetLonghands(propertyName);

                foreach (var longhand in longhands)
                {
                    if (GetPropertyPriority(longhand) != Keywords.Important)
                        return String.Empty;
                }

                return Keywords.Important;
            }
            else if (property != null && property.IsImportant)
                return Keywords.Important;

            return String.Empty;
        }

        /// <summary>
        /// Returns the value of a property.
        /// </summary>
        /// <param name="propertyName">The name of the property to get the value of.</param>
        /// <returns>A value or the empty string if nothing has been set.</returns>
        public String GetPropertyValue(String propertyName)
        {
            if (Factory.Properties.IsShorthand(propertyName))
            {
                var declarations = Factory.Properties.GetLonghands(propertyName);

                foreach (var declaration in declarations)
                {
                    if (GetProperty(declaration) == null)
                        return String.Empty;
                }

                return Factory.Properties.CreateShorthand(propertyName, this).SerializeValue();
            }

            var property = GetProperty(propertyName);

            if (property != null)
                return property.SerializeValue();

            return String.Empty;
        }

        public void SetPropertyValue(String propertyName, String propertyValue)
        {
            SetProperty(propertyName, propertyValue);
        }

        public void SetPropertyPriority(String propertyName, String priority)
        {
            if (_readOnly)
                throw new DomException(DomError.NoModificationAllowed);
            
            if (!Factory.Properties.IsSupported(propertyName))
                return;

            if (!String.IsNullOrEmpty(priority) && !priority.Equals(Keywords.Important, StringComparison.OrdinalIgnoreCase))
                return;

            var important = !String.IsNullOrEmpty(priority);
            var mappings = Factory.Properties.IsShorthand(propertyName) ? Factory.Properties.GetLonghands(propertyName) : Enumerable.Repeat(propertyName, 1);
            
            foreach (var mapping in mappings)
            {
                var property = GetProperty(mapping);

                if (property != null)
                    property.IsImportant = important;
            }
        }

        /// <summary>
        /// Sets a property with the given name and value.
        /// </summary>
        /// <param name="propertyName">The property's name.</param>
        /// <param name="propertyValue">The value of the property.</param>
        /// <param name="priority">The optional priority.</param>
        public void SetProperty(String propertyName, String propertyValue, String priority = null)
        {
            if (_readOnly)
                throw new DomException(DomError.NoModificationAllowed);

            if (!Factory.Properties.IsSupported(propertyName))
                return;

            if (!String.IsNullOrEmpty(propertyValue))
            {
                if (priority != null && !priority.Equals(Keywords.Important, StringComparison.OrdinalIgnoreCase))
                    return;

                var value = CssParser.ParseValue(propertyValue);

                if (value == null)
                    return;

                var property = CreateProperty(propertyName);

                if (property != null && property.TrySetValue(value))
                {
                    property.IsImportant = priority != null;
                    SetProperty(property);
                    RaiseChanged();
                }
            }
            else
                RemoveProperty(propertyName);
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Creates the given property, if it does not already exist.
        /// Otherwise returns the existing declaration.
        /// </summary>
        /// <param name="name">The name of the property to retrieve or create.</param>
        /// <returns>The created / existing property.</returns>
        internal CssProperty CreateProperty(String name)
        {
            return GetProperty(name) ?? _creator.Create(name, this);
        }

        /// <summary>
        /// Gets the given CSS property.
        /// </summary>
        /// <param name="name">The name of the property to get.</param>
        /// <returns>The property with the specified name or null.</returns>
        internal CssProperty GetProperty(String name)
        {
            foreach (var declaration in _declarations)
            {
                if (declaration.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                    return declaration;
            }

            return null;
        }

        /// <summary>
        /// Sets the given CSS property, if the property is equal or higher.
        /// </summary>
        /// <param name="property">The property to set.</param>
        internal void SetProperty(CssProperty property)
        {
            if (property is CssShorthandProperty)
                SetShorthand((CssShorthandProperty)property);
            else
                SetLonghand(property);
        }

        /// <summary>
        /// Updates the CSSStyleDeclaration with the given value.
        /// </summary>
        /// <param name="value">The new value.</param>
        internal void Update(String value)
        {
            _declarations.Clear();

            if (!String.IsNullOrEmpty(value))
                CssParser.AppendDeclarations(this, value);
        }

        /// <summary>
        /// Sets the the declarations from the other style declaration.
        /// </summary>
        /// <param name="style">The style to take the declarations from.</param>
        internal void SetDeclarations(CssStyleDeclaration style)
        {
            ChangeDeclarations(style, m => false, (o, n) => !o.IsImportant || n.IsImportant);
        }

        /// <summary>
        /// Update the the declarations with the given style declaration.
        /// </summary>
        /// <param name="style">The style to take the declarations from.</param>
        internal void UpdateDeclarations(CssStyleDeclaration style)
        {
            ChangeDeclarations(style, m => !m.CanBeInherited, (o, n) => o.IsInherited);
        }

        /// <summary>
        /// Clears the declarations.
        /// </summary>
        internal void Clear()
        {
            _declarations.Clear();
        }

        #endregion

        #region Helpers

        void ChangeDeclarations(CssStyleDeclaration style, Predicate<CssProperty> defaultSkip, Func<CssProperty, CssProperty, Boolean> removeExisting)
        {
            var declarations = new List<CssProperty>();

            foreach (var newdecl in style._declarations)
            {
                var skip = defaultSkip(newdecl);

                foreach (var olddecl in _declarations)
                {
                    if (olddecl.Name == newdecl.Name)
                    {
                        if (removeExisting(olddecl, newdecl))
                            _declarations.Remove(olddecl);
                        else
                            skip = true;

                        break;
                    }
                }

                if (!skip)
                    declarations.Add(newdecl);
            }

            _declarations.AddRange(declarations);
        }

        void SetLonghand(CssProperty property)
        {
            if (!_declarations.Contains(property))
                _declarations.Add(property);
        }

        void SetShorthand(CssShorthandProperty shorthand)
        {
            var properties = shorthand.Properties;

            for (int i = 0; i < properties.Length; i++)
                SetLonghand(properties[i]);
        }

        void RaiseChanged()
        {
            if (Changed != null)
                Changed(this, EventArgs.Empty);
        }

        #endregion

        #region Interface implementation

        CssProperty IPropertyCreator.Create(String name, CssStyleDeclaration style)
        {
            return Factory.Properties.Create(name, this);
        }

        /// <summary>
        /// Returns an ienumerator that enumerates over all entries.
        /// </summary>
        /// <returns>The iterator.</returns>
        public IEnumerator<ICssProperty> GetEnumerator()
        {
            return _declarations.GetEnumerator();
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
