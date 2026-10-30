#if !NET5_0_OR_GREATER
namespace System.Diagnostics.CodeAnalysis
{
    // The trimmer matches this by name, so this internal copy lets the obsolete Object overloads
    // carry their warning on the targets whose BCL predates it.

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Constructor | AttributeTargets.Class, Inherited = false)]
    internal sealed class RequiresUnreferencedCodeAttribute : Attribute
    {
        public RequiresUnreferencedCodeAttribute(String message)
        {
            Message = message;
        }

        public String Message { get; }

        public String? Url { get; set; }
    }
}
#endif
