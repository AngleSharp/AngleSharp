namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents a medium rule.
    /// </summary>
    class CSSMedium : ICssObject
    {
        #region Media Types

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

        readonly static Dictionary<String, Object> features = new Dictionary<String, Object>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Fields



        #endregion

        #region ctor

        static CSSMedium()
        {
            features.Add("min-width", null);
            features.Add("max-width", null);
            features.Add("width", null);
            features.Add("min-height", null);
            features.Add("max-height", null);
            features.Add("height", null);
            features.Add("min-device-width", null);
            features.Add("max-device-width", null);
            features.Add("device-width", null);
            features.Add("min-device-height", null);
            features.Add("max-device-height", null);
            features.Add("device-height", null);
            features.Add("orientation", null);
            features.Add("min-aspect-ratio", null);
            features.Add("max-aspect-ratio", null);
            features.Add("aspect-ratio", null);
            features.Add("min-color", null);
            features.Add("max-color", null);
            features.Add("color", null);
            features.Add("min-color-index", null);
            features.Add("max-color-index", null);
            features.Add("color-index", null);
            features.Add("min-monochrome", null);
            features.Add("max-monochrome", null);
            features.Add("monochrome", null);
            features.Add("grid", null);
            features.Add("scan", null);
        }

        internal CSSMedium()
        {
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

        #endregion

        #region Methods

        public virtual Boolean Validate()
        {
            return true;
        }

        public virtual String ToCss()
        {
            var constraints = String.Empty;

            if (String.IsNullOrEmpty(constraints))
                return Type ?? String.Empty;
            else if (String.IsNullOrEmpty(Type))
                return constraints;

            return String.Concat(Type, " ", constraints);
        }

        internal void AddConstraint(String feature, CSSValue value)
        {
            //min-width : Length
            //    width : Length
            //max-width : Length
            //min-height : Length
            //    height : Length
            //max-height : Length
            //min-device-width : Length
            //    device-width : Length
            //max-device-width : Length
            //min-device-height : Length
            //    device-height : Length
            //max-device-height : Length
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
        }

        #endregion
    }

    sealed class CSSOnlyMedium : CSSMedium
    {
        public override String ToCss()
        {
            return String.Concat("only ", base.ToCss());
        }
    }

    sealed class CSSInvertMedium : CSSMedium
    {
        public override Boolean Validate()
        {
            return !base.Validate();
        }

        public override String ToCss()
        {
            return String.Concat("not ", base.ToCss());
        }
    }
}
