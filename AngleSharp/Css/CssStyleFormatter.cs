namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the standard CSS3 style formatter.
    /// </summary>
    public sealed class CssStyleFormatter : IStyleFormatter
    {
        #region Instance

        /// <summary>
        /// An instance of the CssStyleFormatter.
        /// </summary>
        public static readonly IStyleFormatter Instance = new CssStyleFormatter();

        #endregion

        #region Methods

        String IStyleFormatter.Sheet(IEnumerable<IStyleFormattable> rules)
        {
            var lines = new List<String>();

            foreach (var rule in rules)
                lines.Add(rule.ToCss(this));

            return String.Join(Environment.NewLine, lines);
        }

        String IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
        {
            var sb = Pool.NewStringBuilder().Append('{');

            foreach (var rule in rules)
                sb.Append(' ').Append(rule.ToCss(this));

            return sb.Append(' ').Append('}').ToPool();
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important)
        {
            var rest = String.Concat(value, important ? " !important" : String.Empty);
            return String.Concat(name, ": ", rest);
        }

        String IStyleFormatter.Medium(Boolean exclusive, Boolean inverse, String type, String[] constraints)
        {
            var prefix = exclusive ? "only " : (inverse ? "not " : String.Empty);

            if (constraints.Length != 0)
            {
                var constraint = String.Join(" and ", constraints);

                if (String.IsNullOrEmpty(type))
                    return String.Concat(prefix, constraint);

                return String.Concat(prefix, type, " and ", constraint);
            }

            return String.Concat(prefix, type ?? String.Empty);
        }

        string IStyleFormatter.Constraint(String name, String value)
        {
            var ending = value != null ? ": " + value : String.Empty;
            return String.Concat("(", name, ending, ")");
        }

        String IStyleFormatter.Rule(String name, String value)
        {
            return String.Concat(name, " ", value, ";");
        }

        String IStyleFormatter.Rule(String name, String prelude, String rules)
        {
            var text = String.IsNullOrEmpty(prelude) ? String.Empty : prelude + " ";
            return String.Concat(name, " ", text, rules);
        }

        String IStyleFormatter.Style(String selector, String rules)
        {
            var open = String.IsNullOrEmpty(rules) ? " {" : " { ";
            return String.Concat(selector, open, rules, " }");
        }

        String IStyleFormatter.Declarations(IEnumerable<String> declarations)
        {
            return String.Join("; ", declarations);
        }

        String IStyleFormatter.Comment(String data)
        {
            return String.Join("/* ", data, " */");
        }

        #endregion
    }
}
