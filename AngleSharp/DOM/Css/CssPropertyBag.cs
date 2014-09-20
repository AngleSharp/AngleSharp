namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CssPropertyBag : IEnumerable<ICssProperty>
    {
        #region Fields

        readonly Dictionary<String, KeyValuePair<ICssProperty, Priority>> _bag;

        #endregion

        #region ctor

        public CssPropertyBag()
        {
            _bag = new Dictionary<String, KeyValuePair<ICssProperty, Priority>>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region Methods

        public void TryUpdate(ICssProperty property, Priority priority)
        {
            KeyValuePair<ICssProperty, Priority> value;

            if (_bag.TryGetValue(property.Name, out value))
            {
                if (property.IsImportant == value.Key.IsImportant && value.Value > priority)
                    return;
                else if (!property.IsImportant && value.Key.IsImportant)
                    return;
            }

            _bag[property.Name] = new KeyValuePair<ICssProperty,Priority>(property, priority);
        }

        public void TryUpdate(ICssProperty property)
        {
            KeyValuePair<ICssProperty, Priority> value;

            if (!_bag.TryGetValue(property.Name, out value))
                _bag[property.Name] = new KeyValuePair<ICssProperty, Priority>(property, Priority.Zero);

        }

        public ICssProperty this[String name]
        {
            get 
            {
                KeyValuePair<ICssProperty, Priority> value;

                if (_bag.TryGetValue(name, out value))
                    return value.Key;

                return null; 
            }
        }

        #endregion

        #region IEnumerable

        public IEnumerator<ICssProperty> GetEnumerator()
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
