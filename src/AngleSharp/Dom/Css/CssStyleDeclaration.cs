namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a single CSS declaration block.
    /// </summary>
    sealed class CssStyleDeclaration : CssNode, ICssStyleDeclaration, IBindable
    {
        #region Fields

        private readonly CssRule _parent;
        private readonly CssParser _parser;

        #endregion

        #region Events

        public event Action<String> Changed;

        #endregion

        #region ctor

        private CssStyleDeclaration(CssRule parent, CssParser parser)
        {
            _parent = parent;
            _parser = parser;
        }

        internal CssStyleDeclaration(CssParser parser)
            : this(null, parser)
        {
        }

        internal CssStyleDeclaration()
            : this(null, null)
        {
        }

        internal CssStyleDeclaration(CssRule parent)
            : this(parent, parent.Parser)
        {
        }

        #endregion

        #region Index

        public String this[Int32 index]
        {
            get { return Declarations.GetItemByIndex(index).Name; }
        }

        public String this[String name]
        {
            get { return GetPropertyValue(name); }
        }

        #endregion

        #region Properties

        public IEnumerable<ICssProperty> Declarations
        {
            get { return Children.OfType<ICssProperty>(); }
        }

        public Boolean IsStrictMode
        {
            get { return IsReadOnly || _parser.Options.IsIncludingUnknownDeclarations == false; }
        }

        public String CssText
        {
            get { return this.ToCss(); }
            set { Update(value); RaiseChanged(); }
        }

        public Boolean IsReadOnly
        {
            get { return _parser == null; }
        }

        public Int32 Length
        {
            get { return Declarations.Count(); }
        }

        public ICssRule Parent
        {
            get { return _parent; }
        }

        #endregion

        #region Methods
        
        public void Update(String value)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);

            Clear();

            if (!String.IsNullOrEmpty(value))
            {
                _parser.AppendDeclarations(this, value);
            }
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var list = new List<String>();
            var serialized = new List<String>();

            foreach (var declaration in Declarations)
            {
                var property = declaration.Name;

                if (IsStrictMode)
                {
                    if (serialized.Contains(property))
                    {
                        continue;
                    }

                    var factory = Factory.Properties;
                    var shorthands = factory.GetShorthands(property);

                    if (shorthands.Any())
                    {
                        var longhands = Declarations.Where(m => !serialized.Contains(m.Name)).ToList();

                        foreach (var shorthand in shorthands.OrderByDescending(m => factory.GetLonghands(m).Count()))
                        {
                            var rule = factory.CreateShorthand(shorthand);
                            var properties = factory.GetLonghands(shorthand);
                            var currentLonghands = longhands.Where(m => properties.Contains(m.Name)).ToArray();

                            if (currentLonghands.Length == 0)
                            {
                                continue;
                            }

                            var important = currentLonghands.Count(m => m.IsImportant);

                            if (important > 0 && important != currentLonghands.Length)
                            {
                                continue;
                            }

                            if (properties.Length != currentLonghands.Length)
                            {
                                continue;
                            }

                            var value = rule.Stringify(currentLonghands);

                            if (String.IsNullOrEmpty(value))
                            {
                                continue;
                            }

                            list.Add(CssStyleFormatter.Instance.Declaration(shorthand, value, important != 0));

                            foreach (var longhand in currentLonghands)
                            {
                                serialized.Add(longhand.Name);
                                longhands.Remove(longhand);
                            }
                        }
                    }

                    if (serialized.Contains(property))
                    {
                        continue;
                    }

                    serialized.Add(property);
                }

                list.Add(declaration.ToCss(formatter));
            }

            writer.Write(formatter.Declarations(list));
        }

        public String RemoveProperty(String propertyName)
        {
            if (!IsReadOnly)
            {
                var value = GetPropertyValue(propertyName);
                RemovePropertyByName(propertyName);
                RaiseChanged();
                return value;
            }

            throw new DomException(DomError.NoModificationAllowed);
        }

        public String GetPropertyPriority(String propertyName)
        {
            var property = GetProperty(propertyName);

            if (property != null && property.IsImportant)
            {
                return Keywords.Important;
            }

            var factory = Factory.Properties;

            if (IsStrictMode && factory.IsShorthand(propertyName))
            {
                var longhands = factory.GetLonghands(propertyName);

                foreach (var longhand in longhands)
                {
                    if (!GetPropertyPriority(longhand).Isi(Keywords.Important))
                    {
                        return String.Empty;
                    }
                }

                return Keywords.Important;
            }

            return String.Empty;
        }

        public String GetPropertyValue(String propertyName)
        {
            var property = GetProperty(propertyName);

            if (property == null)
            {
                var factory = Factory.Properties;

                if (IsStrictMode && factory.IsShorthand(propertyName))
                {
                    var shortHand = factory.CreateShorthand(propertyName);
                    var declarations = factory.GetLonghands(propertyName);
                    var properties = new List<CssProperty>();

                    foreach (var declaration in declarations)
                    {
                        property = GetProperty(declaration);

                        if (property == null)
                        {
                            return String.Empty;
                        }

                        properties.Add(property);
                    }

                    return shortHand.Stringify(properties.ToArray());
                }

                return String.Empty;
            }

            return property.Value;
        }

        public void SetPropertyValue(String propertyName, String propertyValue)
        {
            SetProperty(propertyName, propertyValue);
        }

        public void SetPropertyPriority(String propertyName, String priority)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);

            if (String.IsNullOrEmpty(priority) || priority.Isi(Keywords.Important))
            {
                var factory = Factory.Properties;
                var important = !String.IsNullOrEmpty(priority);
                var mappings = IsStrictMode && factory.IsShorthand(propertyName) ?
                    factory.GetLonghands(propertyName) :
                    Enumerable.Repeat(propertyName, 1);

                foreach (var mapping in mappings)
                {
                    var property = GetProperty(mapping);

                    if (property != null)
                    {
                        property.IsImportant = important;
                    }
                }
            }
        }

        public void SetProperty(String propertyName, String propertyValue, String priority = null)
        {
            if (IsReadOnly)
                throw new DomException(DomError.NoModificationAllowed);
            
            if (!String.IsNullOrEmpty(propertyValue))
            {
                if (priority == null || priority.Isi(Keywords.Important))
                {
                    var value = _parser.ParseValue(propertyValue);

                    if (value != null)
                    {
                        var property = CreateProperty(propertyName);

                        if (property != null && property.TrySetValue(value))
                        {
                            property.IsImportant = priority != null;
                            SetProperty(property);
                            RaiseChanged();
                        }
                    }
                }
            }
            else
            {
                RemoveProperty(propertyName);
            }
        }

        #endregion

        #region Internal Methods

        internal ICssProperty CreateProperty(String propertyName)
        {
            var property = GetProperty(propertyName);

            if (property != null)
            {
                return property;
            }
                
            property = Factory.Properties.Create(propertyName);

            if (property != null || IsStrictMode)
            {
                return property;
            }

            return new CssProperty(propertyName);
        }

        internal CssProperty GetProperty(String name)
        {
            return Declarations.Where(m => m.Name.Isi(name)).FirstOrDefault();
        }

        internal void SetProperty(CssProperty property)
        {
            if (property is CssShorthandProperty)
            {
                SetShorthand((CssShorthandProperty)property);
            }
            else
            {
                SetLonghand(property);
            }
        }

        internal void SetDeclarations(IEnumerable<ICssProperty> decls)
        {
            ChangeDeclarations(decls, m => false, (o, n) => !o.IsImportant || n.IsImportant);
        }

        internal void UpdateDeclarations(IEnumerable<ICssProperty> decls)
        {
            ChangeDeclarations(decls, m => !m.CanBeInherited, (o, n) => o.IsInherited);
        }

        #endregion

        #region Helpers

        private void RemovePropertyByName(String propertyName)
        {
            foreach (var declaration in Declarations)
            {
                if (declaration.Name.Is(propertyName))
                {
                    RemoveChild(declaration);
                    break;
                }
            }

            var factory = Factory.Properties;

            if (IsStrictMode && factory.IsShorthand(propertyName))
            {
                var longhands = factory.GetLonghands(propertyName);

                foreach (var longhand in longhands)
                {
                    RemovePropertyByName(longhand);
                }
            }
        }

        private void ChangeDeclarations(IEnumerable<ICssProperty> decls, Predicate<ICssProperty> defaultSkip, Func<ICssProperty, ICssProperty, Boolean> removeExisting)
        {
            var declarations = new List<ICssProperty>();

            foreach (var newdecl in decls)
            {
                var skip = defaultSkip(newdecl);

                foreach (var olddecl in Declarations)
                {
                    if (olddecl.Name.Is(newdecl.Name))
                    {
                        if (removeExisting(olddecl, newdecl))
                        {
                            RemoveChild(olddecl);
                        }
                        else
                        {
                            skip = true;
                        }

                        break;
                    }
                }

                if (!skip)
                {
                    declarations.Add(newdecl);
                }
            }

            foreach (var declaration in declarations)
            {
                AppendChild(declaration);
            }
        }

        private void SetLonghand(ICssProperty property)
        {
            foreach (var declaration in Declarations)
            {
                if (declaration.Name.Is(property.Name))
                {
                    RemoveChild(declaration);
                    break;
                }
            }

            AppendChild(property);
        }

        private void SetShorthand(ICssProperty shorthand)
        {
            var properties = Factory.Properties.CreateLonghandsFor(shorthand.Name);
            shorthand.Export(properties);

            foreach (var property in properties)
            {
                SetLonghand(property);
            }
        }

        private void RaiseChanged()
        {
            Changed?.Invoke(CssText);
        }

        #endregion

        #region Interface implementation

        public IEnumerator<ICssProperty> GetEnumerator()
        {
            return Declarations.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
