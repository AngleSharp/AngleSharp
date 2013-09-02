using System;
using System.Reflection;

namespace AngleSharp
{
    /// <summary>
    /// General information to be used internally about the library.
    /// </summary>
    class Info
    {
        #region Members

        String version;
        String agent;

        #endregion

        #region ctor

        Info()
        {
            version = typeof(Info).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            agent = "AngleSharp/" + version;
        }

        #endregion

        #region Singleton

        static readonly Info instance;

        static Info()
        {
            instance = new Info();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the version of AngleSharp.
        /// </summary>
        public static String Version
        {
            get { return instance.version; }
        }

        /// <summary>
        /// Gets the agent string of AngleSharp.
        /// </summary>
        public static String Agent
        {
            get { return instance.agent; }
        }

        #endregion
    }
}
