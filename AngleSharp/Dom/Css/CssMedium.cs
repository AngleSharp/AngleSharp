namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    sealed class CssMedium : ICssMedium
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
        /// Gets an enumerator over all included media features.
        /// </summary>
        public IEnumerable<IMediaFeature> Features
        {
            get { return _features; }
        }

        /// <summary>
        /// Gets the contained CSS nodes.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return _features; }
        }

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
        /// Adds a constraint to the list of constraints.
        /// </summary>
        /// <param name="feature">The feature to add.</param>
        internal void AddConstraint(MediaFeature feature)
        {
            _features.Add(feature);
        }

        /// <summary>
        /// Determines whether the given object is the same.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if both are the same, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var other = obj as CssMedium;

            if (other != null && 
                other.IsExclusive == this.IsExclusive && 
                other.IsInverse == this.IsInverse && 
                other.Type == this.Type && 
                other._features.Count == this._features.Count)
            {
                foreach (var feature in other._features)
                {
                    var shared = _features.Find(m => m.Name == feature.Name);

                    if (shared == null || shared.Value != feature.Value)
                        return false;
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>The hash code of the object.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion

        #region String Representation

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

        #endregion

        #region Helpers

        Boolean IsInvalid(RenderDevice device, String keyword, RenderDevice.Kind kind)
        {
            return Type == keyword && (device.DeviceType == kind) == IsInverse;
        }

        #endregion
    }
}
