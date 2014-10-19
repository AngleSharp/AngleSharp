namespace AngleSharp.Tools
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Css;
    using AngleSharp.DOM.Html;
    using AngleSharp.DOM.Navigator;
    using System;

    /// <summary>
    /// Represents a sample browsing Window implementation for
    /// automated tests, analysis and as a useful playground.
    /// </summary>
    public class AnalysisWindow : EventTarget, IWindow
    {
        #region ctor

        /// <summary>
        /// Creates a new analysis windows context without any starting
        /// document. The height and width properties are also not set.
        /// </summary>
        public AnalysisWindow()
        {
        }

        /// <summary>
        /// Creates a new analysis window starting with the given document.
        /// </summary>
        /// <param name="document">The document to use.</param>
        public AnalysisWindow(IDocument document)
        {
            Document = document;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a reference to the document that the window contains.
        /// </summary>
        public IDocument Document
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the name of the window.
        /// </summary>
        public String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the outside of the browser window.
        /// </summary>
        public Int32 OuterHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the outside of the browser window.
        /// </summary>
        public Int32 OuterWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the horizontal distance of the left border of the user's
        /// browser from the left side of the screen.
        /// </summary>
        public Int32 ScreenX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the vertical distance of the top border of the user's
        /// browser from the top side of the screen.
        /// </summary>
        public Int32 ScreenY
        {
            get;
            set;
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
            get;
            set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gives the values of all the CSS properties of an element after
        /// applying the active stylesheets and resolving any basic computation
        /// those values may contain.
        /// </summary>
        /// <param name="element">The element to compute the styles for.</param>
        /// <param name="pseudo">The optional pseudo selector to use.</param>
        /// <returns>The style declaration describing the element.</returns>
        public ICssStyleDeclaration GetComputedStyle(IElement element, String pseudo = null)
        {
            var document = Document as Document;

            if (document == null)
                throw new ArgumentException("A valid HTML document is required for computing the style of an element.");

            // if pseudo is :before OR ::before then use the corresponding pseudo-element
            // else if pseudo is :after OR ::after then use the corresponding pseudo-element

            var bag = new CssPropertyBag();

            foreach (var stylesheet in document.StyleSheets)
            {
                var sheet = stylesheet as CSSStyleSheet;

                if (sheet != null && !stylesheet.IsDisabled && ((MediaList)stylesheet.Media).Validate(this))//TODO remove cast ASAP
                {
                    var rules = (CSSRuleList)sheet.Rules;
                    rules.ComputeStyle(bag, this, element);
                }
            }

            var htmlElement = element as IHtmlElement;

            if (htmlElement != null)
                bag.ExtendWith(htmlElement.Style, Priority.Inline);

            bag.InheritFrom(element, this);
            return new CSSStyleDeclaration(bag);
        }

        #endregion

        #region Browsing context

        /// <summary>
        /// Gets the proxy to the current browsing context.
        /// </summary>
        public IWindow Proxy
        {
            get { return this; }
        }

        /// <summary>
        /// Gets the user-agent information.
        /// </summary>
        public INavigator Navigator
        {
            get { return null; }
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
            get { return null; }
        }

        void IWindow.Close()
        {
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
    }
}
