namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The History interface allows to manipulate the browser session history,
    /// that is the pages visited in the tab or frame that the current page is
    /// loaded in.
    /// </summary>
    [DomName("History")]
    public interface IHistory
    {
        /// <summary>
        /// Gets the number of elements in the session history, including the
        /// currently loaded page.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }
        
        /// <summary>
        /// Gets an any value representing the state at the top of the history
        /// stack.
        /// </summary>
        [DomName("state")]
        Object State { get; }
        
        /// <summary>
        /// Loads a page from the session history, identified by its relative
        /// location to the current page, for example -1 for the previous page
        /// or 1 for the next page. When integerDelta is out of bounds (e.g. -1
        /// when there are no previously visited pages in the session history),
        /// the method doesn't do anything and doesn't raise an exception.
        /// Calling go() without parameters or with a non-integer argument has
        /// no effect.
        /// </summary>
        /// <param name="delta">The number of states to surpass.</param>
        [DomName("go")]
        void Go(Int32 delta = 0);
        
        /// <summary>
        /// Goes to the previous page in session history, the same action as
        /// when the user clicks the browser's Back button. Equivalent to
        /// history.go(-1).
        /// </summary>
        [DomName("back")]
        void Back();
        
        /// <summary>
        /// Goes to the next page in session history, the same action as when
        /// the user clicks the browser's Forward button; this is equivalent to
        /// history.go(1).
        /// </summary>
        [DomName("forward")]
        void Forward();
        
        /// <summary>
        /// Pushes the given data onto the session history stack with the
        /// specified title and, if provided, URL. The data is treated as
        /// opaque by the DOM.
        /// </summary>
        /// <param name="data">The data to use.</param>
        /// <param name="title">The title to take.</param>
        /// <param name="url">The URL to consider.</param>
        [DomName("pushState")]
        void PushState(Object data, String title, String url = null);
        
        /// <summary>
        /// Updates the most recent entry on the history stack to have the
        /// specified data, title, and, if provided, URL. The data is treated
        /// as opaque by the DOM.
        /// </summary>
        /// <param name="data">The data to use.</param>
        /// <param name="title">The title to take.</param>
        /// <param name="url">The URL to consider.</param>
        [DomName("replaceState")]
        void ReplaceState(Object data, String title, String url = null);
    }
}
