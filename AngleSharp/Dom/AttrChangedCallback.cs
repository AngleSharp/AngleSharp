namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// Defines the callback signature to react once an attribute changes.
    /// </summary>
    /// <param name="element">The element hosting the attribute.</param>
    /// <param name="name">The name of the changed attribute.</param>
    /// <param name="value">The new value of the attribute.</param>
    internal delegate void AttrChangedCallback(IElement element, String name, String value);
}
