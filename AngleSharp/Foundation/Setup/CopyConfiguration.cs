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
            IsScripting = original.IsScripting;
            IsStyling = original.IsStyling;
            IsEmbedded = original.IsEmbedded;
            Culture = original.Culture;
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

        public CultureInfo Culture
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

        public IEnumerable<IRequester> Requesters
        {
            get { return _original.Requesters; }
        }

        public void ReportError(ParseErrorEventArgs e)
        {
            _original.ReportError(e);
        }
    }
}
