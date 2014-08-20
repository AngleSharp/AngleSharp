namespace AngleSharp
{
    using AngleSharp.Infrastructure;
    using AngleSharp.Network;
    using AngleSharp.Parser;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents the interface for a general setup of AngleSharp
    /// or a particular AngleSharp request.
    /// </summary>
    public interface IConfiguration
    {
        /// <summary>
        /// Gets or sets if (external) requests are allowed. Setting this
        /// to false will disable the usage of any external resource, e.g.
        /// style-sheets, scripts, images, ...
        /// </summary>
        Boolean AllowRequests { get; set; }

        /// <summary>
        /// Gets or sets the current scripting mode. If this is set to true,
        /// then the content of noscript tags will be skipped.
        /// </summary>
        Boolean IsScripting { get; set; }

        /// <summary>
        /// Gets or sets the current CSS mode. If this is set to true,
        /// then style-sheets will be loaded, parsed and evaluated.
        /// </summary>
        Boolean IsStyling { get; set; }

        /// <summary>
        /// Gets or sets the current embedding mode. Enabling embedding will
        /// emulate the document being rendered in an iframe.
        /// </summary>
        Boolean IsEmbedded { get; set; }

        /// <summary>
        /// Gets or sets if the quirks mode should be used for parsing.
        /// </summary>
        Boolean UseQuirksMode { get; set; }

        /// <summary>
        /// Gets or sets the language (code, e.g. en-US, de-DE) to use.
        /// </summary>
        String Language { get; set; }

        /// <summary>
        /// Gets or sets the culture to use.
        /// </summary>
        CultureInfo Culture { get; set; }

        /// <summary>
        /// Gets or sets the user-agent information.
        /// </summary>
        IInfo UserAgentInfo { get; set; }

        /// <summary>
        /// Gets an enumeration over the available script engines.
        /// By default no script engine is integrated.
        /// </summary>
        IEnumerable<IScriptEngine> ScriptEngines { get; }

        /// <summary>
        /// Gets an enumeration over the available style engines,
        /// besides the default CSS engine.
        /// </summary>
        IEnumerable<IStyleEngine> StyleEngines { get; }

        /// <summary>
        /// Creates a requester for performing web (e.g. HTTP) requests.
        /// </summary>
        /// <returns>The constructed HTTP requester.</returns>
        IRequester GetRequester();

        /// <summary>
        /// Method that is called once parse errors are encountered.
        /// </summary>
        void ReportError(ParseErrorEventArgs e);
    }
}
