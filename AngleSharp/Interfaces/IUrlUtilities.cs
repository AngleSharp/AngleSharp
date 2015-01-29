namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The URLUtils interface defines utility methods to work with URLs.
    /// </summary>
    [DomName("URLUtils")]
    [DomNoInterfaceObject]
    public interface IUrlUtilities
    {
        /// <summary>
        /// Gets or sets the whole URL.
        /// </summary>
        [DomName("href")]
        String Href { get; set; }

        /// <summary>
        /// Gets or sets the protocol scheme of the URL, including the final
        /// ':'.
        /// </summary>
        [DomName("protocol")]
        String Protocol { get; set; }

        /// <summary>
        /// Gets or sets the host, that is the hostname, a ':', and the port of
        /// the URL.
        /// </summary>
        [DomName("host")]
        String Host { get; set; }

        /// <summary>
        /// Gets or sets the domain of the URL.
        /// </summary>
        [DomName("hostname")]
        String HostName { get; set; }

        /// <summary>
        /// Gets or sets the port number of the URL.
        /// </summary>
        [DomName("port")]
        String Port { get; set; }

        /// <summary>
        /// Gets or sets an initial '/' followed by the path of the URL.
        /// </summary>
        [DomName("pathname")]
        String PathName { get; set; }

        /// <summary>
        /// Gets or sets a '?' followed by the parameters of the URL.
        /// </summary>
        [DomName("search")]
        String Search { get; set; }

        /// <summary>
        /// Gets or sets a '#' followed by the fragment identifier of the URL.
        /// </summary>
        [DomName("hash")]
        String Hash { get; set; }

        /// <summary>
        /// Gets or sets the username specified before the domain name..
        /// </summary>
        [DomName("username")]
        String UserName { get; set; }

        /// <summary>
        /// Gets or sets the password specified before the domain name.
        /// </summary>
        [DomName("password")]
        String Password { get; set; }

        /// <summary>
        /// Gets the canonical form of the origin of the specific location.
        /// </summary>
        [DomName("origin")]
        String Origin { get; }
    }
}
