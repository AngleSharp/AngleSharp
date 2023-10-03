---
title: "Included Features"
section: "AngleSharp.Core"
---
# Included Features

This section lists features that have been moved from the list of upcoming features to the implemented features. It does not include all available features, even though this may be the ultimate goal of this page. The purpose of moving the implemented features in here is to keep track of the used references. That way it should be easier to track W3C specification changes or if tests are missing.

## Mutation Records

There are types like `MutationObserver`, `MutationObserverInit` and `IMutationRecord` that have been implemented.

These types can act like generic notification listeners, that fire when something in the DOM changes. They would be also used internally by nodes, which may need to know when their content changes. As an example: The `HtmlStyleElement` needs to know when its content changes, since the new content has to be parsed and evaluated.

*Status*: Implemented, tests available.

More information can be found at:

* [HTML5 Rocks Introduction](http://updates.html5rocks.com/2012/02/Detect-DOM-changes-with-Mutation-Observers)
* [MDN Mutation Observer](https://developer.mozilla.org/en/docs/Web/API/MutationObserver)
* [W3C DOM 4](http://www.w3.org/TR/2014/WD-dom-20140710/#mutation-observers)
* [WHATWG DOM Standard](http://dom.spec.whatwg.org/#mutation-observers)

## Provide Various Uri Implementations

A URI parser has been included within AngleSharp. This parser is nearly finished, but it does not do unicode normalization for the host part. This would be too heavy and should usually be provided by the .NET-Framework, however, for our PCL target it is not.

*Status*: Implemented, tests available.

More information can be found at:

* [W3C Location IDL](http://www.w3.org/TR/html5/browsers.html#the-location-interface)
* [W3C URL Definition](http://www.w3.org/TR/url/)
* [WHATWG URL Standard](http://url.spec.whatwg.org/)
* [RFC3986 Specification](https://datatracker.ietf.org/doc/rfc3986/)

## PseudoElement Integration

CSS provides several extensions to the DOM described in the HTML standard. One of these extensions is the integration of so-called pseudo-elements. These elements do not really exist in the DOM, however, they can be queried, and they are also part of the visual tree.

A pseudo-element is not like a real element, and it only implements a special IDL, which will be called `IPseudoElement` in AngleSharp. Most importantly, this interface implies the implementation of the `IGetStyleUtils` interface. So, even though a pseudo-element is not a real element, it has the most important properties, which are required for being part of the drawing process.

*Status*: Minimal implementation provided. No tests yet.

More information can be found at:

* [W3C CSSOM PseudoElement](http://dev.w3.org/csswg/cssom/#pseudoelement)
* [W3C CSS3 Selectors](http://www.w3.org/TR/css3-selectors/)

## Extend the IWindow Interface

Central for any interaction with the DOM (at least from the perspective of a (JavaScript) developer) is the (root) context object, usually a `Window` object. AngleSharp uses the `IWindow` interface as IDL for such objects. Despite the available class `AnalysisWindow` in v0.7 AngleSharp does only provide a `Window` class implementation. It is much more general and may work together with the `RenderDevice` information.

The `Window` implementation also includes timers and more and can be really handy. It is quite crucial and used heavily from the browsing context. However, it is still possible to provide custom implementations.

*Status*: Minimal implementation provided. No tests yet.

More information can be found at:

* [W3C Window Module v1](http://www.w3.org/TR/Window/)
* [MDN Window](https://developer.mozilla.org/en-US/docs/Web/API/Window)
* [CSS View Model](http://www.w3.org/TR/cssom-view/)
* [WHATWG DOM Standard](http://www.whatwg.org/specs/web-apps/current-work/#the-window-object)
* [Selection extension](https://dvcs.w3.org/hg/editing/raw-file/tip/editing.html#dom-document-getselection)

## Event Handling

The ability to add or remove event listeners, along with the `dispatch` function have been included. What is also finished is the transformation of all .NET event handlers to their explicit form.

Consider the following example:

```cs
public event EventListener Aborted;
```

This is an implicit event handler. The C# compiler will create a backing delegate field, which will be initialized to `null`. The `add` (`+=`) and `remove` parts (`-=`) will be used when these operators are applied on the `Aborted` field.

A transformation resulted in the following code:

```cs
public event EventListener Aborted
{
    add { AddEventListener(EventNames.Abort, value); }
    remove { RemoveEventListener(EventNames.Abort, value); }
}
```

Here the backing field is no longer generated. We explicitly define the `add` and `remove` methods, which will just call the `AddEventListener` / `RemoveEventListener` methods. The name of the event can be found at the MDN event reference. Usually it is the official DOM name, e.g., *onabort*, without the *on*-prefix.

This will be added to the `EventNames` class as a readonly static field:

```cs
public static readonly String Abort = "abort";
```

*Status*: Implemented, tests available.

More information can be found at:

* [WHATWG WebApp APIs](http://www.whatwg.org/specs/web-apps/current-work/multipage/webappapis.html#eventhandler)
* [MDN event reference](https://developer.mozilla.org/en-US/docs/Web/Events)
* [W3C DOM3 events](http://www.w3.org/TR/DOM-Level-3-Events)
* [W3C UIEvent extension](https://dvcs.w3.org/hg/d4e/raw-file/tip/source_respec.htm)

## Rework Core DOM Algorithms

The core DOM algorithms have been implemented using MDN and W3C documentation. Nevertheless, it has been obvious that some of these inner workings do not work as expected in edge cases. Therefore, the WHATWG documentation will be considered, as it provides more detailed information with more related information.

As a side effect, the API will also be adapted partially. In the end this will not only be beneficial from a standard compliance perspective, but also for users. Edge cases have to work not only within the HTML5 parser, but also during DOM interaction.

*Status*: Implemented, some tests available.

More information can be found at:

* [WHATWG DOM Standard](http://dom.spec.whatwg.org/)

## Browsing Context

Right now AngleSharp tries to be purely focused on doing a great job in parsing HTML (and CSS), however, at a later stage one might want to build more on top of this library. Therefore, (and to be fully W3C conform), the ability to register / create / open a real browsing context is required.

A browsing context connects various parts. It contains a list with the (previous) documents and knows what document is currently viewed. It provides the `IWindowProxy` implementation, which forwards every call to the `IWindow` object of the currently active document.

Currently, the specification is evaluated and a proper interface is planned. AngleSharp provides a straightforward default implementation, which basically just holds the configuration to use.

*Status*: Minimal implementation provided. No tests yet.

More information can be found at:

* [W3C browsers spec.](http://www.w3.org/TR/2013/CR-html5-20130806/browsers.html#windows)
* [WHAT about browsing](http://www.whatwg.org/specs/web-apps/current-work/multipage/browsers.html)

## Reshape CSS API

The HTML API has been completely reworked. Some of these principles have also been applied for CSS. Since there is nothing available in the official specification, it is hard to provide an API that does not conflict with the official one. Nevertheless, to make AngleSharp effective and useful for working with CSS, an object-oriented API is required.

Using just strings is useful in JavaScript, but not in a language like C# (or F#, VB, ...). We require the compiler to check rules, detect errors and help us by telling the IDE what methods and properties are available. Therefore, a non-standard object-oriented API has been implemented, which sits directly on top of the CSSOM that is being used.

*Status*: Implemented, tests available.

More information can be found at:

* [W3C CSSOM](http://dev.w3.org/csswg/cssom/#the-getstyleutils-interface)

## WebIDL Attributes

The Interface Definition Language (IDL) is used for describing the interfaces accessible from languages such as JavaScript. AngleSharp implements the named interface and algorithms. However, the naming has been changed (sometimes more, sometimes less). In order to automate wrapper generation for, e.g., JavaScript engines, and documentation generators, AngleSharp provides a custom attribute called `DomNameAttribute`. This attribute decorates methods, properties, types and more. Finally one is able to get the `OfficialName` by querying the corresponding `Type` instance.

There are more such attributes, which reveal more sophisticated properties of a member. An example would be if the method should actually be treated like an index getter or setter (or both). Maybe setting a property should be redirected to a property of the property. There are many specified actions, that are described in the official IDL language. Some of these are easy to deduce by using reflection, some might require the help of a custom attribute.

This work package deals with finding important IDL constructs. Then a proper way of dealing with them (e.g., decoration with a custom attribute) has to be found. Finally, the existing interfaces need to be investigated / extended, depending on what has been decided in the second step.

*Status*: Most important ones implemented, more to come upon request / requirement.

More information can be found at:

* [W3C WebIDL](http://www.w3.org/TR/WebIDL/)

## Complete the IDocument Interface

The central node of a document is certainly the `IDocument` object itself. It is the owner of every attached node (directly or indirectly) and it has the connection to the `IWindow` object and the `IBrowsingContext` (only accessible internally), which has the `IConfiguration` instance attached.

Right now the `IDocument` interface is nearly complete and even includes some properties / methods that seem exotic at first. While some of these members do not contribute much (at least at the moment), others are certainly more interesting, especially in conjunction with scripting. One example is the `currentScript` property, which represents the currently executed `IHtmlScriptElement`.

Also, the command API is implemented and can be connected via an available extension. All in all, the `IDocument` interface and its implementation (`Document`) can be said to be completely finished.

*Status*: Implemented, some tests available.

More information can be found at:

* [WHATWG Document Object](http://www.whatwg.org/specs/web-apps/current-work/multipage/dom.html#the-document-object)
* [MDN Document](https://developer.mozilla.org/en-US/docs/Web/API/document)
* [W3C DOM2 (HTMLDocument), now merged into Document](http://www.w3.org/TR/DOM-Level-2-HTML/html.html#ID-26809268)
* [PointerLock extension](https://dvcs.w3.org/hg/pointerlock/raw-file/default/index.html#extensions-to-the-document-interface)
* [CSSOM extensions](http://dev.w3.org/csswg/cssom/#extensions-to-the-document-interface)
* [W3C DOM3 (Document)](http://www.w3.org/TR/DOM-Level-3-Core/#i-Document)
* [W3C DOM4 (Document)](http://www.w3.org/TR/2014/WD-dom-20140710/#interface-document)

## Improve Async Parsing

Asynchronous parsing is quite important. Even though standard (even large) processing may only take a few milliseconds, AngleSharp will also be used with sources that are (network) streams. There are two possible strategies: The first is to download everything before (asynchronous, and is an array of bytes), while the second is to continuously download data while processing the document. The latter has the advantage that even though the document may be long and the transfer may be broken at some point, at least parts of the document are already available.

The other advantage of the second approach is the possibility of starting other downloads, while the original one is still in progress. So instead of having one download after another, we have multiple downloads at once, which uses the available bandwidth even better.

AngleSharp will therefore try to implement the second approach. The `TextSource` class can already handle (network) streams well - and does use the "natively" provided asynchronous callback possibilities. Nevertheless, the `ParseAsync` method only relies on the synchronous one, wrapping it in another `Task` (which is just another `Thread` in this scenario).

The idea is to provide a real asynchronous method, i.e. a method that makes use of the async methods provided in `TextSource`. This will eventually result in a lot of copy / paste with only a few changes, but it is worth the duplication. Of course, some techniques to reduce duplication have to be used to minimize maintenance.

As an important hint here: Any `await` call should be only used with `ConfigureAwait(false)`. This is required to ensure that calling the method synchronously will not result in any deadlocks regardless of the environment.

*Status*: Implemented and the parser is also internally awaiting further tasks such as script execution or stylesheet resolution.

## The Picture Element

The picture element is the solution to responsive images and their problems. Images need to be adjusted for the pixel density and viewing device. The problem is, that the existing `HtmlImageElement` could only handle a single source. Now that changed due to the (not yet implemented) `srcset` and `sizes` attributes. The first one gives various sources along with their potential media queries. The latter is useful for setting the image size depending on the pixel density or width of the viewport.

However, if we want a more fine-grained solution than provided by the new `img`-attributes, then we should look at the `picture`-element. The element has been implemented in the `HtmlPictureElement` class, however, nothing has yet been included. A solution that might work just fine is to port the polyfill to the element. That solution would be a great start and could then be adjusted, if necessary.

*Status*: Element available, `srcset` attribute and `source` children are taken into account. Currently, no device validation possible (part of CSS improvements for v1.0). Hence quasi static.

More information can be found at:

* [Article about srcset and sizes](http://ericportis.com/posts/2014/srcset-sizes/)
* [Polyfill for picture](https://github.com/scottjehl/picturefill/blob/master/src/picturefill.js)
* [WHATWG about Embedded Content](https://html.spec.whatwg.org/multipage/embedded-content.html#embedded-content)
* [W3C Embedded Content specification](http://www.w3.org/html/wg/drafts/html/master/embedded-content.html#the-picture-element)

## Loading Resources

There is more to loading resources than meets the eye. Right now AngleSharp implements very rudimentary algorithms of fetching any external data. This will change in the future.

This change will align with more extensions (and probably constraints) for the `IBrowsingContext`. The browsing context is also from a security point of view crucial. Here the basic flags are set to determine what rights the underlying APIs may have. Another direct implication is to do the correct unloading of the document. This is not yet implemented.

*Status*: Simple implementations available, more sophisticated ones required. The sandboxing parsing is already implemented.

More information can be found at:

* [Browser context names for initial rights](http://www.w3.org/TR/html5/browsers.html#unit-of-related-similar-origin-browsing-contexts)
* [The role of the top browsing context](http://www.w3.org/html/wg/drafts/html/CR/browsers.html#top-level-browsing-context)
* [Unload a document](http://www.w3.org/html/wg/drafts/html/CR/browsers.html#unload-a-document)
* [Navigate to a webpage](http://www.w3.org/html/wg/drafts/html/CR/browsers.html#navigate)
* [Sandboxing](http://www.w3.org/html/wg/drafts/html/CR/browsers.html#sandboxing)

## Include Touch Events

There are several events in the DOM. Even though, some of these events are just plain notifications; others may transport custom data. In this group we find classics such as keyboard or mouse events. The W3C also created special touch events, which carry touch data in the form of a list of touch points.

The purpose of this task is to supply all required interface, implementations and extension points to allow the creation and handling of touch events. This will extend the `IDocument`, `IElement` and other existing interfaces. This will also create new interfaces such as `ITouchEvent` or `ITouchList`.

There are no tricky algorithms, touch implementation or OS specific bindings to create. This is just the layer that allows creation of POD objects.

*Status*: Everything available.

More information can be found at:

* [W3C Touch events Recommendation](http://www.w3.org/TR/touch-events/)

## Possibility of XPath Parsing

Right now CSS selectors is the way of querying the document from an official perspective. Nevertheless, there are people who are quite dissatisfied with this solution. Most of those people are not web developers, and are familiar with even more powerful queries in the form of XPath. Unfortunately XPath is currently not support on a standard level.

*Status*: Available in the `AngleSharp.XPath` library.

More information can be found at:

* [W3C XPathEvaluator IDL](http://www.w3.org/TR/DOM-Level-3-XPath/xpath.html#XPathEvaluator)
* [W3C XPath Specification](http://www.w3.org/TR/xpath/)
