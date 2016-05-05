namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// A list of all pre-defined system cursor modes.
    /// </summary>
    public enum SystemCursor : ushort
    {
        /// <summary>
        /// Automatically determined.
        /// </summary>
        Auto,
        /// <summary>
        /// Default cursor, typically an arrow.
        /// </summary>
        Default,
        /// <summary>
        /// No cursor is rendered.
        /// </summary>
        None,
        /// <summary>
        /// A context menu is available under the cursor.
        /// </summary>
        ContextMenu,
        /// <summary>
        /// Indicating help is available.
        /// </summary>
        Help,
        /// <summary>
        /// E.g. used when hovering over links, typically a hand.
        /// </summary>
        Pointer,
        /// <summary>
        /// The program is busy in the background but the user can
        /// still interact with the interface (unlike for wait).
        /// </summary>
        Progress,
        /// <summary>
        /// The program is busy (sometimes an hourglass or a watch).
        /// </summary>
        Wait,
        /// <summary>
        /// Indicating that cells can be selected.
        /// </summary>
        Cell,
        /// <summary>
        /// Cross cursor, often used to indicate selection in a bitmap.
        /// </summary>
        Crosshair,
        /// <summary>
        /// Indicating text can be selected, typically an I-beam.
        /// </summary>
        Text,
        /// <summary>
        /// Indicating that vertical text can be selected, typically
        /// a sideways I-beam.
        /// </summary>
        VerticalText,
        /// <summary>
        /// Indicating an alias or shortcut is to be created.
        /// </summary>
        Alias,
        /// <summary>
        /// Indicating that something can be copied.
        /// </summary>
        Copy,
        /// <summary>
        /// The hovered object may be moved.
        /// </summary>
        Move,
        /// <summary>
        /// Cursor showing that a drop is not allowed at the current location.
        /// </summary>
        NoDrop,
        /// <summary>
        /// Cursor showing that something cannot be done.
        /// </summary>
        NotAllowed,
        /// <summary>
        /// Moves the right edge.
        /// </summary>
        EResize,
        /// <summary>
        /// Moves the top edge.
        /// </summary>
        NResize,
        /// <summary>
        /// Moves the top right edge.
        /// </summary>
        NeResize,
        /// <summary>
        /// Moves the top left  edge.
        /// </summary>
        NwResize,
        /// <summary>
        /// Moves the bottom edge.
        /// </summary>
        SResize,
        /// <summary>
        /// Moves the bottom right edge.
        /// </summary>
        SeResize,
        /// <summary>
        /// Moves the bottom left edge.
        /// </summary>
        SwResize,
        /// <summary>
        /// Moves the left edge.
        /// </summary>
        WResize,
        /// <summary>
        /// Indicates a bidirectional resize cursor.
        /// Left to right.
        /// </summary>
        EwResize,
        /// <summary>
        /// Indicates a bidirectional resize cursor.
        /// Top to bottom.
        /// </summary>
        NsResize,
        /// <summary>
        /// Indicates a bidirectional resize cursor.
        /// Right top to left bottom.
        /// </summary>
        NeswResize,
        /// <summary>
        /// Indicates a bidirectional resize cursor.
        /// Left top to right bottom.
        /// </summary>
        NwseResize,
        /// <summary>
        /// The item/column can be resized horizontally. Often rendered as arrows
        /// pointing left and right with a vertical bar separating.
        /// </summary>
        ColResize,
        /// <summary>
        /// The item/row can be resized vertically. Often rendered as arrows
        /// pointing up and down with a horizontal bar separating them.
        /// </summary>
        RowResize,
        /// <summary>
        /// Cursor showing that something can be scrolled in any direction (panned).
        /// </summary>
        AllScroll,
        /// <summary>
        /// Indicates that something can be zoomed (magnified) in.
        /// </summary>
        ZoomIn,
        /// <summary>
        /// Indicates that something can be zoomed (magnified) out.
        /// </summary>
        ZoomOut,
        /// <summary>
        /// Indicates that something can be grabbed.
        /// </summary>
        Grab,
        /// <summary>
        /// Indicates that something can be dragged to be moved.
        /// </summary>
        Grabbing,
    }
}
