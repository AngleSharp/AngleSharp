namespace AngleSharp.Text;

using System;
using System.Buffers;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// Forward-only byte source that keeps a bounded decoded UTF-16 window.
/// </summary>
/// <remarks>
/// This source preserves a small lookback window for tokenizer reconsumption. Automatic mode additionally retains
/// a provisional 1024-byte prefix so the existing parser restart path can change encoding before the source freezes.
/// </remarks>
internal sealed class StreamingTextSource : IReadOnlyTextSource
{
    private const Int32 DefaultBufferSize = 4096;
    private const Int32 DefaultLookback = 64;
    private const Int32 EncodingPreludeSize = 1024;

    private readonly Stream _stream;
    private Decoder _decoder;
    private Byte[] _bytes;
    private Char[] _chars;
    private readonly Int32 _decodeChunkSize;
    private readonly Int32 _lookback;
    private readonly Boolean _supportsEncodingRestart;

    private Encoding _encoding;
    private Byte[]? _provisionalBytes;
    private Int32 _provisionalByteCount;
    private Int32 _provisionalCharLength = -1;

    private Int32 _byteOffset;
    private Int32 _byteCount;
    private Int32 _bufferStart;
    private Int32 _bufferLength;
    private Int32 _index;
    private Boolean _streamEnded;
    private Boolean _finished;
    private Boolean _atStart = true;
    private Boolean _initialized;
    private Boolean _encodingCertain;
    private Boolean _decodingPrelude;

    /// <summary>
    /// Creates a bounded UTF-8 streaming source.
    /// </summary>
    internal StreamingTextSource(
        Stream stream,
        Int32 bufferSize = DefaultBufferSize,
        Int32 lookback = DefaultLookback)
        : this(stream, TextEncoding.Utf8, allowEncodingRestart: false, bufferSize, lookback)
    {
    }

    /// <summary>
    /// Creates a bounded source which optionally retains a restartable first 1024 bytes.
    /// </summary>
    internal StreamingTextSource(
        Stream stream,
        Encoding encoding,
        Boolean allowEncodingRestart,
        Int32 bufferSize = DefaultBufferSize,
        Int32 lookback = DefaultLookback)
    {
        if (stream is null)
        {
            throw new ArgumentNullException(nameof(stream));
        }

        if (bufferSize < 128)
        {
            throw new ArgumentOutOfRangeException(nameof(bufferSize), "The buffer size must be at least 128 bytes.");
        }

        if (lookback < 32)
        {
            throw new ArgumentOutOfRangeException(nameof(lookback), "The lookback must be at least 32 characters.");
        }

        if (!stream.CanRead)
        {
            throw new ArgumentException("The stream must be readable.", nameof(stream));
        }

        _stream = stream;
        _encoding = encoding ?? TextEncoding.Utf8;
        _decoder = _encoding.GetDecoder();
        _supportsEncodingRestart = allowEncodingRestart;
        _encodingCertain = !allowEncodingRestart;
        var actualBufferSize = allowEncodingRestart ? Math.Max(bufferSize, EncodingPreludeSize) : bufferSize;
        _bytes = ArrayPool<Byte>.Shared.Rent(actualBufferSize);
        _chars = ArrayPool<Char>.Shared.Rent(checked(actualBufferSize * 2 + lookback));
        _provisionalBytes = allowEncodingRestart ? ArrayPool<Byte>.Shared.Rent(EncodingPreludeSize) : null;
        _decodeChunkSize = actualBufferSize;
        _lookback = lookback;
    }

    /// <summary>
    /// The full text is intentionally not retained by this source.
    /// </summary>
    public String Text => throw new NotSupportedException(
        "A bounded streaming source does not retain the complete input text.");

    /// <summary>
    /// Gets the global end position of the currently decoded window.
    /// </summary>
    public Int32 Length => _bufferStart + _bufferLength;

    /// <summary>
    /// Gets the selected input encoding.
    /// </summary>
    public Encoding CurrentEncoding
    {
        get => _encoding;
        set
        {
            if (value is null || _encodingCertain)
            {
                return;
            }

            var encoding = NormalizeEncoding(value);
            if (encoding.CodePage == _encoding.CodePage)
            {
                FreezeEncoding();
                return;
            }

            ChangeEncoding(encoding);
        }
    }

