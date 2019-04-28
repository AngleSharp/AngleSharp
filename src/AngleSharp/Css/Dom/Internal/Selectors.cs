namespace AngleSharp.Css.Dom
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A list of selectors, which is the basis for CompoundSelector and
    /// SelectorGroup.
    /// </summary>
    abstract class Selectors : IEnumerable<ISelector>
    {
        #region Fields

        protected readonly List<ISelector> _selectors;

        #endregion

        #region ctor

        public Selectors()
        {
            _selectors = new List<ISelector>();
        }

        #endregion

        #region Properties

        public Priority Specificity
        {
            get
            {
                var sum = new Priority();

                for (var i = 0; i < _selectors.Count; i++)
                {
                    sum += _selectors[i].Specificity;
                }

                return sum;
            }
        }

        public String Text => Stringify();

        public Int32 Length => _selectors.Count;

        public ISelector this[Int32 index]
        {
            get => _selectors[index];
            set => _selectors[index] = value;
        }

        #endregion

        #region Methods

        protected abstract String Stringify();

        public void Add(ISelector selector) => _selectors.Add(selector);

        public void Remove(ISelector selector) => _selectors.Remove(selector);

        #endregion

        #region IEnumerable implementation

        public IEnumerator<ISelector> GetEnumerator() => _selectors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
