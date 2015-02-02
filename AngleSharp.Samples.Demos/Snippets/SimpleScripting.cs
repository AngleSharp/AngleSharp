namespace AngleSharp.Samples.Demos.Snippets
{
    using AngleSharp.Scripting;
    using System;
    using System.Threading.Tasks;

    class SimpleScripting : ISnippet
    {
        public async Task Run()
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
    }
}
