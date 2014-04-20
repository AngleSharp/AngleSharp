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

        static readonly Dictionary<String, CursorMode> modes = new Dictionary<String, CursorMode>(StringComparer.OrdinalIgnoreCase);
        static readonly AutoCursorMode _auto = new AutoCursorMode();
        CursorMode _mode;

        #endregion

        #region ctor

        static CSSCursorProperty()
        {
            modes.Add("default", new SystemCursorMode(Cursor.Default));
            modes.Add("none", new SystemCursorMode(Cursor.None));
            modes.Add("context-menu", new SystemCursorMode(Cursor.ContextMenu));
            modes.Add("help", new SystemCursorMode(Cursor.Help));
            modes.Add("pointer", new SystemCursorMode(Cursor.Pointer));
            modes.Add("progress", new SystemCursorMode(Cursor.Progress));
            modes.Add("wait", new SystemCursorMode(Cursor.Wait));
            modes.Add("cell", new SystemCursorMode(Cursor.Cell));
            modes.Add("crosshair", new SystemCursorMode(Cursor.Crosshair));
            modes.Add("text", new SystemCursorMode(Cursor.Text));
            modes.Add("vertical-text", new SystemCursorMode(Cursor.VerticalText));
            modes.Add("alias", new SystemCursorMode(Cursor.Alias));
            modes.Add("copy", new SystemCursorMode(Cursor.Copy));
            modes.Add("move", new SystemCursorMode(Cursor.Move));
            modes.Add("no-drop", new SystemCursorMode(Cursor.NoDrop));
            modes.Add("not-allowed", new SystemCursorMode(Cursor.NotAllowed));
            modes.Add("e-resize", new SystemCursorMode(Cursor.EResize));
            modes.Add("n-resize", new SystemCursorMode(Cursor.NResize));
            modes.Add("ne-resize", new SystemCursorMode(Cursor.NeResize));
            modes.Add("nw-resize", new SystemCursorMode(Cursor.NwResize));
            modes.Add("s-resize", new SystemCursorMode(Cursor.SResize));
            modes.Add("se-resize", new SystemCursorMode(Cursor.SeResize));
            modes.Add("sw-resize", new SystemCursorMode(Cursor.WResize));
            modes.Add("w-resize", new SystemCursorMode(Cursor.WResize));
            modes.Add("ew-resize", new SystemCursorMode(Cursor.EwResize));
            modes.Add("ns-resize", new SystemCursorMode(Cursor.NsResize));
            modes.Add("nesw-resize", new SystemCursorMode(Cursor.NeswResize));
            modes.Add("nwse-resize", new SystemCursorMode(Cursor.NwseResize));
            modes.Add("col-resize", new SystemCursorMode(Cursor.ColResize));
            modes.Add("row-resize", new SystemCursorMode(Cursor.RowResize));
            modes.Add("all-scroll", new SystemCursorMode(Cursor.AllScroll));
            modes.Add("zoom-in", new SystemCursorMode(Cursor.ZoomIn));
            modes.Add("zoom-out", new SystemCursorMode(Cursor.ZoomOut));
            modes.Add("grab", new SystemCursorMode(Cursor.Grab));
            modes.Add("grabbing", new SystemCursorMode(Cursor.Grabbing));
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
            if (value.Is("auto"))
                _mode = _auto;
            else if (value is CSSValueList)
                return Evaluate((CSSValueList)value);
            else if (value == CSSValue.Inherit)
                return true;
            else
            {
                var mode = Evaluate(value);

                if (mode == null)
                    return false;

                _mode = mode;
            }

            return true;
        }

        Boolean Evaluate(CSSValueList values)
        {
            var modes = new List<CursorMode>();
            var entries = values.ToList();
            var acceptMore = true;

            foreach (var entry in entries)
            {
                if (!acceptMore || entry.Length == 0)
                    return false;

                if (entry.Length == 1)
                {
                    var item = Evaluate(entry[0]);

                    if (item == null)
                        return false;

                    acceptMore = item is CustomCursorMode;
                    modes.Add(item);
                }
                else if(entry.Length == 3)
                {
                    var location = entry[0].ToUri();
                    var x = entry[1].ToNumber();
                    var y = entry[2].ToNumber();

                    if (location == null || !x.HasValue || !y.HasValue)
                        return false;

                    modes.Add(new CustomCursorMode(location, x, y));
                }
                else
                    return false;
            }

            if (modes.Count == 1)
                _mode = modes[0];
            else if (modes.Count == 0)
                return false;

            _mode = new MultiCursorMode(modes);
            return true;
        }

        static CursorMode Evaluate(CSSValue value)
        {
            var ident = value as CSSIdentifierValue;

            if (ident != null)
            {
                CursorMode mode;

                if (modes.TryGetValue(ident.Value, out mode))
                    return mode;
            }

            var location = value.ToUri();

            if (location != null)
                return new CustomCursorMode(location);

            return null;
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
