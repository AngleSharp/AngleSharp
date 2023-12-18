namespace AngleSharp.Benchmarks;

using System;
using Dom;
using Html.Parser;
using Html.Parser.Tokens.Struct;

public struct OnlyElementWithId
{
    private readonly String _id;
    private readonly String _tag;

    private Int32 _depth;
    private Boolean _started;

    public OnlyElementWithId(String tag, String id)
    {
        _tag = tag;
        _id = id;
        _depth = 0;
        _started = false;
    }

    public Result Loop(ref StructHtmlToken token, Next next)
    {
        _started = _started ||
                   token.Type == HtmlTokenType.StartTag &&
                   token.Name.Memory.Span.SequenceEqual(_tag.AsSpan()) &&
                   token.Attributes.HasAttribute(AttributeNames.Id, _id);

        if (_started)
        {
            if (token is { Type: HtmlTokenType.StartTag, IsSelfClosing: false })
            {
                _depth++;
            }
            else if (token.Type == HtmlTokenType.EndTag)
            {
                _depth--;
            }

            if (_depth > 0)
            {
                next(ref token);
            }
            else
            {
                return Result.Stop;
            }
        }

        return Result.Continue;
    }
}