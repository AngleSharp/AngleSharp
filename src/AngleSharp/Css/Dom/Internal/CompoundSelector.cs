namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Represents a compound selector, which is a chain of simple selectors
    /// that are not separated by a combinator.
    /// </summary>
    sealed class CompoundSelector : Selectors, ISelector
    {
        public Boolean Match(IElement element, IElement scope)
        {
            for (var i = 0; i < _selectors.Count; i++)
            {
                if (!_selectors[i].Match(element, scope))
                {
                    return false;
                }
            }

            return true;
        }

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Many(_selectors);
        }

        protected override String Stringify()
        {
            var parts = new String[_selectors.Count];

            for (var i = 0; i < _selectors.Count; i++)
            {
                parts[i] = _selectors[i].Text;
            }

            return String.Concat(parts);
        }
    }
}
