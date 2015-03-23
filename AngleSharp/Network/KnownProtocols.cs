namespace AngleSharp.Network
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Contains a list of known protocol names.
    /// </summary>
    static class KnownProtocols
    {
        /// <summary>
        /// The Hypertext Transfer Protocol.
        /// </summary>
        public static readonly String Http = "http";

        /// <summary>
        /// The Hypertext Transfer Protocol Secure.
        /// </summary>
        public static readonly String Https = "https";

        /// <summary>
        /// The File Transfer Protocol.
        /// </summary>
        public static readonly String Ftp = "ftp";

        /// <summary>
        /// The pseudo JavaScript protocol.
        /// </summary>
        public static readonly String JavaScript = "javascript";

        /// <summary>
        /// The pseudo Data protocol.
        /// </summary>
        public static readonly String Data = "data";

        /// <summary>
        /// The pseudo Mailto protocol.
        /// </summary>
        public static readonly String Mailto = "mailto";

        /// <summary>
        /// The pseudo File protocol.
        /// </summary>
        public static readonly String File = "file";

        /// <summary>
        /// The WebSocket protocol.
        /// </summary>
        public static readonly String Ws = "ws";

        /// <summary>
        /// The WebSocket Secure protocol.
        /// </summary>
        public static readonly String Wss = "wss";

        /// <summary>
        /// The Telnet protocol.
        /// </summary>
        public static readonly String Telnet = "telnet";

        /// <summary>
        /// The Secure Shell protocol.
        /// </summary>
        public static readonly String Ssh = "ssh";

        /// <summary>
        /// The legacy gopher protocol.
        /// </summary>
        public static readonly String Gopher = "gopher";
        
        /// <summary>
        /// The binary large object protocol.
        /// </summary>
        public static readonly String Blob = "blob";

        /// <summary>
        /// Checks if the given protocol (without a colon in the end) is
        /// following a relative scheme.
        /// </summary>
        /// <param name="protocol">The protocol to examine.</param>
        /// <returns>
        /// True if the protocol is a relative scheme, otherwise false.
        /// </returns>
        public static Boolean IsRelative(String protocol)
        {
            return protocol.IsOneOf(Http, Https, Ftp, File, Ws, Wss, Gopher);
        }

        /// <summary>
        /// Checks if the given protocol (without a colon in the end) is
        /// suitable for deriving the origin.
        /// </summary>
        /// <param name="protocol">The protocol to examine.</param>
        /// <returns>
        /// True if the protocol is suited for origin, otherwise false.
        /// </returns>
        public static Boolean IsOriginable(String protocol)
        {
            return protocol.IsOneOf(Http, Https, Ftp, Ws, Wss, Gopher);
        }
    }
}
