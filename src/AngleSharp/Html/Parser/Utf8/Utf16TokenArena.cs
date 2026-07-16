using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;
using AngleSharp.Common;

namespace AngleSharp.Html.Parser.Utf8;

/// <summary>
/// Owns decoded token text until the tree constructor requests the next token.
/// </summary>
internal sealed class Utf16TokenArena : IDisposable
{
    private const Int32 MinimumBufferSize = 4096;

    private Char[]? _current;
    private List<Char[]>? _retired;
    private Int32 _written;

    public StringOrMemory Decode(ReadOnlySpan<Byte> utf8)
    {
        if (utf8.IsEmpty)
        {
            return StringOrMemory.Empty;
        }

        EnsureCapacity(utf8.Length);
        var start = _written;
        var charCount = Encoding.UTF8.GetChars(utf8, _current.AsSpan(start, utf8.Length));
        _written += charCount;
        return new ReadOnlyMemory<Char>(_current, start, charCount);
    }

    private void EnsureCapacity(Int32 required)
    {
        if (_current is not null && required <= _current.Length - _written)
        {
            return;
        }

        if (_current is not null)
        {
            (_retired ??= []).Add(_current);
        }

        _current = ArrayPool<Char>.Shared.Rent(Math.Max(MinimumBufferSize, required));
        _written = 0;
    }

    public void Dispose()
    {
        if (_current is not null)
        {
            ArrayPool<Char>.Shared.Return(_current);
            _current = null;
        }

        if (_retired is not null)
        {
            foreach (var buffer in _retired)
            {
                ArrayPool<Char>.Shared.Return(buffer);
            }
            _retired.Clear();
        }
    }
}
