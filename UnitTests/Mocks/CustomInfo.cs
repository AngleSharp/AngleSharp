namespace UnitTests.Mocks
{
    using AngleSharp.Network;
    using System;

    class CustomInfo : IInfo
    {
        public String Version
        {
            get;
            set;
        }

        public String Agent
        {
            get;
            set;
        }
    }
}
