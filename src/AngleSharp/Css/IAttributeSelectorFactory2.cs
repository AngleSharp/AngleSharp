namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using System;

    internal enum AttributeSelectorCaseSensitivity : Byte
    {
        Auto,
        CaseInsensitive,
        CaseSensitive,
    }

    internal interface IAttributeSelectorFactory2 : IAttributeSelectorFactory
    {
        ISelector Create(String combinator, String name, String value, String? prefix, AttributeSelectorCaseSensitivity caseSensitivity);
    }
}
