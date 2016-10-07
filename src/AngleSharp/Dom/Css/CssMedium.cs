namespace AngleSharp.Dom.Css
{
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
        #region Properties

        public IEnumerable<IMediaFeature> Features
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
    }
}
