namespace AngleSharp.Dom.Html
{
    /// <summary>
    /// The list of possible table rules.
    /// </summary>
    public enum TableRules
    {
        /// <summary>
        /// No rules. This is the default value.
        /// </summary>
        None,
        /// <summary>
        /// Rules will appear between rows only.
        /// </summary>
        Rows,
        /// <summary>
        /// Rules will appear between columns only.
        /// </summary>
        Cols,
        /// <summary>
        /// Rules will appear between row groups and column groups only.
        /// </summary>
        Groups,
        /// <summary>
        /// Rules will appear between all rows and columns.
        /// </summary>
        All
    }
}
