namespace AngleSharp.Samples.Demos.Snippets
{
    using AngleSharp.Scripting.JavaScript;
    using System;
    using System.Threading.Tasks;

    class ExtendedScripting : ISnippet
    {
#pragma warning disable CS1998
        public async Task Run()
#pragma warning restore CS1998
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
    }
}
