#nullable disable
namespace AngleSharp.Text;

using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

internal sealed class WritableTextSource : ITextSource
{
    #region Fields

    private const Int32 BufferSize = 4096;

    private readonly Stream _baseStream;
    private readonly MemoryStream _raw;
    private readonly Byte[] _buffer;
    private readonly Char[] _chars;
    private readonly Boolean _detectByteOrderMark;

    private StringBuilder _content;
    private EncodingConfidence _confidence;
    private Boolean _finished;
    private Encoding _encoding;
    private Decoder _decoder;
    private Int32 _index;

    #endregion

    #region ctor

    private WritableTextSource(Encoding encoding, Boolean allocateBuffers)
    {
        if (allocateBuffers)
        {
            _buffer = new Byte[BufferSize];
            _chars = new Char[BufferSize + 1];
        }

        _raw = new MemoryStream();
        _index = 0;
        _encoding = encoding ?? TextEncoding.Utf8;
        _decoder = _encoding.GetDecoder();
    }

    /// <summary>
    /// Creates a new text source from a string. No underlying stream will
    /// be used.
    /// </summary>
    /// <param name="source">The data source.</param>
    public WritableTextSource(String source)
        : this(null, TextEncoding.Utf8)
    {
        _finished = true;
        _content.Append(source);
        _confidence = EncodingConfidence.Irrelevant;
    }

    /// <summary>
    /// Creates a new text source from a string. The underlying stream is
    /// used as an unknown data source.
    /// </summary>
    /// <param name="baseStream">
    /// The underlying stream as data source.
    /// </param>
    /// <param name="encoding">
    /// The initial encoding. Otherwise UTF-8.
    /// </param>
    public WritableTextSource(Stream baseStream, Encoding encoding = null)
        : this(baseStream, encoding, encodingIsCertain: false)
    {
    }

