# Existing tokenizer trace

Measured compatibility entry point: `Utf8MutableDomBenchmark.BoundedUtf16Network4K()` in
`src/AngleSharp.Benchmarks/Utf8MutableDomBenchmark.cs`.

| Root | Full signature rows | Capped rows | Capture |
|---|---:|---:|---|
| `HtmlTokenizer.GetStructToken()` | 925 | 0 | fresh net10 Rig store |
| `HtmlTokenizerTokenSource.TryMoveNext()` | 926 | 0 | fresh net10 Rig store |

Both were captured with `tree --view full --sig --raw --plain --format llm --no-cache`. The current unbounded `rig tree` default produced no `budget-capped` or `depth-capped` rows.

## Compatibility call shape

```text
HtmlTokenizerTokenSource.TryMoveNext()
  HtmlTokenizer.GetStructToken()
    GetNextStructToken()
      BaseTokenizer.GetNext()
        contiguous decoded-char window where available
      HTML tokenizer state handler
      CharBuffer / StringBuilderBuffer token accumulation
      final StringOrMemory payload
  HtmlDomBuilder consumes the token
```

The bounded source decodes the 4 KiB byte window once, then the mature tokenizer scans UTF-16. Its token buffers often produce the final strings retained by the mutable DOM, so it does not need a second compatibility representation.
