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

                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - sep.Length, sep.Length);
                }
            }
            
            return sb.ToPool();
        }

        String IStyleFormatter.BlockRules(IEnumerable<IStyleFormattable> rules)
        {
            var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    writer.Write(Symbols.Space);
                    rule.ToCss(writer, this);
                }
            }

            return sb.Append(Symbols.Space).Append(Symbols.CurlyBracketClose).ToPool();
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important) => String.Concat(name, ": ", String.Concat(value, important ? " !important" : String.Empty));

        String IStyleFormatter.BlockDeclarations(IEnumerable<IStyleFormattable> declarations)
        {
            var sb = StringBuilderPool.Obtain().Append(Symbols.CurlyBracketOpen);

            using (var writer = new StringWriter(sb))
            {
                foreach (var declaration in declarations)
                {
                    writer.Write(Symbols.Space);
                    declaration.ToCss(writer, this);
                    writer.Write(Symbols.Semicolon);
                }

                if (sb.Length > 1)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
            }

            return sb.Append(Symbols.Space).Append(Symbols.CurlyBracketClose).ToPool();
        }

        String IStyleFormatter.Rule(String name, String value) => String.Concat(name, " ", value, ";");

        String IStyleFormatter.Rule(String name, String prelude, String rules) => String.Concat(name, " ", String.IsNullOrEmpty(prelude) ? String.Empty : prelude + " ", rules);

        String IStyleFormatter.Comment(String data) => String.Join("/*", data, "*/");

        #endregion
    }
}
