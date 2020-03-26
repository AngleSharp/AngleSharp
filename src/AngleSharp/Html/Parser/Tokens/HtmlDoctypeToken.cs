namespace AngleSharp.Html.Parser.Tokens
{
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// The DOCTYPE token.
    /// </summary>
    public sealed class HtmlDoctypeToken : HtmlToken
    {
        #region Fields

        private Boolean _quirks;
        private String _publicIdentifier;
        private String _systemIdentifier;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new DOCTYPE token with the quirks mode set initially.
        /// </summary>
        /// <param name="quirksForced">The state of the force-quirks flag.</param>
        /// <param name="position">The token's position.</param>
        public HtmlDoctypeToken(Boolean quirksForced, TextPosition position)
            : base(HtmlTokenType.Doctype, position)
        {
            _publicIdentifier = null;
            _systemIdentifier = null;
            _quirks = quirksForced;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the state of the force-quirks flag.
        /// </summary>
        public Boolean IsQuirksForced
        {
            get => _quirks;
            set => _quirks = value;
        }

        /// <summary>
        /// Gets the state of the public identifier.
        /// </summary>
        public Boolean IsPublicIdentifierMissing => _publicIdentifier == null;

        /// <summary>
        /// Gets the state of the system identifier.
        /// </summary>
        public Boolean IsSystemIdentifierMissing => _systemIdentifier == null;

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicIdentifier
        {
            get => _publicIdentifier ?? String.Empty;
            set => _publicIdentifier = value;
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        public String SystemIdentifier
        {
            get => _systemIdentifier ?? String.Empty;
            set => _systemIdentifier = value;
        }

        /// <summary>
        /// Gets if the given doctype token represents a limited quirks mode state.
        /// </summary>
        public Boolean IsLimitedQuirks
        {
            get
            {
                var pi = PublicIdentifier;
                var si = SystemIdentifier;
                return (pi.StartsWith("-//W3C//DTD XHTML 1.0 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                        pi.StartsWith("-//W3C//DTD XHTML 1.0 Transitional//", StringComparison.OrdinalIgnoreCase) ||
                        si.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                        si.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase));
            }
        }

        /// <summary>
        /// Gets if the given doctype token represents a full quirks mode state.
        /// </summary>
        public Boolean IsFullQuirks
        {
            get
            {
                var pi = PublicIdentifier;
                return IsQuirksForced || !Name.Is("html") ||
                       pi.StartsWith("+//Silmaril//dtd html Pro v0r11 19970101//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//AdvaSoft Ltd//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//AS//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.0 Level 1//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.0 Level 2//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 1//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 2//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.0 Strict//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 2.1E//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 3.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML 3//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Level 0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Level 1//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Level 2//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Level 3//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Strict Level 0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Strict Level 1//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Strict Level 2//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Strict Level 3//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML Strict//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//IETF//DTD HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Metrius//DTD Metrius Presentational//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML Strict//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 Tables//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML Strict//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 Tables//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Netscape Comm. Corp.//DTD HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Netscape Comm. Corp.//DTD Strict HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//O'Reilly and Associates//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//O'Reilly and Associates//DTD HTML Extended 1.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//O'Reilly and Associates//DTD HTML Extended Relaxed 1.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//SoftQuad Software//DTD HoTMetaL PRO 6.0::19990601::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//SoftQuad//DTD HoTMetaL PRO 4.0::19971010::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Spyglass//DTD HTML 2.0 Extended//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//SQ//DTD HTML 2.0 HoTMetaL + extensions//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Sun Microsystems Corp.//DTD HotJava HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//Sun Microsystems Corp.//DTD HotJava Strict HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 3 1995-03-24//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 3.2 Draft//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 3.2S Draft//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 4.0 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML 4.0 Transitional//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML Experimental 19960712//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD HTML Experimental 970421//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3C//DTD W3 HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//W3O//DTD W3 HTML 3.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.Isi("-//W3O//DTD W3 HTML Strict 3.0//EN//") ||
                       pi.StartsWith("-//WebTechs//DTD Mozilla HTML 2.0//", StringComparison.OrdinalIgnoreCase) ||
                       pi.StartsWith("-//WebTechs//DTD Mozilla HTML//", StringComparison.OrdinalIgnoreCase) ||
                       pi.Isi("-/W3C/DTD HTML 4.0 Transitional/EN") ||
                       pi.Isi("HTML") ||
                       SystemIdentifier.Equals("http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd", StringComparison.OrdinalIgnoreCase) ||
                       IsSystemIdentifierMissing && pi.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase) ||
                       IsSystemIdentifierMissing && pi.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase);
            }
        }

        /// <summary>
        /// Gets the status if the given doctype token matches one of the popular conditions.
        /// </summary>
        public Boolean IsValid
        {
            get
            {
                if (Name.Is("html"))
                {
                    if (!IsPublicIdentifierMissing)
                    {
                        var pi = PublicIdentifier;

                        if (pi.Is("-//W3C//DTD HTML 4.0//EN"))
                        {
                            return IsSystemIdentifierMissing || SystemIdentifier.Is("http://www.w3.org/TR/REC-html40/strict.dtd");
                        }
                        else if (pi.Is("-//W3C//DTD HTML 4.01//EN"))
                        {
                            return IsSystemIdentifierMissing || SystemIdentifier.Is("http://www.w3.org/TR/html4/strict.dtd");
                        }
                        else if (pi.Is("-//W3C//DTD XHTML 1.0 Strict//EN"))
                        {
                            return SystemIdentifier.Is("http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd");
                        }
                        else if (pi.Is("-//W3C//DTD XHTML 1.1//EN"))
                        {
                            return SystemIdentifier.Is("http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd");
                        }
                    }

                    return IsSystemIdentifierMissing || SystemIdentifier.Is("about:legacy-compat");
                }

                return false;
            }
        }

        #endregion
    }
}
