namespace AngleSharp.DOM.Navigator
{
    [DomName("NavigatorStorageUtils")]
    interface INavigatorStorageUtilities
    {
        [DomName("yieldForStorageUpdates")]
        void WaitForStorageUpdates();
    }
}
