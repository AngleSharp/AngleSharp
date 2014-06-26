namespace AngleSharp.DOM.Html
{
    using System;

    sealed class HTMLTimeElement : HTMLElement, IHtmlTimeElement
    {
        #region ctor

        internal HTMLTimeElement()
        {
            _name = Tags.Time;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion

        #region Properties

        public String DateTime
        {
            get { return GetAttribute("datetime"); }
            set { SetAttribute("datetime", value); }
        }

        #endregion
    }
}
