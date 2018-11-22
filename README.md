![logo](https://raw.githubusercontent.com/AngleSharp/AngleSharp/master/header.png)

# AngleSharp

[![Build Status](https://img.shields.io/appveyor/ci/FlorianRappl/AngleSharp.svg?style=flat-square)](https://ci.appveyor.com/project/FlorianRappl/AngleSharp)
[![NuGet Count](https://img.shields.io/nuget/v/AngleSharp.svg?style=flat-square)](https://www.nuget.org/packages/AngleSharp/)
[![Issues Open](https://img.shields.io/github/issues/AngleSharp/AngleSharp.svg?style=flat-square)](https://github.com/AngleSharp/AngleSharp/issues)

AngleSharp is a .NET library that gives you the ability to parse angle bracket based hyper-texts like HTML, SVG, and MathML. XML without validation is also supported by the library. An important aspect of AngleSharp is that CSS can also be parsed. The included parser is built upon the official W3C specification. This produces a perfectly portable HTML5 DOM representation of the given source code and ensures compatibility with results in evergreen browsers. Also standard DOM features such as `querySelector` or `querySelectorAll` work for tree traversal.

## Key Features

- **Portable** (designed as a PCL - supporting .NET Standard 1.0)
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
// Setup the configuration to support document loading
var config = Configuration.Default.WithDefaultLoader();
// Load the names of all The Big Bang Theory episodes from Wikipedia
var address = "https://en.wikipedia.org/wiki/List_of_The_Big_Bang_Theory_episodes";
// Asynchronously get the document in a new context using the configuration
var document = await BrowsingContext.New(config).OpenAsync(address);
// This CSS selector gets the desired content
var cellSelector = "tr.vevent td:nth-child(3)";
// Perform the query to get all cells with the content
var cells = document.QuerySelectorAll(cellSelector);
// We are only interested in the text - select it with LINQ
var titles = cells.Select(m => m.TextContent);
```

## Supported Platforms

AngleSharp has been created as a PCL (profile 259) that supports a wide range of platforms. The list includes, but is not limited to:

- .NET Core ("netstandard 1.0", see [.NET Platform Standard](https://github.com/dotnet/corefx/blob/master/Documentation/architecture/net-platform-standard.md))
- .NET Framework 4.5
- Windows 8.1
- Windows Phone 8.1 / Windows Phone Silverlight
- Xamarin.Android
- Xamarin.iOS

Additionally, the NuGet package also comes with support for the following platforms:

- Silverlight 5
- .NET 4.0

Please note, however, that those platforms have dependencies (*Microsoft.Bcl.Async*), which are not needed by originally supported platforms.

Every collection in AngleSharp supports LINQ statements. AngleSharp also provides many useful extension methods for element collections that cannot be found in the official DOM.

## Documentation

Documentation is available in form of the public Wiki here at GitHub.

- [Wiki Home](https://github.com/AngleSharp/AngleSharp/wiki)
- [Documentation](https://github.com/AngleSharp/AngleSharp/wiki/Documentation)
- [API](https://github.com/AngleSharp/AngleSharp/wiki/Api)
- [Examples](https://github.com/AngleSharp/AngleSharp/wiki/Examples)
- [Performance](https://github.com/AngleSharp/AngleSharp/wiki/Performance)

The project's timeline, upcoming implementations, (currently) missing features, and milestones is published and maintained in the Wiki as well. Issues should be reported on the GitHub's project page.

More information is also available by following some of the hyper references mentioned in the Wiki. In-depth articles will be published on the CodeProject, with links being placed in the Wiki at GitHub.

## Vision

The project aims to bring a solid implementation of the W3C DOM for HTML, SVG, MathML, and CSS to the CLR, written in C#. The idea is that you can basically do everything with the DOM in C# that you can do in JavaScript.

Most parts of the DOM are included, even though some may still miss their (right) implementation. The goal for v1.0 is to have almost everything implemented according to the official W3C specification (with useful extensions by the WHATWG).

The API is close to the DOM4 specification, however, the naming has been adjusted to apply with .NET conventions. Nevertheless, to make AngleSharp really useful for, e.g., a JavaScript engine, attributes have been placed on the corresponding interfaces (and methods, properties, ...) to indicate the status of the field in the official specification. This allows automatic generation of DOM objects with the official API.

This is a long-term project which will eventually result in a state of the art parser for the most important angle bracket based hyper-texts (and related description languages like CSS).

## Roadmap

The roadmap presents a draft on what is about to be implemented, and when. The priorities might change, which will affect the roadmap. Additionally the implementation speed will be impacted by factors like people participating in the project and design decisions.

The time estimates are speculative, which means that the project could be totally off those predictions. Finding talented (and motivated) collaborators would certainly speed up the project.

(2018) **0.10.0**

- Split AngleSharp.Core into two libraries (Core, CSS)

(2019) **1.0.0**

- Release of the first stable version
- Provide internal / external communication channel (best flexibility)
- Service model finalized

The current schedule seems to be rather defensive, which does not mean the project will be "finished", i.e., released in version 1.0.0, before the given date. If there is time left, more unit tests will be written and the general code quality will be increased.

## Use-Cases

- Parsing HTML (incl. fragments)
- Parsing CSS (incl. selectors, declarations, ...)
- Constructing HTML (e.g., view-engine)
- Minifying CSS, HTML
- Querying document elements
- Crawling information
- Gathering statistics
- Web automation
- Tools with HTML / CSS support
- Connection to page analytics
- HTML / DOM Unit Tests
- Automated JavaScript interaction
- Testing other script engines
- ...

## Participating in the Project

If you know some feature that AngleSharp is currently missing, and you are willing to implement the feature, then your contribution is more than welcome! Also if you have a really cool idea - do not be shy, we'd like to hear it.

If you have an idea how to improve the API (or what is missing) then posts / messages are also welcome. For instance there have been ongoing discussions about some styles that have been used by AngleSharp (e.g., `HTMLDocument` or `HtmlDocument`) in the past. In the end AngleSharp stopped using `HTMLDocument` (at least visible outside of the library). Now AngleSharp uses names like `IDocument`, `IHtmlElement` and so on. This change would not have been possible without such fruitful discussions.

The project is always searching for additional contributors. Even if you do not have any code to contribute, but rather an idea for improvement, a bug report or a mistake in the documentation. These are the contributions that keep this project active.

More information is found in the [contribution guidelines](.github/CONTRIBUTING.md). We also have a [code of conduct](.github/CODE_OF_CONDUCT.md).

## License

The MIT License (MIT)

Copyright (c) 2013 - 2018 AngleSharp

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
