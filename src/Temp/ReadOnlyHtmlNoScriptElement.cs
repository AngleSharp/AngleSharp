namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlNoScriptElement : ReadOnlyHtmlElement
{
    // public HtmlNoScriptElement(Document owner, String? prefix = null, Boolean? scripting = false)
    //     : base(owner, TagNames.NoScript, prefix, GetFlags(scripting ?? owner.Context.IsScripting()))
    // {
    // }

    public ReadOnlyHtmlNoScriptElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default, Boolean? scripting = false)
        : base(document, TagNames.NoScript, prefix, GetFlags(scripting ?? false))
    {
    }

    private static NodeFlags GetFlags(Boolean scripting)
    {
        if (scripting)
        {
            return NodeFlags.Special | NodeFlags.LiteralText;
        }

        return NodeFlags.Special;
    }
}