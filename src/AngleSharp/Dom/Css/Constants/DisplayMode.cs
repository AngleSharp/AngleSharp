namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// A list of all pre-defined display settings.
    /// </summary>
    public enum DisplayMode : ushort
    {
        /// <summary>
        /// Turns off the display of an element (it has no effect on layout);
        /// all descendant elements also have their display turned off. The
        /// document is rendered as though the element did not exist.
        /// </summary>
        None,
        /// <summary>
        /// The element generates one or more inline element boxes.
        /// </summary>
        Inline,
        /// <summary>
        /// The element generates a block element box.
        /// </summary>
        Block,
        /// <summary>
        /// The element generates a block box for the content and a separate
        /// list-item inline box.
        /// </summary>
        ListItem,
        /// <summary>
        /// The element generates a block element box that will be flowed with
        /// surrounding content as if it were a single inline box (behaving much
        /// like a replaced element would).
        /// </summary>
        InlineBlock,
        /// <summary>
        /// he inline-table value does not have a direct mapping in HTML. It
        /// behaves like a table HTML element, but as an inline box, rather than
        /// a block-level box. Inside the table box is a block-level context.
        /// </summary>
        InlineTable,
        /// <summary>
        /// Behaves like the table HTML element. It defines a block-level box.
        /// </summary>
        Table,
        /// <summary>
        /// Behaves like the caption HTML element.
        /// </summary>
        TableCaption,
        /// <summary>
        /// Behaves like the td HTML element.
        /// </summary>
        TableCell,
        /// <summary>
        /// These elements behave like the corresponding col HTML elements.
        /// </summary>
        TableColumn,
        /// <summary>
        /// These elements behave like the corresponding colgroup HTML elements.
        /// </summary>
        TableColumnGroup,
        /// <summary>
        /// These elements behave like the corresponding tfoot HTML elements.
        /// </summary>
        TableFooterGroup,
        /// <summary>
        /// These elements behave like the corresponding thead HTML elements.
        /// </summary>
        TableHeaderGroup,
        /// <summary>
        /// Behaves like the tr HTML element.
        /// </summary>
        TableRow,
        /// <summary>
        /// These elements behave like the corresponding tbody HTML elements.
        /// </summary>
        TableRowGroup,
        /// <summary>
        /// The element behaves like a block element and lays out its content
        /// according to the flexbox model.
        /// </summary>
        Flex,
        /// <summary>
        /// The element behaves like an inline element and lays out its content
        /// according to the flexbox model.
        /// </summary>
        InlineFlex,
        /// <summary>
        /// The element behaves like a block element and lay out its content
        /// according to the grid model.
        /// </summary>
        Grid,
        /// <summary>
        /// The element behaves like an inline element and lay out its content
        /// according to the grid model.
        /// </summary>
        InlineGrid
    }
}
