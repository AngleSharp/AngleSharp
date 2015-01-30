namespace AngleSharp.Factories
{
    using AngleSharp.Css;
    using AngleSharp.Css.MediaFeatures;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to media feature instance creation mappings.
    /// </summary>
    sealed class MediaFeatureFactory
    {
        readonly Dictionary<String, Func<MediaFeature>> creators = new Dictionary<String, Func<MediaFeature>>(StringComparer.OrdinalIgnoreCase)
        {
            { FeatureNames.MinWidth, () => new WidthMediaFeature(FeatureNames.MinWidth) },
            { FeatureNames.MaxWidth, () => new WidthMediaFeature(FeatureNames.MaxWidth) },
            { FeatureNames.Width, () => new WidthMediaFeature(FeatureNames.Width) },
            { FeatureNames.MinHeight, () => new HeightMediaFeature(FeatureNames.MinHeight) },
            { FeatureNames.MaxHeight, () => new HeightMediaFeature(FeatureNames.MaxHeight) },
            { FeatureNames.Height, () => new HeightMediaFeature(FeatureNames.Height) },
            { FeatureNames.MinDeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.MinDeviceWidth) },
            { FeatureNames.MaxDeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.MaxDeviceWidth) },
            { FeatureNames.DeviceWidth, () => new DeviceWidthMediaFeature(FeatureNames.DeviceWidth) },
            { FeatureNames.MinDeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.MinDeviceHeight) },
            { FeatureNames.MaxDeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.MaxDeviceHeight) },
            { FeatureNames.DeviceHeight, () => new DeviceHeightMediaFeature(FeatureNames.DeviceHeight) },
            { FeatureNames.MinAspectRatio, () => new AspectRatioMediaFeature(FeatureNames.MinAspectRatio) },
            { FeatureNames.MaxAspectRatio, () => new AspectRatioMediaFeature(FeatureNames.MaxAspectRatio) },
            { FeatureNames.AspectRatio, () => new AspectRatioMediaFeature(FeatureNames.AspectRatio) },
            { FeatureNames.MinDeviceAspectRatio, () => new DeviceAspectRatioMediaFeature(FeatureNames.MinDeviceAspectRatio) },
            { FeatureNames.MaxDeviceAspectRatio, () => new DeviceAspectRatioMediaFeature(FeatureNames.MaxDeviceAspectRatio) },
            { FeatureNames.DeviceAspectRatio, () => new DeviceAspectRatioMediaFeature(FeatureNames.DeviceAspectRatio) },
            { FeatureNames.MinColor, () => new ColorMediaFeature(FeatureNames.MinColor) },
            { FeatureNames.MaxColor, () => new ColorMediaFeature(FeatureNames.MaxColor) },
            { FeatureNames.Color, () => new ColorMediaFeature(FeatureNames.Color) },
            { FeatureNames.MinColorIndex, () => new ColorIndexMediaFeature(FeatureNames.MinColorIndex) },
            { FeatureNames.MaxColorIndex, () => new ColorIndexMediaFeature(FeatureNames.MaxColorIndex) },
            { FeatureNames.ColorIndex, () => new ColorIndexMediaFeature(FeatureNames.ColorIndex) },
            { FeatureNames.MinMonochrome, () => new MonochromeMediaFeature(FeatureNames.MinMonochrome) },
            { FeatureNames.MaxMonochrome, () => new MonochromeMediaFeature(FeatureNames.MaxMonochrome) },
            { FeatureNames.Monochrome, () => new MonochromeMediaFeature(FeatureNames.Monochrome) },
            { FeatureNames.MinResolution, () => new ResolutionMediaFeature(FeatureNames.MinResolution) },
            { FeatureNames.MaxResolution, () => new ResolutionMediaFeature(FeatureNames.MaxResolution) },
            { FeatureNames.Resolution, () => new ResolutionMediaFeature(FeatureNames.Resolution) },
            { FeatureNames.Orientation, () => new OrientationMediaFeature() },
            { FeatureNames.Grid, () => new GridMediaFeature() },
            { FeatureNames.Scan, () => new ScanMediaFeature() },
            { FeatureNames.UpdateFrequency, () => new UpdateFrequencyMediaFeature() },
            { FeatureNames.Scripting, () => new ScriptingMediaFeature() },
            { FeatureNames.Pointer, () => new PointerMediaFeature() },
            { FeatureNames.Hover, () => new HoverMediaFeature() }
        };

        /// <summary>
        /// Creates a new media feature.
        /// </summary>
        /// <param name="name">The name of the feature.</param>
        /// <returns>The created feature.</returns>
        public MediaFeature Create(String name)
        {
            Func<MediaFeature> creator;

            if (creators.TryGetValue(name, out creator))
                return creator();

            return null;
        }
    }
}
