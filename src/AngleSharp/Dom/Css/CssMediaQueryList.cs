namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Events;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the class implementing a media query list for notifications.
    /// </summary>
    sealed class CssMediaQueryList : EventTarget, IMediaQueryList
    {
        #region Fields

        readonly IMediaList _media;
        Boolean _matched;

        #endregion

        #region Events

        public event DomEventHandler Changed
        {
            add { AddEventListener(EventNames.Change, value, false); }
            remove { RemoveEventListener(EventNames.Change, value, false); }
        }

        #endregion

        #region ctor

        public CssMediaQueryList(IWindow window, IMediaList media)
        {
            _media = media;
            _matched = ComputeMatched(window);
            window.Resized += Resized;
        }

        #endregion

        #region Properties

        public String MediaText
        {
            get { return _media.MediaText; }
        }

        public IMediaList Media
        {
            get { return _media; }
        }

        public Boolean IsMatched
        {
            get { return _matched; }
        }

        #endregion

        #region Helpers

        Boolean ComputeMatched(IWindow window)
        {
            //TODO use Validate with RenderDevice
            return false;
        }

        void Resized(Object sender, Event ev)
        {
            var window = (IWindow)sender;
            var matched = ComputeMatched(window);

            if (matched != _matched)
            {
                var eventData = new MediaQueryListEvent(EventNames.Change, false, false, _media.MediaText, matched);
                Dispatch(eventData);
            }

            _matched = matched;
        }

        #endregion
    }
}
