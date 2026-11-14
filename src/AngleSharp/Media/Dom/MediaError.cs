namespace AngleSharp.Media.Dom
{
    /// <summary>
    /// The error of a media element that failed to load its resource.
    /// </summary>
    sealed class MediaError : IMediaError
    {
        public MediaError(MediaErrorCode code)
        {
            Code = code;
        }

        public MediaErrorCode Code { get; }
    }
}
