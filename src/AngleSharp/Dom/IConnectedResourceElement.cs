namespace AngleSharp.Dom
{
    /// <summary>
    /// An element that fetches its resource only while it is in a document (as an iframe,
    /// an object or an embed does), which gets told once it got there.
    /// </summary>
    interface IConnectedResourceElement
    {
        /// <summary>
        /// Runs the insertion steps of the element: it was inserted into a document, possibly
        /// as part of a subtree.
        /// </summary>
        void OnConnected();
    }
}
