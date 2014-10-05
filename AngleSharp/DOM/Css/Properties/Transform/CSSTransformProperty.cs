namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform
    /// </summary>
    sealed class CSSTransformProperty : CSSProperty, ICssTransformProperty
    {
        #region Fields

        List<ITransform> _transforms;

        #endregion

        #region ctor

        internal CSSTransformProperty()
            : base(PropertyNames.Transform, PropertyFlags.Animatable)
        {
            _transforms = new List<ITransform>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration over all transformations.
        /// </summary>
        public IEnumerable<ITransform> Transforms
        {
            get { return _transforms; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.None))
            {
                _transforms.Clear();
                return true;
            }

            var transform = value.ToTransform();

            if (transform != null)
            {
                _transforms.Clear();
                _transforms.Add(transform);
                return true;
            }

            var list = value as CSSValueList;

            if (list != null)
            {
                var transforms = new ITransform[list.Length];

                for (int i = 0; i < transforms.Length; i++)
                {
                    transforms[i] = list[i].ToTransform();

                    if (transforms[i] == null)
                        return false;
                }

                _transforms.Clear();
                _transforms.AddRange(transforms);
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}
