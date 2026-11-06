namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Events;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// What the DOM needs from a scripting engine for custom elements, as their constructors and
    /// callbacks are written in its language. Registered as a service of the browsing context.
    /// </summary>
    public interface ICustomElementHost
    {
        /// <summary>
        /// Reads what is needed of a constructor when it is defined. This is the place to refuse
        /// something that is no constructor by throwing.
        /// </summary>
        /// <param name="document">The document of the registry.</param>
        /// <param name="constructor">The constructor as given to the registry.</param>
        /// <returns>What is known about it.</returns>
        CustomElementDescription Describe(IDocument document, Object constructor);

        /// <summary>
        /// Runs the constructor for an existing element. Errors are reported by the host.
        /// </summary>
        /// <param name="element">The element to upgrade.</param>
        /// <param name="definition">The definition the element gets.</param>
        /// <returns>True if the element is one now, false if the constructor failed.</returns>
        Boolean Construct(IElement element, CustomElementDefinition definition);

        /// <summary>
        /// Calls one of the callbacks of a definition. Errors are reported by the host.
        /// </summary>
        /// <param name="document">The document of the registry, which owns the callback.</param>
        /// <param name="element">The element it is called for, which may be in another one by now.</param>
        /// <param name="callback">The callback, as given in the description.</param>
        /// <param name="arguments">The arguments of the call.</param>
        void Invoke(IDocument document, IElement element, Object callback, params Object?[] arguments);
    }

    /// <summary>
    /// What a host read from the constructor of a custom element.
    /// </summary>
    public sealed class CustomElementDescription
    {
        /// <summary>
        /// Creates a new description.
        /// </summary>
        /// <param name="observedAttributes">The attributes to tell the element about.</param>
        /// <param name="callbacks">The callbacks by name, as far as there are any.</param>
        public CustomElementDescription(IEnumerable<String> observedAttributes, IReadOnlyDictionary<String, Object> callbacks)
        {
            ObservedAttributes = observedAttributes ?? Array.Empty<String>();
            Callbacks = callbacks ?? new Dictionary<String, Object>();
        }

        /// <summary>
        /// Gets the attributes to tell the element about.
        /// </summary>
        public IEnumerable<String> ObservedAttributes { get; }

        /// <summary>
        /// Gets the callbacks by name, as far as there are any.
        /// </summary>
        public IReadOnlyDictionary<String, Object> Callbacks { get; }
    }

    /// <summary>
    /// A custom element known to a registry.
    /// </summary>
    public sealed class CustomElementDefinition
    {
        internal const String Connected = "connectedCallback";
        internal const String Disconnected = "disconnectedCallback";
        internal const String AttributeChanged = "attributeChangedCallback";
        internal const String Adopted = "adoptedCallback";

        private readonly HashSet<String> _observedAttributes;
        private readonly IReadOnlyDictionary<String, Object> _callbacks;

        internal CustomElementDefinition(CustomElementRegistry registry, String name, String localName, Object constructor, CustomElementDescription description)
        {
            Registry = registry;
            Name = name;
            LocalName = localName;
            Constructor = constructor;
            _observedAttributes = new HashSet<String>(description.ObservedAttributes);
            _callbacks = description.Callbacks;
        }

        internal CustomElementRegistry Registry { get; }

        /// <summary>
        /// Gets the name it was defined with.
        /// </summary>
        public String Name { get; }

        /// <summary>
        /// Gets the local name of its elements, which differs from the name if it extends another one.
        /// </summary>
        public String LocalName { get; }

        /// <summary>
        /// Gets the constructor as given to the registry.
        /// </summary>
        public Object Constructor { get; }

        internal Object? GetCallback(String name) => _callbacks.TryGetValue(name, out var callback) ? callback : null;

        internal Boolean IsObserving(String attribute) => _observedAttributes.Contains(attribute);

        internal IEnumerable<String> ObservedAttributes => _observedAttributes;
    }

    /// <summary>
    /// The options of defining a custom element.
    /// </summary>
    [DomName("ElementDefinitionOptions")]
    public sealed class ElementDefinitionOptions
    {
        /// <summary>
        /// Gets or sets the name of the element it extends (e.g. button), if any.
        /// </summary>
        [DomName("extends")]
        public String? Extends { get; set; }
    }

    /// <summary>
    /// How far an element is from being a custom one (DOM 4.9), where elements that can't be one
    /// don't get asked.
    /// </summary>
    internal enum CustomElementState : Byte
    {
        Undefined,
        Constructing,
        Failed,
        Custom,
    }

    /// <summary>
    /// What the DOM knows about an element in relation to custom elements. Few elements ever have
    /// any of this, so it's not on the element: an element without an entry is undefined.
    /// </summary>
    internal sealed class CustomElementData
    {
        public CustomElementState State { get; set; }

        public CustomElementDefinition? Definition { get; set; }

        /// <summary>
        /// Gets or sets the name of the custom element it was created as, which isn't an attribute.
        /// </summary>
        public String? IsValue { get; set; }
    }

    /// <summary>
    /// Represents the customElements of a window (HTML 4.13.4), which holds the definitions of custom
    /// elements. Reactions run right after the operation causing them, as there is no end of a script
    /// to wait for in here, and can't be observed to differ unless that operation changed even more.
    /// </summary>
    [DomName("CustomElementRegistry")]
    [DomExposed("Window")]
    public sealed class CustomElementRegistry
    {
        private static readonly HashSet<String> ReservedNames = new HashSet<String>(StringComparer.Ordinal)
        {
            "annotation-xml", "color-profile", "font-face", "font-face-src",
            "font-face-uri", "font-face-format", "font-face-name", "missing-glyph",
        };

        private const String IsAttribute = "is";

        private readonly Document _document;
        private readonly Dictionary<String, CustomElementDefinition> _definitions = new Dictionary<String, CustomElementDefinition>(StringComparer.Ordinal);
        private readonly Dictionary<String, TaskCompletionSource<Object>> _pending = new Dictionary<String, TaskCompletionSource<Object>>(StringComparer.Ordinal);
        private Boolean _isDefining;
        private Boolean _hasAdoptedElements;
        private Boolean _hasUpgradesOfParse;

        // per document, so that a document without custom elements doesn't look anything up
        private readonly ConditionalWeakTable<Element, CustomElementData> _elements = new ConditionalWeakTable<Element, CustomElementData>();

        [ThreadStatic]
        private static Stack<ReactionQueue>? s_reactions;
        private static Boolean s_isInUse;

        internal CustomElementRegistry(Document document)
        {
            _document = document;
        }

        /// <summary>
        /// Gets if the elements of the document may have to be told anything.
        /// </summary>
        internal Boolean HasDefinitions => _definitions.Count > 0 || _hasAdoptedElements;

        internal void HasAdoptedElements() => _hasAdoptedElements = true;

        private static CustomElementRegistry Of(Document document) =>
            document.CustomElements ?? (document.CustomElements = new CustomElementRegistry(document));

        private static CustomElementData? GetData(Element element) =>
            element.Owner?.CustomElements is { } registry && registry._elements.TryGetValue(element, out var data) ? data : null;

        private static CustomElementData GetOrAddData(Element element) =>
            Of(element.Owner)._elements.GetOrCreateValue(element);

        private static CustomElementState GetState(Element element) => GetData(element)?.State ?? CustomElementState.Undefined;

        private static CustomElementDefinition? GetDefinition(Element element) => GetData(element)?.Definition;

        internal static String? GetIsValue(Element element) => GetData(element)?.IsValue;

        internal static void SetIsValue(Element element, String? value)
        {
            if (value is not null)
            {
                GetOrAddData(element).IsValue = value;
            }
        }

        internal static void CopyIsValue(Element source, Element target) => SetIsValue(target, GetIsValue(source));

        internal static void NotifyAttributeChanged(Element element, String name, String? oldValue, String? newValue, String? namespaceUri) =>
            GetDefinition(element)?.Registry.AttributeChanged(element, name, oldValue, newValue, namespaceUri);

        // what it has goes with it to the document, and it has to be told about what happens to it there as well
        internal void ElementMoved(Element element, Document oldDocument, Document newDocument)
        {
            if (_elements.TryGetValue(element, out var data))
            {
                _elements.Remove(element);
                var target = Of(newDocument);
                target._elements.Remove(element);
                target._elements.Add(element, data);

                if (data.Definition is { } definition)
                {
                    target.HasAdoptedElements();
                    definition.Registry.ElementAdopted(element, oldDocument, newDocument);
                }
            }
        }

        internal void ParsingFinished()
        {
            if (_hasUpgradesOfParse)
            {
                _hasUpgradesOfParse = false;
                _document.MarkMutated();
            }
        }

        /// <summary>
        /// Defines a custom element, and makes it of the elements in the document waiting for it.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <param name="constructor">The constructor, as the host of the scripts understands it.</param>
        /// <param name="options">The options, if any.</param>
        [DomName("define")]
        public void Define(String name, Object constructor, ElementDefinitionOptions? options = null)
        {
            if (!IsValidName(name))
            {
                throw new DomException(DomError.Syntax);
            }

            if (_isDefining || _definitions.ContainsKey(name) || _definitions.Values.Any(m => Equals(m.Constructor, constructor)))
            {
                throw new DomException(DomError.NotSupported);
            }

            var extends = options?.Extends;
            var localName = name;

            if (extends != null)
            {
                if (IsValidName(extends) || _document.CreateElement(extends) is Html.Dom.IHtmlUnknownElement)
                {
                    throw new DomException(DomError.NotSupported);
                }

                localName = extends;
            }

            var host = _document.Context.GetService<ICustomElementHost>() ?? throw new DomException(DomError.NotSupported);
            _isDefining = true;
            CustomElementDescription description;

            try
            {
                description = host.Describe(_document, constructor);
            }
            finally
            {
                _isDefining = false;
            }

            _definitions[name] = new CustomElementDefinition(this, name, localName, constructor, description);
            Volatile.Write(ref s_isInUse, true);
            UpgradeTree(_document);

            if (_pending.TryGetValue(name, out var pending))
            {
                _pending.Remove(name);
                pending.TrySetResult(constructor);
            }
        }

        /// <summary>
        /// Gets the constructor of the custom element with the given name.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <returns>The constructor, if defined.</returns>
        public Object? Get(String name) => name != null && _definitions.TryGetValue(name, out var definition) ? definition.Constructor : null;

        /// <summary>
        /// Gets the name of the custom element with the given constructor.
        /// </summary>
        /// <param name="constructor">The constructor of the element.</param>
        /// <returns>The name, if defined.</returns>
        [DomName("getName")]
        public String? GetName(Object constructor) => FindDefinition(constructor)?.Name;

        /// <summary>
        /// Waits for the custom element with the given name to be defined.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <returns>The task giving the constructor of the element.</returns>
        [DomName("whenDefined")]
        public Task<Object> WhenDefined(String name)
        {
            if (!IsValidName(name))
            {
                return Task.FromException<Object>(new DomException(DomError.Syntax));
            }

            if (_definitions.TryGetValue(name, out var definition))
            {
                return Task.FromResult(definition.Constructor);
            }

            if (!_pending.TryGetValue(name, out var pending))
            {
                pending = new TaskCompletionSource<Object>();
                _pending[name] = pending;
            }

            return pending.Task;
        }

        /// <summary>
        /// Upgrades the elements in the given tree, even if they aren't in a document.
        /// </summary>
        /// <param name="root">The root of the tree.</param>
        [DomName("upgrade")]
        public void Upgrade(INode root)
        {
            if (root is Node node)
            {
                UpgradeTree(node);
            }
        }

        /// <summary>
        /// Finds the definition of the given constructor, for a host being asked to run it.
        /// </summary>
        /// <param name="constructor">The constructor.</param>
        /// <returns>The definition, if any.</returns>
        public CustomElementDefinition? FindDefinition(Object constructor) =>
            constructor is null ? null : _definitions.Values.FirstOrDefault(m => Equals(m.Constructor, constructor));

        /// <summary>
        /// Creates the element of a definition without running its constructor, for a host being in
        /// the constructor already, i.e., for what a script creates with new.
        /// </summary>
        /// <param name="definition">The definition of the element.</param>
        /// <returns>The element.</returns>
        public IElement CreateElement(CustomElementDefinition definition)
        {
            var element = (Element)_document.CreateUncustomizedElement(definition.LocalName);
            var data = GetOrAddData(element);
            data.Definition = definition;
            data.State = CustomElementState.Custom;

            if (definition.Name != definition.LocalName)
            {
                data.IsValue = definition.Name;
            }

            return element;
        }

        /// <summary>
        /// Gets if the name is one a custom element can have (HTML 4.13.3).
        /// </summary>
        /// <param name="name">The name to check.</param>
        /// <returns>True if it can be, false otherwise.</returns>
        public static Boolean IsValidName(String? name)
        {
            if (name is null || name.Length == 0 || name[0] < 'a' || name[0] > 'z' || ReservedNames.Contains(name))
            {
                return false;
            }

            var hasHyphen = false;

            for (var i = 0; i < name.Length; i++)
            {
                var c = name[i];
                hasHyphen |= c == '-';

                if (!IsNameCharacter(c))
                {
                    return false;
                }
            }

            return hasHyphen;
        }

        // the PCENChar of the standard, where a surrogate stands for what is above the BMP
        private static Boolean IsNameCharacter(Char c) =>
            c == '-' || c == '.' || c == '_' || c == 0xB7 ||
            (c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') ||
            (c >= 0xC0 && c <= 0xD6) || (c >= 0xD8 && c <= 0xF6) || (c >= 0xF8 && c <= 0x37D) ||
            (c >= 0x37F && c <= 0x1FFF) || (c >= 0x200C && c <= 0x200D) || (c >= 0x203F && c <= 0x2040) ||
            (c >= 0x2070 && c <= 0x218F) || (c >= 0x2C00 && c <= 0x2FEF) || (c >= 0x3001 && c <= 0xDFFF) ||
            (c >= 0xF900 && c <= 0xFDCF) || (c >= 0xFDF0 && c <= 0xFFFD);

        internal static Boolean IsDefined(IElement element)
        {
            if (element.NamespaceUri.Is(NamespaceNames.HtmlUri) && element is Element custom &&
                (IsValidName(element.LocalName) || GetIsValue(custom) is not null || element.HasAttribute(IsAttribute)))
            {
                return GetState(custom) == CustomElementState.Custom;
            }

            return true;
        }

        internal void ElementCreated(Element element) => TryUpgrade(element);

        internal void ElementCloned(Element element) => Schedule(element, () => TryUpgrade(element));

        internal void NodeInserted(Node node)
        {
            if (IsConnected(node))
            {
                foreach (var element in GetElements(node).ToArray())
                {
                    Schedule(element, () =>
                    {
                        var state = GetState(element);

                        if (state == CustomElementState.Undefined)
                        {
                            TryUpgrade(element);
                        }
                        else if (state == CustomElementState.Custom)
                        {
                            Call(element, CustomElementDefinition.Connected);
                        }
                    });
                }
            }
        }

        internal void NodeRemoved(Node node)
        {
            foreach (var element in GetElements(node).ToArray())
            {
                if (GetState(element) == CustomElementState.Custom)
                {
                    Schedule(element, () => Call(element, CustomElementDefinition.Disconnected));
                }
            }
        }

        internal void AttributeChanged(Element element, String name, String? oldValue, String? newValue, String? namespaceUri)
        {
            if (GetData(element) is { State: CustomElementState.Custom, Definition: { } definition } && definition.IsObserving(name))
            {
                Schedule(element, () => Call(element, CustomElementDefinition.AttributeChanged, name, oldValue, newValue, namespaceUri));
            }
        }

        internal void ElementAdopted(Element element, Document oldDocument, Document newDocument)
        {
            if (GetState(element) == CustomElementState.Custom)
            {
                Schedule(element, () => Call(element, CustomElementDefinition.Adopted, oldDocument, newDocument));
            }
        }

        /// <summary>
        /// Starts holding back the reactions of custom elements, for a host calling a member of the
        /// DOM: they run when it returns (HTML 4.13.6), so that what a script started is complete by
        /// the time other scripts get to see it. Holds for this thread, as all DOM work of a page is
        /// on the one thread of its event loop.
        /// </summary>
        public static void PushReactions() => (s_reactions ?? (s_reactions = new Stack<ReactionQueue>())).Push(new ReactionQueue());

        /// <summary>
        /// Runs the reactions held back since the last call of <see cref="PushReactions"/>.
        /// </summary>
        public static void PopReactions() => s_reactions!.Pop().Run();

        /// <summary>
        /// Gets if a custom element was defined at all in this process, i.e., if a host has to care.
        /// </summary>
        public static Boolean IsInUse => Volatile.Read(ref s_isInUse);

        // Without anybody holding back they run right away, which is what the parser asks for.
        private static void Schedule(Element element, Action reaction)
        {
            if (s_reactions is { Count: > 0 } reactions)
            {
                reactions.Peek().Add(element, reaction);
            }
            else
            {
                reaction.Invoke();
            }
        }

        private void UpgradeTree(Node root)
        {
            foreach (var element in GetElements(root).ToArray())
            {
                if (GetState(element) == CustomElementState.Undefined)
                {
                    Schedule(element, () => TryUpgrade(element));
                }
            }
        }

        private void TryUpgrade(Element element)
        {
            if (GetState(element) != CustomElementState.Undefined || !element.NamespaceUri.Is(NamespaceNames.HtmlUri))
            {
                return;
            }

            var definition = Lookup(element);

            if (definition is null)
            {
                return;
            }

            var data = GetOrAddData(element);
            data.State = CustomElementState.Constructing;
            data.Definition = definition;

            if (!_document.Context.GetService<ICustomElementHost>()!.Construct(element, definition))
            {
                data.State = CustomElementState.Failed;
                data.Definition = null;
                return;
            }

            data.State = CustomElementState.Custom;

            // a parse leaves the version alone: all of its upgrades count as one when it's done
            if (_document.IsLoading)
            {
                _hasUpgradesOfParse = true;
            }
            else
            {
                _document.MarkMutated();
            }

            // an upgrade is told like the attributes appeared and the element came into the document
            foreach (var name in definition.ObservedAttributes)
            {
                if (element.HasAttribute(name))
                {
                    Call(element, CustomElementDefinition.AttributeChanged, name, null, element.GetAttribute(name), null);
                }
            }

            if (IsConnected(element))
            {
                Call(element, CustomElementDefinition.Connected);
            }
        }

        private CustomElementDefinition? Lookup(Element element)
        {
            var name = element.LocalName;

            if (_definitions.TryGetValue(name, out var definition) && definition.LocalName == name)
            {
                return definition;
            }

            var isValue = GetIsValue(element) ?? element.GetAttribute(IsAttribute);
            return isValue != null && _definitions.TryGetValue(isValue, out definition) && definition.LocalName == name ? definition : null;
        }

        // the element may have moved on, while what belongs to its definition stays where it is
        private static void Call(Element element, String callbackName, params Object?[] arguments)
        {
            var definition = GetDefinition(element);
            var callback = definition?.GetCallback(callbackName);

            if (callback != null)
            {
                var document = definition!.Registry._document;
                document.Context.GetService<ICustomElementHost>()?.Invoke(document, element, callback, arguments);
            }
        }

        /// <summary>
        /// The reactions held back, where those of an element run together (HTML 4.13.6).
        /// </summary>
        private sealed class ReactionQueue
        {
            private readonly List<KeyValuePair<Element, List<Action>>> _elements = new List<KeyValuePair<Element, List<Action>>>();

            public void Add(Element element, Action reaction)
            {
                foreach (var entry in _elements)
                {
                    if (ReferenceEquals(entry.Key, element))
                    {
                        entry.Value.Add(reaction);
                        return;
                    }
                }

                _elements.Add(new KeyValuePair<Element, List<Action>>(element, new List<Action> { reaction }));
            }

            public void Run()
            {
                foreach (var entry in _elements)
                {
                    foreach (var reaction in entry.Value)
                    {
                        reaction.Invoke();
                    }
                }
            }
        }

        // the node and what is below it in tree order, including what is in shadow trees
        private static IEnumerable<Element> GetElements(Node root)
        {
            if (root is Element element)
            {
                yield return element;

                if (element.ShadowRoot is Node shadow)
                {
                    foreach (var inner in GetElements(shadow))
                    {
                        yield return inner;
                    }
                }
            }

            foreach (var child in root.ChildNodes.OfType<Node>().ToArray())
            {
                foreach (var inner in GetElements(child))
                {
                    yield return inner;
                }
            }
        }

        internal static Boolean IsConnected(INode node)
        {
            var root = node.GetRoot();

            while (root is IShadowRoot shadow && shadow.Host is { } host)
            {
                root = host.GetRoot();
            }

            return root is IDocument;
        }
    }

    /// <summary>
    /// The options of creating an element.
    /// </summary>
    [DomName("ElementCreationOptions")]
    public sealed class ElementCreationOptions
    {
        /// <summary>
        /// Gets or sets the name of the custom element extending the element.
        /// </summary>
        [DomName("is")]
        public String? Is { get; set; }
    }

    /// <summary>
    /// Defines the createElement of documents that takes options.
    /// </summary>
    [DomExposed("Document")]
    public static class ElementCreationExtensions
    {
        /// <summary>
        /// Creates an element that is the custom element with the given name, if it extends it.
        /// </summary>
        /// <param name="document">The document.</param>
        /// <param name="localName">The local name of the element.</param>
        /// <param name="options">The options.</param>
        /// <returns>The element.</returns>
        [DomName("createElement")]
        public static IElement CreateElement(this IDocument document, String localName, ElementCreationOptions? options)
        {
            var owner = (Document)document;
            var element = owner.CreateUncustomizedElement(localName);
            CustomElementRegistry.SetIsValue(element, options?.Is);
            owner.CustomElements?.ElementCreated(element);
            return element;
        }
    }

    /// <summary>
    /// Defines the isConnected member of nodes.
    /// </summary>
    [DomExposed("Node")]
    public static class NodeConnectionExtensions
    {
        /// <summary>
        /// Gets if the node is in a document, also if that is by being in a shadow tree of it.
        /// </summary>
        /// <param name="node">The node.</param>
        /// <returns>True if it is in a document, false otherwise.</returns>
        [DomName("isConnected")]
        [DomAccessor(Accessors.Getter)]
        public static Boolean IsConnected(this INode node) => CustomElementRegistry.IsConnected(node);
    }

    /// <summary>
    /// Defines the toggleAttribute member of elements.
    /// </summary>
    [DomExposed("Element")]
    public static class ElementToggleAttributeExtensions
    {
        /// <summary>
        /// Adds the attribute if it is missing and removes it if it is there.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="force">If given, only ever adds (true) or removes (false) it.</param>
        /// <returns>True if the attribute is there now, false otherwise.</returns>
        [DomName("toggleAttribute")]
        public static Boolean ToggleAttribute(this IElement element, String name, Boolean? force = null)
        {
            if (!name.IsXmlName())
            {
                throw new DomException(DomError.InvalidCharacter);
            }

            if (element.HasAttribute(name))
            {
                if (force == true)
                {
                    return true;
                }

                element.RemoveAttribute(name);
                return false;
            }

            if (force == false)
            {
                return false;
            }

            element.SetAttribute(name, String.Empty);
            return true;
        }
    }

    /// <summary>
    /// Defines the customElements member of the window.
    /// </summary>
    [DomExposed("Window")]
    public static class CustomElementsExtensions
    {
        /// <summary>
        /// Gets the registry of custom elements of the window.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <returns>The registry.</returns>
        [DomName("customElements")]
        [DomAccessor(Accessors.Getter)]
        public static CustomElementRegistry CustomElements(this IWindow window)
        {
            var document = (Document)window.Document;
            return document.CustomElements ?? (document.CustomElements = new CustomElementRegistry(document));
        }
    }
}
