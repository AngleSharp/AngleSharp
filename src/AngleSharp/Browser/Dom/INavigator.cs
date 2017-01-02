namespace AngleSharp.Browser.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the navigator information of a browsing context.
    /// </summary>
    [DomName("Navigator")]
    public interface INavigator : INavigatorId, INavigatorContentUtilities, INavigatorStorageUtilities, INavigatorOnline
    {
    }
}
