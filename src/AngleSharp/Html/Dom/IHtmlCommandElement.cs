namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the command HTML element.
    /// </summary>
    [DomName("HTMLCommandElement")]
    public interface IHtmlCommandElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the type of command.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets the assigned label.
        /// </summary>
        [DomName("label")]
        String Label { get; set; }

        /// <summary>
        /// Gets or sets the icon of the command.
        /// </summary>
        [DomName("icon")]
        String Icon { get; set; }

        /// <summary>
        /// Gets or sets if the command is disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets if the command is checked.
        /// </summary>
        [DomName("checked")]
        Boolean IsChecked { get; set; }

        /// <summary>
        /// Gets or sets the id of the radio group of the command.
        /// </summary>
        [DomName("radiogroup")]
        String RadioGroup { get; set; }

        /// <summary>
        /// Gets the assigned element.
        /// </summary>
        [DomName("command")]
        IHtmlElement Command { get; }
    }
}
