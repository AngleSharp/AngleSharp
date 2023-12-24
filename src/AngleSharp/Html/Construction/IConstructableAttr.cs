namespace AngleSharp.Html.Construction;

using Common;

/// <summary>
/// Represents a constructable attribute.
/// </summary>
public interface IConstructableAttr
{
    /// <summary>
    /// Name of the attribute.
    /// </summary>
    StringOrMemory Name { get; }

    /// <summary>
    /// Value of the attribute.
    /// </summary>
    StringOrMemory Value { get; set; }
};