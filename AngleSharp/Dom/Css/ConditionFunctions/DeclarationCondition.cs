namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;

    sealed class DeclarationCondition : CssNode, IConditionFunction
    {
        readonly CssProperty _property;
        readonly CssValue _value;

        public DeclarationCondition(CssProperty property, CssValue value)
        {
            _property = property;
            _value = value;
        }

        public Boolean Check()
        {
            return (_property is CssUnknownProperty == false) && _property.TrySetValue(_value);
        }

        public override String ToCss(IStyleFormatter formatter)
        {
            var content = formatter.Declaration(_property.Name, _value.CssText, _property.IsImportant);
            return String.Empty.CssFunction(content);
        }
    }
}
