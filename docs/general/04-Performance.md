---
title: "Performance"
section: "AngleSharp.Core"
---
# Performance Evaluations

## General Considerations

The library is not small (it's not huge either), which makes preloading or "warming-up" (or using NGen) a candidate for productive usage. The first runs will always be slower than the following. This is a property of C# / the MSIL (or the JIT process in general), which has nothing to do with AngleSharp.

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

    for (int i = 0; i < 20; i++) {
        min = Math.Min(min, await Task.Run(() => test(source)));
    }

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

One could also make comparisons to other projects. In the official **Performance** project (contained in the Visual Studio solution) you will find the *Majestic* (Majestic 13) parser. This one is nearly always faster than any other solution. Is it the right choice for your project? Probably not. The reason is simple: Majestic does not build a DOM, it also does not care about special tags, meanings and the HTML error correction. Basically Majestic is AngleSharp reduced to its tokenizer, with the tokenizer being a bit simpler.

This is also the reason for excluding Majestic from the performance comparison. However, you are (for your own pleasure) of course allowed to include Majestic again. The code is already checked-in.

## Current Performance

Performance is valued high - even though standard-compliance is valued higher. AngleSharp's repository comes with a set of benchmarks.

### HTML Parser

The setup for this test was:

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 7 2700X, 1 CPU, 16 logical and 8 physical cores
  [Host]   : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
  ShortRun : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1
WarmupCount=3
```

The results are displayed below.

| Method          | UrlTest                  |              Mean |              Error |            StdDev |          Gen 0 |          Gen 1 |          Gen 2 |        Allocated |
|-----------------|--------------------------|------------------:|-------------------:|------------------:|---------------:|---------------:|---------------:|-----------------:|
| **CsQuery**     | **163**                  |  **26,572.23 μs** |   **8,325.645 μs** |    **456.357 μs** |  **3500.0000** |   **843.7500** |   **312.5000** |  **17524.19 KB** |
| HTMLAgilityPack | 163                      |      31,582.19 μs |      10,016.229 μs |        549.023 μs |      2687.5000 |       937.5000 |       312.5000 |      15171.18 KB |
| AngleSharp      | 163                      |      33,778.85 μs |       2,390.223 μs |        131.016 μs |      1866.6667 |       800.0000 |       266.6667 |      11667.82 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **360.cn**               |   **5,183.12 μs** |     **388.822 μs** |     **21.313 μs** |   **421.8750** |   **210.9375** |          **-** |   **2170.52 KB** |
| HTMLAgilityPack | 360.cn                   |       5,995.97 μs |         217.311 μs |         11.912 μs |       585.9375 |       289.0625 |              - |       3530.83 KB |
| AngleSharp      | 360.cn                   |       9,059.31 μs |       1,809.919 μs |         99.208 μs |       468.7500 |       234.3750 |        31.2500 |        2350.3 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **aliexpress**           |   **1,048.39 μs** |      **76.373 μs** |      **4.186 μs** |   **167.9688** |    **54.6875** |          **-** |     **610.8 KB** |
| HTMLAgilityPack | aliexpress               |       2,426.29 μs |         501.179 μs |         27.471 μs |       640.6250 |       179.6875 |        15.6250 |       2251.48 KB |
| AngleSharp      | aliexpress               |       2,058.30 μs |         334.734 μs |         18.348 μs |       156.2500 |        58.5938 |              - |        591.33 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **amazon**               |     **109.46 μs** |       **7.386 μs** |      **0.405 μs** |   **138.6719** |          **-** |          **-** |    **106.57 KB** |
| HTMLAgilityPack | amazon                   |          57.12 μs |           0.217 μs |          0.012 μs |        32.9590 |              - |              - |         25.33 KB |
| AngleSharp      | amazon                   |         108.14 μs |           5.729 μs |          0.314 μs |        32.5928 |              - |              - |         25.07 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **ask**                  |   **2,855.16 μs** |     **136.272 μs** |      **7.470 μs** |   **386.7188** |   **175.7813** |    **42.9688** |   **1917.87 KB** |
| HTMLAgilityPack | ask                      |       6,720.90 μs |         654.156 μs |         35.856 μs |      1187.5000 |       585.9375 |              - |       6688.45 KB |
| AngleSharp      | ask                      |       5,309.43 μs |         440.766 μs |         24.160 μs |       296.8750 |       156.2500 |        70.3125 |       1789.05 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **baidu**                |   **9,185.98 μs** |   **2,700.571 μs** |    **148.027 μs** | **12015.6250** | **11531.2500** | **11109.3750** |  **58430.78 KB** |
| HTMLAgilityPack | baidu                    |      19,836.76 μs |       1,686.464 μs |         92.441 μs |     28000.0000 |       968.7500 |              - |      25856.31 KB |
| AngleSharp      | baidu                    |       5,330.41 μs |       1,213.111 μs |         66.495 μs |       679.6875 |       593.7500 |       414.0625 |       2754.81 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **bing**                 |   **1,787.21 μs** |     **173.355 μs** |      **9.502 μs** |   **851.5625** |   **421.8750** |          **-** |    **4629.7 KB** |
| HTMLAgilityPack | bing                     |       4,301.44 μs |         237.596 μs |         13.023 μs |      1671.8750 |       335.9375 |        62.5000 |       4831.79 KB |
| AngleSharp      | bing                     |       2,191.32 μs |         107.560 μs |          5.896 μs |       183.5938 |        89.8438 |              - |        798.45 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **blogspot**             |     **118.00 μs** |       **4.659 μs** |      **0.255 μs** |    **89.2334** |          **-** |          **-** |      **68.8 KB** |
| HTMLAgilityPack | blogspot                 |         224.73 μs |          18.088 μs |          0.991 μs |       291.2598 |              - |              - |        224.22 KB |
| AngleSharp      | blogspot                 |         284.27 μs |          21.215 μs |          1.163 μs |       106.4453 |         8.7891 |              - |         91.93 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **codeproject**          |   **7,574.01 μs** |   **1,090.559 μs** |     **59.777 μs** | **19085.9375** |   **750.0000** |   **351.5625** |   **29326.3 KB** |
| HTMLAgilityPack | codeproject              |       8,439.77 μs |       1,165.335 μs |         63.876 μs |      2234.3750 |       656.2500 |        15.6250 |       8052.49 KB |
| AngleSharp      | codeproject              |       4,944.07 μs |         142.958 μs |          7.836 μs |       312.5000 |       164.0625 |        39.0625 |       1648.52 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **ebay**                 |   **8,113.73 μs** |     **813.583 μs** |     **44.595 μs** |   **828.1250** |   **484.3750** |   **156.2500** |   **4488.56 KB** |
| HTMLAgilityPack | ebay                     |      13,586.42 μs |         817.790 μs |         44.826 μs |      1921.8750 |       578.1250 |       218.7500 |       9665.77 KB |
| AngleSharp      | ebay                     |      13,333.32 μs |       3,862.157 μs |        211.698 μs |       703.1250 |       468.7500 |       203.1250 |        3515.6 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **en.wikipedia**         |  **28,638.36 μs** |   **3,969.680 μs** |    **217.592 μs** |  **1687.5000** |   **781.2500** |   **218.7500** |   **9941.59 KB** |
| HTMLAgilityPack | en.wikipedia             |      50,891.40 μs |      28,096.639 μs |      1,540.072 μs |      2800.0000 |      1200.0000 |       400.0000 |      14571.52 KB |
| AngleSharp      | en.wikipedia             |      40,260.49 μs |       9,929.362 μs |        544.262 μs |      1692.3077 |       923.0769 |       307.6923 |       9609.23 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **flickr**               |   **5,523.62 μs** |     **528.198 μs** |     **28.952 μs** |  **2187.5000** |  **1953.1250** |  **1343.7500** |  **11630.04 KB** |
| HTMLAgilityPack | flickr                   |      16,674.56 μs |       1,738.980 μs |         95.319 μs |     11187.5000 |       343.7500 |       125.0000 |       18192.6 KB |
| AngleSharp      | flickr                   |       8,770.69 μs |       3,034.916 μs |        166.354 μs |       640.6250 |       531.2500 |       359.3750 |       2906.85 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **florian-rappl**        |     **707.50 μs** |      **25.038 μs** |      **1.372 μs** |   **142.5781** |    **39.0625** |          **-** |    **326.33 KB** |
| HTMLAgilityPack | florian-rappl            |         713.63 μs |          90.272 μs |          4.948 μs |       134.7656 |        40.0391 |              - |        457.35 KB |
| AngleSharp      | florian-rappl            |       1,227.61 μs |          69.995 μs |          3.837 μs |       121.0938 |        39.0625 |              - |        342.15 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **godaddy**              |   **6,863.21 μs** |     **385.012 μs** |     **21.104 μs** |   **843.7500** |   **421.8750** |    **70.3125** |   **3965.29 KB** |
| HTMLAgilityPack | godaddy                  |      17,099.53 μs |       2,352.418 μs |        128.944 μs |      3156.2500 |       562.5000 |       218.7500 |      14457.44 KB |
| AngleSharp      | godaddy                  |      10,710.47 μs |       4,228.312 μs |        231.768 μs |       531.2500 |       312.5000 |       125.0000 |       2959.15 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **google**               |   **2,475.83 μs** |      **66.243 μs** |      **3.631 μs** |  **1820.3125** |   **511.7188** |    **35.1563** |   **6307.94 KB** |
| HTMLAgilityPack | google                   |       8,117.38 μs |         889.700 μs |         48.767 μs |      8265.6250 |       531.2500 |        46.8750 |       9687.47 KB |
| AngleSharp      | google                   |       2,852.58 μs |         207.706 μs |         11.385 μs |       230.4688 |       121.0938 |        54.6875 |       1118.61 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **html5rocks**           |     **165.07 μs** |       **5.314 μs** |      **0.291 μs** |    **96.4355** |     **9.2773** |          **-** |     **91.02 KB** |
| HTMLAgilityPack | html5rocks               |         185.48 μs |          21.314 μs |          1.168 μs |       129.6387 |        16.1133 |              - |        143.17 KB |
| AngleSharp      | html5rocks               |         344.76 μs |          10.928 μs |          0.599 μs |       102.0508 |         8.7891 |              - |         94.98 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **html5test**            |     **549.88 μs** |      **29.206 μs** |      **1.601 μs** |   **347.6563** |    **96.6797** |          **-** |    **812.11 KB** |
| HTMLAgilityPack | html5test                |       1,100.92 μs |         113.205 μs |          6.205 μs |       412.1094 |       136.7188 |              - |       1225.64 KB |
| AngleSharp      | html5test                |         832.16 μs |          80.749 μs |          4.426 μs |       127.9297 |        41.9922 |              - |        326.65 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **huffingtonpost**       |  **19,952.34 μs** |  **12,220.195 μs** |    **669.830 μs** |  **5125.0000** |  **2156.2500** |  **1218.7500** |  **20271.86 KB** |
| HTMLAgilityPack | huffingtonpost           |      23,360.13 μs |         267.385 μs |         14.656 μs |     13531.2500 |       687.5000 |       187.5000 |      19993.95 KB |
| AngleSharp      | huffingtonpost           |      21,286.14 μs |       1,466.969 μs |         80.410 μs |      1093.7500 |       718.7500 |       343.7500 |       5560.22 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **imdb**                 |  **28,846.69 μs** |  **13,103.589 μs** |    **718.252 μs** |  **6937.5000** |  **2093.7500** |  **1406.2500** |  **53164.72 KB** |
| HTMLAgilityPack | imdb                     |      53,782.78 μs |      14,400.153 μs |        789.321 μs |     46000.0000 |      1700.0000 |       600.0000 |      47500.36 KB |
| AngleSharp      | imdb                     |      33,662.42 μs |       4,272.336 μs |        234.181 μs |      1666.6667 |      1200.0000 |       533.3333 |       10612.3 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **kickass.to**           |      **62.52 μs** |      **15.028 μs** |      **0.824 μs** |    **34.4238** |          **-** |          **-** |     **26.47 KB** |
| HTMLAgilityPack | kickass.to               |          21.91 μs |           4.389 μs |          0.241 μs |        26.5198 |              - |              - |         20.39 KB |
| AngleSharp      | kickass.to               |          48.39 μs |          10.936 μs |          0.599 μs |        21.8506 |              - |              - |         16.79 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **linkedin**             |   **3,926.10 μs** |     **765.055 μs** |     **41.935 μs** |   **289.0625** |   **140.6250** |    **23.4375** |    **1599.7 KB** |
| HTMLAgilityPack | linkedin                 |       3,327.32 μs |         415.856 μs |         22.794 μs |       343.7500 |       171.8750 |              - |       1832.89 KB |
| AngleSharp      | linkedin                 |       5,978.55 μs |         261.960 μs |         14.359 μs |       257.8125 |       125.0000 |        54.6875 |       1385.39 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **live**                 |   **1,896.05 μs** |      **90.959 μs** |      **4.986 μs** |   **162.1094** |    **70.3125** |          **-** |    **875.95 KB** |
| HTMLAgilityPack | live                     |       1,728.75 μs |         160.758 μs |          8.812 μs |       189.4531 |        93.7500 |              - |       1068.87 KB |
| AngleSharp      | live                     |       2,967.42 μs |         374.529 μs |         20.529 μs |       148.4375 |        74.2188 |              - |        783.66 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **mail.ru**              |  **14,738.88 μs** |  **12,151.966 μs** |    **666.090 μs** | **55000.0000** |  **1640.6250** |   **593.7500** |  **62888.42 KB** |
| HTMLAgilityPack | mail.ru                  |      20,945.32 μs |       6,117.784 μs |        335.336 μs |     14500.0000 |       406.2500 |       187.5000 |      22585.69 KB |
| AngleSharp      | mail.ru                  |      10,070.01 μs |         757.617 μs |         41.528 μs |       656.2500 |       468.7500 |       250.0000 |       3241.27 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **msn**                  |  **10,318.80 μs** |   **2,216.914 μs** |    **121.517 μs** |  **1406.2500** |   **625.0000** |    **78.1250** |   **6484.48 KB** |
| HTMLAgilityPack | msn                      |      11,391.01 μs |         998.940 μs |         54.755 μs |      2875.0000 |       859.3750 |        15.6250 |       7951.19 KB |
| AngleSharp      | msn                      |      16,140.33 μs |         613.836 μs |         33.646 μs |       781.2500 |       468.7500 |       125.0000 |       4571.39 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **myspace**              |  **22,910.76 μs** |   **2,477.144 μs** |    **135.781 μs** | **62437.5000** |   **781.2500** |   **250.0000** |  **62789.61 KB** |
| HTMLAgilityPack | myspace                  |      20,529.88 μs |       5,132.015 μs |        281.303 μs |      1781.2500 |       781.2500 |       250.0000 |       10333.3 KB |
| AngleSharp      | myspace                  |      20,662.87 μs |       1,721.811 μs |         94.378 μs |       906.2500 |       437.5000 |       125.0000 |       5388.78 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **nbc**                  | **153,610.90 μs** | **241,305.708 μs** | **13,226.780 μs** | **42000.0000** | **39000.0000** | **38000.0000** |  **847835.7 KB** |
| HTMLAgilityPack | nbc                      |     131,026.33 μs |     252,456.797 μs |     13,838.009 μs |    163750.0000 |      1500.0000 |       500.0000 |     142509.96 KB |
| AngleSharp      | nbc                      |      68,009.41 μs |      10,392.195 μs |        569.631 μs |      3125.0000 |      1250.0000 |       500.0000 |      30147.35 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **neobux**               |     **157.14 μs** |       **5.539 μs** |      **0.304 μs** |    **99.3652** |     **5.6152** |          **-** |     **86.08 KB** |
| HTMLAgilityPack | neobux                   |         216.95 μs |           6.915 μs |          0.379 μs |       216.3086 |        14.8926 |              - |        199.43 KB |
| AngleSharp      | neobux                   |         266.50 μs |           9.616 μs |          0.527 μs |        85.9375 |         2.9297 |              - |         69.35 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **netflix**              |   **7,751.63 μs** |   **6,590.316 μs** |    **361.237 μs** |  **7031.2500** |  **5687.5000** |  **5093.7500** |   **36962.4 KB** |
| HTMLAgilityPack | netflix                  |      20,402.40 μs |         532.399 μs |         29.183 μs |     30218.7500 |       187.5000 |        62.5000 |       28514.5 KB |
| AngleSharp      | netflix                  |       5,932.85 μs |         370.366 μs |         20.301 μs |       781.2500 |       718.7500 |       500.0000 |       3935.91 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **news.google**          |  **15,521.61 μs** |   **1,692.227 μs** |     **92.757 μs** | **15578.1250** | **12843.7500** | **11515.6250** |   **66116.9 KB** |
| HTMLAgilityPack | news.google              |      22,252.10 μs |       1,478.085 μs |         81.019 μs |     27093.7500 |       968.7500 |              - |      26119.47 KB |
| AngleSharp      | news.google              |      11,707.40 μs |         923.723 μs |         50.632 μs |      1015.6250 |      1000.0000 |       656.2500 |       4325.95 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **nytimes**              | **124,187.17 μs** | **251,118.584 μs** | **13,764.657 μs** | **49000.0000** | **46000.0000** | **45000.0000** | **718302.89 KB** |
| HTMLAgilityPack | nytimes                  |      95,744.94 μs |       7,128.314 μs |        390.727 μs |    156333.3333 |       333.3333 |       166.6667 |     135003.91 KB |
| AngleSharp      | nytimes                  |      27,782.88 μs |      10,274.712 μs |        563.192 μs |      1187.5000 |       781.2500 |       375.0000 |      17691.08 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **pcmag**                |      **62.39 μs** |       **1.935 μs** |      **0.106 μs** |    **36.1328** |          **-** |          **-** |     **27.81 KB** |
| HTMLAgilityPack | pcmag                    |          45.43 μs |           3.050 μs |          0.167 μs |        65.0024 |              - |              - |         49.94 KB |
| AngleSharp      | pcmag                    |          52.58 μs |           4.139 μs |          0.227 μs |        23.8647 |              - |              - |         18.36 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **peace(...)emark [22]** |     **990.19 μs** |      **81.206 μs** |      **4.451 μs** |   **144.5313** |    **46.8750** |          **-** |     **460.6 KB** |
| HTMLAgilityPack | peace(...)emark [22]     |       1,367.17 μs |         120.318 μs |          6.595 μs |       218.7500 |       103.5156 |              - |        1039.6 KB |
| AngleSharp      | peace(...)emark [22]     |       1,632.73 μs |         134.103 μs |          7.351 μs |       119.1406 |        46.8750 |              - |        499.36 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **pinterest**            |   **1,081.07 μs** |     **115.151 μs** |      **6.312 μs** |   **537.1094** |   **226.5625** |   **146.4844** |   **2078.93 KB** |
| HTMLAgilityPack | pinterest                |       5,919.82 μs |         683.017 μs |         37.438 μs |      7679.6875 |       210.9375 |        62.5000 |       7936.13 KB |
| AngleSharp      | pinterest                |       2,269.28 μs |         142.931 μs |          7.835 μs |       136.7188 |       128.9063 |       128.9063 |       1121.95 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **qq**                   |   **6,051.05 μs** |     **521.770 μs** |     **28.600 μs** |   **453.1250** |   **226.5625** |    **31.2500** |   **2471.28 KB** |
| HTMLAgilityPack | qq                       |       5,935.02 μs |         478.857 μs |         26.248 μs |       593.7500 |       296.8750 |              - |       3345.35 KB |
| AngleSharp      | qq                       |       8,620.11 μs |         605.321 μs |         33.180 μs |       406.2500 |       203.1250 |        46.8750 |       2134.35 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **reddit**               |  **28,485.33 μs** |  **16,629.956 μs** |    **911.544 μs** |  **7343.7500** |  **6656.2500** |  **5937.5000** |  **97804.74 KB** |
| HTMLAgilityPack | reddit                   |      55,492.40 μs |       2,706.110 μs |        148.331 μs |     74000.0000 |       400.0000 |       200.0000 |      68976.93 KB |
| AngleSharp      | reddit                   |      18,756.14 μs |       3,652.892 μs |        200.227 μs |      1343.7500 |      1125.0000 |       687.5000 |       8795.31 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **sitepoint**            |   **5,456.44 μs** |      **75.188 μs** |      **4.121 μs** |  **1039.0625** |   **648.4375** |   **328.1250** |   **3999.25 KB** |
| HTMLAgilityPack | sitepoint                |       9,606.40 μs |         996.101 μs |         54.600 μs |      2796.8750 |       484.3750 |        46.8750 |       7479.26 KB |
| AngleSharp      | sitepoint                |      11,834.08 μs |       1,248.806 μs |         68.451 μs |       718.7500 |       437.5000 |       187.5000 |       3421.89 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **smashing**             |     **107.09 μs** |       **3.101 μs** |      **0.170 μs** |    **76.1719** |          **-** |          **-** |     **58.63 KB** |
| HTMLAgilityPack | smashing                 |          97.93 μs |           8.065 μs |          0.442 μs |       118.7744 |              - |              - |         91.27 KB |
| AngleSharp      | smashing                 |         146.05 μs |           9.340 μs |          0.512 μs |        55.4199 |              - |              - |         42.68 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **sohu**                 |   **8,609.93 μs** |   **1,500.603 μs** |     **82.253 μs** |   **546.8750** |   **281.2500** |    **62.5000** |   **3279.43 KB** |
| HTMLAgilityPack | sohu                     |       8,968.81 μs |         633.634 μs |         34.732 μs |       765.6250 |       375.0000 |              - |       4585.71 KB |
| AngleSharp      | sohu                     |      13,511.16 μs |       3,160.689 μs |        173.248 μs |       546.8750 |       328.1250 |       109.3750 |       3361.91 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **spiegel**              | **211,519.63 μs** | **372,492.375 μs** | **20,417.564 μs** | **34000.0000** | **15000.0000** | **10000.0000** | **576924.01 KB** |
| HTMLAgilityPack | spiegel                  |      75,016.80 μs |      53,364.569 μs |      2,925.092 μs |     12125.0000 |      2125.0000 |       750.0000 |      31002.63 KB |
| AngleSharp      | spiegel                  |      76,223.27 μs |      10,207.845 μs |        559.526 μs |      3000.0000 |      1000.0000 |              - |      17287.29 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **stackoverflow**        |   **7,908.16 μs** |      **24.694 μs** |      **1.354 μs** |   **687.5000** |   **343.7500** |    **46.8750** |   **4275.74 KB** |
| HTMLAgilityPack | stackoverflow            |       6,521.56 μs |         373.698 μs |         20.484 μs |       718.7500 |       343.7500 |              - |       3761.42 KB |
| AngleSharp      | stackoverflow            |      11,342.72 μs |         171.985 μs |          9.427 μs |       515.6250 |       296.8750 |        93.7500 |       2645.64 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **taobao**               |   **8,823.25 μs** |   **4,057.001 μs** |    **222.378 μs** | **10671.8750** |  **3562.5000** |  **2468.7500** |  **28015.66 KB** |
| HTMLAgilityPack | taobao                   |      24,314.95 μs |       2,437.444 μs |        133.605 μs |     29437.5000 |       906.2500 |        31.2500 |      30738.97 KB |
| AngleSharp      | taobao                   |       7,903.38 μs |         422.405 μs |         23.153 μs |       953.1250 |       828.1250 |       546.8750 |       4200.56 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **tmall**                |  **20,233.00 μs** |   **9,593.654 μs** |    **525.861 μs** | **35156.2500** | **24750.0000** | **22781.2500** | **132866.65 KB** |
| HTMLAgilityPack | tmall                    |       3,106.31 μs |         365.161 μs |         20.016 μs |       761.7188 |       238.2813 |              - |          2274 KB |
| AngleSharp      | tmall                    |       3,624.70 μs |          51.738 μs |          2.836 μs |       234.3750 |       175.7813 |       175.7813 |       1540.94 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **tumblr**               |   **1,257.62 μs** |     **103.290 μs** |      **5.662 μs** |   **187.5000** |    **62.5000** |          **-** |    **591.96 KB** |
| HTMLAgilityPack | tumblr                   |       1,522.42 μs |          12.835 μs |          0.704 μs |       210.9375 |       101.5625 |              - |       1236.02 KB |
| AngleSharp      | tumblr                   |       2,845.86 μs |         119.163 μs |          6.532 μs |       148.4375 |        58.5938 |              - |         659.3 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **vk**                   |   **2,879.40 μs** |     **307.347 μs** |     **16.847 μs** |  **1398.4375** |   **472.6563** |   **468.7500** |   **6900.75 KB** |
| HTMLAgilityPack | vk                       |       5,018.50 μs |         932.314 μs |         51.103 μs |      1039.0625 |       492.1875 |              - |       6016.33 KB |
| AngleSharp      | vk                       |       2,900.81 μs |          79.652 μs |          4.366 μs |       214.8438 |       156.2500 |       156.2500 |       1174.17 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **weibo**                |   **6,864.06 μs** |     **753.160 μs** |     **41.283 μs** | **64468.7500** |  **1523.4375** |   **328.1250** |  **55621.54 KB** |
| HTMLAgilityPack | weibo                    |       6,301.79 μs |         533.409 μs |         29.238 μs |      6210.9375 |       242.1875 |        78.1250 |        7947.5 KB |
| AngleSharp      | weibo                    |       1,957.82 μs |          58.780 μs |          3.222 μs |       203.1250 |        85.9375 |        35.1563 |        972.91 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **wordpress**            |   **1,548.06 μs** |      **51.681 μs** |      **2.833 μs** |   **197.2656** |    **76.1719** |          **-** |    **715.13 KB** |
| CsQuery         | wordpress                |       1,517.53 μs |          86.715 μs |          4.753 μs |       197.2656 |        76.1719 |              - |        715.13 KB |
| HTMLAgilityPack | wordpress                |       2,479.26 μs |          82.376 μs |          4.515 μs |       546.8750 |       203.1250 |              - |       1891.62 KB |
| HTMLAgilityPack | wordpress                |       2,492.86 μs |         109.191 μs |          5.985 μs |       546.8750 |       203.1250 |              - |       1891.62 KB |
| AngleSharp      | wordpress                |       3,326.00 μs |         172.623 μs |          9.462 μs |       171.8750 |        82.0313 |              - |        898.76 KB |
| AngleSharp      | wordpress                |       3,315.28 μs |          70.997 μs |          3.892 μs |       171.8750 |        82.0313 |              - |        898.76 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **yahoo**                |  **23,053.18 μs** |  **13,648.954 μs** |    **748.145 μs** | **12718.7500** | **11000.0000** |  **9250.0000** |  **74161.32 KB** |
| HTMLAgilityPack | yahoo                    |      42,640.78 μs |       5,029.334 μs |        275.675 μs |     45500.0000 |       333.3333 |       166.6667 |      49719.34 KB |
| AngleSharp      | yahoo                    |      19,797.23 μs |         910.398 μs |         49.902 μs |      1468.7500 |      1187.5000 |       718.7500 |        7284.6 KB |
|                 |                          |                   |                    |                   |                |                |                |                  |
| **CsQuery**     | **youtube**              |     **183.10 μs** |       **4.834 μs** |      **0.265 μs** |   **103.7598** |    **14.4043** |          **-** |    **108.58 KB** |
| HTMLAgilityPack | youtube                  |         368.96 μs |          45.489 μs |          2.493 μs |       324.7070 |        39.5508 |              - |         346.6 KB |
| AngleSharp      | youtube                  |         454.85 μs |          41.482 μs |          2.274 μs |       119.6289 |        20.0195 |              - |        144.41 KB |

### DOM Querying / CSS Selectors

The setup for this test was:

``` ini
BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19042
AMD Ryzen 7 2700X, 1 CPU, 16 logical and 8 physical cores
  [Host]   : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT
  ShortRun : .NET Framework 4.8 (4.8.4341.0), X64 RyuJIT

Job=ShortRun  IterationCount=3  LaunchCount=1
WarmupCount=3
```

The results are displayed below.

|     Method |             Selector |         Mean |       Error |     StdDev |     Gen 0 |   Gen 1 | Gen 2 |  Allocated |
|----------- |--------------------- |-------------:|------------:|-----------:|----------:|--------:|------:|-----------:|
|    **CsQuery** |               **#title** |     **2.866 μs** |   **0.1455 μs** |  **0.0080 μs** |    **4.4479** |       **-** |     **-** |    **3.42 KB** |
| AngleSharp |               #title |   313.784 μs |  13.9545 μs |  0.7649 μs |   21.4844 |       - |     - |   16.91 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |                **.note** |     **7.283 μs** |   **1.2611 μs** |  **0.0691 μs** |    **8.1787** |       **-** |     **-** |    **6.28 KB** |
| AngleSharp |                .note |   326.217 μs |  25.8384 μs |  1.4163 μs |   21.9727 |       - |     - |   17.13 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |              **a[href]** |   **335.407 μs** |  **38.2870 μs** |  **2.0986 μs** |  **346.6797** |       **-** |     **-** |  **266.64 KB** |
| AngleSharp |              a[href] |   344.184 μs |   9.6133 μs |  0.5269 μs |   27.3438 |       - |     - |   21.21 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** | **a[href][lang][class]** |   **424.967 μs** |  **22.9636 μs** |  **1.2587 μs** |  **454.1016** |       **-** |     **-** |  **349.71 KB** |
| AngleSharp | a[href][lang][class] |   331.470 μs |  37.9205 μs |  2.0786 μs |   21.9727 |       - |     - |   17.25 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |                 **body** |     **3.672 μs** |   **0.6654 μs** |  **0.0365 μs** |    **4.5700** |       **-** |     **-** |    **3.51 KB** |
| AngleSharp |                 body |   313.239 μs |  27.4278 μs |  1.5034 μs |   21.4844 |       - |     - |   16.91 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |             **body div** |    **28.863 μs** |   **3.9254 μs** |  **0.2152 μs** |   **28.3813** |       **-** |     **-** |   **21.83 KB** |
| AngleSharp |             body div |   334.852 μs |  21.7180 μs |  1.1904 μs |   32.2266 |       - |     - |   25.01 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |                  **div** |    **20.936 μs** |   **2.8753 μs** |  **0.1576 μs** |   **20.3857** |       **-** |     **-** |   **15.67 KB** |
| AngleSharp |                  div |   333.623 μs |  12.5988 μs |  0.6906 μs |   22.9492 |       - |     - |   17.92 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |           **div #title** |    **81.408 μs** |   **0.8466 μs** |  **0.0464 μs** |   **78.9795** |       **-** |     **-** |   **60.67 KB** |
| AngleSharp |           div #title |   330.792 μs |  45.5623 μs |  2.4974 μs |   22.4609 |       - |     - |   17.33 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |              **div + p** |    **58.216 μs** |   **3.6349 μs** |  **0.1992 μs** |   **54.5654** |       **-** |     **-** |   **41.92 KB** |
| AngleSharp |              div + p |   420.089 μs |  53.8564 μs |  2.9521 μs |   42.4805 |       - |     - |   32.99 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |              **div &gt; p** |   **167.408 μs** |   **0.7517 μs** |  **0.0412 μs** |  **139.6484** |       **-** |     **-** |  **107.47 KB** |
| AngleSharp |              div &gt; p |   355.433 μs | 198.0923 μs | 10.8581 μs |   47.3633 |       - |     - |   36.54 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |          **div &gt; p &gt; a** |   **359.735 μs** |  **16.1910 μs** |  **0.8875 μs** |  **308.1055** |       **-** |     **-** |  **236.69 KB** |
| AngleSharp |          div &gt; p &gt; a |   347.709 μs |  52.0621 μs |  2.8537 μs |   45.4102 |       - |     - |   35.02 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |                **div p** |   **163.559 μs** |   **1.0895 μs** |  **0.0597 μs** |  **141.3574** |       **-** |     **-** |  **108.61 KB** |
| AngleSharp |                div p |   377.680 μs |  22.3904 μs |  1.2273 μs |   84.4727 |       - |     - |   64.98 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |              **div p a** |   **366.239 μs** |  **15.9706 μs** |  **0.8754 μs** |  **317.8711** |       **-** |     **-** |  **244.52 KB** |
| AngleSharp |              div p a |   437.168 μs |  30.2473 μs |  1.6580 μs |  106.4453 |       - |     - |   82.09 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |              **div ~ p** | **5,177.381 μs** | **205.7935 μs** | **11.2802 μs** | **6203.1250** |       **-** |     **-** | **4769.67 KB** |
| AngleSharp |              div ~ p | 1,895.874 μs | 435.0403 μs | 23.8460 μs |  957.0313 |       - |     - |  736.77 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |            **div, p, a** |   **380.255 μs** |  **33.9667 μs** |  **1.8618 μs** |  **172.8516** | **18.5547** |     **-** |   **170.5 KB** |
| AngleSharp |            div, p, a |   365.555 μs |  37.8889 μs |  2.0768 μs |   43.4570 |       - |     - |   33.48 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |          **div.example** |    **58.140 μs** |   **6.4138 μs** |  **0.3516 μs** |   **66.5283** |       **-** |     **-** |   **51.14 KB** |
| AngleSharp |          div.example |   342.246 μs |   3.7113 μs |  0.2034 μs |   23.4375 |       - |     - |   18.11 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** | **div.e(...).note [21]** |    **98.881 μs** |   **4.8513 μs** |  **0.2659 μs** |  **110.4736** |       **-** |     **-** |   **84.91 KB** |
| AngleSharp | div.e(...).note [21] |   347.342 μs |  40.8616 μs |  2.2398 μs |   23.4375 |       - |     - |   18.52 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |    **div:not(.example)** |    **44.816 μs** |   **0.7542 μs** |  **0.0413 μs** |   **34.9121** |       **-** |     **-** |   **26.83 KB** |
| AngleSharp |    div:not(.example) |   322.179 μs |  10.9380 μs |  0.5995 μs |   22.4609 |       - |     - |   17.59 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |  **div[class!=made_up]** |    **45.888 μs** |  **12.9403 μs** |  **0.7093 μs** |   **49.8657** |       **-** |     **-** |   **38.35 KB** |
| AngleSharp |  div[class!=made_up] |   312.672 μs |   2.2162 μs |  0.1215 μs |   23.4375 |       - |     - |   18.21 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |     **div[class$=mple]** |    **83.416 μs** |   **5.0121 μs** |  **0.2747 μs** |   **97.6563** |       **-** |     **-** |   **75.09 KB** |
| AngleSharp |     div[class$=mple] |   320.199 μs |   4.9361 μs |  0.2706 μs |   23.4375 |       - |     - |   18.21 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |        **div[class*=e]** |    **84.710 μs** |   **8.6005 μs** |  **0.4714 μs** |   **95.9473** |       **-** |     **-** |   **73.76 KB** |
| AngleSharp |        div[class*=e] |   313.320 μs |  11.9783 μs |  0.6566 μs |   23.4375 |       - |     - |   18.21 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |   **div[class=example]** |    **83.916 μs** |   **2.8985 μs** |  **0.1589 μs** |   **95.5811** |       **-** |     **-** |   **73.45 KB** |
| AngleSharp |   div[class=example] |   316.863 μs |  28.4840 μs |  1.5613 μs |   23.4375 |       - |     - |   18.22 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |           **div[class]** |    **61.271 μs** |   **1.7982 μs** |  **0.0986 μs** |   **69.0918** |       **-** |     **-** |   **53.14 KB** |
| AngleSharp |           div[class] |   316.051 μs |  14.2653 μs |  0.7819 μs |   23.4375 |       - |     - |   18.15 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |      **div[class^=exa]** |    **83.783 μs** |  **18.9781 μs** |  **1.0403 μs** |   **96.9238** |       **-** |     **-** |   **74.54 KB** |
| AngleSharp |      div[class^=exa] |   311.789 μs |  16.0779 μs |  0.8813 μs |   23.4375 |       - |     - |   18.21 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** | **div[c(...)mple] [28]** |   **135.848 μs** |   **6.9089 μs** |  **0.3787 μs** |  **166.5039** |       **-** |     **-** |  **128.02 KB** |
| AngleSharp | div[c(...)mple] [28] |   320.242 μs |  19.4510 μs |  1.0662 μs |   23.4375 |       - |     - |   18.33 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |   **div[class|=dialog]** |    **80.618 μs** |   **3.2140 μs** |  **0.1762 μs** |   **77.3926** |       **-** |     **-** |   **59.54 KB** |
| AngleSharp |   div[class|=dialog] |   310.329 μs |  13.1120 μs |  0.7187 μs |   21.9727 |       - |     - |   17.11 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |  **div[class~=example]** |   **102.342 μs** |   **2.3261 μs** |  **0.1275 μs** |  **117.5537** |       **-** |     **-** |    **90.3 KB** |
| AngleSharp |  div[class~=example] |   334.092 μs |  10.3627 μs |  0.5680 μs |   46.3867 |       - |     - |   36.08 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |             **h1#title** |     **4.880 μs** |   **0.2186 μs** |  **0.0120 μs** |    **6.7062** |       **-** |     **-** |    **5.16 KB** |
| AngleSharp |             h1#title |   325.145 μs | 103.5076 μs |  5.6736 μs |   21.9727 |       - |     - |   17.11 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |   **h1#title + div &gt; p** |     **8.236 μs** |   **0.1216 μs** |  **0.0067 μs** |    **9.7656** |       **-** |     **-** |    **7.51 KB** |
| AngleSharp |   h1#title + div &gt; p |   417.149 μs |  28.1992 μs |  1.5457 μs |   50.2930 |       - |     - |   38.96 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** | **h1[id(...)tors) [26]** |     **9.698 μs** |   **0.4063 μs** |  **0.0223 μs** |    **9.8114** |       **-** |     **-** |    **7.55 KB** |
| AngleSharp | h1[id(...)tors) [26] |   333.738 μs |  47.5316 μs |  2.6054 μs |   22.4609 |       - |     - |    17.7 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** | **p:con(...)tors) [21]** |   **383.667 μs** |   **7.8307 μs** |  **0.4292 μs** |   **59.0820** |       **-** |     **-** |   **45.61 KB** |
| AngleSharp | p:con(...)tors) [21] |   717.665 μs |  17.4106 μs |  0.9543 μs |  313.4766 |       - |     - |  241.68 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |        **p:first-child** |   **110.793 μs** |  **17.4810 μs** |  **0.9582 μs** |   **78.8574** |       **-** |     **-** |   **60.62 KB** |
| AngleSharp |        p:first-child |   338.089 μs |  60.8125 μs |  3.3333 μs |   22.9492 |       - |     - |   18.11 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |         **p:last-child** |   **102.990 μs** |  **39.6095 μs** |  **2.1711 μs** |   **66.5283** |       **-** |     **-** |   **51.15 KB** |
| AngleSharp |         p:last-child |   336.307 μs |  17.5237 μs |  0.9605 μs |   22.4609 |       - |     - |   17.57 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |      **p:nth-child(2n)** |   **585.353 μs** |  **15.8116 μs** |  **0.8667 μs** |  **166.0156** |       **-** |     **-** |  **127.86 KB** |
| AngleSharp |      p:nth-child(2n) | 1,202.662 μs |  62.5433 μs |  3.4282 μs |   27.3438 |       - |     - |   21.32 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |    **p:nth-child(2n+1)** |   **587.156 μs** |   **4.7381 μs** |  **0.2597 μs** |  **166.9922** |       **-** |     **-** |  **129.01 KB** |
| AngleSharp |    p:nth-child(2n+1) | 1,240.121 μs | 124.2185 μs |  6.8088 μs |   27.3438 |       - |     - |   21.34 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |    **p:nth-child(even)** |   **581.644 μs** |  **13.6114 μs** |  **0.7461 μs** |  **166.0156** |       **-** |     **-** |  **127.88 KB** |
| AngleSharp |    p:nth-child(even) | 1,250.388 μs | 354.2351 μs | 19.4168 μs |   27.3438 |       - |     - |    21.3 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |       **p:nth-child(n)** |   **625.485 μs** |  **33.0617 μs** |  **1.8122 μs** |  **225.5859** |       **-** |     **-** |  **173.52 KB** |
| AngleSharp |       p:nth-child(n) | 1,216.186 μs |  82.9399 μs |  4.5462 μs |   31.2500 |       - |     - |   25.32 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |     **p:nth-child(odd)** |   **591.015 μs** | **208.7191 μs** | **11.4406 μs** |  **166.9922** |       **-** |     **-** |     **129 KB** |
| AngleSharp |     p:nth-child(odd) | 1,190.050 μs |  45.8750 μs |  2.5146 μs |   27.3438 |       - |     - |    21.3 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |         **p:only-child** |   **140.605 μs** |  **16.9213 μs** |  **0.9275 μs** |  **102.0508** |       **-** |     **-** |   **78.48 KB** |
| AngleSharp |         p:only-child |   526.919 μs |  29.0772 μs |  1.5938 μs |   21.4844 |       - |     - |   17.07 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |         **ul .tocline2** |    **49.207 μs** |   **1.4457 μs** |  **0.0792 μs** |   **45.7153** |       **-** |     **-** |   **35.13 KB** |
| AngleSharp |         ul .tocline2 |   335.199 μs |   7.0508 μs |  0.3865 μs |   24.4141 |       - |     - |   19.04 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** | **ul.toc &gt; li.tocline2** |    **95.223 μs** |   **9.4951 μs** |  **0.5205 μs** |   **88.9893** |       **-** |     **-** |   **68.44 KB** |
| AngleSharp | ul.toc &gt; li.tocline2 |   343.599 μs |  32.2316 μs |  1.7667 μs |   23.4375 |       - |     - |   18.38 KB |
|            |                      |              |             |            |           |         |       |            |
|    **CsQuery** |   **ul.toc li.tocline2** |   **121.851 μs** |   **2.0593 μs** |  **0.1129 μs** |  **108.8867** |       **-** |     **-** |    **83.7 KB** |
| AngleSharp |   ul.toc li.tocline2 |   341.464 μs |  45.5889 μs |  2.4989 μs |   24.9023 |       - |     - |   19.38 KB |
