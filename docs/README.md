# AngleSharp Documentation

This documentation focuses on AngleSharp.Core, the standards-driven parser, DOM, selector engine, and browsing-context infrastructure that the other AngleSharp projects build on.

We have more detailed information regarding the following subjects:

- [Getting Started](general/01-Basics.md)
- [Current Features](general/02-Features.md)
- [Roadmap and Ecosystem](general/03-Upcoming.md)
- [Performance Comparisons](general/04-Performance.md)
- [API Documentation](tutorials/01-API.md)
- [Parser Options](tutorials/02-Options.md)
- [More Examples](tutorials/03-Examples.md)
- [Form Submission by Example](tutorials/04-Forms.md)
- [Migration Information](tutorials/05-Migration.md)
- [Frequently Asked Questions](tutorials/06-Questions.md)

## Why AngleSharp?

If you need some arguments for using AngleSharp (over similar libraries) then here we go:

- AngleSharp is totally standard-driven
- AngleSharp handles HTML, SVG, and MathML content in HTML documents and exposes CSS selector support in core
- AngleSharp stands the html5lib parser tests and aligns with browser behavior for malformed HTML
- AngleSharp handles malformed HTML exactly as every modern browser
- AngleSharp contains brand new elements like support for the `<template>` tag
- AngleSharp performs at least as good as other libraries, mostly better
- AngleSharp provides very useful extension methods for DOM manipulation
- AngleSharp gives you a decoupled API that is easy to use
- AngleSharp keeps the core surface small and exposes richer features through companion packages
- AngleSharp can be used in cross-platform solutions
- AngleSharp can validate and submit forms
- AngleSharp is very easy to extend and customize

There are tons of other arguments. With the provided configuration abilities, and the given extensibility, AngleSharp can be used in various scenarios.

## AngleSharp Ecosystem

AngleSharp.Core intentionally keeps the parser and DOM implementation focused. The wider ecosystem adds CSSOM, JavaScript integration, XML support, rendering, and other higher-level capabilities.

| Project | Purpose | Repository |
|---|---|---|
| AngleSharp.Css | Full CSS parser, CSSOM, and styling services | https://github.com/AngleSharp/AngleSharp.Css |
| AngleSharp.Js | JavaScript integration for browsing contexts | https://github.com/AngleSharp/AngleSharp.Js |
| AngleSharp.Wasm | WebAssembly-oriented integration work for AngleSharp | https://github.com/AngleSharp/AngleSharp.Wasm |
| AngleSharp.Xml | XML, XHTML, and related XML-oriented parsing support | https://github.com/AngleSharp/AngleSharp.Xml |
| AngleSharp.Renderer | Rendering-focused companion project | https://github.com/AngleSharp/AngleSharp.Renderer |
| AngleSharp.Diffing | DOM and markup diffing utilities | https://github.com/AngleSharp/AngleSharp.Diffing |
| AngleSharp.XPath | XPath support on top of the AngleSharp DOM | https://github.com/AngleSharp/AngleSharp.XPath |
