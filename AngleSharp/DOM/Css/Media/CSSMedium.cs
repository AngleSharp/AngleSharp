namespace AngleSharp.DOM.Css
{
    using AngleSharp.DOM.Css.Media;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    public sealed class CSSMedium : ICssObject
    {
        #region Media Types and Features

        readonly static String[] Types = 
        {
            // Intended for television-type devices (low resolution, color, limited scrollability).
            "tv",
            // Intended for non-paged computer screens.
            "screen",
            // Intended for media using a fixed-pitch character grid, such as teletypes, terminals, or portable devices with limited display capabilities.
            "tty",
            // Intended for projectors.
            "projection",
            // Intended for handheld devices (small screen, monochrome, bitmapped graphics, limited bandwidth).
            "handheld",
            // Intended for paged, opaque material and for documents viewed on screen in print preview mode.
            "print",
            // Intended for braille tactile feedback devices.
            "braille",
            // Suitable for all devices.
            "all"
        };

        readonly static Dictionary<String, Func<MediaFeature>> featureConstructors = new Dictionary<String, Func<MediaFeature>>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Fields

        List<MediaFeature> _features;

        #endregion

        #region ctor

        static CSSMedium()
        {
            featureConstructors.Add(FeatureNames.MinWidth, () => new MinWidthMediaFeature());
            featureConstructors.Add(FeatureNames.MaxWidth, () => new MaxWidthMediaFeature());
            featureConstructors.Add(FeatureNames.Width, () => new WidthMediaFeature());
            featureConstructors.Add(FeatureNames.MinHeight, () => new MinHeightMediaFeature());
            featureConstructors.Add(FeatureNames.MaxHeight, () => new MaxHeightMediaFeature());
            featureConstructors.Add(FeatureNames.Height, () => new HeightMediaFeature());
            featureConstructors.Add(FeatureNames.MinDeviceWidth, () => new MinDeviceWidthMediaFeature());
            featureConstructors.Add(FeatureNames.MaxDeviceWidth, () => new MaxDeviceWidthMediaFeature());
            featureConstructors.Add(FeatureNames.DeviceWidth, () => new DeviceWidthMediaFeature());
            featureConstructors.Add(FeatureNames.MinDeviceHeight, () => new MinDeviceHeightMediaFeature());
            featureConstructors.Add(FeatureNames.MaxDeviceHeight, () => new MaxDeviceHeightMediaFeature());
            featureConstructors.Add(FeatureNames.DeviceHeight, () => new DeviceHeightMediaFeature());
            featureConstructors.Add(FeatureNames.Orientation, () => new OrientationMediaFeature());
            featureConstructors.Add(FeatureNames.MinAspectRatio, () => new MinAspectRatioMediaFeature());
            featureConstructors.Add(FeatureNames.MaxAspectRatio, () => new MaxAspectRatioMediaFeature());
            featureConstructors.Add(FeatureNames.AspectRatio, () => new AspectRatioMediaFeature());
            featureConstructors.Add(FeatureNames.MinColor, () => new MinColorMediaFeature());
            featureConstructors.Add(FeatureNames.MaxColor, () => new MaxColorMediaFeature());
            featureConstructors.Add(FeatureNames.Color, () => new ColorMediaFeature());
            featureConstructors.Add(FeatureNames.MinColorIndex, () => new MinColorIndexMediaFeature());
            featureConstructors.Add(FeatureNames.MaxColorIndex, () => new MaxColorIndexMediaFeature());
            featureConstructors.Add(FeatureNames.ColorIndex, () => new ColorIndexMediaFeature());
            featureConstructors.Add(FeatureNames.MinMonochrome, () => new MinMonochromeMediaFeature());
            featureConstructors.Add(FeatureNames.MaxMonochrome, () => new MaxMonochromeMediaFeature());
            featureConstructors.Add(FeatureNames.Monochrome, () => new MonochromeMediaFeature());
            featureConstructors.Add(FeatureNames.MinResolution, () => new MinResolutionMediaFeature());
            featureConstructors.Add(FeatureNames.MaxResolution, () => new MaxResolutionMediaFeature());
            featureConstructors.Add(FeatureNames.Resolution, () => new ResolutionMediaFeature());
            featureConstructors.Add(FeatureNames.Grid, () => new GridMediaFeature());
            featureConstructors.Add(FeatureNames.Scan, () => new ScanMediaFeature());
        }

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
                    constraints[i] = _features[i].ToCss();

                return String.Join(" and ", constraints);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Validates the given medium.
        /// </summary>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public Boolean Validate()
        {
            var condition = IsInverse;

            if (!String.IsNullOrEmpty(Type) && Types.Contains(Type) == condition)
                return false;

            foreach (var feature in _features)
            {
                if (feature.Validate() == condition)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a CSS code representation of the medium.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public String ToCss()
        {
            var constraints = Constraints;
            var prefix = IsExclusive ? "only " : (IsInverse ? "not " : String.Empty);

            if (String.IsNullOrEmpty(constraints))
                return String.Concat(prefix, Type ?? String.Empty);
            else if (String.IsNullOrEmpty(Type))
                return String.Concat(prefix, constraints);

            return String.Concat(prefix, Type, " and ", constraints);
        }

        /// <summary>
        /// Adds a constraint to the list of constraints.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <param name="value">The value of the feature, if any.</param>
        internal Boolean AddConstraint(String name, CSSValue value = null)
        {
            Func<MediaFeature> constructor;

            if (!featureConstructors.TryGetValue(name, out constructor))
                return false;

            var feature = constructor();

            if (value == null && !feature.SetDefaultValue())
                return false;
            else if (value != null && !feature.SetValue(value))
                return false;

            _features.Add(feature);
            return true;
        }

        #endregion
    }
}
