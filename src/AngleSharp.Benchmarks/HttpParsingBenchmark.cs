// #nullable enable
// namespace AngleSharp.Benchmarks;
//
// using System;
// using System.Buffers;
// using System.Collections.Generic;
// using System.IO;
// using System.IO.Pipelines;
// using System.Linq;
// using System.Net.Http;
// using System.Runtime.CompilerServices;
// using System.Text;
// using System.Threading.Tasks;
// using BenchmarkDotNet.Attributes;
// using BenchmarkDotNet.Configs;
// using BenchmarkDotNet.Engines;
// using BenchmarkDotNet.Environments;
// using BenchmarkDotNet.Jobs;
// using Dom;
// using Html;
// using Html.Dom;
// using Html.Parser;
// using Html.Parser.Tokens.Struct;
// using Io;
// using Microsoft.Extensions.ObjectPool;
// using HttpMethod = System.Net.Http.HttpMethod;
//
// [Config(typeof(Config))]
// [MemoryDiagnoser]
// public class HttpParsingBenchmark
// {
//     private class Config : ManualConfig
//     {
//         public Config()
//         {
//             AddJob(Job.ShortRun
//                 .WithRuntime(CoreRuntime.Core80)
//                 .WithStrategy(RunStrategy.Monitoring)
//             );
//         }
//     }
//
//     public static string GetUrl(string id) => $"https://parcels.com/web/guest/trackdartresult?trackFor=0&trackNo={id}";
//
//     [GlobalSetup]
//     public void Setup()
//     {
//         HtmlEntityProvider.Resolver.GetSymbol("test");
//         MimeTypeNames.FromExtension(".txt");
//     }
//
//     [Params("10000000000000", "20000000000000")]
//     public string? Id { get; set; }
//
//     [Benchmark(Baseline = true)]
//     public async Task<List<Event>?> Old()
//     {
//         var events = await ParseSiteOld(Id!);
//         if (events != null)
//         {
//             foreach (var @event in events)
//             {
//                 Console.WriteLine($"{@event.Date} | {@event.Status} | {@event.Location}");
//             }
//         }
//
//         return events;
//     }
//
//     [Benchmark]
//     public async Task<List<Event>?> CustomClientLevel()
//     {
//         var events = await UserLevelParser.Parse(Id!);
//         if (events != null)
//         {
//             foreach (var @event in events)
//             {
//                 Console.WriteLine($"{@event.Date} | {@event.Status} | {@event.Location}");
//             }
//         }
//
//         return events;
//     }
//
//     [Benchmark]
//     public async Task<List<Event>?> CustomLibLevel()
//     {
//         var events = await ParseSiteCustom(Id!);
//         if (events != null)
//         {
//             foreach (var @event in events)
//             {
//                 Console.WriteLine($"{@event.Date} | {@event.Status} | {@event.Location}");
//             }
//         }
//
//         return events;
//     }
//
//     private static readonly SelectorCache SelectorCache = new();
//
//     private static readonly HttpClient Client = new HttpClient(new SocketsHttpHandler
//     {
//         PooledConnectionLifetime = TimeSpan.FromMinutes(5)
//     });
//
//     private static readonly IBrowsingContext Context = BrowsingContext.New(Configuration.Default);
//
//     private static ReadOnlySpan<Char> ID => "id";
//     private static ReadOnlySpan<Char> CLASS => "class";
//     private static HtmlParserOptions Options = new()
//     {
//         IsStrictMode = false,
//         IsScripting = false,
//         IsNotConsumingCharacterReferences = true,
//         IsNotSupportingFrames = true,
//         IsSupportingProcessingInstructions = false,
//         IsEmbedded = false,
//
//         IsKeepingSourceReferences = false,
//         IsPreservingAttributeNames = false,
//         IsAcceptingCustomElementsEverywhere = false,
//
//         SkipScriptText = true,
//         SkipRawText = true,
//         SkipDataText = false,
//         SkipComments = true,
//         SkipPlaintext = true,
//         SkipCDATA = true,
//         SkipRCDataText = true,
//         SkipProcessingInstructions = true,
//         DisableElementPositionTracking = true,
//         ShouldEmitAttribute = static (ref StructHtmlToken _, ReadOnlyMemory<Char> n) =>
//         {
//             var s = n.Span;
//             return s.Length switch
//             {
//                 2 => s.SequenceEqual(ID),
//                 5 => s.SequenceEqual(CLASS),
//                 _ => false
//             };
//         },
//     };
//
//     static readonly HtmlParser Parser = new(Options, Context);
//
//     private static async Task<List<Event>?> ParseSiteCustom(String id)
//     {
//         var request = new HttpRequestMessage(HttpMethod.Get, GetUrl(id));
//         using var lease = await Client.DownloadChars(request);
//
//         var filter = new OnlyElementWithId("div", $"AWB{id}");
//         using var doc = Parser.ParseWithFilter(lease.Data, filter.Loop);
//
//         var div = doc.QuerySelector($"div#AWB{id}");
//         var table = div?.QuerySelector<IHtmlTableElement>($"div#SCAN{id} table");
//         var rows = table?.Bodies.FirstOrDefault()?
//             .Rows
//             .Select(row => row.Cells.Select(it => it.GetText().Trim()).ToList())
//             .SkipLast(1);
//
//         if (rows == null)
//         {
//             if (doc.QuerySelector("#errorDetails > div.alert", SelectorCache)?.GetText()
//                     .Contains("Records Not Found", StringComparison.OrdinalIgnoreCase) == true)
//             {
//                 Console.WriteLine("Not found");
//                 return null;
//             }
//
//             Console.WriteLine("Parser error");
//             return null;
//         }
//
//         var history = rows
//             .Where(it => !String.IsNullOrWhiteSpace(it.ElementAtOrDefault(1)))
//             .Select(row =>
//             {
//                 var location = row.ElementAtOrDefault(0);
//                 var status = row.ElementAtOrDefault(1);
//                 var rawDate = $"{row.ElementAtOrDefault(2)} {row.ElementAtOrDefault(3)}";
//
//                 return new Event
//                 {
//                     Location = location,
//                     Status = status,
//                     Date = rawDate,
//                 };
//             })
//             .ToList();
//
//         var stage =
//             history.FirstOrDefault()?.Status?.Contains("Delivered", StringComparison.OrdinalIgnoreCase) == true
//                 ? "Stage.Delivered"
//                 : "Stage.Transit";
//
//         Console.WriteLine(stage);
//
//         return history;
//     }
//     private static async Task<List<Event>?> ParseSiteOld(String id)
//     {
//         var request = new HttpRequestMessage(HttpMethod.Get, GetUrl(id));
//
//         var response = await Client.SendAsync(request);
//         var html = await response.Content.ReadAsStringAsync();
//
//         var parser = new HtmlParser();
//         var doc = parser.ParseDocument(html);
//
//         var div = doc.QuerySelector($"div#AWB{id}");
//         var table = div?.QuerySelector<IHtmlTableElement>($"div#SCAN{id} table");
//         var rows = table?.Bodies.FirstOrDefault()?
//             .Rows
//             .Select(row => row.Cells.Select(it => it.TextContent.Trim()).ToList())
//             .SkipLast(1)
//             .ToList();
//
//         if (rows == null)
//         {
//             if (doc.QuerySelector("#errorDetails > div.alert")?.TextContent
//                     .Contains("Records Not Found", StringComparison.OrdinalIgnoreCase) == true)
//             {
//                 Console.WriteLine("Not found");
//                 return null;
//             }
//
//             Console.WriteLine("Parser error");
//             return null;
//         }
//
//         var history = rows
//             .Where(it => !String.IsNullOrWhiteSpace(it.ElementAtOrDefault(1)))
//             .Select(row =>
//             {
//                 var rawDate = $"{row.ElementAtOrDefault(2)} {row.ElementAtOrDefault(3)}";
//                 var status = row.ElementAtOrDefault(1)!;
//                 var location = row.ElementAtOrDefault(0);
//
//                 return new Event
//                 {
//                     Location = location,
//                     Status = status,
//                     Date = rawDate,
//                 };
//             }).ToList();
//
//         var stage =
//             history.FirstOrDefault()?.Status?.Contains("Delivered", StringComparison.OrdinalIgnoreCase) == true
//                 ? "Stage.Delivered"
//                 : "Stage.Transit";
//
//         Console.WriteLine(stage);
//
//         return history;
//     }
//
//     private static ClassParser UserLevelParser = new();
//
//     public class ClassParser
//     {
//         private static readonly HttpClient Client = new HttpClient(new SocketsHttpHandler()
//         {
//             PooledConnectionLifetime = TimeSpan.FromMinutes(5)
//         });
//
//         private static readonly SelectorCache Selectors = new();
//
//         public async Task<List<Event>?> Parse(string id)
//         {
//             var client = Client;
//
//             var request = new HttpRequestMessage(HttpMethod.Get, GetUrl(id));
//             using var postResponse = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
//             await using var htmlStream = await postResponse.Content.ReadAsStreamAsync();
//
//             var html = await FindResultsDivHtml(htmlStream, id);
//
//             if (html == null)
//             {
//                 Console.WriteLine("Parser error");
//                 return null!;
//             }
//
//             var htmlParser = new HtmlParser();
//             var doc = htmlParser.ParseDocument(html);
//
//             var div = doc.QuerySelector($"div#AWB{id}");
//             var table = div?.QuerySelector<IHtmlTableElement>($"div#SCAN{id} table");
//             var rawRows = table?.Bodies.FirstOrDefault()?.Rows
//                 .Select(row => row.Cells.Select(it => it.GetText().Trim()).ToList())
//                 .SkipLast(1)
//                 .ToList();
//
//             if (rawRows == null)
//             {
//                 if (doc.QuerySelector("#errorDetails > div.alert", Selectors)?.GetText()
//                         .Contains("Records Not Found", StringComparison.OrdinalIgnoreCase) == true)
//                 {
//                     Console.WriteLine("Not found");
//                 }
//
//                 Console.WriteLine("Error");
//
//                 return null!;
//             }
//
//             var history = rawRows
//                 .Where(it => !string.IsNullOrWhiteSpace(it.ElementAtOrDefault(1)))
//                 .Select(row =>
//                 {
//                     var rawDate = $"{row.ElementAtOrDefault(2)} {row.ElementAtOrDefault(3)}";
//                     var status = row.ElementAtOrDefault(1)!;
//                     var location = row.ElementAtOrDefault(0)!;
//
//                     return new Event
//                     {
//                         Location = location,
//                         Status = status,
//                         Date = rawDate,
//                     };
//                 }).ToList();
//
//             var stage =
//                 history.FirstOrDefault()?.Status?.Contains("Delivered", StringComparison.OrdinalIgnoreCase) == true
//                     ? "Stage.Delivered"
//                     : "Stage.Transit";
//
//             Console.WriteLine(stage);
//
//             return history;
//         }
//
//         private static async Task<string?> FindResultsDivHtml(Stream stream, string id)
//         {
//             var start = $"<div id=\"AWB{id}\"";
//             var end = $"<!-- AWB{id} -->";
//
//             using var state = new CaptureLinesBetween(
//                 MatchStart: line => line.IndexOf(start) >= 0, IncludeStart: true,
//                 MatchEnd: line => line.IndexOf(end) >= 0, IncludeEnd: false,
//                 MaxLinesToCollect: 1000);
//
//             await stream.ProcessLines(state: state);
//
//             return state.Success ? state.Accumulator.ToString() : null;
//         }
//     }
// }
//
// public class Event
// {
//     public String? Date { get; set; }
//     public String? Status { get; set; }
//     public String? Location { get; set; }
// }
//
// public interface IParserState
// {
//     bool Success { get; }
//     bool CanContinue { get; }
//     void Visit(ReadOnlySpan<byte> bytes);
// }
//
// public delegate bool MatchSpan(ReadOnlySpan<char> span);
//
// public sealed class CaptureLinesBetween(
//     MatchSpan MatchStart, bool IncludeStart,
//     MatchSpan MatchEnd, bool IncludeEnd,
//     int MaxLinesToCollect = 1000) : IParserState, IDisposable
// {
//     public MatchSpan MatchStart { get; } = MatchStart;
//     public bool IncludeStart { get; } = IncludeStart;
//
//     public MatchSpan MatchEnd { get; } = MatchEnd;
//     public bool IncludeEnd { get; } = IncludeEnd;
//
//     public int MaxLines { get; } = MaxLinesToCollect;
//
//     public int LinesVisited { get; private set; }
//     public bool FoundStart { get; private set; }
//     public bool FoundEnd { get; private set; }
//
//     public StringBuilder Accumulator { get; } = StringBuilders.LargeStringBuilderPool.Get();
//
//     public bool Success => FoundStart && FoundEnd;
//     public bool CanContinue => !Success && LinesVisited <= MaxLines;
//
//     public void Visit(ReadOnlySpan<byte> bytes)
//     {
//         bytes = bytes.Trim(" \t\r\n"u8);
//
//         if (bytes.Length == 0)
//             return;
//
//         if (!FoundStart)
//         {
//             bytes.AsChars(static (line, self) =>
//             {
//                 if (self.MatchStart(line))
//                 {
//                     self.FoundStart = true;
//                     if (self.IncludeStart)
//                     {
//                         self.Accumulator.Append(line);
//                         self.Accumulator.AppendLine();
//                     }
//
//                     self.LinesVisited++;
//                 }
//             }, this);
//         }
//         else if (FoundStart && !FoundEnd && LinesVisited <= MaxLines)
//         {
//             LinesVisited++;
//
//             bytes.AsChars(static (line, self) =>
//             {
//                 if (self.MatchEnd(line))
//                 {
//                     self.FoundEnd = true;
//                 }
//
//                 if (self is { IncludeEnd: false, FoundEnd: true }) return;
//
//                 self.Accumulator.Append(line);
//                 self.Accumulator.AppendLine();
//             }, this);
//         }
//     }
//
//     public void Dispose()
//     {
//         StringBuilders.LargeStringBuilderPool.Return(Accumulator);
//     }
// }
//
// public delegate void LineAcceptor<in T>(ReadOnlySpan<char> line, T state);
//
// public static class StreamLineReader
// {
//     [AsyncMethodBuilder(typeof(PoolingAsyncValueTaskMethodBuilder))]
//     public static async ValueTask ProcessLines(this Stream input, IParserState state)
//     {
//         var reader = PipeReader.Create(input, new StreamPipeReaderOptions(leaveOpen: true));
//         while (true)
//         {
//             ReadResult result = await reader.ReadAsync();
//             ReadOnlySequence<byte> buffer = result.Buffer;
//
//             while (TryReadLine(ref buffer, out ReadOnlySequence<byte> line))
//             {
//                 ProcessLine(line, state);
//
//                 if (state.Success || !state.CanContinue)
//                     return;
//             }
//
//             reader.AdvanceTo(buffer.Start, buffer.End);
//
//             if (result.IsCompleted)
//             {
//                 break;
//             }
//         }
//
//         await reader.CompleteAsync();
//         return;
//
//         static void ProcessLine(ReadOnlySequence<byte> line, IParserState state)
//         {
//             if (line.IsSingleSegment)
//             {
//                 state.Visit(line.FirstSpan);
//             }
//             else if (line.Length < 256)
//             {
//                 Span<byte> span = stackalloc byte[(int)line.Length];
//                 line.CopyTo(span);
//                 state.Visit(span);
//             }
//             else if (line.Length < 10 * 1024)
//             {
//                 using var mem = MemoryPool<byte>.Shared.Rent((int)line.Length);
//                 line.CopyTo(mem.Memory.Span);
//                 var slice = mem.Memory.Span.Slice(0, (int)line.Length);
//                 state.Visit(slice);
//             }
//         }
//
//         static bool TryReadLine(ref ReadOnlySequence<byte> buffer, out ReadOnlySequence<byte> line)
//         {
//             SequencePosition? position = buffer.PositionOf((byte)'\n');
//
//             if (position == null)
//             {
//                 line = default;
//                 return false;
//             }
//
//             line = buffer.Slice(0, position.Value);
//             buffer = buffer.Slice(buffer.GetPosition(1, position.Value));
//             return true;
//         }
//     }
//
//     public static void AsChars<T>(this ReadOnlySpan<byte> raw, LineAcceptor<T> func, T state)
//     {
//         var l = Encoding.UTF8.GetMaxCharCount(raw.Length);
//         if (l <= 256)
//         {
//             Span<char> destination = stackalloc char[l];
//             int w = Encoding.UTF8.GetChars(raw, destination);
//             Span<char> line = destination.Slice(0, w);
//             func(line, state);
//         }
//         else if (l < 10 * 1024)
//         {
//             using IMemoryOwner<char> mem = MemoryPool<char>.Shared.Rent(l);
//             Span<char> destination = mem.Memory.Span;
//             int w = Encoding.UTF8.GetChars(raw, destination);
//             Span<char> line = destination.Slice(0, w);
//             func(line, state);
//         }
//     }
// }
//
// public class StringBuilders
// {
//     public static readonly ObjectPool<StringBuilder> LargeStringBuilderPool =
//         new DefaultObjectPoolProvider { MaximumRetained = 16 }
//             .CreateStringBuilderPool(2048, 150000);
//
//     public static readonly ObjectPool<StringBuilder> MediumStringBuilderPool =
//         new DefaultObjectPoolProvider { MaximumRetained = 16 }
//             .CreateStringBuilderPool(512, 2048);
// }