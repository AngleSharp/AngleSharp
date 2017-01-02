namespace AngleSharp.Html.Parser
{
    using AngleSharp.Dom;
    using System;
    using System.Text;

    static class HtmlTagNameLookup
    {
        public static String TryGetWellKnownTagName(StringBuilder builder)
        {
            switch (builder.Length)
            {
                case 1:
                    switch (builder[0])
                    {
                        case 'p':
                            return TagNames.P;
                        case 'b':
                            return TagNames.B;
                        case 'i':
                            return TagNames.I;
                        case 's':
                            return TagNames.S;
                        case 'u':
                            return TagNames.U;
                        case 'q':
                            return TagNames.Q;
                        case 'a':
                            return TagNames.A;
                        default:
                            return null;
                    }
                case 2:
                    switch (builder[1])
                    {
                        case 'p':
                            return CharsAreEqual(builder, TagNames.Rp) ? TagNames.Rp : null;
                        case 't':
                            switch (builder[0])
                            {
                                case 'r':
                                    return CharsAreEqual(builder, TagNames.Rt) ? TagNames.Rt : null;
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Dt) ? TagNames.Dt : null;
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Tt) ? TagNames.Tt : null;
                                default:
                                    return null;
                            }
                        case 'b':
                            return CharsAreEqual(builder, TagNames.Rb) ? TagNames.Rb : null;
                        case 'h':
                            return CharsAreEqual(builder, TagNames.Th) ? TagNames.Th : null;
                        case 'd':
                            switch (builder[0])
                            {
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Td) ? TagNames.Td : null;
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Dd) ? TagNames.Dd : null;
                                default:
                                    return null;
                            }
                        case 'r':
                            switch (builder[0])
                            {
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Tr) ? TagNames.Tr : null;
                                case 'b':
                                    return CharsAreEqual(builder, TagNames.Br) ? TagNames.Br : null;
                                case 'h':
                                    return CharsAreEqual(builder, TagNames.Hr) ? TagNames.Hr : null;
                                default:
                                    return null;
                            }
                        case 'l':
                            switch (builder[0])
                            {
                                case 'o':
                                    return CharsAreEqual(builder, TagNames.Ol) ? TagNames.Ol : null;
                                case 'u':
                                    return CharsAreEqual(builder, TagNames.Ul) ? TagNames.Ul : null;
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Dl) ? TagNames.Dl : null;
                                default:
                                    return null;
                            }
                        case 'i':
                            switch (builder[0])
                            {
                                case 'l':
                                    return CharsAreEqual(builder, TagNames.Li) ? TagNames.Li : null;
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Mi) ? TagNames.Mi : null;
                                default:
                                    return null;
                            }
                        case 'm':
                            return CharsAreEqual(builder, TagNames.Em) ? TagNames.Em : null;
                        case '1':
                            return CharsAreEqual(builder, TagNames.H1) ? TagNames.H1 : null;
                        case '2':
                            return CharsAreEqual(builder, TagNames.H2) ? TagNames.H2 : null;
                        case '3':
                            return CharsAreEqual(builder, TagNames.H3) ? TagNames.H3 : null;
                        case '4':
                            return CharsAreEqual(builder, TagNames.H4) ? TagNames.H4 : null;
                        case '5':
                            return CharsAreEqual(builder, TagNames.H5) ? TagNames.H5 : null;
                        case '6':
                            return CharsAreEqual(builder, TagNames.H6) ? TagNames.H6 : null;
                        case 'o':
                            return CharsAreEqual(builder, TagNames.Mo) ? TagNames.Mo : null;
                        case 'n':
                            return CharsAreEqual(builder, TagNames.Mn) ? TagNames.Mn : null;
                        case 's':
                            return CharsAreEqual(builder, TagNames.Ms) ? TagNames.Ms : null;
                        default:
                            return null;
                    }
                case 3:
                    switch (builder[0])
                    {
                        case 'v':
                            return CharsAreEqual(builder, TagNames.Var) ? TagNames.Var : null;
                        case 's':
                            switch (builder[2])
                            {
                                case 'b':
                                    return CharsAreEqual(builder, TagNames.Sub) ? TagNames.Sub : null;
                                case 'p':
                                    return CharsAreEqual(builder, TagNames.Sup) ? TagNames.Sup : null;
                                case 'g':
                                    return CharsAreEqual(builder, TagNames.Svg) ? TagNames.Svg : null;
                                default:
                                    return null;
                            }
                        case 'r':
                            return CharsAreEqual(builder, TagNames.Rtc) ? TagNames.Rtc : null;
                        case 'i':
                            switch (builder[1])
                            {
                                case 'n':
                                    return CharsAreEqual(builder, TagNames.Ins) ? TagNames.Ins : null;
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Img) ? TagNames.Img : null;
                                default:
                                    return null;
                            }
                        case 'd':
                            switch (builder[2])
                            {
                                case 'l':
                                    return CharsAreEqual(builder, TagNames.Del) ? TagNames.Del : null;
                                case 'v':
                                    return CharsAreEqual(builder, TagNames.Div) ? TagNames.Div : null;
                                case 'r':
                                    return CharsAreEqual(builder, TagNames.Dir) ? TagNames.Dir : null;
                                case 'n':
                                    return CharsAreEqual(builder, TagNames.Dfn) ? TagNames.Dfn : null;
                                default:
                                    return null;
                            }
                        case 'c':
                            return CharsAreEqual(builder, TagNames.Col) ? TagNames.Col : null;
                        case 'p':
                            return CharsAreEqual(builder, TagNames.Pre) ? TagNames.Pre : null;
                        case 'b':
                            switch (builder[2])
                            {
                                case 'g':
                                    return CharsAreEqual(builder, TagNames.Big) ? TagNames.Big : null;
                                case 'i':
                                    return CharsAreEqual(builder, TagNames.Bdi) ? TagNames.Bdi : null;
                                case 'o':
                                    return CharsAreEqual(builder, TagNames.Bdo) ? TagNames.Bdo : null;
                                default:
                                    return null;
                            }
                        case 'x':
                            switch (builder[2])
                            {
                                case 'p':
                                    return CharsAreEqual(builder, TagNames.Xmp) ? TagNames.Xmp : null;
                                case 'l':
                                    return CharsAreEqual(builder, TagNames.Xml) ? TagNames.Xml : null;
                                default:
                                    return null;
                            }
                        case 'w':
                            return CharsAreEqual(builder, TagNames.Wbr) ? TagNames.Wbr : null;
                        case 'n':
                            return CharsAreEqual(builder, TagNames.Nav) ? TagNames.Nav : null;
                        case 'm':
                            return CharsAreEqual(builder, TagNames.Map) ? TagNames.Map : null;
                        case 'k':
                            return CharsAreEqual(builder, TagNames.Kbd) ? TagNames.Kbd : null;
                        default:
                            return null;
                    }
                case 4:
                    switch (builder[3])
                    {
                        case 'l':
                            return CharsAreEqual(builder, TagNames.Html) ? TagNames.Html : null;
                        case 'y':
                            switch (builder[0])
                            {
                                case 'b':
                                    return CharsAreEqual(builder, TagNames.Body) ? TagNames.Body : null;
                                case 'r':
                                    return CharsAreEqual(builder, TagNames.Ruby) ? TagNames.Ruby : null;
                                default:
                                    return null;
                            }
                        case 'd':
                            return CharsAreEqual(builder, TagNames.Head) ? TagNames.Head : null;
                        case 'a':
                            switch (builder[0])
                            {
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Meta) ? TagNames.Meta : null;
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Data) ? TagNames.Data : null;
                                case 'a':
                                    return CharsAreEqual(builder, TagNames.Area) ? TagNames.Area : null;
                                default:
                                    return null;
                            }
                        case 'u':
                            return CharsAreEqual(builder, TagNames.Menu) ? TagNames.Menu : null;
                        case 't':
                            switch (builder[0])
                            {
                                case 'f':
                                    return CharsAreEqual(builder, TagNames.Font) ? TagNames.Font : null;
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Slot) ? TagNames.Slot : null;
                                default:
                                    return null;
                            }
                        case 'n':
                            switch (builder[0])
                            {
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Span) ? TagNames.Span : null;
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Main) ? TagNames.Main : null;
                                default:
                                    return null;
                            }
                        case 'm':
                            return CharsAreEqual(builder, TagNames.Form) ? TagNames.Form : null;
                        case 'e':
                            switch (builder[2])
                            {
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Code) ? TagNames.Code : null;
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Base) ? TagNames.Base : null;
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Cite) ? TagNames.Cite : null;
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Time) ? TagNames.Time : null;
                                default:
                                    return null;
                            }
                        case 'r':
                            switch (builder[0])
                            {
                                case 'n':
                                    return CharsAreEqual(builder, TagNames.NoBr) ? TagNames.NoBr : null;
                                case 'a':
                                    return CharsAreEqual(builder, TagNames.Abbr) ? TagNames.Abbr : null;
                                default:
                                    return null;
                            }
                        case 'k':
                            switch (builder[0])
                            {
                                case 'l':
                                    return CharsAreEqual(builder, TagNames.Link) ? TagNames.Link : null;
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Mark) ? TagNames.Mark : null;
                                default:
                                    return null;
                            }
                        case 'p':
                            return CharsAreEqual(builder, TagNames.Samp) ? TagNames.Samp : null;
                        case 'h':
                            return CharsAreEqual(builder, TagNames.Math) ? TagNames.Math : null;
                        case 'c':
                            return CharsAreEqual(builder, TagNames.Desc) ? TagNames.Desc : null;
                        default:
                            return null;
                    }
                case 5:
                    switch (builder[1])
                    {
                        case 'i':
                            switch (builder[0])
                            {
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Title) ? TagNames.Title : null;
                                case 'v':
                                    return CharsAreEqual(builder, TagNames.Video) ? TagNames.Video : null;
                                default:
                                    return null;
                            }
                        case 't':
                            switch (builder[0])
                            {
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Style) ? TagNames.Style : null;
                                case 'm':
                                    return CharsAreEqual(builder, TagNames.Mtext) ? TagNames.Mtext : null;
                                default:
                                    return null;
                            }
                        case 'm':
                            switch (builder[0])
                            {
                                case 'e':
                                    return CharsAreEqual(builder, TagNames.Embed) ? TagNames.Embed : null;
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Small) ? TagNames.Small : null;
                                case 'i':
                                    return CharsAreEqual(builder, TagNames.Image) ? TagNames.Image : null;
                                default:
                                    return null;
                            }
                        case 'a':
                            switch (builder[0])
                            {
                                case 'p':
                                    return CharsAreEqual(builder, TagNames.Param) ? TagNames.Param : null;
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Table) ? TagNames.Table : null;
                                case 'l':
                                    return CharsAreEqual(builder, TagNames.Label) ? TagNames.Label : null;
                                default:
                                    return null;
                            }
                        case 'h':
                            return CharsAreEqual(builder, TagNames.Thead) ? TagNames.Thead : null;
                        case 'b':
                            return CharsAreEqual(builder, TagNames.Tbody) ? TagNames.Tbody : null;
                        case 'f':
                            return CharsAreEqual(builder, TagNames.Tfoot) ? TagNames.Tfoot : null;
                        case 'n':
                            return CharsAreEqual(builder, TagNames.Input) ? TagNames.Input : null;
                        case 'r':
                            switch (builder[0])
                            {
                                case 'f':
                                    return CharsAreEqual(builder, TagNames.Frame) ? TagNames.Frame : null;
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Track) ? TagNames.Track : null;
                                default:
                                    return null;
                            }
                        case 'u':
                            switch (builder[0])
                            {
                                case 'a':
                                    return CharsAreEqual(builder, TagNames.Audio) ? TagNames.Audio : null;
                                case 'q':
                                    return CharsAreEqual(builder, TagNames.Quote) ? TagNames.Quote : null;
                                default:
                                    return null;
                            }
                        case 's':
                            return CharsAreEqual(builder, TagNames.Aside) ? TagNames.Aside : null;
                        case 'e':
                            return CharsAreEqual(builder, TagNames.Meter) ? TagNames.Meter : null;
                        default:
                            return null;
                    }
                case 6:
                    switch (builder[3])
                    {
                        case 'i':
                            switch (builder[1])
                            {
                                case 'c':
                                    return CharsAreEqual(builder, TagNames.Script) ? TagNames.Script : null;
                                case 't':
                                    return CharsAreEqual(builder, TagNames.Strike) ? TagNames.Strike : null;
                                case 'p':
                                    return CharsAreEqual(builder, TagNames.Option) ? TagNames.Option : null;
                                default:
                                    return null;
                            }
                        case 'l':
                            switch (builder[0])
                            {
                                case 'a':
                                    return CharsAreEqual(builder, TagNames.Applet) ? TagNames.Applet : null;
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Dialog) ? TagNames.Dialog : null;
                                default:
                                    return null;
                            }
                        case 'e':
                            switch (builder[0])
                            {
                                case 'o':
                                    return CharsAreEqual(builder, TagNames.Object) ? TagNames.Object : null;
                                case 'l':
                                    return CharsAreEqual(builder, TagNames.Legend) ? TagNames.Legend : null;
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Select) ? TagNames.Select : null;
                                default:
                                    return null;
                            }
                        case 'v':
                            return CharsAreEqual(builder, TagNames.Canvas) ? TagNames.Canvas : null;
                        case 'g':
                            return CharsAreEqual(builder, TagNames.Keygen) ? TagNames.Keygen : null;
                        case 'o':
                            switch (builder[0])
                            {
                                case 's':
                                    return CharsAreEqual(builder, TagNames.Strong) ? TagNames.Strong : null;
                                case 'h':
                                    return CharsAreEqual(builder, TagNames.Hgroup) ? TagNames.Hgroup : null;
                                default:
                                    return null;
                            }
                        case 'a':
                            return CharsAreEqual(builder, TagNames.Iframe) ? TagNames.Iframe : null;
                        case 'r':
                            return CharsAreEqual(builder, TagNames.Source) ? TagNames.Source : null;
                        case 't':
                            switch (builder[0])
                            {
                                case 'b':
                                    return CharsAreEqual(builder, TagNames.Button) ? TagNames.Button : null;
                                case 'c':
                                    return CharsAreEqual(builder, TagNames.Center) ? TagNames.Center : null;
                                case 'f':
                                    return CharsAreEqual(builder, TagNames.Footer) ? TagNames.Footer : null;
                                default:
                                    return null;
                            }
                        case 'u':
                            return CharsAreEqual(builder, TagNames.Figure) ? TagNames.Figure : null;
                        case 'd':
                            return CharsAreEqual(builder, TagNames.Header) ? TagNames.Header : null;
                        case 'p':
                            return CharsAreEqual(builder, TagNames.Output) ? TagNames.Output : null;
                        case 'c':
                            return CharsAreEqual(builder, TagNames.Circle) ? TagNames.Circle : null;
                        default:
                            return null;
                    }
                case 7:
                    switch (builder[0])
                    {
                        case 'D':
                            return CharsAreEqual(builder, TagNames.Doctype) ? TagNames.Doctype : null;
                        case 'b':
                            return CharsAreEqual(builder, TagNames.Bgsound) ? TagNames.Bgsound : null;
                        case 'n':
                            return CharsAreEqual(builder, TagNames.NoEmbed) ? TagNames.NoEmbed : null;
                        case 'm':
                            return CharsAreEqual(builder, TagNames.Marquee) ? TagNames.Marquee : null;
                        case 'c':
                            return CharsAreEqual(builder, TagNames.Caption) ? TagNames.Caption : null;
                        case 'd':
                            return CharsAreEqual(builder, TagNames.Details) ? TagNames.Details : null;
                        case 'i':
                            return CharsAreEqual(builder, TagNames.IsIndex) ? TagNames.IsIndex : null;
                        case 's':
                            switch (builder[1])
                            {
                                case 'u':
                                    return CharsAreEqual(builder, TagNames.Summary) ? TagNames.Summary : null;
                                case 'e':
                                    return CharsAreEqual(builder, TagNames.Section) ? TagNames.Section : null;
                                default:
                                    return null;
                            }
                        case 'l':
                            return CharsAreEqual(builder, TagNames.Listing) ? TagNames.Listing : null;
                        case 'a':
                            switch (builder[1])
                            {
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Address) ? TagNames.Address : null;
                                case 'r':
                                    return CharsAreEqual(builder, TagNames.Article) ? TagNames.Article : null;
                                default:
                                    return null;
                            }
                        case 'p':
                            return CharsAreEqual(builder, TagNames.Picture) ? TagNames.Picture : null;
                        default:
                            return null;
                    }
                case 8:
                    switch (builder[2])
                    {
                        case 's':
                            switch (builder[0])
                            {
                                case 'n':
                                    return CharsAreEqual(builder, TagNames.NoScript) ? TagNames.NoScript : null;
                                case 'b':
                                    return CharsAreEqual(builder, TagNames.BaseFont) ? TagNames.BaseFont : null;
                                default:
                                    return null;
                            }
                        case 'f':
                            return CharsAreEqual(builder, TagNames.NoFrames) ? TagNames.NoFrames : null;
                        case 'n':
                            return CharsAreEqual(builder, TagNames.MenuItem) ? TagNames.MenuItem : null;
                        case 'm':
                            return CharsAreEqual(builder, TagNames.Template) ? TagNames.Template : null;
                        case 'l':
                            return CharsAreEqual(builder, TagNames.Colgroup) ? TagNames.Colgroup : null;
                        case 'x':
                            return CharsAreEqual(builder, TagNames.Textarea) ? TagNames.Textarea : null;
                        case 'e':
                            return CharsAreEqual(builder, TagNames.Fieldset) ? TagNames.Fieldset : null;
                        case 't':
                            switch (builder[0])
                            {
                                case 'd':
                                    return CharsAreEqual(builder, TagNames.Datalist) ? TagNames.Datalist : null;
                                case 'o':
                                    return CharsAreEqual(builder, TagNames.Optgroup) ? TagNames.Optgroup : null;
                                default:
                                    return null;
                            }
                        case 'a':
                            return CharsAreEqual(builder, TagNames.Frameset) ? TagNames.Frameset : null;
                        case 'o':
                            return CharsAreEqual(builder, TagNames.Progress) ? TagNames.Progress : null;
                        default:
                            return null;
                    }
                case 9:
                    return CharsAreEqual(builder, TagNames.Plaintext) ? TagNames.Plaintext : null;
                case 10:
                    switch (builder[0])
                    {
                        case 'b':
                            return CharsAreEqual(builder, TagNames.BlockQuote) ? TagNames.BlockQuote : null;
                        case 'f':
                            return CharsAreEqual(builder, TagNames.Figcaption) ? TagNames.Figcaption : null;
                        default:
                            return null;
                    }
                case 13:
                    return CharsAreEqual(builder, TagNames.ForeignObject) ? TagNames.ForeignObject : null;
                case 14:
                    return CharsAreEqual(builder, TagNames.AnnotationXml) ? TagNames.AnnotationXml : null;
                default:
                    return null;
            }
        }

        private static Boolean CharsAreEqual( StringBuilder builder, String tagName )
        {
            for ( int i = 0; i < tagName.Length; i++ )
            {
                if ( tagName[ i ] != builder[ i ] )
                    return false;
            }

            return true;
        }
    }
}
