namespace AngleSharp.Html.Construction;

/// <summary>
/// Represents a constructable document that is a DOM-shaped node and participates in a browsing
/// host's script and load lifecycle. This is the union of the three facets a document can have
/// during tree construction — <see cref="IConstructableDocumentState"/>,
/// <see cref="IConstructableDocumentNode"/> and <see cref="IConstructableDocumentHost"/> — and
/// exposes exactly the members it always has. Backends that only need some of those facets should
/// implement them directly rather than this interface.
/// </summary>
public interface IConstructableDocument : IConstructableDocumentNode, IConstructableDocumentHost;
