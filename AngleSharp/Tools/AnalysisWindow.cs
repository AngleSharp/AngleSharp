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
    public class AnalysisWindow : EventTarget, IWindow, IWindowProxy
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
        /// Gets the height of the outside of the browser window.
        /// </summary>
        public Int32 OuterHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the width of the outside of the browser window.
        /// </summary>
        public Int32 OuterWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the horizontal distance of the left border of the user's
        /// browser from the left side of the screen.
        /// </summary>
        public Int32 ScreenX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the vertical distance of the top border of the user's
        /// browser from the top side of the screen.
        /// </summary>
        public Int32 ScreenY
        {
            get;
            set;
        }

        public ILocation Location
        {
            get { return Document.Location; }
        }

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
                    var rules = sheet.Rules as CSSRuleList;

                    foreach (var rule in rules.List)
                        rule.ComputeStyle(bag, this, element);
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
        public IWindowProxy Proxy
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

        public event EventListener Aborted
        {
            add { AddEventListener(EventNames.Abort, value); }
            remove { RemoveEventListener(EventNames.Abort, value); }
        }

        public event EventListener Blurred
        {
            add { AddEventListener(EventNames.Blur, value); }
            remove { RemoveEventListener(EventNames.Blur, value); }
        }

        public event EventListener Cancelled
        {
            add { AddEventListener(EventNames.Cancel, value); }
            remove { RemoveEventListener(EventNames.Cancel, value); }
        }

        public event EventListener CanPlay
        {
            add { AddEventListener(EventNames.CanPlay, value); }
            remove { RemoveEventListener(EventNames.CanPlay, value); }
        }

        public event EventListener CanPlayThrough
        {
            add { AddEventListener(EventNames.CanPlayThrough, value); }
            remove { RemoveEventListener(EventNames.CanPlayThrough, value); }
        }

        public event EventListener Changed
        {
            add { AddEventListener(EventNames.Change, value); }
            remove { RemoveEventListener(EventNames.Change, value); }
        }

        public event EventListener Clicked
        {
            add { AddEventListener(EventNames.Click, value); }
            remove { RemoveEventListener(EventNames.Click, value); }
        }

        public event EventListener CueChanged
        {
            add { AddEventListener(EventNames.CueChange, value); }
            remove { RemoveEventListener(EventNames.CueChange, value); }
        }

        public event EventListener DoubleClick
        {
            add { AddEventListener(EventNames.DblClick, value); }
            remove { RemoveEventListener(EventNames.DblClick, value); }
        }

        public event EventListener Drag
        {
            add { AddEventListener(EventNames.Drag, value); }
            remove { RemoveEventListener(EventNames.Drag, value); }
        }

        public event EventListener DragEnd
        {
            add { AddEventListener(EventNames.DragEnd, value); }
            remove { RemoveEventListener(EventNames.DragEnd, value); }
        }

        public event EventListener DragEnter
        {
            add { AddEventListener(EventNames.DragEnter, value); }
            remove { RemoveEventListener(EventNames.DragEnter, value); }
        }

        public event EventListener DragExit
        {
            add { AddEventListener(EventNames.DragExit, value); }
            remove { RemoveEventListener(EventNames.DragExit, value); }
        }

        public event EventListener DragLeave
        {
            add { AddEventListener(EventNames.DragLeave, value); }
            remove { RemoveEventListener(EventNames.DragLeave, value); }
        }

        public event EventListener DragOver
        {
            add { AddEventListener(EventNames.DragOver, value); }
            remove { RemoveEventListener(EventNames.DragOver, value); }
        }

        public event EventListener DragStart
        {
            add { AddEventListener(EventNames.DragStart, value); }
            remove { RemoveEventListener(EventNames.DragStart, value); }
        }

        public event EventListener Dropped
        {
            add { AddEventListener(EventNames.Drop, value); }
            remove { RemoveEventListener(EventNames.Drop, value); }
        }

        public event EventListener DurationChanged
        {
            add { AddEventListener(EventNames.DurationChange, value); }
            remove { RemoveEventListener(EventNames.DurationChange, value); }
        }

        public event EventListener Emptied
        {
            add { AddEventListener(EventNames.Emptied, value); }
            remove { RemoveEventListener(EventNames.Emptied, value); }
        }

        public event EventListener Ended
        {
            add { AddEventListener(EventNames.Ended, value); }
            remove { RemoveEventListener(EventNames.Ended, value); }
        }

        public event EventListener Error
        {
            add { AddEventListener(EventNames.Error, value); }
            remove { RemoveEventListener(EventNames.Error, value); }
        }

        public event EventListener Focused
        {
            add { AddEventListener(EventNames.Focus, value); }
            remove { RemoveEventListener(EventNames.Focus, value); }
        }

        public event EventListener Input
        {
            add { AddEventListener(EventNames.Input, value); }
            remove { RemoveEventListener(EventNames.Input, value); }
        }

        public event EventListener Invalid
        {
            add { AddEventListener(EventNames.Invalid, value); }
            remove { RemoveEventListener(EventNames.Invalid, value); }
        }

        public event EventListener KeyDown
        {
            add { AddEventListener(EventNames.Keydown, value); }
            remove { RemoveEventListener(EventNames.Keydown, value); }
        }

        public event EventListener KeyPress
        {
            add { AddEventListener(EventNames.Keypress, value); }
            remove { RemoveEventListener(EventNames.Keypress, value); }
        }

        public event EventListener KeyUp
        {
            add { AddEventListener(EventNames.Keyup, value); }
            remove { RemoveEventListener(EventNames.Keyup, value); }
        }

        public event EventListener Loaded
        {
            add { AddEventListener(EventNames.Load, value); }
            remove { RemoveEventListener(EventNames.Load, value); }
        }

        public event EventListener LoadedData
        {
            add { AddEventListener(EventNames.LoadedData, value); }
            remove { RemoveEventListener(EventNames.LoadedData, value); }
        }

        public event EventListener LoadedMetadata
        {
            add { AddEventListener(EventNames.LoadedMetaData, value); }
            remove { RemoveEventListener(EventNames.LoadedMetaData, value); }
        }

        public event EventListener Loading
        {
            add { AddEventListener(EventNames.LoadStart, value); }
            remove { RemoveEventListener(EventNames.LoadStart, value); }
        }

        public event EventListener MouseDown
        {
            add { AddEventListener(EventNames.Mousedown, value); }
            remove { RemoveEventListener(EventNames.Mousedown, value); }
        }

        public event EventListener MouseEnter
        {
            add { AddEventListener(EventNames.Mouseenter, value); }
            remove { RemoveEventListener(EventNames.Mouseenter, value); }
        }

        public event EventListener MouseLeave
        {
            add { AddEventListener(EventNames.Mouseleave, value); }
            remove { RemoveEventListener(EventNames.Mouseleave, value); }
        }

        public event EventListener MouseMove
        {
            add { AddEventListener(EventNames.Mousemove, value); }
            remove { RemoveEventListener(EventNames.Mousemove, value); }
        }

        public event EventListener MouseOut
        {
            add { AddEventListener(EventNames.Mouseout, value); }
            remove { RemoveEventListener(EventNames.Mouseout, value); }
        }

        public event EventListener MouseOver
        {
            add { AddEventListener(EventNames.Mouseover, value); }
            remove { RemoveEventListener(EventNames.Mouseover, value); }
        }

        public event EventListener MouseUp
        {
            add { AddEventListener(EventNames.Mouseup, value); }
            remove { RemoveEventListener(EventNames.Mouseup, value); }
        }

        public event EventListener MouseWheel
        {
            add { AddEventListener(EventNames.Wheel, value); }
            remove { RemoveEventListener(EventNames.Wheel, value); }
        }

        public event EventListener Paused
        {
            add { AddEventListener(EventNames.Pause, value); }
            remove { RemoveEventListener(EventNames.Pause, value); }
        }

        public event EventListener Played
        {
            add { AddEventListener(EventNames.Play, value); }
            remove { RemoveEventListener(EventNames.Play, value); }
        }

        public event EventListener Playing
        {
            add { AddEventListener(EventNames.Playing, value); }
            remove { RemoveEventListener(EventNames.Playing, value); }
        }

        public event EventListener Progress
        {
            add { AddEventListener(EventNames.Progress, value); }
            remove { RemoveEventListener(EventNames.Progress, value); }
        }

        public event EventListener RateChanged
        {
            add { AddEventListener(EventNames.RateChange, value); }
            remove { RemoveEventListener(EventNames.RateChange, value); }
        }

        public event EventListener Resetted
        {
            add { AddEventListener(EventNames.Reset, value); }
            remove { RemoveEventListener(EventNames.Reset, value); }
        }

        public event EventListener Resized
        {
            add { AddEventListener(EventNames.Resize, value); }
            remove { RemoveEventListener(EventNames.Resize, value); }
        }

        public event EventListener Scrolled
        {
            add { AddEventListener(EventNames.Scroll, value); }
            remove { RemoveEventListener(EventNames.Scroll, value); }
        }

        public event EventListener Seeked
        {
            add { AddEventListener(EventNames.Seeked, value); }
            remove { RemoveEventListener(EventNames.Seeked, value); }
        }

        public event EventListener Seeking
        {
            add { AddEventListener(EventNames.Seeking, value); }
            remove { RemoveEventListener(EventNames.Seeking, value); }
        }

        public event EventListener Selected
        {
            add { AddEventListener(EventNames.Select, value); }
            remove { RemoveEventListener(EventNames.Select, value); }
        }

        public event EventListener Shown
        {
            add { AddEventListener(EventNames.Show, value); }
            remove { RemoveEventListener(EventNames.Show, value); }
        }

        public event EventListener Stalled
        {
            add { AddEventListener(EventNames.Stalled, value); }
            remove { RemoveEventListener(EventNames.Stalled, value); }
        }

        public event EventListener Submitted
        {
            add { AddEventListener(EventNames.Submit, value); }
            remove { RemoveEventListener(EventNames.Submit, value); }
        }

        public event EventListener Suspended
        {
            add { AddEventListener(EventNames.Suspend, value); }
            remove { RemoveEventListener(EventNames.Suspend, value); }
        }

        public event EventListener TimeUpdated
        {
            add { AddEventListener(EventNames.TimeUpdate, value); }
            remove { RemoveEventListener(EventNames.TimeUpdate, value); }
        }

        public event EventListener Toggled
        {
            add { AddEventListener(EventNames.Toggle, value); }
            remove { RemoveEventListener(EventNames.Toggle, value); }
        }

        public event EventListener VolumeChanged
        {
            add { AddEventListener(EventNames.VolumeChange, value); }
            remove { RemoveEventListener(EventNames.VolumeChange, value); }
        }

        public event EventListener Waiting
        {
            add { AddEventListener(EventNames.Waiting, value); }
            remove { RemoveEventListener(EventNames.Waiting, value); }
        }

        public event EventListener Printed
        {
            add { AddEventListener(EventNames.AfterPrint, value); }
            remove { RemoveEventListener(EventNames.AfterPrint, value); }
        }

        public event EventListener Printing
        {
            add { AddEventListener(EventNames.BeforePrint, value); }
            remove { RemoveEventListener(EventNames.BeforePrint, value); }
        }

        public event EventListener Unloading
        {
            add { AddEventListener(EventNames.Unloading, value); }
            remove { RemoveEventListener(EventNames.Unloading, value); }
        }

        public event EventListener HashChanged
        {
            add { AddEventListener(EventNames.HashChange, value); }
            remove { RemoveEventListener(EventNames.HashChange, value); }
        }

        public event EventListener MessageReceived
        {
            add { AddEventListener(EventNames.Message, value); }
            remove { RemoveEventListener(EventNames.Message, value); }
        }

        public event EventListener WentOffline
        {
            add { AddEventListener(EventNames.Offline, value); }
            remove { RemoveEventListener(EventNames.Offline, value); }
        }

        public event EventListener WentOnline
        {
            add { AddEventListener(EventNames.Online, value); }
            remove { RemoveEventListener(EventNames.Online, value); }
        }

        public event EventListener PageHidden
        {
            add { AddEventListener(EventNames.PageHide, value); }
            remove { RemoveEventListener(EventNames.PageHide, value); }
        }

        public event EventListener PageShown
        {
            add { AddEventListener(EventNames.PageShow, value); }
            remove { RemoveEventListener(EventNames.PageShow, value); }
        }

        public event EventListener PopState
        {
            add { AddEventListener(EventNames.PopState, value); }
            remove { RemoveEventListener(EventNames.PopState, value); }
        }

        public event EventListener Storage
        {
            add { AddEventListener(EventNames.Storage, value); }
            remove { RemoveEventListener(EventNames.Storage, value); }
        }

        public event EventListener Unloaded
        {
            add { AddEventListener(EventNames.Unload, value); }
            remove { RemoveEventListener(EventNames.Unload, value); }
        }

        #endregion

        #region UI Interaction

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
