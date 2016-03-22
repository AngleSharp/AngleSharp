namespace AngleSharp.Css
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

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
            var sb = Pool.NewStringBuilder();
            WriteJoined(sb, rules, Environment.NewLine);
            return sb.ToPool();
        }

        String IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
        {
            var sb = Pool.NewStringBuilder().Append('{');

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    writer.Write(' ');
                    rule.ToCss(writer, this);
                }
            }

            return sb.Append(' ').Append('}').ToPool();
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important)
        {
            var rest = String.Concat(value, important ? " !important" : String.Empty);
            return String.Concat(name, ": ", rest);
        }

        String IStyleFormatter.Declarations(IEnumerable<String> declarations)
        {
            return String.Join("; ", declarations);
        }

        String IStyleFormatter.Medium(Boolean exclusive, Boolean inverse, String type, IEnumerable<IStyleFormattable> constraints)
        {
            var sb = Pool.NewStringBuilder();
            var first = true;

            if (exclusive)
            {
                sb.Append("only ");
            }
            else if (inverse)
            {
                sb.Append("not ");
            }
            
            if (!String.IsNullOrEmpty(type))
            {
                sb.Append(type);
                first = false;
            }

            WriteJoined(sb, constraints, " and ", first);
            return sb.ToPool();
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

        String IStyleFormatter.Style(String selector, IStyleFormattable rules)
        {
            var sb = Pool.NewStringBuilder().Append(selector).Append(" { ");
            var length = sb.Length;

            using (var writer = new StringWriter(sb))
            {
                rules.ToCss(writer, this);
            }

            if (sb.Length > length)
            {
                sb.Append(' ');
            }

            return sb.Append('}').ToPool();
        }

        String IStyleFormatter.Comment(String data)
        {
            return String.Join("/* ", data, " */");
        }

        #endregion

        #region Helpers

        void WriteJoined(StringBuilder sb, IEnumerable<IStyleFormattable> elements, String separator, Boolean first = true)
        {
            using (var writer = new StringWriter(sb))
            {
                foreach (var element in elements)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        writer.Write(separator);
                    }

                    element.ToCss(writer, this);
                }
            }
        }

        #endregion
    }
}
