# AGENTS.md

Guidance for AI coding agents working in this repository. `CLAUDE.md` imports this file, so
keep it the single source of truth and every agent reads the same instructions.

AngleSharp is a standards-driven HTML5 / CSS-selector / XML parser with a full W3C DOM for
.NET. This repository is `AngleSharp.Core` — the parser, the DOM, the selector engine and the
browsing-context machinery. Everything else is a plugin registered as a service:
`AngleSharp.Css` (CSSOM), `AngleSharp.Io` (requesters, cookies), `AngleSharp.Js` (Jint
binding), `AngleSharp.Xml`, `AngleSharp.XPath`, `AngleSharp.Diffing`. Changes here ripple
into all of them.

## Commands

The orchestrator is NUKE (`nuke/Build.cs`), bootstrapped by `build.ps1` / `build.sh`
(`build.cmd` forwards to either). Default target is `RunUnitTests`.

```powershell
.\build.ps1                        # restore, compile, run the full test suite
.\build.ps1 -Target Compile        # other targets: Clean Restore Compile RunUnitTests
.\build.ps1 -Target Package        #   CopyFiles CreatePackage Package PrePublish Publish
```

For the normal edit/test loop use the SDK directly — much faster than the NUKE bootstrap:

```powershell
dotnet build src/AngleSharp/AngleSharp.Core.csproj -f net8.0
dotnet test src/AngleSharp.Core.Tests/AngleSharp.Core.Tests.csproj -f net8.0
dotnet test src/AngleSharp.Core.Tests/AngleSharp.Core.Tests.csproj -f net8.0 --filter "FullyQualifiedName~CssSelector"
dotnet test src/AngleSharp.Core.Tests/AngleSharp.Core.Tests.csproj -f net8.0 --filter "Name=NthChildWithOfSyntax"
```

- Always pass `-f net8.0` when iterating. On Windows the test project also targets `net462`
  and `net472`, so omitting it runs everything three times. The library itself targets
  `netstandard2.0;net8.0;net10.0` (plus `net462;net472` on Windows) — building the solution
  without a `-f` builds all of them.
- `TreatWarningsAsErrors` is on (`src/Directory.Build.props`) — a warning breaks the build.
  There is no separate lint step; the compiler plus `ConfigureAwaitChecker.Analyzer` is it.
- `RunUnitTests` runs the suite **twice**, with `prefetched=false` and `prefetched=true`. That
  variable is real here (`TestRuntime.UsePrefetchedTextSource`): it switches
  `TestExtensions.ToHtmlDocument` between the `String` and the `ReadOnlyMemory<Char>` parser
  overload, i.e. two different text-source implementations. Anything touching the tokenizer or
  `Text/` must pass in both modes.
- Benchmarks (`src/AngleSharp.Benchmarks`, BenchmarkDotNet) — see *Performance* below.
- Two places carry the version and both must be bumped for a release: `CHANGELOG.md` (its top
  entry is parsed by `ReleaseNotesParser` into the NuGet package version and the GitHub
  release body) and `<Version>` in `src/Directory.Build.props` (assembly version). NuGet
  dependencies are declared by hand in `src/AngleSharp.nuspec`, not generated from the csproj.
- There is no `global.json`; the bootstrap scripts use the STS channel, CI installs 10.0.x.

## Architecture

### Configuration and browsing context

`IConfiguration` is an immutable bag of services — either an instance or a
`Func<IBrowsingContext, T>` creator (`Configuration.Default` lists the whole default set in one
place). `ConfigurationExtensions` gives `With` / `Without` / `WithOnly`, each returning a new
configuration. `BrowsingContext` is the browser-tab equivalent: it holds the service list,
resolves `GetService<T>()` by walking that list, and **replaces a creator with its materialized
instance in-place on first resolution**, so services are lazy and per-context.

Almost everything is swappable through this mechanism: `IHtmlParser`, `ICssSelectorParser`, the
element factories, `IDocumentLoader` / `IResourceLoader`, `IScriptingService` (AngleSharp.Js),
`IStylingService` (AngleSharp.Css). A missing service means the feature is silently off —
resource loading and navigation require `WithDefaultLoader()`, scripting requires a scripting
service. Feature checks read the service list, so registering one widens behaviour elsewhere.

