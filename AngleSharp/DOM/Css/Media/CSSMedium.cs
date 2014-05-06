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

        readonly static Dictionary<String, Func<MediaFeature>> featureConstructors = new Dictionary<String, Func<MediaFeature>>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Fields

        List<MediaFeature> _features;

        #endregion

        #region ctor

        static CSSMedium()
        {
            featureConstructors.Add(MinWidth, () => new MinWidthMediaFeature());
            featureConstructors.Add(MaxWidth, () => new MaxWidthMediaFeature());
            featureConstructors.Add(Width, () => new WidthMediaFeature());
            featureConstructors.Add(MinHeight, () => new MinHeightMediaFeature());
            featureConstructors.Add(MaxHeight, () => new MaxHeightMediaFeature());
            featureConstructors.Add(Height, () => new HeightMediaFeature());
            featureConstructors.Add(MinDeviceWidth, () => new MinDeviceWidthMediaFeature());
            featureConstructors.Add(MaxDeviceWidth, () => new MaxDeviceWidthMediaFeature());
            featureConstructors.Add(DeviceWidth, () => new DeviceWidthMediaFeature());
            featureConstructors.Add(MinDeviceHeight, () => new MinDeviceHeightMediaFeature());
            featureConstructors.Add(MaxDeviceHeight, () => new MaxDeviceHeightMediaFeature());
            featureConstructors.Add(DeviceHeight, () => new DeviceHeightMediaFeature());
            featureConstructors.Add(Orientation, () => new OrientationMediaFeature());
            featureConstructors.Add(MinAspectRatio, () => new MinAspectRatioMediaFeature());
            featureConstructors.Add(MaxAspectRatio, () => new MaxAspectRatioMediaFeature());
            featureConstructors.Add(AspectRatio, () => new AspectRatioMediaFeature());
            featureConstructors.Add(MinColor, () => new MinColorMediaFeature());
            featureConstructors.Add(MaxColor, () => new MaxColorMediaFeature());
            featureConstructors.Add(Color, () => new ColorMediaFeature());
            featureConstructors.Add(MinColorIndex, () => new MinColorIndexMediaFeature());
            featureConstructors.Add(MaxColorIndex, () => new MaxColorIndexMediaFeature());
            featureConstructors.Add(ColorIndex, () => new ColorIndexMediaFeature());
            featureConstructors.Add(MinMonochrome, () => new MinMonochromeMediaFeature());
            featureConstructors.Add(MaxMonochrome, () => new MaxMonochromeMediaFeature());
            featureConstructors.Add(Monochrome, () => new MonochromeMediaFeature());
            featureConstructors.Add(MinResolution, () => new MinResolutionMediaFeature());
            featureConstructors.Add(MaxResolution, () => new MaxResolutionMediaFeature());
            featureConstructors.Add(Resolution, () => new ResolutionMediaFeature());
            featureConstructors.Add(Grid, () => new GridMediaFeature());
            featureConstructors.Add(Scan, () => new ScanMediaFeature());
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

        #region Feature

        abstract class MediaFeature : ICssObject
        {
            String _name;
            CSSValue _value;

            public MediaFeature(String name)
            {
                _name = name;
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
                protected set { _value = value; }
            }

            public abstract Boolean SetDefaultValue();

            public abstract Boolean SetValue(CSSValue value);

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
            public MinWidthMediaFeature()
                : base(MinWidth)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxWidthMediaFeature : MediaFeature
        {
            public MaxWidthMediaFeature()
                : base(MaxWidth)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class WidthMediaFeature : MediaFeature
        {
            public WidthMediaFeature()
                : base(Width)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinDeviceWidthMediaFeature : MediaFeature
        {
            public MinDeviceWidthMediaFeature()
                : base(MinDeviceWidth)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxDeviceWidthMediaFeature : MediaFeature
        {
            public MaxDeviceWidthMediaFeature()
                : base(MaxDeviceWidth)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class DeviceWidthMediaFeature : MediaFeature
        {
            public DeviceWidthMediaFeature()
                : base(DeviceWidth)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinHeightMediaFeature : MediaFeature
        {
            public MinHeightMediaFeature()
                : base(MinHeight)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxHeightMediaFeature : MediaFeature
        {
            public MaxHeightMediaFeature()
                : base(MaxHeight)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class HeightMediaFeature : MediaFeature
        {
            public HeightMediaFeature()
                : base(Height)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinDeviceHeightMediaFeature : MediaFeature
        {
            public MinDeviceHeightMediaFeature()
                : base(MinDeviceHeight)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MaxDeviceHeightMediaFeature : MediaFeature
        {
            public MaxDeviceHeightMediaFeature()
                : base(MaxDeviceHeight)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class DeviceHeightMediaFeature : MediaFeature
        {
            public DeviceHeightMediaFeature()
                : base(DeviceHeight)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToLength();
                return length.HasValue;
            }
        }

        sealed class MinColorIndexMediaFeature : MediaFeature
        {
            public MinColorIndexMediaFeature()
                : base(MinColorIndex)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MaxColorIndexMediaFeature : MediaFeature
        {
            public MaxColorIndexMediaFeature()
                : base(MaxColorIndex)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class ColorIndexMediaFeature : MediaFeature
        {
            public ColorIndexMediaFeature()
                : base(ColorIndex)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MinColorMediaFeature : MediaFeature
        {
            public MinColorMediaFeature()
                : base(MinColor)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MaxColorMediaFeature : MediaFeature
        {
            public MaxColorMediaFeature()
                : base(MaxColor)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class ColorMediaFeature : MediaFeature
        {
            public ColorMediaFeature()
                : base(Color)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MinMonochromeMediaFeature : MediaFeature
        {
            public MinMonochromeMediaFeature()
                : base(MinMonochrome)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MaxMonochromeMediaFeature : MediaFeature
        {
            public MaxMonochromeMediaFeature()
                : base(MaxMonochrome)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MonochromeMediaFeature : MediaFeature
        {
            public MonochromeMediaFeature()
                : base(Monochrome)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToInteger();
                return length.HasValue;
            }
        }

        sealed class MinResolutionMediaFeature : MediaFeature
        {
            public MinResolutionMediaFeature()
                : base(MinResolution)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToResolution();
                return length.HasValue;
            }
        }

        sealed class MaxResolutionMediaFeature : MediaFeature
        {
            public MaxResolutionMediaFeature()
                : base(MaxResolution)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToResolution();
                return length.HasValue;
            }
        }

        sealed class ResolutionMediaFeature : MediaFeature
        {
            public ResolutionMediaFeature()
                : base(Resolution)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToResolution();
                return length.HasValue;
            }
        }

        sealed class MinAspectRatioMediaFeature : MediaFeature
        {
            public MinAspectRatioMediaFeature()
                : base(MinAspectRatio)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToAspectRatio();
                return length.HasValue;
            }
        }

        sealed class MaxAspectRatioMediaFeature : MediaFeature
        {
            public MaxAspectRatioMediaFeature()
                : base(MaxAspectRatio)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToAspectRatio();
                return length.HasValue;
            }
        }

        sealed class AspectRatioMediaFeature : MediaFeature
        {
            public AspectRatioMediaFeature()
                : base(AspectRatio)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
            }

            public override Boolean Validate()
            {
                var length = Value.ToAspectRatio();
                return length.HasValue;
            }
        }

        sealed class OrientationMediaFeature : MediaFeature
        {
            public OrientationMediaFeature()
                : base(Orientation)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
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
            public ScanMediaFeature()
                : base(Scan)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
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
            public GridMediaFeature()
                : base(Grid)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return false;
            }

            public override Boolean SetValue(CSSValue value)
            {
                Value = value;
                return true;
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
