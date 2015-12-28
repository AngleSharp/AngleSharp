namespace AngleSharp.Dom.Collections
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Represents a list of media elements.
    /// </summary>
    [DebuggerStepThrough]
    sealed class MediaList : CssNode, IMediaList
    {
        #region Fields

        readonly CssParser _parser;
        readonly List<CssMedium> _media;

        #endregion

        #region ctor

        internal MediaList(CssParser parser)
        {
            _parser = parser;
            _media = new List<CssMedium>();
            Children = _media;
        }

        #endregion

        #region Properties

        public String this[Int32 index]
        {
            get
            {
                if (index < 0 || index >= _media.Count)
                {
                    return null;
                }

                return _media[index].ToCss();
            }
        }

        public Int32 Length
        {
            get { return _media.Count; }
        }

        public String MediaText
        {
            get { return this.ToCss(); }
            set
            {
                _media.Clear();

                foreach (var medium in _parser.ParseMediaList(value))
                {
                    if (medium == null)
                    {
                        throw new DomException(DomError.Syntax);
                    }

                    _media.Add(medium);
                }
            }
        }

        #endregion

        #region Methods

        public override String ToCss(IStyleFormatter formatter)
        {
            var parts = new String[_media.Count];

            for (var i = 0; i < _media.Count; i++)
            {
                parts[i] = _media[i].ToCss(formatter);
            }

            return String.Join(", ", parts);
        }

        public Boolean Validate(RenderDevice device)
        {
            foreach (var media in _media)
            {
                if (!media.Validate(device))
                {
                    return false;
                }
            }

            return true;
        }

        public void Add(String newMedium)
        {
            var medium = _parser.ParseMedium(newMedium);

            if (medium == null)
            {
                throw new DomException(DomError.Syntax);
            }

            _media.Add(medium);
        }

        public void Remove(String oldMedium)
        {
            var medium = _parser.ParseMedium(oldMedium);

            if (medium == null)
            {
                throw new DomException(DomError.Syntax);
            }

            foreach (var item in _media)
            {
                if (item.Equals(medium))
                {
                    _media.Remove(item);
                    return;
                }
            }

            throw new DomException(DomError.NotFound);
        }

        #endregion

        #region Internal Methods

        public void Add(CssMedium medium)
        {
            _media.Add(medium);
        }

        public void Clear()
        {
            _media.Clear();
        }

        public void Import(MediaList list)
        {
            _media.Clear();
            _media.AddRange(list._media);
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<ICssMedium> GetEnumerator()
        {
            return _media.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
