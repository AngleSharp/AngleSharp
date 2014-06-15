namespace AngleSharp.DOM.Navigator
{
    using System;

    [DomName("NavigatorID")]
    interface INavigatorId
    {
        [DomName("appName")]
        String Name { get; }

        [DomName("appVersion")]
        String Version { get; }

        [DomName("platform")]
        String Platform { get; }

        [DomName("userAgent")]
        String UserAgent { get; }
    }
}
