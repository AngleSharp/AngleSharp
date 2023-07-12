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
IDocument document = parser.ParseDocument(html);
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

`IsKeepingSourceReferences` option decides whether to keep positional information or reference on a text source to be serialized.
For serialization, we would have no way or response of source reference of any selected element of a document.

Example of this option be:

```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    IsKeepingSourceReferences = false
});
var html = "<html><head></head><body><p>foo</p></body></html>";
IDocument document = parser.ParseDocument(html);
Console.WriteLine(document.QuerySelector("a").SourceReference?.Position.ToString());
```

The outcome would be none, `SourceReference` would be null as given by the parser.
Turning the option to `true` would give us the position as expected:

`Ln 1, Col 26, Pos 26`

We could also format the html to be pretty and get a prettier result, appending the code:

```cs
document = parser.ParseDocument(document.Prettify());
```
And we would get `Ln 4, Col 3, Pos 33`

## `IsSupportingProcessingInstructions`
`IsSupportingProcessingInstructions` option causes the parser to emit `ProcessingInstruction` nodes whenever `<? ... >` tokens are encountered, those are an SGML and XML node types intended to carry instructions to the application.

SGML PI is enclosed within `<?` and `>`, while XML PI is enclosed within `<?` and `?>`.

Take the following case for example:
```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    IsSupportingProcessingInstructions = true
});
var html = "<html><head></head><body><p><?xml version=\"1.0\" encoding=\"UTF - 8\" ?></p></body></html>";
IDocument document = parser.ParseDocument(html);
Console.WriteLine(document.DocumentElement.ToHtml());
```

this gives the `<html><head></head><body><p><??xml version=\"1.0\" encoding=\"UTF - 8\" ?></p></body></html>` response as we have enabled the PI support option.
Otherwise, we would get a comment node enclosing the issued PI node: `"<html><head></head><body><p><!--<?xml version=\"1.0\" encoding=\"UTF - 8\" ?>--></p></body></html>"`

## `OnCreated`

`OnCreated` feature performs an action based on a new element being created and position within the document which gets parsed by the formatter accordingly.
In detail, it is possible to reposition and change element's attributes and perform other actions on it as it is created with the formatter.

For example, we could perform the following:
```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    OnCreated = (element, position) =>
    {
        if (25 <= position.Index && position.Index < 35)
            element.TextContent += " bar";
    }
});
var html = "<html><head></head><body><p>foo</p></body></html>";
IDocument document = parser.ParseDocument(html);
Console.WriteLine(document.DocumentElement.ToHtml());
```

Which would give us a reformatted html string based on position range \[25, 35\) and element `<p>` within that range being formatted `<html><head></head><body><p>foo bar</p></body></html>`

In general, it would not be expected to pass this option in one parsing for a big enough text as it would take more time to process it.

## `IsStrictMode`

"strict mode" directive from JavaScript's ES5 is represented by `IsStrictMode` option in this case. Simply put, setting this option as true in $HtmlParserOptions$ informs the parser that any JS code that it will include will have a "strict mode" applied in it.

The following code will give us an `HtmlParseException`

```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    IsStrictMode = true
});
var html = "<html><head></head><body><script>x = 0;</script><p>foo</p></body></html>";
IDocument document = parser.ParseDocument(html);
Console.WriteLine(document.DocumentElement.ToHtml());
```

In this case we had strict mode on, so we had to declare `x` as `let x` instead for example.

By default, strict mode is false, and we would get the response as expected.

## `IsEmbedded`, `IsNotSupportingFrames`, and `IsScripting`

The `IsEmbedded` option allows you to use the HTML parser with the content being assumed to be embedded in a valid HTML document already. This is used to determine if a doctype is needed. In embedded mode the doctype can be missing without entering the quirks mode.

Likewise, the `IsNotSupportingFrames` option can be used to act as if frames are not allowed. When actual `frameset` or similar tags are hit, then tags such as `noframes` are interpreted differently (i.e., as if they would not exist). As of today, most browsers still support frames - even though they should not be used anymore. Note that frames do not include `<iframe>`, which is not impacted by this flag.

The `IsScripting` option emulates the behavior of the browser when parsing the `innerHTML`. Without scripting the `noscript` and `script` tag change places. Here, the `noscript` tag will be evaluated (instead of being ignored). Additionally, the content of the `script` tag will be ignored. Enable `IsScripting` to - from a parsing perspective - see the page as JavaScript-enabled browser would do.

**Remark**: Turning on the `IsScripting` option and having a JavaScript engine integrated (e.g., from *AngleSharp.Js*) is not the same. AngleSharp will actually turn on the `IsScripting` automatically when it finds that a JavaScript engine has been included.

## `IsAcceptingCustomElementsEverywhere`

The `IsAcceptingCustomElementsEverywhere` option allows custom elements such as `my-element` to be used in locations where they are usually forbidden.

Take the following HTML:

```html
<html>
    <head>
        <my-element foo="bar"></my-element>
    </head>
</html>
```

The DOM will actually have the `my-element` node in the `body`. It looks like the original HTML has been:

```html
<html>
    <head>
    </head>
    <body>
        <my-element foo="bar"></my-element>
    </body>
</html>
```

In case you want to allow custom elements everywhere you can just provide the flag:

```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    IsAcceptingCustomElementsEverywhere = true
});
var html = @"<html><head><my-element foo=""bar""></my-element></head></html>";
IDocument document = parser.ParseDocument(html);
Console.WriteLine(document.DocumentElement.ToHtml());
```

This will keep the `my-element` in the head. Just remember that the content, i.e., children, of the custom element need to follow the rules of the outer context.

## `IsPreservingAttributeNames`

This option allows you to keep the original attribute names. Usually, attribute names are normalized such that they only use lowercase characters.

Take the following non-valid HTML from an Angular template:

```html
<div *ngIf="condition">Content to render when condition is true.</div>
```

Without this option the HTML DOM would look as if the original source has been:

```html
<div *ngif="condition">Content to render when condition is true.</div>
```

In contrast, you can use this option like:

```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    IsPreservingAttributeNames = true
});
var html = @"<div *ngIf=""condition"">Content to render when condition is true.</div>";
IDocument document = parser.ParseDocument(html);
Console.WriteLine(document.DocumentElement.ToHtml());
```

This will keep the attribute as-is, i.e., like `*ngIf` instead of `*ngif`.
