namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using AngleSharp.Dom.Navigator;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services;


    /// <summary>
    /// Represents a sample browsing Window implementation for automated tests,
    /// analysis and as a useful playground.
    /// </summary>
    sealed class Window : EventTarget, IWindow
    {
        #region Fields

        readonly Document _document;
        readonly List<CancellationTokenSource> _tasks;

        String _name;
        Int32 _outerHeight;
        Int32 _outerWidth;
        Int32 _screenX;
        Int32 _screenY;
        String _status;
        Boolean _closed;
        INavigator _navigator;

        #endregion

        #region ctor

        public Window(Document document)
        {
            _document = document;
            _tasks = new List<CancellationTokenSource>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the proxy to the current browsing context.
        /// </summary>
        public IWindow Proxy
        {
            get { return _document.Context.Current; }
        }

        /// <summary>
        /// Gets the user-agent information.
        /// </summary>
        public INavigator Navigator
        {
            get { return _navigator ?? (_navigator = CreateNavigator()); }
        }

        /// <summary>
        /// Gets a reference to the document that the window contains.
        /// </summary>
        public IDocument Document
        {
            get { return _document; }
        }

        /// <summary>
        /// Gets or sets the name of the window.
        /// </summary>
        public String Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// Gets or sets the height of the outside of the browser window.
        /// </summary>
        public Int32 OuterHeight
        {
            get { return _outerHeight; }
            set { _outerHeight = value; }
        }

        /// <summary>
        /// Gets or sets the width of the outside of the browser window.
        /// </summary>
        public Int32 OuterWidth
        {
            get { return _outerWidth; }
            set { _outerWidth = value; }
        }

        /// <summary>
        /// Gets or sets the horizontal distance of the left border of the 
        /// user's browser from the left side of the screen.
        /// </summary>
        public Int32 ScreenX
        {
            get { return _screenX; }
            set { _screenX = value; }
        }

        /// <summary>
        /// Gets or sets the vertical distance of the top border of the user's
        /// browser from the top side of the screen.
        /// </summary>
        public Int32 ScreenY
        {
            get { return _screenY; }
            set { _screenY = value; }
        }

        /// <summary>
        /// Gets the location of the currently contained document.
        /// </summary>
        public ILocation Location
        {
            get { return Document.Location; }
        }

        /// <summary>
        /// Gets or sets the status string.
        /// </summary>
        public String Status
        {
            get { return _status; }
            set { _status = value; }
        }

        /// <summary>
        /// Gets if the window is currently open or already closed.
        /// </summary>
        public Boolean IsClosed
        {
            get { return _closed; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">The element to compute the style for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        public ICssStyleDeclaration GetComputedStyle(IElement element, String pseudo = null)
        {
            var styleCollection = this.GetStyleCollection();
            return styleCollection.ComputeDeclarations(element, pseudo);
        }

        #endregion

        #region Events

        event DomEventHandler IGlobalEventHandlers.Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        event DomEventHandler IGlobalEventHandlers.CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        event DomEventHandler IGlobalEventHandlers.CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        event DomEventHandler IGlobalEventHandlers.CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        event DomEventHandler IGlobalEventHandlers.DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        event DomEventHandler IGlobalEventHandlers.KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        event DomEventHandler IGlobalEventHandlers.KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        event DomEventHandler IGlobalEventHandlers.KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        event DomEventHandler IGlobalEventHandlers.LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        event DomEventHandler IGlobalEventHandlers.LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        event DomEventHandler IGlobalEventHandlers.MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        event DomEventHandler IGlobalEventHandlers.RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        event DomEventHandler IGlobalEventHandlers.TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        event DomEventHandler IGlobalEventHandlers.VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        event DomEventHandler IGlobalEventHandlers.Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        event DomEventHandler IWindowEventHandlers.Printed
        {
            add { AddEventListener(EventNames.AfterPrint, value); }
            remove { RemoveEventListener(EventNames.AfterPrint, value); }
        }

        event DomEventHandler IWindowEventHandlers.Printing
        {
            add { AddEventListener(EventNames.BeforePrint, value); }
            remove { RemoveEventListener(EventNames.BeforePrint, value); }
        }

        event DomEventHandler IWindowEventHandlers.Unloading
        {
            add { AddEventListener(EventNames.Unloading, value); }
            remove { RemoveEventListener(EventNames.Unloading, value); }
        }

        event DomEventHandler IWindowEventHandlers.HashChanged
        {
            add { AddEventListener(EventNames.HashChange, value); }
            remove { RemoveEventListener(EventNames.HashChange, value); }
        }

        event DomEventHandler IWindowEventHandlers.MessageReceived
        {
            add { AddEventListener(EventNames.Message, value); }
            remove { RemoveEventListener(EventNames.Message, value); }
        }

        event DomEventHandler IWindowEventHandlers.WentOffline
        {
            add { AddEventListener(EventNames.Offline, value); }
            remove { RemoveEventListener(EventNames.Offline, value); }
        }

        event DomEventHandler IWindowEventHandlers.WentOnline
        {
            add { AddEventListener(EventNames.Online, value); }
            remove { RemoveEventListener(EventNames.Online, value); }
        }

        event DomEventHandler IWindowEventHandlers.PageHidden
        {
            add { AddEventListener(EventNames.PageHide, value); }
            remove { RemoveEventListener(EventNames.PageHide, value); }
        }

        event DomEventHandler IWindowEventHandlers.PageShown
        {
            add { AddEventListener(EventNames.PageShow, value); }
            remove { RemoveEventListener(EventNames.PageShow, value); }
        }

        event DomEventHandler IWindowEventHandlers.PopState
        {
            add { AddEventListener(EventNames.PopState, value); }
            remove { RemoveEventListener(EventNames.PopState, value); }
        }

        event DomEventHandler IWindowEventHandlers.Storage
        {
            add { AddEventListener(EventNames.Storage, value); }
            remove { RemoveEventListener(EventNames.Storage, value); }
        }

        event DomEventHandler IWindowEventHandlers.Unloaded
        {
            add { AddEventListener(EventNames.Unload, value); }
            remove { RemoveEventListener(EventNames.Unload, value); }
        }

        #endregion

        #region UI Interaction

        IHistory IWindow.History
        {
            get { return _document.Context.SessionHistory; }
        }

        IWindow IWindow.Open(String url, String name, String features, String replace)
        {
            //TODO Context ?
            var document = new HtmlDocument();
            document.Location.Href = url;
            return new Window(document) { Name = name };
        }

        void IWindow.Close()
        {
            _closed = true;
        }

        void IWindow.Stop()
        {
        }

        void IWindow.Focus()
        {
        }

        void IWindow.Blur()
        {
        }

        void IWindow.Alert(String message)
        {
        }

        Boolean IWindow.Confirm(String message)
        {
            return false;
        }

        void IWindow.Print()
        {
        }

        #endregion

        #region Timers

        Int32 IWindowTimers.SetTimeout(Action<IWindow> handler, Int32 timeout)
        {
            return QueueTask(DoTimeout, handler, timeout);
        }

        void IWindowTimers.ClearTimeout(Int32 handle)
        {
            Clear(handle);
        }

        Int32 IWindowTimers.SetInterval(Action<IWindow> handler, Int32 timeout)
        {
            return QueueTask(DoInterval, handler, timeout);
        }

        void IWindowTimers.ClearInterval(Int32 handle)
        {
            Clear(handle);
        }

        #endregion

        #region Helpers

        async Task DoTimeout(Action<IWindow> callback, Int32 timeout, CancellationToken token)
        {
            await token.Delay(timeout).ConfigureAwait(false);

            if (token.IsCancellationRequested)
                return;

            callback(this);
        }

        async Task DoInterval(Action<IWindow> callback, Int32 timeout, CancellationToken token)
        {
            await token.Delay(timeout).ConfigureAwait(false);

            if (token.IsCancellationRequested)
                return;

            _document.QueueTask(DoInterval(callback, timeout, token));
            callback(this);
        }

        Int32 QueueTask(Func<Action<IWindow>, Int32, CancellationToken, Task> taskCreator, Action<IWindow> callback, Int32 timeout)
        {
            var id = _tasks.Count;
            var cts = new CancellationTokenSource();
            _document.QueueTask(() => _document.QueueTask(taskCreator(callback, timeout, cts.Token)));
            _tasks.Add(cts);
            return id;
        }

        void Clear(Int32 handle)
        {
            if (_tasks.Count > handle && _tasks[handle].IsCancellationRequested == false)
                _tasks[handle].Cancel();
        }

        INavigator CreateNavigator()
        {
            var service = _document.Options.GetService<INavigatorService>();

            if (service != null)
                return service.Create(this);

            return null;
        }

        #endregion
    }
}
