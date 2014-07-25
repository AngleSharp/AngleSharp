namespace AngleSharp
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    interface ITextSource
    {
        Encoding CurrentEncoding { get; set; }

        Int32 Index { get; set; }

        Int32 Length { get; }

        Char Read();

        Task<Char> ReadAsync(CancellationToken cancellationToken);

        Char this[Int32 index] { get; }
    }
}
