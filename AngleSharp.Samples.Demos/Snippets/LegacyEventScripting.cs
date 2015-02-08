namespace AngleSharp.Samples.Demos.Snippets
{
    using AngleSharp.Scripting.JavaScript;
    using System;
    using System.Threading.Tasks;

    class LegacyEventScripting : ISnippet
    {
#pragma warning disable CS1998
        public async Task Run()
#pragma warning restore CS1998
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
    }
}
