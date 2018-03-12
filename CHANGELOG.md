# 0.9.9.2

- Provided `Tokenize` extension method for `TextSource` (#636)
- Replaced Conditional Weak Table for performance gains (#637)
- Fixed a bug for non-unicode characters to stop parsing (#590)
- Fixed weird `iframe` behavior to self-load (#581)

# 0.9.9.1

- Fixed build (#566)
- Several bugfixes (#587, see: #491, #406, #512, and #544)
- Memory improvement (#588)
- Corrected casing (#589)
- Fixed wrong default for Url ports (#624)
- Fixed behavior on empty string selectors (#601)
- Fixed multiple slashes stackoverflow (#613)
- Fixed parsing with initial comment (#610)
- Fixed cookie time format handling (#599, #598)
- Fixed stackoverflow when parsing (#570)
- Initialize default `Request` content (#567)

# 0.9.9

- Attribute API refinements (#422)
- Cookies are now sent for all requests (#420)
- Fixed DOM attribute name culture bug (#419)
- Added CSS `word-wrap` (`overflow-wrap`) and `word-breakword` properties (#417)
- Added CSS `text-align-last`, `text-anchor`, and `text-justify` properties (#413)
- Added CSS `stroke-*` properties (#407, #409, #411)
- Handle compression in HTTP responses (#416)
- Included `StatusCode` property in `IDocument` (#408)
- Improved default event loop (#404)
- Fixed invalid entity errors in XML (#401)
- Added HTML `mark` element (#399)

# 0.9.8.1

- Added parsing callback (e.g., to retrieve line number) (#374)
- Exposed the original `Source` in documents (#396)
- Fixed a crash during redirects for requesters (#394)

# 0.9.8

- Improved the `HttpRequester` (#387)
- Extended the `CookieContainer` (#385)
- Bug fixes for `ToCss()` (#382)
- Added custom mime-type handling (#381)
- Transport cookie on page redirect (#368)
- Submit from Button (#354)
- Implemented document unloading (#339)
- Possibility to use strict mode (#336)
- Included subresource integrity (#308)
- `IHtmlCollection` helpers and API improvement (#293)
- General performance improvements (#390)

# 0.9.7

- Fixed some bugs (#343, #325, #341, #347, #355, #358)
- Improved cookie handling (#280, #274, #365)
- Added a document factory (#331)
- `EventNames`, `AttributeNames` and others are available (#330)
- Allow setting the active document (#281)
- Improved Xamarin.iOS build (#85)
- Changed service API slightly (#157)
- Enhanced CoreCLR support (#270, #362)

# 0.9.6

- Fixed some bugs (#304, #295, #286)
- Provide XHTML markup formatter (#128, #313)
- Dropped the `IEventAggregator` (#156)
- Allow custom selector factories (#182, #233)
- Open `TagNames` (#252)
- Internal cosmetics and improvements (#279, #300)
- Execute dynamically added scripts (#287)
- Property declarations of CSS rules can be modified (#297)
- Allow saving HTML DOM to a stream using `ToHtml` (#249)
- React to attributes containing event handlers (#190)

# 0.9.5

- Fixed some bugs (#282, #273, #266, #260, #256, #250, #243, #234, #230, #229, #223, #208)
- Added missing `bottom` CSS property (#253)
- Invalid indices throw now exceptions (#232)
- Provide new `dotnet` target (#271)
- Changed behavior of `OpenAsync` to wait for resources (#158)
- Added a set of extension methods for index-related selectors (#183)
- Redesigned the request pipeline (#189)
- Added extension method for submitting forms (#218)
- `Style` is now available at `IElement` level (#193)
- Enhanced to distinguish between the stylesheet types (#191)
- Included extension methods to improve CSSOM modifications (#205)

# 0.9.4

- Added the link relation factory (#174)
- Fixed a bug related to `document.write` (#173)
- Delay document loading for resources (#178)
- Improved HTTP requester performance (#194)
- Added HTML imports (#179)
- Include CSS color enhancements (#176)
- Enhanced encoding with multibyte characters (#210, #212)
- Use common `CssNode` as root (#145)
- Added `Attr` extension method (#199)

# 0.9.3

- Important bugfixes (#160, #161, #162, #165, #170)
- XML parser enhancements
- Ability to provide custom entities
- Fixed `CompareDocumentPosition` (#168)

# 0.9.2

- Some bugfixes (#150)
- XML parser enhancement (#144)
- JSON form submission (#126)
- Changed license to MIT
- Provide flex hex parsing (`Color`)

# 0.9.1

- Content of `iframe` can be set
- Default `IEventLoop` provided
- Improved the `HttpRequester`
- Fixed obtaining ext. stylesheets without CSS
- Added option to filter requests
- Parse CSS unicode escapes

# 0.9.0

- Implemented `srcset` attribute
- Implemented `picture` element
- Made Shadow DOM API spec. draft available
- Custom `MimeType` datatype
- Supports CSS round-tripping
- Assembly is now strongly signed
- Provide standard `IEventAggregator` implementation

# 0.8.9

- Improved resource fetching
- Fixed waiting mechanism
- Fixed form submission (avoid initial empty line)
- HTML Parser perf. improved
- Expose `INamedNodeMap` interface
- Fixed problems with `@import`
- Added the `@viewport` CSS rule

# 0.8.8

- Fixed a bug in the `HtmlDomBuilder`
- Adjusted CSSOM for tolerating unknown rules
- Parser enhancements

# 0.8.7

- CSS parser more flexible
- Allow inline styles to be customized

# 0.8.6

- Owner's are now weakly referenced
- The CSS parser supports unknown parsing
- Fixed several bugs in the CSS parser
- Fixed some smaller issues
- The `AngleSharp.Linq` namespace is now `AngleSharp.Extensions`
- Improved decoding algorithms
- `PrettyStyleFormatter` for readable CSS output
- `IStyleFormatter` with default `CssStyleFormatter` implementation
- `DocumentRequest` static helpers (for `GET` and `POST`)
- Default `ICookieService` implementation offered

# 0.8.5

- Fixed CSS property parsing
- Implemented new CSS value converters
- Fixed some cookie issues
- Made `Configuration.Default` available
- Changed some namespace assignments (e.g., `ScriptOptions`)
- Fixed `HtmlLinkElement` issue
- Fixed CSS twisted comment issue

# 0.8.4

- Added ability to wait for outstanding requests
- Fixed missing dashes in hostnames
- Changed CSS parser / tokenizer interaction
- Reworked CSS value model
- Extended `IMarkupFormatter` to serialize attributes
- Included encoding service
- Fixed `BrowsingContext` content from string loading
- Improved HTML parser performance
- Allow unknown properties in the CSSOM

# 0.8.3

- `PrettyMarkupFormatter` for readable output
- Add some missing `ConfigureAwait(false)` calls
- Included virtual response callback for the context
- Add `IHtmlDocument` for completeness
- Reintegrated `XmlParser` (only non-validating)
- Changed `Configuration` to be immutable
- All `IConfiguration` extensions return new object
- Fixed smaller issues

# 0.8.2

- `DocumentBuilder` declared obsolete
- Fixed bugs in DOM methods
- Added `ToHtml()` overloads with custom formatters
- Changed CSS value model
- Fixed BOM in form submits
- Changed case of tags to mimick browsers
- Improved url encoded form submission
- Improved CSS shorthand properties
- Fixed `Origin` of `Url`
- Improved loading customization
- Included `IEventAggregator` for events
- Fixed several smaller bugs

# 0.8.1

- `IsInvalid` of `Url` corrected
- Included .NET 4 version in the NuGet package
- Included Silverlight version in the NuGet package
- Fixed a few smaller bugs

# 0.8.0

- Major encoding improvements
- DOM ranges are (weakly) connected and updated
- Mutation callbacks are now included
- Implemented parsing keyframe selectors
- CSS document (rule) functions are checked
- CSS value model finished
- `HTMLObjectElement` can be serviced
- All CSS4 selectors included (excluding `||`)
- Small pseudo element integration
- More services e.g. spellcheck available
- Media features and CSS properties extended
- HTML5 constraint form validation finished
- All HTML5 input types are supported
- Changed `DOM` namespace to `Dom` to fix naming
- Finished `Url` implementation according to spec.

# 0.7.0

- Native (callback based) async parsing
- Interfaces for resource loading defined
- Browsing context available / creation possible
- Extension methods to `IConfiguration` available
- More attributes added
- Namespace changes for the attributes
- CSS property architecture finalized

# 0.6.1

- Minor bug fixes
- DOM Events
- Configuration improved
- Performance improvements
- Url origin
- Core algorithms changed [WHATWG]
- Scripting and styling interfaces

# 0.6.0

- Refactored DOM model
- Window implementation
- Compute style information
- More documentation
- Tree traversal included
- Refactored DOM attributes
- Script engine interface
- Style engine interface
- Removed IOC container
- New source code reader
- Unified Url class

# 0.5.1

- Finished CSS properties
- CSS parser update
- Media rules implemented
- Rewritten URL parsing
- More documentation
- Extended DocumentBuilder

# 0.5.0

- Support for Legacy version of AngleSharp
- Updated styling
- Removed XML parser
- API renewal
- Dependency Injection update
- New CSS value model
- More DOM properties
- CSS properties added
- Finished form submission

# 0.4.0

- DTD parsing improved
- Rewritten CSS parser
- More methods and DOM completeness
- XML validation

# 0.3.7

- Namespaces fully included
- Doctype improved
- DTD parsing finished
- Cleanup
- CSS rules extended
- CSS functions added

# 0.3.6

- Performance improvements
- Fixed CSS selector
- More documentation
- Live collection
- Started form submission
- Fixed double-escaped script tags
- Improved parsing of inline styling

# 0.3.5

- Finished template implementation
- MathML and SVG elements
- More annotations
- RelList established
- Improved TextNode

# 0.3.4

- More DOM properties
- Hyper reference normalization
- Introduced template element
- Polish API

# 0.3.3

- Fixed double escaped script content
- DOM extensions
- Configuration object
- Default HTTP requester

# 0.3.2

- Fixed parser bugs
- Improved entity retrieval
- Extended CDATA parsing
- Improved fragment parsing

# 0.3.1

- Improved character parsing
- Cleanup
- Line skipping for textarea, pre
- Extended Location

# 0.3.0

- Included XmlParser
- DTD parsing
- Added IOC container
- Finished element type declaration

# 0.2.9

- Fixed GetElementsByName
- Improved GetElementsByTagName
- Converted to PCL project
- Updated Children property

# 0.2.8

- Binding capabilities
- CSS parser update
- More decorations
- Integrated table, frame
- Added pooling

# 0.2.7

- Updated CSS serialization
- Intermediate objects
- More DOM attributes

# 0.2.6

- Improved API
- QuerySelector implementation
- Fixed DocumentUri bug

# 0.2.5

- HTML element re-ordering
- Extended documentation
- HtmlColor structure

# 0.2.4

- Faster CSS parser
- Initial CSS parser release

# 0.2.3

- StyleSheet integration
- Ignore unknown CSS rules

# 0.2.2

- Updated documentation
- Fixed parsing bug

# 0.2.1

- XML documentation
- Merged DocumentBuilder and NodeBuilder

# 0.2.0

- Initial release