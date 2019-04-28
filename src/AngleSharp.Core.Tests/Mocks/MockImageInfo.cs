namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Media;
    using System;

    class MockImageInfo : IImageInfo
    {
        public Int32 Width => 0;

        public Int32 Height => 0;

        public Url Source
        {
            get;
            set;
        }
    }
}
