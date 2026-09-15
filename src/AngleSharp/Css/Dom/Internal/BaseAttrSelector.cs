namespace AngleSharp.Css.Dom
{
    using System;

    abstract class BaseAttrSelector
    {
        private readonly String _name;
        private readonly String? _prefix;
        private readonly String _attr;
        private readonly StringComparison _comparison;
        private readonly AttributeSelectorCaseSensitivity _caseSensitivity;

        public BaseAttrSelector(String name, String? prefix, Boolean insensitive = false, AttributeSelectorCaseSensitivity caseSensitivity = AttributeSelectorCaseSensitivity.Auto)
        {
            _name = name;
            _prefix = prefix;
            _comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            _caseSensitivity = caseSensitivity;

            if (!String.IsNullOrEmpty(prefix) && prefix is not "*")
            {
                _attr = String.Concat(prefix, ":", name);
            }
            else
            {
                _attr = name;
            }
        }

        public Priority Specificity => Priority.OneClass;

        protected String Attribute => !String.IsNullOrEmpty(_prefix) ? String.Concat(CssUtilities.Escape(_prefix!), "|", CssUtilities.Escape(_name)) : CssUtilities.Escape(_name);

        protected String Name => _attr;

        /// <summary>
        /// How the attribute value is compared, which the ASCII case-insensitive modifier selects.
        /// </summary>
        protected StringComparison Comparison => _comparison;

        /// <summary>
        /// The case-sensitivity modifier as it has to be written back out, or an empty string.
        /// </summary>
        /// <remarks>
        /// Only a modifier the author actually wrote is written back: the comparison alone cannot
        /// tell one apart from the case-insensitive match HTML gives ~44 attributes by name, so
        /// deriving it from the comparison both invents an "i" nobody asked for and drops an
        /// explicit "s" - which re-parses to a different match set.
        /// </remarks>
        protected String Modifier => _caseSensitivity switch
        {
            AttributeSelectorCaseSensitivity.CaseInsensitive => " i",
            AttributeSelectorCaseSensitivity.CaseSensitive => " s",
            _ => String.Empty,
        };
    }
}
