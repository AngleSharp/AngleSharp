namespace AngleSharp.Css.Dom
{
    using System;

    abstract class BaseAttrSelector
    {
        private readonly String _name;
        private readonly String? _prefix;
        private readonly String _attr;
        private readonly StringComparison _comparison;

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
        /// The case-insensitive modifier as it has to be written back out, or an empty string.
        /// </summary>
        /// <remarks>
        /// Leaving it out of <see cref="ISelector.Text"/> would turn a case-insensitive selector
        /// into a case-sensitive one the next time that text is parsed, which is what every
        /// consumer storing or forwarding a selector ends up doing.
        /// </remarks>
        protected String Modifier => _comparison == StringComparison.OrdinalIgnoreCase ? " i" : String.Empty;
    }
}
