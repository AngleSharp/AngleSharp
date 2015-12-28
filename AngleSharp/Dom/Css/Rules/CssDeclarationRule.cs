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

        readonly List<CssProperty> _declarations;
        readonly String _name;

        #endregion

        #region ctor

        internal CssDeclarationRule(CssRuleType type, String name, CssParser parser)
            : base(type, parser)
        {
            _declarations = new List<CssProperty>();
            _name = name;
            Children = _declarations;
        }

        #endregion

        #region Internal Methods

        internal void SetProperty(CssProperty property)
        {
            for (var i = 0; i < _declarations.Count; i++)
            {
                if (_declarations[i].Name.Is(property.Name))
                {
                    _declarations[i] = property;
                    return;
                }
            }

            _declarations.Add(property);
        }

        protected override void ReplaceWith(ICssRule rule)
        {
            var newRule = (CssDeclarationRule)rule;
            _declarations.Clear();
            _declarations.AddRange(newRule._declarations);
            newRule._declarations.Clear();
        }

        #endregion

        #region String representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var rules = formatter.Block(_declarations.Where(m => m.HasValue));
            return formatter.Rule("@" + _name, null, rules);
        }

        #endregion

        #region Helpers

        protected String GetValue(String propertyName)
        {
            foreach (var declaration in _declarations)
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
            foreach (var declaration in _declarations)
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
