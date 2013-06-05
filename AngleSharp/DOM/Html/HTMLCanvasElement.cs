using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML canvas element.
    /// </summary>
    public sealed class HTMLCanvasElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The canvas tag.
        /// </summary>
        public const string Tag = "canvas";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML canvas element.
        /// </summary>
        public HTMLCanvasElement()
        {
            _name = Tag;
        }

        #endregion

        #region Methods

        //TODO
        //http://www.w3.org/html/wg/drafts/html/master/embedded-content-0.html#the-canvas-element

        public object GetContext(string contextId, params object[] args)
        {
            //TODO
            throw new NotImplementedException();
        }

        public bool SupportsContext(string contextId, params object[] args)
        {
            //TODO
            return false;
        }

        public void SetContext(object context)
        {
            //TODO
        }

        public object TransferControlToProxy()
        {
            //TODO
            throw new NotImplementedException();
        }

        public string ToDataURL(string type = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        public void ToBlob(object callback, string type = null)
        {
            //TODO
            throw new NotImplementedException();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the displayed width of the canvas element.
        /// </summary>
        public uint Width
        {
            get { return ToInteger(GetAttribute("width"), 300u); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the displayed height of the canvas element.
        /// </summary>
        public uint Height
        {
            get { return ToInteger(GetAttribute("height"), 150u); }
            set { SetAttribute("height", value.ToString()); }
        }

        #endregion

        #region Internal properties

        protected internal override bool IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
