namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents the keyframe selector.
    /// </summary>
    sealed class KeyframeSelector : CssNode, IKeyframeSelector
    {
        #region Fields

        readonly List<Percent> _stops;

        #endregion

        #region ctor

        public KeyframeSelector(IEnumerable<Percent> stops)
        {
            _stops = new List<Percent>(stops);
        }

        #endregion

        #region Properties

        public IEnumerable<Percent> Stops
        {
            get { return _stops; }
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Methods

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            if (_stops.Count > 0)
            {
                writer.Write(_stops[0].ToString());

                for (var i = 1; i < _stops.Count; i++)
                {
                    writer.Write(", ");
                    writer.Write(_stops[i].ToString());
                }
            }
        }

        #endregion
    }
}
