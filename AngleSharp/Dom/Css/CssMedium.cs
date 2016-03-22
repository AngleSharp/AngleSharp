namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a medium rule. More information available at:
    /// http://www.w3.org/TR/css3-mediaqueries/
    /// </summary>
    sealed class CssMedium : CssNode, ICssMedium
    {
        #region Media Types and Features

        readonly static String[] KnownTypes = 
        {
            // Intended for non-paged computer screens.
            Keywords.Screen,
            // Intended for speech synthesizers.
            Keywords.Speech,
            // Intended for paged material and for documents viewed on screen in print preview mode.
            Keywords.Print,
            // Suitable for all devices.
            Keywords.All
        };

        #endregion

        #region Properties

        public IEnumerable<MediaFeature> Features
        {
            get { return Children.OfType<MediaFeature>(); }
        }

        IEnumerable<IMediaFeature> ICssMedium.Features
        {
            get { return Features; }
        }

        public String Type
        {
            get;
            internal set;
        }

        public Boolean IsExclusive
        {
            get;
            internal set;
        }

        public Boolean IsInverse
        {
            get;
            internal set;
        }

        public String Constraints
        {
            get 
            {
                var constraints = Features.Select(m => m.ToCss());
                return String.Join(" and ", constraints);
            }
        }

        #endregion

        #region Methods

        public Boolean Validate(RenderDevice device)
        {
            if (!String.IsNullOrEmpty(Type) && KnownTypes.Contains(Type) == IsInverse)
            {
                return false;
            }

            if (IsInvalid(device))
            {
                return false;
            }

            return !Features.Any(m => m.Validate(device) == IsInverse);
        }

        public override Boolean Equals(Object obj)
        {
            var other = obj as CssMedium;

            if (other != null && 
                other.IsExclusive == IsExclusive && 
                other.IsInverse == IsInverse && 
                other.Type.Is(Type) && 
                other.Features.Count() == Features.Count())
            {
                foreach (var feature in other.Features)
                {
                    var isShared = Features.Any(m => m.Name.Is(feature.Name) && m.Value.Is(feature.Value));

                    if (!isShared)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            writer.Write(formatter.Medium(IsExclusive, IsInverse, Type, Features));
        }

        #endregion

        #region Helpers

        Boolean IsInvalid(RenderDevice device)
        {
            return IsInvalid(device, Keywords.Screen, RenderDevice.Kind.Screen) ||
                IsInvalid(device, Keywords.Speech, RenderDevice.Kind.Speech) ||
                IsInvalid(device, Keywords.Print, RenderDevice.Kind.Printer);
        }

        Boolean IsInvalid(RenderDevice device, String keyword, RenderDevice.Kind kind)
        {
            return keyword.Is(Type) && (device.DeviceType == kind) == IsInverse;
        }

        #endregion
    }
}
