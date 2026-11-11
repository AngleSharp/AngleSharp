namespace AngleSharp.Benchmarks;

using System;
using System.Text;

/// <summary>
/// Builds a deterministic, synthetic page that exercises most of the HTML parser at once: every
/// tokenizer content model (data, RCDATA, RAWTEXT, script data, comments, doctype), quoted,
/// unquoted and duplicate attributes, character references, upper-case and custom tags, tables
/// with implied and foster-parented content, forms, misnested formatting (adoption agency),
/// implied end tags, templates and inline SVG / MathML.
/// </summary>
/// <remarks>
/// The proportions follow what a content-heavy real page looks like - mostly short text runs
/// between attribute-carrying tags, a few large script and style blocks - so a change that helps
/// here should help on <see cref="ParserBenchmark"/> too, without needing the network.
/// </remarks>
public static class ComplexDocument
{
    private static readonly String[] Words =
    [
        "angle", "bracket", "parser", "document", "element", "attribute", "selector", "standard",
        "browser", "render", "layout", "network", "stream", "buffer", "token", "tree", "node",
        "the", "a", "of", "and", "to", "in", "is", "for", "with", "that", "on", "as", "it", "by",
        "performance", "specification", "construction", "über", "naïve", "café", "日本語", "中文",
        "données", "größe", "🚀", "quick", "brown", "fox", "jumps", "over", "lazy", "dog",
    ];

    private static readonly String[] Classes =
    [
        "container", "row", "col-md-6", "col-sm-12", "card", "card-body", "card-title", "btn",
        "btn-primary", "nav-item", "nav-link", "active", "d-flex", "text-muted", "mb-3", "mt-2",
        "list-group-item", "badge", "is-hidden", "js-toggle", "media", "media-body", "lead",
    ];

    private static readonly String[] Entities =
    [
        "&amp;", "&nbsp;", "&lt;", "&gt;", "&quot;", "&copy;", "&mdash;", "&#8212;", "&#x2014;",
        "&hellip;", "&euro;", "&notit;", "&amp", "&unknown;",
    ];

    /// <summary>
    /// Creates the document.
    /// </summary>
    /// <param name="sections">The number of content sections; ~14 KB of markup each.</param>
    /// <param name="seed">The seed of the generator, to vary the content.</param>
    /// <returns>The HTML source.</returns>
    public static String Create(Int32 sections = 64, Int32 seed = 20261110)
    {
        var rng = new Random(seed);
        var sb = new StringBuilder(sections * 16 * 1024);

        Head(sb, rng);
        sb.Append("<body class=\"home page-template\" data-spy='scroll' data-target=#navbar>\n");
        Navigation(sb, rng);
        sb.Append("<main id=\"content\" role=\"main\">\n");

        for (var i = 0; i < sections; i++)
        {
            sb.Append("<section id=\"section-").Append(i).Append("\" class=\"");
            ClassList(sb, rng);
            sb.Append("\" data-index=\"").Append(i).Append("\">\n");
            sb.Append("<h2 class=\"section-title\"><a href=\"#section-").Append(i).Append("\">");
            Sentence(sb, rng, 3, 7);
            sb.Append("</a></h2>\n");

            Article(sb, rng, i);
            CardGrid(sb, rng, i);
            Table(sb, rng, i);
            Form(sb, rng, i);
            Lists(sb, rng, i);
            Formatting(sb, rng);
            Foreign(sb, rng, i);
            DeepNesting(sb, rng, 24 + i % 8);
            Legacy(sb, rng, i);
            InlineScript(sb, rng, i);

            sb.Append("</section>\n");
        }

        sb.Append("</main>\n<footer class=\"site-footer\"><p>&copy; 2026 ");
        Sentence(sb, rng, 4, 8);
        sb.Append("</p></footer>\n");
        sb.Append("<script src=\"/assets/js/app.min.js\" async defer></script>\n");
        sb.Append("</body>\n</html>\n<!-- trailing comment -->");
        return sb.ToString();
    }

