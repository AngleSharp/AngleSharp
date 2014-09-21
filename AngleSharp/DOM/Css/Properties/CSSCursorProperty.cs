namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CSSCursorProperty : CSSProperty, ICssCursorProperty
    {
        #region Fields

        static readonly Dictionary<String, CursorMode> modes = new Dictionary<String, CursorMode>(StringComparer.OrdinalIgnoreCase);
        static readonly AutoCursorMode _auto = new AutoCursorMode();
        CursorMode _mode;

        #endregion

        #region ctor

        static CSSCursorProperty()
        {
            modes.Add(Keywords.Default, new SystemCursorMode(SystemCursor.Default));
            modes.Add(Keywords.None, new SystemCursorMode(SystemCursor.None));
            modes.Add(Keywords.ContextMenu, new SystemCursorMode(SystemCursor.ContextMenu));
            modes.Add(Keywords.Help, new SystemCursorMode(SystemCursor.Help));
            modes.Add(Keywords.Pointer, new SystemCursorMode(SystemCursor.Pointer));
            modes.Add(Keywords.Progress, new SystemCursorMode(SystemCursor.Progress));
            modes.Add(Keywords.Wait, new SystemCursorMode(SystemCursor.Wait));
            modes.Add(Keywords.Cell, new SystemCursorMode(SystemCursor.Cell));
            modes.Add(Keywords.Crosshair, new SystemCursorMode(SystemCursor.Crosshair));
            modes.Add(Keywords.Text, new SystemCursorMode(SystemCursor.Text));
            modes.Add(Keywords.VerticalText, new SystemCursorMode(SystemCursor.VerticalText));
            modes.Add(Keywords.Alias, new SystemCursorMode(SystemCursor.Alias));
            modes.Add(Keywords.Copy, new SystemCursorMode(SystemCursor.Copy));
            modes.Add(Keywords.Move, new SystemCursorMode(SystemCursor.Move));
            modes.Add(Keywords.NoDrop, new SystemCursorMode(SystemCursor.NoDrop));
            modes.Add(Keywords.NotAllowed, new SystemCursorMode(SystemCursor.NotAllowed));
            modes.Add(Keywords.EastResize, new SystemCursorMode(SystemCursor.EResize));
            modes.Add(Keywords.NorthResize, new SystemCursorMode(SystemCursor.NResize));
            modes.Add(Keywords.NorthEastResize, new SystemCursorMode(SystemCursor.NeResize));
            modes.Add(Keywords.NorthWestResize, new SystemCursorMode(SystemCursor.NwResize));
            modes.Add(Keywords.SouthResize, new SystemCursorMode(SystemCursor.SResize));
            modes.Add(Keywords.SouthEastResize, new SystemCursorMode(SystemCursor.SeResize));
            modes.Add(Keywords.SouthWestResize, new SystemCursorMode(SystemCursor.WResize));
            modes.Add(Keywords.WestResize, new SystemCursorMode(SystemCursor.WResize));
            modes.Add(Keywords.EastWestResize, new SystemCursorMode(SystemCursor.EwResize));
            modes.Add(Keywords.NorthSouthResize, new SystemCursorMode(SystemCursor.NsResize));
            modes.Add(Keywords.NorthEastSouthWestResize, new SystemCursorMode(SystemCursor.NeswResize));
            modes.Add(Keywords.NorthWestSouthEastResize, new SystemCursorMode(SystemCursor.NwseResize));
            modes.Add(Keywords.ColResize, new SystemCursorMode(SystemCursor.ColResize));
            modes.Add(Keywords.RowResize, new SystemCursorMode(SystemCursor.RowResize));
            modes.Add(Keywords.AllScroll, new SystemCursorMode(SystemCursor.AllScroll));
            modes.Add(Keywords.ZoomIn, new SystemCursorMode(SystemCursor.ZoomIn));
            modes.Add(Keywords.ZoomOut, new SystemCursorMode(SystemCursor.ZoomOut));
            modes.Add(Keywords.Grab, new SystemCursorMode(SystemCursor.Grab));
            modes.Add(Keywords.Grabbing, new SystemCursorMode(SystemCursor.Grabbing));
        }

        internal CSSCursorProperty()
            : base(PropertyNames.Cursor, PropertyFlags.Inherited)
        {
            _mode = _auto;
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Auto))
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
                    var x = entry[1].ToSingle();
                    var y = entry[2].ToSingle();

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
            CursorMode mode;

            if (modes.TryGetValue(value, out mode))
                return mode;

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
            readonly Url _url;
            readonly Single _x;
            readonly Single _y;

            public CustomCursorMode(Url url, Single? x = null, Single? y = null)
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
            readonly List<CursorMode> _preferences;

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
            readonly SystemCursor _cursor;

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
