namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The collection of (known / used) tags.
    /// </summary>
    public static class TagNames
    {
        #region General / Special

        /// <summary>
        /// Gets the DOCTYPE constant.
        /// </summary>
        public const String Doctype = "DOCTYPE";

        #endregion

        #region HTML Tags

        /// <summary>
        /// The html tag.
        /// </summary>
        public const String Html = "html";

        /// <summary>
        /// The body tag.
        /// </summary>
        public const String Body = "body";

        /// <summary>
        /// The head tag.
        /// </summary>
        public const String Head = "head";

        /// <summary>
        /// The meta tag.
        /// </summary>
        public const String Meta = "meta";

        /// <summary>
        /// The title tag.
        /// </summary>
        public const String Title = "title";

        /// <summary>
        /// The bgsound tag.
        /// </summary>
        public const String Bgsound = "bgsound";

        /// <summary>
        /// The script tag.
        /// </summary>
        public const String Script = "script";

        /// <summary>
        /// The style tag.
        /// </summary>
        public const String Style = "style";

        /// <summary>
        /// The noembed tag.
        /// </summary>
        public const String NoEmbed = "noembed";

        /// <summary>
        /// The noscript tag.
        /// </summary>
        public const String NoScript = "noscript";

        /// <summary>
        /// The noframes tag.
        /// </summary>
        public const String NoFrames = "noframes";

        /// <summary>
        /// The menu tag.
        /// </summary>
        public const String Menu = "menu";

        /// <summary>
        /// The menuitem tag.
        /// </summary>
        public const String MenuItem = "menuitem";

        /// <summary>
        /// The var tag.
        /// </summary>
        public const String Var = "var";

        /// <summary>
        /// The ruby tag.
        /// </summary>
        public const String Ruby = "ruby";

        /// <summary>
        /// The sub tag.
        /// </summary>
        public const String Sub = "sub";

        /// <summary>
        /// The sup tag.
        /// </summary>
        public const String Sup = "sup";

        /// <summary>
        /// The rp tag.
        /// </summary>
        public const String Rp = "rp";

        /// <summary>
        /// The rt tag.
        /// </summary>
        public const String Rt = "rt";

        /// <summary>
        /// The rb tag.
        /// </summary>
        public const String Rb = "rb";

        /// <summary>
        /// The rtc tag.
        /// </summary>
        public const String Rtc = "rtc";

        /// <summary>
        /// The applet tag.
        /// </summary>
        public const String Applet = "applet";

        /// <summary>
        /// The embed tag.
        /// </summary>
        public const String Embed = "embed";

        /// <summary>
        /// The marquee tag.
        /// </summary>
        public const String Marquee = "marquee";

        /// <summary>
        /// The param tag.
        /// </summary>
        public const String Param = "param";

        /// <summary>
        /// The object tag.
        /// </summary>
        public const String Object = "object";

        /// <summary>
        /// The canvas tag.
        /// </summary>
        public const String Canvas = "canvas";

        /// <summary>
        /// The font tag.
        /// </summary>
        public const String Font = "font";

        /// <summary>
        /// The ins tag.
        /// </summary>
        public const String Ins = "ins";

        /// <summary>
        /// The del tag.
        /// </summary>
        public const String Del = "del";

        /// <summary>
        /// The template tag.
        /// </summary>
        public const String Template = "template";

        /// <summary>
        /// The slot tag.
        /// </summary>
        public const String Slot = "slot";

        /// <summary>
        /// The caption tag.
        /// </summary>
        public const String Caption = "caption";

        /// <summary>
        /// The col tag.
        /// </summary>
        public const String Col = "col";

        /// <summary>
        /// The colgroup tag.
        /// </summary>
        public const String Colgroup = "colgroup";

        /// <summary>
        /// The table tag.
        /// </summary>
        public const String Table = "table";

        /// <summary>
        /// The thead tag.
        /// </summary>
        public const String Thead = "thead";

        /// <summary>
        /// The tbody tag.
        /// </summary>
        public const String Tbody = "tbody";

        /// <summary>
        /// The tfoot tag.
        /// </summary>
        public const String Tfoot = "tfoot";

        /// <summary>
        /// The th tag.
        /// </summary>
        public const String Th = "th";

        /// <summary>
        /// The td tag.
        /// </summary>
        public const String Td = "td";

        /// <summary>
        /// The tr tag.
        /// </summary>
        public const String Tr = "tr";

        /// <summary>
        /// The input tag.
        /// </summary>
        public const String Input = "input";

        /// <summary>
        /// The keygen tag.
        /// </summary>
        public const String Keygen = "keygen";

        /// <summary>
        /// The textarea tag.
        /// </summary>
        public const String Textarea = "textarea";

        /// <summary>
        /// The p tag.
        /// </summary>
        public const String P = "p";

        /// <summary>
        /// The span tag.
        /// </summary>
        public const String Span = "span";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public const String Dialog = "dialog";

        /// <summary>
        /// The fieldset tag.
        /// </summary>
        public const String Fieldset = "fieldset";

        /// <summary>
        /// The legend tag.
        /// </summary>
        public const String Legend = "legend";

        /// <summary>
        /// The label tag.
        /// </summary>
        public const String Label = "label";

        /// <summary>
        /// The details tag.
        /// </summary>
        public const String Details = "details";

        /// <summary>
        /// The form tag.
        /// </summary>
        public const String Form = "form";

        /// <summary>
        /// The isindex tag.
        /// </summary>
        public const String IsIndex = "isindex";

        /// <summary>
        /// The pre tag.
        /// </summary>
        public const String Pre = "pre";

        /// <summary>
        /// The data tag.
        /// </summary>
        public const String Data = "data";

        /// <summary>
        /// The datalist tag.
        /// </summary>
        public const String Datalist = "datalist";

        /// <summary>
        /// The ol tag.
        /// </summary>
        public const String Ol = "ol";

        /// <summary>
        /// The tag ul.
        /// </summary>
        public const String Ul = "ul";

        /// <summary>
        /// The dl tag.
        /// </summary>
        public const String Dl = "dl";

        /// <summary>
        /// The li tag.
        /// </summary>
        public const String Li = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        public const String Dd = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        public const String Dt = "dt";

        /// <summary>
        /// The b tag.
        /// </summary>
        public const String B = "b";

        /// <summary>
        /// The big tag.
        /// </summary>
        public const String Big = "big";

        /// <summary>
        /// The strike tag.
        /// </summary>
        public const String Strike = "strike";

        /// <summary>
        /// The code tag.
        /// </summary>
        public const String Code = "code";

        /// <summary>
        /// The em tag.
        /// </summary>
        public const String Em = "em";

        /// <summary>
        /// The i tag.
        /// </summary>
        public const String I = "i";

        /// <summary>
        /// The s tag.
        /// </summary>
        public const String S = "s";

        /// <summary>
        /// The small tag.
        /// </summary>
        public const String Small = "small";

        /// <summary>
        /// The strong tag.
        /// </summary>
        public const String Strong = "strong";

        /// <summary>
        /// The u tag.
        /// </summary>
        public const String U = "u";

        /// <summary>
        /// The tt tag.
        /// </summary>
        public const String Tt = "tt";

        /// <summary>
        /// The nobr tag.
        /// </summary>
        public const String NoBr = "nobr";

        /// <summary>
        /// The select tag.
        /// </summary>
        public const String Select = "select";

        /// <summary>
        /// The option tag.
        /// </summary>
        public const String Option = "option";

        /// <summary>
        /// The optgroup tag.
        /// </summary>
        public const String Optgroup = "optgroup";

        /// <summary>
        /// The link tag.
        /// </summary>
        public const String Link = "link";

        /// <summary>
        /// The frameset tag.
        /// </summary>
        public const String Frameset = "frameset";

        /// <summary>
        /// The frame tag.
        /// </summary>
        public const String Frame = "frame";

        /// <summary>
        /// The iframe tag.
        /// </summary>
        public const String Iframe = "iframe";

        /// <summary>
        /// The audio tag.
        /// </summary>
        public const String Audio = "audio";

        /// <summary>
        /// The video tag.
        /// </summary>
        public const String Video = "video";

        /// <summary>
        /// The source tag.
        /// </summary>
        public const String Source = "source";

        /// <summary>
        /// The track tag.
        /// </summary>
        public const String Track = "track";

        /// <summary>
        /// The h1 tag.
        /// </summary>
        public const String H1 = "h1";

        /// <summary>
        /// The h2 tag.
        /// </summary>
        public const String H2 = "h2";

        /// <summary>
        /// The h3 tag.
        /// </summary>
        public const String H3 = "h3";

        /// <summary>
        /// The h4 tag.
        /// </summary>
        public const String H4 = "h4";

        /// <summary>
        /// The h5 tag.
        /// </summary>
        public const String H5 = "h5";

        /// <summary>
        /// The h6 tag.
        /// </summary>
        public const String H6 = "h6";

        /// <summary>
        /// The div tag.
        /// </summary>
        public const String Div = "div";

        /// <summary>
        /// The quote tag.
        /// </summary>
        public const String Quote = "quote";

        /// <summary>
        /// The blockquote tag.
        /// </summary>
        public const String BlockQuote = "blockquote";

        /// <summary>
        /// The q tag.
        /// </summary>
        public const String Q = "q";

        /// <summary>
        /// The base tag.
        /// </summary>
        public const String Base = "base";

        /// <summary>
        /// The basefont tag.
        /// </summary>
        public const String BaseFont = "basefont";

        /// <summary>
        /// The a tag.
        /// </summary>
        public const String A = "a";

        /// <summary>
        /// The area tag.
        /// </summary>
        public const String Area = "area";

        /// <summary>
        /// The button tag.
        /// </summary>
        public const String Button = "button";

        /// <summary>
        /// The cite tag.
        /// </summary>
        public const String Cite = "cite";

        /// <summary>
        /// The main tag.
        /// </summary>
        public const String Main = "main";

        /// <summary>
        /// The summary tag.
        /// </summary>
        public const String Summary = "summary";

        /// <summary>
        /// The xmp tag.
        /// </summary>
        public const String Xmp = "xmp";

        /// <summary>
        /// The br tag.
        /// </summary>
        public const String Br = "br";

        /// <summary>
        /// The wbr tag.
        /// </summary>
        public const String Wbr = "wbr";

        /// <summary>
        /// The hr tag.
        /// </summary>
        public const String Hr = "hr";

        /// <summary>
        /// The dir tag.
        /// </summary>
        public const String Dir = "dir";

        /// <summary>
        /// The center tag.
        /// </summary>
        public const String Center = "center";

        /// <summary>
        /// The listing tag.
        /// </summary>
        public const String Listing = "listing";

        /// <summary>
        /// The img tag.
        /// </summary>
        public const String Img = "img";

        /// <summary>
        /// The image tag (this is not the right tag).
        /// </summary>
        public const String Image = "image";

        /// <summary>
        /// The nav tag.
        /// </summary>
        public const String Nav = "nav";

        /// <summary>
        /// The address tag.
        /// </summary>
        public const String Address = "address";

        /// <summary>
        /// The article tag.
        /// </summary>
        public const String Article = "article";

        /// <summary>
        /// The aside tag.
        /// </summary>
        public const String Aside = "aside";

        /// <summary>
        /// The figcaption tag.
        /// </summary>
        public const String Figcaption = "figcaption";

        /// <summary>
        /// The figure tag.
        /// </summary>
        public const String Figure = "figure";

        /// <summary>
        /// The section tag.
        /// </summary>
        public const String Section = "section";

        /// <summary>
        /// The footer tag.
        /// </summary>
        public const String Footer = "footer";

        /// <summary>
        /// The header tag.
        /// </summary>
        public const String Header = "header";

        /// <summary>
        /// The hgroup tag.
        /// </summary>
        public const String Hgroup = "hgroup";

        /// <summary>
        /// The plaintext tag.
        /// </summary>
        public const String Plaintext = "plaintext";

        /// <summary>
        /// The time tag.
        /// </summary>
        public const String Time = "time";

        /// <summary>
        /// The progress tag.
        /// </summary>
        public const String Progress = "progress";

        /// <summary>
        /// The meter tag.
        /// </summary>
        public const String Meter = "meter";

        /// <summary>
        /// The output tag.
        /// </summary>
        public const String Output = "output";

        /// <summary>
        /// The map tag.
        /// </summary>
        public const String Map = "map";

        /// <summary>
        /// The picture tag.
        /// </summary>
        public const String Picture = "picture";

        /// <summary>
        /// The mark tag.
        /// </summary>
        public const String Mark = "mark";

        /// <summary>
        /// The dfn tag.
        /// </summary>
        public const String Dfn = "dfn";

        /// <summary>
        /// The kbd tag.
        /// </summary>
        public const String Kbd = "kbd";

        /// <summary>
        /// The samp tag.
        /// </summary>
        public const String Samp = "samp";

        /// <summary>
        /// The abbr tag.
        /// </summary>
        public const String Abbr = "abbr";

        /// <summary>
        /// The bdi tag.
        /// </summary>
        public const String Bdi = "bdi";

        /// <summary>
        /// The bdo tag.
        /// </summary>
        public const String Bdo = "bdo";

        #endregion

        #region MathML Tags

        /// <summary>
        /// The math tag.
        /// </summary>
        public const String Math = "math";

        /// <summary>
        /// The mi tag.
        /// </summary>
        public const String Mi = "mi";

        /// <summary>
        /// The mo tag.
        /// </summary>
        public const String Mo = "mo";

        /// <summary>
        /// The mn tag.
        /// </summary>
        public const String Mn = "mn";

        /// <summary>
        /// The ms tag.
        /// </summary>
        public const String Ms = "ms";

        /// <summary>
        /// The mtext tag.
        /// </summary>
        public const String Mtext = "mtext";

        /// <summary>
        /// The annotation-xml tag.
        /// </summary>
        public const String AnnotationXml = "annotation-xml";

        #endregion

        #region SVG Tags

        /// <summary>
        /// The svg tag.
        /// </summary>
        public const String Svg = "svg";

        /// <summary>
        /// The foreignObject tag.
        /// </summary>
        public const String ForeignObject = "foreignObject";

        /// <summary>
        /// The desc tag.
        /// </summary>
        public const String Desc = "desc";

        /// <summary>
        /// The circle tag.
        /// </summary>
        public const String Circle = "circle";

        #endregion

        #region XML Tags

        /// <summary>
        /// The xml tag.
        /// </summary>
        public const String Xml = "xml";

        #endregion

        #region Combinations

        internal static readonly HashSet<String> AllForeignExceptions = new()
        {
            B, Big, BlockQuote, Body, Br, Center, Code, Dd, Div, Dl, Dt, Em, Embed, Head,
            Hr, I, Img, Li, Ul, H3, H2, H4, H1, H6, H5, Listing, Menu, Meta, NoBr, Ol, P,
            Pre, Ruby, S, Small, Span, Strike, Strong, Sub, Sup, Table, Tt, U, Var
        };

        internal static readonly HashSet<String> AllBeforeHead = new ()
        {
            Html, Body, Br, Head
        };

        internal static readonly HashSet<String> AllNoShadowRoot = new ()
        {
            Button, Details, Input, Marquee, Meter, Progress, Select, Textarea, Keygen
        };

        internal static readonly HashSet<String> AllHead = new ()
        {
            Style, Link, Meta, Title, NoFrames, Template, Base, BaseFont, Bgsound
        };

        internal static readonly HashSet<String> AllHeadNoTemplate = new ()
        {
            Link, Meta, Script, Style, Title, Base, BaseFont, Bgsound, NoFrames
        };

        internal static readonly HashSet<String> AllHeadBase = new ()
        {
            Link, Base, BaseFont, Bgsound
        };

        internal static readonly HashSet<String> AllBodyBreakrow = new ()
        {
            Br, Area, Embed, Keygen, Wbr
        };

        internal static readonly HashSet<String> AllBodyClosed = new ()
        {
            MenuItem, Param, Source, Track
        };

        internal static readonly HashSet<String> AllNoScript = new ()
        {
            Style, Link, BaseFont, Meta, NoFrames, Bgsound
        };

        internal static readonly HashSet<String> AllHeadings = new ()
        {
            H3, H2, H4, H1, H6, H5
        };

        internal static readonly HashSet<String> AllBlocks = new ()
        {
            Ol, Ul, Dl, Fieldset, Button, Figcaption, Figure, Article, Aside, BlockQuote,
            Center, Address, Dialog, Dir, Summary, Details, Listing, Footer, Header, Nav,
            Section, Menu, Hgroup, Main, Pre
        };

        internal static readonly HashSet<String> AllBody = new ()
        {
            Ol, Dl, Fieldset, Figcaption, Figure, Article, Aside, BlockQuote, Center, Address,
            Dialog, Dir, Summary, Details, Main, Footer, Header, Nav, Section, Menu, Hgroup
        };

        internal static readonly HashSet<String> AllBodyObsolete = new ()
        {
            Applet, Marquee, Object
        };

        internal static readonly HashSet<String> AllInput = new ()
        {
            Input, Keygen, Textarea
        };

        internal static readonly HashSet<String> AllBasicBlocks = new ()
        {
            Address, Div, P
        };

        internal static readonly HashSet<String> AllSemanticFormatting = new ()
        {
            B, Strong, Code, Em, U, I
        };

        internal static readonly HashSet<String> AllClassicFormatting = new ()
        {
            Font, S, Small, Strike, Big, Tt
        };

        internal static readonly HashSet<String> AllFormatting = new ()
        {
            B, Strong, Code, Em, U, I, NoBr, Font, S, Small, Strike, Big, Tt
        };

        internal static readonly HashSet<String> AllNested = new ()
        {
            Tbody, Td, Tfoot, Th, Thead, Tr, Caption, Col, Colgroup, Frame, Head
        };

        internal static readonly HashSet<String> AllCaptionEnd = new()
        {
            Tbody, Col, Tfoot, Td, Thead, Caption, Th, Colgroup, Tr
        };

        internal static readonly HashSet<String> AllCaptionStart = new ()
        {
            Tbody, Col, Tfoot, Td, Thead, Tr, Body, Th, Colgroup, Html
        };

        internal static readonly HashSet<String> AllTable = new ()
        {
            Tbody, Col, Tfoot, Td, Thead, Tr
        };

        internal static readonly HashSet<String> AllTableRoot = new ()
        {
            Caption, Colgroup, Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableGeneral = new ()
        {
            Caption, Colgroup, Col, Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableSections = new ()
        {
            Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableMajor = new ()
        {
            Tbody, Tfoot, Thead, Table, Tr
        };

        internal static readonly HashSet<String> AllTableSpecial = new ()
        {
            Td, Th,  Body, Caption, Col, Colgroup, Html
        };

        internal static readonly HashSet<String> AllTableCore = new ()
        {
            Tr, Table, Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableInner = new ()
        {
            Tbody, Tr, Thead, Th, Tfoot, Td
        };

        internal static readonly HashSet<String> AllTableSelects = new ()
        {
            Tr, Table, Tbody, Tfoot, Thead, Td, Th, Caption
        };

        internal static readonly HashSet<String> AllTableCells = new ()
        {
            Td, Th
        };

        internal static readonly HashSet<String> AllTableCellsRows = new ()
        {
            Tr, Td, Th
        };

        internal static readonly HashSet<String> AllTableHead = new ()
        {
            Script, Style, Template
        };

        internal static readonly HashSet<String> DisallowedCustomElementNames = new ()
        {
            "annotation-xml",
            "color-profile",
            "font-face",
            "font-face-src",
            "font-face-uri",
            "font-face-format",
            "font-face-name",
            "missing-glyph"
        };

        #endregion
    }
}