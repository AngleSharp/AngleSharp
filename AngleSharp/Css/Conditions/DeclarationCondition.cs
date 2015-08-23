namespace AngleSharp.Css.Conditions
{
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;

    sealed class DeclarationCondition : CssCondition
    {
        readonly CssProperty _property;
        readonly CssValue _value;

        public DeclarationCondition(CssProperty property, CssValue value)
        {
            _property = property;
            _value = value;
        }

        public override String GetSource()
        {
            var source = String.Concat("(", _property.GetSource(), ")");
            return Decorate(source);
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

        public override IEnumerable<CssNode> GetChildren()
        {
            yield return _property;
        }
    }
}
