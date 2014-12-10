namespace AngleSharp.Css.Media
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to media feature instance creation mappings.
    /// </summary>
    static class CssMediaFeatureFactory
    {
        static readonly Dictionary<String, Func<MediaFeature>> featureConstructors = new Dictionary<String, Func<MediaFeature>>(StringComparer.OrdinalIgnoreCase);

        static CssMediaFeatureFactory()
        {
            featureConstructors.Add(FeatureNames.MinWidth, () => new WidthMediaFeature(FeatureNames.MinWidth));
            featureConstructors.Add(FeatureNames.MaxWidth, () => new WidthMediaFeature(FeatureNames.MaxWidth));
            featureConstructors.Add(FeatureNames.Width, () => new WidthMediaFeature(FeatureNames.Width));
            featureConstructors.Add(FeatureNames.MinHeight, () => new HeightMediaFeature(FeatureNames.MinHeight));
            featureConstructors.Add(FeatureNames.MaxHeight, () => new HeightMediaFeature(FeatureNames.MaxHeight));
            featureConstructors.Add(FeatureNames.Height, () => new HeightMediaFeature(FeatureNames.Height));
            featureConstructors.Add(FeatureNames.MinDeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.MinDeviceWidth));
            featureConstructors.Add(FeatureNames.MaxDeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.MaxDeviceWidth));
            featureConstructors.Add(FeatureNames.DeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.DeviceWidth));
            featureConstructors.Add(FeatureNames.MinDeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.MinDeviceHeight));
            featureConstructors.Add(FeatureNames.MaxDeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.MaxDeviceHeight));
            featureConstructors.Add(FeatureNames.DeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.DeviceHeight));
            featureConstructors.Add(FeatureNames.Orientation, () => new OrientationMediaFeature());
            featureConstructors.Add(FeatureNames.MinAspectRatio, () => new AspectRatioMediaFeature(FeatureNames.MinAspectRatio));
            featureConstructors.Add(FeatureNames.MaxAspectRatio, () => new AspectRatioMediaFeature(FeatureNames.MaxAspectRatio));
            featureConstructors.Add(FeatureNames.AspectRatio, () => new AspectRatioMediaFeature(FeatureNames.AspectRatio));
            featureConstructors.Add(FeatureNames.MinColor, () => new ColorMediaFeature(FeatureNames.MinColor));
            featureConstructors.Add(FeatureNames.MaxColor, () => new ColorMediaFeature(FeatureNames.MaxColor));
            featureConstructors.Add(FeatureNames.Color, () => new ColorMediaFeature(FeatureNames.Color));
            featureConstructors.Add(FeatureNames.MinColorIndex, () => new ColorIndexMediaFeature(FeatureNames.MinColorIndex));
            featureConstructors.Add(FeatureNames.MaxColorIndex, () => new ColorIndexMediaFeature(FeatureNames.MaxColorIndex));
            featureConstructors.Add(FeatureNames.ColorIndex, () => new ColorIndexMediaFeature(FeatureNames.ColorIndex));
            featureConstructors.Add(FeatureNames.MinMonochrome, () => new MonochromeMediaFeature(FeatureNames.MinMonochrome));
            featureConstructors.Add(FeatureNames.MaxMonochrome, () => new MonochromeMediaFeature(FeatureNames.MaxMonochrome));
            featureConstructors.Add(FeatureNames.Monochrome, () => new MonochromeMediaFeature(FeatureNames.Monochrome));
            featureConstructors.Add(FeatureNames.MinResolution, () => new ResolutionMediaFeature(FeatureNames.MinResolution));
            featureConstructors.Add(FeatureNames.MaxResolution, () => new ResolutionMediaFeature(FeatureNames.MaxResolution));
            featureConstructors.Add(FeatureNames.Resolution, () => new ResolutionMediaFeature(FeatureNames.Resolution));
            featureConstructors.Add(FeatureNames.Grid, () => new GridMediaFeature());
            featureConstructors.Add(FeatureNames.Scan, () => new ScanMediaFeature());
        }

        /// <summary>
        /// Creates a new media feature.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The created feature.</returns>
        public static MediaFeature Create(String name)
        {
            Func<MediaFeature> constructor;

            if (featureConstructors.TryGetValue(name, out constructor))
                return constructor();

            return null;
        }
    }
}
