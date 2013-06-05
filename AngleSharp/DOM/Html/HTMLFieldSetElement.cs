using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML fieldset element.
    /// </summary>
    public class HTMLFieldSetElement : HTMLElement
    {
        /// <summary>
        /// THe fieldset tag.
        /// </summary>
        public const string Tag = "fieldset";

        /// <summary>
        /// Creates a new HTML fieldset element.
        /// </summary>
        public HTMLFieldSetElement()
        {
            _name = Tag;
        }

        /// <summary>
        /// Gets if the element has any descendent elements that do not
        /// satisfy their constraints.
        /// </summary>
        public bool IsInvalid
        {
            get
            {
                return _children.QuerySelector("*:invalid") != null;
            }
        }

        protected internal override bool IsSpecial
        {
            get
            {
                return true;
            }
        }
    }
}
