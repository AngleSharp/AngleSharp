# Form Submission by Example

## Standard Forms

One of the most needed functionalities of a dynamic DOM is the possibility to submit forms. Submitting a form with AngleSharp can be as simple as:

```cs
var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
var queryDocument = await context.OpenAsync("https://google.com");
var form = queryDocument.QuerySelector<IHtmlFormElement>("form");
var resultDocument = await form.SubmitAsync(new { q = "anglesharp" });
// e.g., resultDocument.QuerySelectorAll<IHtmlAnchorElement>("#ires .g h3.r a").Select(m => m.Href).Dump();
```

The shown example uses a special overload of the `SubmitAsync` method of an `IHtmlFormElement`, which allows us to pass in an anonymous object that consists of the names of the form fields with their desired values. Alternatively, we could have written code like

```cs
// ...
var queryInput = form.Elements["q"] as IHtmlInputElement;

if (queryInput != null)
{
    queryInput.Value = "anglesharp";
}
```

The latter version is much more verbose and requires additional checking on our side. The upside of this version is that we can also treat the error case (i.e., the expected field with the name `q` is not found or is not an `IHtmlInputElement`), which is completely ignored in the former version.

An important aspect of submitting forms is that we require at least a default loader. Forms usually require making an HTTP request for loading a new document (mostly using the POST verb), which yields the demand for having a requester configured upfront. Note that sometimes forms even come with stronger requirements, especially if they are properly secured. In such cases the use of `WithCookies()` to configure a cookie container could be mandatory.

The code for using a cookie container looks almost identical:

```cs
var config = Configuration.Default
    .WithDefaultLoader()
    .WithCookies();
var context = BrowsingContext.New(config);
// ...
```

## AJAX / JavaScript Powered Forms

(tbd)
