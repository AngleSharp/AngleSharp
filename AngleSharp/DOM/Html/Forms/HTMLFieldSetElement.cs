using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML fieldset element.
    /// </summary>
    public sealed class HTMLFieldSetElement : HTMLFormControlElement
    {
        /// <summary>
        /// The fieldset tag.
        /// </summary>
        internal const string Tag = "fieldset";

        /// <summary>
        /// Creates a new HTML fieldset element.
        /// </summary>
        internal HTMLFieldSetElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the element has any descendent elements that do not
        /// satisfy their constraints.
        /// </summary>
        public bool IsInvalid
        {
            get { return _children.QuerySelector("*:invalid") != null; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }
    }
}
