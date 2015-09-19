namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class DeclarationCondition : IConditionFunction
    {
        readonly CssProperty _property;
        readonly CssValue _value;

        public DeclarationCondition(CssProperty property, CssValue value)
        {
            _property = property;
            _value = value;
        }

        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        public Boolean Check()
        {
            return (_property is CssUnknownProperty == false) && _property.TrySetValue(_value);
        }

        public String ToCss()
        {
            return ToCss(CssStyleFormatter.Instance);
        }

        public String ToCss(IStyleFormatter formatter)
        {
            var content = formatter.Declaration(_property.Name, _value.CssText, _property.IsImportant);
            return String.Concat("(", content, ")");
        }
    }
}
