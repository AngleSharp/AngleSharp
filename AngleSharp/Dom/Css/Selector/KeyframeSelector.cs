namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Css.Values;
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

        public KeyframeSelector(List<Percent> stops)
        {
            _stops = stops;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets an enumeration over all stops.
        /// </summary>
        public IEnumerable<Percent> Stops
        {
            get { return _stops; }
        }

        /// <summary>
        /// Gets the text representation of the keyframe selector.
        /// </summary>
        public String Text
        {
            get { return Serialize(", "); }
        }

        #endregion

        #region Methods

        public override String GetSource()
        {
            var plain = Serialize(",");
            return Decorate(plain);
        }

        #endregion

        #region Helpers

        String Serialize(String separator)
        {
            var stops = new String[_stops.Count];

            for (int i = 0; i < stops.Length; i++)
                stops[i] = _stops[i].ToString();

            return String.Join(separator, stops);
        }

        #endregion
    }
}
