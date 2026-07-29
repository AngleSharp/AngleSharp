
namespace AngleSharp.Dom.Geometry
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the DomPoint class.
    /// </summary>
    [DomName("DOMPoint")]
    [DomName("SVGPoint")]
    [DomExposed("Window")]
    [DomExposed("Worker")]
    public class DomPoint : DomPointReadOnly, IDomPoint
    {
        /// <summary>
        /// Initializes a new instance of the DomPoint class.
        /// </summary>
        [DomConstructor]
        public DomPoint(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double w = 1.0)
            : base(x, y, z, w)
        {
        }

        /// <summary>
        /// Provides the member value.
        /// </summary>
        [DomName("x")]
        public new Double X
        {
            get => base.X;
            set => base.X = value;
        }

        /// <summary>
        /// Provides the member value.
        /// </summary>
        [DomName("y")]
        public new Double Y
        {
            get => base.Y;
            set => base.Y = value;
        }

        /// <summary>
        /// Provides the member value.
        /// </summary>
        [DomName("z")]
        public new Double Z
        {
            get => base.Z;
            set => base.Z = value;
        }

        /// <summary>
        /// Provides the member value.
        /// </summary>
        [DomName("w")]
        public new Double W
        {
            get => base.W;
            set => base.W = value;
        }

        /// <summary>
        /// Provides the member value.
        /// </summary>
        [DomName("fromPoint")]
        public static new DomPoint FromPoint(DomPointInit? other = null)
        {
            other ??= new DomPointInit();
            return new DomPoint(other.X, other.Y, other.Z, other.W);
        }
    }
}
