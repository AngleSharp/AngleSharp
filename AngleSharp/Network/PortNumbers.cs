namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains a collection of default ports.
    /// </summary>
    static class PortNumbers
    {
        static readonly Dictionary<String, String> ports = new Dictionary<String, String>
        {
            { KnownProtocols.Http, "80" },
            { KnownProtocols.Https, "443" },
            { KnownProtocols.Ftp, "21" },
            { KnownProtocols.File, "" },
            { KnownProtocols.Ws, "80" },
            { KnownProtocols.Wss, "443" },
            { KnownProtocols.Gopher, "70" },
            { KnownProtocols.Telnet, "23" },
            { KnownProtocols.Ssh, "22" },
        };

        /// <summary>
        /// Gets the default port for the given protocol, if the protocol is a
        /// relative scheme protocol.
        /// </summary>
        /// <param name="protocol">The protocol to get the port for.</param>
        /// <returns>
        /// The string representing the default port, or null, if the protocol
        /// is not known.
        /// </returns>
        public static String GetDefaultPort(String protocol)
        {
            var value = String.Empty;

            if (ports.TryGetValue(protocol, out value))
                return value;

            return null;
        }
    }
}
