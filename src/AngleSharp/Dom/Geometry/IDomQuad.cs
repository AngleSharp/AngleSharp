
namespace AngleSharp.Dom.Geometry;

/// <summary>
/// Represents the IDomQuad interface.
/// </summary>
public interface IDomQuad
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    IDomPoint P1 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    IDomPoint P2 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    IDomPoint P3 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    IDomPoint P4 { get; }

    /// <summary>
    /// Executes GetBounds and returns a value.
    /// </summary>
    IDomRect GetBounds();
}
