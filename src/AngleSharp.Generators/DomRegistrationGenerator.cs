namespace AngleSharp.Generators
{
    using Microsoft.CodeAnalysis;
    using System;

    /// <summary>
    /// Turns the DOM attributes of an assembly asking for it ([assembly: GenerateDomRegistration])
    /// into a static class whose Register method declares that DOM on a DomRegistry.
    /// </summary>
    /// <remarks>
    /// The generator ships with AngleSharp, so every consumer's compiler loads it; a compilation
    /// without the attribute is left after one look at its assembly attributes. The output follows
    /// what AngleSharp.Js derives by reflection, rule for rule (see DomModel), so that a registered
    /// DOM and a reflected one behave alike.
    /// </remarks>
    [Generator(LanguageNames.CSharp)]
    public sealed class DomRegistrationGenerator : IIncrementalGenerator
    {
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            context.RegisterSourceOutput(context.CompilationProvider, static (spc, compilation) => Execute(spc, compilation));
        }

        private static void Execute(SourceProductionContext context, Compilation compilation)
        {
            var target = GetTargetTypeName(compilation);

            if (target == null)
            {
                return;
            }

            var model = new DomModel(compilation, context.ReportDiagnostic, context.CancellationToken);
            var source = new DomEmitter(model).Emit(target);
            context.AddSource(target + ".g.cs", source);
        }

        private static String? GetTargetTypeName(Compilation compilation)
        {
            foreach (var attribute in compilation.Assembly.GetAttributes())
            {
                if (attribute.AttributeClass?.ToDisplayString() == Names.GenerateDomRegistration &&
                    attribute.ConstructorArguments.Length == 1 &&
                    attribute.ConstructorArguments[0].Value is String name &&
                    name.Length > 0)
                {
                    return name;
                }
            }

            return null;
        }
    }

    static class Names
    {
        public const String GenerateDomRegistration = "AngleSharp.Attributes.GenerateDomRegistrationAttribute";
        public const String DomName = "AngleSharp.Attributes.DomNameAttribute";
        public const String DomAccessor = "AngleSharp.Attributes.DomAccessorAttribute";
        public const String DomConstructor = "AngleSharp.Attributes.DomConstructorAttribute";
        public const String DomExposed = "AngleSharp.Attributes.DomExposedAttribute";
        public const String DomInitDict = "AngleSharp.Attributes.DomInitDictAttribute";
        public const String DomInstance = "AngleSharp.Attributes.DomInstanceAttribute";
        public const String DomLiterals = "AngleSharp.Attributes.DomLiteralsAttribute";
        public const String DomNoInterfaceObject = "AngleSharp.Attributes.DomNoInterfaceObjectAttribute";
        public const String DomPutForwards = "AngleSharp.Attributes.DomPutForwardsAttribute";
        public const String DomReturnType = "AngleSharp.Attributes.DomReturnTypeAttribute";
        public const String DomSameObject = "AngleSharp.Attributes.DomSameObjectAttribute";
        public const String DomConstructorFunction = "AngleSharp.Js.Attributes.DomConstructorFunctionAttribute";
        public const String IWindow = "AngleSharp.Dom.IWindow";
        public const String INode = "AngleSharp.Dom.INode";
        public const String DomEventHandler = "AngleSharp.Dom.DomEventHandler";
        public const String DocumentReadyState = "AngleSharp.Dom.DocumentReadyState";
        public const String Obsolete = "System.ObsoleteAttribute";
        public const String Task = "System.Threading.Tasks.Task";
    }

    static class Diagnostics
    {
        private const String Category = "AngleSharp.DomRegistration";

        /// <summary>
        /// A DOM member the registration cannot express, which scripts will therefore not see.
        /// </summary>
        public static readonly DiagnosticDescriptor Skipped = new DiagnosticDescriptor(
            "ASDOM001",
            "DOM member not registered",
            "The DOM member '{0}' of '{1}' is not registered: {2}",
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        /// <summary>
        /// A member added to the DOM of another library under a name that library already has,
        /// which a registration would replace while reflection keeps the library's own (rule 7).
        /// </summary>
        public static readonly DiagnosticDescriptor Collision = new DiagnosticDescriptor(
            "ASDOM002",
            "Added DOM member shadows an existing one",
            "The member '{0}' added to '{1}' has the name of a member '{1}' declares already, so it is not registered",
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);

        /// <summary>
        /// A DomExposed target, base or namespace that names nothing the registration knows.
        /// </summary>
        public static readonly DiagnosticDescriptor Unresolved = new DiagnosticDescriptor(
            "ASDOM003",
            "DOM name not found",
            "'{0}' refers to the DOM name '{1}', which no accessible registered type carries",
            Category,
            DiagnosticSeverity.Warning,
            isEnabledByDefault: true);
    }
}
