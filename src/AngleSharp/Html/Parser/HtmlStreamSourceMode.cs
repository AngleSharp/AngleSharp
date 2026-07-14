namespace AngleSharp.Html.Parser
{
    /// <summary>
    /// Selects how a byte stream is decoded and retained while parsing HTML.
    /// </summary>
    public enum HtmlStreamSourceMode : System.Byte
    {
        /// <summary>
        /// Uses the compatible source that retains the decoded input and supports encoding restarts.
        /// </summary>
        Buffered,

        /// <summary>
        /// Decodes explicitly UTF-8 input through a bounded character window.
        /// </summary>
        Utf8Streaming,
    }
}
