namespace AngleSharp.DOM.Navigator
{
    using System;

    [DomName("NavigatorOnLine")]
    interface INavigatorOnline
    {
        [DomName("onLine")]
        Boolean IsOnline { get; }
    }
}
