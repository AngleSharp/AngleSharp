namespace AngleSharp
{
    using System;
    using System.Reflection;

    /// <summary>
    /// General information to be used internally about the library.
    /// </summary>
    sealed class DefaultInfo : IInfo
    {
        #region Fields

        String version;
        String agent;

        #endregion

        #region ctor

        public DefaultInfo()
        {
            version = typeof(DefaultInfo).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            agent = "AngleSharp/" + version;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the version of AngleSharp.
        /// </summary>
        public String Version
        {
            get { return version; }
        }

        /// <summary>
        /// Gets the agent string of AngleSharp.
        /// </summary>
        public String Agent
        {
            get { return agent; }
        }

        #endregion
    }
}
