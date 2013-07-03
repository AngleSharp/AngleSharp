AngleSharp
==========

AngleSharp is a .NET library that gives you the ability to parse angle bracket based
hyper-texts like HTML, SVG, MathML and XML. CSS can also be parsed with this library.
The parser is build upon the official W3C specification. This produces a perfectly
portable HTML5 DOM representation of the given source code.

The advantage over similar libraries like the HtmlAgilityPack is that CSS (including
selectors) is already build in. Also the parser uses the HTML 5.1 specification, which
defines error handling and element correction. While the HtmlAgilityPack focuses on
giving .NET users a nice and easy way to handle HTML documents, this library focuses
on giving web developers working with C# all possibilities as they would have in a
browser using JavaScript.

Current status
--------------

The project aims to bring a solid implementation of the W3C DOM for HTML, SVG, MathML,
XML and CSS to the CLR, written in C#. The idea is that you can can basically do
everything with the DOM in C# that you can do in JavaScript.

The naming convention has been changing from camelCase to PascalCase (upper camel case).
This is intentional to fit the .NET naming coventions. Nevertheless as a long-term plan
all W3C defined IDL properties and methods will be decorated with attributes that contain
the original name, which will highly simplify using the library in combination with, e.g.
V8 and JavaScript files that can then also be used in a browser.

This is a long-term project which will eventually result in a state of the art parser for
the most important angle bracket based hyper-texts (and related description languages like
CSS).

Change log
----------

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

The time estimates are all ultra-conservative, which means that the project will probably (and hopefully) be much faster than the predicted schedule.

(July 2013) **0.3.0:**
- Alpha version including functional XML parser (no requirement for validating)
- DOM skeleton implemented

(September 2013) **0.4.0:**
- Validating XML parser implemented
- SVG and MathML DOM started
- Useful helpers like URL management implemented

(October 2013) **0.5.0**
- CSS model implemented (e.g. *getComputedStyle* works)
- Draft interfaces for optional resource and rendering defined

(December 2013) **0.6.0**
- MathML DOM finished
- Most important parts of HTML DOM implemented

(March 2014) **0.7.0**
- SVG document included
- SVG DOM skeleton implemented

(May 2014) **0.8.0**
- Full HTML DOM implemented
- CSS computation works with everything

(July 2014) **0.9.0**
- Most important SVG elements implemented
- HTML5 parser at 100% with complete DOM, MathML and SVG

(September 2014) **1.0.0**
- Final release of the first version

Use-cases
---------

- Parsing HTML (fragments)
- Constructing HTML (e.g. view-engine)
- Minifying CSS, HTML
- Querying document elements
- Crawling information
- ...

Participating in the project
----------------------------

If you know some feature that AngleSharp is currently missing, and you are willing to
implement the feature, then your contribution is more than welcome! Also if you have
a really cool idea - do not be shy, we'd like to hear it.

Some legal stuff
----------------

Copyright (c) 2013, Florian Rappl and collaborators.
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met:

*	Redistributions of source code must retain the above copyright
	notice, this list of conditions and the following disclaimer.

*	Redistributions in binary form must reproduce the above copyright
	notice, this list of conditions and the following disclaimer in the
	documentation and/or other materials provided with the distribution.

*	Neither the name of the AngleSharp team nor the names of its contributors
	may be used to endorse or promote products derived from this
	software without specific prior written permission.

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.