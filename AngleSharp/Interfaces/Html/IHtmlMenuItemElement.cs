namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the menuitem HTML element.
    /// </summary>
    [DomName("HTMLMenuItemElement")]
    public interface IHtmlMenuItemElement : IHtmlElement
    {
        /// <summary>
        /// Gets the assigned master command, if any.
        /// </summary>
        [DomName("command")]
        IHtmlElement Command { get; }

        /// <summary>
        /// Gets or sets the type of command.
        /// </summary>
        [DomName("type")]
        String Type { get; set; }

        /// <summary>
        /// Gets or sets the user-visible label.
        /// </summary>
        [DomName("label")]
        String Label { get; set; }

        /// <summary>
        /// Gets or sets the icon for the command.
        /// </summary>
        [DomName("icon")]
        String Icon { get; set; }

        /// <summary>
        /// Gets or sets if the menuitem element is enabled or disabled.
        /// </summary>
        [DomName("disabled")]
        Boolean IsDisabled { get; set; }

        /// <summary>
        /// Gets or sets if the menuitem element is checked or not.
        /// </summary>
        [DomName("checked")]
        Boolean IsChecked { get; set; }

        /// <summary>
        /// Gets or sets if the menuitem element is the default command.
        /// </summary>
        [DomName("default")]
        Boolean IsDefault { get; set; }

        /// <summary>
        /// Gets or sets the name of group of commands to
        /// treat as a radio button group.
        /// </summary>
        [DomName("radiogroup")]
        String RadioGroup { get; set; }
    }
}
