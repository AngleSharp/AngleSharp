namespace AngleSharp.Core.Tests.Mocks
{
    using AngleSharp.Io.Dom;
    using AngleSharp.Io;
    using System;
    using System.IO;

    class FileEntry : IFile
    {
        private readonly String _fileName;
        private readonly Stream _content;
        private readonly DateTime _modified;

        public FileEntry(String fileName, Stream content)
        {
            _fileName = fileName;
            _content = content;
            _modified = DateTime.Now;
        }

        public Stream Body => _content;

        public Boolean IsClosed => _content.CanRead == false;

        public DateTime LastModified => _modified;

        public Int32 Length => (Int32)_content.Length;

        public String Name => _fileName;

        public String Type => MimeTypeNames.FromExtension(Path.GetExtension(_fileName));

        public void Close()
        {
            _content.Close();
        }

        public void Dispose()
        {
            _content.Dispose();
        }

        public IBlob Slice(Int32 start = 0, Int32 end = Int32.MaxValue, String contentType = null)
        {
            var ms = new MemoryStream();
            _content.Position = start;
            var buffer = new Byte[Math.Max(0, Math.Min(end, _content.Length) - start)];
            _content.Read(buffer, 0, buffer.Length);
            ms.Write(buffer, 0, buffer.Length);
            _content.Position = 0;
            return new FileEntry(_fileName, ms);
        }
    }
}