    internal WritableTextSource(Stream baseStream, Encoding encoding, Boolean encodingIsCertain)
        : this(encoding, allocateBuffers: baseStream != null)
    {
        _baseStream = baseStream;
        _content = StringBuilderPool.Obtain();
        _detectByteOrderMark = !encodingIsCertain;
        _confidence = encodingIsCertain ? EncodingConfidence.Certain : EncodingConfidence.Tentative;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the full text buffer.
    /// </summary>
    [MemberNotNull("_content")]
    public String Text => _content.ToString();

    /// <summary>
    /// Gets the character at the given position in the text buffer.
    /// </summary>
    /// <param name="index">The index of the character.</param>
    /// <returns>The character.</returns>
    public Char this[Int32 index] => Replace(_content[index]);

    /// <summary>
    /// Gets the length of the text buffer.
    /// </summary>
    public Int32 Length => _content.Length;

    /// <summary>
    /// Gets or sets the encoding to use.
    /// </summary>
    public Encoding CurrentEncoding
    {
        get => _encoding;
        set
        {
            if (_confidence != EncodingConfidence.Tentative)
            {
                return;
            }

            if (_encoding.IsUnicode())
            {
                _confidence = EncodingConfidence.Certain;
                return;
            }

            if (value.IsUnicode())
            {
                value = TextEncoding.Utf8;
            }

            if (value == _encoding)
            {
                _confidence = EncodingConfidence.Certain;
                return;
            }

            _encoding = value;
            _decoder = value.GetDecoder();

            var buffer = _raw.GetBuffer();
            var raw_chars = new Char[_encoding.GetMaxCharCount((Int32)_raw.Length)];
            var charLength = _decoder.GetChars(buffer, 0, (Int32)_raw.Length, raw_chars, 0);

            var carriesPosition = _index <= charLength && _index <= _content.Length &&
                                  PrefixMatches(_content, raw_chars, _index);

            if (carriesPosition)
            {
                //If everything seems to fit up to this point, do an
                //instant switch
                _confidence = EncodingConfidence.Certain;
                _content.Remove(_index, _content.Length - _index);
                _content.Append(raw_chars, _index, charLength - _index);
            }
            else
            {
                //Otherwise consider restart from beginning ...
                _index = 0;
                _content.Clear().Append(raw_chars, 0, charLength);
                throw new NotSupportedException();
            }

            return;

            static Boolean PrefixMatches(StringBuilder builder, Char[] content, Int32 length)
            {
                for (var i = 0; i < length; i++)
                {
                    if (builder[i] != content[i])
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }

    /// <summary>
    /// Gets or sets the current index of the insertation and read point.
    /// </summary>
    public Int32 Index
    {
        get => _index;
        set => _index = value;
    }

    #endregion

    #region Disposable

    /// <summary>
    /// Disposes the text source by freeing the underlying stream, if any.
    /// </summary>
    public void Dispose()
    {
        var isDisposed = _content is null;

        if (!isDisposed)
        {
            _raw.Dispose();
            _content!.Clear().ReturnToPool();
            _content = null;
        }
    }

    #endregion

    #region Text Methods

    /// <summary>
    /// Reads the next character from the buffer or underlying stream, if
    /// any.
    /// </summary>
    /// <returns>The next character.</returns>
    public Char ReadCharacter()
    {
        if (_index < _content.Length)
        {
            return Replace(_content[_index++]);
        }

        ExpandBuffer(BufferSize);
        var index = _index++;
        return index < _content.Length ? Replace(_content[index]) : Symbols.EndOfFile;
    }

    /// <summary>
    /// Reads the upcoming numbers of characters from the buffer or
    /// underlying stream, if any.
    /// </summary>
    /// <param name="characters">The number of characters to read.</param>
    /// <returns>The string with the next characters.</returns>
    public String ReadCharacters(Int32 characters)
    {
        var start = _index;
        var end = start + characters;

        if (end <= _content!.Length)
        {
            _index += characters;
            return _content.ToString(start, characters);
        }

        ExpandBuffer(Math.Max(BufferSize, characters));
        _index += characters;
        characters = Math.Min(characters, _content.Length - start);
        return _content.ToString(start, characters);
    }

    /// <inheritdoc/>
    public StringOrMemory ReadMemory(Int32 characters)
    {
        return new StringOrMemory(ReadCharacters(characters));
    }

    /// <summary>
    /// Prefetches the number of bytes by expanding the internal buffer.
    /// </summary>
    /// <param name="length">The number of bytes to prefetch.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The awaitable task.</returns>
    public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken) =>
        ExpandBufferAsync(length, cancellationToken);

    /// <summary>
    /// Prefetches the whole stream by expanding the internal buffer.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The awaitable task.</returns>
    public async Task PrefetchAllAsync(CancellationToken cancellationToken)
    {
        if (_baseStream != null && _content!.Length == 0 && _detectByteOrderMark)
        {
            await DetectByteOrderMarkAsync(cancellationToken).ConfigureAwait(false);
        }

        while (!_finished)
        {
            await ReadIntoBufferAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Gets the content length, if known.
    /// </summary>
    /// <param name="length">Found length if known</param>
    /// <returns>True if length is available</returns>
    public Boolean TryGetContentLength(out Int32 length)
    {
        length = 0;
        return false;
    }

    /// <summary>
    /// Inserts the given content at the current insertation mark. Moves the
    /// insertation mark.
    /// </summary>
    /// <param name="content">The content to insert.</param>
    public void InsertText(String content)
    {
        if (_index >= 0 && _index < _content!.Length)
        {
            _content.Insert(_index, content);
        }
        else
        {
            _content!.Append(content);
        }

        _index += content.Length;
    }

    #endregion

    #region Helpers

    private static Char Replace(Char c) =>
        c == Symbols.EndOfFile ? (Char)0xFFFD : c;

    private async Task DetectByteOrderMarkAsync(CancellationToken cancellationToken)
    {
        var count = 0;
        var reachedEnd = false;
        while (count < 4)
        {
            var read = await _baseStream!.ReadAsync(
                _buffer,
                count,
                BufferSize - count,
                cancellationToken).ConfigureAwait(false);
            if (read == 0)
            {
                reachedEnd = true;
                break;
            }

            count += read;
        }

        if (ByteOrderMark.TryDetect(_buffer, count, out var encoding, out var preambleLength))
        {
            _encoding = encoding;
            count -= preambleLength;
            Array.Copy(_buffer, preambleLength, _buffer, 0, count);
            _decoder = encoding.GetDecoder();
            _confidence = EncodingConfidence.Certain;
        }

        if (count == 0 && !reachedEnd)
        {
            count = await _baseStream!.ReadAsync(
                _buffer,
                0,
                BufferSize,
                cancellationToken).ConfigureAwait(false);
        }

        AppendContentFromBuffer(count);
    }

    private async Task ExpandBufferAsync(Int64 size, CancellationToken cancellationToken)
    {
        if (!_finished && _content!.Length == 0 && _detectByteOrderMark)
        {
            await DetectByteOrderMarkAsync(cancellationToken).ConfigureAwait(false);
        }

        while (!_finished && size + _index > _content!.Length)
        {
            await ReadIntoBufferAsync(cancellationToken).ConfigureAwait(false);
        }
    }

    private async Task ReadIntoBufferAsync(CancellationToken cancellationToken)
    {
        var returned = await _baseStream!.ReadAsync(_buffer, 0, BufferSize, cancellationToken).ConfigureAwait(false);
        AppendContentFromBuffer(returned);
    }

    private void ExpandBuffer(Int64 size)
    {
        if (!_finished && _content!.Length == 0 && _detectByteOrderMark)
        {
            DetectByteOrderMarkAsync(CancellationToken.None).Wait();
        }

        while (!_finished && size + _index > _content!.Length)
        {
            ReadIntoBuffer();
        }
    }

    private void ReadIntoBuffer()
    {
        var returned = _baseStream!.Read(_buffer, 0, BufferSize);
        AppendContentFromBuffer(returned);
    }

    private void AppendContentFromBuffer(Int32 size)
    {
        _finished = size == 0;
        var charLength = _decoder.GetChars(_buffer, 0, size, _chars, 0);

        if (_confidence != EncodingConfidence.Certain)
        {
            _raw.Write(_buffer, 0, size);
        }

        _content.Append(_chars, 0, charLength);
    }

    #endregion

    #region Confidence

    private enum EncodingConfidence : Byte
    {
        Tentative,
        Certain,
        Irrelevant,
    }

    #endregion
}