namespace AngleSharp.ReadOnly;

using System;
using Common;
using Dom;

internal class ReadOnlyHtmlScriptElement : ReadOnlyHtmlElement
{
    // public HtmlScriptElement(Document owner, String? prefix = null, Boolean parserInserted = false, Boolean started = false)
    //     : base(owner, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
    // {
    //     _forceAsync = false;
    //     _started = started;
    //     _parserInserted = parserInserted;
    //     _request = new ScriptRequestProcessor(owner.Context, this);
    // }

    public ReadOnlyHtmlScriptElement(ReadOnlyHtmlDocument document, StringOrMemory prefix = default, Boolean parserInserted = false, Boolean started = false)
        : base(document, TagNames.Script, prefix, NodeFlags.Special | NodeFlags.LiteralText)
    {
        // _forceAsync = false;
        // _started = started;
        // _parserInserted = parserInserted;
        // _request = new ScriptRequestProcessor(document.Context, this);
    }
}