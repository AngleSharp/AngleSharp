namespace AngleSharp.Network.RequestProcessors
{
    using System.Threading.Tasks;

    interface IRequestProcessor
    {
        Task Process(ResourceRequest request);
    }
}
