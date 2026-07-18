#pragma warning disable CS1591 // Experimental implementation detail; shape is intentionally unsettled.

using System;
using System.Buffers;
using System.Text;

namespace AngleSharp.Html.Parser.Utf8;

public enum Utf8InputContract : byte
{
    /// <summary>Validates arbitrary input and replaces malformed UTF-8 with U+FFFD.</summary>
    ArbitraryBytes,

    /// <summary>
    /// Skips bulk validation because the producer guarantees well-formed UTF-8. Supplying malformed input violates the
    /// contract and produces unspecified token payloads.
    /// </summary>
    WellFormedUtf8,
}

/// <summary>
/// Owns UTF-8 framing, validation, malformed-input replacement, and source-byte accounting before bytes reach the
/// HTML tokenizer state machine.
/// </summary>
internal struct Utf8RuneValidator
{
    private readonly Int64 _maximumInputBytesAllowed;
    private readonly Utf8InputContract _contract;
    private UInt32 _carry;
    private Int64 _bytesConsumed;
    private Int32 _validatedPrefixLength;
    private Byte _carryLength;

    internal Utf8RuneValidator(Int64 maximumInputBytesAllowed, Utf8InputContract contract)
    {
        _maximumInputBytesAllowed = maximumInputBytesAllowed;
        _contract = contract;
    }

    internal readonly Int64 BytesConsumed => _bytesConsumed;

    internal Int32 Write(
        Utf8HtmlTokenizer tokenizer,
        ReadOnlySpan<Byte> utf8,
        Boolean yieldOnRequest
    )
    {
        var previousBytesConsumed = _bytesConsumed;
        var observedInputBytes = SaturatingAdd(_bytesConsumed, utf8.Length);
        if (observedInputBytes > _maximumInputBytesAllowed)
        {
            throw new HtmlStreamingLimitExceededException(
                HtmlStreamingLimit.InputBytes,
                _maximumInputBytesAllowed,
                observedInputBytes
            );
        }

        _bytesConsumed = observedInputBytes;
        var index = 0;
        if (_carryLength != 0)
        {
            index = DrainCarry(tokenizer, utf8, yieldOnRequest);
            if (_carryLength != 0)
            {
                if (yieldOnRequest && tokenizer.IsYieldRequested)
                {
                    _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                }
                return index;
            }
            if (yieldOnRequest && tokenizer.IsYieldRequested)
            {
                _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                return index;
            }
        }

        if (_contract == Utf8InputContract.WellFormedUtf8)
        {
            return WriteWellFormed(tokenizer, utf8, index, previousBytesConsumed, yieldOnRequest);
        }

        while (index < utf8.Length)
        {
            if (_validatedPrefixLength != 0)
            {
                var available = Math.Min(_validatedPrefixLength, utf8.Length - index);
                var consumed = tokenizer.WriteNormalizedUtf8(
                    utf8.Slice(index, available),
                    yieldOnRequest
                );
                _validatedPrefixLength -= consumed;
                index += consumed;
                if (consumed != available)
                {
                    _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                    return index;
                }
                if (yieldOnRequest && tokenizer.IsYieldRequested)
                {
                    _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                    return index;
                }
                continue;
            }

            var remaining = utf8[index..];
            var nonAscii = remaining.IndexOfAnyExceptInRange((Byte)0x00, (Byte)0x7F);
            if (nonAscii < 0)
            {
                nonAscii = remaining.Length;
            }
            if (nonAscii != 0)
            {
                _validatedPrefixLength = nonAscii;
                continue;
            }

            var remainingUtf8 = utf8[index..];
            var completeLength = CompleteUtf8PrefixLength(remainingUtf8);
            if (
                completeLength != 0
                && System.Text.Unicode.Utf8.IsValid(remainingUtf8[..completeLength])
            )
            {
                _validatedPrefixLength = completeLength;
                continue;
            }

            if (completeLength != 0)
            {
                var malformedConsumed = WriteMalformedUtf8(
                    tokenizer,
                    remainingUtf8[..completeLength],
                    yieldOnRequest
                );
                index += malformedConsumed;
                if (
                    malformedConsumed != completeLength
                    || (yieldOnRequest && tokenizer.IsYieldRequested)
                )
                {
                    _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                    return index;
                }

                if (completeLength != remainingUtf8.Length)
                {
                    SaveCarry(remainingUtf8[completeLength..]);
                    index = utf8.Length;
                }
                continue;
            }

            SaveCarry(remainingUtf8);
            index = utf8.Length;
        }

        return index;
    }

