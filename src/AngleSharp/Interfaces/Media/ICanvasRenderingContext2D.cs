namespace AngleSharp.Dom.Media
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Html;
    using System;

    /// <summary>
    /// Represents the canvas rendering context.
    /// More information is available at the WHATWG homepage:
    /// http://www.whatwg.org/specs/web-apps/current-work/multipage/scripting.html#canvasrenderingcontext2d
    /// </summary>
    [DomName("CanvasRenderingContext2D")]
    public interface ICanvasRenderingContext2D : IRenderingContext
    {
        /// <summary>
        /// Gets the associated canvas element.
        /// </summary>
        [DomName("canvas")]
        IHtmlCanvasElement Canvas { get; }

        /// <summary>
        /// Gets or sets the width of the canvas.
        /// </summary>
        [DomName("width")]
        Int32 Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the canvas.
        /// </summary>
        [DomName("height")]
        Int32 Height { get; set; }

        /// <summary>
        /// Push state on state stack.
        /// </summary>
        [DomName("save")]
        void SaveState();

        /// <summary>
        /// Pop state stack and restore state.
        /// </summary>
        [DomName("restore")]
        void RestoreState();
    }
}
