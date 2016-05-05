namespace AngleSharp.Services.Media
{
    /// <summary>
    /// Specifies general resource information.
    /// </summary>
    public interface IResourceInfo
    {
        /// <summary>
        /// Gets the source of the resource.
        /// </summary>
        Url Source { get; set; }
    }
}
