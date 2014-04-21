namespace AngleSharp
{
    using System;

    /// <summary>
    /// Contains a list of known protocol names.
    /// </summary>
    static class KnownProtocols
    {
        /// <summary>
        /// The Hypertext Transfer Protocol.
        /// </summary>
        public const String Http = "http:";

        /// <summary>
        /// The Hypertext Transfer Protocol Secure.
        /// </summary>
        public const String Https = "https:";

        /// <summary>
        /// The File Transfer Protocol.
        /// </summary>
        public const String Ftp = "ftp:";

        /// <summary>
        /// The pseudo JavaScript Protocol.
        /// </summary>
        public const String JavaScript = "javascript:";

        /// <summary>
        /// The pseudo Data Protocol.
        /// </summary>
        public const String Data = "data:";

        /// <summary>
        /// The pseudo Mailto Protocol.
        /// </summary>
        public const String Mailto = "mailto:";
    }
}
