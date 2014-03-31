namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CSSCursorProperty : CSSProperty
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

        public CSSCursorProperty()
            : base(PropertyNames.CURSOR)
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

        sealed class AutoCursorMode : CursorMode
        {
        }

        sealed class CustomCursorMode : CursorMode
        {
            Uri _url;
            Single _x;
            Single _y;

            public CustomCursorMode(Uri url, Single? x = null, Single? y = null)
            {
                _url = url;
                _x = x ?? 0f;
                _y = y ?? 0f;
            }
        }

        sealed class MultiCursorMode : CursorMode
        {
            List<CursorMode> _preferences;

            public MultiCursorMode(List<CursorMode> preferences)
            {
                _preferences = preferences;
            }
        }

        sealed class SystemCursorMode : CursorMode
        {
            public SystemCursorMode(Cursor cursor)
            {

            }
        }

        #endregion

        #region Cursors

        enum Cursor
        {
            Default,
            None,
            ContextMenu,
            Help,
            Pointer,
            Progress,
            Wait,
            Cell,
            Crosshair,
            Text,
            VerticalText,
            Alias,
            Copy,
            Move,
            NoDrop,
            NotAllowed,
            EResize,
            NResize,
            NeResize,
            NwResize,
            SResize,
            SeResize,
            SwResize,
            WResize,
            EwResize,
            NsResize,
            NeswResize,
            NwseResize,
            ColResize,
            RowResize,
            AllScroll,
            ZoomIn,
            ZoomOut,
            Grab,
            Grabbing,
        }

        #endregion
    }
}
