namespace AngleSharp.Parser.Html
{
    /// <summary>
    /// Possible insertation mode values.
    /// </summary>
    enum HtmlTreeMode
    {
        /// <summary>
        /// Initial insertation mode.
        /// </summary>
        Initial,
        /// <summary>
        /// Before the html tag insertation mode.
        /// </summary>
        BeforeHtml,
        /// <summary>
        /// Before the head tag insertation mode.
        /// </summary>
        BeforeHead,
        /// <summary>
        /// Within the head tag insertation mode.
        /// </summary>
        InHead,
        /// <summary>
        /// Within the head tag in a noscript section.
        /// </summary>
        InHeadNoScript,
        /// <summary>
        /// After the head tag insertation mode.
        /// </summary>
        AfterHead,
        /// <summary>
        /// Within the body tag insertation mode.
        /// </summary>
        InBody,
        /// <summary>
        /// Within some text area insertation mode.
        /// </summary>
        Text,
        /// <summary>
        /// Within a table tag insertation mode.
        /// </summary>
        InTable,
        /// <summary>
        /// Within the table caption tag.
        /// </summary>
        InCaption,
        /// <summary>
        /// Within the column group tag.
        /// </summary>
        InColumnGroup,
        /// <summary>
        /// Within the table body tag.
        /// </summary>
        InTableBody,
        /// <summary>
        /// Within a table row tag.
        /// </summary>
        InRow,
        /// <summary>
        /// Within a table division tag.
        /// </summary>
        InCell,
        /// <summary>
        /// Within a select tag insertation mode.
        /// </summary>
        InSelect,
        /// <summary>
        /// Within a select tag in a table.
        /// </summary>
        InSelectInTable,
        /// <summary>
        /// Within the template tag.
        /// </summary>
        InTemplate,
        /// <summary>
        /// After the body tag.
        /// </summary>
        AfterBody,
        /// <summary>
        /// Within the frameset tag.
        /// </summary>
        InFrameset,
        /// <summary>
        /// After the frameset tag.
        /// </summary>
        AfterFrameset,
        /// <summary>
        /// After the after the body tag.
        /// </summary>
        AfterAfterBody,
        /// <summary>
        /// Once we are far behind the frameset tag.
        /// </summary>
        AfterAfterFrameset
    }
}
