namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    abstract class CssDeclarationRule : CssRule
    {
        #region Fields

        readonly List<CssProperty> _declarations;
        readonly String _name;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new @-rule storing declarations.
        /// </summary>
        internal CssDeclarationRule(CssRuleType type, String name, CssParser parser)
            : base(type, parser)
        {
            _declarations = new List<CssProperty>();
            _name = name;
        }

        #endregion

        #region Methods

        public override IEnumerable<CssNode> GetChildren()
        {
            return _declarations;
        }

        #endregion

        #region Internal methods

        internal void SetProperty(CssProperty property)
        {
            for (int i = 0; i < _declarations.Count; i++)
            {
                if (_declarations[i].Name == property.Name)
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
                if (declaration.HasValue && declaration.Name == propertyName)
                    return declaration.Value;
            }

            return String.Empty;
        }

        protected void SetValue(String propertyName, String valueText)
        {
            foreach (var declaration in _declarations)
            {
                if (declaration.Name == propertyName)
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
