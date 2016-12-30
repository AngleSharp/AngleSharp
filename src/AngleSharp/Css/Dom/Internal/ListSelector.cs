namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a group of selectors, i.e., zero or more selectors separated
    /// by commas.
    /// </summary>
    sealed class ListSelector : Selectors, ISelector
    {
        public void Accept(ISelectorVisitor visitor)
        {
            visitor.List(_selectors);
        }

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

        protected override String Stringify()
        {
            var parts = new String[_selectors.Count];

            for (var i = 0; i < _selectors.Count; i++)
            {
                parts[i] = _selectors[i].Text;
            }

            return String.Join(", ", parts);
        }
    }
}
