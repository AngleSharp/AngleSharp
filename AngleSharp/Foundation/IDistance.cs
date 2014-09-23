namespace AngleSharp
{
    using System;

    /// <summary>
    /// Functionality for length computation.
    /// </summary>
    public interface IDistance
    {
        String ToCss();

        Single ToPixel();
    }
}