    private static void Head(StringBuilder sb, Random rng)
    {
        sb.Append("<!DOCTYPE html>\n<html lang=\"en\" class=\"no-js\">\n<head>\n");
        sb.Append("<meta charset=\"utf-8\">\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\n");
        sb.Append("<title>Complex &amp; synthetic benchmark document &mdash; AngleSharp</title>\n");

        for (var i = 0; i < 24; i++)
        {
            sb.Append("<meta property=\"og:").Append(Words[i]).Append("\" content=\"");
            Sentence(sb, rng, 3, 9);
            sb.Append("\">\n");
        }

        for (var i = 0; i < 8; i++)
        {
            sb.Append("<link rel=\"stylesheet\" href=\"/assets/css/bundle-").Append(i)
              .Append(".css?v=").Append(rng.Next(100000)).Append("\" media=\"all\">\n");
        }

        sb.Append("<style>\n");

        for (var i = 0; i < 400; i++)
        {
            sb.Append('.').Append(Classes[i % Classes.Length]).Append('-').Append(i)
              .Append(" > a:hover { color: #").Append(rng.Next(0xffffff).ToString("x6"))
              .Append("; margin: ").Append(rng.Next(40)).Append("px auto; content: \"<b>&amp;</b>\"; }\n");
        }

        sb.Append("</style>\n");
        sb.Append("<script type=\"application/ld+json\">{\"@context\":\"https://schema.org\",\"@type\":\"WebSite\",\"name\":\"<AngleSharp>\"}</script>\n");
        sb.Append("<noscript><link rel=\"stylesheet\" href=\"/noscript.css\"></noscript>\n");
        sb.Append("</head>\n");
    }

    private static void Navigation(StringBuilder sb, Random rng)
    {
        sb.Append("<header><nav id=\"navbar\" class=\"navbar navbar-expand-lg\" aria-label=\"Main\">\n<ul class=\"navbar-nav\">\n");

        for (var i = 0; i < 12; i++)
        {
            sb.Append("<li class=\"nav-item dropdown\"><a class=\"nav-link\" href=\"/category/").Append(i)
              .Append("/\" aria-haspopup=\"true\" aria-expanded=\"false\">");
            Sentence(sb, rng, 1, 3);
            sb.Append("</a>\n<ul class=\"dropdown-menu\">\n");

            for (var j = 0; j < 8; j++)
            {
                // Unclosed <li>, closed by the next one.
                sb.Append("<li><a class=\"dropdown-item\" href=\"/category/").Append(i).Append('/').Append(j)
                  .Append("?ref=nav&amp;pos=").Append(j).Append("\">");
                Sentence(sb, rng, 1, 4);
                sb.Append("</a>\n");
            }

            sb.Append("</ul></li>\n");
        }

        sb.Append("</ul>\n</nav></header>\n");
    }

    private static void Article(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<article class=\"post\" itemscope itemtype=\"https://schema.org/Article\">\n");
        sb.Append("<!-- article ").Append(index).Append(" -- with -- dashes -->\n");

        for (var p = 0; p < 6; p++)
        {
            // Half of the paragraphs rely on the implied </p>.
            sb.Append("<p class=\"lead\" data-p=\"").Append(p).Append("\">");

            for (var s = 0; s < 5; s++)
            {
                Sentence(sb, rng, 6, 18);

                switch (rng.Next(7))
                {
                    case 0:
                        sb.Append(" <a href=\"https://example.com/").Append(Word(rng)).Append("?q=1&amp;lang=en#frag\" title='");
                        Sentence(sb, rng, 2, 4);
                        sb.Append("' rel=\"noopener noreferrer\" target=_blank>");
                        Sentence(sb, rng, 1, 3);
                        sb.Append("</a> ");
                        break;
                    case 1:
                        sb.Append(" <strong>").Append(Word(rng)).Append("</strong> ");
                        break;
                    case 2:
                        sb.Append(" <em class=\"hl\">").Append(Word(rng)).Append("</em> ").Append(Entities[rng.Next(Entities.Length)]).Append(' ');
                        break;
                    case 3:
                        sb.Append(" <code>").Append(Word(rng)).Append("&lt;T&gt;()</code> ");
                        break;
                    case 4:
                        sb.Append(" <span class=\"").Append(Classes[rng.Next(Classes.Length)]).Append("\" data-id=").Append(rng.Next(1000)).Append('>')
                          .Append(Word(rng)).Append("</span><br>\r\n");
                        break;
                    default:
                        sb.Append(' ').Append(Entities[rng.Next(Entities.Length)]).Append(' ');
                        break;
                }
            }

            sb.Append(p % 2 == 0 ? "</p>\n" : "\n");
        }

        sb.Append("<figure><img src=\"/img/").Append(index).Append(".jpg\" srcset=\"/img/").Append(index)
          .Append("-2x.jpg 2x, /img/").Append(index).Append("-3x.jpg 3x\" alt=\"");
        Sentence(sb, rng, 3, 6);
        sb.Append("\" width=640 height=480 loading=lazy><figcaption>");
        Sentence(sb, rng, 4, 9);
        sb.Append("</figcaption></figure>\n");

