namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;

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

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write("(");
            writer.Write(formatter.Declaration(_property.Name, _value.CssText, _property.IsImportant));
            writer.Write(")");
        }
    }
}
