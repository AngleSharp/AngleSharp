namespace AngleSharp.Dom.Events
{
    using System;

    /// <summary>
    /// All possible mouse button values that are currently pressed.
    /// </summary>
    [Flags]
    public enum MouseButtons : ushort
    {
        /// <summary>
        /// No buttons currently pressed.
        /// </summary>
        None = 0,
        /// <summary>
        /// The primary button of the device. In general, the left button or the only button on single-button
        /// devices, used to activate a user interface control or select text.
        /// </summary>
        Primary = 0,
        /// <summary>
        /// The secondary button. In general, the right button, often used to display a context menu.
        /// </summary>
        Secondary = 2,
        /// <summary>
        /// The auxiliary button. In general, the middle button, often combined with a mouse wheel.
        /// </summary>
        Auxiliary = 4
    }
}
