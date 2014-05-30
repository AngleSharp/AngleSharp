namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;

    interface IQueryElements
    {
        HTMLCollection GetElementsByClassName(String classNames);
        HTMLCollection GetElementsByTagName(String tagName);
        HTMLCollection GetElementsByTagNameNS(String namespaceURI, String tagName);

        Element QuerySelector(String selectors);
        HTMLCollection QuerySelectorAll(String selectors);
    }
}
