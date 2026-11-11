namespace AngleSharp.Text;

using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// Represents a fully loaded immutable text source.
/// </summary>
public sealed class ReadOnlyMemoryTextSource : IReadOnlyTextSource
{
    private Int32 _index;
    private String? _content;
    private readonly ReadOnlyMemory<Char> _memory;
    private readonly Int32 _length;

    // What the memory is a view of, where that is a string or an array - which it nearly always
    // is. Getting a span out of a memory is a type test and a slice every time, too much for
    // the tokenizer to pay for each single character it reads.
    private readonly String? _text;
    private readonly Char[]? _array;
    private readonly Int32 _start;

    #region ctor

    /// <summary>
    ///
    /// </summary>
    /// <param name="memory"></param>
    public ReadOnlyMemoryTextSource(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
        _length = memory.Length;

        if (MemoryMarshal.TryGetString(memory, out var text, out var start, out _))
        {
            _text = text;
            _start = start;
        }
        else if (MemoryMarshal.TryGetArray(memory, out var segment))
        {
            _array = segment.Array;
            _start = segment.Offset;
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    public ReadOnlyMemoryTextSource(String str)
    {
        _content = str;
        _memory = str.AsMemory();
        _length = _memory.Length;
        _text = str;
    }

    #endregion

    #region Properties

    /// <ihneritdoc />
    public String Text
    {
        get
        {
            return _content ??= _memory.Span.ToString();
        }
    }

    /// <summary>
    /// Gets the underlying memory buffer.
    /// </summary>
    internal ReadOnlyMemory<Char> Memory => _memory;

    /// <summary>
    /// Gets everything from the current index on. Only valid while that is within the source.
    /// </summary>
    internal ReadOnlySpan<Char> Remaining
    {
        get
        {
            var index = _index;
            var count = _length - index;

            if (_text is not null)
            {
                return _text.AsSpan(_start + index, count);
            }
            else if (_array is not null)
            {
                return _array.AsSpan(_start + index, count);
            }

            return _memory.Span.Slice(index, count);
        }
    }

    /// <ihneritdoc />
    public Char this[Int32 index] => _content != null ? _content[index] : _memory.Span[index];

    /// <ihneritdoc />
    public Int32 Length => _length;

    /// <ihneritdoc />
    public Encoding CurrentEncoding
    {
        get => TextEncoding.Utf8;
        set { }
    }

    /// <ihneritdoc />
    public Int32 Index
    {
        get => _index;
        set => _index = value;
    }

    #endregion

    #region Disposable

    /// <ihneritdoc />
    public void Dispose()
    {
    }

    #endregion

    #region Text Methods

    /// <ihneritdoc />
    public Char ReadCharacter()
    {
        var index = _index;
        _index = index + 1;

        if ((UInt32)index < (UInt32)_length)
        {
            if (_text is not null)
            {
                return _text[_start + index];
            }
            else if (_array is not null)
            {
                return _array[_start + index];
            }

            return _memory.Span[index];
        }

        return Symbols.EndOfFile;
    }

    /// <ihneritdoc />
    public String ReadCharacters(Int32 characters)
    {
        return ReadMemory(characters).ToString();
    }

    /// <ihneritdoc />
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

    /// <ihneritdoc />
    public Task PrefetchAsync(Int32 length, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <ihneritdoc />
    public Task PrefetchAllAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    /// <ihneritdoc />
    public Boolean TryGetContentLength(out Int32 length)
    {
        length = _length;
        return true;
    }

    #endregion
}