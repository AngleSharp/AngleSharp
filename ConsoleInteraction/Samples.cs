using System;
using AngleSharp;

namespace ConsoleInteraction
{
    class Samples
    {
        public static void RunAll()
        {
            First();
        }

        static void First() 
        {
            var document = DocumentBuilder.Html("<h1>Some example source</h1><p>This is a paragraph element");
            //Do something with document like the following

            Console.WriteLine("Serializing the (original) document:");
            Console.WriteLine(document.ToHtml());

            var p = document.CreateElement("p");
            p.TextContent = "This is another paragraph.";

            Console.WriteLine("Inserting another element in the body ...");
            document.Body.AppendChild(p);

            Console.WriteLine("Serializing the document again:");
            Console.WriteLine(document.ToHtml());
        }
    }
}
