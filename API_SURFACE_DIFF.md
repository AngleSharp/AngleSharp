# AngleSharp public API surface diff

Generated: 2026-07-16 12:30:40 +01:00

Candidate: `C:\work\AngleSharp-core-utf8-tokenizer\src\AngleSharp\bin\Release\net8.0\AngleSharp.dll`

The candidate is the current working tree. It includes the unstaged generic custom-document parser overload; that entry is marked by appearing in the effective diff even though it is not part of the staged tokenizer commit.

| Baseline | Added types | Added members | Removed types | Removed members |
|---|---:|---:|---:|---:|
| origin/main | 13 | 100 | 0 | 0 |
| Rebased byte-source base | 11 | 82 | 0 | 0 |

ApiCompat currently reports **13 added public types versus `origin/main`**, not 14. The previously public `IHtmlTokenSource` is absent because the staged minimization makes it internal. The effective tokenizer follow-up adds 11 public UTF-8 types; the generic parser overload is a separate unstaged member.

## Cumulative diff versus `origin/main`

- Added types: 13
- Added members: 100
- Removed types: 0
- Removed members: 0

### `AngleSharp.Html.Parser.HtmlParser`

```diff
+ public System.Threading.Tasks.Task<AngleSharp.Html.Dom.IHtmlDocument> ParseDocumentAsync(System.IO.Stream source, AngleSharp.Html.Parser.HtmlStreamSourceMode sourceMode, System.Text.Encoding encoding, System.Threading.CancellationToken cancel);
+ public System.Threading.Tasks.Task<TDocument> ParseDocumentAsync<TDocument, TElement>(System.IO.Stream source, AngleSharp.Html.Parser.HtmlStreamSourceMode sourceMode, System.Text.Encoding encoding, AngleSharp.Html.Parser.TokenizerMiddleware middleware, System.Threading.CancellationToken cancel);
```

### `AngleSharp.Html.Parser.HtmlStreamSourceMode`

```diff
+ public static const AngleSharp.Html.Parser.HtmlStreamSourceMode Buffered;
+ public static const AngleSharp.Html.Parser.HtmlStreamSourceMode Streaming;
+ public enum AngleSharp.Html.Parser.HtmlStreamSourceMode
```

### `AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit`

```diff
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit BufferedTokenBytes;
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit InputBytes;
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit NestingDepth;
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit QueryCaptureBytes;
+ public enum AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit
```

### `AngleSharp.Html.Parser.Utf8.HtmlStreamingLimitExceededException`

```diff
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit Limit { get; }
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimitExceededException(AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit limit, long allowed, long observed);
+ public long Allowed { get; }
+ public long Observed { get; }
+ public class AngleSharp.Html.Parser.Utf8.HtmlStreamingLimitExceededException
```

### `AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits`

```diff
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits Default { get; }
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits Unlimited { get; }
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits(int maximumBufferedTokenBytes, int maximumNestingDepth, long maximumInputBytes, long maximumQueryCaptureBytes);
+ public int MaximumBufferedTokenBytes { get; }
+ public int MaximumNestingDepth { get; }
+ public long MaximumInputBytes { get; }
+ public long MaximumQueryCaptureBytes { get; }
+ public static const int DefaultMaximumBufferedTokenBytes;
+ public static const int DefaultMaximumNestingDepth;
+ public static const long DefaultMaximumInputBytes;
+ public static const long DefaultMaximumQueryCaptureBytes;
+ public class AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits
```

### `AngleSharp.Html.Parser.Utf8.IOptimizedUtf8HtmlTokenSink`

```diff
+ public bool WantsAttribute(System.ReadOnlySpan<byte> name);
+ public void EndTag(System.ReadOnlySpan<byte> name, ulong hash);
+ public void StartTag(System.ReadOnlySpan<byte> name, ulong hash);
+ public interface AngleSharp.Html.Parser.Utf8.IOptimizedUtf8HtmlTokenSink
```

### `AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink`

