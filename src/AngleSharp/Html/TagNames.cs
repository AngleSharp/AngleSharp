namespace AngleSharp.Html
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
        public static readonly String Doctype = "DOCTYPE";

        #endregion

        #region HTML Tags

        /// <summary>
        /// The html tag.
        /// </summary>
        public static readonly String Html = "html";

        /// <summary>
        /// The body tag.
        /// </summary>
        public static readonly String Body = "body";

        /// <summary>
        /// The head tag.
        /// </summary>
        public static readonly String Head = "head";

        /// <summary>
        /// The meta tag.
        /// </summary>
        public static readonly String Meta = "meta";

        /// <summary>
        /// The title tag.
        /// </summary>
        public static readonly String Title = "title";

        /// <summary>
        /// The bgsound tag.
        /// </summary>
        public static readonly String Bgsound = "bgsound";

        /// <summary>
        /// The script tag.
        /// </summary>
        public static readonly String Script = "script";

        /// <summary>
        /// The style tag.
        /// </summary>
        public static readonly String Style = "style";

        /// <summary>
        /// The noembed tag.
        /// </summary>
        public static readonly String NoEmbed = "noembed";

        /// <summary>
        /// The noscript tag.
        /// </summary>
        public static readonly String NoScript = "noscript";

        /// <summary>
        /// The noframes tag.
        /// </summary>
        public static readonly String NoFrames = "noframes";

        /// <summary>
        /// The menu tag.
        /// </summary>
        public static readonly String Menu = "menu";

        /// <summary>
        /// The menuitem tag.
        /// </summary>
        public static readonly String MenuItem = "menuitem";

        /// <summary>
        /// The var tag.
        /// </summary>
        public static readonly String Var = "var";

        /// <summary>
        /// The ruby tag.
        /// </summary>
        public static readonly String Ruby = "ruby";

        /// <summary>
        /// The sub tag.
        /// </summary>
        public static readonly String Sub = "sub";

        /// <summary>
        /// The sup tag.
        /// </summary>
        public static readonly String Sup = "sup";

        /// <summary>
        /// The rp tag.
        /// </summary>
        public static readonly String Rp = "rp";

        /// <summary>
        /// The rt tag.
        /// </summary>
        public static readonly String Rt = "rt";

        /// <summary>
        /// The rb tag.
        /// </summary>
        public static readonly String Rb = "rb";

        /// <summary>
        /// The rtc tag.
        /// </summary>
        public static readonly String Rtc = "rtc";

        /// <summary>
        /// The applet tag.
        /// </summary>
        public static readonly String Applet = "applet";

        /// <summary>
        /// The embed tag.
        /// </summary>
        public static readonly String Embed = "embed";

        /// <summary>
        /// The marquee tag.
        /// </summary>
        public static readonly String Marquee = "marquee";

        /// <summary>
        /// The param tag.
        /// </summary>
        public static readonly String Param = "param";

        /// <summary>
        /// The object tag.
        /// </summary>
        public static readonly String Object = "object";

        /// <summary>
        /// The canvas tag.
        /// </summary>
        public static readonly String Canvas = "canvas";

        /// <summary>
        /// The font tag.
        /// </summary>
        public static readonly String Font = "font";

        /// <summary>
        /// The ins tag.
        /// </summary>
        public static readonly String Ins = "ins";

        /// <summary>
        /// The del tag.
        /// </summary>
        public static readonly String Del = "del";

        /// <summary>
        /// The template tag.
        /// </summary>
        public static readonly String Template = "template";

        /// <summary>
        /// The slot tag.
        /// </summary>
        public static readonly String Slot = "slot";

        /// <summary>
        /// The caption tag.
        /// </summary>
        public static readonly String Caption = "caption";

        /// <summary>
        /// The col tag.
        /// </summary>
        public static readonly String Col = "col";

        /// <summary>
        /// The colgroup tag.
        /// </summary>
        public static readonly String Colgroup = "colgroup";

        /// <summary>
        /// The table tag.
        /// </summary>
        public static readonly String Table = "table";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public static readonly String Thead = "thead";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public static readonly String Tbody = "tbody";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public static readonly String Tfoot = "tfoot";

        /// <summary>
        /// The th tag.
        /// </summary>
        public static readonly String Th = "th";

        /// <summary>
        /// The td tag.
        /// </summary>
        public static readonly String Td = "td";

        /// <summary>
        /// The tr tag.
        /// </summary>
        public static readonly String Tr = "tr";

        /// <summary>
        /// The input tag.
        /// </summary>
        public static readonly String Input = "input";

        /// <summary>
        /// The keygen tag.
        /// </summary>
        public static readonly String Keygen = "keygen";

        /// <summary>
        /// The textarea tag.
        /// </summary>
        public static readonly String Textarea = "textarea";

        /// <summary>
        /// The p tag.
        /// </summary>
        public static readonly String P = "p";

        /// <summary>
        /// The span tag.
        /// </summary>
        public static readonly String Span = "span";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public static readonly String Dialog = "dialog";

        /// <summary>
        /// The fieldset tag.
        /// </summary>
        public static readonly String Fieldset = "fieldset";

        /// <summary>
        /// The legend tag.
        /// </summary>
        public static readonly String Legend = "legend";

        /// <summary>
        /// The label tag.
        /// </summary>
        public static readonly String Label = "label";

        /// <summary>
        /// The details tag.
        /// </summary>
        public static readonly String Details = "details";

        /// <summary>
        /// The form tag.
        /// </summary>
        public static readonly String Form = "form";

        /// <summary>
        /// The isindex tag.
        /// </summary>
        public static readonly String IsIndex = "isindex";

        /// <summary>
        /// The pre tag.
        /// </summary>
        public static readonly String Pre = "pre";

        /// <summary>
        /// The data tag.
        /// </summary>
        public static readonly String Data = "data";

        /// <summary>
        /// The datalist tag.
        /// </summary>
        public static readonly String Datalist = "datalist";

        /// <summary>
        /// The ol tag.
        /// </summary>
        public static readonly String Ol = "ol";

        /// <summary>
        /// The tag ul.
        /// </summary>
        public static readonly String Ul = "ul";

        /// <summary>
        /// The dl tag.
        /// </summary>
        public static readonly String Dl = "dl";

        /// <summary>
        /// The li tag.
        /// </summary>
        public static readonly String Li = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        public static readonly String Dd = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        public static readonly String Dt = "dt";

        /// <summary>
        /// The b tag.
        /// </summary>
        public static readonly String B = "b";

        /// <summary>
        /// The big tag.
        /// </summary>
        public static readonly String Big = "big";

        /// <summary>
        /// The strike tag.
        /// </summary>
        public static readonly String Strike = "strike";

        /// <summary>
        /// The code tag.
        /// </summary>
        public static readonly String Code = "code";

        /// <summary>
        /// The em tag.
        /// </summary>
        public static readonly String Em = "em";

        /// <summary>
        /// The i tag.
        /// </summary>
        public static readonly String I = "i";

        /// <summary>
        /// The s tag.
        /// </summary>
        public static readonly String S = "s";

        /// <summary>
        /// The small tag.
        /// </summary>
        public static readonly String Small = "small";

        /// <summary>
        /// The strong tag.
        /// </summary>
        public static readonly String Strong = "strong";

        /// <summary>
        /// The u tag.
        /// </summary>
        public static readonly String U = "u";

        /// <summary>
        /// The tt tag.
        /// </summary>
        public static readonly String Tt = "tt";

        /// <summary>
        /// The nobr tag.
        /// </summary>
        public static readonly String NoBr = "nobr";

        /// <summary>
        /// The select tag.
        /// </summary>
        public static readonly String Select = "select";

        /// <summary>
        /// The option tag.
        /// </summary>
        public static readonly String Option = "option";

        /// <summary>
        /// The optgroup tag.
        /// </summary>
        public static readonly String Optgroup = "optgroup";

        /// <summary>
        /// The link tag.
        /// </summary>
        public static readonly String Link = "link";

        /// <summary>
        /// The frameset tag.
        /// </summary>
        public static readonly String Frameset = "frameset";

        /// <summary>
        /// The frame tag.
        /// </summary>
        public static readonly String Frame = "frame";

        /// <summary>
        /// The iframe tag.
        /// </summary>
        public static readonly String Iframe = "iframe";

        /// <summary>
        /// The audio tag.
        /// </summary>
        public static readonly String Audio = "audio";

        /// <summary>
        /// The video tag.
        /// </summary>
        public static readonly String Video = "video";

        /// <summary>
        /// The source tag.
        /// </summary>
        public static readonly String Source = "source";

        /// <summary>
        /// The track tag.
        /// </summary>
        public static readonly String Track = "track";

        /// <summary>
        /// The h1 tag.
        /// </summary>
        public static readonly String H1 = "h1";

        /// <summary>
        /// The h2 tag.
        /// </summary>
        public static readonly String H2 = "h2";

        /// <summary>
        /// The h3 tag.
        /// </summary>
        public static readonly String H3 = "h3";

        /// <summary>
        /// The h4 tag.
        /// </summary>
        public static readonly String H4 = "h4";

        /// <summary>
        /// The h5 tag.
        /// </summary>
        public static readonly String H5 = "h5";

        /// <summary>
        /// The h6 tag.
        /// </summary>
        public static readonly String H6 = "h6";

        /// <summary>
        /// The div tag.
        /// </summary>
        public static readonly String Div = "div";

        /// <summary>
        /// The quote tag.
        /// </summary>
        public static readonly String Quote = "quote";

        /// <summary>
        /// The blockquote tag.
        /// </summary>
        public static readonly String BlockQuote = "blockquote";

        /// <summary>
        /// The q tag.
        /// </summary>
        public static readonly String Q = "q";

        /// <summary>
        /// The base tag.
        /// </summary>
        public static readonly String Base = "base";

        /// <summary>
        /// The basefont tag.
        /// </summary>
        public static readonly String BaseFont = "basefont";

        /// <summary>
        /// The a tag.
        /// </summary>
        public static readonly String A = "a";

        /// <summary>
        /// The area tag.
        /// </summary>
        public static readonly String Area = "area";

        /// <summary>
        /// The button tag.
        /// </summary>
        public static readonly String Button = "button";

        /// <summary>
        /// The cite tag.
        /// </summary>
        public static readonly String Cite = "cite";

        /// <summary>
        /// The main tag.
        /// </summary>
        public static readonly String Main = "main";

        /// <summary>
        /// The summary tag.
        /// </summary>
        public static readonly String Summary = "summary";

        /// <summary>
        /// The xmp tag.
        /// </summary>
        public static readonly String Xmp = "xmp";

        /// <summary>
        /// The br tag.
        /// </summary>
        public static readonly String Br = "br";

        /// <summary>
        /// The wbr tag.
        /// </summary>
        public static readonly String Wbr = "wbr";

        /// <summary>
        /// The hr tag.
        /// </summary>
        public static readonly String Hr = "hr";

        /// <summary>
        /// The dir tag.
        /// </summary>
        public static readonly String Dir = "dir";

        /// <summary>
        /// The center tag.
        /// </summary>
        public static readonly String Center = "center";

        /// <summary>
        /// The listing tag.
        /// </summary>
        public static readonly String Listing = "listing";

        /// <summary>
        /// The img tag.
        /// </summary>
        public static readonly String Img = "img";

        /// <summary>
        /// The image tag (this is not the right tag).
        /// </summary>
        public static readonly String Image = "image";

        /// <summary>
        /// The nav tag.
        /// </summary>
        public static readonly String Nav = "nav";

        /// <summary>
        /// The address tag.
        /// </summary>
        public static readonly String Address = "address";

        /// <summary>
        /// The article tag.
        /// </summary>
        public static readonly String Article = "article";

        /// <summary>
        /// The aside tag.
        /// </summary>
        public static readonly String Aside = "aside";

        /// <summary>
        /// The figcaption tag.
        /// </summary>
        public static readonly String Figcaption = "figcaption";

        /// <summary>
        /// The figure tag.
        /// </summary>
        public static readonly String Figure = "figure";

        /// <summary>
        /// The section tag.
        /// </summary>
        public static readonly String Section = "section";

        /// <summary>
        /// The footer tag.
        /// </summary>
        public static readonly String Footer = "footer";

        /// <summary>
        /// The header tag.
        /// </summary>
        public static readonly String Header = "header";

        /// <summary>
        /// The hgroup tag.
        /// </summary>
        public static readonly String Hgroup = "hgroup";

        /// <summary>
        /// The plaintext tag.
        /// </summary>
        public static readonly String Plaintext = "plaintext";

        /// <summary>
        /// The time tag.
        /// </summary>
        public static readonly String Time = "time";

        /// <summary>
        /// The progress tag.
        /// </summary>
        public static readonly String Progress = "progress";

        /// <summary>
        /// The meter tag.
        /// </summary>
        public static readonly String Meter = "meter";

        /// <summary>
        /// The output tag.
        /// </summary>
        public static readonly String Output = "output";

        /// <summary>
        /// The map tag.
        /// </summary>
        public static readonly String Map = "map";

        /// <summary>
        /// The picture tag.
        /// </summary>
        public static readonly String Picture = "picture";

        /// <summary>
        /// The mark tag.
        /// </summary>
        public static readonly String Mark = "mark";

        /// <summary>
        /// The dfn tag.
        /// </summary>
        public static readonly String Dfn = "dfn";

        /// <summary>
        /// The kbd tag.
        /// </summary>
        public static readonly String Kbd = "kbd";

        /// <summary>
        /// The samp tag.
        /// </summary>
        public static readonly String Samp = "samp";

        /// <summary>
        /// The abbr tag.
        /// </summary>
        public static readonly String Abbr = "abbr";

        /// <summary>
        /// The bdi tag.
        /// </summary>
        public static readonly String Bdi = "bdi";

        /// <summary>
        /// The bdo tag.
        /// </summary>
        public static readonly String Bdo = "bdo";

        #endregion

        #region MathML Tags

        /// <summary>
        /// The math tag.
        /// </summary>
        public static readonly String Math = "math";

        /// <summary>
        /// The mi tag.
        /// </summary>
        public static readonly String Mi = "mi";

        /// <summary>
        /// The mo tag.
        /// </summary>
        public static readonly String Mo = "mo";

        /// <summary>
        /// The mn tag.
        /// </summary>
        public static readonly String Mn = "mn";

        /// <summary>
        /// The ms tag.
        /// </summary>
        public static readonly String Ms = "ms";

        /// <summary>
        /// The mtext tag.
        /// </summary>
        public static readonly String Mtext = "mtext";

        /// <summary>
        /// The annotation-xml tag.
        /// </summary>
        public static readonly String AnnotationXml = "annotation-xml";

        #endregion

        #region SVG Tags

        /// <summary>
        /// The svg tag.
        /// </summary>
        public static readonly String Svg = "svg";

        /// <summary>
        /// The foreignObject tag.
        /// </summary>
        public static readonly String ForeignObject = "foreignObject";

        /// <summary>
        /// The desc tag.
        /// </summary>
        public static readonly String Desc = "desc";

        /// <summary>
        /// The circle tag.
        /// </summary>
        public static readonly String Circle = "circle";

        #endregion

        #region XML Tags

        /// <summary>
        /// The xml tag.
        /// </summary>
        public static readonly String Xml = "xml";

        #endregion

        #region Combinations

        internal static readonly HashSet<String> AllForeignExceptions = new HashSet<String>
        {
            B, Big, BlockQuote, Body, Br, Center, Code, Dd, Div, Dl, Dt, Em, Embed, Head, 
            Hr, I, Img, Li, Ul, H3, H2, H4, H1, H6, H5, Listing, Menu, Meta, NoBr, Ol, P, 
            Pre, Ruby, S, Small, Span, Strike, Strong, Sub, Sup, Table, Tt, U, Var
        };

        internal static readonly HashSet<String> AllBeforeHead = new HashSet<String>
        {
            Html, Body, Br, Head
        };

        internal static readonly HashSet<String> AllNoShadowRoot = new HashSet<String>
        {
            Button, Details, Input, Marquee, Meter, Progress, Select, Textarea, Keygen
        };

        internal static readonly HashSet<String> AllHead = new HashSet<String>
        {
            Style, Link, Meta, Title, NoFrames, Template, Base, BaseFont, Bgsound
        };

        internal static readonly HashSet<String> AllHeadNoTemplate = new HashSet<String>
        {
            Link, Meta, Script, Style, Title, Base, BaseFont, Bgsound, NoFrames
        };

        internal static readonly HashSet<String> AllHeadBase = new HashSet<String>
        {
            Link, Base, BaseFont, Bgsound
        };

        internal static readonly HashSet<String> AllBodyBreakrow = new HashSet<String>
        {
            Br, Area, Embed, Keygen, Wbr
        };

        internal static readonly HashSet<String> AllBodyClosed = new HashSet<String>
        {
            MenuItem, Param, Source, Track
        };

        internal static readonly HashSet<String> AllNoScript = new HashSet<String>
        {
            Style, Link, BaseFont, Meta, NoFrames, Bgsound
        };

        internal static readonly HashSet<String> AllHeadings = new HashSet<String>
        {
            H3, H2, H4, H1, H6, H5
        };

        internal static readonly HashSet<String> AllBlocks = new HashSet<String>
        {
            Ol, Ul, Dl, Fieldset, Button, Figcaption, Figure, Article, Aside, BlockQuote, 
            Center, Address, Dialog, Dir, Summary, Details, Listing, Footer, Header, Nav, 
            Section, Menu, Hgroup, Main, Pre
        };

        internal static readonly HashSet<String> AllBody = new HashSet<String>
        {
            Ol, Dl, Fieldset, Figcaption, Figure, Article, Aside, BlockQuote, Center, Address, 
            Dialog, Dir, Summary, Details, Main, Footer, Header, Nav, Section, Menu, Hgroup
        };

        internal static readonly HashSet<String> AllBodyObsolete = new HashSet<String>
        {
            Applet, Marquee, Object
        };

        internal static readonly HashSet<String> AllInput = new HashSet<String>
        {
            Input, Keygen, Textarea
        };

        internal static readonly HashSet<String> AllBasicBlocks = new HashSet<String>
        {
            Address, Div, P
        };

        internal static readonly HashSet<String> AllSemanticFormatting = new HashSet<String>
        {
            B, Strong, Code, Em, U, I
        };

        internal static readonly HashSet<String> AllClassicFormatting = new HashSet<String> 
        {
            Font, S, Small, Strike, Big, Tt
        };

        internal static readonly HashSet<String> AllFormatting = new HashSet<String> 
        {
            B, Strong, Code, Em, U, I, NoBr, Font, S, Small, Strike, Big, Tt
        };

        internal static readonly HashSet<String> AllNested = new HashSet<String>
        {
            Tbody, Td, Tfoot, Th, Thead, Tr, Caption, Col, Colgroup, Frame, Head
        };

        internal static readonly HashSet<String> AllCaptionEnd = new HashSet<String>
        {
            Tbody, Col, Tfoot, Td, Thead, Caption, Th, Colgroup, Tr
        };

        internal static readonly HashSet<String> AllCaptionStart = new HashSet<String>
        {
            Tbody, Col, Tfoot, Td, Thead, Tr, Body, Th, Colgroup, Html
        };

        internal static readonly HashSet<String> AllTable = new HashSet<String>
        {
            Tbody, Col, Tfoot, Td, Thead, Tr
        };

        internal static readonly HashSet<String> AllTableRoot = new HashSet<String>
        {
            Caption, Colgroup, Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableGeneral = new HashSet<String>
        {
            Caption, Colgroup, Col, Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableSections = new HashSet<String>
        {
            Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableMajor = new HashSet<String>
        {
            Tbody, Tfoot, Thead, Table, Tr
        };

        internal static readonly HashSet<String> AllTableSpecial = new HashSet<String>
        {
            Td, Th,  Body, Caption, Col, Colgroup, Html
        };

        internal static readonly HashSet<String> AllTableCore = new HashSet<String>
        {
            Tr, Table, Tbody, Tfoot, Thead
        };

        internal static readonly HashSet<String> AllTableInner = new HashSet<String>
        {
            Tbody, Tr, Thead, Th, Tfoot, Td
        };

        internal static readonly HashSet<String> AllTableSelects = new HashSet<String>
        {
            Tr, Table, Tbody, Tfoot, Thead, Td, Th, Caption
        };

        internal static readonly HashSet<String> AllTableCells = new HashSet<String>
        {
            Td, Th
        };

        internal static readonly HashSet<String> AllTableCellsRows = new HashSet<String>
        {
            Tr, Td, Th
        };

        internal static readonly HashSet<String> AllTableHead = new HashSet<String>
        {
            Script, Style, Template
        };

        #endregion
    }
}
