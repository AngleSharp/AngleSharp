namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    sealed class CssPropertyBag : IEnumerable<CSSProperty>
    {
        #region Fields

        readonly Dictionary<String, KeyValuePair<CSSProperty, Priority>> _bag;

        #endregion

        #region ctor

        public CssPropertyBag()
        {
            _bag = new Dictionary<String, KeyValuePair<CSSProperty, Priority>>(StringComparer.OrdinalIgnoreCase);
        }

        #endregion

        #region Methods

        public void TryUpdate(CSSProperty property, Priority priority)
        {
            KeyValuePair<CSSProperty, Priority> value;

            if (_bag.TryGetValue(property.Name, out value))
            {
                if (property.IsImportant == value.Key.IsImportant && value.Value > priority)
                    return;
                else if (!property.IsImportant && value.Key.IsImportant)
                    return;
            }

            _bag[property.Name] = new KeyValuePair<CSSProperty, Priority>(property, priority);
        }

        public void TryUpdate(CSSProperty property)
        {
            KeyValuePair<CSSProperty, Priority> value;

            if (!_bag.TryGetValue(property.Name, out value))
                _bag[property.Name] = new KeyValuePair<CSSProperty, Priority>(property, Priority.Zero);

        }

        public CSSProperty this[String name]
        {
            get 
            {
                KeyValuePair<CSSProperty, Priority> value;

                if (_bag.TryGetValue(name, out value))
                    return value.Key;

                return null; 
            }
        }

        #endregion

        #region IEnumerable

        public IEnumerator<CSSProperty> GetEnumerator()
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
