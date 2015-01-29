namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// The HTMLElement interface represents any HTML element. Some elements directly
    /// implement this interface, other implement it via an interface that inherit it.
    /// </summary>
    [DomName("HTMLElement")]
    public interface IHtmlElement : IElement, IElementCssInlineStyle
    {
        /// <summary>
        /// Gets or sets the value of the lang attribute.
        /// </summary>
        [DomName("lang")]
        String Language { get; set; }

        /// <summary>
        /// Gets or sets the value of the title attribute.
        /// </summary>
        [DomName("title")]
        String Title { get; set; }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        [DomName("dir")]
        String Direction { get; set; }

        /// <summary>
        /// Gets access to all the custom data attributes (data-*) set on the element. It is a map of DOMString,
        /// one entry for each custom data attribute.
        /// </summary>
        [DomName("dataset")]
        IStringMap Dataset { get; }

        /// <summary>
        /// Gets or sets if the element should be translated.
        /// </summary>
        [DomName("translate")]
        Boolean IsTranslated { get; set; }

        /// <summary>
        /// Gets or sets the position of the element in the tabbing order.
        /// </summary>
        [DomName("tabIndex")]
        Int32 TabIndex { get; set; }

        /// <summary>
        /// Gets or sets if spell-checking is activated.
        /// </summary>
        [DomName("spellcheck")]
        Boolean IsSpellChecked { get; set; }

        /// <summary>
        /// Gets or sets whether or not the element is editable. This enumerated
        /// attribute can have the values true, false and inherited.
        /// </summary>
        [DomName("contentEditable")]
        String ContentEditable { get; set; }

        /// <summary>
        /// Gets if the element is currently contenteditable.
        /// </summary>
        [DomName("isContentEditable")]
        Boolean IsContentEditable { get; }

        /// <summary>
        /// Gets or sets if the element is hidden.
        /// </summary>
        [DomName("hidden")]
        Boolean IsHidden { get; set; }

        /// <summary>
        /// Gets or sets if the element is draggable.
        /// </summary>
        [DomName("draggable")]
        Boolean IsDraggable { get; set; }

        /// <summary>
        /// Gets or sets the access key assigned to the element.
        /// </summary>
        [DomName("accessKey")]
        String AccessKey { get; set; }

        /// <summary>
        /// Gets the element's assigned access key.
        /// </summary>
        [DomName("accessKeyLabel")]
        String AccessKeyLabel { get; }

        /// <summary>
        /// Gets or sets the assigned context menu.
        /// </summary>
        [DomName("contextMenu")]
        IHtmlMenuElement ContextMenu { get; set; }

        /// <summary>
        /// Gets the dropzone for this element.
        /// </summary>
        [DomName("dropzone")]
        [DomPutForwards("value")]
        ISettableTokenList DropZone { get; }

        /// <summary>
        /// Simulates a mouse click on an element.
        /// </summary>
        [DomName("click")]
        void DoClick();

        /// <summary>
        /// Puts the keyboard focus on the given element.
        /// </summary>
        [DomName("focus")]
        void DoFocus();

        /// <summary>
        /// Removes the keyboard focus on the given element.
        /// </summary>
        [DomName("blur")]
        void DoBlur();

        /// <summary>
        /// Forces the invocation of a spell check on the content.
        /// </summary>
        [DomName("forceSpellCheck")]
        void DoSpellCheck();

    }
}
