namespace AngleSharp.Css
{
    using AngleSharp.Dom.Css;
    using System;

    /// <summary>
    /// A computed style related to a render device.
    /// </summary>
    public class RenderStyle
    {
        /// <summary>
        /// Gets or sets the height of the style.
        /// </summary>
        public Int32 Height
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the style.
        /// </summary>
        public Int32 Width
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        public Color ForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the back color.
        /// </summary>
        public Color BackColor
        {
            get;
            set;
        }
    }
}
