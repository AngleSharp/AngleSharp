namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Represents the renderers setting.
    /// </summary>
    public class RenderDevice
    {
        /// <summary>
        /// Creates a new render device with the given device width and height.
        /// These values are then also the initial values of the viewport.
        /// </summary>
        /// <param name="width">The width of the device.</param>
        /// <param name="height">The height of the device.</param>
        public RenderDevice(Int32 width, Int32 height)
        {
            DeviceWidth = width;
            DeviceHeight = height;
            ViewPortWidth = width;
            ViewPortHeight = height;
            ColorBits = 32;
            MonochromeBits = 0;
            Resolution = 96;
            DeviceType = Kind.Screen;
            IsInterlaced = false;
            IsGrid = false;
            Frequency = 60;
        }

        /// <summary>
        /// Gets or sets the options of the viewport.
        /// </summary>
        public IConfiguration Options
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the width of the viewport in pixels.
        /// </summary>
        public Int32 ViewPortWidth
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the height of the viewport in pixels.
        /// </summary>
        public Int32 ViewPortHeight
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the output is interlaced.
        /// </summary>
        public Boolean IsInterlaced
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the output is not a bitmap but a grid.
        /// </summary>
        public Boolean IsGrid
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the width of the device in pixels.
        /// </summary>
        public Int32 DeviceWidth
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the height of the device in pixels.
        /// </summary>
        public Int32 DeviceHeight
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the pixel density of the device in dpi.
        /// </summary>
        public Int32 Resolution
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the update frequency of the device in frames / s.
        /// </summary>
        public Int32 Frequency
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of color bits of the device, e.g. 32.
        /// </summary>
        public Int32 ColorBits
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the number of monochrome bits of the device, e.g. 0
        /// if the device is color.
        /// </summary>
        public Int32 MonochromeBits
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the type of the device.
        /// </summary>
        public Kind DeviceType
        {
            get;
            set;
        }

        /// <summary>
        /// All possible device kinds.
        /// </summary>
        public enum Kind
        {
            /// <summary>
            /// A screen device. Default.
            /// </summary>
            Screen,
            /// <summary>
            /// A printing device.
            /// </summary>
            Printer,
            /// <summary>
            /// A device for speech output.
            /// </summary>
            Speech,
            /// <summary>
            /// Some other device.
            /// </summary>
            Other
        }
    }
}
