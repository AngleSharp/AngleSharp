namespace AngleSharp
{
    using AngleSharp.Infrastructure;
    using AngleSharp.Network;
    using AngleSharp.Parser;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents a copy of a provided original configuration.
    /// </summary>
    sealed class CopyConfiguration : IConfiguration
    {
        readonly IConfiguration _original;

        public CopyConfiguration(IConfiguration original)
        {
            _original = original;
            AllowRequests = original.AllowRequests;
            IsScripting = original.IsScripting;
            IsStyling = original.IsStyling;
            IsEmbedded = original.IsEmbedded;
            UseQuirksMode = original.UseQuirksMode;
            Language = original.Language;
            Culture = original.Culture;
            UserAgentInfo = original.UserAgentInfo;
        }

        public Boolean AllowRequests
        {
            get;
            set;
        }

        public Boolean IsScripting
        {
            get;
            set;
        }

        public Boolean IsStyling
        {
            get;
            set;
        }

        public Boolean IsEmbedded
        {
            get;
            set;
        }

        public Boolean UseQuirksMode
        {
            get;
            set;
        }

        public String Language
        {
            get;
            set;
        }

        public CultureInfo Culture
        {
            get;
            set;
        }

        public IInfo UserAgentInfo
        {
            get;
            set;
        }

        public IEnumerable<IService> Services
        {
            get { return _original.Services; }
        }

        public IEnumerable<IScriptEngine> ScriptEngines
        {
            get { return _original.ScriptEngines; }
        }

        public IEnumerable<IStyleEngine> StyleEngines
        {
            get { return _original.StyleEngines; }
        }

        public IRequester GetRequester()
        {
            return _original.GetRequester();
        }

        public void ReportError(ParseErrorEventArgs e)
        {
            _original.ReportError(e);
        }
    }
}
