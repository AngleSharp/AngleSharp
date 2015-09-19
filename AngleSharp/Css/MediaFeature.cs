namespace AngleSharp.Css
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a feature expression within
    /// a media query.
    /// </summary>
    public abstract class MediaFeature : IMediaFeature
    {
        #region Fields

        readonly Boolean _min;
        readonly Boolean _max;
        readonly String _name;
        CssValue _value;

        #endregion

        #region ctor

        internal MediaFeature(String name)
        {
            _name = name;
            _min = name.StartsWith("min-");
            _max = name.StartsWith("max-");
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the contained nodes.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        /// <summary>
        /// Gets the name of the feature.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets if the feature represents the minimum.
        /// </summary>
        public Boolean IsMinimum
        {
            get { return _min; }
        }

        /// <summary>
        /// Gets if the feature represents the maximum.
        /// </summary>
        public Boolean IsMaximum
        {
            get { return _max; }
        }

        /// <summary>
        /// Gets the value of the feature, if any.
        /// </summary>
        public String Value
        {
            get { return HasValue ? _value.CssText : String.Empty; }
        }

        /// <summary>
        /// Gets if a value has been set for this feature.
        /// </summary>
        public Boolean HasValue
        {
            get { return _value != null && _value.Count > 0; }
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the used value converter.
        /// </summary>
        internal abstract IValueConverter Converter
        {
            get;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to set the given value.
        /// </summary>
        /// <param name="value">The value that should be used.</param>
        /// <returns>True if the given value is accepted, otherwise false.</returns>
        internal Boolean TrySetValue(CssValue value)
        {
            var result = false;

            if (value == null)
                result = !IsMinimum && !IsMaximum && Converter.HasDefault();
            else
                result = Converter.Convert(value) != null;

            if (result)
                _value = value;

            return result;
        }

        /// <summary>
        /// Validates the given feature against the provided rendering device.
        /// </summary>
        /// <param name="device">The provided rendering device.</param>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public abstract Boolean Validate(RenderDevice device);

        /// <summary>
        /// Returns the (complete) CSS style representation of the node.
        /// </summary>
        /// <returns>The source code snippet.</returns>
        public String ToCss()
        {
            return ToCss(CssStyleFormatter.Instance);
        }

        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public String ToCss(IStyleFormatter formatter)
        {
            return formatter.Constraint(_name, HasValue ? Value : null);
        }

        #endregion
    }
}
