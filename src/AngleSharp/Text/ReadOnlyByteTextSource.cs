namespace AngleSharp.Text;

using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// Represents an immutable byte buffer decoded as a fully loaded text source.
/// </summary>
/// <remarks>
/// The source retains the original bytes so that an encoding declaration discovered by the HTML
/// tokenizer can re-decode the document without reopening or copying an input stream.
/// </remarks>
public sealed class ReadOnlyByteTextSource : IReadOnlyTextSource
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
    /// Creates a byte source that detects a byte order mark and otherwise starts with UTF-8.
    /// </summary>
    /// <param name="bytes">The immutable byte buffer.</param>
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
    /// <param name="bytes">The immutable byte buffer.</param>
    /// <param name="encoding">The authoritative encoding.</param>
    public ReadOnlyByteTextSource(ReadOnlyMemory<Byte> bytes, Encoding encoding)
    {
        _bytes = bytes;
        _encoding = encoding ?? throw new ArgumentNullException(nameof(encoding));
        _encodingCertain = true;
        _chars = Decode(encoding, out _charLength);
    }

    /// <inheritdoc />
    public String Text => _text ??= new String(_chars, 0, _charLength);

    /// <inheritdoc />
    public Int32 Length => _charLength;

    /// <inheritdoc />
    public Encoding CurrentEncoding
    {
        get => _encoding;
        set => SetEncoding(value);
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

        if (start >= _charLength)
        {
            return ReadOnlyMemory<Char>.Empty;
        }

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

    private void SetEncoding(Encoding? encoding)
    {
        if (encoding is null || _encodingCertain)
        {
            return;
        }

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
        var carriesPosition = _index <= _charLength &&
            _index <= replacementLength &&
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

    private Char[] Decode(Encoding encoding, out Int32 charLength)
    {
        var bytes = _bytes.Slice(_preambleLength);
        var chars = ArrayPool<Char>.Shared.Rent(Math.Max(1, encoding.GetMaxCharCount(bytes.Length)));

        try
        {
#if NET8_0_OR_GREATER
            charLength = encoding.GetChars(bytes.Span, chars.AsSpan());
#else
            if (MemoryMarshal.TryGetArray(bytes, out var segment))
            {
                charLength = encoding.GetChars(segment.Array!, segment.Offset, segment.Count, chars, 0);
            }
            else
            {
                var copy = bytes.ToArray();
                charLength = encoding.GetChars(copy, 0, copy.Length, chars, 0);
            }
#endif
            return chars;
        }
        catch
        {
            ArrayPool<Char>.Shared.Return(chars);
            throw;
        }
    }
}
