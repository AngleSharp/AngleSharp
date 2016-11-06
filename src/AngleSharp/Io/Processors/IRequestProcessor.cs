namespace AngleSharp.Io.Processors
{
    using System.Threading.Tasks;

    interface IRequestProcessor
    {
        IDownload Download { get; }

        Task ProcessAsync(ResourceRequest request);
    }
}
