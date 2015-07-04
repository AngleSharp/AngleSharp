namespace AngleSharp.Css
{
    using System;

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

        String IStyleFormatter.Sheet(String[] rules)
        {
            var sb = Pool.NewStringBuilder();

            if (rules.Length > 0)
            {
                sb.Append(rules[0]);

                for (int i = 1; i < rules.Length; i++)
                {
                    sb.AppendLine().Append(rules[i]);
                }
            }

            return sb.ToPool();
        }
        String IStyleFormatter.Declaration(String name, String value, Boolean important)
        {
            var rest = String.Concat(value, important ? " !important" : String.Empty);
            return String.Concat(name, ": ", rest, ";");
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

        #endregion
    }
}
