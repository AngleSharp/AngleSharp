using AngleSharp;
using AngleSharp.DOM.Html;
using AngleSharp.Scripting;
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
            SimpleScriptingSample();
            ExtendedScriptingSample();
            EventScriptingExample();
            EventLegacyScriptingExample();
        }

        static void FirstExample() 
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

        static void UsingLinq()
        {
            //Create a new document from the given source
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
            //Create a new document from the given source
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
            //Create empty document
            var document = DocumentBuilder.Html(String.Empty);

            //Set title
            document.Title = "My document";

            //Get the body
            var body = document.Body;

            //Create elements using generics, set properties
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

            //Output the resulting DOM
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }

        static void SimpleScriptingSample()
        {
            //We require a custom configuration
            var config = new Configuration();

            //Including a script engine
            config.Register(new JavaScriptEngine());

            //And enabling scripting
            config.IsScripting = true;

            //This is our sample source, we will set the title and write on the document
            var source = @"<!doctype html>
<html>
<head><title>Sample</title></head>
<body>
<script>
document.title = 'Simple manipulation...';
document.write('<span class=greeting>Hello World!</span>');
</script>
</body>";
            var document = DocumentBuilder.Html(source, config);

            //Modified HTML will be output
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }

        static void ExtendedScriptingSample()
        {
            //We require a custom configuration
            var config = new Configuration();

            //Including a script engine
            config.Register(new JavaScriptEngine());

            //And enabling scripting + styling (should be enabled anyway)
            config.IsScripting = true;
            config.IsStyling = true;

            //This is our sample source, we will do some DOM manipulation
            var source = @"<!doctype html>
<html>
<head><title>Sample</title></head>
<style>
.bold {
    font-weight: bold;
}
.italic {
    font-style: italic;
}
span {
    font-size: 12pt;
}
div {
    background: #777;
    color: #f3f3f3;
}
</style>
<body>
<div id=content></div>
<script>
(function() {
    var doc = document;
    var content = doc.querySelector('#content');
    var span = doc.createElement('span');
    span.id = 'myspan';
    span.classList.add('bold', 'italic');
    span.textContent = 'Some sample text';
    content.appendChild(span);
    var script = doc.querySelector('script');
    script.parentNode.removeChild(script);
})();
</script>
</body>";
            var document = DocumentBuilder.Html(source, config);

            //HTML will have changed completely (e.g., no more script element)
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }

        static void EventScriptingExample()
        {
            //We require a custom configuration
            var config = new Configuration();

            //Including a script engine
            config.Register(new JavaScriptEngine());

            //And enabling scripting
            config.IsScripting = true;

            //This is our sample source, we will trigger the load event
            var source = @"<!doctype html>
<html>
<head><title>Event sample</title></head>
<body>
<script>
console.log('Before setting the handler!');

document.addEventListener('load', function() {
    console.log('Document loaded!');
});

document.addEventListener('hello', function() {
    console.log('hello world from JavaScript!');
});

console.log('After setting the handler!');
</script>
</body>";
            var document = DocumentBuilder.Html(source, config);

            //HTML should be output in the end
            Console.WriteLine(document.DocumentElement.OuterHtml);

            //Register Hello event listener from C# (we also have one in JS)
            document.AddEventListener("hello", (s, ev) =>
            {
                Console.WriteLine("hello world from C#!");
            });

            var e = document.CreateEvent("event");
            e.Init("hello", false, false);
            document.Dispatch(e);
        }

        static void EventLegacyScriptingExample()
        {
            //We require a custom configuration
            var config = new Configuration();

            //Including a script engine
            config.Register(new JavaScriptEngine());

            //And enabling scripting
            config.IsScripting = true;

            //This is our sample source, we will trigger the load event
            var source = @"<!doctype html>
<html>
<head><title>Event sample</title></head>
<body>
<script>
console.log('Before setting the handler via onload!');

document.onload = function() {
    console.log('Document loaded (legacy way)!');
};

console.log('After setting the handler via onload!');
</script>
</body>";
            var document = DocumentBuilder.Html(source, config);

            //HTML should be output in the end
            Console.WriteLine(document.DocumentElement.OuterHtml);
        }
    }
}
