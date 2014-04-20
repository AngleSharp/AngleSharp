namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    public sealed class CSSCursorProperty : CSSProperty
    {
        #region Fields

        static readonly ValueConverter<CursorMode> modes;
        static readonly AutoCursorMode _auto = new AutoCursorMode();
        CursorMode _mode;

        #endregion

        #region ctor

        static CSSCursorProperty()
        {
            modes = new ValueConverter<CursorMode>();
            modes.AddStatic("auto", _auto);
            modes.AddStatic("default", new SystemCursorMode(Cursor.Default));
            modes.AddStatic("none", new SystemCursorMode(Cursor.None));
            modes.AddStatic("context-menu", new SystemCursorMode(Cursor.ContextMenu));
            modes.AddStatic("help", new SystemCursorMode(Cursor.Help));
            modes.AddStatic("pointer", new SystemCursorMode(Cursor.Pointer));
            modes.AddStatic("progress", new SystemCursorMode(Cursor.Progress));
            modes.AddStatic("wait", new SystemCursorMode(Cursor.Wait));
            modes.AddStatic("cell", new SystemCursorMode(Cursor.Cell));
            modes.AddStatic("crosshair", new SystemCursorMode(Cursor.Crosshair));
            modes.AddStatic("text", new SystemCursorMode(Cursor.Text));
            modes.AddStatic("vertical-text", new SystemCursorMode(Cursor.VerticalText));
            modes.AddStatic("alias", new SystemCursorMode(Cursor.Alias));
            modes.AddStatic("copy", new SystemCursorMode(Cursor.Copy));
            modes.AddStatic("move", new SystemCursorMode(Cursor.Move));
            modes.AddStatic("no-drop", new SystemCursorMode(Cursor.NoDrop));
            modes.AddStatic("not-allowed", new SystemCursorMode(Cursor.NotAllowed));
            modes.AddStatic("e-resize", new SystemCursorMode(Cursor.EResize));
            modes.AddStatic("n-resize", new SystemCursorMode(Cursor.NResize));
            modes.AddStatic("ne-resize", new SystemCursorMode(Cursor.NeResize));
            modes.AddStatic("nw-resize", new SystemCursorMode(Cursor.NwResize));
            modes.AddStatic("s-resize", new SystemCursorMode(Cursor.SResize));
            modes.AddStatic("se-resize", new SystemCursorMode(Cursor.SeResize));
            modes.AddStatic("sw-resize", new SystemCursorMode(Cursor.WResize));
            modes.AddStatic("w-resize", new SystemCursorMode(Cursor.WResize));
            modes.AddStatic("ew-resize", new SystemCursorMode(Cursor.EwResize));
            modes.AddStatic("ns-resize", new SystemCursorMode(Cursor.NsResize));
            modes.AddStatic("nesw-resize", new SystemCursorMode(Cursor.NeswResize));
            modes.AddStatic("nwse-resize", new SystemCursorMode(Cursor.NwseResize));
            modes.AddStatic("col-resize", new SystemCursorMode(Cursor.ColResize));
            modes.AddStatic("row-resize", new SystemCursorMode(Cursor.RowResize));
            modes.AddStatic("all-scroll", new SystemCursorMode(Cursor.AllScroll));
            modes.AddStatic("zoom-in", new SystemCursorMode(Cursor.ZoomIn));
            modes.AddStatic("zoom-out", new SystemCursorMode(Cursor.ZoomOut));
            modes.AddStatic("grab", new SystemCursorMode(Cursor.Grab));
            modes.AddStatic("grabbing", new SystemCursorMode(Cursor.Grabbing));
            modes.AddConstructed<CustomCursorMode>();
            modes.AddMultiple<MultiCursorMode>();
        }

        internal CSSCursorProperty()
            : base(PropertyNames.Cursor)
        {
            _mode = _auto;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            CursorMode mode;

            if (modes.TryCreate(value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes
        
        abstract class CursorMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The browser determines the cursor to display based on the current context.
        /// </summary>
        sealed class AutoCursorMode : CursorMode
        {
        }

        /// <summary>
        /// A url pointing to an image file.
        /// </summary>
        sealed class CustomCursorMode : CursorMode
        {
            Location _url;
            Single _x;
            Single _y;

            public CustomCursorMode(Location url, Single? x = null, Single? y = null)
            {
                _url = url;
                _x = x ?? 0f;
                _y = y ?? 0f;
            }
        }

        /// <summary>
        /// A list of urls pointing to an image files, giving the fallback order.
        /// </summary>
        sealed class MultiCursorMode : CursorMode
        {
            List<CursorMode> _preferences;

            public MultiCursorMode(List<CursorMode> preferences)
            {
                _preferences = preferences;
            }
        }

        /// <summary>
        /// Sets the cursor from a predefined list of cursors.
        /// </summary>
        sealed class SystemCursorMode : CursorMode
        {
            Cursor _cursor;

            public SystemCursorMode(Cursor cursor)
            {
                _cursor = cursor;
            }
        }

        #endregion

        #region Cursors

        enum Cursor
        {
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

        #endregion
    }
}
