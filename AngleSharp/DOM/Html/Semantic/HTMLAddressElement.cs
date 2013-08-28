using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngleSharp.DOM.Html
{
    sealed class HTMLAddressElement : HTMLElement
    {
        internal HTMLAddressElement()
        {
            _name = Tags.ADDRESS;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}
