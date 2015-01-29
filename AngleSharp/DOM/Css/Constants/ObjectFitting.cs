namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// The enumeration over all possible object fitting values.
    /// </summary>
    public enum ObjectFitting
    {
        /// <summary>
        /// The replaced content is not resized to fit inside the element's content box:
        /// determine the object's concrete object size using the default sizing
        /// algorithm with no specified size, and a default object size equal to the
        /// replaced element's used width and height.
        /// </summary>
        None,
        /// <summary>
        /// The replaced content is sized to fill the element's content box: the object's
        /// concrete object size is the element's used width and height.
        /// </summary>
        Fill,
        /// <summary>
        /// The replaced content is sized to maintain its aspect ratio while fitting
        /// within the element's content box: its concrete object size is resolved as a
        /// contain constraint against the element's used width and height.
        /// </summary>
        Contain,
        /// <summary>
        /// The replaced content is sized to maintain its aspect ratio while filling the
        /// element's entire content box: its concrete object size is resolved as a cover
        /// constraint against the element's used width and height.
        /// </summary>
        Cover,
        /// <summary>
        /// Size the content as if none or contain were specified, whichever would result
        /// in a smaller concrete object size.
        /// </summary>
        ScaleDown
    }
}