```diff
+ public void Attribute(System.ReadOnlySpan<byte> name, System.ReadOnlySpan<byte> value);
+ public void Comment(System.ReadOnlySpan<byte> utf8);
+ public void Doctype(in AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken token);
+ public void Doctype(System.ReadOnlySpan<byte> utf8);
+ public void EndOfFile();
+ public void EndTag(System.ReadOnlySpan<byte> name);
+ public void StartTag(System.ReadOnlySpan<byte> name);
+ public void StartTagEnd(bool selfClosing);
+ public void Text(System.ReadOnlySpan<byte> utf8);
+ public interface AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink
```

### `AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken(System.ReadOnlySpan<byte> name, System.ReadOnlySpan<byte> publicIdentifier, bool isPublicIdentifierMissing, System.ReadOnlySpan<byte> systemIdentifier, bool isSystemIdentifierMissing, bool isQuirksForced);
+ public bool IsPublicIdentifierMissing { get; }
+ public bool IsQuirksForced { get; }
+ public bool IsSystemIdentifierMissing { get; }
+ public System.ReadOnlySpan<byte> Name { get; }
+ public System.ReadOnlySpan<byte> PublicIdentifier { get; }
+ public System.ReadOnlySpan<byte> SystemIdentifier { get; }
+ public struct AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits limits);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics stateMetrics, AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits limits, bool countInputBytes);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics stateMetrics);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters Counters { get; }
+ public bool IsAcceptingCharacterData { get; set; }
+ public bool IsModeControlledExternally { get; set; }
+ public int StateCount { get; }
+ public static System.Threading.Tasks.ValueTask<AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters> TokenizeAsync(System.IO.Pipelines.PipeReader reader, AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, System.Threading.CancellationToken cancellationToken, AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits limits);
+ public System.Collections.Generic.IReadOnlyList<AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric> GetStateMetrics();
+ public void Complete();
+ public void EnterCDataSection();
+ public void SetMode(AngleSharp.Html.Parser.HtmlParseMode mode, string contextTagName);
+ public void Write(System.ReadOnlyMemory<byte> utf8);
+ public void Write(System.ReadOnlySpan<byte> utf8);
+ public class AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters(long BytesConsumed, long InputSegments, long Reconsumes, int MaximumSourceLookbehind, int MaximumBufferedTokenBytes);
+ public bool Equals(AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters other);
+ public bool Equals(object obj);
+ public int GetHashCode();
+ public int MaximumBufferedTokenBytes { get; set; }
+ public int MaximumSourceLookbehind { get; set; }
+ public long BytesConsumed { get; set; }
+ public long InputSegments { get; set; }
+ public long Reconsumes { get; set; }
+ public string ToString();
+ public void Deconstruct(out long BytesConsumed, out long InputSegments, out long Reconsumes, out int MaximumSourceLookbehind, out int MaximumBufferedTokenBytes);
+ public struct AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric(string State, long ByteVisits, long Runs, int MaximumRunLength);
+ public bool Equals(AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric other);
+ public bool Equals(object obj);
+ public int GetHashCode();
+ public int MaximumRunLength { get; set; }
+ public long ByteVisits { get; set; }
+ public long Runs { get; set; }
+ public string State { get; set; }
+ public string ToString();
+ public void Deconstruct(out string State, out long ByteVisits, out long Runs, out int MaximumRunLength);
+ public struct AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics(int stateCount);
+ public System.Collections.Generic.IReadOnlyList<AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric> Snapshot(System.Collections.Generic.IReadOnlyList<string> stateNames);
+ public void Record(int state, int byteCount);
+ public class AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics
```

### `AngleSharp.Html.Parser.Utf8.Utf8NameHash`

```diff
+ public static const ulong Offset;
+ public static ulong Append(ulong hash, byte value);
+ public static ulong Append(ulong hash, System.ReadOnlySpan<byte> value);
+ public static ulong Compute(System.ReadOnlySpan<byte> value);
+ public class AngleSharp.Html.Parser.Utf8.Utf8NameHash
```
### `AngleSharp.Text.ReadOnlyByteTextSource`

```diff
+ public AngleSharp.Common.StringOrMemory ReadMemory(int characters);
+ public AngleSharp.Text.ReadOnlyByteTextSource(System.ReadOnlyMemory<byte> bytes, System.Text.Encoding encoding);
+ public AngleSharp.Text.ReadOnlyByteTextSource(System.ReadOnlyMemory<byte> bytes);
+ public bool TryGetContentLength(out int length);
+ public char Item[int index] { get; }
+ public char ReadCharacter();
+ public int Index { get; set; }
+ public int Length { get; }
+ public string ReadCharacters(int characters);
+ public string Text { get; }
+ public System.Text.Encoding CurrentEncoding { get; set; }
+ public System.Threading.Tasks.Task PrefetchAllAsync(System.Threading.CancellationToken cancellationToken);
+ public System.Threading.Tasks.Task PrefetchAsync(int length, System.Threading.CancellationToken cancellationToken);
+ public void Dispose();
+ public class AngleSharp.Text.ReadOnlyByteTextSource
```

### `AngleSharp.Text.TextSource`

```diff
+ public AngleSharp.Text.TextSource(AngleSharp.Text.ReadOnlyByteTextSource source);
```

## Effective diff versus rebased byte-source base

- Added types: 11
- Added members: 82
- Removed types: 0
- Removed members: 0

### `AngleSharp.Html.Parser.HtmlParser`

```diff
+ public System.Threading.Tasks.Task<TDocument> ParseDocumentAsync<TDocument, TElement>(System.IO.Stream source, AngleSharp.Html.Parser.HtmlStreamSourceMode sourceMode, System.Text.Encoding encoding, AngleSharp.Html.Parser.TokenizerMiddleware middleware, System.Threading.CancellationToken cancel);
```

### `AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit`

```diff
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit BufferedTokenBytes;
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit InputBytes;
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit NestingDepth;
+ public static const AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit QueryCaptureBytes;
+ public enum AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit
```

### `AngleSharp.Html.Parser.Utf8.HtmlStreamingLimitExceededException`

```diff
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit Limit { get; }
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimitExceededException(AngleSharp.Html.Parser.Utf8.HtmlStreamingLimit limit, long allowed, long observed);
+ public long Allowed { get; }
+ public long Observed { get; }
+ public class AngleSharp.Html.Parser.Utf8.HtmlStreamingLimitExceededException
```

### `AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits`

```diff
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits Default { get; }
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits Unlimited { get; }
+ public AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits(int maximumBufferedTokenBytes, int maximumNestingDepth, long maximumInputBytes, long maximumQueryCaptureBytes);
+ public int MaximumBufferedTokenBytes { get; }
+ public int MaximumNestingDepth { get; }
+ public long MaximumInputBytes { get; }
+ public long MaximumQueryCaptureBytes { get; }
+ public static const int DefaultMaximumBufferedTokenBytes;
+ public static const int DefaultMaximumNestingDepth;
+ public static const long DefaultMaximumInputBytes;
+ public static const long DefaultMaximumQueryCaptureBytes;
+ public class AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits
```

### `AngleSharp.Html.Parser.Utf8.IOptimizedUtf8HtmlTokenSink`

```diff
+ public bool WantsAttribute(System.ReadOnlySpan<byte> name);
+ public void EndTag(System.ReadOnlySpan<byte> name, ulong hash);
+ public void StartTag(System.ReadOnlySpan<byte> name, ulong hash);
+ public interface AngleSharp.Html.Parser.Utf8.IOptimizedUtf8HtmlTokenSink
```

### `AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink`

```diff
+ public void Attribute(System.ReadOnlySpan<byte> name, System.ReadOnlySpan<byte> value);
+ public void Comment(System.ReadOnlySpan<byte> utf8);
+ public void Doctype(in AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken token);
+ public void Doctype(System.ReadOnlySpan<byte> utf8);
+ public void EndOfFile();
+ public void EndTag(System.ReadOnlySpan<byte> name);
+ public void StartTag(System.ReadOnlySpan<byte> name);
+ public void StartTagEnd(bool selfClosing);
+ public void Text(System.ReadOnlySpan<byte> utf8);
+ public interface AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink
```

### `AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken(System.ReadOnlySpan<byte> name, System.ReadOnlySpan<byte> publicIdentifier, bool isPublicIdentifierMissing, System.ReadOnlySpan<byte> systemIdentifier, bool isSystemIdentifierMissing, bool isQuirksForced);
+ public bool IsPublicIdentifierMissing { get; }
+ public bool IsQuirksForced { get; }
+ public bool IsSystemIdentifierMissing { get; }
+ public System.ReadOnlySpan<byte> Name { get; }
+ public System.ReadOnlySpan<byte> PublicIdentifier { get; }
+ public System.ReadOnlySpan<byte> SystemIdentifier { get; }
+ public struct AngleSharp.Html.Parser.Utf8.Utf8DoctypeToken
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits limits);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics stateMetrics, AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits limits, bool countInputBytes);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics stateMetrics);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer(AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink);
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters Counters { get; }
+ public bool IsAcceptingCharacterData { get; set; }
+ public bool IsModeControlledExternally { get; set; }
+ public int StateCount { get; }
+ public static System.Threading.Tasks.ValueTask<AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters> TokenizeAsync(System.IO.Pipelines.PipeReader reader, AngleSharp.Html.Parser.Utf8.IUtf8HtmlTokenSink sink, System.Threading.CancellationToken cancellationToken, AngleSharp.Html.Parser.Utf8.HtmlStreamingLimits limits);
+ public System.Collections.Generic.IReadOnlyList<AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric> GetStateMetrics();
+ public void Complete();
+ public void EnterCDataSection();
+ public void SetMode(AngleSharp.Html.Parser.HtmlParseMode mode, string contextTagName);
+ public void Write(System.ReadOnlyMemory<byte> utf8);
+ public void Write(System.ReadOnlySpan<byte> utf8);
+ public class AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizer
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters(long BytesConsumed, long InputSegments, long Reconsumes, int MaximumSourceLookbehind, int MaximumBufferedTokenBytes);
+ public bool Equals(AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters other);
+ public bool Equals(object obj);
+ public int GetHashCode();
+ public int MaximumBufferedTokenBytes { get; set; }
+ public int MaximumSourceLookbehind { get; set; }
+ public long BytesConsumed { get; set; }
+ public long InputSegments { get; set; }
+ public long Reconsumes { get; set; }
+ public string ToString();
+ public void Deconstruct(out long BytesConsumed, out long InputSegments, out long Reconsumes, out int MaximumSourceLookbehind, out int MaximumBufferedTokenBytes);
+ public struct AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerCounters
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric(string State, long ByteVisits, long Runs, int MaximumRunLength);
+ public bool Equals(AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric other);
+ public bool Equals(object obj);
+ public int GetHashCode();
+ public int MaximumRunLength { get; set; }
+ public long ByteVisits { get; set; }
+ public long Runs { get; set; }
+ public string State { get; set; }
+ public string ToString();
+ public void Deconstruct(out string State, out long ByteVisits, out long Runs, out int MaximumRunLength);
+ public struct AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric
```

### `AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics`

```diff
+ public AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics(int stateCount);
+ public System.Collections.Generic.IReadOnlyList<AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetric> Snapshot(System.Collections.Generic.IReadOnlyList<string> stateNames);
+ public void Record(int state, int byteCount);
+ public class AngleSharp.Html.Parser.Utf8.Utf8HtmlTokenizerStateMetrics
```

### `AngleSharp.Html.Parser.Utf8.Utf8NameHash`

```diff
+ public static const ulong Offset;
+ public static ulong Append(ulong hash, byte value);
+ public static ulong Append(ulong hash, System.ReadOnlySpan<byte> value);
+ public static ulong Compute(System.ReadOnlySpan<byte> value);
+ public class AngleSharp.Html.Parser.Utf8.Utf8NameHash
```

