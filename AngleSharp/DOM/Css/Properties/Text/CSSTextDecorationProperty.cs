namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    sealed class CSSTextDecorationProperty : CSSProperty, ICssTextDecorationProperty
    {
        #region Fields

        TextDecorationStyle _style;
        List<TextDecorationLine> _line;
        Color _color;

        #endregion

        #region ctor

        internal CSSTextDecorationProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.TextDecoration, rule, PropertyFlags.Animatable | PropertyFlags.Shorthand)
        {
            _line = new List<TextDecorationLine>();
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the decoration style property.
        /// </summary>
        public TextDecorationStyle DecorationStyle
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the value of the line property.
        /// </summary>
        public IEnumerable<TextDecorationLine> Line
        {
            get { return _line; }
        }

        /// <summary>
        /// Gets the value of the color property.
        /// </summary>
        public Color Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _style = TextDecorationStyle.Solid;
            _color = Color.Black;
            _line.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var list = value as CSSValueList;

            if (list == null)
                list = new CSSValueList(value);

            TextDecorationStyle? style = null;
            Color? color = null;
            var line = new List<TextDecorationLine>();

            foreach (var item in list)
            {
                if (color == null && (color = item.ToColor()).HasValue)
                    continue;
                else if (style == null && (style = item.ToDecorationStyle()).HasValue)
                    continue;

                var element = item.ToDecorationLine();

                if (!element.HasValue)
                    return false;

                line.Add(element.Value);
            }

            _style = style ?? TextDecorationStyle.Solid;
            _line = line;
            _color = color ?? Color.Black;
            return true;
        }

        /// <summary>
        /// Instead of specifying individual longhand properties, a
        /// keyword can be used to represent a specific system font.
        /// </summary>
        /// <param name="setting">The setting to apply.</param>
        void SetTo(SystemSetting setting)
        {
            //TODO set properties to the setting given by the enumeration value
        }

        #endregion

        #region Predefined settings

        enum SystemSetting
        {
            /// <summary>
            /// The font used for captioned controls (e.g., buttons, drop-downs, etc.).
            /// </summary>
            Caption,
            /// <summary>
            /// The font used to label icons.
            /// </summary>
            Icon,
            /// <summary>
            /// The font used in menus (e.g., dropdown menus and menu lists).
            /// </summary>
            Menu,
            /// <summary>
            /// The font used in dialog boxes.
            /// </summary>
            MessageBox,
            /// <summary>
            /// The font used for labeling small controls.
            /// </summary>
            SmallCaption,
            /// <summary>
            /// The font used in window status bars.
            /// </summary>
            StatusBar
        }

        #endregion
    }
}
