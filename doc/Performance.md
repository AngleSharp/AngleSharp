# Performance Evaluations

## General Considerations

The library is not small (its not huge either), which makes preloading or "warming-up" (or using NGen) a candidate for productive usage. The first runs will always be slower than the following. This is a property of C# / the MSIL (or the JIT process in general), which has nothing to do with AngleSharp.

AngleSharp has been written with performance in mind. Actually the priority is:

1. Standard conformance
2. Performance
3. Helpers / extensions
4. Tooling

Even though there are still optimizations possible (since everything had to be working first, i.e. standard conformance has been considered), the performance is promising. Parsers in established web browsers (usually written in C/C++) will still have a faster execution time, but for a completely managed process the whole parsing / DOM creation procedure is very fast.

In the following AngleSharp will be compared to other popular libraries, which are either very popular (like the HtmlAgilityPack) or ambitious (like CsQuery).

## Comparison with the Html Agility Pack

The Html Agility Pack (HAP) is a good comparison target, since it is most used and established since a long time. Obviously AngleSharp is a great replacement for HAP due to the following reasons:

* Standardized HTML5 parsing model
* Much better error correction / handling
* Also parses SVG / MathML elements correctly
* Can handle CSS (selectors, rules, ...)
* Better performance

While the first points are all quite obvious and clear (that is why AngleSharp has been developed in the first place), the last point is controversial.

To proof this point a small test program has been written. The following code snippet represents the kernel of this program.

```c#
static async Task<Int64> Test(Func<String, Int64> test, String source)
{
    var min = Int64.MaxValue;

    for (int i = 0; i < 20; i++)
        min = Math.Min(min, await Task.Run(() => test(source)));

    return min;
}

static Int64 TestAngle(String source)
{
    var sw = Stopwatch.StartNew();
    var parser = new HtmlParser(source);
    parser.Parse();
    sw.Stop();
    return sw.ElapsedMilliseconds;
}

static Int64 TestAgility(String source)
{
    var sw = Stopwatch.StartNew();
    var document = new HtmlDocument();
    document.LoadHtml(source);
    sw.Stop();
    return sw.ElapsedMilliseconds;
}
```

This program then calls the `Test` method with the source code of several webpages. All of them are quite important / used very often and some of them are quite big. The biggest is definitely the official W3C syntax specification.

The following image shows the outcome of running this test. (caution: Outdated versions of *all* included parser; just for illustration)

