namespace AngleSharp.Dom.Css
{
    using AngleSharp.Dom.Events;
    using System;

    sealed class CssMediaQueryList : EventTarget, IMediaQueryList
    {
        readonly IMediaList _media;
        Boolean _matched;

        public event DomEventHandler Changed;

        public CssMediaQueryList(IWindow window, IMediaList media)
        {
            _media = media;
            _matched = ComputeMatched(window);
            window.Resized += Resized;
        }

        Boolean ComputeMatched(IWindow window)
        {
            //TODO use Validate with RenderDevice
            return false;
        }

        void Resized(Object sender, Event ev)
        {
            var window = (IWindow)sender;
            var matched = ComputeMatched(window);

            //TODO use MediaQueryListEvent
            if (matched != _matched && Changed != null)
                Changed(this, new Event());

            _matched = matched;
        }

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
    }
}
