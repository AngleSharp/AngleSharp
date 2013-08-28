using System;

namespace AngleSharp
{
    /// <summary>
    /// The collection of (known / used) tags.
    /// </summary>
    static class Tags
    {
        #region HTML Tags

        /// <summary>
        /// The html tag.
        /// </summary>
        public const String HTML = "html";

        /// <summary>
        /// The body tag.
        /// </summary>
        public const String BODY = "body";

        /// <summary>
        /// The head tag.
        /// </summary>
        public const String HEAD = "head";

        /// <summary>
        /// The meta tag.
        /// </summary>
        public const String META = "meta";

        /// <summary>
        /// The title tag.
        /// </summary>
        public const String TITLE = "title";

        /// <summary>
        /// The bgsound tag.
        /// </summary>
        public const String BGSOUND = "bgsound";

        /// <summary>
        /// The script tag.
        /// </summary>
        public const String SCRIPT = "script";

        /// <summary>
        /// The style tag.
        /// </summary>
        public const String STYLE = "style";

        /// <summary>
        /// The noembed tag.
        /// </summary>
        public const string NOEMBED = "noembed";

        /// <summary>
        /// The noscript tag.
        /// </summary>
        public const string NOSCRIPT = "noscript";

        /// <summary>
        /// The noframes tag.
        /// </summary>
        public const string NOFRAMES = "noframes";

        /// <summary>
        /// The menu tag.
        /// </summary>
        public const String MENU = "menu";

        /// <summary>
        /// The menuitem tag.
        /// </summary>
        public const String MENUITEM = "menuitem";

        /// <summary>
        /// The var tag.
        /// </summary>
        public const String VAR = "var";

        /// <summary>
        /// The ruby tag.
        /// </summary>
        public const String RUBY = "ruby";

        /// <summary>
        /// The sub tag.
        /// </summary>
        public const String SUB = "sub";

        /// <summary>
        /// The sup tag.
        /// </summary>
        public const String SUP = "sup";

        /// <summary>
        /// The rp tag.
        /// </summary>
        public const String RP = "rp";

        /// <summary>
        /// The rt tag.
        /// </summary>
        public const String RT = "rt";

        /// <summary>
        /// The applet tag.
        /// </summary>
        public const String APPLET = "applet";

        /// <summary>
        /// The embed tag.
        /// </summary>
        public const String EMBED = "embed";

        /// <summary>
        /// The marquee tag.
        /// </summary>
        public const String MARQUEE = "marquee";

        /// <summary>
        /// The param tag.
        /// </summary>
        public const String PARAM = "param";

        /// <summary>
        /// The object tag.
        /// </summary>
        public const String OBJECT = "object";

        /// <summary>
        /// The canvas tag.
        /// </summary>
        public const String CANVAS = "canvas";

        /// <summary>
        /// The font tag.
        /// </summary>
        public const String FONT = "font";

        /// <summary>
        /// The ins tag.
        /// </summary>
        public const String INS = "ins";

        /// <summary>
        /// The del tag.
        /// </summary>
        public const String DEL = "del";

        /// <summary>
        /// The template tag.
        /// </summary>
        public const String TEMPLATE = "template";

        /// <summary>
        /// The caption tag.
        /// </summary>
        public const String CAPTION = "caption";

        /// <summary>
        /// The col tag.
        /// </summary>
        public const String COL = "col";

        /// <summary>
        /// The colgroup tag.
        /// </summary>
        public const String COLGROUP = "colgroup";

        /// <summary>
        /// The table tag.
        /// </summary>
        public const String TABLE = "table";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public const String THEAD = "thead";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public const String TBODY = "tbody";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public const String TFOOT = "tfoot";

        /// <summary>
        /// The th tag.
        /// </summary>
        public const String TH = "th";

        /// <summary>
        /// The td tag.
        /// </summary>
        public const String TD = "td";

        /// <summary>
        /// The tr tag.
        /// </summary>
        public const String TR = "tr";

        /// <summary>
        /// The input tag.
        /// </summary>
        public const String INPUT = "input";

        /// <summary>
        /// The keygen tag.
        /// </summary>
        public const String KEYGEN = "keygen";

