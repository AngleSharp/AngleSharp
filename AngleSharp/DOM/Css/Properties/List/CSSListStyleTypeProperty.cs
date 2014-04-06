namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/list-style-type
    /// </summary>
    sealed class CSSListStyleTypeProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, ListStyle> styles = new Dictionary<String, ListStyle>(StringComparer.OrdinalIgnoreCase);
        ListStyle _style;

        #endregion

        #region ctor

        static CSSListStyleTypeProperty()
        {
            styles.Add("disc", ListStyle.Disc);
            styles.Add("circle", ListStyle.Circle);
            styles.Add("square", ListStyle.Square);
            styles.Add("decimal", ListStyle.Decimal);
            styles.Add("decimal-leading-zero", ListStyle.DecimalLeadingZero);
            styles.Add("lower-roman", ListStyle.LowerRoman);
            styles.Add("upper-roman", ListStyle.UpperRoman);
            styles.Add("lower-greek", ListStyle.LowerGreek);
            styles.Add("lower-latin", ListStyle.LowerLatin);
            styles.Add("upper-latin", ListStyle.UpperLatin);
            styles.Add("armenian", ListStyle.Armenian);
            styles.Add("georgian", ListStyle.Georgian);
            styles.Add("lower-alpha", ListStyle.LowerLatin);
            styles.Add("upper-alpha", ListStyle.UpperLatin);
            styles.Add("none", ListStyle.None);
        }

        public CSSListStyleTypeProperty()
            : base(PropertyNames.ListStyleType)
        {
            _inherited = true;
            _style = ListStyle.Disc;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                ListStyle position;

                if (styles.TryGetValue(ident.Value, out position))
                {
                    _style = position;
                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion

        #region Positions

        enum ListStyle
        {
            /// <summary>
            /// No list style at all.
            /// </summary>
            None,
            /// <summary>
            /// A filled circle (default value)
            /// </summary>
            Disc,
            /// <summary>
            /// A hollow circle
            /// </summary>
            Circle,
            /// <summary>
            /// A filled square
            /// </summary>
            Square,
            /// <summary>
            /// Han decimal numbers, Beginning with 1.
            /// </summary>
            Decimal,
            /// <summary>
            /// Decimal numbers, Padded by initial zeros, E.g. 01, 02, 03, ... 98, 99
            /// </summary>
            DecimalLeadingZero,
            /// <summary>
            /// Lowercase roman numerals, E.g.i, ii, iii, iv, v, ...
            /// </summary>
            LowerRoman,
            /// <summary>
            /// Uppercase roman numerals, E.g.I, II, III, IV, V ...
            /// </summary>
            UpperRoman,
            /// <summary>
            /// Lowercase classical Greek, alpha, beta, gamma…, E.g.α, β, γ ...
            /// </summary>
            LowerGreek,
            /// <summary>
            /// Lowercase ASCII letters, E.g.a, b, c, ... z
            /// </summary>
            LowerLatin,
            /// <summary>
            /// Uppercase ASCII letters, E.g.A, B, C, ... Z
            /// </summary>
            UpperLatin,
            /// <summary>
            /// Traditional Armenian numbering, (ayb/ayp, ben/pen, gim/keem ... )
            /// </summary>
            Armenian,
            /// <summary>
            /// Traditional Georgian numbering, E.g.an, ban, gan, ... he, tan, in…
            /// </summary>
            Georgian
        }

        #endregion
    }
}
