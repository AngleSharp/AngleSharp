namespace AngleSharp.Core.Tests.Mocks
{
    using System;
    using System.IO;
    using AngleSharp.Dom.Io;
    using AngleSharp.Network;

    class FileEntry : IFile
    {
        readonly String _fileName;
        readonly Stream _content;
        readonly DateTime _modified;

        public FileEntry(String fileName, Stream content)
        {
            _fileName = fileName;
            _content = content;
            _modified = DateTime.Now;
        }

        public Stream Body
        {
            get
            {
                return _content;
            }
        }

        public bool IsClosed
        {
            get
            {
                return _content.CanRead == false;
            }
        }

        public DateTime LastModified
        {
            get
            {
                return _modified;
            }
        }

        public Int32 Length
        {
            get
            {
                return (Int32)_content.Length;
            }
        }

        public String Name
        {
            get
            {
                return _fileName;
            }
        }

        public String Type
        {
            get
            {
                return MimeTypes.FromExtension(Path.GetExtension(_fileName));
            }
        }

        public void Close()
        {
            _content.Close();
        }

        public void Dispose()
        {
            _content.Dispose();
        }

        public IBlob Slice(int start = 0, int end = int.MaxValue, string contentType = null)
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
