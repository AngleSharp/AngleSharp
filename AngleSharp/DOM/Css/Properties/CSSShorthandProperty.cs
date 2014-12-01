namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for all shorthand properties
    /// </summary>
    abstract class CSSShorthandProperty : CSSProperty
    {
        #region Fields

        readonly CSSProperty[] _properties;

        #endregion

        #region ctor

        public CSSShorthandProperty(String name, CSSStyleDeclaration rule, PropertyFlags flags = PropertyFlags.None)
            : base(name, rule, flags | PropertyFlags.Shorthand)
        {
            _properties = CssPropertyFactory.CreateLonghandsFor(name, rule).ToArray();
            Reset();
        }

        #endregion

        #region Properties

        public CSSProperty[] Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        protected TProperty Get<TProperty>()
        {
            return _properties.OfType<TProperty>().FirstOrDefault();
        }

        protected Boolean IsComplete(IEnumerable<CSSProperty> properties)
        {
            foreach (var property in _properties)
            {
                if (!properties.Contains(property))
                    return false;
            }

            return true;
        }

        protected static Boolean ExpandPeriodic(CSSValueList list)
        {
            if (list.Length == 0 || list.Length > 4)
                return false;

            if (list.Length == 1)
                list.Add(list[0]);

            if (list.Length == 2)
                list.Add(list[0]);

            if (list.Length == 3)
                list.Add(list[1]);

            return true;
        }

        protected static String SerializePeriodic(CSSProperty t, CSSProperty r, CSSProperty b, CSSProperty l)
        {
            var top = t.SerializeValue();
            var right = r.SerializeValue();
            var bottom = b.SerializeValue();
            var left = l.SerializeValue();
            return SerializePeriodic(top, right, bottom, left);
        }

        protected static String SerializePeriodic(IDistance t, IDistance r, IDistance b, IDistance l)
        {
            var top = t.CssText;
            var right = r.CssText;
            var bottom = b.CssText;
            var left = l.CssText;
            return SerializePeriodic(top, right, bottom, left);
        }

        protected static String SerializePeriodic(String top, String right, String bottom, String left)
        {
            if (left != right)
                return String.Format("{0} {1} {2} {3}", top, right, bottom, left);
            else if (bottom != top)
                return String.Format("{0} {1} {2}", top, right, bottom);
            else if (right != top)
                return String.Format("{0} {1}", top, right);

            return top;
        }

        internal sealed override void Reset()
        {
            foreach (var property in _properties)
                property.Reset();
        }

        internal override sealed String SerializeValue()
        {
            return SerializeValue(_properties);
        }

        /// <summary>
        /// Serializes the shorthand with only the given properties.
        /// </summary>
        /// <param name="properties">The properties to use.</param>
        /// <returns>The serialized value or an empty string, if serialization is not possible.</returns>
        internal abstract String SerializeValue(IEnumerable<CSSProperty> properties);

        #endregion
    }
}
