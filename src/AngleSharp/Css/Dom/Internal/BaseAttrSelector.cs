namespace AngleSharp.Css.Dom
{
    using System;

    abstract class BaseAttrSelector
    {
        private readonly String _name;
        private readonly String? _prefix;
        private readonly String _attr;
        private readonly StringComparison _comparison;
        private AttributeSelectorCaseSensitivity _caseSensitivity;

        public BaseAttrSelector(String name, String? prefix, Boolean insensitive = false)
        {
            _name = name;
            _prefix = prefix;
            _comparison = insensitive ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;

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
        /// States which modifier the selector was written with, which the comparison cannot say:
        /// it is already resolved and reads the same whether the author asked for it or HTML did.
        /// </summary>
        public void DeclareCaseSensitivity(AttributeSelectorCaseSensitivity caseSensitivity) => _caseSensitivity = caseSensitivity;

        /// <summary>
        /// The case-sensitivity modifier as CSSOM asks for it, or an empty string.
        /// </summary>
        /// <remarks>
        /// CSSOM appends the modifier only if the flag is present on the selector, so only one the
        /// author actually wrote is serialized - never the case-insensitive match HTML gives some
        /// ~44 attributes by name.
        /// </remarks>
        protected String Modifier => _caseSensitivity switch
        {
            AttributeSelectorCaseSensitivity.CaseInsensitive => " i",
            AttributeSelectorCaseSensitivity.CaseSensitive => " s",
            _ => String.Empty,
        };
    }
}
