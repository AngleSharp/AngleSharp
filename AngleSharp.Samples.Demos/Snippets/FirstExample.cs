namespace AngleSharp.Samples.Demos.Snippets
{
    using System;
    using System.Threading.Tasks;

    class FirstExample : ISnippet
    {
        public async Task Run()
        {
            //Create a new document from the given source
            var document = DocumentBuilder.Html("<h1>Some example source</h1><p>This is a paragraph element");

            //Do something with document like the following
            Console.WriteLine("Serializing the (original) document:");
            Console.WriteLine(document.DocumentElement.OuterHtml);

            var p = document.CreateElement("p");
            p.TextContent = "This is another paragraph.";

            Console.WriteLine("Inserting another element in the body ...");
            document.Body.AppendChild(p);

            Console.WriteLine("Serializing the document again:");
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
