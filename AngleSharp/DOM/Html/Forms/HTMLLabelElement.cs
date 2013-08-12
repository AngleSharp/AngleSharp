using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    [DOM("HTMLLabelElement")]
    public sealed class HTMLLabelElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The label tag.
        /// </summary>
        internal const string Tag = "label";

        #endregion

        #region ctor

        internal HTMLLabelElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the control that the label is assigned for if any.
        /// </summary>
        [DOM("control")]
        public ILabelabelElement Control
        {
            get
            {
                var controlId = GetAttribute("for");

                if (!String.IsNullOrEmpty(controlId))
                {
                    var control = _owner.GetElementById(controlId);

                    if (control is ILabelabelElement)
                        return (ILabelabelElement)control;
                }
                
                return null;
            }
        }

        /// <summary>
        /// Gets the form element that the label is assigned for if
        /// any.
        /// </summary>
        [DOM("form")]
        public HTMLFormElement Form
        {
            get { return GetAssignedForm(); }
        }

        #endregion
    }
}
