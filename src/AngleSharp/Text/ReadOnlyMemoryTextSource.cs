namespace AngleSharp.Text;

using System;
using System.Runtime.CompilerServices;
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

    #region ctor

    /// <summary>
    ///
    /// </summary>
    /// <param name="memory"></param>
    public ReadOnlyMemoryTextSource(ReadOnlyMemory<Char> memory)
    {
        _memory = memory;
        _length = memory.Length;
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
        if (_index < _length)
        {
            return _memory.Span[_index++];
        }

        _index += 1;
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