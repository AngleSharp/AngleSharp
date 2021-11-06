---
title: "Configuration"
section: "AngleSharp.Core"
---
# HTML Parser Options

The HTML parser comes with some options, which may be helpful to cover specific scenarios.

## `IsNotConsumingCharacterReferences`

If `IsNotConsumingCharacterReferences` is active then every ampersand will just be considered as an ampersand. For serialization that still means that ampersands are represented as `&amp;`, but we know that we could just replace `&amp;` with `&`.

Alternatively, we could use our own formatter.

Let's look at one example:

```cs
var formatter = new MyFormatter();
var parser = new HtmlParser(new HtmlParserOptions
{
    IsNotConsumingCharacterReferences = true,
});
var html = "<html><head></head><body><p>&amp;foo</p></body></html>";
var document = parser.ParseDocument(html);
Console.WriteLine(document.DocumentElement.ToHtml(formatter));
```

where we use a custom formatter such as

```cs
class MyFormatter : IMarkupFormatter
{
    public string CloseTag(IElement element, bool selfClosing) => HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing);
    public string Comment(IComment comment) => HtmlMarkupFormatter.Instance.Comment(comment);
    public string Doctype(IDocumentType doctype) => HtmlMarkupFormatter.Instance.Doctype(doctype);
    public string LiteralText(ICharacterData text) => HtmlMarkupFormatter.Instance.LiteralText(text);
    public string OpenTag(IElement element, bool selfClosing) => HtmlMarkupFormatter.Instance.OpenTag(element, selfClosing);
    public string Processing(IProcessingInstruction processing) => HtmlMarkupFormatter.Instance.Processing(processing);
    public string Text(ICharacterData text) => HtmlMarkupFormatter.Instance.LiteralText(text);
}
```

Now the outcome looks as follows:

```html
<html><head></head><body><p>&foo</p></body></html>
```

In contrast, turning the `IsNotConsumingCharacterReferences` to `false` yields.

```html
<html><head></head><body><p>&amp;foo</p></body></html>
```

So in summary:

The option has an impact on the parsing of & characters. In `false` (default) we start consuming character references, which is the spec but may "eat" information you want to digest later. In `true` we never consume & characters, but emit them to the DOM (like if `&amp;` would have been seen in spec. compliant mode).

For serialization (e.g., `InnerHtml` use, or more explicit via `ToHtml`), however, we interpret any seen `&` as `&amp;` (this way its round-trip usable, plus compliant with the serialization spec). So in the example above we use a custom formatter to use the character node data literally.

## `IsKeepingSourceReferences`

(tbd)

## `IsSupportingProcessingInstructions`

(tbd)

## `OnCreated`

(tbd)

## `IsStrictMode`

(tbd)

## `IsEmbedded`, `IsNotSupportingFrames`, and `IsScripting`

(tbd)
