namespace AngleSharp.Generators
{
    using Microsoft.CodeAnalysis;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;

    /// <summary>
    /// What a registration declares, worked out from the DOM attributes of the compilation and of
    /// the AngleSharp libraries it references.
    /// </summary>
    /// <remarks>
    /// The rules are the ones AngleSharp.Js applies when it reflects over the same attributes
    /// (DomShapeBuilder, PrototypeTypeCache, CreatorCache, ConstantCache, Extensibility), so that
    /// which member wins a name, what is inherited and what an enum becomes agree. The one
    /// deliberate difference is the key of a prototype: reflection keys it by the internal class
    /// an object is built from, a registration by the public interface (or class) carrying the
    /// DOM name, as another library adding members to it has to be able to name it.
    /// </remarks>
    sealed class DomModel
    {
        //  see ConstantCache: enums whose name does not say the interface owning their constants
        private static readonly Dictionary<String, String> ConstantOwners = new Dictionary<String, String>(StringComparer.Ordinal)
        {
            { "AngleSharp.Dom.NodeType", "Node" },
            { "AngleSharp.Dom.DocumentPositions", "Node" },
            { "AngleSharp.Dom.DomError", "DOMException" },
        };

        private readonly Action<Diagnostic> _report;
        private readonly CancellationToken _cancel;
        private readonly Dictionary<ISymbol, DomType> _definitions = new Dictionary<ISymbol, DomType>(SymbolEqualityComparer.Default);
        private readonly Dictionary<ITypeSymbol, DomType> _instances = new Dictionary<ITypeSymbol, DomType>(SymbolEqualityComparer.Default);
        private readonly Dictionary<String, DomType> _ownByName = new Dictionary<String, DomType>(StringComparer.Ordinal);
        private readonly Dictionary<String, DomType> _foreignByName = new Dictionary<String, DomType>(StringComparer.Ordinal);
        private readonly HashSet<String> _namespaceNames = new HashSet<String>(StringComparer.Ordinal);
        private readonly Dictionary<DomType, Simulation> _simulations = new Dictionary<DomType, Simulation>();

        public DomModel(Compilation compilation, Action<Diagnostic> report, CancellationToken cancel)
        {
            Compilation = compilation;
            _report = report;
            _cancel = cancel;
            Window = compilation.GetTypeByMetadataName(Names.IWindow);
            Node = compilation.GetTypeByMetadataName(Names.INode);
            EventHandler = compilation.GetTypeByMetadataName(Names.DomEventHandler);
            Task = compilation.GetTypeByMetadataName(Names.Task);

            foreach (var assembly in compilation.SourceModule.ReferencedAssemblySymbols)
            {
                if (assembly.Name.StartsWith("AngleSharp", StringComparison.Ordinal))
                {
                    Catalog(assembly, false);
                }
            }

            Catalog(compilation.Assembly, true);
            CollectInstantiations();
        }

        public Compilation Compilation { get; }

        public INamedTypeSymbol? Window { get; }

        public INamedTypeSymbol? Node { get; }

        public INamedTypeSymbol? EventHandler { get; }

        public INamedTypeSymbol? Task { get; }

        /// <summary>
        /// Gets the own DOM types in the order they are declared: mixins, types, the closed forms
        /// of generic ones, and the types sharing the interface object of another one.
        /// </summary>
        public List<DomType> Types { get; } = new List<DomType>();

        public List<INamedTypeSymbol> Namespaces { get; } = new List<INamedTypeSymbol>();

        public List<INamedTypeSymbol> Enums { get; } = new List<INamedTypeSymbol>();

        public List<INamedTypeSymbol> Holders { get; } = new List<INamedTypeSymbol>();

        public List<INamedTypeSymbol> Classes { get; } = new List<INamedTypeSymbol>();

        public void Skip(ISymbol member, ISymbol owner, String reason) =>
            _report(Diagnostic.Create(Diagnostics.Skipped, GetLocation(member), member.Name, owner.Name, reason));

        public void Collide(ISymbol member, String target) =>
            _report(Diagnostic.Create(Diagnostics.Collision, GetLocation(member), member.Name, target));

        public void Unresolved(ISymbol source, String name) =>
            _report(Diagnostic.Create(Diagnostics.Unresolved, GetLocation(source), source.Name, name));

        private Location? GetLocation(ISymbol symbol)
        {
            var location = symbol.Locations.FirstOrDefault();
            return location != null && location.IsInSource ? location : Compilation.Assembly.Locations.FirstOrDefault();
        }

        #region Attributes

        public static IEnumerable<AttributeData> GetAttributes(ISymbol symbol, String name) =>
            symbol.GetAttributes().Where(m => m.AttributeClass?.ToDisplayString() == name);

        public static Boolean Has(ISymbol symbol, String name) => GetAttributes(symbol, name).Any();

        public static List<String> GetDomNames(ISymbol symbol)
        {
            var names = new List<String>();

            foreach (var attribute in GetAttributes(symbol, Names.DomName))
            {
                if (attribute.ConstructorArguments.Length > 0 && attribute.ConstructorArguments[0].Value is String name && !names.Contains(name))
                {
                    names.Add(name);
                }
            }

            return names;
        }

