#nullable disable
namespace AngleSharp.Text;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// A stream abstraction to handle encoding and more.
/// </summary>
public sealed class PrefetchedTextSource : IReadOnlyTextSource
{
    private Int32 _index;
    private String _content;

    private readonly ReadOnlyMemory<Char> _memory;
    private readonly Int32 _length;

    #region ctor

    /// <summary>
    ///
    /// </summary>
    /// <param name="memory"></param>
    public PrefetchedTextSource(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
        _length = memory.Length;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    public PrefetchedTextSource(String str)
    {
        _content = str;
        _memory = str.AsMemory();
        _length = _memory.Length;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the full text buffer.
    /// </summary>
    [MemberNotNull("_content")]
    public String Text
    {
        get
        {
            return _content ??= _memory.Span.CreateString();
        }
    }

    /// <summary>
    /// Gets the character at the given position in the text buffer.
    /// </summary>
    /// <param name="index">The index of the character.</param>
    /// <returns>The character.</returns>
    public Char this[Int32 index] => _content[index];

    /// <summary>
    /// Gets the length of the text buffer.
    /// </summary>
    public Int32 Length => _length;

    /// <summary>
    /// Gets or sets the encoding to use.
    /// </summary>
    public Encoding CurrentEncoding
    {
        get => Encoding.Default;
        set { }
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
    }

    #endregion

    #region Text Methods

    /// <summary>
    /// Reads the next character from the buffer or underlying stream, if any.
    /// </summary>
    /// <returns>The next character.</returns>
    public Char ReadCharacter()
    {
        if (_index < _length)
        {
            return ReplaceEof(_content[_index++]);
        }

        _index += 1;
        return Symbols.EndOfFile;
    }

    /// <summary>
    /// Reads the upcoming numbers of characters from the buffer or
    /// underlying stream, if any.
    /// </summary>
    /// <param name="characters">The number of characters to read.</param>
    /// <returns>The string with the next characters.</returns>
    public String ReadCharacters(Int32 characters)
    {
        return ReadMemory(characters).String;
    }

    public StringOrMemory ReadMemory(Int32 characters)
    {
        var start = _index;
        var end = start + characters;

        if (end <= _length)
        {
            _index += characters;
            return _memory.Slice(start, characters);
        }

        _index += characters;
        characters = Math.Min(characters, _length - start);
        return _memory.Slice(start, characters);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="length"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PrefetchAllAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="length"></param>
    /// <returns></returns>
    public Boolean TryGetContentLength(out Int32 length)
    {
        length = _length;
        return true;
    }

    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static Char ReplaceEof(Char c) => c == Symbols.EndOfFile ? (Char)0xFFFD : c;

}