namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Base class for all nth-child (or related) selectors.
    /// </summary>
    abstract class ChildSelector : CssNode, ISelector
    {
        #region Fields

        readonly String _name;
        protected Int32 _step;
        protected Int32 _offset;
        protected ISelector _kind;

        #endregion

        #region ctor

        public ChildSelector(String name)
        {
            _name = name;
        }

        #endregion

        #region Properties

        public Priority Specifity
        {
            get { return Priority.OneClass; }
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        #endregion

        #region Methods

        internal ChildSelector With(Int32 step, Int32 offset, ISelector kind)
        {
            _step = step;
            _offset = offset;
            _kind = kind;
            return this;
        }

        public abstract Boolean Match(IElement element);

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var a = _step.ToString();
            var b = String.Empty;

            if (_offset > 0)
            {
                b = "+" + _offset.ToString();
            }
            else if (_offset < 0)
            {
                b = _offset.ToString();
            }

            writer.Write(":{0}({1}n{2})", _name, a, b);
        }

        #endregion
    }
}
