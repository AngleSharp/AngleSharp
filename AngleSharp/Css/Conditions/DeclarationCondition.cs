namespace AngleSharp.Css.Conditions
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class DeclarationCondition : CssCondition
    {
        readonly CssProperty _property;
        readonly CssValue _value;

        public DeclarationCondition(CssProperty property, CssValue value)
        {
            _property = property;
            _value = value;
        }
        
        protected override String Serialize()
        {
            var important = _property.IsImportant ? " !important" : String.Empty;
            var rest = String.Concat(_value.CssText, important, ")");
            return String.Concat("(", _property.Name, ": ", rest);
        }

        public override Boolean Check()
        {
            return (_property is CssUnknownProperty == false) && _property.TrySetValue(_value);
        }
    }
}
