# API Documentation

## Core API

AngleSharp has been created with a useful, yet standard complient API. If you just care about parsing a single document or stylesheet, then you can always just use the various parsers, such as the `HtmlParser` or the `CssParser`. In most cases (parsing and using webpages) we recommend the `BrowsingContext` located in the `AngleSharp` namespace. This namespace also contains some types with extension methods and a `Url` class that strictly follows the algorithms described in the WHATWG specification.

The `AngleSharp.Attributes` namespace also features the attributes being used to decorate the interfaces (and enumerations and delegates). Here we have:

* `DomAccessorAttribute` for defining special accessors such as getters, setters or deleters
* `DomHistoricalAttribute` to indicate an obsolete status
* `DomDescriptionAttribute` to store a description string for a DOM part
* `DomNoInterfaceObjectAttribute` to declare a type being interfaced only, i.e. no object realization available
* `DomPutForwardsAttribute` to set the name of the method of the related object, where input should be forwarded
* `DomNameAttribute` to represent the original API name

The API of the interfaces / DOM objects has been changed in such a way, that it is still the original DOM API (nothing is missing), but with naming and types being .NET conform and (hopefully) easier to work with.

### Configuration

The `IConfiguration` is the main interface for extending AngleSharp. If we do not care, we do not have to supply AngleSharp with an instance of `IConfiguration` for parsing documents or URLs. In that scenario a default configuration will be considered.

This default configuration can be chosen by calling the method `SetDefault` on the `Configuration` class. We only need to pass in an instance of the custom configuration to use as the default configuration. The `Configuration` class is the default implementation of the `IConfiguration` interface. The default implementation is usually a good starting point for providing custom configurations.

The role of an `IConfiguration` object is to provide an enumeration of services (all implementing the `IService` interface) to use, an `IEventAggregator`, if any, and to specify the culture to use. While the services extend the DOM beyond a mostly static version, the event aggregator makes receiving notifications from the DOM possible. The culture is important in parsing and interpretation of the DOM.

The following example sets the `Culture` of the new `IConfiguration` object:

	var config = new Configuration(culture: new CultureInfo("de-de"));

If no `Culture` is specified, the culture from the currently active thread will be taken.

Another possible way to do this is by using the extension methods. For example if only want to adjust the culture of the default configuration we can do the following:

	var config = Configuration.Default.SetCulture("de-de");

Please note that the `Configuration` is immutable by default and all extension methods will either return the current instance (unmodified) or a new object (modified compared to the originally passed one).

Additionally we might create our own class that is more convinient and flexible than the immutable implementation. We could either extend the default implementation, or implement the interface. Note, however, that the extension methods should actually make working with the `IConfiguration` pretty easy and straight forward.

Implementing the interface is also possible, but requires of course more work, since every property or methods needs to be re-implemented. However, since the properties of the default implementation are not `virtual`, it might be the only chance for providing the desired setup. In general there should be only few reasons to implement `IConfiguration` ourselves.

### Future Thoughts

The `AngleSharp.Css.Values` namespace contains a set of types used in the CSSOM, such as:

* `Angle` to represent angular values
* `Color` to represent color (ARGB) values
* `Frequency` to represent frequencies
* `Length` to represent length values
* `Number` to represent numbers in the CSSOM
* `Percent` to wrap percentage numbers
* `Priority` to give priority for CSS selectors
* `Resolution` to represent resolution values
* `Time` to represent time (spans)
* `TransformMatrix` to store tranformation information

The CSSOM is quite rich and is not fully available right now. The CSSOM will probably be partially cut out of the core library and integrated to another library.

## Extension Points

AngleSharp is supposed to create an universal HTML5 parser that is accessible in the .NET world and written completely in managed code. However, some applications may want to go beyond the parser. The parser alone would require a lot of help from the outside to create the DOM.

In order to supply users of AngleSharp with a full DOM implementation, AngleSharp extends the HTML5 parser with a real DOM implementation. Nevertheless, this again brings some additional dependencies. What if, e.g., the `HtmlAudioElement` should play audio from its source? Of course one could just write a wrapper around the element, reading out the `Source` and supervising changes. But then this one wrapper would probably be incompatible (and / or behave differently) than the rest of the DOM.

This inconsistency is avoided by definining such external behavior in form of interfaces. Objects implementing such an interface might be registered to be used within AngleSharp.

In this page the various extension points will be listed and explained. Currently missing (but planned) interfaces will be sketched.

### IStylingService and IScriptingService

If a style (also external stylesheets in a `<link>` tag) or a script block is encountered, AngleSharp tries to find a matching engine. An implementation of `IStylingService` for instance looks at the provided mime-type and returns an associated `IStyleEngine` object or `null`. If the latter is encountered, another `IStylingService` object is tried, until a proper engine has been found, or all styling services have been asked. The same algorithm applies to `IScriptingService`. These servives are only describing functionality of a factory, flyweight repository or binding.

