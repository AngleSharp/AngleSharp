namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a scoped query and wraps a selector, providing
    /// the proper scope element for match operations.
    /// </summary>
    sealed class ScopedSelector : ISelector
    {
        #region Fields

        private readonly IElement _scope;
        private readonly ISelector _selector;

        #endregion

        #region ctor

        public ScopedSelector(IElement scope, ISelector selector)
        {
            _scope = scope;
            _selector = selector;
        }

        #endregion

        #region Properties

        public Priority Specifity
        {
            get { return _selector.Specifity; }
        }

        public String Text
        {
            get { return _selector.Text; }
        }

        #endregion

        #region Methods

        public void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            _selector.ToCss(writer, formatter);
        }

        public Boolean Match(IElement element, IElement scope)
        {
            return _selector.Match(element, _scope);
        }

        #endregion
    }
}