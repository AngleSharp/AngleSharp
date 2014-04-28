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
            modes.Add("default", new SystemCursorMode(SystemCursor.Default));
            modes.Add("none", new SystemCursorMode(SystemCursor.None));
            modes.Add("context-menu", new SystemCursorMode(SystemCursor.ContextMenu));
            modes.Add("help", new SystemCursorMode(SystemCursor.Help));
            modes.Add("pointer", new SystemCursorMode(SystemCursor.Pointer));
            modes.Add("progress", new SystemCursorMode(SystemCursor.Progress));
            modes.Add("wait", new SystemCursorMode(SystemCursor.Wait));
            modes.Add("cell", new SystemCursorMode(SystemCursor.Cell));
            modes.Add("crosshair", new SystemCursorMode(SystemCursor.Crosshair));
            modes.Add("text", new SystemCursorMode(SystemCursor.Text));
            modes.Add("vertical-text", new SystemCursorMode(SystemCursor.VerticalText));
            modes.Add("alias", new SystemCursorMode(SystemCursor.Alias));
            modes.Add("copy", new SystemCursorMode(SystemCursor.Copy));
            modes.Add("move", new SystemCursorMode(SystemCursor.Move));
            modes.Add("no-drop", new SystemCursorMode(SystemCursor.NoDrop));
            modes.Add("not-allowed", new SystemCursorMode(SystemCursor.NotAllowed));
            modes.Add("e-resize", new SystemCursorMode(SystemCursor.EResize));
            modes.Add("n-resize", new SystemCursorMode(SystemCursor.NResize));
            modes.Add("ne-resize", new SystemCursorMode(SystemCursor.NeResize));
            modes.Add("nw-resize", new SystemCursorMode(SystemCursor.NwResize));
            modes.Add("s-resize", new SystemCursorMode(SystemCursor.SResize));
            modes.Add("se-resize", new SystemCursorMode(SystemCursor.SeResize));
            modes.Add("sw-resize", new SystemCursorMode(SystemCursor.WResize));
            modes.Add("w-resize", new SystemCursorMode(SystemCursor.WResize));
            modes.Add("ew-resize", new SystemCursorMode(SystemCursor.EwResize));
            modes.Add("ns-resize", new SystemCursorMode(SystemCursor.NsResize));
            modes.Add("nesw-resize", new SystemCursorMode(SystemCursor.NeswResize));
            modes.Add("nwse-resize", new SystemCursorMode(SystemCursor.NwseResize));
            modes.Add("col-resize", new SystemCursorMode(SystemCursor.ColResize));
            modes.Add("row-resize", new SystemCursorMode(SystemCursor.RowResize));
            modes.Add("all-scroll", new SystemCursorMode(SystemCursor.AllScroll));
            modes.Add("zoom-in", new SystemCursorMode(SystemCursor.ZoomIn));
            modes.Add("zoom-out", new SystemCursorMode(SystemCursor.ZoomOut));
            modes.Add("grab", new SystemCursorMode(SystemCursor.Grab));
            modes.Add("grabbing", new SystemCursorMode(SystemCursor.Grabbing));
        }

        internal CSSCursorProperty()
            : base(PropertyNames.Cursor)
        {
            _mode = _auto;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected system cursor, if any.
        /// </summary>
        public SystemCursor Cursor
        {
            get
            {
                if (_mode is SystemCursorMode)
                    return ((SystemCursorMode)_mode).Cursor;

                return SystemCursor.None;
            }
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
            SystemCursor _cursor;

            public SystemCursorMode(SystemCursor cursor)
            {
                _cursor = cursor;
            }

            public SystemCursor Cursor
            {
                get { return _cursor; }
            }
        }

        #endregion
    }
}
