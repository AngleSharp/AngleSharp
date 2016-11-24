namespace AngleSharp.Css
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;

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
            var sb = StringBuilderPool.Obtain();
            var sep = Environment.NewLine;

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    rule.ToCss(writer, this);
                    writer.Write(sep);
                }

                sb.Remove(sb.Length - sep.Length, sep.Length);
            }
            
            return sb.ToPool();
        }

        String IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
        {
            var sb = StringBuilderPool.Obtain().Append('{');

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

        String IStyleFormatter.Rule(String name, String value)
        {
            return String.Concat(name, " ", value, ";");
        }

        String IStyleFormatter.Rule(String name, String prelude, String rules)
        {
            var text = String.IsNullOrEmpty(prelude) ? String.Empty : prelude + " ";
            return String.Concat(name, " ", text, rules);
        }

        String IStyleFormatter.Comment(String data)
        {
            return String.Join("/* ", data, " */");
        }

        #endregion
    }
}
