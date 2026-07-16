# UTF-8 tokenizer investigation delta

## Scope correction

The native tokenizer itself is not 15-20% slower. A controlled layer decomposition on the same 126,853-byte fixture and 4 KiB segmentation produced:

| Layer | Mean | Allocated |
|---|---:|---:|
| Native borrowed tokenizer | 422.1 us | 2.08 KB |
| Native tokenizer with required yields | 471.2 us | 2.08 KB |
| Native structure-only `StructHtmlToken` adapter | 668.6 us | 3.20 KB |
| Native full adapter | 927.3 us | 250.72 KB |
| Mature bounded tokenizer | 689.2 us | 298.05 KB |

The borrowed byte kernel is about 39% faster than the mature tokenizer. Native becomes slower when it is forced through the existing mutable-DOM token contract. These layer numbers were captured during the direct-text experiment; use them to attribute costs, not to rank the final all-direct payload implementation.

Approximate native compatibility costs from the controlled decomposition:

- tree-feedback yield protocol: 49 us;
- structure/token-slot/pull adapter after yielding: 197 us;
- text and attribute payload materialization: 259 us.

Mode feedback itself was only about 16 us (`665.9 us` with feedback versus `649.4 us` without). A single input chunk was previously only about 11 us faster than 4 KiB chunks, so input segmentation is not the cause.

## Measured optimization

`Utf8HtmlTokenSource` originally decoded payloads into a document-lifetime pooled UTF-16 arena. `HtmlDomBuilder` then converted those memories to the strings retained by the mutable DOM. Decoding directly to final strings removes that extra representation.

Adjacent 20-iteration runs (the machine was noisy, so compare native absolute time first):

| Compatibility implementation | Oldschool | Native | Native delta |
|---|---:|---:|---:|
| Original UTF-16 arena | 2.427 ms | 2.809 ms | +15.7% |
| Direct final strings | 2.505 ms | 2.712 ms | +8.3% |

Native improved by about 3.5% (`2.809 -> 2.712 ms`) with allocation unchanged at 1.34 MB. The relative gap is less reliable because the oldschool baseline moved between runs.

The final adapter-only run was slower (`1.084 ms`, 284.85 KB) because it materializes final strings that this diagnostic benchmark immediately discards. In the real mutable-DOM lane those strings replace a later arena-to-string copy, so the end-to-end result above is the acceptance gate.

An arena-reset experiment did not improve runtime and was reverted.

## Ranked next work

1. Keep direct final-string decoding in the mutable-DOM adapter and validate it across broader parser tests.
2. If mutable-DOM parity is a product goal, let the builder consume native borrowed events directly. The measured ceiling is large: 422 us in the borrowed kernel versus 927 us through the full compatibility adapter.
3. If the existing `StructHtmlToken` contract must remain, profile and specialize its start/end-tag handoff. The structure-only adapter still costs about 197 us beyond mandatory yields.
4. Batch only work that does not require tree-construction feedback. Eliminating every yield is worth at most about 49 us on this fixture, so it is not the first target.
5. Do not expand the canonical-name table for this fixture: only 41 of 3,351 tag occurrences miss it (seven unique names).

The raw/query tokenizer remains the main win. Compatibility with the mutable DOM is a separate optimization problem and should not be used to judge the byte tokenizer kernel.
