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
var config = Configuration.Default.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true }).WithCookies();
var context = BrowsingContext.New(config);
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
var config = Configuration.Default
    .WithRequesters(handler)
    .WithCookies()
    .WithDefaultLoader();
var context = BrowsingContext.New(config);
var document = await context.OpenAsync(url);
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

## How can I perform a click on a div without an UI?

Let's first visit what can be done with AngleSharp:

- Any kind of requests incl. their manipulation (on request, but also before response)
- General cookie management (and their manipulation, of course)
- Querying the DOM and perform "simple" actions (e.g., clicking a button, submitting a form)
- Running trivial JavaScript files

Here trivial means: Scripts that do not need any capabilities beyond what AngleSharp offers, e.g., rendering tree information, advanced CSSOM access, ... - or scripts that require non-ES5 compliant parsers (e.g., make use of ES6 or some special non-standard capabilities).

The problem is that in order to "click" a div on a page a script needs to be run. This script can now fall into the "trivial" category, however, most likely it is not. Now you have 2 options:

- Try it out and maybe it works / great, otherwise ...
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
var links = document
    .Links
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
var products = document.QuerySelectorAll("div.product");

foreach (var product in products)
{
    var productTitle = product.ChildNodes
        .First(o => o.NodeType == NodeType.Text && o.TextContent.Trim() != "");
    Console.WriteLine(productTitle.TextContent.Trim());
}
```

Notice that newlines between elements are also text nodes, so we need to filter those out.

## How to produce self-closing tags?

Given the following usage scenario:

```cs
var document = new HtmlParser().Parse("");

var tag = document.CreateElement("customTag");
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