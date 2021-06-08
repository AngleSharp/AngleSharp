# 0.16.0

Released on Tuesday, June 8 2021.

- Moved `Url` from `AngleSharp` to `AngleSharp.Dom`
- Remove usage of CWT and introduce context info bag (#918)
- Fixed recursion depth of `FindDescendant` with configurable limit (#936)
- Fixed `IAttr` to actually inherit from `INode`
- Added ability to parse only the head from a stream (#966)
- Added new `UrlSearchParams` class (#970)
- Exposed `Url` class to `URL` DOM API (#970)

# 0.15.0

Released on Thursday, April 22 2021.

- Added `ToHtmlAsync()` extension method (#863)
- Added `ParseFragment` overload using `Stream` (#896)
- Added `euc-kr` encoding (#928)
- Fixed CSS attribute value comparison w.r.t. case sensitivity (#864)
- Fixed issue in the Heisenberg algorithm of the HTML5 parser (#893)
- Fixed issue with the strictness of broken char references in attribute values (#902)
- Fixed possible NRE in the markup formatters / attribute serialization (#903)
- Fixed `GetSelector` returning invalid ID selector (#909)
- Fixed `GetSelector` returning duplicate element ids (#910)
- Fixed possible NRE in media and `embed` elements without `src` (#914)
- Updated CI/CD system to GitHub Actions (#942)
- Dropped support for the .NET Standard 1.3 target (#944)

# 0.14.0

Released on Tuesday, March 31 2020.

- Included context event `error` for error tracking (#698)
- Extended the `IMarkupFormatter` for literal text (#821)
- Extended the `IElementFactory` definition
- Fixed an issue regarding n-th child for the `GetSelector` utility (#835)
- Added explicit support for .NET 4.6.1 (#842)
- Added public constructor to `BrowsingContext` (#844)
- Force `IBrowsingContext` to be `IDisposable` for cleanup purposes
- Fixed missing `Media` property of stylesheets (#846)
- Improved API of `IMarkupFormatter` (#858)
- Enhanced existing `IMarkupFormatter` instances to allow inheritance
- Added default accepts header for standard document requests (#859)

# 0.13.0

Released on Friday, September 6 2019.

- Removed the `TaskEventLoop` (#782)
- Renamed `WithCookies` to `WithDefaultCookies`
- Fixed bubbling of `DOMContentLoaded` event (#789)
- Fixed maximum recursion depth at query selector (#763)
- Added `MinifyMarkupFormatter` (#745)
- Added `Prettify` and `Minify` extension methods
- Moved `hashchange` to be emitted on `IWindow`
- Added option to avoid consuming character references (#494)
- Added more pseudo elements for GCPM
- Fixed character position starting at normalized CRLF (#786)
- Improved setting `Href` in `Url`
- Fixed crash for invalid attribute names during SVG parsing (#795)
- Added more punycode replacement characters on .NET Standard 1.3 (#797)
- Added use of correct hostname IDN on .NET Standard 2.0 and .NET Framework 4.6 (#797)
- Added ability to delay load in `Document` (#815)

# 0.12.1

Released on Wednesday, May 15 2019.

- Binary version fix
- Updated documentation regarding AngleSharp.Js

# 0.12.0

Released on Thursday, May 2 2019.

- Added `GetExtension` helper to `MimeTypeNames`
- Improved extension capability for document fragment parsing
- Added `Index` and more documentation to `TextPosition` (#787)
- Added ability to create a selector for an element (#784)
- Enhanced documentation (#776, #774, #771)
- Fixed DOM name attribute in `AdjacentPosition` (#775)
- Fixed bug regarding `set-cookie` header (#768)
- Added attribute start position in token (#766)
- Added support for XML processing instruction (#761)
- Fixed serialization of xmlns attributes (#760)
- Added `ISourceReference` for source position retrieval (#754)

# 0.11.0

Released on Monday, February 11 2019.

- Moved everything from `AngleSharp.Xml` to its own library (#139)
- Added more examples regarding forms (#242)
- Moved `ISvgDocument` and `AutoSelectedMarkupFormatter` to AngleSharp.Xml
- Improved fragment parsing (#594)
- Fixed inconsistent behavior in fragment parsing (#741)
- Improved migration documentation (#743)
- Fixed internal bug in `link` resolution (#753)
- Removed `SetDefault` function from `Configuration`

# 0.10.1

Released on Monday, January 7 2019.

- Added .NET Standard 1.3 target (#738)
- Fixed missing reference to `System.Encoding.CodePages` (#740)
- Fixed invalid context of `OuterHtml` parsing (#741)

# 0.10.0

Released on Friday, January 4 2019.

- Removed CSS parser from AngleSharp.Core (#139)
- Improvements to CSS selector evaluation (#352, #550)
- Fixed bug in `location.assign` (#496)
- Introduced the concept of scope to selectors (#440)
- Improved the `IEntityProvider` (#442)
- Fixed parsing the Cookie header (#431)
- Some fixes regarding attributes (#434)
- Pseudo-class `:has()` improvements (#439)
- Allow underscores in URLs (#445)
- Improved XML namespace handling (#448)
- Fixed the insert method (#449)
- Restructured services and configuration (#454)
- Fixed trailing comments bug (#459)
- Make AngleSharp build on Linux (#460)
- Corrected "specifity" (#463)
- Optimized node iteration allocations (#476)
- Improved cookie handling for path-dependent cookies (#477)
- Optimized tag name allocations (#479)
- Micro-Optimized `CharacterData.Append()` (#481)
- Fixed culture-specific number parsing (#482)
- Fast resource dictionary lookup (#485)
- Introduced the `ISelectorVisitor` to get selector information (#487)
- Fixed multi-threading element initialization bug (#489)
- Extended the attribute changed callback with `IAttributeObserver` (#491)
- Fixed computation of `href` (#501)
- General cookie handling improvements (#519, #548, #607, #702)
- Solved potential encoding issues in .NET Core (#534)
- Improved parsing of invalid HTML tags (#543)
- Improved the default requester (#572)
- Updated parts to adhere to HTML 5.2 (#618)
- Added support for parsing `noframes` (#631)
- Fixed build on Visual Studio 2017 (#679)
- Updated to new csproj format (#713)
- Fixed parser mode selection w.r.t. templates (#735)
- Placed extensions close to their instances
- Exposed the `ILinkRelationFactory` interface and default implementation
- Exposed the `IInputTypeFactory` interface and default implementation
- Reordered events for requesters and parsers

# 0.9.11.0

Released on Friday, November 23 2018.

- Prevent entity overflow (#716)
- Fixed bug in URL parser (#711)
- Include setup to configure default `HttpWebRequest` (#700)

# 0.9.10.0

Released on Sunday, July 15 2018.

- `ColSpan` default should be 1 (#689)
- `RowSpan` default should be 1 (#688)
- Expose image source set via `SourceSet.Parse` (#682)
- Implemented case insensitive attribute selector (#666)
- Fixed invalid date in `MemoryCookieProvider` (#663)

# 0.9.9.2

Released on Tuesday, March 13 2018.

- Provided `Tokenize` extension method for `TextSource` (#636)
- Replaced Conditional Weak Table for performance gains (#637)
- Fixed a bug for non-Unicode characters to stop parsing (#590)
- Fixed weird `iframe` behavior to self-load (#581)

# 0.9.9.1

Released on Wednesday, January 3 2017.

- Fixed build (#566)
- Several bugfixes (#587, see: #491, #406, #512, and #544)
- Memory improvement (#588)
- Corrected casing (#589)
- Fixed wrong default for `Url` ports (#624)
- Fixed behavior on empty string selectors (#601)
- Fixed multiple slashes stack overflow (#613)
- Fixed parsing with initial comment (#610)
- Fixed cookie time format handling (#599, #598)
- Fixed stack overflow when parsing (#570)
- Initialize default `Request` content (#567)

# 0.9.9.0

Released on Friday, October 7 2016.

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

Released on Saturday, September 10 2016.

- Added parsing callback (e.g., to retrieve line number) (#374)
- Exposed the original `Source` in documents (#396)
- Fixed a crash during redirects for requesters (#394)

# 0.9.8.0

Released on Saturday, September 3 2016.

- Improved the `HttpRequester` (#387)
- Extended the `CookieContainer` (#385)
- Bug fixes for `ToCss()` (#382)
- Added custom mime-type handling (#381)
- Transport cookie on page redirect (#368)
- Submit from Button (#354)
- Implemented document unloading (#339)
- Possibility to use strict mode (#336)
- Included sub-resource integrity (#308)
- `IHtmlCollection` helpers and API improvement (#293)
- General performance improvements (#390)

# 0.9.7.0

Released on Sunday, July 17 2016.

- Fixed some bugs (#343, #325, #341, #347, #355, #358)
- Improved cookie handling (#280, #274, #365)
- Added a document factory (#331)
- `EventNames`, `AttributeNames` and others are available (#330)
- Allow setting the active document (#281)
- Improved Xamarin.iOS build (#85)
- Changed service API slightly (#157)
- Enhanced CoreCLR support (#270, #362)

# 0.9.6.0

Released on Thursday, May 5 2016.

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

# 0.9.5.0

Released on Wednesday, March 16 2016.

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

# 0.9.4.0

Released on Wednesday, December 30 2015.

- Added the link relation factory (#174)
- Fixed a bug related to `document.write` (#173)
- Delay document loading for resources (#178)
- Improved HTTP requester performance (#194)
- Added HTML imports (#179)
- Include CSS color enhancements (#176)
- Enhanced encoding with multibyte characters (#210, #212)
- Use common `CssNode` as root (#145)
- Added `Attr` extension method (#199)

# 0.9.3.0

Released on Thursday, October 8 2015.

- Important bugfixes (#160, #161, #162, #165, #170)
- XML parser enhancements
- Ability to provide custom entities
- Fixed `CompareDocumentPosition` (#168)

# 0.9.2.0

Released on Thursday, September 24 2015.

- Some bugfixes (#150)
- XML parser enhancement (#144)
- JSON form submission (#126)
- Changed license to MIT
- Provide flex hex parsing (`Color`)

# 0.9.1.0

Released on Wednesday, September 9 2015.

- Content of `iframe` can be set
- Default `IEventLoop` provided
- Improved the `HttpRequester`
- Fixed obtaining ext. stylesheets without CSS
- Added option to filter requests
- Parse CSS Unicode escapes

# 0.9.0.0

Released on Wednesday, August 27 2015.

- Implemented `srcset` attribute
- Implemented `picture` element
- Made Shadow DOM API spec. draft available
- Custom `MimeType` datatype
- Supports CSS round-tripping
- Assembly is now strongly signed
- Provide standard `IEventAggregator` implementation

# 0.8.9.0

Released on Wednesday, July 29 2015.

- Improved resource fetching
- Fixed waiting mechanism
- Fixed form submission (avoid initial empty line)
- HTML Parser perf. improved
- Expose `INamedNodeMap` interface
- Fixed problems with `@import`
- Added the `@viewport` CSS rule

# 0.8.8.0

Released on Wednesday, July 22 2015.

- Fixed a bug in the `HtmlDomBuilder`
- Adjusted CSSOM for tolerating unknown rules
- Parser enhancements

# 0.8.7.0

Released on Wednesday, July 15 2015.

- CSS parser more flexible
- Allow inline styles to be customized

# 0.8.6.0

Released on Wednesday, July 8 2015.

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

# 0.8.5.0

Released on Wednesday, June 17 2015.

- Fixed CSS property parsing
- Implemented new CSS value converters
- Fixed some cookie issues
- Made `Configuration.Default` available
- Changed some namespace assignments (e.g., `ScriptOptions`)
- Fixed `HtmlLinkElement` issue
- Fixed CSS twisted comment issue

# 0.8.4.0

Released on Wednesday, June 3 2015.

- Added ability to wait for outstanding requests
- Fixed missing dashes in hostnames
- Changed CSS parser / tokenizer interaction
- Reworked CSS value model
- Extended `IMarkupFormatter` to serialize attributes
- Included encoding service
- Fixed `BrowsingContext` content from string loading
- Improved HTML parser performance
- Allow unknown properties in the CSSOM

# 0.8.3.0

Released on Wednesday, April 22 2015.

- `PrettyMarkupFormatter` for readable output
- Add some missing `ConfigureAwait(false)` calls
- Included virtual response callback for the context
- Add `IHtmlDocument` for completeness
- Reintegrated `XmlParser` (only non-validating)
- Changed `Configuration` to be immutable
- All `IConfiguration` extensions return new object
- Fixed smaller issues

# 0.8.2.0

Released on Wednesday, April 15 2015.

- `DocumentBuilder` declared obsolete
- Fixed bugs in DOM methods
- Added `ToHtml()` overloads with custom formatters
- Changed CSS value model
- Fixed BOM in form submits
- Changed case of tags to mimic browsers
- Improved URL encoded form submission
- Improved CSS shorthand properties
- Fixed `Origin` of `Url`
- Improved loading customization
- Included `IEventAggregator` for events
- Fixed several smaller bugs

# 0.8.1.0

Released on Tuesday, February 10 2015.

- `IsInvalid` of `Url` corrected
- Included .NET 4 version in the NuGet package
- Included Silverlight version in the NuGet package
- Fixed a few smaller bugs

# 0.8.0.0

Released on Monday, February 02 2015.

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

# 0.7.0.0

Released on Saturday, November 08 2014.

- Native (callback based) asynchronous parsing
- Interfaces for resource loading defined
- Browsing context available / creation possible
- Extension methods to `IConfiguration` available
- More attributes added
- Namespace changes for the attributes
- CSS property architecture finalized

# 0.6.1.0

Released on Thursday, August 21 2014.

- Minor bug fixes
- DOM Events
- Configuration improved
- Performance improvements
- `Url` origin
- Core algorithms changed [WHATWG]
- Scripting and styling interfaces

# 0.6.0.0

Released on Sunday, July 27 2014.

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
- Unified `Url` class

# 0.5.1.0

Released on Tuesday, May 27 2014.

- Finished CSS properties
- CSS parser update
- Media rules implemented
- Rewritten URL parsing
- More documentation
- Extended DocumentBuilder

# 0.5.0.0

Released on Monday, April 21 2014.

- Support for Legacy version of AngleSharp
- Updated styling
- Removed XML parser
- API renewal
- Dependency Injection update
- New CSS value model
- More DOM properties
- CSS properties added
- Finished form submission

# 0.4.0.0

Released on Thursday, November 21 2013.

- DTD parsing improved
- Rewritten CSS parser
- More methods and DOM completeness
- XML validation

# 0.3.7.0

Released on Wednesday, September 11 2013.

- Namespaces fully included
- Doctype improved
- DTD parsing finished
- Cleanup
- CSS rules extended
- CSS functions added

# 0.3.6.0

Released on Tuesday, September 03 2013.

- Performance improvements
- Fixed CSS selector
- More documentation
- Live collection
- Started form submission
- Fixed double-escaped script tags
- Improved parsing of inline styling

# 0.3.5.0

Released on Thursday, August 29 2013.

- Finished template implementation
- MathML and SVG elements
- More annotations
- `RelList` established
- Improved TextNode

# 0.3.4.0

Released on Monday, August 26 2013.

- More DOM properties
- Hyper reference normalization
- Introduced template element
- Polish API

# 0.3.3.0

Released on Wednesday, August 21 2013.

- Fixed double escaped script content
- DOM extensions
- Configuration object
- Default HTTP requester

# 0.3.2.0

Released on Sunday, August 18 2013.

- Fixed parser bugs
- Improved entity retrieval
- Extended CDATA parsing
- Improved fragment parsing

# 0.3.1.0

Released on Wednesday, August 14 2013.

- Improved character parsing
- Cleanup
- Line skipping for `textarea`, `pre`
- Extended Location

# 0.3.0.0

Released on Thursday, July 18 2013.

- Included XmlParser
- DTD parsing
- Added IOC container
- Finished element type declaration

# 0.2.9.0

Released on Wednesday, July 10 2013.

- Fixed GetElementsByName
- Improved GetElementsByTagName
- Converted to PCL project
- Updated Children property

# 0.2.8.0

Released on Wednesday, July 03 2013.

- Binding capabilities
- CSS parser update
- More decorations
- Integrated table, frame
- Added pooling

# 0.2.7.0

Released on Wednesday, June 26 2013.

- Updated CSS serialization
- Intermediate objects
- More DOM attributes

# 0.2.6.0

Released on Wednesday, June 19 2013.

- Improved API
- QuerySelector implementation
- Fixed DocumentUri bug

# 0.2.5.0

Released on Tuesday, June 18 2013.

- HTML element re-ordering
- Extended documentation
- HtmlColor structure

# 0.2.4.0

Released on Sunday, June 16 2013.

- Faster CSS parser
- Initial CSS parser release

# 0.2.3.0

Released on Wednesday, June 12 2013.

- StyleSheet integration
- Ignore unknown CSS rules

# 0.2.2.0

Released on Sunday, June 09 2013.

- Updated documentation
- Fixed parsing bug

# 0.2.1.0

Released on Sunday, June 09 2013.

- XML documentation
- Merged DocumentBuilder and NodeBuilder

# 0.2.0.0

Released on Wednesday, June 05 2013.

- Initial release