        public static String? GetString(ISymbol symbol, String attribute) =>
            GetAttributes(symbol, attribute).Select(m => m.ConstructorArguments.Length > 0 ? m.ConstructorArguments[0].Value as String : null).FirstOrDefault(m => m != null);

        public static Int32 GetAccessor(ISymbol symbol)
        {
            var attribute = GetAttributes(symbol, Names.DomAccessor).FirstOrDefault();
            return attribute != null && attribute.ConstructorArguments.Length > 0 && attribute.ConstructorArguments[0].Value != null
                ? Convert.ToInt32(attribute.ConstructorArguments[0].Value)
                : -1;
        }

        public static Boolean IsObsoleteError(ISymbol symbol) =>
            GetAttributes(symbol, Names.Obsolete).Any(m => m.ConstructorArguments.Length > 1 && m.ConstructorArguments[1].Value is true);

        public const Int32 Getter = 0x1;
        public const Int32 Setter = 0x2;
        public const Int32 Adder = 0x8;
        public const Int32 Remover = 0x10;
        public const Int32 Method = 0x20;

        #endregion

        #region Catalog

        private void Catalog(IAssemblySymbol assembly, Boolean own)
        {
            var candidates = new List<INamedTypeSymbol>();

            foreach (var type in GetAllTypes(assembly.GlobalNamespace))
            {
                _cancel.ThrowIfCancellationRequested();

                //  what the generated code cannot name stays for reflection alone
                if (own && !IsAccessible(type))
                {
                    continue;
                }

                var names = GetDomNames(type);

                if (own && type.IsStatic && Has(type, Names.DomExposed) && (names.Count == 0 || Has(type, Names.DomNoInterfaceObject)))
                {
                    Holders.Add(type);
                }

                if (own && type.TypeKind == TypeKind.Class && !type.IsStatic)
                {
                    Classes.Add(type);
                }

                //  a WebIDL enumeration needs no name of its own, its strings are what counts
                if (own && type.TypeKind == TypeKind.Enum && names.Count == 0 &&
                    (Has(type, Names.DomLiterals) || type.ToDisplayString() == Names.DocumentReadyState))
                {
                    Enums.Add(type);
                    continue;
                }

                if (names.Count == 0)
                {
                    continue;
                }

                if (type.TypeKind == TypeKind.Enum)
                {
                    if (own)
                    {
                        Enums.Add(type);
                    }
                }
                else if (type.IsStatic)
                {
                    //  a static class with a name is a WebIDL namespace, unless it is a mixin
                    if (!Has(type, Names.DomNoInterfaceObject))
                    {
                        _namespaceNames.Add(names[0]);

                        if (own)
                        {
                            Namespaces.Add(type);
                        }
                    }
                }
                else if (type.TypeKind == TypeKind.Interface || type.TypeKind == TypeKind.Class)
                {
                    candidates.Add(type);
                }
            }

            //  an interface without an interface object that others build on is a mixin
            var bases = new HashSet<ISymbol>(candidates.SelectMany(m => m.AllInterfaces).Select(m => m.OriginalDefinition), SymbolEqualityComparer.Default);
            var byName = own ? _ownByName : _foreignByName;
            var variants = new List<DomType>();
            var mixins = new List<DomType>();
            var primaries = new List<DomType>();

            //  The interface carrying a name comes first, whatever the order of declaration: it is
            //  what the prototype chain is built from, while a class of the same name (EventTarget)
            //  only shares its interface object, adding the constructor.
            foreach (var type in candidates.OrderBy(m => m.TypeKind == TypeKind.Interface ? 0 : 1))
            {
                var names = GetDomNames(type);
                var noInterfaceObject = Has(type, Names.DomNoInterfaceObject);
                var isMixin = type.TypeKind == TypeKind.Interface && noInterfaceObject && bases.Contains(type);
                var exposed = GetAttributes(type, Names.DomExposed).Select(m => m.ConstructorArguments.Length > 0 ? m.ConstructorArguments[0].Value as String : null).Where(m => m != null).ToList();
                var info = new DomType(type, names, isMixin, noInterfaceObject, own, exposed!);
                _definitions[type] = info;

                if (byName.TryGetValue(info.Name, out var first) && !(own && first.IsGenericDefinition && info.IsGenericDefinition))
                {
                    if (!type.IsGenericType)
                    {
                        info.IsVariant = true;
                        variants.Add(info);
                    }
                }
                else
                {
                    if (!own && _foreignByName.ContainsKey(info.Name))
                    {
                        continue;
                    }

                    byName[info.Name] = info;

                    if (!type.IsGenericType)
                    {
                        (isMixin ? mixins : primaries).Add(info);
                    }
                }

                if (!type.IsGenericType)
                {
                    _instances[type] = info;
                }
            }

            if (own)
            {
                Types.AddRange(mixins);
                Types.AddRange(primaries);
                _variants.AddRange(variants);
            }
        }

        private readonly List<DomType> _variants = new List<DomType>();

