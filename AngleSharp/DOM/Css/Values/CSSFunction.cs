namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CssFunction : ICssValue
    {
        public CssFunction(String name, List<ICssValue> arguments)
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

        public CssValueType Type
        {
            get { return CssValueType.Primitive; }
        }

        public String CssText
        {
            get { return FunctionNames.Build(Name, Arguments.Select(m => m.CssText).ToArray()); }
        }
    }
}
