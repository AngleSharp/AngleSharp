namespace AngleSharp.Css.Dom
{
    using AngleSharp.Text;
    using System;

    abstract class BaseAttrSelector
    {
        private readonly String _name;
        private readonly String _prefix;
        private readonly String _attr;

        public BaseAttrSelector(String name, String prefix)
        {
            _name = name;
            _prefix = prefix;

            if (!String.IsNullOrEmpty(prefix) && !prefix.Is("*"))
            {
                _attr = String.Concat(prefix, ":", name);
            }
            else
            {
                _attr = name;
            }
        }

        public Priority Specificity => Priority.OneClass;

        protected String Attribute => !String.IsNullOrEmpty(_prefix) ? String.Concat(_prefix, "|", _name) : _name;

        protected String Name => _attr;
    }
}
