# Major Releases

A more detailed changelog can be found in the [CHANGELOG](../CHANGELOG.md).

**0.10.0**

- `HttpRequester` improvements
- Make configuration re-usable and easier to work with
- Extracted major part of CSS to `AngleSharp.Css`
- Changed license to MIT
- Enhanced form submission (e.g., added JSON type)
- Added more factories (e.g., link relation factory, document factory)
- Allow usage of HTML imports and more web component parts
- Included sub-resource integrity
- Various performance enhancements
- Improved document unloading
- Default cookie management improvements
- Added custom mime-type handling
- Improvements to CSS selector evaluation and usage (e.g., custom extensions, `ISelectorVisitor`)
- Enhanced the URL parsing
- Changed target to .NET Standard 2.0 (and enable build on Linux)
- Updated to adhere to HTML 5.2 where noted

**0.9.0**

- Improved DOM algorithms and performance
- Shadow DOM draft implemented
- The `picture` element is now support (with `srcset`)
- More neat helpers
- Custom `MimeType`
- `DocumentBuilder` removed
- AngleSharp events aggregated in `IEventAggregator`
- Non-validating XML parser reintegrated
- CSSOM improved (also allows round-trip)
- Included default cookie service
- Deployed with strong name
- Improved parser front-ends (`HtmlParser`, `CssParser`, ...)

**0.8.0**

- New CSS value model integrated
- PseudoElement available
- Mutation records connected
- Encoding basically finished
- Memory leak fixed
- All CSS4 selectors (excluding `||`) included
- Finished `Url` implementation
- HTML5 form validation
- Media features and CSS properties extended
- Namespace naming fix
- All HTML5 input types are supported

**0.7.0**

- Native (callback based) async parsing
- Interfaces for resource loading defined
- Browsing context available / creation possible (if demanded)
- Event model included (`addEventListener`, ...)
- CSS property / value architecture finalized
- Sample JavaScript engine based on Jint included

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
