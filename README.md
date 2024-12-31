![logo](https://raw.githubusercontent.com/AngleSharp/AngleSharp/master/header.png)

# AngleSharp

[![CI](https://github.com/AngleSharp/AngleSharp/actions/workflows/ci.yml/badge.svg)](https://github.com/AngleSharp/AngleSharp/actions/workflows/ci.yml)
[![GitHub Tag](https://img.shields.io/github/tag/AngleSharp/AngleSharp.svg?style=flat-square)](https://github.com/AngleSharp/AngleSharp/releases)
[![NuGet Count](https://img.shields.io/nuget/dt/AngleSharp.svg?style=flat-square)](https://www.nuget.org/packages/AngleSharp/)
[![Issues Open](https://img.shields.io/github/issues/AngleSharp/AngleSharp.svg?style=flat-square)](https://github.com/AngleSharp/AngleSharp/issues)
[![Gitter Chat](http://img.shields.io/badge/gitter-AngleSharp/AngleSharp-blue.svg?style=flat-square)](https://gitter.im/AngleSharp/AngleSharp)
[![StackOverflow Questions](https://img.shields.io/stackexchange/stackoverflow/t/anglesharp.svg?style=flat-square)](https://stackoverflow.com/tags/anglesharp)
[![CLA Assistant](https://cla-assistant.io/readme/badge/AngleSharp/AngleSharp?style=flat-square)](https://cla-assistant.io/AngleSharp/AngleSharp)

AngleSharp is a .NET library that gives you the ability to parse angle bracket based hyper-texts like HTML, SVG, and MathML. XML without validation is also supported by the library. An important aspect of AngleSharp is that CSS can also be parsed. The included parser is built upon the official W3C specification. This produces a perfectly portable HTML5 DOM representation of the given source code and ensures compatibility with results in evergreen browsers. Also standard DOM features such as `querySelector` or `querySelectorAll` work for tree traversal.

:zap::zap: **Migrating from AngleSharp 0.9 to AngleSharp 0.10 or later** (incl. 1.0)? Look at our [migration documentation](docs/tutorials/05-Migration.md). :zap::zap:

## Key Features

- **Portable** (using .NET Standard 2.0)
- **Standards conform** (works exactly as evergreen browsers)
- **Great performance** (outperforms similar parsers in most scenarios)
- **Extensible** (extend with your own services)
- **Useful abstractions** (type helpers, jQuery like construction)
- **Fully functional DOM** (all the lists, iterators, and events you know)
- **Form submission** (easily log in everywhere)
- **Navigation** (a `BrowsingContext` is like a browser tab - control it from .NET!).
- **LINQ enhanced** (use LINQ with DOM elements, naturally without wrappers)

The advantage over similar libraries like *HtmlAgilityPack* is that the exposed DOM is using the official W3C specified API, i.e., that even things like `querySelectorAll` are available in AngleSharp. Also the parser uses the HTML 5.1 specification, which defines error handling and element correction. The AngleSharp library focuses on standards compliance, interactivity, and extensibility. It is therefore giving web developers working with C# all possibilities as they know from using the DOM in any modern browser.

The performance of AngleSharp is quite close to the performance of browsers. Even very large pages can be processed within milliseconds. AngleSharp tries to minimize memory allocations and reuses elements internally to avoid unnecessary object creation.

## Simple Demo

The simple example will use the website of Wikipedia for data retrieval.

```cs
var config = Configuration.Default.WithDefaultLoader();
var address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
var context = BrowsingContext.New(config);
var document = await context.OpenAsync(address);
var cellSelector = "tr.vevent td:nth-child(3)";
var cells = document.QuerySelectorAll(cellSelector);
var titles = cells.Select(m => m.TextContent);
```

Or the same with explicit types:

```cs
IConfiguration config = Configuration.Default.WithDefaultLoader();
string address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
IBrowsingContext context = BrowsingContext.New(config);
IDocument document = await context.OpenAsync(address);
string cellSelector = "tr.vevent td:nth-child(3)";
IHtmlCollection<IElement> cells = document.QuerySelectorAll(cellSelector);
IEnumerable<string> titles = cells.Select(m => m.TextContent);
```

In the example we see:

* How to setup the configuration for supporting document loading
* Asynchronously get the document in a new context using the configuration
* Performing a query to get all cells with the content of interest
* The whole DOM supports LINQ queries

Every collection in AngleSharp supports LINQ statements. AngleSharp also provides many useful extension methods for element collections that cannot be found in the official DOM.

## Supported Platforms

AngleSharp has been created as a .NET Standard 2.0 compatible library. This includes, but is not limited to:

- .NET Core (2.0 and later)
- .NET Framework (4.6.2 and later)
- Xamarin.Android (7.0 and 8.0)
- Xamarin.iOS (10.0 and 10.14)
- Xamarin.Mac (3.0 and 3.8)
- Mono (4.6 and 5.4)
- UWP (10.0 and 10.0.16299)
- Unity (2018.1)

## Documentation

The documentation of AngleSharp is located [in the docs folder](docs/README.md). More examples, best-practices, and general information can be found there. The documentation also contains a list of [frequently asked questions](docs/tutorials/06-Questions.md).

More information is also available by following some of the hyper references mentioned in the Wiki. In-depth articles will be published on the CodeProject, with links being placed in the Wiki at GitHub.

## Use-Cases

- Parsing HTML (incl. fragments)
- Parsing CSS (incl. selectors, declarations, ...)
- Constructing HTML (e.g., view-engine)
- Minifying CSS, HTML, ...
- Querying document elements
- Crawling information
- Gathering statistics
- Web automation
- Tools with HTML / CSS / ... support
- Connection to page analytics
- HTML / DOM unit tests
- Automated JavaScript interaction
- Testing other concepts, e.g., script engines
- ...

## Vision

The project aims to bring a solid implementation of the W3C DOM for HTML, SVG, MathML, and CSS to the CLR - all written in C#. The idea is that you can basically do everything with the DOM in C# that you can do in JavaScript (plus, of course, more).

Most parts of the DOM are included, even though some may still miss their (fully specified / correct) implementation. The goal for v1.0 is to have *all practically relevant* parts implemented according to the official W3C specification (with useful extensions by the WHATWG).

The API is close to the DOM4 specification, however, the naming has been adjusted to apply with .NET conventions. Nevertheless, to make AngleSharp really useful for, e.g., a JavaScript engine, attributes have been placed on the corresponding interfaces (and methods, properties, ...) to indicate the status of the field in the official specification. This allows automatic generation of DOM objects with the official API.

This is a long-term project which will eventually result in a state of the art parser for the most important angle bracket based hyper-texts.

Our hope is to build a community around web parsing and libraries from this project. So far we had great contributions, but that goal was not fully achieved. Want to help? Get in touch with us!

## Participating in the Project

If you know some feature that AngleSharp is currently missing, and you are willing to implement the feature, then your contribution is more than welcome! Also if you have a really cool idea - do not be shy, we'd like to hear it.

If you have an idea how to improve the API (or what is missing) then posts / messages are also welcome. For instance there have been ongoing discussions about some styles that have been used by AngleSharp (e.g., `HTMLDocument` or `HtmlDocument`) in the past. In the end AngleSharp stopped using `HTMLDocument` (at least visible outside of the library). Now AngleSharp uses names like `IDocument`, `IHtmlElement` and so on. This change would not have been possible without such fruitful discussions.

The project is always searching for additional contributors. Even if you do not have any code to contribute, but rather an idea for improvement, a bug report or a mistake in the documentation. These are the contributions that keep this project active.

Live discussions can take place in our [Gitter chat](https://gitter.im/AngleSharp/AngleSharp), which supports using GitHub accounts.

More information is found in the [contribution guidelines](.github/CONTRIBUTING.md). All contributors can be found [in the CONTRIBUTORS](CONTRIBUTORS.md) file.

This project has also adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.

For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## Funding / Support

If you use AngleSharp frequently, but you do not have the time to support the project by active participation you may still be interested to ensure that the AngleSharp projects keeps the lights on.

Therefore we created a backing model via [Bountysource](https://salt.bountysource.com/teams/anglesharp). Any donation is welcome and much appreciated. We will mostly spend the money on dedicated development time to improve AngleSharp where it needs to be improved, plus invest in the web utility eco-system in .NET (e.g., in JavaScript engines, other parsers, or a renderer for AngleSharp to mention some outstanding projects).

> Visit [Bountysource](https://salt.bountysource.com/teams/anglesharp) for more details.

## Development

AngleSharp is written in the most recent version of C# and thus requires Roslyn as a compiler. Using an IDE like Visual Studio 2019+ is recommended on Windows. Alternatively, VSCode (with OmniSharp or another suitable Language Server Protocol implementation) should be the tool of choice on other platforms.

The code tries to be as clean as possible. Notably the following rules are used:

- Use braces for any conditional / loop body
- Use the `-Async` suffixed methods when available
- Use VIP ("Var If Possible") style (in C++ called AAA: Almost Always Auto) to place types on the right

More important, however, is the proper usage of tests. Any new feature should come with a set of tests to cover the functionality and prevent regression.

## Changelog

A very detailed [changelog](CHANGELOG.md) exists. If you are just interested in major releases then have a look at [the GitHub releases](https://github.com/AngleSharp/AngleSharp/releases).

## .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).

## License

AngleSharp is released using the MIT license. For more information see the [license file](./LICENSE).
