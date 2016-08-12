namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents the base class for all style-rule similar rules.
    /// </summary>
    abstract class CssDeclarationRule : CssRule, ICssProperties
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

        public String this[String propertyName]
        {
            get { return GetValue(propertyName); }
        }

        public IEnumerable<CssProperty> Declarations
        {
            get { return Children.OfType<CssProperty>(); }
        }

        public Int32 Length
        {
            get { return Declarations.Count(); }
        }

        #endregion

        #region Methods

        public String GetPropertyValue(String propertyName)
        {
            return GetValue(propertyName);
        }

        public String GetPropertyPriority(String propertyName)
        {
            return null;
        }

        public void SetProperty(String propertyName, String propertyValue, String priority = null)
        {
            SetValue(propertyName, propertyValue);
        }

        public String RemoveProperty(String propertyName)
        {
            foreach (var declaration in Declarations)
            {
                if (declaration.HasValue && declaration.Name.Is(propertyName))
                {
                    var value = declaration.Value;
                    RemoveChild(declaration);
                    return value;
                }
            }

            return null;
        }

        public IEnumerator<ICssProperty> GetEnumerator()
        {
            return Declarations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
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

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var rules = new FormatTransporter(Declarations);
            var content = formatter.Style("@" + _name, rules);
            writer.Write(content);
        }

        #endregion

        #region Helpers

        struct FormatTransporter : IStyleFormattable
        {
            private readonly IEnumerable<CssProperty> _properties;

            public FormatTransporter(IEnumerable<CssProperty> properties)
            {
                _properties = properties.Where(m => m.HasValue);
            }

            public void ToCss(TextWriter writer, IStyleFormatter formatter)
            {
                var properties = _properties.Select(m => m.ToCss(formatter));
                var content = formatter.Declarations(properties);
                writer.Write(content);
            }
        }

        protected abstract CssProperty CreateNewProperty(String name);

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

            var property = CreateNewProperty(propertyName);

            if (property != null)
            {
                var value = Parser.ParseValue(valueText);
                property.TrySetValue(value);
                AppendChild(property);
            }
        }

        #endregion
    }
}
