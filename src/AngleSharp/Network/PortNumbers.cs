namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Contains a collection of default ports.
    /// </summary>
    static class PortNumbers
    {
        private static readonly Dictionary<String, String> Ports = new Dictionary<String, String>
        {
            { ProtocolNames.Http, "80" },
            { ProtocolNames.Https, "443" },
            { ProtocolNames.Ftp, "21" },
            { ProtocolNames.File, "" },
            { ProtocolNames.Ws, "80" },
            { ProtocolNames.Wss, "443" },
            { ProtocolNames.Gopher, "70" },
            { ProtocolNames.Telnet, "23" },
            { ProtocolNames.Ssh, "22" },
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
            var value = default(String);
            Ports.TryGetValue(protocol, out value);
            return value;
        }
    }
}
