namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Css.Media;
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    sealed class CSSMedium : IEnumerable<MediaFeature>
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

        internal CSSMedium()
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
                    constraints[i] = _features[i].CssText;

                return String.Join(" and ", constraints);
            }
        }

        /// <summary>
        /// Gets a CSS code representation of the medium.
        /// </summary>
        public String CssText
        {
            get
            {
                var constraints = Constraints;
                var prefix = IsExclusive ? "only " : (IsInverse ? "not " : String.Empty);

                if (String.IsNullOrEmpty(constraints))
                    return String.Concat(prefix, Type ?? String.Empty);
                else if (String.IsNullOrEmpty(Type))
                    return String.Concat(prefix, constraints);

                return String.Concat(prefix, Type, " and ", constraints);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates the given medium.
        /// </summary>
        /// <param name="window">The current browsing window.</param>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public Boolean Validate(IWindow window)
        {
            var condition = IsInverse;

            if (!String.IsNullOrEmpty(Type) && Types.Contains(Type) == condition)
                return false;

            foreach (var feature in _features)
            {
                if (feature.Validate(window) == condition)
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
            foreach (var feature in _features)
                yield return feature;
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
        internal Boolean AddConstraint(String name, ICssValue value = null)
        {
            var feature = CssMediaFeatureFactory.Create(name);

            if (feature == null || !feature.TrySetValue(value))
                return false;

            _features.Add(feature);
            return true;
        }

        #endregion
    }
}
