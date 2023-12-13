#nullable disable
namespace AngleSharp.Text;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// A stream abstraction to handle encoding and more.
/// </summary>
public sealed class PrefetchedTextSource : IReadOnlyTextSource
{
    private const Int32 BufferSize = 4096;
    private readonly ReadOnlyMemory<Char> _memory;
    private Int32 _index;

    #region ctor

    /// <summary>
    ///
    /// </summary>
    /// <param name="memory"></param>
    public PrefetchedTextSource(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the full text buffer.
    /// </summary>
    [MemberNotNull("_content")]
    public String Text => new String(_memory.Span);

    /// <summary>
    /// Gets the character at the given position in the text buffer.
    /// </summary>
    /// <param name="index">The index of the character.</param>
    /// <returns>The character.</returns>
    public Char this[Int32 index] => _memory.Span[index];

    /// <summary>
    /// Gets the length of the text buffer.
    /// </summary>
    public Int32 Length => _memory.Length;

    /// <summary>
    /// Gets or sets the encoding to use.
    /// </summary>
    public Encoding CurrentEncoding => Encoding.Default;

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
    /// Reads the next character from the buffer or underlying stream, if
    /// any.
    /// </summary>
    /// <returns>The next character.</returns>
    public Char ReadCharacter()
    {
        if (_index < _memory.Length)
        {
            return Replace(_memory.Span[_index++]);
        }

        var index = _index++;
        return index < _memory.Span.Length ? Replace(_memory.Span[index]) : Symbols.EndOfFile;
    }

    /// <summary>
    /// Reads the upcoming numbers of characters from the buffer or
    /// underlying stream, if any.
    /// </summary>
    /// <param name="characters">The number of characters to read.</param>
    /// <returns>The string with the next characters.</returns>
    public String ReadCharacters(Int32 characters)
    {

        /*
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
            return _content.ToString(start, characters);*/

        var start = _index;
        var end = start + characters;

        if (end <= _memory!.Length)
        {
            _index += characters;
            return new String(_memory.Span.Slice(start, characters));
        }

        _index += characters;
        characters = Math.Min(characters, _memory.Length - start);
        return new String(_memory.Span.Slice(start, characters));
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="length"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task PrefetchAsync(int length, CancellationToken cancellationToken)
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

    #endregion

    private static Char Replace(Char c) => c == Symbols.EndOfFile ? (Char)0xFFFD : c;

    private void ExpandBuffer(Int64 size)
    {
        // if (!_finished && _content!.Length == 0)
        // {
        //     DetectByteOrderMarkAsync(CancellationToken.None).Wait();
        // }
        //
        // while (!_finished && size + _index > _content!.Length)
        // {
        //     ReadIntoBuffer();
        // }
    }
}