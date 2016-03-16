namespace AngleSharp.Network.RequestProcessors
{
    using System.Threading.Tasks;

    interface IRequestProcessor
    {
        IDownload Download { get; }

        Task Process(ResourceRequest request);
    }
}
