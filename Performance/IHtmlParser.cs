using System;

namespace Performance
{
    interface IHtmlParser
    {
        String Name { get; }

        void Parse(String source);
    }
}