### IStyleEngine

AngleSharp already includes a CSS parser. This was one of the design goals of AngleSharp. Additionally it is required, since the (HTML) DOM is strongly coupled to CSS on some points. One example would be the `querySelector` and `querySelectorAll` methods. These methods require a CSS parser (or at least a CSS selector parser). In the end an object that is able to match certain elements is produced.

Nevertheless, an HTML browser may or may not know other styling possibilities than CSS. Currently CSS is used by default (and without specifying, e.g., `text/css` in the `type` attribute). However, one could register a style parser for a certain (or multiple) such types. Following this way, it is also possible to register a custom CSS parser, overriding the currently provided one.

### IScriptEngine

AngleSharp does not provide a script engine by default. Of course any JavaScript engine would be a great addition, since JavaScript is the programming language of the web.

However, right now there are no intentions in providing an official / integrated solution. The `AngleSharp.Scripting` project that is contained in the solution, is a sample project, to demonstrate how simple it is to write such an extension.

Finally we have to think about allowing C# as a scripting language. This would certainly be possible. Backed up with scriptcs or any other solution (Roslyn?) this would be a great addition, which could also be something different. In the long run it is great that AngleSharp supports multiple scripting engines.

Registering a scripting engine and providing scripting capabilities are two different pairs of shoes. AngleSharp requires the user to activate scripting (indifferent if scripting engines are available or not) before scripts are being executed.

### ISpellCheckService

Allows registration of individual spell-checkers. Each spell-checker is registered with its culture, making it sensitive to whatever culture the webpage or text is probably using.

The API allows ignoring certain words, running queries if a word is correctly spelled or getting suggestions for the correct spelling of a word. Right now the API has been implemented synchronously, but in the future that might switch to a better version, that is fully asynchronous.

### IResourceService<IObjectInfo>

This extension is implemented in various interfaces, see:

* `IAudioService`, for the `HtmlAudioElement`
* `IVideoService`, for the `HtmlVideoElement`
* `IImageService`, for the `HtmlImageElement`
* `IObjectService`, for the `HtmlObjectElement`

The basic idea is to determine certain properties of the contained resource. The implementation of the specialization of `IResourceInfo` carries the resource dependent information, which would be the dimensions and more for an image. In case of an `HtmlAudioElement` the full media controller would be implemented as well, allowing playback of the media stream.

In case of the `HtmlObjectElement` this leads directly to plugins, such as Adobe Flash or others. Obviously the AngleSharp core is not responsible for these very specialized tasks.

### INavigatorService

Creates a `INavigator` instance for the given `IWindow` instance. The `INavigator` seems to be quite generic at first, however, it could be really specialized to the underlying `IWindow` instance, especially in terms of accessing media resources (e.g., webcam or microphone).

AngleSharp does not implement the interface in order to leave room for more adequate and specialized implementations. Also the previously described ability to enhance the user-experience with external peripherals seems appealing.

### IHistoryService

The `IHistoryService` describes the functionality to create a new DOM `IHistory` object, which will be associated with a browsing context. The browsing context is passed to the constructor function described by the interface.

### IWindowService

There are multiple DOM elements that could be implemented in a more specialized and useful way. A crucial element is the `IWindow` implementation. It is not directly required, since all DOM interaction involves the `IDocument`, which is not dependent on the `IWindow`. However, especially in scripting environments, the `IWindow` instance fulfills a quite important role.

For more advanced scenarios, like rendering, a custom implementation of the `IWindow` interface seems important. Therefore the given service gives users the ability to register a custom `IWindow` creator. Note, that at the moment such a custom creator would be required to inherit at least from `EventTarget` (which implements `IEventTarget`). In the future this requirement will hopefully be omitted.

### ILoaderService

Loading documents or resources in AngleSharp can be fully customized. The main interface is the `ILoaderService`, which defines two constructor functions. One constructor function to create a new `IDocumentLoader` and another to create a new `IResourceLoader`. While the former is then used to load real documents in the context of a browsing context (hence there is max. one `IDocumentLoader` per `IBrowsingContext`), the latter is used for loading resources in the context of an `IDocument`. Obviously we require at most one `IResourceLoader` per `IDocument`.

There are two big advantages in this architecture:

1. The responsibilities are clearly separated and every context (main or document) can track their own requests.
2. It is easy to turn off resource loading (even for particular elements) without affecting document loading / form submission at all.

There is also a default implementation called `LoaderService`, which has boolean properties that switch the resource and / or document loading on or off. The default implementation makes use of the available requesters.
