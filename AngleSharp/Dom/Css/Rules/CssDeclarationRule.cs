namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents the base class for all style-rule similar rules.
    /// </summary>
    abstract class CssDeclarationRule : CssRule
    {
        #region Fields

        readonly String _name;

        #endregion

        #region ctor

        internal CssDeclarationRule(CssRuleType type, String name, CssParser parser)
            : base(type, parser)
        {
            _name = name;
        }

        #endregion

        #region Properties

        public IEnumerable<CssProperty> Declarations
        {
            get { return Children.OfType<CssProperty>(); }
        }

        #endregion

        #region Internal Methods

        internal void SetProperty(CssProperty property)
        {
            foreach (var declaration in Declarations)
            {
                if (declaration.Name.Is(property.Name))
                {
                    ReplaceChild(declaration, property);
                    return;
                }
            }

            AppendChild(property);
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(Declarations.Where(m => m.HasValue));
            return formatter.Rule("@" + _name, null, rules);
        }

        #endregion

        #region Helpers

        protected String GetValue(String propertyName)
        {
            foreach (var declaration in Declarations)
            {
                if (declaration.HasValue && declaration.Name.Is(propertyName))
                {
                    return declaration.Value;
                }
            }

            return String.Empty;
        }

        protected void SetValue(String propertyName, String valueText)
        {
            foreach (var declaration in Declarations)
            {
                if (declaration.Name.Is(propertyName))
                {
                    var value = Parser.ParseValue(valueText);
                    declaration.TrySetValue(value);
                    return;
                }
            }
        }

        #endregion
    }
}
