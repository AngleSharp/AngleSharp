---
title: "Questions"
section: "AngleSharp.Core"
---
# Frequently Asked Questions

## How to download a picture?

This is nothing directly to do with AngleSharp. You can perform any kind of requests to some URL.

Here is an example:

```cs
var imageUrl = @"https://via.placeholder.com/150";
var localPath = @"g:\downloads\image.jpg";

using (var client = new HttpClient())
{
	using (var response = await client.GetAsync(imageUrl))
	{
		using (var source = await response.Content.ReadAsStreamAsync())
		{
			using (var target = File.OpenWrite(localPath))
			{
				await source.CopyToAsync(target);
			}
		}
	}
}
```

If there is some reason for needing to, e.g., re-use some cookies obtained via AngleSharp then you can either share the cookie container or use the requester from AngleSharp.

```cs
var imageUrl = @"https://via.placeholder.com/150";
var localPath = @"g:\downloads\image.jpg";
var download = context.GetService<IDocumentLoader>().FetchAsync(new DocumentRequest(new Url(imageUrl)));

using (var response = await download.Task)
{
	using (var target = File.OpenWrite(localPath))
	{
		await response.Content.CopyToAsync(target);
	}
}
```

This assumes a configuration / context such as

```cs
IConfiguration config = Configuration.Default
    .WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
    .WithCookies();
IBrowsingContext context = BrowsingContext.New(config);
```

## Is it possible to get the HTML after JavaScript and Blazor run?

AngleSharp is just a browser core and even though running JavaScript is possible (there is an experimental plugin out there), it will not work with complicated stuff (e.g., running Angular). I do not know if any WASM plugin exists, so I guess running something like Blazor is not possible unless someone codes the WASM plugin.

## How to convert HTML to XML using AngleSharp?

Unfortunately there is no (always working, i.e., silver bullet) way to convert HTML to XML – both formats are actually incompatible. The problem gets even more severe with incompatible object models, e.g., creating a DOM from AngleSharp and converting it to an `XmlDocument` instance.

You would need to perform the conversion "manually" with a mapping function. As the formats are incompatible you will need to specify what to convert and how to react in the cases where no mapping exists...

## How to deal with other methods of authentication (e.g., Kerberos)?

This is highly dependent on the authentication scheme. Let's say we use Windows authentication scheme ("IWA" or sometimes referred to as NTLM/Kerberos). There are several ways of archiving this (you will definitely need the `CookieContainer` to be active, so `WithCookies()` is required, however, will not get you authenticated in the first place). You will definitely want to either use the `HttpRequester` from the *AngleSharp.Io* library (as this one offers you the ability to re-configure it) or roll out your own implementation of an IRequester.

For the old `HttpWebRequest` you just set the right authentication level and credentials (e.g., as below) to get the proper authentication going.

```cs
req.AuthenticationLevel = System.Net.Security.AuthenticationLevel.MutualAuthRequested;
req.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
```

Another very common scenario is standard network credentials. These can also be easier supplied using the `HttpClient`-based approach of *AngleSharp.Io*.

```cs
var credentials = new NetworkCredential("user", "pass", "domain");
var handler = new HttpClientHandler { Credentials = credentials };
IConfiguration config = Configuration.Default
    .WithRequesters(handler)
    .WithCookies()
    .WithDefaultLoader();
IBrowsingContext context = BrowsingContext.New(config);
IDocument document = await context.OpenAsync(url);
```

## How to use a proxy with AngleSharp?

We recommend using the `HttpClient`-based `IRequester` implementation from *AngleSharp.Io*. This one can be properly re-configured.

As an example the following handler may be used coming from some `proxyServerSettings` providing some `Port` and `Address`.

```cs
var handler = new HttpClientHandler()
{
    Proxy = new WebProxy(String.Format("{0}:{1}", proxyServerSettings.Address, proxyServerSettings.Port), false),
    PreAuthenticate = true,
    UseDefaultCredentials = false,
};
```

## The content in textareas is HTML encoded!

Given some content like

```html
<p><textarea>one<p>two
```

AngleSharp views this parts like that:

```html
<p><textarea>one&lt;p&gt;two</textarea></p>
```

