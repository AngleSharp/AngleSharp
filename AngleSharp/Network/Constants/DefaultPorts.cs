namespace AngleSharp
{
    using System;

    /// <summary>
    /// Contains a collection of default ports.
    /// </summary>
    static class DefaultPorts
    {
        /// <summary>
        /// Gets the default port for the file transfer protocol.
        /// </summary>
        public static readonly String FtpPort = "21";

        /// <summary>
        /// Gets the default port for the local file protocol.
        /// </summary>
        public static readonly String FilePort = "";

        /// <summary>
        /// Gets the default port for the gopher protocol.
        /// </summary>
        public static readonly String GopherPort = "70";

        /// <summary>
        /// Gets the default port for the hyper-text transfer protocol.
        /// </summary>
        public static readonly String HttpPort = "80";

        /// <summary>
        /// Gets the default port for the secure hyper-text transfer protocol.
        /// </summary>
        public static readonly String HttpsPort = "443";

        /// <summary>
        /// Gets the default port for the websocket protocol.
        /// </summary>
        public static readonly String WsPort = "80";

        /// <summary>
        /// Gets the default port for the secure websocket protocol.
        /// </summary>
        public static readonly String WssPort = "443";
    }
}
