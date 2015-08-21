namespace AngleSharp.Css
{
    using AngleSharp.Parser.Css;
    using System.Collections.Generic;

    class CssNode
    {
        List<CssToken> _trivia;
        TextPosition _start;
        TextPosition _end;

        public List<CssToken> Trivia
        {
            get { return _trivia; }
            set { _trivia = value; }
        }

        public TextPosition Start
        {
            get { return _start; }
            set { _start = value; }
        }

        public TextPosition End
        {
            get { return _end; }
            set { _end = value; }
        }
    }
}
