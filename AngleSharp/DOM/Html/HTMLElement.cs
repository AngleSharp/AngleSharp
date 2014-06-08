namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents a standard HTML element in the node tree.
    /// </summary>
    [DOM("HTMLElement")]
    public class HTMLElement : Element
    {
        #region ctor

        /// <summary>
        /// Creates a standard HTML element.
        /// </summary>
        internal HTMLElement()
        {
            _ns = Namespaces.Html; 
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets the status if this node is in the HTML namespace.
        /// </summary>
        internal protected override Boolean IsInHtml
        {
            get { return true; }
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        [DOM("cloneNode")]
        public override Node CloneNode(Boolean deep = true)
        {
            var node = HTMLFactory.Create(_name, _owner);
            CopyProperties(this, node, deep);
            CopyAttributes(this, node);
            return node;
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

                if (par == null && !String.IsNullOrEmpty(formid))
                    par = _owner.GetElementById(formid);
                else
                    return null;
            }

            return par as HTMLFormElement;
        }

        #endregion
    }
}
