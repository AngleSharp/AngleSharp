namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a medium rule.
    /// </summary>
    abstract class CSSMedium : ICssObject
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

        #endregion

        /// <summary>
        /// Gets the type of medium that is represented.
        /// </summary>
        public String Type
        {
            get;
            internal set;
        }

        public virtual Boolean Validate()
        {
            return true;
        }

        public abstract String ToCss();

        internal void AddConstraint(String feature, CSSValue value)
        {
            //min-width
            //width
            //max-width
            //..
        }
    }

    sealed class OnlyMedium : CSSMedium
    {
        public override String ToCss()
        {
            throw new NotImplementedException();
        }
    }

    sealed class NormalMedium : CSSMedium
    {
        public override String ToCss()
        {
            throw new NotImplementedException();
        }
    }

    sealed class InvertMedium : CSSMedium
    {
        public override Boolean Validate()
        {
            return !base.Validate();
        }

        public override String ToCss()
        {
            throw new NotImplementedException();
        }
    }
}
