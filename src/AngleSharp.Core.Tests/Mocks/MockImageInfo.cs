namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Dom;
    using AngleSharp.Media;
    using System;

    class MockImageInfo : IImageInfo
    {
        public Int32 Width { get; set; }

        public Int32 Height { get; set; }

        public Url Source
        {
            get;
            set;
        }
    }
}
