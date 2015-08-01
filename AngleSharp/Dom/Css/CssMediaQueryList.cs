namespace AngleSharp.Dom.Css
{
    using System;

    sealed class CssMediaQueryList : EventTarget, IMediaQueryList
    {
        readonly IMediaList _media;
        readonly IWindow _window;

        public event DomEventHandler Changed;

        public CssMediaQueryList(IWindow window, IMediaList media)
        {
            _window = window;
            _media = media;
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
            //TODO use Validate with RenderDevice
            get { return true; }
        }
    }
}
