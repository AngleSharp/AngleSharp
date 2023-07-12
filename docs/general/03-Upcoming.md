---
title: "Upcoming Features"
section: "AngleSharp.Core"
---
# Upcoming Features

The GitHub project management features are definitely nice, but they do not provide enough space for discussion and an in-depth presentation of the upcoming features. This list is not ordered in any way (importance, time-to-release, ...) and should only be regarded as a collection of ideas and reminders.

## CSSOM View Module

Rendering might be one of the most interesting aspects of AngleSharp. AngleSharp won't cover rendering directly. Instead, it will be easy to hook up a custom rendering engine. A (sample) library will probably be provided, like with the AngleSharp.Scripting one, which serves as an example for including a JavaScript engine. One of the cornerstones of rendering is including the CSSOM view module, which specifies a lot of methods, interfaces and more. This will be crucial when connecting to a real device, such as a screen. Also fake devices, which might be used for analysis (see `Window`), profit from the CSSOM view module.

Not all interfaces and methods have to be implemented / provided by the core AngleSharp library. Instead, one of the main tasks in this work package is to determine a good hierarchy, which provides both, flexibility and information, which can be used for many purposes.

*Status*: Partially included. Most of these things will be determined once rendering (ext. library) is scratched.

More information can be found at:

* [W3C CSSOM](http://dev.w3.org/csswg/cssom-view/)

## Rendering

Okay, so here it is. There will be rendering, however, not directly within AngleSharp. However, to see what is (still) definitely required from a core perspective, a reference implementation has to be provided. This implementation should follow the official standard described in the CSS 2.1 specification and modules that arrived later.

*Status*: Sketched, outlined, but nothing happened so far.

More information can be found at:

* [W3C CSS 2.1 specification](http://www.w3.org/TR/CSS21/)
* [HTML Renderer project](https://github.com/ArthurHub/HTML-Renderer)
* [MDN Visual Formatting](https://developer.mozilla.org/en-US/docs/Web/Guide/CSS/Visual_formatting_model)

## Define UI Handling

Most of this section is an extension to `IWindow`. We require a few points:

* UI specific tree traversal, keeping track of styling
* The event loop (right now placed in `Document`)
* ...

Most of these things will be more obvious once a sample renderer implementation will be done and released. This will happen after v0.7, probably in October or more likely in November.

The core source should be a browsing context. This, however, is a point on its own. A browsing context will be superior to `IWindow` instances, which are tightly bound to `IDocument` instances. A browsing context will also carry the device information, which is currently available at the `IWindow` level.

*Status*: Completely missing.

## Finish Style Resolution

On a simple level the style resolution is already finished. However, with pseudo-elements still missing, the `IWindow` being unfinished and other open questions, it is not possible to consider style resolution being solved.

In v0.7 there won't be any change (besides bugfixes and some minor feature updates) to the current status. First the CSS object model has to be refined / finished. Once the generated CSS tree is satisfying the style resolution will be on the list.

As a fact the style resolution is really important for (any) rendering project. Having style information is the most important thing for drawing the visual tree. In the end the visual tree is mostly based on the style information. The general rule is: Style + Text = presentation.

*Status*: Will be finished after the CSSOM is finished. This will likely happen for v1.0.

More information can be found at:

* [W3C DOM2 View](http://www.w3.org/TR/DOM-Level-2-Style/css.html#CSS-CSSview-getComputedStyle)
* [W3C CSSOM getComputedStyle](http://dev.w3.org/csswg/cssom/#dom-window-getcomputedstyle)
* [W3C CSSOM resolved values](http://dev.w3.org/csswg/cssom/#resolved-values)

## Provide Further Extension Points

For instance an automatic file system buffer, which (obviously) requires access to the file system. Additionally, storage might require access to the file system, but maybe in a different form. Also, media types such as video, audio or image will require access to video and audio streaming / playing, as well as image displaying.

It is highly likely that v0.7 will already introduce some media extensions, such as gathering image or audio information (from raw bytes), or accessing the storage for the offline API. This will also introduce the extended configuration pipeline.

The extended configuration pipeline will be the core during DOM interaction. While the parser is the core during source parsing, the configuration pipeline contains all related data.

Already available are, e.g.:

* Style parsers
* Scripting engines
* HTTP requester

Missing among others are:

* Mime to media type
* Resource database
* Local storage

The ultimate goal is to have a multitude of extension points, which can then be fully customized, extended and used. This way AngleSharp stays quite atomic (parsers, DOM) and provides the abilities to grow (beyond local). A headless browser that is fully customizable and extensible.

*Status*: Available, more ideas needed.

## MathML

MathML is a markup language that should allow writing equations like LaTeX. Therefore, MathML has special tags, which carry a semantic meaning, such as being an operator, an identifier, a symbol, space, raw text and much more. HTML5 allows embedding of MathML by specification.

Right now AngleSharp can handle MathML as foreign elements in HTML. This is the way it should be. There are no plans to support "Math documents" or similar constructs. However, right now AngleSharp is limited to only a few MathML elements. All other elements will be regarded as a general `MathElement` element, with a custom `NodeName`.

There are several reasons to include more information on the MathML (sub) DOM. Therefore, it is crucial to include all other specifically defined elements in AngleSharp. In the end the `MathFactory` has to be extended to also support these other elements. That should be sufficient to support MathML.

It has to be investigated if other properties, such as special entities, need to be included as well. If so, the question is: How to change the entity retrieval mode? There are several options, but the solution has to be robust, easy to extend, and well-performing.

*Status*: Basics included, shifted back to v1.0.

More information can be found at:

* [W3C MathML 3.0 TR](http://www.w3.org/TR/MathML3/)

## SVG

SVG is an XML based vector file format. HTML5 allows embedding SVG images in the document by specification. Some older browsers may require SVG images to be referenced (externally). AngleSharp supports HTML5 and therefore allows SVG to be included directly in the document.

Even though there are no plans for a "Math document", AngleSharp should consider having an "SVG document" in form of using the `IXmlDocument` interface (with the `XMLDocument` implementation). That way external SVG images can be parsed as well. It is yet unclear, if such documents will just use the HTML parser, or if the XML parser will be revived for this. If so it will be probably downgraded, to drop support for DTD and others. That way, the XML parser will be non-validating. This is probably the best solution.

There are lots of special elements and attributes, which need to be covered. In the end, however, this effort will be much less than implementing the HTML DOM.

*Status*: Basics included, shifted back to v1.0.

More information can be found at:

* [W3C SVG 1.1 TR](http://www.w3.org/TR/SVG11/)

## Slimming AngleSharp

AngleSharp is not really giant, but it is continuously growing. While the project will never drop its core consisting of

* HTML parser,
* CSS parser,
* (Core) IDL interfaces,
* (Core) DOM implementation,
* configuration and extensions,

Some contained abilities should be moved to other (extension) projects. Beginning with the completion of v0.8 a major evaluation will begin, to determine which parts are core / important and which are extensions. The main purpose of this evaluation will be:

* The keep AngleSharp small for small tasks
* To reduce the footprint
* Making AngleSharp even more extensible
* Providing extensions only if they are required

So if one includes AngleSharp just to parse a few lines of HTML, or a single HTML document, this task should be possible only with the core library. If one wants to make screenshots of webpages, emulate a full browsing context, or integrate JavaScript fully with AngleSharp (not just on top of a document), then the extensions are here to help.

In the end the idea is to give users only what they need, not more. If they want more, they can opt in by getting more advanced NuGet packages, which work as extensions for the core library.

*Status*: CSS already moved out. More to come until v1.0.
