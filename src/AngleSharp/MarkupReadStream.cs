namespace AngleSharp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html;
using AngleSharp.Html.Dom;

// Advances the DOM serializer only when the consumer reads. The document and
// formatter remain caller-owned for the stream's lifetime.
internal sealed class MarkupReadStream : Stream
{
    private static readonly Char[] EmptyChars = Array.Empty<Char>();
    private readonly IEnumerator<String> _tokens;
    private readonly Encoder _encoder = new UTF8Encoding(false).GetEncoder();
    private readonly Byte[] _bytes = new Byte[4096];
    private Char[] _chars = EmptyChars;
    private Int32 _charOffset;
    private Int32 _byteOffset;
    private Int32 _byteCount;
    private Int32 _reading;
    private Boolean _atEnd;
    private Boolean _disposed;
    private ExceptionDispatchInfo? _failure;

    public MarkupReadStream(INode node, IMarkupFormatter? formatter = null)
    {
        if (node is null)
        {
            throw new ArgumentNullException(nameof(node));
        }

        _tokens = Serialize(node, formatter ?? HtmlMarkupFormatter.Instance).GetEnumerator();
    }

    public override Boolean CanRead => !_disposed;
    public override Boolean CanSeek => false;
    public override Boolean CanWrite => false;
    public override Int64 Length => throw new NotSupportedException();
    public override Int64 Position
    {
        get => throw new NotSupportedException();
        set => throw new NotSupportedException();
    }

    public override Int32 Read(Byte[] buffer, Int32 offset, Int32 count)
    {
        if (buffer is null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (offset < 0 || count < 0 || offset > buffer.Length - count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return ReadCore(buffer, offset, count, CancellationToken.None);
    }

    private Int32 ReadCore(Byte[] destination, Int32 offset, Int32 length, CancellationToken cancellationToken)
    {
        if (_disposed)
        {
            throw new ObjectDisposedException(nameof(MarkupReadStream));
        }

        _failure?.Throw();
        cancellationToken.ThrowIfCancellationRequested();

        if (length == 0)
        {
            return 0;
        }

        if (Interlocked.CompareExchange(ref _reading, 1, 0) != 0)
        {
            throw new InvalidOperationException("Concurrent reads are unsupported.");
        }

        try
        {
            var written = 0;
            while (written < length)
            {
                if (written != 0 && cancellationToken.IsCancellationRequested)
                {
                    break;
                }

                if (_byteOffset == _byteCount && !Fill(cancellationToken))
                {
                    break;
                }

                var count = Math.Min(length - written, _byteCount - _byteOffset);
                Buffer.BlockCopy(_bytes, _byteOffset, destination, offset + written, count);
                _byteOffset += count;
                written += count;
            }
            return written;
        }
        catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            throw;
        }
        catch (Exception error)
        {
            _failure = ExceptionDispatchInfo.Capture(error);
            throw;
        }
        finally
        {
            Volatile.Write(ref _reading, 0);
        }
    }

    public override Task<Int32> ReadAsync(Byte[] buffer, Int32 offset, Int32 count, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            return Task.FromCanceled<Int32>(cancellationToken);
        }

        if (buffer is null)
        {
            throw new ArgumentNullException(nameof(buffer));
        }

        if (offset < 0 || count < 0 || offset > buffer.Length - count)
        {
            throw new ArgumentOutOfRangeException();
        }

        try
        {
            return Task.FromResult(ReadCore(buffer, offset, count, cancellationToken));
        }
        catch (Exception error)
        {
            return Task.FromException<Int32>(error);
        }
    }

    private Boolean Fill(CancellationToken cancellationToken)
    {
        _byteOffset = 0;
        _byteCount = 0;

        while (true)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (_charOffset == _chars.Length && !_atEnd)
            {
                if (_tokens.MoveNext())
                {
                    _chars = _tokens.Current.ToCharArray();
                    _charOffset = 0;
                    if (_chars.Length == 0)
                    {
                        continue;
                    }
                }
                else
                {
                    _atEnd = true;
                }
            }

            _encoder.Convert(_chars, _charOffset, _chars.Length - _charOffset,
                _bytes, 0, _bytes.Length, _atEnd, out var charsUsed, out var bytesUsed, out _);
            _charOffset += charsUsed;
            _byteCount = bytesUsed;
            if (bytesUsed != 0)
            {
                return true;
            }

            if (_atEnd)
            {
                return false;
            }

            if (charsUsed == 0 && _charOffset != _chars.Length)
            {
                throw new InvalidOperationException("UTF-8 encoder made no progress.");
            }
        }
    }

    protected override void Dispose(Boolean disposing)
    {
        if (disposing && !_disposed)
        {
            _disposed = true;
            _tokens.Dispose();
        }

        base.Dispose(disposing);
    }

    public override void Flush() => throw new NotSupportedException();
    public override Int64 Seek(Int64 offset, SeekOrigin origin) => throw new NotSupportedException();
    public override void SetLength(Int64 value) => throw new NotSupportedException();
    public override void Write(Byte[] buffer, Int32 offset, Int32 count) => throw new NotSupportedException();

    private static IEnumerable<String> Serialize(INode root, IMarkupFormatter formatter)
    {
        var frames = new Stack<Frame>();
        frames.Push(new Frame(root));

        while (frames.Count != 0)
        {
            var frame = frames.Peek();
            var node = frame.Node;

            if (frame.Stage == 0)
            {
                frame.Stage = 1;
                if (node is IComment comment)
                {
                    yield return formatter.Comment(comment);
                }
                else if (node is IProcessingInstruction instruction)
                {
                    yield return formatter.Processing(instruction);
                }
                else if (node is ICharacterData data)
                {
                    yield return data.Parent?.Flags.HasFlag(NodeFlags.LiteralText) == true ? formatter.LiteralText(data) : formatter.Text(data);
                }
                else if (node is IDocumentType doctype)
                {
                    yield return formatter.Doctype(doctype);
                }
                else if (node is IElement element)
                {
                    frame.Element = element;
                    frame.SelfClosing = element.Flags.HasFlag(NodeFlags.SelfClosing);
                    yield return formatter.OpenTag(element, frame.SelfClosing);
                }

                continue;
            }

            if (frame.Stage == 1)
            {
                frame.Stage = 2;
                if (frame.Element is IElement element && !frame.SelfClosing &&
                    element.Flags.HasFlag(NodeFlags.LineTolerance) && element.FirstChild is IText text &&
                    text.Data.IndexOf('\n') >= 0)
                {
                    yield return "\n";
                }

                continue;
            }

            if (frame.Stage == 2)
            {
                frame.Stage = 3;
                if (frame.Element is IHtmlTemplateElement template)
                {
                    frames.Push(new Frame(template.Content));
                }

                continue;
            }

            if (frame.Stage == 3)
            {
                if (frame.ChildIndex < node.ChildNodes.Length)
                {
                    frames.Push(new Frame(node.ChildNodes[frame.ChildIndex++]));
                }
                else
                {
                    frame.Stage = 4;
                }

                continue;
            }

            frames.Pop();
            if (frame.Element is IElement closing)
            {
                yield return formatter.CloseTag(closing, frame.SelfClosing);
            }
        }
    }

    private sealed class Frame
    {
        public Frame(INode node)
        {
            Node = node;
        }
        public INode Node { get; }
        public IElement? Element { get; set; }
        public Boolean SelfClosing { get; set; }
        public Int32 Stage { get; set; }
        public Int32 ChildIndex { get; set; }
    }
}
