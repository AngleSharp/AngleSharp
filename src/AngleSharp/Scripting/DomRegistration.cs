namespace AngleSharp.Scripting
{
    using System;

    /// <summary>
    /// Describes the DOM surface a scripting engine exposes to script, in place of discovering it
    /// by reflecting over the DOM attributes.
    /// </summary>
    /// <remarks>
    /// Given one, an engine exposes exactly what it registers: no assembly is scanned and no
    /// attribute is read, which is what makes the surface known to the trimmer and usable under
    /// native AoT. Each library ships the registration of its own DOM, so it always matches the
    /// version in use; an application combines the ones of the libraries it uses, see
    /// <see cref="DomRegistrations.Combine(DomRegistration[])"/>.
    /// </remarks>
    /// <param name="registry">The registry to declare the DOM types on.</param>
    public delegate void DomRegistration(DomRegistry registry);

    /// <summary>
    /// Helpers for composing <see cref="DomRegistration"/> instances.
    /// </summary>
    public static class DomRegistrations
    {
        /// <summary>
        /// Combines several registrations, e.g. the ones of AngleSharp and AngleSharp.Css, into one.
        /// They run in the given order, so a later one adds to or replaces what an earlier one
        /// declared of the same type.
        /// </summary>
        /// <param name="registrations">The registrations to run.</param>
        /// <returns>The combined registration.</returns>
        public static DomRegistration Combine(params DomRegistration?[] registrations)
        {
            registrations = registrations ?? throw new ArgumentNullException(nameof(registrations));

            return registry =>
            {
                for (var i = 0; i < registrations.Length; i++)
                {
                    registrations[i]?.Invoke(registry);
                }
            };
        }
    }
}
