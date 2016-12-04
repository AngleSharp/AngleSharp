namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// Base class for all nth-child (or related) selectors.
    /// </summary>
    abstract class ChildSelector : ISelector
    {
        #region Fields

        private readonly String _name;
        private readonly Int32 _step;
        private readonly Int32 _offset;
        private readonly ISelector _kind;

        #endregion

        #region ctor

        public ChildSelector(String name, Int32 step, Int32 offset, ISelector kind)
        {
            _name = name;
            _step = step;
            _offset = offset;
            _kind = kind;
        }

        #endregion

        #region Properties

        public Priority Specificity
        {
            get { return Priority.OneClass; }
        }

        public String Text
        {
            get
            {
                var a = _step.ToString();
                var b = String.Empty;
                var c = String.Empty;

                if (_offset > 0)
                {
                    b = "+";
                    c = (+_offset).ToString();
                }
                else if (_offset < 0)
                {
                    b = "-";
                    c = (-_offset).ToString();
                }

                return String.Format(":{0}({1}n{2}{3})", _name, a, b, c);
            }
        }

        public String Name
        {
            get { return _name; }
        }

        public Int32 Step
        {
            get { return _step; }
        }

        public Int32 Offset
        {
            get { return _offset; }
        }

        public ISelector Kind
        {
            get { return _kind; }
        }

        #endregion

        #region Methods

        public abstract Boolean Match(IElement element, IElement scope);

        public void Accept(ISelectorVisitor visitor)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