        private static IEnumerable<INamedTypeSymbol> GetAllTypes(INamespaceSymbol ns)
        {
            foreach (var member in ns.GetMembers())
            {
                if (member is INamespaceSymbol inner)
                {
                    foreach (var type in GetAllTypes(inner))
                    {
                        yield return type;
                    }
                }
                else if (member is INamedTypeSymbol type)
                {
                    yield return type;

                    foreach (var nested in GetNested(type))
                    {
                        yield return nested;
                    }
                }
            }
        }

        private static IEnumerable<INamedTypeSymbol> GetNested(INamedTypeSymbol type)
        {
            foreach (var nested in type.GetTypeMembers())
            {
                yield return nested;

                foreach (var inner in GetNested(nested))
                {
                    yield return inner;
                }
            }
        }

        /// <summary>
        /// Registers the closed forms of the generic DOM types (IHtmlCollection&lt;T&gt;) that the
        /// own DOM uses, each a type of its own sharing the interface object of the first one.
        /// </summary>
        private void CollectInstantiations()
        {
            var found = new List<INamedTypeSymbol>();
            var seen = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);

            void Visit(ITypeSymbol? type)
            {
                if (type is IArrayTypeSymbol array)
                {
                    Visit(array.ElementType);
                }
                else if (type is INamedTypeSymbol named && named.IsGenericType && seen.Add(named))
                {
                    foreach (var argument in named.TypeArguments)
                    {
                        Visit(argument);
                    }

                    if (!named.IsUnboundGenericType && named.TypeArguments.All(m => m.TypeKind != TypeKind.TypeParameter) &&
                        _definitions.TryGetValue(named.OriginalDefinition, out var definition) && definition.IsGenericDefinition)
                    {
                        found.Add(named);
                    }
                }
            }

            IEnumerable<INamedTypeSymbol> sources = Types.Select(m => m.Symbol).Concat(_variants.Select(m => m.Symbol)).Concat(Holders).Concat(Namespaces).Concat(Classes);

            foreach (var type in sources)
            {
                foreach (var contract in type.AllInterfaces)
                {
                    Visit(contract);
                }

                foreach (var member in type.GetMembers())
                {
                    switch (member)
                    {
                        case IMethodSymbol method:
                            Visit(method.ReturnType);

                            foreach (var parameter in method.Parameters)
                            {
                                Visit(parameter.Type);
                            }

                            break;
                        case IPropertySymbol property:
                            Visit(property.Type);
                            break;
                    }
                }
            }

            //  The form the definition is constrained to (IHtmlCollection<IElement>) owns the
            //  interface object, the others share it.
            foreach (var group in found.GroupBy(m => m.OriginalDefinition, SymbolEqualityComparer.Default))
            {
                var definition = _definitions[group.Key!];
                var ordered = group
                    .OrderBy(m => IsConstraintForm(m) ? 0 : 1)
                    .ThenBy(m => m.ToDisplayString(), StringComparer.Ordinal)
                    .ToList();

                foreach (var instance in ordered)
                {
                    var info = new DomType(instance, definition.Names, false, definition.NoInterfaceObject, true, definition.Exposed);
                    _instances[instance] = info;
                    Types.Add(info);
                }
            }

            Types.AddRange(_variants);
        }