This is standard behavior as defined in the official HTML specification. The textarea tag switches to a new parse state and does not automatically close. It needs to encounter a textarea closing tag for being closed. This new parsing state essentially ignores all reserved characters (e.g., `<`), which leads to the serialization representation that you see using to their encoded values.

So the problem is not the encoding (this is just a serialization representation), but rather, that the textarea did not close, which will now place all (assumed?) children in the textarea as raw input.

There is, unfortunately, nothing that you can do here - you will need to close the `textarea`. All browsers (hence the initial remark with the specification) see it the same way - so this is not unique to AngleSharp.

## What can I "click" in AngleSharp?

The only thing you can click with AngleSharp (Core, i.e., non-JS) is everything that has an anchor (the link will be followed), such as `a`, or submit (e.g., `button`) buttons where the form will be submitted. If, e.g., we have a `div` that has a click handler defined in JS nothing would come out.

## How can I perform a click on a div without a UI?

Let's first visit again what can be done with AngleSharp:

- Any kind of requests incl. their manipulation (on request, but also before response)
- General cookie management (and their manipulation, of course)
- Querying the DOM and perform "simple" actions (e.g., clicking a button, submitting a form)
- Running trivial JavaScript files

Here trivial means: Scripts that do not need any capabilities beyond what AngleSharp offers, e.g., rendering tree information, advanced CSSOM access, ... - or scripts that require non-ES5 compliant parsers (e.g., make use of ES6 or some special non-standard capabilities).

The problem is that in order to "click" a div on a page a script needs to be run. This script can now fall into the "trivial" category, however, most likely it is not. Now you have 2 options:

- Try it out, and maybe it works / great, otherwise ...
- See what the script is doing (obviously some HTTP request eventually ...) and do the same

The latter can of course be re-implemented in C# / AngleSharp. So you can create an HTTP request, get the data and either do something on that data set directly (it may be JSON and already what you want ....) or (if it is serving partial HTML) re-parse it and integrate it on the real page.

## How can I remove all elements matching a certain selector?

The following code works for all `span` elements. Make sure to adjust the selector according to your problem.

```cs
foreach (var element in document.QuerySelectorAll('span'))
{
    element.Remove();
}
```

## How is DocumentUri different from Url?

The properties correspond to DOM properties of the same names.

Per MDN:

> HTML documents have a document.URL property which returns the same value [as document.documentURI]. Unlike URL, documentURI is available on all types of documents.

So theoretically, only `DocumentUri` is guaranteed to always return a value.

## How can a set of URLs from be extracted from an HTML document using LINQ?

Let's say the URLs can always be found in standard anchor links (`a`). One possible way is to use

```cs
IEnumerable<IHtmlAnchorElement> links = document.Links
    .OfType<IHtmlAnchorElement>()
    .Select(e => e.Href)
    .Where(h => h.Contains(keyword));
```

Depending on our criteria we may use different LINQ statements (or at least a different `Where` clause).

## How to specify an input file to DOM of `<input type='file'>`?

Every `IHtmlInputElement` has a `Files` property that can be used to add files.

```cs
var input = document.QuerySelector<IHtmlInputElement>("input[type=file][name=myInputFile]");
input?.Files.Add(file);
```

In the previously used example the file variable refers to any IFile instance. AngleSharp is a PCL does not come with a proper implementation out of the box, however, a simple one may look like:

```cs
class FileEntry : IFile
{
    private readonly String _fileName;
    private readonly Stream _content;
    private readonly String _type;
    private readonly DateTime _modified;

    public FileEntry(String fileName, String type, Stream content)
    {
        _fileName = fileName;
        _type = type;
        _content = content;
        _modified = DateTime.Now;
    }

    public Stream Body
    {
        get { return _content; }
    }

    public Boolean IsClosed
    {
        get { return _content.CanRead == false; }
    }

    public DateTime LastModified
    {
        get { return _modified; }
    }

    public Int32 Length
    {
        get
        {
            return (Int32)_content.Length;
        }
    }

    public String Name
    {
        get { return _fileName; }
    }

    public String Type
    {
        get { return _type; }
    }

    public void Close()
    {
        _content.Close();
    }

    public void Dispose()
    {
        _content.Dispose();
    }

    public IBlob Slice(Int32 start = 0, Int32 end = Int32.MaxValue, String contentType = null)
    {
        var ms = new MemoryStream();
        _content.Position = start;
        var buffer = new Byte[Math.Max(0, Math.Min(end, _content.Length) - start)];
        _content.Read(buffer, 0, buffer.Length);
        ms.Write(buffer, 0, buffer.Length);
        _content.Position = 0;
        return new FileEntry(_fileName, _type, ms);
    }
}
```

