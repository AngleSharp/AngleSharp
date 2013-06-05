using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM
{
    interface IQueryElements
    {
        HTMLCollection GetElementsByClassName(string classNames);
        HTMLCollection GetElementsByTagName(string tagName);
        HTMLCollection GetElementsByTagNameNS(string namespaceURI, string tagName);

        Element QuerySelector(string selectors);
        HTMLCollection QuerySelectorAll(string selectors);
    }
}
