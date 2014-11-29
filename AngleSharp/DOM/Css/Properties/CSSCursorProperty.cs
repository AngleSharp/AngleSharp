namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/cursor
    /// </summary>
    sealed class CSSCursorProperty : CSSProperty, ICssCursorProperty
    {
        #region Fields

        static readonly Dictionary<String, SystemCursor> modes = new Dictionary<String, SystemCursor>(StringComparer.OrdinalIgnoreCase);
        CustomCursor[] _customs;
        SystemCursor _system;

        #endregion

        #region ctor

        static CSSCursorProperty()
        {
            modes.Add(Keywords.Auto, SystemCursor.Auto);
            modes.Add(Keywords.Default, SystemCursor.Default);
            modes.Add(Keywords.None, SystemCursor.None);
            modes.Add(Keywords.ContextMenu, SystemCursor.ContextMenu);
            modes.Add(Keywords.Help, SystemCursor.Help);
            modes.Add(Keywords.Pointer, SystemCursor.Pointer);
            modes.Add(Keywords.Progress, SystemCursor.Progress);
            modes.Add(Keywords.Wait, SystemCursor.Wait);
            modes.Add(Keywords.Cell, SystemCursor.Cell);
            modes.Add(Keywords.Crosshair, SystemCursor.Crosshair);
            modes.Add(Keywords.Text, SystemCursor.Text);
            modes.Add(Keywords.VerticalText, SystemCursor.VerticalText);
            modes.Add(Keywords.Alias, SystemCursor.Alias);
            modes.Add(Keywords.Copy, SystemCursor.Copy);
            modes.Add(Keywords.Move, SystemCursor.Move);
            modes.Add(Keywords.NoDrop, SystemCursor.NoDrop);
            modes.Add(Keywords.NotAllowed, SystemCursor.NotAllowed);
            modes.Add(Keywords.EastResize, SystemCursor.EResize);
            modes.Add(Keywords.NorthResize, SystemCursor.NResize);
            modes.Add(Keywords.NorthEastResize, SystemCursor.NeResize);
            modes.Add(Keywords.NorthWestResize, SystemCursor.NwResize);
            modes.Add(Keywords.SouthResize, SystemCursor.SResize);
            modes.Add(Keywords.SouthEastResize, SystemCursor.SeResize);
            modes.Add(Keywords.SouthWestResize, SystemCursor.WResize);
            modes.Add(Keywords.WestResize, SystemCursor.WResize);
            modes.Add(Keywords.EastWestResize, SystemCursor.EwResize);
            modes.Add(Keywords.NorthSouthResize, SystemCursor.NsResize);
            modes.Add(Keywords.NorthEastSouthWestResize, SystemCursor.NeswResize);
            modes.Add(Keywords.NorthWestSouthEastResize, SystemCursor.NwseResize);
            modes.Add(Keywords.ColResize, SystemCursor.ColResize);
            modes.Add(Keywords.RowResize, SystemCursor.RowResize);
            modes.Add(Keywords.AllScroll, SystemCursor.AllScroll);
            modes.Add(Keywords.ZoomIn, SystemCursor.ZoomIn);
            modes.Add(Keywords.ZoomOut, SystemCursor.ZoomOut);
            modes.Add(Keywords.Grab, SystemCursor.Grab);
            modes.Add(Keywords.Grabbing, SystemCursor.Grabbing);
        }

        internal CSSCursorProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Cursor, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected system cursor, if any.
        /// </summary>
        public SystemCursor Cursor
        {
            get { return _system; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _customs = new CustomCursor[0];
            _system = SystemCursor.Auto;
        }

        void SetCursor(CustomCursor[] customs, SystemCursor system)
        {
            _customs = customs;
            _system = system;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return TakeList(
                       WithUrl().To(m => new CustomCursor { Url = new Url(m) }).Or(
                       WithArgs(WithUrl(), WithNumber(), WithNumber(), v => new CustomCursor { Url = new Url(v.Item1), X = v.Item2, Y = v.Item3 }))
                   ).RequiresEnd(From(modes)).TryConvert(value, nv => SetCursor(nv.Item1, nv.Item2));
        }

        #endregion

        #region Custom Cursor

        /// <summary>
        /// A url pointing to an image file.
        /// </summary>
        struct CustomCursor
        {
            public Url Url;
            public Single X;
            public Single Y;
        }

        #endregion
    }
}
