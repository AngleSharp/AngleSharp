namespace AngleSharp.Scripting
{
    using System.Linq;

    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Sets scripting to true, registers the JavaScript engine and returns the same instance.
        /// </summary>
        /// <typeparam name="TConfiguration">Instance of Configuration.</typeparam>
        /// <param name="configuration">The configuration to modify.</param>
        /// <returns>The same object, for chaining.</returns>
        public static TConfiguration WithJavaScript<TConfiguration>(this TConfiguration configuration)
            where TConfiguration : Configuration
        {
            configuration.IsScripting = true;

            if (!configuration.ScriptEngines.OfType<JavaScriptEngine>().Any())
                configuration.Register(new JavaScriptEngine());

            return configuration;
        }
    }
}
