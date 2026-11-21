namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    sealed class AttrInListSelector : BaseAttrSelector, ISelector
    {
        private readonly String _value;

        public AttrInListSelector(String name, String value, String? prefix = null, Boolean insensitive = false)
            : base(name, prefix, insensitive)
        {
            _value = value;
        }

        public String Text => String.Concat("[", Attribute, "~=", _value.CssString(), Modifier, "]");

        public void Accept(ISelectorVisitor visitor) => visitor.Attribute(Attribute, "~=", _value);

        public Boolean Match(IElement element, IElement? scope)
        {
            if (!String.IsNullOrEmpty(_value))
            {
                var actual = element.GetAttribute(Name);
                return actual is not null && ContainsToken(actual, _value, Comparison);
            }

            return false;
        }

        // The same as actual.SplitSpaces().Contains(value), which allocated an array and a
        // string per token for every element tested
        private static Boolean ContainsToken(String list, String value, StringComparison comparison)
        {
            var i = 0;
            var n = list.Length;

            while (i < n)
            {
                while (i < n && list[i].IsSpaceCharacter())
                {
                    i++;
                }

                var start = i;

                while (i < n && !list[i].IsSpaceCharacter())
                {
                    i++;
                }

                if (i - start == value.Length && list.AsSpan(start, i - start).Equals(value.AsSpan(), comparison))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
