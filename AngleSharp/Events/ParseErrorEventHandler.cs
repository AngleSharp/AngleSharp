using System;

namespace AngleSharp.Events
{
    /// <summary>
    /// Definition for an method that handles parse error events.
    /// </summary>
    /// <param name="sender">The sender of the event (usually the parser).</param>
    /// <param name="e">The error object with detailled information.</param>
    public delegate void ParseErrorEventHandler(Object sender, ParseErrorEventArgs e);
}
