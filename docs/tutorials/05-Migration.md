---
title: "Upgrading"
section: "AngleSharp.Core"
---
# Migration Guide

## 0.17 to 1.0

From an API perspective the two versions are compatible, however, the ABI is not compatible as some constructors changed (adding new optional arguments etc.). Therefore, a recompilation is necessary before dynamically using 1.0.

## 0.16 to 0.17

The support for .NET Framework 4.6 has been dropped. AngleSharp now works exclusively on .NET 4.6.1 or newer / .NET Standard 2.0. If you use an older framework you'll either need to fork AngleSharp or remain on an older version of AngleSharp.

## 0.15 to 0.16

The `Url` class has been moved from `AngleSharp` to `AngleSharp.Dom`. Potentially, you'll need to adjust your `using` statements or use of fully qualified names.

## 0.14 to 0.15

Dropped support for .NET Standard 1.3. AngleSharp now works exclusively on .NET 4.5 or newer / .NET Standard 2.0. If you use an older framework you'll either need to fork AngleSharp or remain on an older version of AngleSharp.

## 0.13 to 0.14

If you implemented `IBrowsingContext` then you'll also need to implement `IDisposable`. Most users should not be affected by this.

## 0.12 to 0.13

Renamed the configuration method `WithCookies` to `WithDefaultCookies`. Our recommendation is to use `WithCookies` from `AngleSharp.Io`.

Removed the `TaskEventLoop`. Usually, since this is a low level construct, it should not have any impact on your code.

## 0.11 to 0.12

For this change we do not expect any migration work unless a custom implementation of `IElement` has been done (unlikely).

## 0.10 to 0.11

This release follows the spirit of 0.10 and prepares for the 1.0 later this year. There are mainly additions, but also one important breaking change: We removed everything that is related to AngleSharp.Xml. This is now part of separate library called AngleSharp.Xml.

### SVG

The `ISvgDocument` interface and its implementation `SvgDocument` have been removed. They are now available via the `AngleSharp.Xml` library. There should be no need to access these types directly - in most cases `IDocument` should be more than sufficient.

### XML

The full `AngleSharp.Xml` namespace has been moved to a dedicated library with the same name.

### XHTML

As with XML also XHTML has been mostly removed. This is not a big change though. It only impacts the `AutoSelectedMarkupFormatter`, which is now part of the AngleSharp.Xml library. Furthermore, it moved from `AngleSharp.Xhtml` to the `AngleSharp.Xml` namespace.

### Peer Dependencies

The peer dependency to the System.Encoding.CodePages package for the .NET Framework release is gone. This is now also a dependency for the .NET Framework target.

## 0.9.x to 0.10 (or later)

The v0.10 release line of AngleSharp is breaking towards formerly used APIs. Even though the same concepts are mostly applied, many things changed and an upgrade from AngleSharp pre v0.10 to 0.10 will certainly break things. The following points should help you to perform the migration as fast as possible.

In the following points the v0.10 release line will be named "current", while older releases will be referred to as "previous".

### Silverlight / Pre .NET 4.5

:warn: These platforms are no longer support. No solution planned.

> Recommendation: Stay at AngleSharp pre-v0.10 for the moment. Sorry for inconvenience!

### Configuration

The way to configure AngleSharp was changed. Earlier, the provided configuration was simply referenced by, e.g., the `BrowsingContext`. Now upon creation the browsing context is doing some evaluation and creates its own copy of the configuration. Thus, a configuration can also be seen as a (re-)usable draft for what will become the options to be considered from a browsing context.

The extension methods for working with an `IConfiguration` type of object changed. Along the standard `With` we now also have

- `WithOnly`, which will remove earlier occurrences of the same type and
- `Without`, which will drop any existing occurrence of the given type.

Additionally, besides the overloads using a plain object and a specific type of service, we also got a creator overload. This overload features a function `Func<IBrowsingContext, T>` (with `T` being the type of the service) to be used once the configuration is used by a browsing context.

The default configuration extenders remained the same (such as `WithDefaultLoader`), however, their arguments may have changed. In case of `WithDefaultLoader` you need to supply an object instead of using a callback. Commonly, instead of doing

```cs
config.WithDefaultLoader(setup => setup.IsResourceLoadingEnabled = true)
```

you now have to write

```cs
config.WithDefaultLoader(new LoaderOptions { IsResourceLoadingEnabled = true })
```

### HTML

The unified parser interface has been changed. It is no longer possible to call `Parse`, instead this is now `ParseDocument`. Hence, some old code like

```cs
IDocument htmlDocument = parser.Parse("");
```

is now

```cs
IDocument htmlDocument = parser.ParseDocument("");
```

Note: Same applies to the `Async` parsing (which is still recommended). Here we now have `ParseDocumentAsync`.

Also, the `HtmlParser` does no longer accept an `IConfiguration` in the constructor. In this case we implicitly created an `BrowsingContext`, which we want to avoid to show the user what is really happening. Instead, a browsing context should be passed in now.

The following old code

