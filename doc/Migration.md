# Migration Guide

## 0.9 to 0.10

The v0.10 release line of AngleSharp is breaking towards formerly used APIs. Even though the same concepts are mostly applied, many things changed and an upgrade from AngleSharp pre v0.10 to 0.10 will certainly break things. The following points should help you to perform the migration as fast as possible.

In the following points the v0.10 release line will be named "current", while older releases will be referred to as "previous".

### Silverlight / Pre .NET 4.5

These platforms are no longer support. No solution planned.

### Configuration

The way to configure AngleSharp was changed.

(tbd)

### CSS

The current version of AngleSharp split out the CSS parsing (except CSS selectors) in its own library. This library is called `AngleSharp.Css` and is available via NuGet.

The new library is much more feature rich than the old integration. Besides an improved object model (CSSOM and beyond) we included support for many things, e.g., CSS custom properties (also known as CSS variables), flexbox, and grid. The correctness tests of the used value conversions have been extended as well.

The basic usage is to configure AngleSharp using `WithCss`. Then, e.g., the style can be accessed by using `GetStyle` from `AngleSharp.Css.Dom`. Setting the style works now with the `SetStyle` extension method. This replaces the old `Style` property.

The `ICssStyleDeclaration` does not contain all known declarations as properties. Instead, extension methods are used to dynamically attach these getters and setters, e.g., `GetDisplay()` and `SetDisplay(value)`  instead of `Display { get; set; }`.

In previous versions the `IWindow` also contained CSS methods for style computation. These are now also available in the new CSS library as extension methods. The `WindowExtensions` are contained in the namespace `AngleSharp.Dom`.

(tbd)

### Namespaces

The current version of AngleSharp reordered how namespaces are used. While previous versions used a model like `AngleSharp.Dom.Html`, the new release uses, e.g., `AngleSharp.Html.Dom`.

The parsers have also moved. Formerly, you accessed the HTML parser via `AngleSharp.Parser.Html`. Now the access is done via `AngleSharp.Html.Parser`.

(tbd)

### Extension Methods

The common namespace `AngleSharp.Extensions` is gone. Now extension methods are always contained in their respective bucket. As an example the node extensions like `GetAncestors` are now in `AngleSharp.Dom` (as they are generic `INode` extensions and independent of `IHtmlElement`).

Extension methods are now also considered important for script engines to bring extensibility to AngleSharp. Since C# does not allow us to create extension properties, e.g., `Style` of `IElement` is now available in form of an extension method contained in `AngleSharp.Css`. Indeed, two extension methods, `GetStyle` and `SetStyle` are defined. They are defined in a static class containing the `DomExposed` attribute for each DOM interface to extend. To define the methods as properties the `DomAccessor` attribute is used.

(tbd)

### Missing?

(tbd)
