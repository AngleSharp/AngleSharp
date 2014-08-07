AngleSharp
==========

AngleSharp is a .NET library that gives you the ability to parse angle bracket based hyper-texts like HTML, SVG, and MathML. In the future XML might be included, however, since .NET offers a variety of capable XML parsers there is a discussion about redundancy. An important aspect of AngleSharp is that CSS can also be parsed. The parser is build upon the official W3C specification. This produces a perfectly portable HTML5 DOM representation of the given source code. Also current features such as `querySelector` or `querySelectorAll` work for tree traversal.

The advantage over similar libraries like the HtmlAgilityPack is that e.g. CSS (including selectors) is already build in. Also the parser uses the HTML 5.1 specification, which defines error handling and element correction. While the HtmlAgilityPack focuses on giving .NET users a nice and easy way to handle HTML documents, this library focuses on giving web developers working with C# all possibilities as they would have in a browser using JavaScript. Hence the DOM is build in a more reliable, standard-conform and faster way than with the other solutions.

The performance of AngleSharp is quite close to the performance of browsers. Even very large pages can be processed within milliseconds. AngleSharp tries to minimize memory allocations and reuses elements internally to avoid unnecessary object creation.

Documentation
-------------

Documentation is available in form of the public Wiki here at GitHub. 
* [Wiki Home](https://github.com/FlorianRappl/AngleSharp/wiki)
* [Documentation](https://github.com/FlorianRappl/AngleSharp/wiki/Documentation)
* [API](https://github.com/FlorianRappl/AngleSharp/wiki/Api)
* [Examples](https://github.com/FlorianRappl/AngleSharp/wiki/Examples)
* [Performance](https://github.com/FlorianRappl/AngleSharp/wiki/Performance)

More information is also available by following some of the hyper references mentioned in the Wiki. In-depth articles will be published on the CodeProject, with links being placed in the Wiki at GitHub.

Current status
--------------

The project aims to bring a solid implementation of the W3C DOM for HTML, SVG, MathML and CSS to the CLR, written in C#. The idea is that you can can basically do everything with the DOM in C# that you can do in JavaScript.

Most parts of the DOM are included, even though some may still miss their (right) implementation. The goal for v1.0 is to have almost everything implemented according to the official W3C specification (with useful extensions by the WHATWG).

The API is close to the DOM4 specification, however, the naming has been adjusted to apply with .NET conventions. Nevertheless, to make AngleSharp really useful for, e.g., a JavaScript engine, attributes have been placed on the corresponding interfaces (and methods, properties, ...) to indicate the status of the field in the official specification. This allows automatic generation of DOM objects with the official API.

This is a long-term project which will eventually result in a state of the art parser for the most important angle bracket based hyper-texts (and related description languages like CSS).

**Update** With version v0.6.0 the new API has been published. This API seems to be much cleaner and more extensible than the previous one. It also separates the implementation from the specification, which is important for working with the API, as new changes are less likely to break existing code (API usage). Most API changes have also been included to mainly reflect DOM4, with some (obsolete) parts being removed.

Change log
----------

**0.6.0:**
- Implemented parsing of CSS media queries
- Improved URL parsing according to RFC 3986
- 100% finished HTML5 parser
- 98% finished CSS3 parser
- CSS properties and values defined and implemented
- CSS model implemented (i.e. *getComputedStyle* works)
- Tree traversal included (`NodeIterator` and `TreeWalker`)
- Configuration model changed
- API changed (now interface driven)
- New source management for better handling and performance

**0.5.0:**
- Major API changes (DI is now the only singleton)
- 98% finished HTML5 parser
- 95% finished CSS3 parser
- 85% finished HTML DOM
- Included `Submit()` method for forms

**0.4.0:**
- Final alpha version
- 98% finished HTML5 parser
- 90% finished CSS3 parser
- 85% finished HTML DOM
- Removed XML parser (until HTML and CSS are finished)
- Included WebRequester

**0.3.0:**
- Alpha version
- 95% finished HTML5 parser
- 90% finished CSS3 parser
- 85% finished HTML DOM
- Includes non-validating XML parser
- QuerySelectors etc. are fully working
- DOMAttribute applied where possible

**0.2.0:**
- First released version (pre-alpha)
- 95% finished HTML5 parser
- 70% finished CSS3 parser
- 80% finished HTML DOM
- SVG and MathML DOM are not implemented yet
- Performance seems to be quite OK

Roadmap
-------

The roadmap presents a draft on what is about to be implemented, and when. The priorities might change, which will affect the roadmap. Additionally the implementation speed will be impacted by factors like people participating in the project and design decisions.

The time estimates are speculative, which means that the project could be totally off those predictions. Finding talented (and motivated) collaborators would certainly speed up the project.

(September 2014) **0.7.0**
- Native (callback based) async parsing
- Interfaces for resource loading defined
- Browsing context available / creation possible (if demanded)
- Event model included (`addEventListener`, ...)
- CSS property / value architecture finalized
- Sample JavaScript engine based on Jint included

(December 2014) **0.8.0**
- Interface for rendering defined
- Full HTML DOM implemented
- CSS computation works with everything
- PseudoElement available
- Mutation records connected
- MathML support improved

(March 2015) **0.9.0**
- (Simple?) XPath query support
- MathML DOM finished
- SVG document included
- SVG DOM skeleton implemented
- Most important SVG elements implemented

(July 2015) **1.0.0**
- Final release of the first version
- MathML and SVG finalized (for HTML)
- HTML5 parser at 100% with complete DOM

The current schedule seems to be rather defensive, which does not mean the project will be "finished", i.e. released in version 1.0.0, before the given date. If there is time left, more unit tests will be written and the general code quality will be increased.

Use-cases
---------

- Parsing HTML (incl. fragments)
- Parsing CSS (incl. selectors, declarations, ...)
- Constructing HTML (e.g., view-engine)
- Minifying CSS, HTML
- Querying document elements
- Crawling information
- Gathering statistics
- Connection to page analytics
- HTML / DOM Unit Tests
- Automated JavaScript interaction
- Testing other script engines
- ...

Participating in the project
----------------------------

If you know some feature that AngleSharp is currently missing, and you are willing to implement the feature, then your contribution is more than welcome! Also if you have a really cool idea - do not be shy, we'd like to hear it.

If you have an idea how to improve the API (or what is missing) then posts / messages are also welcome. For instance there have been ongoing discussions about some styles that are used by AngleSharp (e.g. `HTMLDocument` instead of `HtmlDocument`). In the end AngleSharp stopped using `HTMLDocument` (at least visible outside of the library). Now AngleSharp uses names like `IHtmlDocument`. This change would not have been possible without fruitful discussions.

The project is always searching for additional contributors. Even if you do not have any code to contribute, but rather an idea for improvement, a bug report or a mistake in the documentation. These are the contributions that keep this project active. 

Some legal stuff
----------------

Copyright (c) 2013-2014, Florian Rappl and collaborators.
All rights reserved.

Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:

*	Redistributions of source code must retain the above copyright 	notice, this list of conditions and the following disclaimer.

*	Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.

*	Neither the name of the AngleSharp team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL FLORIAN RAPPL BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.