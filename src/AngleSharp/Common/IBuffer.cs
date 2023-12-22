namespace AngleSharp.Common;

using System;
using System.Text;

interface ISlimBuffer
{
    Int32 Length { get; }
    Char this[Int32 i] { get; }
}

readonly struct StringBuilderAdapter : ISlimBuffer
{
    private readonly StringBuilder _sb;
    public StringBuilderAdapter(StringBuilder sb) => _sb = sb;
    public Int32 Length => _sb.Length;
    public Char this[Int32 i] => _sb[i];
}

interface IBuffer : ISlimBuffer, IDisposable
{
    IBuffer Append(Char c);
    void Discard();
    Int32 Capacity { get; }
    IBuffer Remove(Int32 start, Int32 length);
    void ReturnToPool();
    IBuffer Insert(Int32 index, Char c);
    IBuffer Append(ReadOnlySpan<Char> str);
    StringOrMemory GetDataAndClear();
    public Boolean HasText(ReadOnlySpan<Char> test, StringComparison comparison = StringComparison.Ordinal);
    public Boolean HasTextAt(ReadOnlySpan<Char> test, int offset, int length, StringComparison comparison = StringComparison.Ordinal);
    public String ToString();
}