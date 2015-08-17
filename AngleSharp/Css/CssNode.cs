namespace AngleSharp.Css
{
    public class CssNode
    {
        public CssNode(TextRange range)
        {
            Range = range;
        }

        public TextRange Range
        {
            get;
            private set;
        }
    }
}