### The HTML pipeline

`TextSource` → `HtmlTokenizer` → `HtmlDomBuilder<TDocument, TElement>` → DOM.

- `Html/Parser/HtmlTokenizer.cs` (~3000 lines) is the spec tokenizer state machine, built on
  `Common/BaseTokenizer.cs` which owns the character buffer and position tracking.
- `Html/Parser/HtmlDomBuilder.cs` (~4500 lines) is tree construction — insertion modes
  (`HtmlTreeMode`), the open-element and active-formatting-element stacks, foster parenting,
  the adoption agency algorithm. This file mirrors the spec section by section; when fixing a
  parsing bug, find the corresponding spec step rather than patching symptoms.
- Tokens are **structs** (`Html/Parser/Tokens/Struct/StructHtmlToken.cs`) whose names are
  `StringOrMemory` slices into the source, not strings.

The tree builder is generic over `IConstructableDocument` / `IConstructableElement`
(`Html/Construction`). The real DOM is just one implementation of that contract, supplied by
`HtmlDomConstructionFactory` via the `IHtmlElementConstructionFactory` service. The public
generic entry point

```csharp
TDocument ParseDocument<TDocument, TElement>(TextSource source, TokenizerMiddleware? middleware = null)
```

lets a consumer run the full spec-correct parser into their own lightweight tree with no
AngleSharp DOM objects at all. Keep tree-construction logic free of concrete DOM types — it
must stay expressible through the `IConstructable*` interfaces.

### The DOM

The public surface is interfaces (`Dom/IElement.cs`, `Html/Dom/IHtml*Element.cs`, …); the
implementations are `internal` and live in `Dom/Internal`, `Html/Dom/Internal`,
`Svg/Dom`, `Mathml/Dom`. `Node` and `Element` are the base classes; `NodeFlags` marks
spec-relevant traits (special elements, HTML-only, self-closing, …) that the parser consults.
Collections such as `HtmlCollection` are **live** — they query on enumeration.

`AngleSharp.Attributes` (`[DomName]`, `[DomConstructor]`, `[DomExposed]`, `[DomInitDict]`,
`[DomPutForwards]`, …) annotates ~190 files with the official spec name of each type and
member. Core itself does not read them: they are the contract `AngleSharp.Js` reflects over at
runtime to build the entire JavaScript binding. Renaming a member without carrying its
`[DomName]` along, or adding a DOM member without one, silently changes or omits the JS API.

Mutation is observable through `MutationObserver` plus the internal `IAttributeObserver`
service, which the parser uses on a fast path during construction.

### CSS in core

Only what `querySelector` needs: `Css/Parser` (`CssTokenizer`, `CssSelectorParser`,
`CssSelectorConstructor`) compiles a selector string into the `ISelector` tree in
`Css/Dom/Internal` (one small class per simple selector, composed by `ComplexSelector`,
`CompoundSelector`, `ListSelector`). `ISelector.Match(element, scope)` is the hot path for every
query. Specificity is `Priority`; `ISelectorVisitor` walks a parsed selector.

Full stylesheet parsing and the CSSOM are **not** here — they live in AngleSharp.Css behind
`IStylingService`. The attribute / pseudo-class / pseudo-element selector factories are
services too, so plugins can add selectors core does not know.

### IO and loading

`Io/BaseLoader` with `DefaultDocumentLoader` / `DefaultResourceLoader`, `DefaultHttpRequester`,
and one `Io/Processors/*RequestProcessor` per resource kind (script, stylesheet, image, frame,
object, media). CORS, integrity and cookie hooks are interfaces. AngleSharp.Io replaces this
stack with a `HttpClient`-based one.

### Text sources

`Text/TextSource` is a facade over two strategies:

- `WritableTextSource` — accumulating and rewindable, backed by `CharArrayTextSource` /
  `ReadOnlyMemoryTextSource` / `StringTextSource` depending on the input. Rewinding is what
  makes the meta-charset restart path work.
