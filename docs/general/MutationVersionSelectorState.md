# Selector state and document mutation versions

`Document.MutationVersion` lets a consumer cache derived results without observing or walking every node. That only works when native state that affects the result also advances the version. A checked input is a small counterexample to DOM-only tracking:

```csharp
var document = (Document)new HtmlParser().ParseDocument("<input type=checkbox>");
var input = (IHtmlInputElement)document.QuerySelector("input");
var version = document.MutationVersion;
var cached = input.Matches(":checked"); // false
input.IsChecked = true;                 // native host call, not a wrapper call
if (version != document.MutationVersion)
    cached = input.Matches(":checked");
// cached must now be true.
```

Before selector-state invalidation, the native setter changed the selector result without changing the version. It changed no attribute or serialized markup and produced no `MutationObserver` record. `NativeCheckednessInvalidatesACacheWithoutMutationRecords` is an executable regression for this use; its cache returns the stale value on the base implementation.

The concrete consumer is Jint.Browser: `input:checked { display: none }` can change whether a target has a layout box between two geometry reads. A repeated unchanged read should reuse its previous result, but a same-turn read after a host's `IHtmlInputElement.IsChecked` assignment must recompute it. This change supplies the missing notification; it does not add a cache or layout behavior to Core.

An external wrapper works only if every caller agrees to use it. An embedding API also exposes native AngleSharp nodes, so external code cannot intercept arbitrary calls through those interfaces. The native control implementations are internal and sealed; replacing the element factory would require replacing the controls and reproducing their existing DOM behavior. A DOM observer cannot report a state change for which no record exists, even if records are delivered or drained synchronously. Rescanning control state on every read can detect it, but gives up the cheap unchanged-read requirement; serialized DOM does not contain the state at all. The existing native setters are the narrowest place that can cover these writes.

The same reasoning applies to selectedness, indeterminacy, value/dirty-value/custom-validity state and focus. Focus advances the version at the actual transition, before either focus callback can query a cache. Form reset uses the existing control mutation paths. No interface member, observer registration or mutation record is added. Read-only selector and validity queries do not advance the version. The token remains conservative: attempted/no-op writes can advance it, so compare for equality only.

This is not a universal renderer revision. Stylesheets, render-device settings, custom selector services and host-owned state still require separate invalidation signals or an uncached fallback.

Parser bookkeeping is handled by the separate [Core #1347](https://github.com/AngleSharp/AngleSharp/pull/1347)
follow-up to #1344. Native control initialization must not add increments to that construction path;
cloning copies validity state directly instead of calling a notifying user setter. Consumers must
invalidate at parser boundaries independently, or keep reads query-local while construction can resume.
User changes made from parser callbacks still count as mutations.
