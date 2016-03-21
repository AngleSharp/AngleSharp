namespace AngleSharp.Dom.Css
{
    using System;
    using System.IO;

    /// <summary>
    /// Represents a group of selectors, i.e., zero or more selectors separated
    /// by commas.
    /// </summary>
    sealed class ListSelector : Selectors, ISelector
    {
        #region Properties

        public Boolean IsInvalid 
        { 
            get; 
            internal set; 
        }

        #endregion

        #region Methods

        public Boolean Match(IElement element)
        {
            for (var i = 0; i < _selectors.Count; i++)
            {
                if (_selectors[i].Match(element))
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
