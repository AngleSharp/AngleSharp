using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML progress element.
    /// </summary>
    public sealed class HTMLProgressElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The progress tag.
        /// </summary>
        internal const string Tag = "progress";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML progress element.
        /// </summary>
        internal HTMLProgressElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

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

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}
