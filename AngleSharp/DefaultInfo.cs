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

        readonly String _version;
        readonly String _agent;

        #endregion

        #region ctor

        public DefaultInfo()
        {
            _version = typeof(DefaultInfo).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            _agent = "AngleSharp/" + _version;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the version of AngleSharp.
        /// </summary>
        public String Version
        {
            get { return _version; }
        }

        /// <summary>
        /// Gets the agent string of AngleSharp.
        /// </summary>
        public String Agent
        {
            get { return _agent; }
        }

        #endregion
    }
}
