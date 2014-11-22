namespace ConsoleInteraction
{
    using AngleSharp;
    using AngleSharp.DOM.Html;
    using AngleSharp.Scripting;
    using AngleSharp.Linq;
    using System;
    using System.Linq;

    class Samples
    {
        public static void RunAll()
        {
            ErrorHandling();
            FirstExample();
            UsingLinq();
            SingleElements();
            Construction();
            SimpleScriptingSample();
            ExtendedScriptingSample();
            CustomEventScriptingExample();
            LegacyEventScriptingExample();
            GatherDataFromRssFeed();
        }

        static void ErrorHandling()
        {
            //The original source, see
            //http://www.google.com/error
            var source = @"
<!DOCTYPE html>
<html lang=en>
  <meta charset=utf-8>
  <meta name=viewport content=""initial-scale=1, minimum-scale=1, width=device-width"">
  <title>Error 404 (Not Found)!!1</title>
  <style>
    *{margin:0;padding:0}html,code{font:15px/22px arial,sans-serif}html{background:#fff;color:#222;padding:15px}body{margin:7% auto 0;max-width:390px;min-height:180px;padding:30px 0 15px}* > body{background:url(//www.google.com/images/errors/robot.png) 100% 5px no-repeat;padding-right:205px}p{margin:11px 0 22px;overflow:hidden}ins{color:#777;text-decoration:none}a img{border:0}@media screen and (max-width:772px){body{background:none;margin-top:0;max-width:none;padding-right:0}}#logo{background:url(//www.google.com/images/errors/logo_sm_2.png) no-repeat}@media only screen and (min-resolution:192dpi){#logo{background:url(//www.google.com/images/errors/logo_sm_2_hr.png) no-repeat 0% 0%/100% 100%;-moz-border-image:url(//www.google.com/images/errors/logo_sm_2_hr.png) 0}}@media only screen and (-webkit-min-device-pixel-ratio:2){#logo{background:url(//www.google.com/images/errors/logo_sm_2_hr.png) no-repeat;-webkit-background-size:100% 100%}}#logo{display:inline-block;height:55px;width:150px}
  </style>
  <a href=//www.google.com/><span id=logo aria-label=Google></span></a>
  <p><b>404.</b> <ins>That’s an error.</ins>
  <p>The requested URL <code>/error</code> was not found on this server.  <ins>That’s all we know.</ins>";

            //Just get the DOM representation
            var doc = DocumentBuilder.Html(source);

            //Serialize it back to the console
            Console.WriteLine(doc.DocumentElement.OuterHtml);
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
                Console.WriteLine(item.Text());

            Console.WriteLine();
            Console.WriteLine("CSS:");

            foreach (var item in blueListItemsLinq)
                Console.WriteLine(item.Text());
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
            Console.WriteLine(emphasize.Text());   //boldanditalic

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

        static void CustomEventScriptingExample()
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
<head><title>Custom Event sample</title></head>
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

        static void Html5Test()
        {
            //We require a custom configuration
            var config = new Configuration();

            //Including a script engine
            config.Register(new JavaScriptEngine());

            //And enabling scripting + styling (should be enabled anyway)
            config.IsScripting = true;
            config.IsStyling = true;

            var document = DocumentBuilder.Html(new Uri("http://html5test.com/"), config);
            var points = document.QuerySelector("#score > .pointsPanel > h2 > strong").TextContent;
            Console.WriteLine("AngleSharp received {0} points form HTML5Test.com", points);
        }

        static void LegacyEventScriptingExample()
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
<head><title>Legacy event sample</title></head>
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

        static void GatherDataFromRssFeed()
        {
            var feed = DocumentBuilder.Html(new Uri("http://www.florian-rappl.de/RSS"));
            var items = feed.QuerySelectorAll("item").Select(m => new 
            { 
                Updated = DateTime.Parse(m.GetElementsByTagName("a10:updated").First().TextContent),
                Title = m.QuerySelector("title").TextContent
            });

            Console.WriteLine("Available titles:");

            foreach (var item in items)
                Console.WriteLine("- {0} ({1})", item.Title, item.Updated);
        }
    }
}