![Comparison AngleSharp HtmlAgilityPack](http://www.florian-rappl.de/img/0/comparison_as_hap_csq.png)

Note that we took the lowest wall-time in 20 trials. The outcome details change slightly if we take the average - since AngleSharp has more code-paths and therefore requires a greater mix in warm-up runs than the HAP, however, the overall outcome is still the same with AngleSharp being faster.

The only entry where HAP excels is "GoogleNews". This is, however, a false impression. The true speedup comes from omitting the (large) inline style-sheet. Here AngleSharp has to parse CSS as well, which of course takes some additional time. The official benchmark program below excludes such edge cases by disabling CSS parsing (therefore only HTML parsing is benchmarked as intended).

## Comparison with CsQuery / validator.nu HTML Parser

The CsQuery project aims to be a C# port of jQuery. To parse HTML a port of the validator.nu HTML5 engine has been selected. The validity of the resulting DOM should therefore on roughly the same level as AngleSharp, however, missing important elements such as `template` or `main`. The above image also includes the performance measurement for the HTML parser.

The parsing performance is mixed compared to AngleSharp. AngleSharp has been measured in an early stage (v0.8.6), i.e. it is likely that the performance of AngleSharp will increase even further. While the CsQuery project is ambitious and contains some interesting ideas and features, AngleSharp tries to solve the problem from the other side - follow the standards first and add nice features on top afterwards. The past has shown that implementing fancy features or a non-standard, but better, API first, will lead to problems later on.

In general there are areas where AngleSharp is performing better, and other pages where the validator.nu parser is faster. In most cases the winner does not really matter (cases such as 2ms vs 3ms).

## Comparison with Other Solutions

One could also make comparisons to other projects. In the official **Performance** project (contained in the Visual Studio solution) you will find the *Majestic* (Majestic 13) parser. This one is nearly always faster than any other solution. Is it the right choice for your project? Probably not. The reason is simple: Majestic does not build a DOM, it also does not care about special tags, meanings and the HTML error correction. Basically Majestic is AngleSharp reduced to its tokenizer, with the tokenizer being a little bit simpler.

This is also the reason for excluding Majestic from the performance comparison. However, you are (for your own pleasure) of course allowed to include Majestic again. The code is already checked-in.

## Current Performance

Currently the performance of AngleSharp is quite satisfying. On an Intel Core i5 4570 with 3.2 GHz system with 16 GB of RAM we could gather the following statistics (from the official benchmark program).

```text
                            RUNNING TESTS (v0.9.1)
============================================================================
                       AngleSharp           CsQuery        HTMLAgilityPack
----------------------------------------------------------------------------
amazon                     1ms                7ms                0ms
blogspot                   1ms                2ms                5ms
smashing                   1ms                1ms                1ms
youtube                   11ms               15ms               13ms
weibo                      0ms                0ms                0ms
yahoo                      8ms               35ms               22ms
google                     2ms                2ms                8ms
linkedin                   3ms                2ms                3ms
pinterest                  1ms                1ms                5ms
news.google               28ms               34ms               41ms
baidu                      1ms                1ms                6ms
codeproject                4ms                4ms                4ms
ebay                       8ms                8ms                8ms
msn                       18ms               18ms               13ms
nbc                        5ms                4ms                8ms
qq                        17ms              1060ms              52ms
florian-rappl              0ms                1ms                0ms
stackoverflow             16ms               15ms               12ms
html5rocks                 0ms                0ms                0ms
live                       0ms                0ms                0ms
taobao                    14ms               15ms                7ms
huffingtonpost            11ms                9ms               10ms
wordpress                  1ms                0ms                0ms
myspace                   20ms               29ms               21ms
flickr                     3ms                5ms               13ms
godaddy                    6ms                5ms                7ms
reddit                     6ms                9ms                6ms
nytimes                   14ms               13ms               13ms
peacekeeper.futu...        0ms                0ms                1ms
pcmag                      9ms               11ms               16ms
sitepoint                  1ms                2ms                3ms
html5test                  0ms                1ms                2ms
spiegel                   15ms               12ms               13ms
tmall                      2ms                3ms                2ms
sohu                      20ms               46ms               39ms
vk                         2ms                0ms                1ms
wordpress                  2ms                0ms                0ms
bing                       1ms                1ms                4ms
tumblr                     2ms                3ms                3ms
ask                        0ms                0ms                1ms
mail.ru                    6ms               11ms               15ms
imdb                       6ms                4ms                6ms
kickass.to                 0ms                0ms                0ms
360.cn                     4ms                4ms                8ms
163                       32ms               45ms               56ms
neobux                     1ms                0ms                0ms
aliexpress                10ms                9ms                9ms
netflix                    4ms                3ms                7ms
w3                        912ms              579ms             1064ms
en.wikipedia              37ms               26ms               33ms
----------------------------------------------------------------------------
Total                    1292ms             2080ms             1583ms
----------------------------------------------------------------------------
Fastest                    20                 19                 11
----------------------------------------------------------------------------
Slowest                    13                 12                 25
----------------------------------------------------------------------------
```

This run has been made with the default setting of 5 repeats and 1 run. Fluctuations might be high and a representative examination has to consider lots of repeats and runs.

At the moment the performance of AngleSharp seems to be better than HAP and about equal to CsQuery. There are edge cases (as soon above), which are under investigation. They will certainly be improved before relasing v1.0.0.
