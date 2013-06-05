using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML progress element.
    /// </summary>
    public sealed class HTMLProgressElement : HTMLElement
    {
        /// <summary>
        /// The progress tag.
        /// </summary>
        public const string Tag = "progress";

        /// <summary>
        /// Creates a new HTML progress element.
        /// </summary>
        public HTMLProgressElement()
        {
            _name = Tag;
        }

        //TODO
        //http://www.w3.org/html/wg/drafts/html/master/forms.html#the-progress-element

        public double Value
        {
            get;
            set;
        }

        public double Max
        {
            get;
            set;
        }

        public double Position
        {
            get;
            private set;
        }

        public NodeList Labels
        {
            get
            {
                //TODO
                return null;
            }
        }

        protected internal override bool IsSpecial
        {
            get { return false; }
        }
    }
}