A more sophisticated one would auto-determine the MIME type and have constructor overloads to allow passing in (local) file paths etc.

## How to parse text from anonymous block?

Text is modeled as a `TextNode`, it is a type of node beside element, comment node, processing instruction, etc. That's why `NextElementSibling` you tried didn't include the text in the result since it intended to return elements only, as the name suggests.

You can get text nodes located directly within product div by traversing through the div's `ChildNodes` and then filter by `NodeType`, for example:

```cs
IHtmlCollection<IElement> products = document.QuerySelectorAll("div.product");

foreach (var product in products)
{
    INode productTitle = product.ChildNodes
        .First(o => o.NodeType == NodeType.Text && o.TextContent.Trim() != "");
    Console.WriteLine(productTitle.TextContent.Trim());
}
```

Notice that newlines between elements are also text nodes, so we need to filter those out.

## How to produce self-closing tags?

Given the following usage scenario:

```cs
IBrowsingContext context = BrowsingContext.New();
IDocument document = await context.OpenNewAsync();

IElement tag = document.CreateElement("customTag");
tag.SetAttribute("attr", "x");
tag.AsSelfClosing();

Console.WriteLine(tag.OuterHtml);
tag.ToHtml(Console.Out, CustomHtmlMarkupFormatter.Instance);
```

We get the following output:

```html
<customtag attr="x">
<customtag attr="x" />
```

There are two places where you can work some stuff in to achieve such thing.

- `readonly NodeFlags Node._flags`: Keep in mind that this field, its property and the host class are all not exposed. So you would need to some dirty hack to get the job one. Also, the default formatter `HtmlMarkupFormatter` use only `>`, and not `/>`.
- Create your own `IMarkupFormatter`.

Here is a solution that uses both mentioned points.

Let's start with some dirty hack:

```cs
public static class ElementExtensions
{
    public static void AsSelfClosing(this IElement element)
    {
        const int SelfClosing = 0x1;

        var type = typeof(IElement).Assembly.GetType("AngleSharp.Dom.Node");
        var field = type.GetField("_flags", BindingFlags.Instance | BindingFlags.NonPublic);

        var flags = (uint)field.GetValue(element);
        flags |= SelfClosing;
        field.SetValue(element, Enum.ToObject(field.FieldType, flags));
    }
}
```

Now let's roll out our own markup formatter:

```cs
public class CustomHtmlMarkupFormatter : IMarkupFormatter
{
    public static readonly CustomHtmlMarkupFormatter Instance = new CustomHtmlMarkupFormatter();

    public string Text(String text) => HtmlMarkupFormatter.Instance.Text(text);
    public string Comment(IComment comment) => HtmlMarkupFormatter.Instance.Comment(comment);
    public string Processing(IProcessingInstruction processing) => HtmlMarkupFormatter.Instance.Processing(processing);
    public string Doctype(IDocumentType doctype) => HtmlMarkupFormatter.Instance.Doctype(doctype);
    public string CloseTag(IElement element, Boolean selfClosing) => HtmlMarkupFormatter.Instance.CloseTag(element, selfClosing);
    public string Attribute(IAttr attribute) => HtmlMarkupFormatter.Instance.Attribute(attribute);

    public string OpenTag(IElement element, Boolean selfClosing)
    {
        var temp = new StringBuilder();
        temp.Append('<');

        if (!String.IsNullOrEmpty(element.Prefix))
        {
            temp.Append(element.Prefix).Append(':');
        }

        temp.Append(element.LocalName);

        foreach (var attribute in element.Attributes)
        {
            temp.Append(" ").Append(Instance.Attribute(attribute));
        }

        temp.Append(selfClosing ? " />" : ">");

        return temp.ToString();
    }
}
```

