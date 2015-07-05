namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the an CSS3 markup formatter with inserted intends.
    /// </summary>
    public class PrettyStyleFormatter : IStyleFormatter
    {
        #region Fields

        String _intendString;
        String _newLineString;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new instance of the pretty style formatter.
        /// </summary>
        public PrettyStyleFormatter()
        {
            _intendString = "\t";
            _newLineString = "\n";
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the indentation string.
        /// </summary>
        public String Indentation
        {
            get { return _intendString; }
            set { _intendString = value; }
        }

        /// <summary>
        /// Gets or sets the newline string.
        /// </summary>
        public String NewLine
        {
            get { return _newLineString; }
            set { _newLineString = value; }
        }

        #endregion

        #region Methods

        String IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
        {
            var lines = new List<String>();

            foreach (var rule in rules)
                lines.Add(rule.ToCss(this));

            return String.Join(_newLineString + _newLineString, lines);
        }

        String IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
        {
            var sb = Pool.NewStringBuilder().Append('{').Append(' ');

            foreach (var rule in rules)
            {
                var content = Intend(rule.ToCss(this));
                sb.Append(_newLineString).Append(content).Append(_newLineString);
            }

            return sb.Append('}').ToPool();
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important)
        {
            return CssStyleFormatter.Instance.Declaration(name, value, important);
        }

        String IStyleFormatter.Declarations(IEnumerable<String> declarations)
        {
            return String.Join(_newLineString, declarations);
        }

        String IStyleFormatter.Medium(Boolean exclusive, Boolean inverse, String type, String[] constraints)
        {
            return CssStyleFormatter.Instance.Medium(exclusive, inverse, type, constraints);
        }

        String IStyleFormatter.Constraint(String name, String value)
        {
            return CssStyleFormatter.Instance.Constraint(name, value);
        }

        String IStyleFormatter.Rule(String name, String value)
        {
            return CssStyleFormatter.Instance.Rule(name, value);
        }

        String IStyleFormatter.Rule(String name, String prelude, String rules)
        {
            return CssStyleFormatter.Instance.Rule(name, prelude, rules);
        }

        String IStyleFormatter.Style(String selector, String rules)
        {
            if (!String.IsNullOrEmpty(rules))
            {
                var sb = Pool.NewStringBuilder().Append(selector);
                sb.Append(' ').Append('{');
                sb.Append(_newLineString);
                sb.Append(Intend(rules));
                sb.Append(_newLineString);
                return sb.Append('}').ToPool();
            }

            return selector + " { }";
        }

        #endregion

        #region Helpers

        String Intend(String content)
        {
            return _intendString + content.Replace(_newLineString, _newLineString + _intendString);
        }

        #endregion
    }
}
