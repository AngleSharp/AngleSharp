namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io.Dom;
    using AngleSharp.Io;
    using System;
    using System.IO;

    class FileEntry : IFile
    {
        public FileEntry(String fileName, Stream content)
        {
            Name = fileName;
            Body = content;
            LastModified = DateTime.Now;
        }

        public Stream Body { get; }

        public Boolean IsClosed => Body.CanRead == false;

        public DateTime LastModified { get; }

        public Int32 Length => (Int32)Body.Length;

        public String Name { get; }

        public String Type => MimeTypeNames.FromExtension(Path.GetExtension(Name));

        public void Close()
        {
            Body.Close();
        }

        public void Dispose()
        {
            Body.Dispose();
        }

        public IBlob Slice(Int32 start = 0, Int32 end = Int32.MaxValue, String contentType = null)
        {
            var ms = new MemoryStream();
            Body.Position = start;
            var buffer = new Byte[Math.Max(0, Math.Min(end, Body.Length) - start)];
            Body.Read(buffer, 0, buffer.Length);
            ms.Write(buffer, 0, buffer.Length);
            Body.Position = 0;
            return new FileEntry(Name, ms);
        }
    }
}