- `StreamingTextSource` — bounded forward-only decoding with a small lookback window for
  tokenizer reconsumption, plus a provisional 1 KB prefix so the encoding can still change
  before the source freezes (`StreamTextSourceMode.Bounded`).

Encoding resolution is BOM → `EncodingMetaHandler` → restart with the new encoding.

## Performance

**Performance is a headline feature of this library, not an afterthought.** AngleSharp is
benchmarked against HtmlAgilityPack and CsQuery and the README claims browser-class speed on
real pages. Throughput *and* allocations both count: a change that costs allocations on the
parse path is a regression even if wall time is unchanged. Recent history is largely perf work
— an `O(n²)` fix for large plain-text runs, the bounded streaming source, attribute-observer
dispatch during parsing, selector matching, URL parsing.

Measure, do not assert:

```powershell
dotnet run -c Release --project src/AngleSharp.Benchmarks -f net8.0 -- --filter "*SelectorBenchmark*"
dotnet run -c Release --project src/AngleSharp.Benchmarks -f net8.0 -- --list flat
```

`MemoryDiagnoser` is enabled on every benchmark class, so always report the allocation column
alongside the timing. `SelectorBenchmark` and `StreamingTextSourceBenchmark` work offline off
`page.html`, `ScanDataTextBenchmark` off synthetic input; `ParserBenchmark` fetches ~20 real
sites and caches them under `temp/`, so it needs network on the first run. `net472` is a
benchmark target as well, and the framework-vs-core gap is where `#if`-guarded fast paths get
lost — check both when touching them.

Techniques the codebase relies on; match them in new code:

- **Slice, do not allocate strings.** `StringOrMemory` (`Common/`) wraps a
  `ReadOnlyMemory<Char>` over the source so token names, attribute names and text runs cost
  nothing until someone asks for a `String`. `OrdinalStringOrMemoryComparer` lets those slices
  be dictionary keys directly.
- **Pool buffers.** `BaseTokenizer` picks `ArrayPoolBuffer` when the source length is known
  upfront (rented from `ArrayPool<Char>.Shared`, max capacity bounded by the source) and falls
  back to `StringBuilderBuffer` otherwise. `StringBuilderPool` recycles builders process-wide
  and is tunable (`MaxCount`, `SizeLimit`, `IsPoolingDisabled` for high-parallelism callers).
- **Intern the well-known names.** `HtmlTagNameLookup` and `HtmlAttributesLookup` map a
  freshly-scanned buffer back onto the `TagNames` / `AttributeNames` constants. Downstream code
  then uses `ReferenceEquals` for zero-cost dispatch — `HtmlElementFactory.Create` has a
  reference-equality fast path for the hottest tags before touching a dictionary. Do not break
  that invariant by handing the parser non-interned names.
- **Bulk-scan with spans.** `BaseTokenizer.ScanDataText` finds the next terminator with
  `IndexOfAny` and appends the whole run at once instead of looping per character. Character-
  at-a-time loops over large runs are exactly the shape that produced the last `O(n²)` bug.
- **Guard newer BCL APIs, keep a real fallback.** `FrozenDictionary` (`HtmlElementFactory`,
  `CssSelectorConstructor`) and `SearchValues` (`BaseTokenizer`) sit behind
  `#if NET8_0_OR_GREATER` with a `Dictionary` / manual `IndexOfAny` alternative. `Span<T>`,
  `Memory<T>` and `ArrayPool<T>` *are* available on every target — they arrive transitively via
  `System.Text.Encoding.CodePages`, the library's only package dependency — so span-based code
  needs no guard. `netstandard2.0` and `net462` are shipping targets, not an afterthought.
- **No LINQ, no closures, no enumerator allocation on per-token / per-node / per-element
  paths.** The tokenizer, tree builder and selector matchers are plain loops. LINQ is fine in
  one-off setup code that feeds a static cache.
- **Ordinal comparisons.** Use the `Is` / `Isi` extensions in `Text/StringExtensions.cs` rather
  than `ToLower()` comparisons; they do not allocate. DOM names are case-sensitive where the
  spec says so.
- **Lazy per-instance state.** Fields such as `Element._classList` stay null until used; there
  can be very many elements. When adding per-node state, ask what it costs on a document that
  never touches it.
