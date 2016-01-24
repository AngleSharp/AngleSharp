namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css.Values;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

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

        #region String Representation

        public override String ToCss(IStyleFormatter formatter)
        {
            var stops = new String[_stops.Count];

            for (var i = 0; i < stops.Length; i++)
            {
                stops[i] = _stops[i].ToString();
            }

            return String.Join(", ", stops);
        }

        #endregion
    }
}
