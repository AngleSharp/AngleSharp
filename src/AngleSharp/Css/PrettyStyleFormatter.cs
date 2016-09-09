namespace AngleSharp.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents the an CSS3 markup formatter with inserted intends.
    /// </summary>
    public class PrettyStyleFormatter : IStyleFormatter
    {
        #region Fields

        private String _intendString;
        private String _newLineString;

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
            var sb = Pool.NewStringBuilder();
            var first = true;

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    if (first)
                    {
                        first = false;
                    }
                    else
                    {
                        writer.Write(_newLineString);
                        writer.Write(_newLineString);
                    }

                    rule.ToCss(writer, this);
                }
            }

            return sb.ToPool();
        }

        String IStyleFormatter.Block(IEnumerable<IStyleFormattable> rules)
        {
            var sb = Pool.NewStringBuilder().Append('{').Append(' ');

            using (var writer = new StringWriter(sb))
            {
                foreach (var rule in rules)
                {
                    writer.Write(_newLineString);
                    writer.Write(Intend(rule.ToCss(this)));
                    writer.Write(_newLineString);
                }
            }

            return sb.Append('}').ToPool();
        }

        String IStyleFormatter.Declaration(String name, String value, Boolean important)
        {
            return CssStyleFormatter.Instance.Declaration(name, value, important);
        }

        String IStyleFormatter.Declarations(IEnumerable<String> declarations)
        {
            return String.Join(_newLineString, declarations.Select(m => m + ";"));
        }

        String IStyleFormatter.Medium(Boolean exclusive, Boolean inverse, String type, IEnumerable<IStyleFormattable> constraints)
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

        String IStyleFormatter.Style(String selector, IStyleFormattable rules)
        {
            var sb = Pool.NewStringBuilder().Append(selector).Append(" {");
            var content = rules.ToCss(this);

            if (!String.IsNullOrEmpty(content))
            {
                sb.Append(_newLineString);
                sb.Append(Intend(content));
                sb.Append(_newLineString);
            }
            else
            {
                sb.Append(' ');
            }

            return sb.Append('}').ToPool();
        }

        String IStyleFormatter.Comment(String data)
        {
            return CssStyleFormatter.Instance.Comment(data);
        }

        #endregion

        #region Helpers

        private String Intend(String content)
        {
            return _intendString + content.Replace(_newLineString, _newLineString + _intendString);
        }

        #endregion
    }
}
