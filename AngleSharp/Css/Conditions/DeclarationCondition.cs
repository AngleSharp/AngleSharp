namespace AngleSharp.Css.Conditions
{
    using AngleSharp.Dom.Css;
    using System;

    sealed class DeclarationCondition : ICondition
    {
        readonly CssProperty _property;
        readonly CssValue _value;

        public DeclarationCondition(CssProperty property, CssValue value)
        {
            _property = property;
            _value = value;
        }

        public String Text
        {
            get
            {
                var important = _property.IsImportant ? " !important" : String.Empty;
                var rest = String.Concat(_value.CssText, important, ")");
                return String.Concat("(", _property.Name, ": ", rest);
            }
        }

        public Boolean Check()
        {
            return (_property is CssUnknownProperty == false) && _property.TrySetValue(_value);
        }
    }
}
