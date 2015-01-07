namespace AngleSharp.Css
{
    using AngleSharp.DOM.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class PropertyBag : IEnumerable<CssProperty>
    {
        #region Fields

        readonly Dictionary<String, KeyValuePair<CssProperty, Priority>> _bag;

        #endregion

        #region ctor

        public PropertyBag()
        {
            _bag = new Dictionary<String, KeyValuePair<CssProperty, Priority>>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region Methods

        public void TryUpdate(CssProperty property, Priority priority)
        {
            KeyValuePair<CssProperty, Priority> value;

            if (_bag.TryGetValue(property.Name, out value))
            {
                if (property.IsImportant == value.Key.IsImportant && value.Value > priority)
                    return;
                else if (!property.IsImportant && value.Key.IsImportant)
                    return;
            }

            _bag[property.Name] = new KeyValuePair<CssProperty, Priority>(property, priority);
        }

        public void TryUpdate(CssProperty property)
        {
            KeyValuePair<CssProperty, Priority> value;

            if (!_bag.TryGetValue(property.Name, out value))
                _bag[property.Name] = new KeyValuePair<CssProperty, Priority>(property, Priority.Zero);

        }

        public CssProperty this[String name]
        {
            get 
            {
                KeyValuePair<CssProperty, Priority> value;

                if (_bag.TryGetValue(name, out value))
                    return value.Key;

                return null; 
            }
        }

        #endregion

        #region IEnumerable

        public IEnumerator<CssProperty> GetEnumerator()
        {
            return _bag.Values.Select(m => m.Key).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _bag.Values.Select(m => m.Key).GetEnumerator();
        }

        #endregion
    }
}
