namespace AngleSharp.Scripting
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Takes the DOM surface a <see cref="DomRegistration"/> exposes to script. Implemented by a
    /// scripting engine.
    /// </summary>
    /// <remarks>
    /// The API is shaped for a source generator rather than for hand-written code: everything is
    /// a strongly typed delegate, so nothing the engine does with a registered member needs
    /// reflection, and what is registered is exactly what the trimmer keeps. It covers what the
    /// DOM attributes describe, so a generator can translate those one to one.
    /// The declarations are abstract classes rather than interfaces on purpose: an engine built
    /// against an older AngleSharp keeps working with a newer one, as a member added later comes
    /// with a default implementation instead of leaving the engine's class incomplete.
    /// </remarks>
    public abstract class DomRegistry
    {
        /// <summary>
        /// Declares a DOM interface, or returns the declaration made of it before, so that a later
        /// registration (e.g. of another library) can add members to it. Its interface object is
        /// published on the window under the name.
        /// </summary>
        /// <remarks>
        /// Several CLR types may be declared under the same name, e.g. the closed forms of
        /// IHtmlCollection&lt;T&gt; all standing for HTMLCollection. They share the interface object
        /// of the first one: it is published once, its constants, static members and constructors
        /// are the ones of all of them, and an instance of any of them is an instance of it. Each
        /// keeps a prototype of its own, as its members are invoked on its own CLR type.
        /// </remarks>
        /// <typeparam name="T">
        /// The CLR type whose instances the prototype is for - usually the exported interface,
        /// e.g. IElement. An object gets the prototype of the most derived type it implements.
        /// </typeparam>
        /// <param name="name">The DOM name, e.g. "Element".</param>
        /// <returns>The declaration to add members to.</returns>
        public abstract DomTypeRegistration<T> Type<T>(String name) where T : class;

        /// <summary>
        /// Declares an interface mixin (e.g. ParentNode), whose members are added to every type
        /// including it. Its interface object is published like the one of a type, unless
        /// declared <see cref="DomTypeRegistration{T}.WithoutInterfaceObject"/> as browsers have
        /// it, but an object never gets its prototype: a mixin is not a link of the prototype chain.
        /// </summary>
        /// <typeparam name="T">The CLR type the members are invoked on, e.g. IParentNode.</typeparam>
        /// <param name="name">The DOM name, e.g. "ParentNode".</param>
        /// <returns>The declaration to add members to.</returns>
        public abstract DomTypeRegistration<T> Mixin<T>(String name) where T : class;

        /// <summary>
        /// Declares an object of constants published on the window, as for a numeric DOM enum.
        /// Named like an interface, the constants are added to its interface object instead
        /// (e.g. Node.ELEMENT_NODE).
        /// </summary>
        /// <param name="name">The name the object is published under.</param>
        /// <returns>The declaration to add constants to.</returns>
        public abstract DomConstantsRegistration Constants(String name);

        /// <summary>
        /// Declares a WebIDL enumeration: values of the enum are given to script as the
        /// registered strings, and the strings are accepted for the enum.
        /// </summary>
        /// <typeparam name="TEnum">The CLR enum.</typeparam>
        /// <returns>The declaration to add the literals to.</returns>
        public abstract DomLiteralsRegistration<TEnum> Literals<TEnum>() where TEnum : struct, Enum;

        /// <summary>
        /// Declares a WebIDL namespace (e.g. CSS): a plain object published on the window, which
        /// holds static operations, attributes and constants, and the interface objects of the
        /// types declared <see cref="DomTypeRegistration{T}.InNamespace(String)"/> it, but is no
        /// constructor and has no prototype of its own.
        /// </summary>
        /// <param name="name">The name the namespace is published under.</param>
        /// <returns>The declaration to add members to.</returns>
        public abstract DomNamespaceRegistration Namespace(String name);

        /// <summary>
        /// Declares a named constructor (e.g. Image or Option), which is another way of creating
        /// a registered type rather than a type of its own.
        /// </summary>
        /// <typeparam name="T">The registered type it creates.</typeparam>
        /// <param name="name">The name the constructor is published under.</param>
        /// <param name="create">Creates the object from the arguments script passed.</param>
        /// <param name="length">The number of required arguments, as Function.length reports.</param>
        public abstract void ConstructorFunction<T>(String name, Func<DomArguments, T> create, Int32 length = 0) where T : class;

        /// <summary>
        /// Declares an object every window gets one of under the given name.
        /// </summary>
        /// <param name="name">The name the object is published under.</param>
        /// <param name="create">Creates the object for a window.</param>
        public abstract void Instance(String name, Func<IWindow, Object?> create);
    }

    /// <summary>
    /// The declaration of one DOM interface or mixin.
    /// </summary>
    /// <remarks>
    /// Declaring a member again under the same name replaces the earlier declaration, so a
    /// later library wins. The exception are declarations carrying a <see cref="DomSignature"/>:
    /// those of the same name accumulate as overloads, picked per call by WebIDL's overload
    /// resolution.
    /// </remarks>
    /// <typeparam name="T">The CLR type the members are invoked on.</typeparam>
    public abstract class DomTypeRegistration<T> where T : class
    {
        /// <summary>
        /// Chains the prototype of this type to the one of the given type, which also has to be
        /// registered, as Element.prototype is chained to Node.prototype.
        /// </summary>
        /// <typeparam name="TBase">The registered type to inherit from.</typeparam>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Inherits<TBase>() where TBase : class;

        /// <summary>
        /// Adds the members of a registered mixin to this type. Members of the type itself take
        /// precedence over those of the mixin.
        /// </summary>
        /// <typeparam name="TMixin">The registered mixin.</typeparam>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Includes<TMixin>() where TMixin : class;

        /// <summary>
        /// Publishes no interface object on the window ([LegacyNoInterfaceObject]), so script cannot
        /// name the type, and the prototype carries no "constructor". The type can then have
        /// neither an alias, a constructor nor static members; its constants stay on the prototype.
        /// </summary>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> WithoutInterfaceObject();

        /// <summary>
        /// Publishes the interface object on the object of a registered namespace instead of the
        /// window ([LegacyNamespace]), as WebAssembly.Module is.
        /// </summary>
        /// <param name="name">The name of the namespace.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> InNamespace(String name);

        /// <summary>
        /// Publishes the interface object under another name as well, e.g. a legacy one.
        /// </summary>
        /// <param name="name">The additional name.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Alias(String name);

        /// <summary>
        /// Makes the interface object constructible with "new". Without a constructor, it throws
        /// "Illegal constructor", as HTMLElement does.
        /// </summary>
        /// <param name="create">Creates the object from the arguments script passed.</param>
        /// <param name="length">The number of required arguments, as Function.length reports.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Constructor(Func<DomArguments, T> create, Int32 length = 0);

        /// <summary>
        /// Adds a constructor overload.
        /// </summary>
        /// <param name="create">Creates the object from the arguments script passed.</param>
        /// <param name="signature">What the overload takes, which picks it for a call.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Constructor(Func<DomArguments, T> create, DomSignature signature);

        /// <summary>
        /// Declares an attribute, which is read-only without a setter.
        /// </summary>
        /// <typeparam name="TValue">The type of the attribute.</typeparam>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="getter">Reads the attribute.</param>
        /// <param name="setter">Writes the attribute, if it can be written.</param>
        /// <param name="flags">The WebIDL extended attributes of the attribute.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Property<TValue>(String name, Func<T, TValue> getter, Action<T, TValue>? setter = null, DomPropertyFlags flags = DomPropertyFlags.None);

        /// <summary>
        /// Declares a read-only attribute an assignment to which is forwarded to an attribute of
        /// its value ([PutForwards]), as element.style = '...' sets element.style.cssText.
        /// </summary>
        /// <typeparam name="TValue">The type of the attribute.</typeparam>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="getter">Reads the attribute.</param>
        /// <param name="forwardTo">The DOM name of the attribute of the value to assign.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> ForwardingProperty<TValue>(String name, Func<T, TValue> getter, String forwardTo);

        /// <summary>
        /// Declares an operation giving a result.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="length">The number of required arguments, as Function.length reports.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Method(String name, Func<T, DomArguments, Object?> invoke, Int32 length = 0);

        /// <summary>
        /// Declares an operation giving undefined.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="length">The number of required arguments, as Function.length reports.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Method(String name, Action<T, DomArguments> invoke, Int32 length = 0);

        /// <summary>
        /// Adds an overload of an operation giving a result.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="signature">What the overload takes, which picks it for a call.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Method(String name, Func<T, DomArguments, Object?> invoke, DomSignature signature);

        /// <summary>
        /// Adds an overload of an operation giving undefined.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="signature">What the overload takes, which picks it for a call.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Method(String name, Action<T, DomArguments> invoke, DomSignature signature);

        /// <summary>
        /// Declares an event handler attribute (e.g. onclick): assigning a function subscribes
        /// it, assigning another one or null unsubscribes it again.
        /// </summary>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="add">Subscribes the handler.</param>
        /// <param name="remove">Unsubscribes the handler.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> EventHandler(String name, Action<T, DomEventHandler> add, Action<T, DomEventHandler> remove);

        /// <summary>
        /// Declares a constant, which is found on the interface object and on the prototype.
        /// </summary>
        /// <param name="name">The DOM name of the constant.</param>
        /// <param name="value">The value of the constant.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Constant(String name, Double value);

        /// <summary>
        /// Declares a static operation, which is found on the interface object.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="length">The number of required arguments, as Function.length reports.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Static(String name, Func<DomArguments, Object?> invoke, Int32 length = 0);

        /// <summary>
        /// Adds an overload of a static operation.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="signature">What the overload takes, which picks it for a call.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Static(String name, Func<DomArguments, Object?> invoke, DomSignature signature);

        /// <summary>
        /// Declares a static attribute, which is found on the interface object.
        /// </summary>
        /// <typeparam name="TValue">The type of the attribute.</typeparam>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="getter">Reads the attribute.</param>
        /// <param name="setter">Writes the attribute, if it can be written.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> StaticProperty<TValue>(String name, Func<TValue> getter, Action<TValue>? setter = null);

        /// <summary>
        /// Declares a static attribute reading (and writing) the window of the script, as one of
        /// the window's settings, e.g. Notification.permission.
        /// </summary>
        /// <typeparam name="TValue">The type of the attribute.</typeparam>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="getter">Reads the attribute for the window.</param>
        /// <param name="setter">Writes the attribute for the window, if it can be written.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> StaticProperty<TValue>(String name, Func<IWindow, TValue> getter, Action<IWindow, TValue>? setter = null);

        /// <summary>
        /// Declares an indexed property getter: obj[0] reads it. An index out of range is told
        /// by throwing an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="getter">Reads the item at the index.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Indexer(Func<T, Int32, Object?> getter);

        /// <summary>
        /// Declares the type an array-like collection (e.g. NodeList): indices and length are
        /// read from these two, and the object is iterable.
        /// </summary>
        /// <param name="length">Gets the number of items.</param>
        /// <param name="item">Gets the item at an index below the length.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> Collection(Func<T, Int32> length, Func<T, Int32, Object?> item);

        /// <summary>
        /// Declares a named property getter and, optionally, setter: obj.foo reads a name that is
        /// no member of the prototype through it. A null result means there is no such entry.
        /// For an <see cref="IStringMap"/> (a dataset) the camel-cased property name is turned
        /// into the dashed attribute name first.
        /// </summary>
        /// <typeparam name="TValue">The type of an entry.</typeparam>
        /// <param name="getter">Reads the entry of the name.</param>
        /// <param name="setter">Writes the entry of the name, if entries can be written.</param>
        /// <returns>The declaration.</returns>
        public abstract DomTypeRegistration<T> NamedIndexer<TValue>(Func<T, String, TValue> getter, Action<T, String, TValue>? setter = null);
    }

    /// <summary>
    /// The declaration of a WebIDL namespace.
    /// </summary>
    public abstract class DomNamespaceRegistration
    {
        /// <summary>
        /// Declares an operation of the namespace.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="length">The number of required arguments, as Function.length reports.</param>
        /// <returns>The declaration.</returns>
        public abstract DomNamespaceRegistration Static(String name, Func<DomArguments, Object?> invoke, Int32 length = 0);

        /// <summary>
        /// Adds an overload of an operation of the namespace.
        /// </summary>
        /// <param name="name">The DOM name of the operation.</param>
        /// <param name="invoke">Runs the operation with the arguments script passed.</param>
        /// <param name="signature">What the overload takes, which picks it for a call.</param>
        /// <returns>The declaration.</returns>
        public abstract DomNamespaceRegistration Static(String name, Func<DomArguments, Object?> invoke, DomSignature signature);

        /// <summary>
        /// Declares an attribute of the namespace.
        /// </summary>
        /// <typeparam name="TValue">The type of the attribute.</typeparam>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="getter">Reads the attribute.</param>
        /// <param name="setter">Writes the attribute, if it can be written.</param>
        /// <returns>The declaration.</returns>
        public abstract DomNamespaceRegistration StaticProperty<TValue>(String name, Func<TValue> getter, Action<TValue>? setter = null);

        /// <summary>
        /// Declares a static attribute reading (and writing) the window of the script, as one of
        /// the window's settings, e.g. Notification.permission.
        /// </summary>
        /// <typeparam name="TValue">The type of the attribute.</typeparam>
        /// <param name="name">The DOM name of the attribute.</param>
        /// <param name="getter">Reads the attribute for the window.</param>
        /// <param name="setter">Writes the attribute for the window, if it can be written.</param>
        /// <returns>The declaration.</returns>
        public abstract DomNamespaceRegistration StaticProperty<TValue>(String name, Func<IWindow, TValue> getter, Action<IWindow, TValue>? setter = null);

        /// <summary>
        /// Declares a constant of the namespace.
        /// </summary>
        /// <param name="name">The name of the constant.</param>
        /// <param name="value">The value of the constant.</param>
        /// <returns>The declaration.</returns>
        public abstract DomNamespaceRegistration Constant(String name, Double value);
    }

    /// <summary>
    /// The declaration of an object of constants.
    /// </summary>
    public abstract class DomConstantsRegistration
    {
        /// <summary>
        /// Declares a constant.
        /// </summary>
        /// <param name="name">The name of the constant.</param>
        /// <param name="value">The value of the constant.</param>
        /// <returns>The declaration.</returns>
        public abstract DomConstantsRegistration Constant(String name, Double value);
    }

    /// <summary>
    /// The declaration of a WebIDL enumeration.
    /// </summary>
    /// <typeparam name="TEnum">The CLR enum.</typeparam>
    public abstract class DomLiteralsRegistration<TEnum> where TEnum : struct, Enum
    {
        /// <summary>
        /// Declares the string standing for a value of the enum.
        /// </summary>
        /// <param name="value">The value of the enum.</param>
        /// <param name="literal">The string script sees and passes.</param>
        /// <returns>The declaration.</returns>
        public abstract DomLiteralsRegistration<TEnum> Literal(TEnum value, String literal);
    }

    /// <summary>
    /// The WebIDL extended attributes of a registered attribute.
    /// </summary>
    [Flags]
    public enum DomPropertyFlags
    {
        /// <summary>
        /// An ordinary attribute.
        /// </summary>
        None = 0,
        /// <summary>
        /// [SameObject]: script gets the same object each time, even if the CLR getter creates a
        /// new one per read.
        /// </summary>
        SameObject = 1,
    }
}
