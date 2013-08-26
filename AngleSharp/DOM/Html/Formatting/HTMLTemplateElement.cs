using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the template element.
    /// </summary>
    sealed class HTMLTemplateElement : HTMLElement
    {
        internal HTMLTemplateElement()
        {
            _name = Tags.TEMPLATE;
        }
    }
}
