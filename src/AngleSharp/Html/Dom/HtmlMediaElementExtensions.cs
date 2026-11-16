namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using System.Threading.Tasks;

    /// <summary>
    /// Members of media elements beyond their interface.
    /// </summary>
    [DomExposed("HTMLMediaElement")]
    public static class HtmlMediaElementExtensions
    {
        /// <summary>
        /// Plays the media, like <see cref="AngleSharp.Media.Dom.IMediaController.Play"/>, and
        /// gives a task that completes once it plays. It fails with an AbortError if the media
        /// is paused (or loads another resource) before, with a NotSupportedError if its
        /// resource can't be played, and with what the controller of the resource throws
        /// (e.g. a NotAllowedError of an autoplay policy).
        /// </summary>
        /// <param name="element">The element to play.</param>
        /// <returns>The task completing once the media plays.</returns>
        [DomName("play")]
        public static Task PlayAsync(this IHtmlMediaElement element)
        {
            if (element is IPlayableMediaElement playable)
            {
                return playable.PlayAsync();
            }

            element.Play();
            return Task.CompletedTask;
        }
    }

    /// <summary>
    /// The media elements of the library, which know when they play.
    /// </summary>
    interface IPlayableMediaElement
    {
        Task PlayAsync();
    }
}
