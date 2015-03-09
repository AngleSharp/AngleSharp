namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Base class for all shorthand properties
    /// </summary>
    abstract class CssShorthandProperty : CssProperty
    {
        #region Fields

        readonly CssProperty[] _properties;

        #endregion

        #region ctor

        public CssShorthandProperty(String name, CssStyleDeclaration rule, PropertyFlags flags = PropertyFlags.None)
            : base(name, rule, flags | PropertyFlags.Shorthand)
        {
            _properties = Factory.Properties.CreateLonghandsFor(name, rule).ToArray();
        }

        #endregion

        #region Properties

        public CssProperty[] Properties
        {
            get { return _properties; }
        }

        #endregion

        #region Methods

        protected sealed override Object GetDefault(IElement element)
        {
            return null;
        }

        protected sealed override Object Compute(IElement element)
        {
            return null;
        }

        protected TProperty Get<TProperty>()
        {
            return _properties.OfType<TProperty>().FirstOrDefault();
        }

        protected static Boolean ExpandPeriodic(CssValueList list)
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

        protected static String SerializePeriodic(CssProperty t, CssProperty r, CssProperty b, CssProperty l)
        {
            var top = t.SerializeValue();
            var right = r.SerializeValue();
            var bottom = b.SerializeValue();
            var left = l.SerializeValue();
            return SerializePeriodic(top, right, bottom, left);
        }

        protected static String SerializePeriodic(ICssValue top, ICssValue right, ICssValue bottom, ICssValue left)
        {
            return SerializePeriodic(top.CssText, right.CssText, bottom.CssText, left.CssText);
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
            base.Reset();

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
        internal abstract String SerializeValue(IEnumerable<CssProperty> properties);

        #endregion
    }
}
