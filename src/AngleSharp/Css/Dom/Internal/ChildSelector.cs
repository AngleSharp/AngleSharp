namespace AngleSharp.Css.Dom
{
    using System;

    /// <summary>
    /// Base class for all nth-child (or related) selectors.
    /// </summary>
    abstract class ChildSelector
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

        public Priority Specificity => Priority.OneClass;

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

        public String Name => _name;

        public Int32 Step => _step;

        public Int32 Offset => _offset;

        public ISelector Kind => _kind;

        #endregion

        #region Methods

        public void Accept(ISelectorVisitor visitor)
        {
            visitor.Child(_name, _step, _offset, _kind);
        }

        #endregion
    }
}