        private static Boolean IsConstraintForm(INamedTypeSymbol type)
        {
            var parameters = type.OriginalDefinition.TypeParameters;

            for (var i = 0; i < parameters.Length; i++)
            {
                if (parameters[i].ConstraintTypes.Length != 1 || !SymbolEqualityComparer.Default.Equals(parameters[i].ConstraintTypes[0], type.TypeArguments[i]))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region Lookup

        public DomType? Find(ITypeSymbol? type) =>
            type != null && _instances.TryGetValue(type, out var info) ? info : null;

        /// <summary>
        /// Gets the type a DOM name stands for - this compilation's first, then the one of the
        /// libraries referenced.
        /// </summary>
        public DomType? FindByName(String name) =>
            _ownByName.TryGetValue(name, out var own) && !own.IsGenericDefinition ? own :
            _foreignByName.TryGetValue(name, out var foreign) && !foreign.IsGenericDefinition ? foreign : null;

        public Boolean IsNamespace(String name) => _namespaceNames.Contains(name);

        //  An explicitly re-declared member (IHtmlCollection<T> naming IReadOnlyList<T>'s indexer)
        //  is private; what is invoked is the member it stands for.
        public Boolean IsAccessible(ISymbol symbol) => Compilation.IsSymbolAccessibleWithin(Implemented(symbol), Compilation.Assembly);

        public static ISymbol Implemented(ISymbol member) => member switch
        {
            IMethodSymbol m when m.ExplicitInterfaceImplementations.Length > 0 => m.ExplicitInterfaceImplementations[0],
            IPropertySymbol p when p.ExplicitInterfaceImplementations.Length > 0 => p.ExplicitInterfaceImplementations[0],
            IEventSymbol e when e.ExplicitInterfaceImplementations.Length > 0 => e.ExplicitInterfaceImplementations[0],
            _ => member,
        };

        /// <summary>
        /// The type tree as reflection walks it (GetTypeTree): all interfaces, the type, its bases.
        /// </summary>
        public static List<INamedTypeSymbol> GetTree(INamedTypeSymbol type)
        {
            var tree = new List<INamedTypeSymbol>(type.AllInterfaces);
            tree.Add(type);

            for (var current = type.BaseType; current != null; current = current.BaseType)
            {
                tree.Add(current);
            }

            return tree;
        }

        /// <summary>
        /// Works out the prototype chain of a type: the registered type it inherits from, the
        /// mixins it includes, and the types whose members it declares itself.
        /// </summary>
        public void Analyze(DomType type)
        {
            if (type.Flatten != null)
            {
                return;
            }

            var symbol = type.Symbol;
            var tree = GetTree(symbol);
            var candidates = tree.Where(m => !SymbolEqualityComparer.Default.Equals(m, symbol)).Select(Find).Where(m => m != null).Select(m => m!).ToList();

            if (!type.IsMixin)
            {
                if (symbol.TypeKind == TypeKind.Class)
                {
                    for (var current = symbol.BaseType; current != null && type.Base == null; current = current.BaseType)
                    {
                        var found = Find(current);

                        if (found != null && !found.IsMixin)
                        {
                            type.Base = found;
                        }
                    }
                }

                if (type.Base == null)
                {
                    var bases = candidates.Where(m => !m.IsMixin && m.Symbol.TypeKind == TypeKind.Interface).ToList();
                    var maximal = bases.Where(b => !bases.Any(o => o != b && o.Symbol.AllInterfaces.Contains(b.Symbol, SymbolEqualityComparer.Default))).ToList();
                    type.Base = maximal.FirstOrDefault(m => symbol.Interfaces.Contains(m.Symbol, SymbolEqualityComparer.Default)) ?? maximal.FirstOrDefault();
                }
            }

            var cover = new HashSet<ITypeSymbol>(SymbolEqualityComparer.Default);

            if (type.Base != null)
            {
                cover.Add(type.Base.Symbol);

                foreach (var inherited in GetTree(type.Base.Symbol))
                {
                    cover.Add(inherited);
                }
            }

            if (!type.IsMixin)
            {
                foreach (var mixin in candidates.Where(m => m.IsMixin && !cover.Contains(m.Symbol)).Distinct())
                {
                    type.Mixins.Add(mixin);
                }

                foreach (var mixin in type.Mixins)
                {
                    cover.Add(mixin.Symbol);

                    foreach (var inherited in mixin.Symbol.AllInterfaces)
                    {
                        cover.Add(inherited);
                    }
                }
            }

            type.Flatten = new HashSet<ITypeSymbol>(tree.Where(m => !cover.Contains(m)), SymbolEqualityComparer.Default);
        }

        #endregion

        #region Members

        /// <summary>
        /// Replays DomShapeBuilder for a type: which member each name ends up with, walking the
        /// whole type tree in the same order, with the same rules for overloads and precedence.
        /// </summary>
        public Simulation Simulate(DomType type)
        {
            if (_simulations.TryGetValue(type, out var existing))
            {
                return existing;
            }

            var sim = new Simulation();
            var tree = GetTree(type.Symbol);

            foreach (var current in tree)
            {
                foreach (var property in current.GetMembers().OfType<IPropertySymbol>())
                {
                    if (property.IsStatic)
                    {
                        continue;
                    }

                    //  an indexed accessor needs no name of its own (a dataset's)
                    var names = GetDomNames(property);
                    var accessor = GetAccessor(property);

                    if (names.Count == 0 && (accessor <= 0 || (accessor & (Getter | Setter)) == 0))
                    {
                        continue;
                    }

                    if (accessor == Method)
                    {
                        foreach (var name in names)
                        {
                            SetMethod(sim, name, new Callable(CallableKind.PropertyGetter, property, current, false));
                        }

                        continue;
                    }

                    var isIndexed = accessor > 0 && (accessor & (Getter | Setter)) != 0;

                    if (isIndexed || names.Contains("item"))
                    {
                        SetIndexer(sim, property, current);
                    }

                    if (property.Parameters.Length > 0)
                    {
                        if (property.GetMethod != null)
                        {
                            foreach (var name in names)
                            {
                                SetMethod(sim, name, new Callable(CallableKind.IndexerGetter, property, current, false));
                            }
                        }

                        continue;
                    }

                    foreach (var name in names)
                    {
                        sim.Define(name, new Member(MemberKind.Property, current)
                        {
                            Property = property,
                            ForwardTo = GetString(property, Names.DomPutForwards),
                            SameObject = Has(property, Names.DomSameObject),
                        });
                    }
                }

                foreach (var method in current.GetMembers().OfType<IMethodSymbol>())
                {
                    if (method.IsStatic || method.MethodKind != MethodKind.Ordinary && method.MethodKind != MethodKind.ExplicitInterfaceImplementation)
                    {
                        continue;
                    }

                    foreach (var name in GetDomNames(method))
                    {
                        SetMethod(sim, name, new Callable(CallableKind.Method, method, current, false));
                    }
                }

                foreach (var ev in current.GetMembers().OfType<IEventSymbol>())
                {
                    foreach (var name in GetDomNames(ev))
                    {
                        sim.Define(name, new Member(MemberKind.Event, current) { Event = ev });
                    }
                }

                if (sim.Collection == null)
                {
                    sim.CollectionIndexer ??= current.GetMembers().OfType<IPropertySymbol>().FirstOrDefault(m => IsCollectionIndexer(m));
                    sim.CollectionLength ??= current.GetMembers().OfType<IPropertySymbol>().FirstOrDefault(m => IsLength(m));

                    if (sim.CollectionIndexer != null && sim.CollectionLength != null)
                    {
                        sim.Collection = true;
                    }
                }
            }

            //  Only the extensions of this compilation: another library registers its own. A type
            //  of another library gets them too, which is what this one adds to it.
            SetAddedOverloads(sim, tree);
            SetExtensionMembers(sim, type);

            SetStringifier(sim, type);
            _simulations[type] = sim;
            return sim;
        }

        private static Boolean IsCollectionIndexer(IPropertySymbol property)
        {
            if (property.Parameters.Length != 1 || property.Parameters[0].Type.SpecialType != SpecialType.System_Int32 || property.GetMethod == null)
            {
                return false;
            }

            var accessor = GetAccessor(property);
            return accessor > 0 && (accessor & (Getter | Setter)) != 0 || GetDomNames(property).Contains("item");
        }

        private static Boolean IsLength(IPropertySymbol property) =>
            property.Parameters.Length == 0 && !property.IsStatic && property.Type.SpecialType == SpecialType.System_Int32 && property.GetMethod != null &&
            GetDomNames(property).Contains("length");

        private static void SetIndexer(Simulation sim, IPropertySymbol property, INamedTypeSymbol owner)
        {
            if (property.Parameters.Length != 1)
            {
                return;
            }

            var type = property.Parameters[0].Type.SpecialType;

            if (type == SpecialType.System_Int32)
            {
                if (property.GetMethod != null)
                {
                    sim.NumericIndexer = property;
                }
            }
            else if (type == SpecialType.System_String)
            {
                if (property.GetMethod != null)
                {
                    sim.NamedGetter = property;
                }

                if (property.SetMethod != null)
                {
                    sim.NamedSetter = property;
                }
            }
        }

        private static void SetMethod(Simulation sim, String name, Callable callable)
        {
            if (!sim.IsDeclared(name))
            {
                var member = new Member(MemberKind.Method, callable.Owner);
                member.Overloads.Add(callable);
                sim.Define(name, member);
            }
            else if (sim.Members.TryGetValue(name, out var existing) && existing.Kind == MemberKind.Method &&
                SymbolEqualityComparer.Default.Equals(existing.Owner, callable.Owner))
            {
                //  only within one type: a method further up the tree is hidden, not overloaded
                existing.Overloads.Add(callable);
            }
        }

        //  A library can add overloads to a method of the DOM (e.g. addEventListener with options).
        private void SetAddedOverloads(Simulation sim, List<INamedTypeSymbol> tree)
        {
            var names = new HashSet<String>(tree.SelectMany(GetDomNames), StringComparer.Ordinal);

            foreach (var holder in Holders)
            {
                if (!GetAttributes(holder, Names.DomExposed).Any(m => m.ConstructorArguments.Length > 0 && m.ConstructorArguments[0].Value is String target && names.Contains(target)))
                {
                    continue;
                }

                foreach (var entry in GetExtensions(holder))
                {
                    if (entry.Value.Overloads.Count > 0 && sim.Members.TryGetValue(entry.Key, out var member) && member.Kind == MemberKind.Method)
                    {
                        foreach (var overload in entry.Value.Overloads)
                        {
                            if (!member.Overloads.Any(m => SymbolEqualityComparer.Default.Equals(m.Symbol, overload)))
                            {
                                member.Overloads.Add(new Callable(CallableKind.Method, overload, holder, true));
                                member.Extended = true;
                            }
                        }
                    }
                }
            }
        }

        private void SetExtensionMembers(Simulation sim, DomType type)
        {
            foreach (var holder in GetHoldersOf(type.Name))
            {
                foreach (var entry in GetExtensions(holder))
                {
                    var name = entry.Key;
                    var value = entry.Value;

                    if (sim.IsDeclared(name))
                    {
                        continue;
                    }

                    if (value.Adder != null && value.Remover != null)
                    {
                        sim.Define(name, new Member(MemberKind.ExtensionEvent, holder) { Adder = value.Adder, Remover = value.Remover, FromExtension = true });
                    }
                    else if (value.Getter != null || value.Setter != null)
                    {
                        sim.Define(name, new Member(MemberKind.ExtensionProperty, holder) { Getter = value.Getter, Setter = value.Setter, ForwardTo = value.Forward, FromExtension = true });
                    }
                    else if (value.Overloads.Count > 0)
                    {
                        var member = new Member(MemberKind.Method, holder) { FromExtension = true };

                        foreach (var overload in value.Overloads)
                        {
                            member.Overloads.Add(new Callable(CallableKind.Method, overload, holder, true));
                        }

                        sim.Define(name, member);
                    }
                }
            }
        }

        public IEnumerable<INamedTypeSymbol> GetHoldersOf(String name) =>
            Holders.Where(holder => GetAttributes(holder, Names.DomExposed).Any(m => m.ConstructorArguments.Length > 0 && m.ConstructorArguments[0].Value as String == name));

        /// <summary>
        /// What Extensibility.GetExtensions makes of the extension methods of a static class.
        /// </summary>
        public static Dictionary<String, ExtensionEntry> GetExtensions(INamedTypeSymbol holder)
        {
            var entries = new Dictionary<String, ExtensionEntry>(StringComparer.Ordinal);
            var order = new List<String>();

            foreach (var method in holder.GetMembers().OfType<IMethodSymbol>())
            {
                if (!method.IsExtensionMethod)
                {
                    continue;
                }

                var accessor = GetAccessor(method);
                var forward = GetString(method, Names.DomPutForwards);

                foreach (var name in GetDomNames(method))
                {
                    if (!entries.TryGetValue(name, out var entry))
                    {
                        entry = new ExtensionEntry { Forward = forward };
                        entries.Add(name, entry);
                    }

                    switch (accessor)
                    {
                        case Setter:
                            entry.Setter = method;
                            break;
                        case Getter:
                            entry.Getter = method;
                            break;
                        case Remover:
                            entry.Remover = method;
                            break;
                        case Adder:
                            entry.Adder = method;
                            break;
                        default:
                            entry.Overloads.Add(method);
                            break;
                    }
                }
            }

            return entries;
        }

        //  A WebIDL stringifier has no attribute: its class overrides ToString, which only such do.
        private void SetStringifier(Simulation sim, DomType type)
        {
            if (sim.Members.ContainsKey("toString"))
            {
                return;
            }

            var symbol = type.Symbol.TypeKind == TypeKind.Class ? type.Symbol : null;

            if (symbol == null && type.IsOwn)
            {
                //  the classes an instance of the interface is built from
                symbol = Classes.FirstOrDefault(m => !m.IsAbstract && SymbolEqualityComparer.Default.Equals(Canonicalize(m)?.Symbol, type.Symbol) && OverridesToString(m));
            }

            if (symbol != null && OverridesToString(symbol))
            {
                sim.Define("toString", new Member(MemberKind.Stringifier, type.Symbol));
            }
        }

        private static Boolean OverridesToString(INamedTypeSymbol type)
        {
            for (var current = type; current != null && current.SpecialType != SpecialType.System_Object; current = current.BaseType)
            {
                if (current.GetMembers("ToString").OfType<IMethodSymbol>().Any(m => m.Parameters.Length == 0 && m.IsOverride))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Gets the registered type an object of the class gets the prototype of: the most derived
        /// one it implements, as JsDomSurface.Canonicalize picks it.
        /// </summary>
        public DomType? Canonicalize(INamedTypeSymbol type)
        {
            DomType? best = null;

            foreach (var candidate in GetTree(type).Select(Find))
            {
                if (candidate != null && !candidate.IsMixin &&
                    (best == null || IsAssignable(candidate.Symbol, best.Symbol) && !SymbolEqualityComparer.Default.Equals(best.Symbol, candidate.Symbol)))
                {
                    best = candidate;
                }
            }

            return best;
        }

        /// <summary>
        /// Gets if a value of the first type can be used as one of the second.
        /// </summary>
        public static Boolean IsAssignable(ITypeSymbol from, ITypeSymbol to) =>
            SymbolEqualityComparer.Default.Equals(from, to) ||
            from is INamedTypeSymbol named && GetTree(named).Contains(to, SymbolEqualityComparer.Default);

        /// <summary>
        /// Gets the members a type has to declare itself: those its base prototype and mixins do
        /// not give it already, the same test DomShapeBuilder applies to the prototype above.
        /// </summary>
        public List<KeyValuePair<String, Member>> GetOwnMembers(DomType type)
        {
            Analyze(type);
            var sim = Simulate(type);
            var baseSim = type.Base != null ? Simulate(type.Base) : null;
            var mixinSims = type.Mixins.Select(Simulate).ToList();
            var result = new List<KeyValuePair<String, Member>>();

            foreach (var name in sim.Order)
            {
                if (String.Equals(name, "constructor", StringComparison.Ordinal))
                {
                    continue;
                }

                var member = sim.Members[name];
                Member? provided = null;

                foreach (var mixinSim in mixinSims)
                {
                    if (mixinSim.Members.TryGetValue(name, out var fromMixin))
                    {
                        provided = fromMixin;
                        break;
                    }
                }

                if (provided == null && baseSim != null)
                {
                    baseSim.Members.TryGetValue(name, out provided);
                }

                if (provided == null || !member.IsSameAs(provided))
                {
                    result.Add(new KeyValuePair<String, Member>(name, member));
                }
            }

            return result;
        }

        #endregion

        #region Statics

        /// <summary>
        /// What CreatorCache.GetStaticMembers finds: the static members of the type and of its
        /// base classes, a derived class hiding one of the same name.
        /// </summary>
        public List<StaticMember> GetStaticMembers(INamedTypeSymbol type)
        {
            var members = new List<StaticMember>();
            var names = new HashSet<String>(StringComparer.Ordinal);

            for (var current = type; current != null; current = current.BaseType)
            {
                var found = new List<StaticMember>();

                StaticMember Get(String name)
                {
                    var member = found.FirstOrDefault(m => m.Name == name);

                    if (member == null)
                    {
                        member = new StaticMember(name);
                        found.Add(member);
                    }

                    return member;
                }

                foreach (var method in current.GetMembers().OfType<IMethodSymbol>())
                {
                    if (!method.IsStatic || method.MethodKind != MethodKind.Ordinary || method.DeclaredAccessibility != Accessibility.Public)
                    {
                        continue;
                    }

                    if (method.IsExtensionMethod && !TakesWindow(method))
                    {
                        continue;
                    }

                    var accessor = GetAccessor(method);

                    foreach (var name in GetDomNames(method))
                    {
                        var member = Get(name);

                        if (accessor == Getter)
                        {
                            member.Getter = method;
                        }
                        else if (accessor == Setter)
                        {
                            member.Setter = method;
                        }
                        else
                        {
                            member.Overloads.Add(method);
                        }
                    }
                }

                foreach (var property in current.GetMembers().OfType<IPropertySymbol>())
                {
                    if (!property.IsStatic || property.DeclaredAccessibility != Accessibility.Public)
                    {
                        continue;
                    }

                    foreach (var name in GetDomNames(property))
                    {
                        found.RemoveAll(m => m.Name == name);
                        found.Add(new StaticMember(name) { Property = property });
                    }
                }

                foreach (var member in found)
                {
                    if (names.Add(member.Name))
                    {
                        members.Add(member);
                    }
                }
            }

            return members;
        }

        public Boolean TakesWindow(IMethodSymbol method) =>
            method.Parameters.Length > 0 && Window != null && SymbolEqualityComparer.Default.Equals(method.Parameters[0].Type, Window);

        #endregion

        #region Constants

        /// <summary>
        /// Splits the own enums into WebIDL enumerations, standalone constant objects (NodeFilter)
        /// and constants of an interface or namespace (Node.ELEMENT_NODE), as reflection does.
        /// </summary>
        public void ClassifyEnums(List<INamedTypeSymbol> literals, List<KeyValuePair<String, INamedTypeSymbol>> standalone, List<KeyValuePair<String, KeyValuePair<String, Double>>> owned)
        {
            var typeNames = new HashSet<String>(StringComparer.OrdinalIgnoreCase);

            foreach (var type in GetAllTypes(Compilation.Assembly.GlobalNamespace))
            {
                if (type.TypeKind != TypeKind.Enum && type.DeclaredAccessibility == Accessibility.Public)
                {
                    var name = GetDomNames(type).FirstOrDefault();

                    if (name != null)
                    {
                        typeNames.Add(name);
                    }
                }
            }

            foreach (var type in Enums)
            {
                if (Has(type, Names.DomLiterals) || type.ToDisplayString() == Names.DocumentReadyState)
                {
                    literals.Add(type);
                    continue;
                }

                var name = GetDomNames(type)[0];

                if (!typeNames.Contains(name))
                {
                    standalone.Add(new KeyValuePair<String, INamedTypeSymbol>(name, type));
                }

                var owner = ConstantOwners.TryGetValue(type.ToDisplayString(), out var known) ? known : name;

                foreach (var field in type.GetMembers().OfType<IFieldSymbol>().Where(m => m.HasConstantValue))
                {
                    var constant = GetDomNames(field).FirstOrDefault();

                    if (constant != null && IsConstantName(constant) &&
                        !owned.Any(m => m.Key == owner && m.Value.Key == constant))
                    {
                        owned.Add(new KeyValuePair<String, KeyValuePair<String, Double>>(owner, new KeyValuePair<String, Double>(constant, Convert.ToDouble(field.ConstantValue))));
                    }
                }
            }
        }

        private static Boolean IsConstantName(String name) =>
            name.Length > 0 && Char.IsUpper(name[0]) && name.All(c => Char.IsUpper(c) || Char.IsDigit(c) || c == '_');

        #endregion
    }

    sealed class DomType
    {
        public DomType(INamedTypeSymbol symbol, List<String> names, Boolean isMixin, Boolean noInterfaceObject, Boolean isOwn, List<String> exposed)
        {
            Symbol = symbol;
            Names = names;
            IsMixin = isMixin;
            NoInterfaceObject = noInterfaceObject;
            IsOwn = isOwn;
            Exposed = exposed;
        }

        public INamedTypeSymbol Symbol { get; }

        public String Name => Names[0];

        public List<String> Names { get; }

        public Boolean IsMixin { get; }

        public Boolean NoInterfaceObject { get; }

        public Boolean IsOwn { get; }

        public List<String> Exposed { get; }

        public Boolean IsVariant { get; set; }

        public Boolean IsGenericDefinition => Symbol.IsGenericType && SymbolEqualityComparer.Default.Equals(Symbol, Symbol.OriginalDefinition);

        public DomType? Base { get; set; }

        public List<DomType> Mixins { get; } = new List<DomType>();

        public HashSet<ITypeSymbol>? Flatten { get; set; }

        public override String ToString() => Symbol.ToDisplayString();
    }

    enum MemberKind
    {
        Property,
        Method,
        Event,
        ExtensionProperty,
        ExtensionEvent,
        Stringifier,
    }

    enum CallableKind
    {
        Method,
        PropertyGetter,
        IndexerGetter,
    }

    sealed class Callable
    {
        public Callable(CallableKind kind, ISymbol symbol, INamedTypeSymbol owner, Boolean isExtension)
        {
            Kind = kind;
            Symbol = symbol;
            Owner = owner;
            IsExtension = isExtension;
        }

        public CallableKind Kind { get; }

        public ISymbol Symbol { get; }

        public INamedTypeSymbol Owner { get; }

        public Boolean IsExtension { get; }
    }

    sealed class Member
    {
        public Member(MemberKind kind, INamedTypeSymbol owner)
        {
            Kind = kind;
            Owner = owner;
        }

        public MemberKind Kind { get; }

        public INamedTypeSymbol Owner { get; }

        public List<Callable> Overloads { get; } = new List<Callable>();

        public IPropertySymbol? Property { get; set; }

        public IEventSymbol? Event { get; set; }

        public IMethodSymbol? Getter { get; set; }

        public IMethodSymbol? Setter { get; set; }

        public IMethodSymbol? Adder { get; set; }

        public IMethodSymbol? Remover { get; set; }

        public String? ForwardTo { get; set; }

        public Boolean SameObject { get; set; }

        public Boolean FromExtension { get; set; }

        public Boolean Extended { get; set; }

        public Boolean IsSameAs(Member other)
        {
            var eq = SymbolEqualityComparer.Default;

            if (Kind != other.Kind || ForwardTo != other.ForwardTo || SameObject != other.SameObject)
            {
                return false;
            }

            switch (Kind)
            {
                case MemberKind.Method:
                    return Overloads.Count == other.Overloads.Count && Overloads.Zip(other.Overloads, (a, b) => eq.Equals(a.Symbol, b.Symbol) && a.Kind == b.Kind).All(m => m);
                case MemberKind.Property:
                    return eq.Equals(Property, other.Property);
                case MemberKind.Event:
                    return eq.Equals(Event, other.Event);
                case MemberKind.ExtensionProperty:
                    return eq.Equals(Getter, other.Getter) && eq.Equals(Setter, other.Setter);
                case MemberKind.ExtensionEvent:
                    return eq.Equals(Adder, other.Adder) && eq.Equals(Remover, other.Remover);
                default:
                    return false;
            }
        }
    }

    sealed class Simulation
    {
        public List<String> Order { get; } = new List<String>();

        public Dictionary<String, Member> Members { get; } = new Dictionary<String, Member>(StringComparer.Ordinal);

        public IPropertySymbol? NumericIndexer { get; set; }

        public IPropertySymbol? NamedGetter { get; set; }

        public IPropertySymbol? NamedSetter { get; set; }

        public Boolean? Collection { get; set; }

        public IPropertySymbol? CollectionIndexer { get; set; }

        public IPropertySymbol? CollectionLength { get; set; }

        public Boolean IsDeclared(String name) => Members.ContainsKey(name) || DomModelReserved.Names.Contains(name);

        //  a member replacing one of the same name keeps its position
        public void Define(String name, Member member)
        {
            if (!Members.ContainsKey(name))
            {
                Order.Add(name);
            }

            Members[name] = member;
        }
    }

    //  What Jint's Object.prototype carries: a method of such a name was never registered.
    static class DomModelReserved
    {
        public static readonly HashSet<String> Names = new HashSet<String>(StringComparer.Ordinal)
        {
            "constructor", "__defineGetter__", "__defineSetter__", "hasOwnProperty", "__lookupGetter__",
            "__lookupSetter__", "isPrototypeOf", "propertyIsEnumerable", "toString", "valueOf",
            "__proto__", "toLocaleString",
        };
    }

    sealed class ExtensionEntry
    {
        public IMethodSymbol? Getter;
        public IMethodSymbol? Setter;
        public IMethodSymbol? Adder;
        public IMethodSymbol? Remover;
        public String? Forward;
        public List<IMethodSymbol> Overloads = new List<IMethodSymbol>();
    }

    sealed class StaticMember
    {
        public StaticMember(String name)
        {
            Name = name;
        }

        public String Name { get; }

        public IMethodSymbol? Getter { get; set; }

        public IMethodSymbol? Setter { get; set; }

        public IPropertySymbol? Property { get; set; }

        public List<IMethodSymbol> Overloads { get; } = new List<IMethodSymbol>();
    }
}
