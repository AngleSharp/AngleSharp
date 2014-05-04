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
        readonly static String MinColor = "min-color";
        readonly static String MinColorIndex = "min-color-index";
        readonly static String MinMonochrome = "min-monochrome";
        readonly static String MaxWidth = "max-width";
        readonly static String MaxHeight = "max-height";
        readonly static String MaxDeviceWidth = "max-device-width";
        readonly static String MaxDeviceHeight = "max-device-height";
        readonly static String MaxAspectRatio = "max-aspect-ratio";
        readonly static String MaxColor = "max-color";
        readonly static String MaxColorIndex = "max-color-index";
        readonly static String MaxMonochrome = "max-monochrome";
        readonly static String Width = "width";
        readonly static String Height = "height";
        readonly static String DeviceWidth = "device-width";
        readonly static String DeviceHeight = "device-height";
        readonly static String AspectRatio = "aspect-ratio";
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
        Boolean _invalid;

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
            //features.Add(Orientation, null);
            //features.Add(MinAspectRatio, null);
            //features.Add(MaxAspectRatio, null);
            //features.Add(AspectRatio, null);
            //features.Add(MinColor, null);
            //features.Add(MaxColor, null);
            //features.Add(Color, null);
            //features.Add(MinColorIndex, null);
            //features.Add(MaxColorIndex, null);
            //features.Add(ColorIndex, null);
            //features.Add(MinMonochrome, null);
            //features.Add(MaxMonochrome, null);
            //features.Add(Monochrome, null);
            //features.Add(Grid, null);
            //features.Add(Scan, null);
        }

        internal CSSMedium()
        {
            _features = new List<MediaFeature>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the given medium is invalid and should always
        /// evaluated to false.
        /// </summary>
        public Boolean Invalid
        {
            get { return _invalid; }
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
            foreach (var feature in _features)
            {
                if (!feature.Validate())
                    return false;
            }

            return !Invalid;
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
        internal void AddConstraint(String feature, CSSValue value = null)
        {
            Func<CSSValue, MediaFeature> constructor;

            if (featureConstructors.TryGetValue(feature, out constructor))
                _features.Add(constructor(value));
            else
                _invalid = true;
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

        //orientation : portrait | landscape
        //min-aspect-ratio : Ratio e.g. 3/4
        //    aspect-ratio : Ratio
        //max-aspect-ratio : Ratio
        //min-color : Integer
        //    color : Integer
        //max-color : Integer
        //min-color-index : Integer
        //    color-index : Integer
        //max-color-index : Integer
        //min-monochrome : Integer
        //    monochrome : Integer
        //max-monochrome : Integer
        //min-resolution : Resolution
        //    resolution : Resolution
        //max-resolution : Resolution
        //scan : progressive | interlace
        //grid : Integer
        //..

        #endregion
    }
}
