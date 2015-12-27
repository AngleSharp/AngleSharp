namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Services.Media;

    class MockImageInfo : IImageInfo
    {
        public int Width
        {
            get { return 0; }
        }

        public int Height
        {
            get { return 0; }
        }

        public Url Source
        {
            get;
            set;
        }
    }
}
