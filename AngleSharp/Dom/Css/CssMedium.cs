namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

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

        #region Fields

        readonly List<MediaFeature> _features;

        #endregion

        #region ctor

        public CssMedium()
        {
            _features = new List<MediaFeature>();
            Children = _features;
        }

        #endregion

        #region Properties

        public IEnumerable<IMediaFeature> Features
        {
            get { return _features; }
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
                var constraints = new String[_features.Count];

                for (int i = 0; i < _features.Count; i++)
                {
                    constraints[i] = _features[i].ToCss();
                }

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

            foreach (var feature in _features)
            {
                if (feature.Validate(device) == IsInverse)
                {
                    return false;
                }
            }

            return true;
        }

        public override Boolean Equals(Object obj)
        {
            var other = obj as CssMedium;

            if (other != null && 
                other.IsExclusive == IsExclusive && 
                other.IsInverse == IsInverse && 
                other.Type == Type && 
                other._features.Count == _features.Count)
            {
                foreach (var feature in other._features)
                {
                    var shared = _features.Find(m => m.Name.Is(feature.Name));

                    if (shared == null || !shared.Value.Is(feature.Value))
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

        #endregion

        #region Internal Methods

        internal void AddConstraint(MediaFeature feature)
        {
            _features.Add(feature);
        }

        internal void RemoveConstraint(MediaFeature feature)
        {
            _features.Remove(feature);
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var constraints = new String[_features.Count];

            for (int i = 0; i < _features.Count; i++)
            {
                constraints[i] = _features[i].ToCss(formatter);
            }

            return formatter.Medium(IsExclusive, IsInverse, Type, constraints);
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
