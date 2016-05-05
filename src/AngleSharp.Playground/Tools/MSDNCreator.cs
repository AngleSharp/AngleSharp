namespace AngleSharp.Playground.Tools
{
    /// <summary>
    /// Creates tests based on the official tests
    /// specified at the MSDN IE testcenter page:
    /// http://samples.msdn.microsoft.com/ietestcenter
    /// </summary>
    class MSDNCreator
    {
        /*
         *  JavaScript snippet to read out / generate properties from the MSDN
         *  (in order to generate DOM properties)
         *  
         *  Example usage for http://msdn.microsoft.com/en-us/library/windows/apps/hh702466.aspx
         *  --------------------------------------------------------------
            var content = [];

            $('#mytable > tbody > tr').each(function(i, v) {
                var cells = $('td', this);

                if(cells.length === 0)
                    return;

                var title = cells[0].textContent;
                var description = cells[1].textContent;
                content.push({
                    title : title,
                    description : description
                });
            });

            var blocks = [];

            $.each(content, function() {
                var name = this.title.charAt(0).toUpperCase() + this.title.slice(1);
                var css = this.title.replace(/([a-z])([A-Z])/g, '$1-$2').toLowerCase();
                blocks.push([
                    '/// <summary>',
                    '/// ' + this.description,
                    '/// </summary>',
                    [DOM("' + this.title + '")]',
                    'public String ' + name,
                    '{',
                    '   get { return GetPropertyValue("' + css + '") ?? String.Empty; }',
                    '   set { SetProperty("' + css + '", value); }',
                    '}'
                ].join('\n');
            });

            var ph = $('<pre />').appendTo('body');
            ph.textContent = blocks.join('\n');
         *  --------------------------------------------------------------
         * 
         * 
         */
    }
}
