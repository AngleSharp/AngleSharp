namespace AngleSharp.Css.Values
{
    using System;

    /// <summary>
    /// Represents a CSS counter.
    /// </summary>
    public sealed class Counter
    {
        #region Fields

        readonly String _identifier;
        readonly String _listStyle;
        readonly String _separator;

        #endregion

        #region ctor

        /// <summary>
        /// Specifies a counter value.
        /// </summary>
        /// <param name="identifier">The identifier of the counter.</param>
        /// <param name="listStyle">The used list style.</param>
        /// <param name="separator">The separator of the counter.</param>
        public Counter(String identifier, String listStyle, String separator)
        {
            _identifier = identifier;
            _listStyle = listStyle;
            _separator = separator;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the identifier of the counter.
        /// </summary>
        public String CounterIdentifier
        {
            get { return _identifier; }
        }

        /// <summary>
        /// Gets the style of the counter.
        /// </summary>
        public String ListStyle
        {
            get { return _listStyle; }
        }

        /// <summary>
        /// Gets the defined separator of the counter.
        /// </summary>
        public String DefinedSeparator
        {
            get { return _separator; }
        }

        #endregion
    }
}
