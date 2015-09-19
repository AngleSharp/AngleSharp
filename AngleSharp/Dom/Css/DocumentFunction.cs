namespace AngleSharp.Dom.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents a feature expression within a media query.
    /// </summary>
    abstract class DocumentFunction : IDocumentFunction
    {
        #region Fields

        readonly String _name;
        readonly String _data;

        #endregion

        #region ctor

        internal DocumentFunction(String name, String data)
        {
            _name = name;
            _data = data;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the name of the function.
        /// </summary>
        public String Name
        {
            get { return _name; }
        }

        /// <summary>
        /// Gets the function's argument.
        /// </summary>
        public String Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets the contained CSS nodes.
        /// </summary>
        public IEnumerable<ICssNode> Children
        {
            get { return Enumerable.Empty<ICssNode>(); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Evaluates the function for the provided URL.
        /// </summary>
        /// <param name="url">The URL to evaluate.</param>
        /// <returns>True if the URL is matched, otherwise false.</returns>
        public abstract Boolean Matches(Url url);

        /// <summary>
        /// Returns the (complete) CSS style representation of the node.
        /// </summary>
        /// <returns>The source code snippet.</returns>
        public String ToCss()
        {
            return String.Concat(_name, "(", _data.CssString(), ")");
        }

        /// <summary>
        /// Returns the serialization of the node guided by the formatter.
        /// </summary>
        /// <param name="formatter">The formatter to use.</param>
        /// <returns>The source code snippet.</returns>
        public String ToCss(IStyleFormatter formatter)
        {
            return ToCss();
        }

        #endregion
    }
}
