namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents the keyframe selector.
    /// </summary>
    sealed class KeyframeSelector : IKeyframeSelector
    {
        #region Fields

        private readonly List<String> _stops;

        #endregion

        #region ctor

        public KeyframeSelector(IEnumerable<String> stops)
        {
            _stops = new List<String>(stops);
        }

        #endregion

        #region Properties

        public IEnumerable<String> Stops
        {
            get { return _stops; }
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_stops.Count > 0)
            {
                writer.Write(_stops[0]);

                for (var i = 1; i < _stops.Count; i++)
                {
                    writer.Write(", ");
                    writer.Write(_stops[i]);
                }
            }
        }

        #endregion
    }
}