        /// <summary>
        /// The textarea tag.
        /// </summary>
        public const String TEXTAREA = "textarea";

        /// <summary>
        /// The p tag.
        /// </summary>
        public const String P = "p";

        /// <summary>
        /// The span tag.
        /// </summary>
        public const String SPAN = "span";

        /// <summary>
        /// The dialog tag.
        /// </summary>
        public const String DIALOG = "dialog";

        /// <summary>
        /// The fieldset tag.
        /// </summary>
        public const String FIELDSET = "fieldset";

        /// <summary>
        /// The legend tag.
        /// </summary>
        public const String LEGEND = "legend";

        /// <summary>
        /// The label tag.
        /// </summary>
        public const String LABEL = "label";

        /// <summary>
        /// The details tag.
        /// </summary>
        public const String DETAILS = "details";

        /// <summary>
        /// The form tag.
        /// </summary>
        public const String FORM = "form";

        /// <summary>
        /// The isindex tag.
        /// </summary>
        public const String ISINDEX = "isindex";

        /// <summary>
        /// The pre tag.
        /// </summary>
        public const String PRE = "pre";

        /// <summary>
        /// The datalist tag.
        /// </summary>
        public const String DATALIST = "datalist";

        /// <summary>
        /// The ol tag.
        /// </summary>
        public const String OL = "ol";

        /// <summary>
        /// The tag ul.
        /// </summary>
        public const String UL = "ul";

        /// <summary>
        /// The dl tag.
        /// </summary>
        public const String DL = "dl";

        /// <summary>
        /// The li tag.
        /// </summary>
        public const String LI = "li";

        /// <summary>
        /// The dd tag.
        /// </summary>
        public const String DD = "dd";

        /// <summary>
        /// The dt tag.
        /// </summary>
        public const String DT = "dt";

        /// <summary>
        /// The b tag.
        /// </summary>
        public const String B = "b";

        /// <summary>
        /// The big tag.
        /// </summary>
        public const String BIG = "big";

        /// <summary>
        /// The strike tag.
        /// </summary>
        public const String STRIKE = "strike";

        /// <summary>
        /// The code tag.
        /// </summary>
        public const String CODE = "code";

        /// <summary>
        /// The em tag.
        /// </summary>
        public const String EM = "em";

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
        public const String SMALL = "small";

        /// <summary>
        /// The strong tag.
        /// </summary>
        public const String STRONG = "strong";

        /// <summary>
        /// The u tag.
        /// </summary>
        public const String U = "u";

        /// <summary>
        /// The tt tag.
        /// </summary>
        public const String TT = "tt";

        /// <summary>
        /// The nobr tag.
        /// </summary>
        public const String NOBR = "nobr";

        /// <summary>
        /// The select tag.
        /// </summary>
        public const String SELECT = "select";

        /// <summary>
        /// The option tag.
        /// </summary>
        public const String OPTION = "option";

        /// <summary>
        /// The optgroup tag.
        /// </summary>
        public const String OPTGROUP = "optgroup";

        /// <summary>
        /// The link tag.
        /// </summary>
        public const String LINK = "link";

        /// <summary>
        /// The frameset tag.
        /// </summary>
        public const String FRAMESET = "frameset";

        /// <summary>
        /// The frame tag.
        /// </summary>
        public const String FRAME = "frame";

        /// <summary>
        /// The iframe tag.
        /// </summary>
        public const String IFRAME = "iframe";

        /// <summary>
        /// The audio tag.
        /// </summary>
        public const String AUDIO = "audio";

        /// <summary>
        /// The video tag.
        /// </summary>
        public const String VIDEO = "video";

        /// <summary>
        /// The source tag.
        /// </summary>
        public const String SOURCE = "source";

        /// <summary>
        /// The track tag.
        /// </summary>
        public const String TRACK = "track";

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
        public const String DIV = "div";

        /// <summary>
        /// The quote tag.
        /// </summary>
        public const String QUOTE = "quote";

        /// <summary>
        /// The blockquote tag.
        /// </summary>
        public const String BLOCKQUOTE = "blockquote";

        /// <summary>
        /// The q tag.
        /// </summary>
        public const String Q = "q";

