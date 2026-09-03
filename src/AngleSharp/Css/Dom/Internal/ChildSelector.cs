namespace AngleSharp.Css.Dom
{
    using System;
    using System.Linq;

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

        public Priority Specificity
        {
            get
            {
                var specificity = Priority.OneClass;

                if (IncludeParameterInSpecificity)
                {
                    specificity += Kind is ListSelector list
                        ? list.Max(x => x.Specificity)
                        : Kind.Specificity;
                }

                return specificity;
            }
        }

        protected virtual Boolean IncludeParameterInSpecificity => false;

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

                // The of-clause decides which siblings are counted at all, so a serialization
                // without it names a different selector: one that counts every sibling, and one
                // whose specificity no longer carries the inner selector.
                var d = ReferenceEquals(_kind, AllSelector.Instance)
                    ? String.Empty
                    : String.Concat(" of ", _kind.Text);

                return String.Format(":{0}({1}n{2}{3}{4})", _name, a, b, c, d);
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