## How to use AngleSharp in Unity?

The following steps will allow AngleSharp to be fully integrated from NuGet into a Unity solution.

1. Get the AngleSharp NuGet package from the VS NuGet Package manager.
2. Build the solution (Build -> Build Solution)
3. Copy the "netstandard2.0" folder into the unity Assets folder. You can find it in "[your project]\Packages\AngleSharp.0.11.0". Version may vary.

More details why this is the current approach can be found in this [StackOverflow](https://stackoverflow.com/questions/53447595/nuget-packages-in-unity) answer. This behavior is known and the advised approach is to use VS for installing the NuGet (which resolves the .NET Standard 2.0 dependency as it should be).

The answer was taken from a discussion in issue #774.

## How to create elements from a string?

This is possible using a document fragment.

There are multiple possibilities how to use a document fragment, one way would be to use fragment parsing for generating a node list in the right (element) context:

```cs
IBrowsingContext context = BrowsingContext.New(Configuration.Default);
IDocument document = await context.OpenAsync(r => r.Content("<div id=app><div>Some already available content...</div></div>"));
IElement app = document.QuerySelector("#app");
IHtmlParser parser = context.GetService<IHtmlParser>();
INodeList nodes = parser.ParseFragment("<div id='div1'>hi<p>world</p></div>", app);
app.Append(nodes.ToArray());
```

The example shows how nodes can be created in the context of a certain element (#app in this case) and that the behavior is different than, e.g., using `InnerHtml`, which would remove existing nodes.

## Can I retrieve the positions of elements in the source code?

By default, AngleSharp will throw away the "tokens" that associate the element with a position in the source code. This is mostly done due to the required memory consumption. The tag tokens transport not only the position, but also some additional fields like the name, flags and other meta information, as well as attributes. These tokens, however, can be preserved.

Currently, there are two ways to do this (both accessible via the `HtmlParserOptions`).

1. For one-time scenarios during parsing the `OnCreated` callback can be used. The first argument is the `IElement` instance. The second argument received by the callback is a `TextPosition` value.
2. For retrieval at a later point in time the `IsKeepingSourceReferences` option could be set to `true`. This way the `SourceReference` property of all parser-created `IElement` instances will be non-null. Currently, the referenced `ISourceReference` only contains a `Position` property.

In code for option 1 this looks as follows:

```cs
var bodyPos = TextPosition.Empty;
var parser = new HtmlParser(new HtmlParserOptions
{
    OnCreated = (IElement element, TextPosition position) =>
    {
        if (element.TagName == "BODY")
        {
            bodyPos = position;
        }
    },
});
IDocument document = parser.ParseDocument("<!doctype html><body>");
```

The code for option 2 looks as follows:

```cs
var parser = new HtmlParser(new HtmlParserOptions
{
    IsKeepingSourceReferences = true,
});
IDocument document = parser.ParseDocument("<!doctype html><body>");
TextPosition bodyPos = document.Body.SourceReference.Position;
```

In both cases the position we care about will be stored in `bodyPos`.

**Remark**: As `SourceReference` may be empty (e.g., when we omit the provided option or if we select an element that came in *after* parsing) we advise of using `SourceReference?.Position`, where we would end up with a `Nullable<TextPosition>`. Ideally, we then just use `TextPosition.Empty` as the fallback, e.g., in the code above:

```cs
TextPosition bodyPos = document.Body.SourceReference?.Position ?? TextPosition.Empty;
```

## How can I specify encoding for loading a document?

When you have the document available as a stream you may want to give AngleSharp a specific encoding - just like a webserver would do.

Actually, AngleSharp's virtual request API makes that (and other use cases to emulate HTTP-features) quite easy:

```cs
IBrowsingContext context = BrowsingContext.New();

IDocument document = await context.OpenAsync(req => req.Content(myStream).Header("content-type", "text/html; charset=UTF-8"));
```

The encoding decision in AngleSharp follows the same priority list as a browser does. Essentially, that means that the byte-order mark (BOM) always is considered the highest standard for it, but a header has higher precedence than a meta tag found in the source.

In any case, there is also the complication of a "guess" vs a "confident" pick. So the BOM would still be checked as it's standardized per W3C.
