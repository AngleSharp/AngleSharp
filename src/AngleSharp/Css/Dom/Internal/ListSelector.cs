namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a group of selectors, i.e., zero or more selectors separated
    /// by commas.
    /// </summary>
    sealed class ListSelector : Selectors, ISelector
    {
        #region Methods

        public Boolean Match(IElement element, IElement scope)
        {
            for (var i = 0; i < _selectors.Count; i++)
            {
                if (_selectors[i].Match(element, scope))
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_selectors.Count > 0)
            {
                writer.Write(_selectors[0].Text);

                for (var i = 1; i < _selectors.Count; i++)
                {
                    writer.Write(Symbols.Comma);
                    writer.Write(_selectors[i].Text);
                }
            }
        }

        #endregion
    }
}
