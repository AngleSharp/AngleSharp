namespace AngleSharp.DOM.Css
{
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
                : base(FeatureNames.MinWidth)
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
                : base(FeatureNames.MaxWidth)
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
                : base(FeatureNames.Width)
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
                : base(FeatureNames.MinDeviceWidth)
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
                : base(FeatureNames.MaxDeviceWidth)
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
                : base(FeatureNames.DeviceWidth)
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
                : base(FeatureNames.MinHeight)
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
                : base(FeatureNames.MaxHeight)
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
                : base(FeatureNames.Height)
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
                : base(FeatureNames.MinDeviceHeight)
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
                : base(FeatureNames.MaxDeviceHeight)
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
                : base(FeatureNames.DeviceHeight)
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
                : base(FeatureNames.MinColorIndex)
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
                : base(FeatureNames.MaxColorIndex)
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
                : base(FeatureNames.ColorIndex)
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
                : base(FeatureNames.MinColor)
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
                : base(FeatureNames.MaxColor)
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
                : base(FeatureNames.Color)
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
                : base(FeatureNames.MinMonochrome)
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
                : base(FeatureNames.MaxMonochrome)
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
                : base(FeatureNames.Monochrome)
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
                : base(FeatureNames.MinResolution)
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
                : base(FeatureNames.MaxResolution)
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
                : base(FeatureNames.Resolution)
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
                : base(FeatureNames.MinAspectRatio)
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
                : base(FeatureNames.MaxAspectRatio)
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
                : base(FeatureNames.AspectRatio)
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
                : base(FeatureNames.Orientation)
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
                : base(FeatureNames.Scan)
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
                : base(FeatureNames.Grid)
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