    private Int32 DrainCarry(
        Utf8HtmlTokenizer tokenizer,
        ReadOnlySpan<Byte> utf8,
        Boolean yieldOnRequest
    )
    {
        Span<Byte> candidate = stackalloc Byte[4];
        var index = 0;
        while (_carryLength != 0)
        {
            candidate.Clear();
            CopyCarryTo(candidate);
            var status = Rune.DecodeFromUtf8(candidate[.._carryLength], out _, out var consumed);
            if (status == OperationStatus.Done)
            {
                tokenizer.WriteNormalizedUtf8(candidate[..consumed], yieldOnRequest);
                ClearCarry();
                return index;
            }
            if (status == OperationStatus.InvalidData)
            {
                tokenizer.WriteNormalizedUtf8("\uFFFD"u8, yieldOnRequest);
                ShiftCarry(Math.Max(consumed, 1));
                if (yieldOnRequest && tokenizer.IsYieldRequested)
                {
                    return index;
                }
                continue;
            }
            if (index == utf8.Length)
            {
                break;
            }

            AppendCarry(utf8[index++]);
        }

        return index;
    }

    internal void Complete(Utf8HtmlTokenizer tokenizer)
    {
        if (_carryLength == 0)
        {
            return;
        }

        tokenizer.WriteNormalizedUtf8("\uFFFD"u8, yieldOnRequest: false);
        ClearCarry();
    }

    private Int32 WriteWellFormed(
        Utf8HtmlTokenizer tokenizer,
        ReadOnlySpan<Byte> utf8,
        Int32 index,
        Int64 previousBytesConsumed,
        Boolean yieldOnRequest
    )
    {
        var remaining = utf8[index..];
        var completeLength = CompleteUtf8PrefixLength(remaining);
        if (completeLength != 0)
        {
            var consumed = tokenizer.WriteNormalizedUtf8(remaining[..completeLength], yieldOnRequest);
            index += consumed;
            if (consumed != completeLength)
            {
                _bytesConsumed = SaturatingAdd(previousBytesConsumed, index);
                return index;
            }
        }

        if (completeLength != remaining.Length)
        {
            SaveCarry(remaining[completeLength..]);
            index = utf8.Length;
        }

        return index;
    }

    private static Int32 WriteMalformedUtf8(
        Utf8HtmlTokenizer tokenizer,
        ReadOnlySpan<Byte> utf8,
        Boolean yieldOnRequest
    )
    {
        var index = 0;
        var validStart = 0;
        while (index < utf8.Length)
        {
            var status = Rune.DecodeFromUtf8(utf8[index..], out _, out var consumed);
            if (status == OperationStatus.Done)
            {
                index += consumed;
                continue;
            }

            if (index != validStart)
            {
                var validConsumed = tokenizer.WriteNormalizedUtf8(
                    utf8.Slice(validStart, index - validStart),
                    yieldOnRequest
                );
                validStart += validConsumed;
                if (validStart != index || (yieldOnRequest && tokenizer.IsYieldRequested))
                {
                    return validStart;
                }
            }

            tokenizer.WriteNormalizedUtf8("\uFFFD"u8, yieldOnRequest);
            if (status == OperationStatus.NeedMoreData)
            {
                return utf8.Length;
            }

            index += Math.Max(consumed, 1);
            validStart = index;
        }

        if (index != validStart)
        {
            validStart += tokenizer.WriteNormalizedUtf8(utf8[validStart..], yieldOnRequest);
        }

        return validStart;
    }

    private void AppendCarry(Byte value)
    {
        _carry |= (UInt32)value << (_carryLength * 8);
        _carryLength++;
    }

    private readonly void CopyCarryTo(Span<Byte> destination)
    {
        for (var index = 0; index < _carryLength; index++)
        {
            destination[index] = (Byte)(_carry >> (index * 8));
        }
    }

    private void SaveCarry(ReadOnlySpan<Byte> value)
    {
        _carry = 0;
        _carryLength = 0;
        foreach (var item in value)
        {
            AppendCarry(item);
        }
    }

    private void ShiftCarry(Int32 consumed)
    {
        _carry >>= consumed * 8;
        _carryLength -= (Byte)consumed;
    }

    private void ClearCarry()
    {
        _carry = 0;
        _carryLength = 0;
    }

    private static Int32 CompleteUtf8PrefixLength(ReadOnlySpan<Byte> value)
    {
        if (value.IsEmpty)
        {
            return 0;
        }

        var lead = value.Length - 1;
        while (lead > 0 && value[lead] is >= 0x80 and <= 0xBF && value.Length - lead < 4)
        {
            lead--;
        }

        var expected = Utf8SequenceLength(value[lead]);
        return expected > 1 && value.Length - lead < expected ? lead : value.Length;
    }

    private static Int32 Utf8SequenceLength(Byte lead) =>
        lead switch
        {
            < 0x80 => 1,
            < 0xE0 => 2,
            < 0xF0 => 3,
            < 0xF8 => 4,
            _ => 1,
        };

    private static Int64 SaturatingAdd(Int64 left, Int64 right) =>
        left > Int64.MaxValue - right ? Int64.MaxValue : left + right;
}
