namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;

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

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            for (var i = 0; i < _selectors.Count; i++)
            {
                writer.Write(_selectors[i].Text);
            }
        }

        #endregion
    }
}
