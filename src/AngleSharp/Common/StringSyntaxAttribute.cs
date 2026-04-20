#if !NET7_0_OR_GREATER
namespace System.Diagnostics.CodeAnalysis;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Field | AttributeTargets.Property)]
internal sealed class StringSyntaxAttribute : Attribute
{
    public const String Regex = nameof(Regex);
    public const String Uri = nameof(Uri);
    public const String Json = nameof(Json);
    public const String Xml = nameof(Xml);
    public const String CompositeFormat = nameof(CompositeFormat);

    public StringSyntaxAttribute(String syntax)
    {
        Syntax = syntax;
        Arguments = Array.Empty<Object?>();
    }

    public StringSyntaxAttribute(String syntax, params Object?[] arguments)
    {
        Syntax = syntax;
        Arguments = arguments;
    }

    public String Syntax { get; }

    public Object?[] Arguments { get; }
}
#endif
