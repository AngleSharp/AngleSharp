using System;

namespace AngleSharp.Html.Parser.Utf8;

public readonly ref struct Utf8DoctypeToken(
    ReadOnlySpan<Byte> name,
    ReadOnlySpan<Byte> publicIdentifier,
    Boolean isPublicIdentifierMissing,
    ReadOnlySpan<Byte> systemIdentifier,
    Boolean isSystemIdentifierMissing,
    Boolean isQuirksForced
)
{
    public ReadOnlySpan<Byte> Name { get; } = name;
    public ReadOnlySpan<Byte> PublicIdentifier { get; } = publicIdentifier;
    public Boolean IsPublicIdentifierMissing { get; } = isPublicIdentifierMissing;
    public ReadOnlySpan<Byte> SystemIdentifier { get; } = systemIdentifier;
    public Boolean IsSystemIdentifierMissing { get; } = isSystemIdentifierMissing;
    public Boolean IsQuirksForced { get; } = isQuirksForced;
}