```cs
var parser = new HtmlParser(Configuration.Default);
```

is therefore to be replaced with

```cs
IBrowsingContext context = BrowsingContext.New(Configuration.Default);
var parser = new HtmlParser(context);
```

but would be much better expressed as

```cs
IBrowsingContext context = BrowsingContext.New(Configuration.Default);
var parser = context.GetService<IHtmlParser>();
```

### CSS

The current version of AngleSharp split out the CSS parsing (except CSS selectors) in its own library. This library is called `AngleSharp.Css` and is available via NuGet.

The new library is much more feature rich than the old integration. Besides an improved object model (CSSOM and beyond) we included support for many things, e.g., CSS custom properties (also known as CSS variables), flexbox, and grid. The correctness tests of the used value conversions have been extended as well.

The basic usage is to configure AngleSharp using `WithCss`. Then, e.g., the style can be accessed by using `GetStyle` from `AngleSharp.Css.Dom`. Setting the style works now with the `SetStyle` extension method. This replaces the old `Style` property.

The `ICssStyleDeclaration` does not contain all known declarations as properties. Instead, extension methods are used to dynamically attach these getters and setters, e.g., `GetDisplay()` and `SetDisplay(value)`  instead of `Display { get; set; }`.

Therefore, the following old code won't work anymore:

```cs
((IHtmlElement)element).Style.Display = "flex";
```

Instead, we now have to use the AngleSharp.Css NuGet package, which should be used in the configuration like `Configuration.Default.WithCss()`. If all this is fulfilled the following extension method will work:

```cs
((IHtmlElement)element).Style.SetDisplay("flex");
```

In previous versions the `IWindow` also contained CSS methods for style computation. These are now also available in the new CSS library as extension methods. The `WindowExtensions` are contained in the namespace `AngleSharp.Dom`.

### Building Query Selectors

In AngleSharp v0.9 we can construct an `ISelector` directly like:

```cs
var parser = new CssParser();
ISelector selector = parser.ParseSelector("p > a");
```

Starting with AngleSharp v0.10 such direct access should be avoided. The `CssParser` is gone anyway and exists only in a reduced form within AngleSharp.Core (no CSS support), which implements the `ICssSelectorParser` interface.

The current way for accessing this functionality is via the service collection.

```cs
IConfiguration config = Configuration.Default;

// use the consuming (or a new) context
IBrowsingContext context = BrowsingContext.New(config);

// get the registered parser instance
ICssSelectorParser parser = context.GetService<ICssSelectorParser>();

// use as before
ISelector selector = parser.ParseSelector("foo");
```

Normally, a `BrowsingContext` instance already exists thus making the access much simpler.

### Scripting

:warn: Currently, `AngleSharp.Scripting.Js` is incompatible with AngleSharp v0.10.

We plan to deprecate this package and release `AngleSharp.Js` instead. In the meantime there is no replacement.

> Recommendation: Stay at AngleSharp pre-v0.10 for the moment and wait until AngleSharp.Js is released. Sorry for inconvenience!

### Namespaces

The current version of AngleSharp reordered how namespaces are used. While previous versions used a model like `AngleSharp.Dom.Html`, the new release uses, e.g., `AngleSharp.Html.Dom`.

The parsers have also moved. Formerly, you accessed the HTML parser via `AngleSharp.Parser.Html`. Now the access is done via `AngleSharp.Html.Parser`.

The `AngleSharp.Network` namespace has been removed. All IO related definitions can be found in `AngleSharp.Io` (same name as the NuGet package). Network related definitions are contained within in `AngleSharp.Io.Network`.

Furthermore, any core level text manipulation code can be found in `AngleSharp.Text`. Things that would be mainly seen as parts of a browser are now in `AngleSharp.Browser`.

### Extension Methods

The common namespace `AngleSharp.Extensions` is gone. Now extension methods are always contained in their respective bucket. As an example the node extensions like `GetAncestors` are now in `AngleSharp.Dom` (as they are generic `INode` extensions and independent of `IHtmlElement`).

Extension methods are now also considered important for script engines to bring extensibility to AngleSharp. Since C# does not allow us to create extension properties, e.g., `Style` of `IElement` is now available in form of an extension method contained in `AngleSharp.Css`. Indeed, two extension methods, `GetStyle` and `SetStyle` are defined. They are defined in a static class containing the `DomExposed` attribute for each DOM interface to extend. To define the methods as properties the `DomAccessor` attribute is used.

Interesting for working with text sources (e.g., in parsers) is the `AngleSharp.Text` namespace. It carries also the extensions for, e.g., working with a `StringSource`, which is a source investigation object wrapped around an existing stream (as opposed to a `TextSource`, which wraps around a text document from a `Stream`).

### Missing?

Don't hesitate to ask a question at [StackOverflow](https://stackoverflow.com/tags/anglesharp) or here at GitHub. If something important is left unclear regarding the migration it should be included in this guide.

You can also directly make a PR for this guide if you figured something out that should have been explained here. Thanks!
