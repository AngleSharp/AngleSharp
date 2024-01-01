namespace AngleSharp.Text;

using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

/// <summary>
/// Char array based immutable text source
/// </summary>
public sealed class StringTextSource : IReadOnlyTextSource
{
    private readonly String _string;
    private readonly ReadOnlyMemory<Char> _memory;
    private readonly Int32 _length;

    private Int32 _index;

    /// <summary>
    /// Creates a new text source from a string
    /// </summary>
    public StringTextSource(String source)
    {
        _string = source;
        _length = source.Length;
        _memory = source.AsMemory();
    }

    #region Properties

    /// <ihneritdoc />
    public String Text => _string;

    /// <ihneritdoc />
    public Char this[Int32 index] => _string[index];

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
            return _string[_index++];
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