#if NET8_0_OR_GREATER
namespace AngleSharp.Text;

using System;
using System.Buffers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// Represents an immutable byte buffer decoded as a fully loaded text source.
/// </summary>
public sealed class ReadOnlyByteTextSource : IContiguousTextSource
{
    private readonly ReadOnlyMemory<Byte> _bytes;
    private readonly Int32 _preambleLength;

    private Char[] _chars;
    private Int32 _charLength;
    private String? _text;
    private Encoding _encoding;
    private Int32 _index;
    private Boolean _encodingCertain;

    /// <summary>
    /// Creates a byte source that detects a BOM and otherwise starts with UTF-8.
    /// </summary>
    public ReadOnlyByteTextSource(ReadOnlyMemory<Byte> bytes)
    {
        _bytes = bytes;

        if (ByteOrderMark.TryDetect(bytes.Span, out var encoding, out var preambleLength))
        {
            _encoding = encoding;
            _preambleLength = preambleLength;
            _encodingCertain = true;
        }
        else
        {
            _encoding = TextEncoding.Utf8;
        }

        _chars = Decode(_encoding, out _charLength);
    }

    /// <summary>
    /// Creates a byte source decoded with an authoritative encoding.
    /// </summary>
    public ReadOnlyByteTextSource(ReadOnlyMemory<Byte> bytes, Encoding encoding)
    {
        if (encoding is null)
        {
            throw new ArgumentNullException(nameof(encoding));
        }

        _bytes = bytes;
        _encoding = encoding;
        _encodingCertain = true;
        _chars = Decode(encoding, out _charLength);
    }

    /// <inheritdoc />
    public String Text
    {
        get
        {
            return _text ??= new String(_chars, 0, _charLength);
        }
    }

    /// <inheritdoc />
    public Int32 Length => _charLength;

    /// <inheritdoc />
    public Encoding CurrentEncoding
    {
        get => _encoding;
        set => SetEncoding(value);
    }

    private void SetEncoding(Encoding? encoding)
    {
        if (encoding is null || _encodingCertain)
        {
            return;
        }

        // A document that is already known to be UTF-16 ignores a declared encoding outright
        if (_encoding.IsUnicode())
        {
            _encodingCertain = true;
            return;
        }

        if (encoding.IsUnicode())
        {
            encoding = TextEncoding.Utf8;
        }

        if (encoding.CodePage == _encoding.CodePage)
        {
            _encodingCertain = true;
            return;
        }

        var replacement = Decode(encoding, out var replacementLength);

        // Position only carries over if the consumed prefix decoded identically; re-decoding
        // shifts offsets, forcing a re-tokenize via the NotSupportedException below.
        var carriesPosition = _index <= _charLength && _index <= replacementLength &&
            _chars.AsSpan(0, _index).SequenceEqual(replacement.AsSpan(0, _index));

        var previous = _chars;
        _encoding = encoding;
        _encodingCertain = true;
        _chars = replacement;
        _charLength = replacementLength;
        _text = null;
        ArrayPool<Char>.Shared.Return(previous);

        if (!carriesPosition)
        {
            _index = 0;
            throw new NotSupportedException();
        }
    }

    /// <inheritdoc />
    public Int32 Index
    {
        get => _index;
        set => _index = value;
    }

    /// <inheritdoc />
    public Char this[Int32 index] => _chars[index];

    /// <inheritdoc />
    public Char ReadCharacter()
    {
        if (_index < _charLength)
        {
            return _chars[_index++];
        }

        _index++;
        return Symbols.EndOfFile;
    }

    /// <inheritdoc />
    public String ReadCharacters(Int32 characters) => ReadMemory(characters).ToString();

    /// <inheritdoc />
    public StringOrMemory ReadMemory(Int32 characters)
    {
        var start = _index;
        var end = start + characters;
        if (end <= _charLength)
        {
            _index += characters;
            return new ReadOnlyMemory<Char>(_chars, start, characters);
        }

        _index += characters;
        characters = Math.Min(characters, _charLength - start);
        return new ReadOnlyMemory<Char>(_chars, start, characters);
    }

    /// <inheritdoc />
    public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken) => Task.CompletedTask;

    /// <inheritdoc />
    public Task PrefetchAllAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    /// <inheritdoc />
    public Boolean TryGetContentLength(out Int32 length)
    {
        length = _charLength;
        return true;
    }

    /// <inheritdoc />
    Boolean IContiguousTextSource.TryGetRemainingSpan(out ReadOnlySpan<Char> remaining)
    {
        if ((UInt32)_index < (UInt32)_charLength)
        {
            remaining = _chars.AsSpan(_index, _charLength - _index);
            return true;
        }

        remaining = default;
        return false;
    }

    /// <inheritdoc />
    public void Dispose()
    {
        if (_chars is null)
        {
            return;
        }

        ArrayPool<Char>.Shared.Return(_chars);
        _chars = null!;
        _charLength = 0;
        _text = null;
    }

    private Char[] Decode(Encoding encoding, out Int32 charLength)
    {
        var bytes = _bytes.Span.Slice(_preambleLength);
        var chars = ArrayPool<Char>.Shared.Rent(Math.Max(1, encoding.GetMaxCharCount(bytes.Length)));
        try
        {
            charLength = encoding.GetChars(bytes, chars.AsSpan());
            return chars;
        }
        catch
        {
            ArrayPool<Char>.Shared.Return(chars);
            throw;
        }
    }

}
#endif