    /// <summary>
    /// Gets or sets the global read position inside the retained window.
    /// </summary>
    public Int32 Index
    {
        get => _index;
        set
        {
            var maximum = _finished ? Length + 1 : Length;
            if (value < _bufferStart || value > maximum)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "The requested position is outside the retained window.");
            }

            _index = value;
        }
    }

    /// <inheritdoc />
    public Char this[Int32 index]
    {
        get
        {
            if (index < _bufferStart || index >= Length)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            return _chars[index - _bufferStart];
        }
    }

    /// <inheritdoc />
    public Char ReadCharacter()
    {
        if (_index < Length)
        {
            return _chars[_index++ - _bufferStart];
        }

        EnsureAvailable(1, allowFreeze: true);
        if (_index >= Length)
        {
            _index++;
            return Symbols.EndOfFile;
        }
        return _chars[_index++ - _bufferStart];
    }

    /// <inheritdoc />
    public String ReadCharacters(Int32 characters)
    {
        if (characters < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(characters));
        }

        if (characters == 0)
        {
            return String.Empty;
        }

        var available = Math.Max(0, Length - _index);
        if (characters > available && !_finished)
        {
            var contiguous = Math.Min(characters, GetMaximumContiguousRead());
            EnsureAvailable(contiguous, allowFreeze: true);
            available = Math.Max(0, Length - _index);
        }

        if (characters <= available || _finished)
        {
            var count = Math.Min(characters, available);
            var result = new String(_chars, _index - _bufferStart, count);
            AdvanceAfterRead(characters, count);
            return result;
        }

        var buffer = new Char[characters];
        var written = 0;
        while (written < buffer.Length)
        {
            var remaining = buffer.Length - written;
            EnsureAvailable(Math.Min(remaining, GetMaximumContiguousRead()), allowFreeze: true);
            available = Math.Max(0, Length - _index);
            if (available == 0)
            {
                _index++;
                break;
            }

            var count = Math.Min(remaining, available);
            Array.Copy(_chars, _index - _bufferStart, buffer, written, count);
            _index += count;
            written += count;
        }
        return new String(buffer, 0, written);
    }

    /// <inheritdoc />
    public StringOrMemory ReadMemory(Int32 characters)
    {
        if (characters < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(characters));
        }

        if (characters == 0)
        {
            return StringOrMemory.Empty;
        }

        if (characters <= GetMaximumContiguousRead())
        {
            var available = Math.Max(0, Length - _index);
            if (characters > available && !_finished)
            {
                EnsureAvailable(characters, allowFreeze: true);
                available = Math.Max(0, Length - _index);
            }

            if (characters <= available || _finished)
            {
                var count = Math.Min(characters, available);
                if (count > 0)
                {
                    var memory = new ReadOnlyMemory<Char>(_chars, _index - _bufferStart, count);
                    AdvanceAfterRead(characters, count);
                    return memory;
                }

                AdvanceAfterRead(characters, count);
                return StringOrMemory.Empty;
            }
        }

        return new StringOrMemory(ReadCharacters(characters));
    }

    /// <inheritdoc />
    public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken)
    {
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length));
        }

        return EnsureAvailableAsync(
            Math.Min(length, _decodeChunkSize * 2),
            allowFreeze: false,
            cancellationToken);
    }

    /// <summary>
    /// A bounded source cannot prefetch and retain the complete stream.
    /// </summary>
    public Task PrefetchAllAsync(CancellationToken cancellationToken) =>
        Task.FromException(new NotSupportedException(
            "A bounded streaming source cannot retain the complete input text."));

    /// <inheritdoc />
    public Boolean TryGetContentLength(out Int32 length)
    {
        length = 0;
        return false;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_chars is null)
        {
            return;
        }

        ReleaseProvisionalBytes();
        ArrayPool<Byte>.Shared.Return(_bytes);
        ArrayPool<Char>.Shared.Return(_chars);
        _bytes = null!;
        _chars = null!;
    }

    private Int32 GetMaximumContiguousRead() => Math.Max(1, _chars.Length - _lookback - 2);

    private void AdvanceAfterRead(Int32 requested, Int32 actual)
    {
        _index += actual;
        if (actual < requested)
        {
            _index++;
        }
    }

    private void EnsureAvailable(Int32 count, Boolean allowFreeze)
    {
        Initialize();
        var target = checked(_index + count);
        if (allowFreeze && IsAtProvisionalBoundary() && _index >= _provisionalCharLength)
        {
            FreezeEncoding();
        }

        while (Length < target && !_finished)
        {
            if (IsAtProvisionalBoundary())
            {
                if (!allowFreeze || _index < _provisionalCharLength)
                {
                    break;
                }

                FreezeEncoding();
            }

            if (_byteOffset >= _byteCount && !_streamEnded)
            {
                _byteCount = _stream.Read(_bytes, 0, _decodeChunkSize);
                _byteOffset = 0;
                _streamEnded = _byteCount == 0;
            }
            DecodeAvailable();
        }
    }

    private async Task EnsureAvailableAsync(Int32 count, Boolean allowFreeze, CancellationToken cancellationToken)
    {
        await InitializeAsync(cancellationToken).ConfigureAwait(false);
        var target = checked(_index + count);
        if (allowFreeze && IsAtProvisionalBoundary() && _index >= _provisionalCharLength)
        {
            FreezeEncoding();
        }
        while (Length < target && !_finished)
        {
            if (IsAtProvisionalBoundary())
            {
                if (!allowFreeze || _index < _provisionalCharLength)
                {
                    break;
                }

                FreezeEncoding();
            }

            cancellationToken.ThrowIfCancellationRequested();
            if (_byteOffset >= _byteCount && !_streamEnded)
            {
#if NET8_0_OR_GREATER
                _byteCount = await _stream.ReadAsync(
                    _bytes.AsMemory(0, _decodeChunkSize),
                    cancellationToken).ConfigureAwait(false);
#else
                _byteCount = await _stream.ReadAsync(
                    _bytes,
                    0,
                    _decodeChunkSize,
                    cancellationToken).ConfigureAwait(false);
#endif
                _byteOffset = 0;
                _streamEnded = _byteCount == 0;
            }
            DecodeAvailable();
        }
    }

    private void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        if (_supportsEncodingRestart)
        {
            ReadPrelude();
            PreparePrelude();
        }

        _initialized = true;
    }

    private async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (_initialized)
        {
            return;
        }

        if (_supportsEncodingRestart)
        {
            await ReadPreludeAsync(cancellationToken).ConfigureAwait(false);
            PreparePrelude();
        }

        _initialized = true;
    }

    private void ReadPrelude()
    {
        while (_byteCount < EncodingPreludeSize)
        {
            var read = _stream.Read(_bytes, _byteCount, EncodingPreludeSize - _byteCount);
            if (read == 0)
            {
                _streamEnded = true;
                break;
            }

            _byteCount += read;
            if (_byteCount >= 4 && ByteOrderMark.TryDetect(_bytes, _byteCount, out _, out _))
            {
                break;
            }
        }
    }

    private async Task ReadPreludeAsync(CancellationToken cancellationToken)
    {
        while (_byteCount < EncodingPreludeSize)
        {
#if NET8_0_OR_GREATER
            var read = await _stream.ReadAsync(
                _bytes.AsMemory(_byteCount, EncodingPreludeSize - _byteCount),
                cancellationToken).ConfigureAwait(false);
#else
            var read = await _stream.ReadAsync(
                _bytes,
                _byteCount,
                EncodingPreludeSize - _byteCount,
                cancellationToken).ConfigureAwait(false);
#endif

            if (read == 0)
            {
                _streamEnded = true;
                break;
            }

            _byteCount += read;
            if (_byteCount >= 4 && ByteOrderMark.TryDetect(_bytes, _byteCount, out _, out _))
            {
                break;
            }
        }
    }

    private void PreparePrelude()
    {
        if (ByteOrderMark.TryDetect(_bytes, _byteCount, out var detected, out var bomLength))
        {
            _encoding = detected;
            _decoder = detected.GetDecoder();
            Array.Copy(_bytes, bomLength, _bytes, 0, _byteCount - bomLength);
            _byteCount -= bomLength;
            _encodingCertain = true;
            ReleaseProvisionalBytes();
            return;
        }

        _provisionalByteCount = _byteCount;
        Array.Copy(_bytes, 0, _provisionalBytes!, 0, _provisionalByteCount);
        _decodingPrelude = true;
    }

    private static Encoding NormalizeEncoding(Encoding encoding) =>
        encoding.IsUnicode() ? TextEncoding.Utf8 : encoding;

    private Boolean IsAtProvisionalBoundary() =>
        !_encodingCertain && _provisionalCharLength >= 0 && Length >= _provisionalCharLength;

    private void ChangeEncoding(Encoding encoding)
    {
        if (_provisionalBytes is null || _provisionalCharLength < 0 || _bufferStart != 0)
        {
            FreezeEncoding();
            return;
        }

        var decoder = encoding.GetDecoder();
        var replacement = ArrayPool<Char>.Shared.Rent(_chars.Length);
        try
        {
            var replacementLength = decoder.GetChars(
                _provisionalBytes,
                0,
                _provisionalByteCount,
                replacement,
                0,
                _streamEnded);
            var prefixMatches = _index <= replacementLength &&
                _index <= _bufferLength &&
                new ReadOnlySpan<Char>(_chars, 0, _index).SequenceEqual(
                    new ReadOnlySpan<Char>(replacement, 0, _index));

            _encoding = encoding;
            _decoder = decoder;
            _bufferStart = 0;
            _bufferLength = replacementLength;
            _provisionalCharLength = replacementLength;
            Array.Copy(replacement, 0, _chars, 0, replacementLength);
            FreezeEncoding();

            if (!prefixMatches)
            {
                _index = 0;
                throw new NotSupportedException();
            }
        }
        finally
        {
            ArrayPool<Char>.Shared.Return(replacement);
        }
    }

    private void FreezeEncoding()
    {
        _encodingCertain = true;
        ReleaseProvisionalBytes();
    }

    private void ReleaseProvisionalBytes()
    {
        if (_provisionalBytes is not null)
        {
            ArrayPool<Byte>.Shared.Return(_provisionalBytes);
            _provisionalBytes = null;
        }
    }

    private void DecodeAvailable()
    {
        MakeRoom();
        _decoder.Convert(
            _bytes,
            _byteOffset,
            _byteCount - _byteOffset,
            _chars,
            _bufferLength,
            _chars.Length - _bufferLength,
            _streamEnded,
            out var bytesUsed,
            out var charsUsed,
            out var completed);

        _byteOffset += bytesUsed;
        _bufferLength += charsUsed;
        if (_decodingPrelude && _byteOffset >= _byteCount)
        {
            _decodingPrelude = false;
            _provisionalCharLength = _bufferLength;
        }
        if (_atStart && _bufferLength > 0)
        {
            _atStart = false;
            if (_chars[0] == '\uFEFF')
            {
                Array.Copy(_chars, 1, _chars, 0, _bufferLength - 1);
                _bufferLength--;
            }
        }
        if (_streamEnded && _byteOffset >= _byteCount && completed)
        {
            _finished = true;
        }

        if (bytesUsed == 0 && charsUsed == 0 && !_finished)
        {
            throw new InvalidOperationException("The decoder made no progress.");
        }
    }

    private void MakeRoom()
    {
        if (_chars.Length - _bufferLength >= 2)
        {
            return;
        }

        var retainFrom = Math.Max(_bufferStart, _index - _lookback);
        var discard = retainFrom - _bufferStart;
        if (discard <= 0)
        {
            throw new InvalidOperationException("The requested forward window exceeds the streaming buffer capacity.");
        }

        Array.Copy(_chars, discard, _chars, 0, _bufferLength - discard);
        _bufferStart += discard;
        _bufferLength -= discard;
    }

}
