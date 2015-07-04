namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    sealed class CssMedium : IEnumerable<MediaFeature>, IStyleFormattable
    {
        #region Media Types and Features

        readonly static String[] Types = 
        {
            // Intended for non-paged computer screens.
            Keywords.Screen,
            // Intended for speech synthesizers.
            Keywords.Speech,
            // Intended for paged material and for documents viewed on screen in print preview mode.
            Keywords.Print,
            // Suitable for all devices.
            Keywords.All
        };

        #endregion

        #region Fields

        readonly List<MediaFeature> _features;

        #endregion

        #region ctor

        internal CssMedium()
        {
            _features = new List<MediaFeature>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the type of medium that is represented.
        /// </summary>
        public String Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the medium has been created using the only keyword.
        /// </summary>
        public Boolean IsExclusive
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets if the medium has been created using the not keyword.
        /// </summary>
        public Boolean IsInverse
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets a string describing the covered constraints.
        /// </summary>
        public String Constraints
        {
            get
            {
                var constraints = new String[_features.Count];

                for (int i = 0; i < _features.Count; i++)
                    constraints[i] = _features[i].ToCss();

                return String.Join(" and ", constraints);
            }
        }

        #endregion

        #region Methods

        public String ToCss()
        {
            return ToCss(CssStyleFormatter.Instance);
        }

        public String ToCss(IStyleFormatter formatter)
        {
            var constraints = new String[_features.Count];

            for (int i = 0; i < _features.Count; i++)
                constraints[i] = _features[i].ToCss(formatter);

            return formatter.Medium(IsExclusive, IsInverse, Type, constraints);
        }

        /// <summary>
        /// Validates the given medium against the provided rendering device.
        /// </summary>
        /// <param name="device">The current render device.</param>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public Boolean Validate(RenderDevice device)
        {
            if (!String.IsNullOrEmpty(Type) && Types.Contains(Type) == IsInverse)
                return false;

            if (IsInvalid(device, Keywords.Screen, RenderDevice.Kind.Screen) ||
                IsInvalid(device, Keywords.Speech, RenderDevice.Kind.Speech) ||
                IsInvalid(device, Keywords.Print, RenderDevice.Kind.Printer))
                return false;

            foreach (var feature in _features)
            {
                if (feature.Validate(device) == IsInverse)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Gets an enumerator over all included media features.
        /// </summary>
        /// <returns>The specialized enumerator.</returns>
        public IEnumerator<MediaFeature> GetEnumerator()
        {
            return _features.GetEnumerator();
        }

        /// <summary>
        /// Gets a general enumerator over the included media features.
        /// </summary>
        /// <returns>The general enumerator.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Adds a constraint to the list of constraints.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <param name="value">The value of the feature, if any.</param>
        internal Boolean AddConstraint(String name, CssValue value = null)
        {
            var feature = Factory.MediaFeatures.Create(name);

            if (feature == null || !feature.TrySetValue(value))
                return false;

            _features.Add(feature);
            return true;
        }

        #endregion

        #region Helpers

        Boolean IsInvalid(RenderDevice device, String keyword, RenderDevice.Kind kind)
        {
            return Type == keyword && (device.DeviceType == kind) == IsInverse;
        }

        #endregion
    }
}
