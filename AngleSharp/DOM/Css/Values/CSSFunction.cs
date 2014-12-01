namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CSSFunction : CSSValue
    {
        public CSSFunction(String name, List<ICssValue> arguments)
            : base(CssValueType.Primitive)
        {
            Name = name;
            Arguments = arguments;
        }

        public String Name
        {
            get;
            private set;
        }

        public List<ICssValue> Arguments
        {
            get;
            private set;
        }

        public override String ToCss()
        {
            return FunctionNames.Build(Name, Arguments.Select(m => m.CssText).ToArray());
        }
    }
}
