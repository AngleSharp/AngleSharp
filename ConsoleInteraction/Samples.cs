using AngleSharp;
using AngleSharp.DOM.Html;
using System;
using System.Linq;

namespace ConsoleInteraction
{
    class Samples
    {
        public static void RunAll()
        {
            FirstExample();
            UsingLinq();
            SingleElements();
            Construction();
        }

        static void FirstExample() 
        {
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

        static void UsingLinq()
        {
            var document = DocumentBuilder.Html("<ul><li>First item<li>Second item<li class='blue'>Third item!<li class='blue red'>Last item!</ul>");

            //Do something with LINQ
            var blueListItemsLinq = document.All.Where(m => m.TagName == "li" && m.ClassList.Contains("blue"));

            //Or directly with CSS selectors
            var blueListItemsSelector = document.QuerySelectorAll("li.blue");

            Console.WriteLine("Comparing both ways ...");

            Console.WriteLine();
            Console.WriteLine("LINQ:");

            foreach (var item in blueListItemsLinq)
                Console.WriteLine(item.ToText());

            Console.WriteLine();
            Console.WriteLine("CSS:");

            foreach (var item in blueListItemsLinq)
                Console.WriteLine(item.ToText());
        }

        static void SingleElements()
        {
            var document = DocumentBuilder.Html("<b><i>This is some <em> bold <u>and</u> italic </em> text!</i></b>");
            var emphasize = document.QuerySelector("em");

            Console.WriteLine("Difference between several ways of getting text:");
            Console.WriteLine();
            Console.WriteLine("Only from C# / AngleSharp:");
            Console.WriteLine();
            Console.WriteLine(emphasize.ToHtml());   //<em> bold <u>and</u> italic </em>
            Console.WriteLine(emphasize.ToText());   //boldanditalic

            Console.WriteLine();
            Console.WriteLine("From the DOM:");
            Console.WriteLine();
            Console.WriteLine(emphasize.InnerHtml);  // bold <u>and</u> italic
            Console.WriteLine(emphasize.OuterHtml);  //<em> bold <u>and</u> italic </em>
            Console.WriteLine(emphasize.TextContent);// bold and italic 
        }

        static void Construction()
        {
            var document = DocumentBuilder.Html(String.Empty);
            document.Title = "My document";
            var body = document.Body;
            var p1 = document.CreateElement<IHtmlParagraphElement>();
            p1.TextContent = "First paragraph";
            body.AppendChild(p1);
            var p2 = document.CreateElement<IHtmlParagraphElement>();
            p2.TextContent = "Second paragraph";
            body.AppendChild(p2);
            var a = document.CreateElement<IHtmlAnchorElement>();
            a.TextContent = "Some hyperlink";
            a.Href = "http://www.myurl";
            body.AppendChild(a);
            var img = document.CreateElement<IHtmlImageElement>();
            img.Source = "http://www.example.com/link";
            body.AppendChild(img);
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
