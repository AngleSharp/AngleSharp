namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    class CSSMedium : ICssObject
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

        readonly static String MinWidth = "min-width";
        readonly static String MinHeight = "min-height";
        readonly static String MinDeviceWidth = "min-device-width";
        readonly static String MinDeviceHeight = "min-device-height";
        readonly static String MinAspectRatio = "min-aspect-ratio";
        readonly static String MinResolution = "min-resolution";
        readonly static String MinColor = "min-color";
        readonly static String MinColorIndex = "min-color-index";
        readonly static String MinMonochrome = "min-monochrome";
        readonly static String MaxWidth = "max-width";
        readonly static String MaxHeight = "max-height";
        readonly static String MaxDeviceWidth = "max-device-width";
        readonly static String MaxDeviceHeight = "max-device-height";
        readonly static String MaxAspectRatio = "max-aspect-ratio";
        readonly static String MaxResolution = "max-resolution";
        readonly static String MaxColor = "max-color";
        readonly static String MaxColorIndex = "max-color-index";
        readonly static String MaxMonochrome = "max-monochrome";
        readonly static String Width = "width";
        readonly static String Height = "height";
        readonly static String DeviceWidth = "device-width";
        readonly static String DeviceHeight = "device-height";
        readonly static String AspectRatio = "aspect-ratio";
        readonly static String Resolution = "resolution";
        readonly static String Color = "color";
        readonly static String ColorIndex = "color-index";
        readonly static String Monochrome = "monochrome";
        readonly static String Orientation = "orientation";
        readonly static String Grid = "grid";
        readonly static String Scan = "scan";

        readonly static Dictionary<String, Func<CSSValue, MediaFeature>> featureConstructors = new Dictionary<String, Func<CSSValue, MediaFeature>>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Fields

        List<MediaFeature> _features;

        #endregion

        #region ctor

        static CSSMedium()
        {
            featureConstructors.Add(MinWidth, value => new MinWidthMediaFeature(value));
            featureConstructors.Add(MaxWidth, value => new MaxWidthMediaFeature(value));
            featureConstructors.Add(Width, value => new WidthMediaFeature(value));
            featureConstructors.Add(MinHeight, value => new MinHeightMediaFeature(value));
            featureConstructors.Add(MaxHeight, value => new MaxHeightMediaFeature(value));
            featureConstructors.Add(Height, value => new HeightMediaFeature(value));
            featureConstructors.Add(MinDeviceWidth, value => new MinDeviceWidthMediaFeature(value));
            featureConstructors.Add(MaxDeviceWidth, value => new MaxDeviceWidthMediaFeature(value));
            featureConstructors.Add(DeviceWidth, value => new DeviceWidthMediaFeature(value));
            featureConstructors.Add(MinDeviceHeight, value => new MinDeviceHeightMediaFeature(value));
            featureConstructors.Add(MaxDeviceHeight, value => new MaxDeviceHeightMediaFeature(value));
            featureConstructors.Add(DeviceHeight, value => new DeviceHeightMediaFeature(value));
            featureConstructors.Add(Orientation, value => new OrientationMediaFeature(value));
            featureConstructors.Add(MinAspectRatio, value => new MinAspectRatioMediaFeature(value));
            featureConstructors.Add(MaxAspectRatio, value => new MaxAspectRatioMediaFeature(value));
            featureConstructors.Add(AspectRatio, value => new AspectRatioMediaFeature(value));
            featureConstructors.Add(MinColor, value => new MinColorMediaFeature(value));
            featureConstructors.Add(MaxColor, value => new MaxColorMediaFeature(value));
            featureConstructors.Add(Color, value => new ColorMediaFeature(value));
            featureConstructors.Add(MinColorIndex, value => new MinColorIndexMediaFeature(value));
            featureConstructors.Add(MaxColorIndex, value => new MaxColorIndexMediaFeature(value));
            featureConstructors.Add(ColorIndex, value => new ColorIndexMediaFeature(value));
            featureConstructors.Add(MinMonochrome, value => new MinMonochromeMediaFeature(value));
            featureConstructors.Add(MaxMonochrome, value => new MaxMonochromeMediaFeature(value));
            featureConstructors.Add(Monochrome, value => new MonochromeMediaFeature(value));
            featureConstructors.Add(MinResolution, value => new MinResolutionMediaFeature(value));
            featureConstructors.Add(MaxResolution, value => new MaxResolutionMediaFeature(value));
            featureConstructors.Add(Resolution, value => new ResolutionMediaFeature(value));
            featureConstructors.Add(Grid, value => new GridMediaFeature(value));
            featureConstructors.Add(Scan, value => new ScanMediaFeature(value));
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
        public virtual Boolean Validate()
        {
            if (!String.IsNullOrEmpty(Type) && !Types.Contains(Type))
                return false;

            foreach (var feature in _features)
            {
                if (!feature.Validate())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns a CSS code representation of the medium.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public virtual String ToCss()
        {
            var constraints = Constraints;

            if (String.IsNullOrEmpty(constraints))
                return Type ?? String.Empty;
            else if (String.IsNullOrEmpty(Type))
                return constraints;

            return String.Concat(Type, " and ", constraints);
        }

        /// <summary>
        /// Adds a constraint to the list of constraints.
        /// </summary>
        /// <param name="feature">The name of the feature.</param>
        /// <param name="value">The value of the feature, if any.</param>
        internal Boolean AddConstraint(String feature, CSSValue value = null)
        {
            Func<CSSValue, MediaFeature> constructor;

            if (!featureConstructors.TryGetValue(feature, out constructor))
                return false;

            _features.Add(constructor(value));
            return true;
        }

        #endregion

        #region Feature

        abstract class MediaFeature : ICssObject
        {
            String _name;
            CSSValue _value;

            public MediaFeature(String name, CSSValue value)
            {
                _name = name;
                _value = value;

                if (value == null)
                    TakeDefault();
                else
                    Consider(value);
            }

            /// <summary>
            /// Gets the name of the feature.
            /// </summary>
            public String Name
            {
                get { return _name; }
            }

            /// <summary>
            /// Gets the value of the feature.
            /// </summary>
            public CSSValue Value
            {
                get { return _value; }
            }

            protected virtual void TakeDefault()
            {
                //TODO
            }

            protected virtual void Consider(CSSValue value)
            {
                //TODO
            }

            /// <summary>
            /// Validates the given feature.
            /// </summary>
            /// <returns>True if the constraints are satisfied, otherwise false.</returns>
            public abstract Boolean Validate();

            /// <summary>
            /// Returns a CSS code representation of the medium.
            /// </summary>
            /// <returns>A string that contains the code.</returns>
            public String ToCss()
            {
                if (_value == null)
                    return String.Concat("(", _name, ")");

                return String.Concat("(", _name, ": ", _value.ToCss(), ")");
            }
        }

        sealed class MinWidthMediaFeature : MediaFeature
        {
            public MinWidthMediaFeature(CSSValue value)
                : base(MinWidth, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxWidthMediaFeature : MediaFeature
        {
            public MaxWidthMediaFeature(CSSValue value)
                : base(MaxWidth, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class WidthMediaFeature : MediaFeature
        {
            public WidthMediaFeature(CSSValue value)
                : base(Width, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinDeviceWidthMediaFeature : MediaFeature
        {
            public MinDeviceWidthMediaFeature(CSSValue value)
                : base(MinDeviceWidth, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxDeviceWidthMediaFeature : MediaFeature
        {
            public MaxDeviceWidthMediaFeature(CSSValue value)
                : base(MaxDeviceWidth, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class DeviceWidthMediaFeature : MediaFeature
        {
            public DeviceWidthMediaFeature(CSSValue value)
                : base(DeviceWidth, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinHeightMediaFeature : MediaFeature
        {
            public MinHeightMediaFeature(CSSValue value)
                : base(MinHeight, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxHeightMediaFeature : MediaFeature
        {
            public MaxHeightMediaFeature(CSSValue value)
                : base(MaxHeight, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class HeightMediaFeature : MediaFeature
        {
            public HeightMediaFeature(CSSValue value)
                : base(Height, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinDeviceHeightMediaFeature : MediaFeature
        {
            public MinDeviceHeightMediaFeature(CSSValue value)
                : base(MinDeviceHeight, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxDeviceHeightMediaFeature : MediaFeature
        {
            public MaxDeviceHeightMediaFeature(CSSValue value)
                : base(MaxDeviceHeight, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class DeviceHeightMediaFeature : MediaFeature
        {
            public DeviceHeightMediaFeature(CSSValue value)
                : base(DeviceHeight, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinColorIndexMediaFeature : MediaFeature
        {
            public MinColorIndexMediaFeature(CSSValue value)
                : base(MinColorIndex, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MaxColorIndexMediaFeature : MediaFeature
        {
            public MaxColorIndexMediaFeature(CSSValue value)
                : base(MaxColorIndex, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class ColorIndexMediaFeature : MediaFeature
        {
            public ColorIndexMediaFeature(CSSValue value)
                : base(ColorIndex, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MinColorMediaFeature : MediaFeature
        {
            public MinColorMediaFeature(CSSValue value)
                : base(MinColor, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MaxColorMediaFeature : MediaFeature
        {
            public MaxColorMediaFeature(CSSValue value)
                : base(MaxColor, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class ColorMediaFeature : MediaFeature
        {
            public ColorMediaFeature(CSSValue value)
                : base(Color, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MinMonochromeMediaFeature : MediaFeature
        {
            public MinMonochromeMediaFeature(CSSValue value)
                : base(MinMonochrome, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MaxMonochromeMediaFeature : MediaFeature
        {
            public MaxMonochromeMediaFeature(CSSValue value)
                : base(MaxMonochrome, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MonochromeMediaFeature : MediaFeature
        {
            public MonochromeMediaFeature(CSSValue value)
                : base(Monochrome, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MinResolutionMediaFeature : MediaFeature
        {
            public MinResolutionMediaFeature(CSSValue value)
                : base(MinResolution, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToResolution();
                return length.HasValue;
            }
        }

        sealed class MaxResolutionMediaFeature : MediaFeature
        {
            public MaxResolutionMediaFeature(CSSValue value)
                : base(MaxResolution, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToResolution();
                return length.HasValue;
            }
        }

        sealed class ResolutionMediaFeature : MediaFeature
        {
            public ResolutionMediaFeature(CSSValue value)
                : base(Resolution, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToResolution();
                return length.HasValue;
            }
        }

        sealed class MinAspectRatioMediaFeature : MediaFeature
        {
            public MinAspectRatioMediaFeature(CSSValue value)
                : base(MinAspectRatio, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToAspectRatio();
                return length.HasValue;
            }
        }

        sealed class MaxAspectRatioMediaFeature : MediaFeature
        {
            public MaxAspectRatioMediaFeature(CSSValue value)
                : base(MaxAspectRatio, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToAspectRatio();
                return length.HasValue;
            }
        }

        sealed class AspectRatioMediaFeature : MediaFeature
        {
            public AspectRatioMediaFeature(CSSValue value)
                : base(AspectRatio, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToAspectRatio();
                return length.HasValue;
            }
        }

        sealed class OrientationMediaFeature : MediaFeature
        {
            public OrientationMediaFeature(CSSValue value)
                : base(Orientation, value)
            {
            }

            public override Boolean Validate()
            {
                if (Value.Is("portrait"))
                    return true;
                else if (Value.Is("landscape"))
                    return true;

                return false;
            }
        }

        sealed class ScanMediaFeature : MediaFeature
        {
            public ScanMediaFeature(CSSValue value)
                : base(Scan, value)
            {
            }

            public override Boolean Validate()
            {
                if (Value.Is("progressive"))
                    return true;
                else if (Value.Is("interlace"))
                    return true;

                return false;
            }
        }

        sealed class GridMediaFeature : MediaFeature
        {
            public GridMediaFeature(CSSValue value)
                : base(Grid, value)
            {
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        #endregion
    }
}
