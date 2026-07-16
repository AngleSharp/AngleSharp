# Native UTF-8 tokenizer trace

Measured compatibility entry point: `Utf8MutableDomBenchmark.NativeUtf8Network4K()` in
`src/AngleSharp.Benchmarks/Utf8MutableDomBenchmark.cs`.

Two full graphs are required. The tokenizer graph describes byte parsing; the adapter graph includes the work needed to feed AngleSharp's existing token-at-a-time tree constructor.

| Root | Full signature rows | Capped rows | Capture |
|---|---:|---:|---|
| `Utf8HtmlTokenizer.Write(ReadOnlySpan<byte>)` | 398 | 0 | fresh net10 Rig store |
| `Utf8HtmlTokenSource.TryMoveNext()` | 401 | 0 | fresh net10 Rig store |

Both were captured from a `net10.0` Rig index with `tree --view full --sig --raw --plain --format llm --no-cache`. `rig tree` is unbounded by default; neither output contains `budget-capped` or `depth-capped` rows.

## Compatibility call shape

```text
Utf8HtmlTokenSource.TryMoveNext()
  ReleaseCurrent()
  Utf8HtmlTokenizer.WriteUntilYield(ReadOnlySpan<byte>)
    WriteCore(...)
      WriteValidUtf8(...)
        byte-state machine and bulk scans
        borrowed sink callbacks
          Text / StartTag / Attribute / EndTag
          Utf8HtmlTokenSource builds StructHtmlToken
          RequestYield() when tree-construction feedback is required
  Current -> ref StructHtmlToken
  HtmlDomBuilder consumes the token
```

The native tokenizer kernel keeps input and temporary names as borrowed UTF-8 spans. Compatibility mode adds yielding, token slots, UTF-8-to-UTF-16 payload conversion, and pull-style delivery to the existing builder.