        sb.Append("<pre><code class=\"language-csharp\">\nvar document = await context.OpenAsync(req =&gt; req.Content(\"&lt;p&gt;\"));\n");

        for (var i = 0; i < 8; i++)
        {
            sb.Append("    foreach (var item in document.QuerySelectorAll(\"").Append(Classes[rng.Next(Classes.Length)])
              .Append(" &gt; li\")) { Console.WriteLine(item.TextContent); }\r\n");
        }

        sb.Append("</code></pre>\n<blockquote cite=\"https://html.spec.whatwg.org/\"><p>");
        Sentence(sb, rng, 20, 40);
        sb.Append("</blockquote>\n</article>\n");
    }

    private static void CardGrid(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<div class=\"row\">\n");

        for (var i = 0; i < 12; i++)
        {
            sb.Append("<div class=\"");
            ClassList(sb, rng);
            sb.Append("\" id=\"card-").Append(index).Append('-').Append(i)
              .Append("\" data-price=\"").Append(rng.Next(100)).Append('.').Append(rng.Next(100)).Append("\" data-sku=\"SKU-").Append(rng.Next(1000000))
              .Append("\" style=\"background-image:url('/img/c").Append(i).Append(".png');order:").Append(i).Append("\" hidden>")
              .Append("<div class=\"card-body\"><h5 class=\"card-title\">");
            Sentence(sb, rng, 2, 5);
            sb.Append("</h5><p class=\"card-text text-muted\">");
            Sentence(sb, rng, 8, 16);
            sb.Append("</p><a href=\"/product/").Append(rng.Next(100000)).Append("\" class=\"btn btn-primary\" role=\"button\">")
              .Append(Word(rng)).Append("</a> <button type=\"button\" class=\"btn js-toggle\" onclick=\"toggle(this, ").Append(i)
              .Append(")\" disabled>").Append(Word(rng)).Append("</button></div></div>\n");
        }

        sb.Append("</div>\n");
    }

    private static void Table(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<table class=\"table table-striped\" id=\"table-").Append(index).Append("\">\n");
        sb.Append("<caption>");
        Sentence(sb, rng, 3, 6);
        sb.Append("</caption>\n<colgroup><col span=\"2\"><col class=\"num\"></colgroup>\n<thead><tr>");

        for (var c = 0; c < 8; c++)
        {
            sb.Append("<th scope=\"col\">").Append(Word(rng)).Append("</th>");
        }

        sb.Append("</tr></thead>\n");

        // No <tbody>, no </tr>, no </td>: all implied.
        for (var r = 0; r < 24; r++)
        {
            sb.Append("<tr class=\"").Append(r % 2 == 0 ? "even" : "odd").Append("\" data-row=").Append(r).Append('>');

            for (var c = 0; c < 8; c++)
            {
                if (c == 0)
                {
                    sb.Append("<th scope=\"row\">").Append(r);
                }
                else if (c == 5 && r % 6 == 0)
                {
                    sb.Append("<td colspan=\"2\" class=\"num\">").Append(rng.Next(100000));
                    c++;
                }
                else
                {
                    sb.Append("<td>").Append(Word(rng)).Append(' ').Append(rng.Next(1000));
                }
            }

            sb.Append('\n');
        }

        // Foster-parented text and element inside the table.
        sb.Append("stray text <span>fostered</span>\n");
        sb.Append("<tfoot><tr><td colspan=\"8\">").Append(Entities[rng.Next(Entities.Length)]).Append("</td></tr></tfoot>\n</table>\n");
    }

    private static void Form(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<form action=\"/submit?section=").Append(index).Append("\" method=\"post\" class=\"form-inline\" novalidate>\n<fieldset><legend>");
        Sentence(sb, rng, 2, 4);
        sb.Append("</legend>\n");

        for (var i = 0; i < 6; i++)
        {
            sb.Append("<div class=\"form-group mb-3\"><label for=\"f").Append(index).Append('-').Append(i).Append("\">").Append(Word(rng))
              .Append("</label><input type=\"").Append(i % 3 == 0 ? "text" : i % 3 == 1 ? "email" : "checkbox")
              .Append("\" class=\"form-control\" id=\"f").Append(index).Append('-').Append(i)
              .Append("\" name=\"field[").Append(i).Append("]\" value=\"").Append(Word(rng))
              .Append(" &quot;quoted&quot;\" placeholder=\"");
            Sentence(sb, rng, 2, 4);
            sb.Append("\" required autocomplete=off /></div>\n");
        }

        sb.Append("<select name=\"choice\" class=\"custom-select\">\n<optgroup label=\"Group\">\n");

        for (var i = 0; i < 20; i++)
        {
            // Unclosed <option>.
            sb.Append("<option value=\"").Append(i).Append('"').Append(i == 3 ? " selected" : "").Append('>');
            Sentence(sb, rng, 1, 3);
            sb.Append('\n');
        }

        sb.Append("</optgroup></select>\n<textarea name=\"comment\" rows=\"4\" cols=\"40\">\n");
        Sentence(sb, rng, 10, 25);
        sb.Append(" <b>not a tag</b> &amp; </textare still text\n</textarea>\n");
        sb.Append("<button type=\"submit\" class=\"btn btn-primary\">Submit</button>\n</fieldset></form>\n");
    }

    private static void Lists(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<ul class=\"list-group\">\n");

        for (var i = 0; i < 10; i++)
        {
            sb.Append("<li class=\"list-group-item\">");
            Sentence(sb, rng, 3, 8);

            if (i % 3 == 0)
            {
                sb.Append("<ol start=\"").Append(i).Append("\" reversed>");

                for (var j = 0; j < 5; j++)
                {
                    sb.Append("<li value=").Append(j).Append('>');
                    Sentence(sb, rng, 2, 5);
                }

                sb.Append("</ol>");
            }

            sb.Append('\n');
        }

        sb.Append("</ul>\n<dl>");

        for (var i = 0; i < 8; i++)
        {
            sb.Append("<dt>").Append(Word(rng)).Append("<dd>");
            Sentence(sb, rng, 4, 10);
        }

        sb.Append("</dl>\n<details").Append(index % 2 == 0 ? " open" : "").Append("><summary>");
        Sentence(sb, rng, 2, 4);
        sb.Append("</summary>");
        Sentence(sb, rng, 10, 20);
        sb.Append("</details>\n");
    }

    private static void Formatting(StringBuilder sb, Random rng)
    {
        // Misnested formatting elements - adoption agency and active formatting reconstruction.
        for (var i = 0; i < 6; i++)
        {
            sb.Append("<p><b>");
            Sentence(sb, rng, 2, 4);
            sb.Append("<i>");
            Sentence(sb, rng, 2, 4);
            sb.Append("</b>");
            Sentence(sb, rng, 2, 4);
            sb.Append("</i> <u><s>").Append(Word(rng)).Append("</u></s> <a href=\"#a\">").Append(Word(rng))
              .Append("<a href=\"#b\">").Append(Word(rng)).Append("</a>\n<div><font color=red size=+1>");
            Sentence(sb, rng, 2, 4);
            sb.Append("</div><p>");
            Sentence(sb, rng, 2, 4);
            sb.Append("</font></p>\n");
        }

        sb.Append("<nobr>").Append(Word(rng)).Append("<nobr>").Append(Word(rng)).Append("</nobr>\n");
    }

    private static void Foreign(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<svg xmlns=\"http://www.w3.org/2000/svg\" xmlns:xlink=\"http://www.w3.org/1999/xlink\" viewBox=\"0 0 100 100\" width=\"100\" height=\"100\" preserveAspectRatio=\"xMidYMid meet\">\n");
        sb.Append("<defs><linearGradient id=\"g").Append(index).Append("\" gradientUnits=\"userSpaceOnUse\"><stop offset=\"0\" stop-color=\"#fff\"/><stop offset=\"1\" stop-color=\"#000\"/></linearGradient><clipPath id=\"c").Append(index).Append("\"><rect width=\"50\" height=\"50\"/></clipPath></defs>\n");

        for (var i = 0; i < 10; i++)
        {
            sb.Append("<g transform=\"translate(").Append(rng.Next(100)).Append(',').Append(rng.Next(100)).Append(")\"><path d=\"M");

            for (var j = 0; j < 12; j++)
            {
                sb.Append(rng.Next(100)).Append(' ').Append(rng.Next(100)).Append(" L");
            }

            sb.Append("0 0Z\" fill=\"url(#g").Append(index).Append(")\" stroke-width=\"").Append(rng.Next(5))
              .Append("\"/><circle cx=\"").Append(rng.Next(100)).Append("\" cy=\"").Append(rng.Next(100))
              .Append("\" r=\"4\"/><use xlink:href=\"#c").Append(index).Append("\"/></g>\n");
        }

        sb.Append("<foreignObject width=\"100\" height=\"50\"><div xmlns=\"http://www.w3.org/1999/xhtml\"><p>");
        Sentence(sb, rng, 3, 6);
        sb.Append("</p></div></foreignObject>\n<text x=\"0\" y=\"15\"><![CDATA[x < y && y > z]]>").Append(Word(rng)).Append("</text>\n</svg>\n");

        sb.Append("<math xmlns=\"http://www.w3.org/1998/Math/MathML\" display=\"block\"><mrow><mi>x</mi><mo>=</mo><mfrac><mrow><mo>&minus;</mo><mi>b</mi><mo>&PlusMinus;</mo><msqrt><msup><mi>b</mi><mn>2</mn></msup><mo>&minus;</mo><mn>4</mn><mi>a</mi><mi>c</mi></msqrt></mrow><mrow><mn>2</mn><mi>a</mi></mrow></mfrac></mrow><annotation-xml encoding=\"text/html\"><span definitionURL=\"x\">")
          .Append(Word(rng)).Append("</span></annotation-xml></math>\n");
    }

    private static void DeepNesting(StringBuilder sb, Random rng, Int32 depth)
    {
        for (var i = 0; i < depth; i++)
        {
            sb.Append(i % 3 == 0 ? "<div class=\"level-" : i % 3 == 1 ? "<section class=\"level-" : "<span class=\"level-").Append(i).Append("\">");
        }

        Sentence(sb, rng, 3, 6);

        for (var i = depth - 1; i >= 0; i--)
        {
            sb.Append(i % 3 == 0 ? "</div>" : i % 3 == 1 ? "</section>" : "</span>");
        }

        sb.Append('\n');
    }

    private static void Legacy(StringBuilder sb, Random rng, Int32 index)
    {
        // Upper-case markup, duplicate attributes, custom elements, templates, void elements.
        sb.Append("<DIV CLASS=\"Legacy\" ID=legacy-").Append(index).Append(" class=\"duplicate\" ALIGN=center>\r\n<CENTER><FONT FACE=\"Arial\">");
        Sentence(sb, rng, 3, 6);
        sb.Append("</FONT></CENTER><HR><BR/><IMG SRC=x.gif ALT=''>\r\n</DIV>\n");
        sb.Append("<my-widget data-config='{\"a\":1,\"b\":[1,2,3]}' :bound=\"value\" @click=\"handler\" v-if=\"x > 1 && y < 2\"><slot-content slot=\"header\">");
        Sentence(sb, rng, 2, 5);
        sb.Append("</slot-content></my-widget>\n");
        sb.Append("<template id=\"tpl-").Append(index).Append("\"><tr><td class=\"cell\">{{ value }}</td></tr><li>").Append(Word(rng)).Append("</li></template>\n");
        sb.Append("<picture><source srcset=\"a.webp\" type=\"image/webp\"><source srcset=\"a.avif\" type=\"image/avif\"><img src=\"a.jpg\" alt=\"\"></picture>\n");
        sb.Append("<video controls preload=\"none\" poster=\"p.jpg\"><source src=\"v.mp4\" type=\"video/mp4\"><track kind=\"captions\" src=\"c.vtt\" srclang=\"en\"></video>\n");
        sb.Append("<!--[if lt IE 9]><script src=\"html5shiv.js\"></script><![endif]-->\n<?php echo 'bogus comment'; ?>\n");
        sb.Append("<p>stray end tags</span></b></em></p></div-not-open>\n");
    }

    private static void InlineScript(StringBuilder sb, Random rng, Int32 index)
    {
        sb.Append("<script>\n(function (w, d) {\n  'use strict';\n  var data").Append(index).Append(" = [");

        for (var i = 0; i < 40; i++)
        {
            sb.Append("{ id: ").Append(rng.Next(100000)).Append(", name: \"").Append(Word(rng))
              .Append("\", html: \"<div class='x'>\" + (a < b && c > d ? '</div>' : '<\\/script>') },\n    ");
        }

        sb.Append("];\n  if (w.length < 10 && d.title > '') { d.write('<!-- <script> not the end </scr' + 'ipt> -->'); }\n})(window, document);\n</script>\n");
    }

    private static void ClassList(StringBuilder sb, Random rng)
    {
        var count = 1 + rng.Next(4);

        for (var i = 0; i < count; i++)
        {
            if (i != 0)
            {
                sb.Append(' ');
            }

            sb.Append(Classes[rng.Next(Classes.Length)]);
        }
    }

    private static void Sentence(StringBuilder sb, Random rng, Int32 min, Int32 max)
    {
        var count = min + rng.Next(max - min + 1);

        for (var i = 0; i < count; i++)
        {
            if (i != 0)
            {
                sb.Append(' ');
            }

            sb.Append(Words[rng.Next(Words.Length)]);
        }
    }

    private static String Word(Random rng) => Words[rng.Next(Words.Length)];
}
