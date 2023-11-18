namespace AngleSharp.Dom
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// The collection of (known / used) tags.
    /// </summary>
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class TagNames
    {
        #region General / Special

        /// <summary>
        /// Gets the DOCTYPE constant.
        /// </summary>
        public static readonly String Doctype = _Doctype;
        internal const String _Doctype = "DOCTYPE";

        #endregion

        #region HTML Tags

        /// <summary>
        /// The html tag.
        /// </summary>
        public static readonly String Html = _Html;
        internal const String _Html = "html";

        /// <summary>
        /// The body tag.
        /// </summary>
        public static readonly String Body = _Body;
        internal const String _Body = "body";

        /// <summary>
        /// The head tag.
        /// </summary>
        public static readonly String Head = _Head;
        internal const String _Head = "head";

        /// <summary>
        /// The meta tag.
        /// </summary>
        public static readonly String Meta = _Meta;
        internal const String _Meta = "meta";

        /// <summary>
        /// The title tag.
        /// </summary>
        public static readonly String Title = _Title;
        internal const String _Title = "title";

        /// <summary>
        /// The bgsound tag.
        /// </summary>
        public static readonly String Bgsound = _Bgsound;
        internal const String _Bgsound = "bgsound";

        /// <summary>
        /// The script tag.
        /// </summary>
        public static readonly String Script = _Script;
        internal const String _Script = "script";

        /// <summary>
        /// The style tag.
        /// </summary>
        public static readonly String Style = _Style;
        internal const String _Style = "style";

        /// <summary>
        /// The noembed tag.
        /// </summary>
        public static readonly String NoEmbed = _NoEmbed;
        internal const String _NoEmbed = "noembed";

        /// <summary>
        /// The noscript tag.
        /// </summary>
        public static readonly String NoScript = _NoScript;
        internal const String _NoScript = "noscript";

        /// <summary>
        /// The noframes tag.
        /// </summary>
        public static readonly String NoFrames = _NoFrames;
        internal const String _NoFrames = "noframes";

        /// <summary>
        /// The menu tag.
        /// </summary>
        public static readonly String Menu = _Menu;
        internal const String _Menu = "menu";

        /// <summary>
        /// The menuitem tag.
        /// </summary>
        public static readonly String MenuItem = _MenuItem;
        internal const String _MenuItem = "menuitem";

        /// <summary>
        /// The var tag.
        /// </summary>
        public static readonly String Var = _Var;
        internal const String _Var = "var";

        /// <summary>
        /// The ruby tag.
        /// </summary>
        public static readonly String Ruby = _Ruby;
        internal const String _Ruby = "ruby";

        /// <summary>
        /// The sub tag.
        /// </summary>
        public static readonly String Sub = _Sub;
        internal const String _Sub = "sub";

        /// <summary>
        /// The sup tag.
        /// </summary>
        public static readonly String Sup = _Sup;
        internal const String _Sup = "sup";

        /// <summary>
        /// The rp tag.
        /// </summary>
        public static readonly String Rp = _Rp;
        internal const String _Rp = "rp";

        /// <summary>
        /// The rt tag.
        /// </summary>
        public static readonly String Rt = _Rt;
        internal const String _Rt = "rt";

        /// <summary>
        /// The rb tag.
        /// </summary>
        public static readonly String Rb = _Rb;
        internal const String _Rb = "rb";

        /// <summary>
        /// The rtc tag.
        /// </summary>
        public static readonly String Rtc = _Rtc;
        internal const String _Rtc = "rtc";

        /// <summary>
        /// The applet tag.
        /// </summary>
        public static readonly String Applet = _Applet;
        internal const String _Applet = "applet";

        /// <summary>
        /// The embed tag.
        /// </summary>
        public static readonly String Embed = _Embed;
        internal const String _Embed = "embed";

        /// <summary>
        /// The marquee tag.
        /// </summary>
        public static readonly String Marquee = _Marquee;
        internal const String _Marquee = "marquee";

        /// <summary>
        /// The param tag.
        /// </summary>
        public static readonly String Param = _Param;
        internal const String _Param = "param";

        /// <summary>
        /// The object tag.
        /// </summary>
        public static readonly String Object = _Object;
        internal const String _Object = "object";

        /// <summary>
        /// The canvas tag.
        /// </summary>
        public static readonly String Canvas = _Canvas;
        internal const String _Canvas = "canvas";

        /// <summary>
        /// The font tag.
        /// </summary>
        public static readonly String Font = _Font;
        internal const String _Font = "font";

        /// <summary>
        /// The ins tag.
        /// </summary>
        public static readonly String Ins = _Ins;
        internal const String _Ins = "ins";

        /// <summary>
        /// The del tag.
        /// </summary>
        public static readonly String Del = _Del;
        internal const String _Del = "del";

        /// <summary>
        /// The template tag.
        /// </summary>
        public static readonly String Template = _Template;
        internal const String _Template = "template";

        /// <summary>
        /// The slot tag.
        /// </summary>
        public static readonly String Slot = _Slot;
        internal const String _Slot = "slot";

        /// <summary>
        /// The caption tag.
        /// </summary>
        public static readonly String Caption = _Caption;
        internal const String _Caption = "caption";

        /// <summary>
        /// The col tag.
        /// </summary>
        public static readonly String Col = _Col;
        internal const String _Col = "col";

        /// <summary>
        /// The colgroup tag.
        /// </summary>
        public static readonly String Colgroup = _Colgroup;
        internal const String _Colgroup = "colgroup";

        /// <summary>
        /// The table tag.
        /// </summary>
        public static readonly String Table = _Table;
        internal const String _Table = "table";

        /// <summary>
        /// The thead tag.
        /// </summary>
        public static readonly String Thead = _Thead;
        internal const String _Thead = "thead";

        /// <summary>
        /// The tbody tag.
        /// </summary>
        public static readonly String Tbody = _Tbody;
        internal const String _Tbody = "tbody";

        /// <summary>
        /// The tfoot tag.
        /// </summary>
        public static readonly String Tfoot = _Tfoot;
        internal const String _Tfoot = "tfoot";

        /// <summary>
        /// The th tag.
        /// </summary>
        public static readonly String Th = _Th;
        internal const String _Th = "th";

        /// <summary>
        /// The td tag.
        /// </summary>
        public static readonly String Td = _Td;
        internal const String _Td = "td";

        /// <summary>
        /// The tr tag.
        /// </summary>
        public static readonly String Tr = _Tr;
        internal const String _Tr = "tr";

        /// <summary>
        /// The input tag.
        /// </summary>
        public static readonly String Input = _Input;
        internal const String _Input = "input";

        /// <summary>
        /// The keygen tag.
        /// </summary>
        public static readonly String Keygen = _Keygen;
        internal const String _Keygen = "keygen";

        /// <summary>
        /// The textarea tag.
        /// </summary>
        public static readonly String Textarea = _Textarea;
        internal const String _Textarea = "textarea";

        /// <summary>
        /// The p tag.
        /// </summary>
        public static readonly String P = _P;
        internal const String _P = "p";

        /// <summary>
        /// The span tag.
        /// </summary>
        public static readonly String Span = _Span;
        internal const String _Span = "span";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public static readonly String Dialog = _Dialog;
        internal const String _Dialog = "dialog";

        /// <summary>
        /// The fieldset tag.
        /// </summary>
        public static readonly String Fieldset = _Fieldset;
        internal const String _Fieldset = "fieldset";

        /// <summary>
        /// The legend tag.
        /// </summary>
        public static readonly String Legend = _Legend;
        internal const String _Legend = "legend";

        /// <summary>
        /// The label tag.
        /// </summary>
        public static readonly String Label = _Label;
        internal const String _Label = "label";

        /// <summary>
        /// The details tag.
        /// </summary>
        public static readonly String Details = _Details;
        internal const String _Details = "details";

        /// <summary>
        /// The form tag.
        /// </summary>
        public static readonly String Form = _Form;
        internal const String _Form = "form";

        /// <summary>
        /// The isindex tag.
        /// </summary>
        public static readonly String IsIndex = _IsIndex;
        internal const String _IsIndex = "isindex";

        /// <summary>
        /// The pre tag.
        /// </summary>
        public static readonly String Pre = _Pre;
        internal const String _Pre = "pre";

        /// <summary>
        /// The data tag.
        /// </summary>
        public static readonly String Data = _Data;
        internal const String _Data = "data";

        /// <summary>
        /// The datalist tag.
        /// </summary>
        public static readonly String Datalist = _Datalist;
        internal const String _Datalist = "datalist";

        /// <summary>
        /// The ol tag.
        /// </summary>
        public static readonly String Ol = _Ol;
        internal const String _Ol = "ol";

        /// <summary>
        /// The tag ul.
        /// </summary>
        public static readonly String Ul = _Ul;
        internal const String _Ul = "ul";

        /// <summary>
        /// The dl tag.
        /// </summary>
        public static readonly String Dl = _Dl;
        internal const String _Dl = "dl";

        /// <summary>
        /// The li tag.
        /// </summary>
        public static readonly String Li = _Li;
        internal const String _Li = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        public static readonly String Dd = _Dd;
        internal const String _Dd = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        public static readonly String Dt = _Dt;
        internal const String _Dt = "dt";

        /// <summary>
        /// The b tag.
        /// </summary>
        public static readonly String B = _B;
        internal const String _B = "b";

        /// <summary>
        /// The big tag.
        /// </summary>
        public static readonly String Big = _Big;
        internal const String _Big = "big";

        /// <summary>
        /// The strike tag.
        /// </summary>
        public static readonly String Strike = _Strike;
        internal const String _Strike = "strike";

        /// <summary>
        /// The code tag.
        /// </summary>
        public static readonly String Code = _Code;
        internal const String _Code = "code";

        /// <summary>
        /// The em tag.
        /// </summary>
        public static readonly String Em = _Em;
        internal const String _Em = "em";

        /// <summary>
        /// The i tag.
        /// </summary>
        public static readonly String I = _I;
        internal const String _I = "i";

        /// <summary>
        /// The s tag.
        /// </summary>
        public static readonly String S = _S;
        internal const String _S = "s";

        /// <summary>
        /// The small tag.
        /// </summary>
        public static readonly String Small = _Small;
        internal const String _Small = "small";

        /// <summary>
        /// The strong tag.
        /// </summary>
        public static readonly String Strong = _Strong;
        internal const String _Strong = "strong";

        /// <summary>
        /// The u tag.
        /// </summary>
        public static readonly String U = _U;
        internal const String _U = "u";

        /// <summary>
        /// The tt tag.
        /// </summary>
        public static readonly String Tt = _Tt;
        internal const String _Tt = "tt";

        /// <summary>
        /// The nobr tag.
        /// </summary>
        public static readonly String NoBr = _NoBr;
        internal const String _NoBr = "nobr";

        /// <summary>
        /// The select tag.
        /// </summary>
        public static readonly String Select = _Select;
        internal const String _Select = "select";

        /// <summary>
        /// The option tag.
        /// </summary>
        public static readonly String Option = _Option;
        internal const String _Option = "option";

        /// <summary>
        /// The optgroup tag.
        /// </summary>
        public static readonly String Optgroup = _Optgroup;
        internal const String _Optgroup = "optgroup";

        /// <summary>
        /// The link tag.
        /// </summary>
        public static readonly String Link = _Link;
        internal const String _Link = "link";

        /// <summary>
        /// The frameset tag.
        /// </summary>
        public static readonly String Frameset = _Frameset;
        internal const String _Frameset = "frameset";

        /// <summary>
        /// The frame tag.
        /// </summary>
        public static readonly String Frame = _Frame;
        internal const String _Frame = "frame";

        /// <summary>
        /// The iframe tag.
        /// </summary>
        public static readonly String Iframe = _Iframe;
        internal const String _Iframe = "iframe";

        /// <summary>
        /// The audio tag.
        /// </summary>
        public static readonly String Audio = _Audio;
        internal const String _Audio = "audio";

        /// <summary>
        /// The video tag.
        /// </summary>
        public static readonly String Video = _Video;
        internal const String _Video = "video";

        /// <summary>
        /// The source tag.
        /// </summary>
        public static readonly String Source = _Source;
        internal const String _Source = "source";

        /// <summary>
        /// The track tag.
        /// </summary>
        public static readonly String Track = _Track;
        internal const String _Track = "track";

        /// <summary>
        /// The h1 tag.
        /// </summary>
        public static readonly String H1 = _H1;
        internal const String _H1 = "h1";

        /// <summary>
        /// The h2 tag.
        /// </summary>
        public static readonly String H2 = _H2;
        internal const String _H2 = "h2";

        /// <summary>
        /// The h3 tag.
        /// </summary>
        public static readonly String H3 = _H3;
        internal const String _H3 = "h3";

        /// <summary>
        /// The h4 tag.
        /// </summary>
        public static readonly String H4 = _H4;
        internal const String _H4 = "h4";

        /// <summary>
        /// The h5 tag.
        /// </summary>
        public static readonly String H5 = _H5;
        internal const String _H5 = "h5";

        /// <summary>
        /// The h6 tag.
        /// </summary>
        public static readonly String H6 = _H6;
        internal const String _H6 = "h6";

        /// <summary>
        /// The div tag.
        /// </summary>
        public static readonly String Div = _Div;
        internal const String _Div = "div";

        /// <summary>
        /// The quote tag.
        /// </summary>
        public static readonly String Quote = _Quote;
        internal const String _Quote = "quote";

        /// <summary>
        /// The blockquote tag.
        /// </summary>
        public static readonly String BlockQuote = _BlockQuote;
        internal const String _BlockQuote = "blockquote";

        /// <summary>
        /// The q tag.
        /// </summary>
        public static readonly String Q = _Q;
        internal const String _Q = "q";

        /// <summary>
        /// The base tag.
        /// </summary>
        public static readonly String Base = _Base;
        internal const String _Base = "base";

        /// <summary>
        /// The basefont tag.
        /// </summary>
        public static readonly String BaseFont = _BaseFont;
        internal const String _BaseFont = "basefont";

        /// <summary>
        /// The a tag.
        /// </summary>
        public static readonly String A = _A;
        internal const String _A = "a";

        /// <summary>
        /// The area tag.
        /// </summary>
        public static readonly String Area = _Area;
        internal const String _Area = "area";

        /// <summary>
        /// The button tag.
        /// </summary>
        public static readonly String Button = _Button;
        internal const String _Button = "button";

        /// <summary>
        /// The cite tag.
        /// </summary>
        public static readonly String Cite = _Cite;
        internal const String _Cite = "cite";

        /// <summary>
        /// The main tag.
        /// </summary>
        public static readonly String Main = _Main;
        internal const String _Main = "main";

        /// <summary>
        /// The summary tag.
        /// </summary>
        public static readonly String Summary = _Summary;
        internal const String _Summary = "summary";

        /// <summary>
        /// The xmp tag.
        /// </summary>
        public static readonly String Xmp = _Xmp;
        internal const String _Xmp = "xmp";

        /// <summary>
        /// The br tag.
        /// </summary>
        public static readonly String Br = _Br;
        internal const String _Br = "br";

        /// <summary>
        /// The wbr tag.
        /// </summary>
        public static readonly String Wbr = _Wbr;
        internal const String _Wbr = "wbr";

        /// <summary>
        /// The hr tag.
        /// </summary>
        public static readonly String Hr = _Hr;
        internal const String _Hr = "hr";

        /// <summary>
        /// The dir tag.
        /// </summary>
        public static readonly String Dir = _Dir;
        internal const String _Dir = "dir";

        /// <summary>
        /// The center tag.
        /// </summary>
        public static readonly String Center = _Center;
        internal const String _Center = "center";

        /// <summary>
        /// The listing tag.
        /// </summary>
        public static readonly String Listing = _Listing;
        internal const String _Listing = "listing";

        /// <summary>
        /// The img tag.
        /// </summary>
        public static readonly String Img = _Img;
        internal const String _Img = "img";

        /// <summary>
        /// The image tag (this is not the right tag).
        /// </summary>
        public static readonly String Image = _Image;
        internal const String _Image = "image";

        /// <summary>
        /// The nav tag.
        /// </summary>
        public static readonly String Nav = _Nav;
        internal const String _Nav = "nav";

        /// <summary>
        /// The address tag.
        /// </summary>
        public static readonly String Address = _Address;
        internal const String _Address = "address";

        /// <summary>
        /// The article tag.
        /// </summary>
        public static readonly String Article = _Article;
        internal const String _Article = "article";

        /// <summary>
        /// The aside tag.
        /// </summary>
        public static readonly String Aside = _Aside;
        internal const String _Aside = "aside";

        /// <summary>
        /// The figcaption tag.
        /// </summary>
        public static readonly String Figcaption = _Figcaption;
        internal const String _Figcaption = "figcaption";

        /// <summary>
        /// The figure tag.
        /// </summary>
        public static readonly String Figure = _Figure;
        internal const String _Figure = "figure";

        /// <summary>
        /// The section tag.
        /// </summary>
        public static readonly String Section = _Section;
        internal const String _Section = "section";

        /// <summary>
        /// The footer tag.
        /// </summary>
        public static readonly String Footer = _Footer;
        internal const String _Footer = "footer";

        /// <summary>
        /// The header tag.
        /// </summary>
        public static readonly String Header = _Header;
        internal const String _Header = "header";

        /// <summary>
        /// The hgroup tag.
        /// </summary>
        public static readonly String Hgroup = _Hgroup;
        internal const String _Hgroup = "hgroup";

        /// <summary>
        /// The plaintext tag.
        /// </summary>
        public static readonly String Plaintext = _Plaintext;
        internal const String _Plaintext = "plaintext";

        /// <summary>
        /// The time tag.
        /// </summary>
        public static readonly String Time = _Time;
        internal const String _Time = "time";

        /// <summary>
        /// The progress tag.
        /// </summary>
        public static readonly String Progress = _Progress;
        internal const String _Progress = "progress";

        /// <summary>
        /// The meter tag.
        /// </summary>
        public static readonly String Meter = _Meter;
        internal const String _Meter = "meter";

        /// <summary>
        /// The output tag.
        /// </summary>
        public static readonly String Output = _Output;
        internal const String _Output = "output";

        /// <summary>
        /// The map tag.
        /// </summary>
        public static readonly String Map = _Map;
        internal const String _Map = "map";

        /// <summary>
        /// The picture tag.
        /// </summary>
        public static readonly String Picture = _Picture;
        internal const String _Picture = "picture";

        /// <summary>
        /// The mark tag.
        /// </summary>
        public static readonly String Mark = _Mark;
        internal const String _Mark = "mark";

        /// <summary>
        /// The dfn tag.
        /// </summary>
        public static readonly String Dfn = _Dfn;
        internal const String _Dfn = "dfn";

        /// <summary>
        /// The kbd tag.
        /// </summary>
        public static readonly String Kbd = _Kbd;
        internal const String _Kbd = "kbd";

        /// <summary>
        /// The samp tag.
        /// </summary>
        public static readonly String Samp = _Samp;
        internal const String _Samp = "samp";

        /// <summary>
        /// The abbr tag.
        /// </summary>
        public static readonly String Abbr = _Abbr;
        internal const String _Abbr = "abbr";

        /// <summary>
        /// The bdi tag.
        /// </summary>
        public static readonly String Bdi = _Bdi;
        internal const String _Bdi = "bdi";

        /// <summary>
        /// The bdo tag.
        /// </summary>
        public static readonly String Bdo = _Bdo;
        internal const String _Bdo = "bdo";

        #endregion

        #region MathML Tags

        /// <summary>
        /// The math tag.
        /// </summary>
        public static readonly String Math = _Math;
        internal const String _Math = "math";

        /// <summary>
        /// The mi tag.
        /// </summary>
        public static readonly String Mi = _Mi;
        internal const String _Mi = "mi";

        /// <summary>
        /// The mo tag.
        /// </summary>
        public static readonly String Mo = _Mo;
        internal const String _Mo = "mo";

        /// <summary>
        /// The mn tag.
        /// </summary>
        public static readonly String Mn = _Mn;
        internal const String _Mn = "mn";

        /// <summary>
        /// The ms tag.
        /// </summary>
        public static readonly String Ms = _Ms;
        internal const String _Ms = "ms";

        /// <summary>
        /// The mtext tag.
        /// </summary>
        public static readonly String Mtext = _Mtext;
        internal const String _Mtext = "mtext";

        /// <summary>
        /// The annotation-xml tag.
        /// </summary>
        public static readonly String AnnotationXml = _AnnotationXml;
        internal const String _AnnotationXml = "annotation-xml";

        #endregion

        #region SVG Tags

        /// <summary>
        /// The svg tag.
        /// </summary>
        public static readonly String Svg = _Svg;
        internal const String _Svg = "svg";

        /// <summary>
        /// The foreignObject tag.
        /// </summary>
        public static readonly String ForeignObject = _ForeignObject;
        internal const String _ForeignObject = "foreignObject";

        /// <summary>
        /// The desc tag.
        /// </summary>
        public static readonly String Desc = _Desc;
        internal const String _Desc = "desc";

        /// <summary>
        /// The circle tag.
        /// </summary>
        public static readonly String Circle = _Circle;
        internal const String _Circle = "circle";

        #endregion

        #region XML Tags

        /// <summary>
        /// The xml tag.
        /// </summary>
        public static readonly String Xml = _Xml;
        internal const String _Xml = "xml";

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