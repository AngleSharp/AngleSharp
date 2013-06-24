using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for all HTML form control elements.
    /// </summary>
    public abstract class HTMLFormControlElement : HTMLElement, ILabelabelElement
    {
        #region Members

        NodeList labels;

        #endregion

        #region ctor

        internal HTMLFormControlElement()
        {
            labels = new NodeList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if labels are supported.
        /// </summary>
        public Boolean SupportsLabels
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        public NodeList Labels
        {
            get { return labels; }
        }

        #endregion

        #region Internal methods

        /// <summary>
        /// Gets the assigned form if any (use only on selected elements).
        /// </summary>
        /// <returns>The parent form OR assigned form if any.</returns>
        protected HTMLFormElement GetAssignedForm()
        {
            var par = _parent;

            while (!(par is HTMLFormElement))
            {
                if (par == null)
                    break;

                par = par.ParentElement;
            }

            if (par == null && _owner == null)
                return null;
            else if (par == null)
            {
                var formid = GetAttribute("form");

                if (par == null && !string.IsNullOrEmpty(formid))
                    par = _owner.GetElementById(formid);
                else
                    return null;
            }

            return par as HTMLFormElement;
        }

        #endregion
    }
}
