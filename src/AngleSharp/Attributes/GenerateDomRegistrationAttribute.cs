namespace AngleSharp.Attributes
{
    using System;

    /// <summary>
    /// Asks the generator shipped with AngleSharp to turn the DOM attributes of this assembly into
    /// a <see cref="AngleSharp.Scripting.DomRegistration"/>: a static class of the given name with a
    /// <c>Register(DomRegistry registry)</c> method, which declares the DOM types, members and
    /// constants the assembly exposes - and the members it adds to the DOM of others
    /// (<see cref="DomExposedAttribute"/>) - without any reflection at run time.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class GenerateDomRegistrationAttribute : Attribute
    {
        /// <summary>
        /// Requests the registration to be generated.
        /// </summary>
        /// <param name="typeName">
        /// The full name of the static class to generate, e.g. "AngleSharp.Css.CssDom".
        /// </param>
        public GenerateDomRegistrationAttribute(String typeName)
        {
            TypeName = typeName;
        }

        /// <summary>
        /// Gets the full name of the static class to generate.
        /// </summary>
        public String TypeName { get; }
    }
}
