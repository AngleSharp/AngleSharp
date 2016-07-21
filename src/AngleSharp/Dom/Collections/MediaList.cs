namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Represents a list of media elements.
    /// </summary>
    sealed class MediaList : CssNode, IMediaList
    {
        #region Fields

        readonly CssParser _parser;

        #endregion

        #region ctor

        internal MediaList(CssParser parser)
        {
            _parser = parser;
        }

        #endregion

        #region Index

        public String this[Int32 index]
        {
            get { return Media.GetItemByIndex(index).ToCss(); }
        }

        #endregion

        #region Properties

        public IEnumerable<CssMedium> Media
        {
            get { return Children.OfType<CssMedium>(); }
        }

        public Int32 Length
        {
            get { return Media.Count(); }
        }

        public String MediaText
        {
            get { return this.ToCss(); }
            set
            {
                Clear();

                foreach (var medium in _parser.ParseMediaList(value))
                {
                    if (medium == null)
                        throw new DomException(DomError.Syntax);

                    AppendChild(medium);
                }
            }
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var parts = Media.ToArray();

            if (parts.Length > 0)
            {
                parts[0].ToCss(writer, formatter);

                for (var i = 1; i < parts.Length; i++)
                {
                    writer.Write(", ");
                    parts[i].ToCss(writer, formatter);
                }
            }
        }

        public Boolean Validate(RenderDevice device)
        {
            return !Media.Any(m => !m.Validate(device));
        }

        public void Add(String newMedium)
        {
            var medium = _parser.ParseMedium(newMedium);

            if (medium == null)
                throw new DomException(DomError.Syntax);

            AppendChild(medium);
        }

        public void Remove(String oldMedium)
        {
            var medium = _parser.ParseMedium(oldMedium);

            if (medium == null)
                throw new DomException(DomError.Syntax);

            foreach (var item in Media)
            {
                if (item.Equals(medium))
                {
                    RemoveChild(item);
                    return;
                }
            }

            throw new DomException(DomError.NotFound);
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<ICssMedium> GetEnumerator()
        {
            return Media.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
