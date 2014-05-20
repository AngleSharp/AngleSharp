namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration
    /// </summary>
    public sealed class CSSTextDecorationProperty : CSSProperty
    {
        #region Fields

        CSSTextDecorationStyleProperty _style;
        CSSTextDecorationLineProperty _line;
        CSSTextDecorationColorProperty _color;

        #endregion

        #region ctor

        internal CSSTextDecorationProperty()
            : base(PropertyNames.TextDecoration)
        {
            _style = new CSSTextDecorationStyleProperty();
            _line = new CSSTextDecorationLineProperty();
            _color = new CSSTextDecorationColorProperty();
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the decoration style property.
        /// </summary>
        public CSSTextDecorationStyleProperty Style
        {
            get { return _style; }
        }

        /// <summary>
        /// Gets the value of the line property.
        /// </summary>
        public CSSTextDecorationLineProperty Line
        {
            get { return _line; }
        }

        /// <summary>
        /// Gets the value of the color property.
        /// </summary>
        public CSSTextDecorationColorProperty Color
        {
            get { return _color; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value == CSSValue.Inherit)
                return true;

            var list = value as CSSValueList;

            if (list == null)
                list = new CSSValueList(value);

            var index = 0;
            var startGroup = new List<CSSProperty>(3);
            var style = new CSSTextDecorationStyleProperty();
            var line = new CSSTextDecorationLineProperty();
            var color = new CSSTextDecorationColorProperty();
            startGroup.Add(style);
            startGroup.Add(line);
            startGroup.Add(color);

            while (true)
            {
                var length = startGroup.Count;

                for (int i = 0; i < length; i++)
                {
                    if (CheckSingleProperty(startGroup[i], index, list))
                    {
                        startGroup.RemoveAt(i);
                        index++;
                        break;
                    }
                }

                if (length == startGroup.Count)
                    break;
            }

            if (index == list.Length)
            {
                _style = style;
                _line = line;
                _color = color;
                return true;
            }

            return false;
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