- **Watch the AoT targets.** Issue #1252 was segfaults on Android under .NET AoT, fixed by
  concretizing an interface call in `ClassSelector.Match` and by making the lazy `ClassList`
  initialization `Interlocked`-safe. Devirtualization tricks and lazy fields on shared nodes
  need to be both thread-safe and AoT-safe.

`docs/general/04-Performance.md` holds the user-facing comparison numbers; update it when a
change moves them materially.

## Code conventions

From `.github/CONTRIBUTING.md` and `.editorconfig` — several differ from typical modern C#:

- `using` directives go **inside** the namespace declaration (newer file-scoped-namespace files
  put them right after the namespace line — same effect).
- Framework type names, not keywords: `String`, `Int32`, `Boolean`, `Object`.
- Prefer `var` on the left-hand side wherever possible ("VIP" / RHS convention).
- Always use statement blocks; blank line between two non-simple statements.
- Be explicit about access modifiers.
- `ConfigureAwait(false)` on every `await` — enforced by an analyzer, so omitting it fails the
  build.
- 4 spaces, LF, UTF-8, trimmed trailing whitespace; 2 spaces in `*.csproj`.
- Nullable reference types are enabled for the library (not for the tests).
- Older files use `#region Fields / ctor / Properties / Methods / Helpers`; match the file you
  are editing rather than converting it.
- Comments explain *why* a non-obvious construct exists (spec step, fast path, AoT workaround).
  Keep that density — do not narrate what the code already says.

## Tests

NUnit 3 (classic model: `Assert.AreEqual`), everything in `src/AngleSharp.Core.Tests`.

The idiomatic test parses through the string extensions rather than constructing a parser:

```csharp
var document = "<div class='note'>text</div>".ToHtmlDocument();
Assert.AreEqual(1, document.QuerySelectorAll(".note").Length);
```

- `String.ToHtmlDocument(config?, onError?)` / `.ToHtmlFragment(context)` in
  `TestExtensions.cs`; `Configuration` helpers there add mock requesters, scripting or
  resource loading (`WithMockRequester`, `WithVirtualRequester`, `WithScripting`).
- `Mocks/` has the substitutes for anything external — `MockRequester`, `VirtualRequester`,
  `CallbackScriptEngine`, `StandingEventLoop`. Prefer them over hitting the network.
- Tests that genuinely need network call `Helper.IsNetworkAvailable()`, which reports
  `Inconclusive` rather than failing; `Helper.IsFramework(...)` does the same for TFM-specific
  tests.
- **Large parts of the suite are generated** from the official spec suites by the scripts in
  `src/TestGeneration` (`generate.linq` for LINQPad, `generator.js` for the JS ones) out of
  html5lib `.dat` files and W3C JSON fixtures: `Html/TreeConstruction.cs` (~3400 lines),
  `Css/CssW3CSelector.cs` (~6900 lines), the `Validity*` files, `Html/HtmlEntity.cs`. Do not
  hand-edit those — change the fixture or the generator and regenerate. Do not read them
  wholesale either; grep for the case you need.
- `Vulnerabilities/` holds regression tests for reported security issues; a security fix
  belongs there.
- Every behavioural change wants a test, and per CONTRIBUTING the test should come from the
  W3C/WHATWG specification or the html5lib suite where one exists.

## Repository notes

These files are synced from the `AngleSharp.GitBase` repository and should not be edited here:
`.editorconfig`, `.gitignore`, `.gitattributes`, `.github/*`, `build.ps1`, `build.sh`,
`LICENSE`.

CI (`.github/workflows/ci.yml`) builds on Linux and Windows; on Windows it selects the NUKE
target from the branch (`main` → `Publish`, `devel` → `PrePublish`, otherwise the default).
`CONTRIBUTING.md` asks for feature branches (`feature/#777` or `issue-777`) and pull requests
against `devel`; `main` is reserved for releases. Release-worthy changes get a `CHANGELOG.md`
line under the top version heading.

User-facing documentation lives in `docs/` (`docs/README.md` is the index) and is published
separately — keep it in sync when public API or behaviour changes.
