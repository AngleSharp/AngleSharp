namespace AngleSharp.Dom.Css
{
    using System;

    /// <summary>
    /// Represents a compound selector, which is a chain of simple selectors
    /// that are not separated by a combinator.
    /// </summary>
    sealed class CompoundSelector : Selectors, ISelector
    {
        #region Methods

        public Boolean Match(IElement element)
        {
            for (var i = 0; i < _selectors.Count; i++)
            {
                if (!_selectors[i].Match(element))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var sb = Pool.NewStringBuilder();

            for (var i = 0; i < _selectors.Count; i++)
            {
                sb.Append(_selectors[i].Text);
            }

            return sb.ToPool();
        }

        #endregion
    }
}
