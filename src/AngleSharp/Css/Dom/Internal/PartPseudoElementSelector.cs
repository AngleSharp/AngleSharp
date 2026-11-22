namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;
    using System.Linq;

    /// <summary>
    /// The `::part()` pseudo-element (CSS Shadow Parts 1): it stands for the elements of the
    /// shadow tree of the host that have all the given names in their `part` attribute. Those
    /// are styled as the pseudo-element of the host named `part(...)` with all their part names
    /// (see PseudoElementNames.Part).
    /// </summary>
    sealed class PartPseudoElementSelector : ISelector
    {
        private readonly String[] _names;

        public PartPseudoElementSelector(String[] names)
        {
            _names = names;
        }

        public Priority Specificity => Priority.OneTag;

        public String Text => PseudoElementNames.Separator + PseudoElementNames.Part + "(" + String.Join(" ", _names.Select(CssUtilities.Escape)) + ")";

        public void Accept(ISelectorVisitor visitor) => visitor.PseudoElement(PseudoElementNames.Part);

        public Boolean Match(IElement element, IElement? scope)
        {
            if (element is not IPseudoElement pseudo || !pseudo.PseudoName.StartsWith(PseudoElementNames.Part + "(", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            var given = pseudo.PseudoName.Substring(PseudoElementNames.Part.Length + 1).TrimEnd(')').Split(' ');
            return _names.All(name => given.Contains(name, StringComparer.Ordinal));
        }
    }
}
