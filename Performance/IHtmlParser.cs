namespace Performance
{
    using System;

    interface IHtmlParser
    {
        String Name { get; }

        void Parse(String source);
    }
}
