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
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    _length = length.Value;
                    Value = value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxWidthMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class WidthMediaFeature : MediaFeature
        {
            Length _length;

            public WidthMediaFeature()
                : base(Width)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinDeviceWidthMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxDeviceWidthMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false; 
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class DeviceWidthMediaFeature : MediaFeature
        {
            Length _length;

            public DeviceWidthMediaFeature()
                : base(DeviceWidth)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinHeightMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxHeightMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class HeightMediaFeature : MediaFeature
        {
            Length _length;

            public HeightMediaFeature()
                : base(Height)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinDeviceHeightMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxDeviceHeightMediaFeature : MediaFeature
        {
            Length _length;

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
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class DeviceHeightMediaFeature : MediaFeature
        {
            Length _length;

            public DeviceHeightMediaFeature()
                : base(DeviceHeight)
            {
            }

            public override Boolean SetDefaultValue()
            {
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var length = value.ToLength();

                if (length.HasValue)
                {
                    Value = value;
                    _length = length.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinColorIndexMediaFeature : MediaFeature
        {
            Int32 _index;

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
                var index = value.ToInteger();

                if (index.HasValue && index.Value >= 0)
                {
                    Value = value;
                    _index = index.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxColorIndexMediaFeature : MediaFeature
        {
            Int32 _index;

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
                var index = value.ToInteger();

                if (index.HasValue && index.Value >= 0)
                {
                    Value = value;
                    _index = index.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class ColorIndexMediaFeature : MediaFeature
        {
            Int32 _index;

            public ColorIndexMediaFeature()
                : base(ColorIndex)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _index = 0;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var index = value.ToInteger();

                if (index.HasValue && index.Value >= 0)
                {
                    Value = value;
                    _index = index.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinColorMediaFeature : MediaFeature
        {
            Int32 _color;

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
                var color = value.ToInteger();

                if (color.HasValue && color.Value > 0)
                {
                    Value = value;
                    _color = color.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxColorMediaFeature : MediaFeature
        {
            Int32 _color;

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
                var color = value.ToInteger();

                if (color.HasValue && color.Value > 0)
                {
                    Value = value;
                    _color = color.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class ColorMediaFeature : MediaFeature
        {
            Int32 _color;

            public ColorMediaFeature()
                : base(Color)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _color = 1;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var color = value.ToInteger();

                if (color.HasValue && color.Value > 0)
                {
                    Value = value;
                    _color = color.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinMonochromeMediaFeature : MediaFeature
        {
            Int32 _index;

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
                var index = value.ToInteger();

                if (index.HasValue && index.Value >= 0)
                {
                    Value = value;
                    _index = index.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxMonochromeMediaFeature : MediaFeature
        {
            Int32 _index;

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
                var index = value.ToInteger();

                if (index.HasValue && index.Value >= 0)
                {
                    Value = value;
                    _index = index.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MonochromeMediaFeature : MediaFeature
        {
            Int32 _index;

            public MonochromeMediaFeature()
                : base(Monochrome)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _index = 0;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var index = value.ToInteger();

                if (index.HasValue && index.Value >= 0)
                {
                    Value = value;
                    _index = index.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinResolutionMediaFeature : MediaFeature
        {
            Resolution _res;

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
                var res = value.ToResolution();

                if (res.HasValue)
                {
                    Value = value;
                    _res = res.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxResolutionMediaFeature : MediaFeature
        {
            Resolution _res;

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
                var res = value.ToResolution();

                if (res.HasValue)
                {
                    Value = value;
                    _res = res.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class ResolutionMediaFeature : MediaFeature
        {
            Resolution _res;

            public ResolutionMediaFeature()
                : base(Resolution)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _res = new Resolution(72f, DOM.Resolution.Unit.Dpi);
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var res = value.ToResolution();

                if (res.HasValue)
                {
                    Value = value;
                    _res = res.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MinAspectRatioMediaFeature : MediaFeature
        {
            Single _ratio;

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
                var ratio = value.ToAspectRatio();

                if (ratio.HasValue)
                {
                    Value = value;
                    _ratio = ratio.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class MaxAspectRatioMediaFeature : MediaFeature
        {
            Single _ratio;

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
                var ratio = value.ToAspectRatio();

                if (ratio.HasValue)
                {
                    Value = value;
                    _ratio = ratio.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class AspectRatioMediaFeature : MediaFeature
        {
            Single _ratio;

            public AspectRatioMediaFeature()
                : base(AspectRatio)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _ratio = 1f;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var ratio = value.ToAspectRatio();

                if (ratio.HasValue)
                {
                    Value = value;
                    _ratio = ratio.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class OrientationMediaFeature : MediaFeature
        {
            Boolean _portrait;
            Boolean _landscape;

            public OrientationMediaFeature()
                : base(Orientation)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _portrait = true;
                _landscape = true;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                if (Value.Is("portrait"))
                {
                    Value = value;
                    _portrait = true;
                    _landscape = false;
                    return true;
                }
                else if (Value.Is("landscape"))
                {
                    Value = value;
                    _portrait = false;
                    _landscape = true;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class ScanMediaFeature : MediaFeature
        {
            Boolean _progressive;
            Boolean _interlace;

            public ScanMediaFeature()
                : base(Scan)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _progressive = true;
                _interlace = true;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                if (Value.Is("progressive"))
                {
                    Value = value;
                    _progressive = true;
                    _interlace = false;
                    return true;
                }
                else if (Value.Is("interlace"))
                {
                    Value = value;
                    _progressive = false;
                    _interlace = true;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        sealed class GridMediaFeature : MediaFeature
        {
            Int32 _grid;

            public GridMediaFeature()
                : base(Grid)
            {
            }

            public override Boolean SetDefaultValue()
            {
                _grid = 0;
                return true;
            }

            public override Boolean SetValue(CSSValue value)
            {
                var grid = value.ToInteger();

                if (grid.HasValue && grid.Value >= 0)
                {
                    Value = value;
                    _grid = grid.Value;
                    return true;
                }

                return false;
            }

            public override Boolean Validate()
            {
                return true;
            }
        }

        #endregion
    }
}
