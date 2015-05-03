namespace AngleSharp.Parser.Html
{
    using System;

    /// <summary>
    /// The DOCTYPE token.
    /// </summary>
    sealed class HtmlDoctypeToken : HtmlToken
    {
        #region Fields

        Boolean _quirks;
        String _publicIdentifier;
        String _systemIdentifier;

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
            get { return _quirks; }
            set { _quirks = value; }
        }

        /// <summary>
        /// Gets the state of the public identifier.
        /// </summary>
        public Boolean IsPublicIdentifierMissing
        {
            get { return _publicIdentifier == null; }
        }

        /// <summary>
        /// Gets the state of the system identifier.
        /// </summary>
        public Boolean IsSystemIdentifierMissing
        {
            get { return _systemIdentifier == null; }
        }

        /// <summary>
        /// Gets or sets the value of the public identifier.
        /// </summary>
        public String PublicIdentifier
        {
            get { return _publicIdentifier ?? String.Empty; }
            set { _publicIdentifier = value; }
        }

        /// <summary>
        /// Gets or sets the value of the system identifier.
        /// </summary>
        public String SystemIdentifier
        {
            get { return _systemIdentifier ?? String.Empty; }
            set { _systemIdentifier = value; }
        }

        /// <summary>
        /// Gets if the given doctype token represents a limited quirks mode state.
        /// </summary>
        public Boolean IsLimitedQuirks
        {
            get
            {
                if (PublicIdentifier.StartsWith("-//W3C//DTD XHTML 1.0 Frameset//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD XHTML 1.0 Transitional//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (SystemIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (SystemIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase))
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Gets if the given doctype token represents a full quirks mode state.
        /// </summary>
        public Boolean IsFullQuirks
        {
            get
            {
                if (IsQuirksForced)
                    return true;
                else if (Name == null || Name != "html")
                    return true;
                else if (PublicIdentifier.StartsWith("+//Silmaril//dtd html Pro v0r11 19970101//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//AdvaSoft Ltd//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//AS//DTD HTML 3.0 asWedit + extensions//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Level 1//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Level 2//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 1//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Strict Level 2//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0 Strict//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 2.1E//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 3.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML 3//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 1//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 2//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Level 3//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 1//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 2//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict Level 3//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML Strict//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//IETF//DTD HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Metrius//DTD Metrius Presentational//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML Strict//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 2.0 Tables//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML Strict//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Microsoft//DTD Internet Explorer 3.0 Tables//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Netscape Comm. Corp.//DTD HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Netscape Comm. Corp.//DTD Strict HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//O'Reilly and Associates//DTD HTML 2.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//O'Reilly and Associates//DTD HTML Extended 1.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//O'Reilly and Associates//DTD HTML Extended Relaxed 1.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//SoftQuad Software//DTD HoTMetaL PRO 6.0::19990601::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//SoftQuad//DTD HoTMetaL PRO 4.0::19971010::extensions to HTML 4.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Spyglass//DTD HTML 2.0 Extended//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//SQ//DTD HTML 2.0 HoTMetaL + extensions//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Sun Microsystems Corp.//DTD HotJava HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//Sun Microsystems Corp.//DTD HotJava Strict HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 3 1995-03-24//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2 Draft//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2 Final//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 3.2S Draft//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.0 Frameset//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.0 Transitional//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML Experimental 19960712//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD HTML Experimental 970421//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3C//DTD W3 HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//W3O//DTD W3 HTML 3.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.Equals("-//W3O//DTD W3 HTML Strict 3.0//EN//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//WebTechs//DTD Mozilla HTML 2.0//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.StartsWith("-//WebTechs//DTD Mozilla HTML//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.Equals("-/W3C/DTD HTML 4.0 Transitional/EN", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (PublicIdentifier.Equals("HTML", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (SystemIdentifier.Equals("http://www.ibm.com/data/dtd/v11/ibmxhtml1-transitional.dtd", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (IsSystemIdentifierMissing && PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Frameset//", StringComparison.OrdinalIgnoreCase))
                    return true;
                else if (IsSystemIdentifierMissing && PublicIdentifier.StartsWith("-//W3C//DTD HTML 4.01 Transitional//", StringComparison.OrdinalIgnoreCase))
                    return true;

                return false;
            }
        }

        /// <summary>
        /// Gets the status if the given doctype token matches one of the popular conditions.
        /// </summary>
        public Boolean IsValid
        {
            get
            {
                if (Name != null && Name == "html")
                {
                    if (IsPublicIdentifierMissing)
                        return IsSystemIdentifierMissing || SystemIdentifier.Equals("about:legacy-compat");
                    else if (PublicIdentifier.Equals("-//W3C//DTD HTML 4.0//EN"))
                        return IsSystemIdentifierMissing || SystemIdentifier.Equals("http://www.w3.org/TR/REC-html40/strict.dtd");
                    else if (PublicIdentifier.Equals("-//W3C//DTD HTML 4.01//EN"))
                        return IsSystemIdentifierMissing || SystemIdentifier.Equals("http://www.w3.org/TR/html4/strict.dtd");
                    else if (PublicIdentifier.Equals("-//W3C//DTD XHTML 1.0 Strict//EN"))
                        return SystemIdentifier.Equals("http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd");
                    else if (PublicIdentifier.Equals("-//W3C//DTD XHTML 1.1//EN"))
                        return SystemIdentifier.Equals("http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd");

                    return false;
                }

                return false;
            }
        }

        #endregion
    }
}
