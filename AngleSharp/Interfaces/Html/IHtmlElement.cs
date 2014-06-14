namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;

    [DomName("HTMLElement")]
    public interface IHtmlElement
    {
        [DomName("lang")]
        String Lang { get; set; }

        [DomName("title")]
        String Title { get; set; }

        [DomName("dir")]
        DirectionMode Dir { get; set; }

        [DomName("dataset")]
        IStringMap Dataset { get; }

        [DomName("translate")]
        Boolean IsTranslated { get; set; }

        [DomName("style")]
        CSSStyleDeclaration Style { get; }

        [DomName("tabIndex")]
        Int32 TabIndex { get; set; }

        [DomName("spellcheck")]
        Boolean Spellcheck { get; set; }

        [DomName("contentEditable")]
        ContentEditableMode ContentEditable { get; set; }

        [DomName("isContentEditable")]
        Boolean IsContentEditable { get; }
    }
}
