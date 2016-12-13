namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Media;
    using System;

    class MockImageInfo : IImageInfo
    {
        public Int32 Width
        {
            get { return 0; }
        }

        public Int32 Height
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