        /// <summary>
        /// The base tag.
        /// </summary>
        public const String BASE = "base";

        /// <summary>
        /// The basefont tag.
        /// </summary>
        public const String BASEFONT = "basefont";

        /// <summary>
        /// The a tag.
        /// </summary>
        public const String A = "a";

        /// <summary>
        /// The area tag.
        /// </summary>
        public const String AREA = "area";

        /// <summary>
        /// The button tag.
        /// </summary>
        public const String BUTTON = "button";

        /// <summary>
        /// The cite tag.
        /// </summary>
        public const String CITE = "cite";

        /// <summary>
        /// The main tag.
        /// </summary>
        public const String MAIN = "main";

        /// <summary>
        /// The summary tag.
        /// </summary>
        public const String SUMMARY = "summary";

        /// <summary>
        /// The xmp tag.
        /// </summary>
        public const String XMP = "xmp";

        /// <summary>
        /// The br tag.
        /// </summary>
        public const String BR = "br";

        /// <summary>
        /// The wbr tag.
        /// </summary>
        public const String WBR = "wbr";

        /// <summary>
        /// The hr tag.
        /// </summary>
        public const String HR = "hr";

        /// <summary>
        /// The dir tag.
        /// </summary>
        public const String DIR = "dir";

        /// <summary>
        /// The center tag.
        /// </summary>
        public const String CENTER = "center";

        /// <summary>
        /// The listing tag.
        /// </summary>
        public const String LISTING = "listing";

        /// <summary>
        /// The img tag.
        /// </summary>
        public const String IMG = "img";

        /// <summary>
        /// The image tag (this is not the right tag).
        /// </summary>
        public const String IMAGE = "image";

        /// <summary>
        /// The nav tag.
        /// </summary>
        public const String NAV = "nav";

        /// <summary>
        /// The address tag.
        /// </summary>
        public const String ADDRESS = "address";

        /// <summary>
        /// The article tag.
        /// </summary>
        public const String ARTICLE = "article";

        /// <summary>
        /// The aside tag.
        /// </summary>
        public const String ASIDE = "aside";

        /// <summary>
        /// The figcaption tag.
        /// </summary>
        public const String FIGCAPTION = "figcaption";

        /// <summary>
        /// The figure tag.
        /// </summary>
        public const String FIGURE = "figure";

        /// <summary>
        /// The section tag.
        /// </summary>
        public const String SECTION = "section";

        /// <summary>
        /// The footer tag.
        /// </summary>
        public const String FOOTER = "footer";

        /// <summary>
        /// The header tag.
        /// </summary>
        public const String HEADER = "header";

        /// <summary>
        /// The hgroup tag.
        /// </summary>
        public const String HGROUP = "hgroup";

        /// <summary>
        /// The plaintext tag.
        /// </summary>
        public const String PLAINTEXT = "plaintext";

        #endregion

        #region MathML Tags

        /// <summary>
        /// The math tag.
        /// </summary>
        public const String MATH = "math";

        /// <summary>
        /// The mi tag.
        /// </summary>
        public const String MI = "mi";

        /// <summary>
        /// The mo tag.
        /// </summary>
        public const String MO = "mo";

        /// <summary>
        /// The mn tag.
        /// </summary>
        public const String MN = "mn";

        /// <summary>
        /// The ms tag.
        /// </summary>
        public const String MS = "ms";

        /// <summary>
        /// The mtext tag.
        /// </summary>
        public const String MTEXT = "mtext";

        /// <summary>
        /// The annotation-xml tag.
        /// </summary>
        public const String ANNOTATION_XML = "annotation-xml";

        #endregion

        #region SVG Tags

        /// <summary>
        /// The svg tag.
        /// </summary>
        public const String SVG = "svg";

        /// <summary>
        /// The foreignObject tag.
        /// </summary>
        public const String FOREIGNOBJECT = "foreignObject";

        /// <summary>
        /// The desc tag.
        /// </summary>
        public const String DESC = "desc";

        /// <summary>
        /// The circle tag.
        /// </summary>
        public const String CIRCLE = "circle";

        #endregion
    }
}
