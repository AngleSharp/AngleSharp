namespace AngleSharp.Services.Default
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides the basic set of functionality for retrieving style engines.
    /// </summary>
    public class StylingService : IStylingService
    {
        readonly List<IStyleEngine> _engines;

        /// <summary>
        /// Creates a new styling service.
        /// </summary>
        public StylingService()
        {
            _engines = new List<IStyleEngine>();
        }

        /// <summary>
        /// Registers a new styling engine.
        /// </summary>
        /// <param name="engine">The engine to add.</param>
        public virtual void Register(IStyleEngine engine)
        {
            _engines.Add(engine);
        }

        /// <summary>
        /// Unregisters a new styling engine.
        /// </summary>
        /// <param name="engine">The engine to remove.</param>
        public virtual void Unregister(IStyleEngine engine)
        {
            _engines.Remove(engine);
        }

        /// <summary>
        /// Gets the registered engine for the provided mime-type.
        /// </summary>
        /// <param name="mimeType">The type of the engine.</param>
        /// <returns>The engine for the mime-type, if any.</returns>
        public virtual IStyleEngine GetEngine(String mimeType)
        {
            foreach (var engine in _engines)
            {
                if (engine.Type.Equals(mimeType, StringComparison.OrdinalIgnoreCase))
                    return engine;
            }

            return null;
        }
    }
}
