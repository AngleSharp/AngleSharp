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

        private Priority? _specificity;

        #endregion

        #region ctor

        public Selectors()
        {
            _selectors = [];
        }

        #endregion

        #region Properties

        // A selector is immutable once parsing has assembled it - Add/Remove below only ever run
        // while a CssSelectorConstructor is still building this list - so the specificity can be
        // computed once and reused. CssStyleRule.TryMatch reads Specificity on every successful
        // match, once per matched rule per element, which made this the same recomputation
        // repeated across an entire cascade.
        public Priority Specificity => _specificity ??= ComputeSpecificity();

        public String Text => Stringify();

        public Int32 Length => _selectors.Count;

        public ISelector this[Int32 index]
        {
            get => _selectors[index];
            set
            {
                _selectors[index] = value;
                Invalidate();
            }
        }

        #endregion

        #region Methods

        protected abstract Priority ComputeSpecificity();

        protected abstract String Stringify();

        public void Add(ISelector selector)
        {
            _selectors.Add(selector);
            Invalidate();
        }

        public void Remove(ISelector selector)
        {
            _selectors.Remove(selector);
            Invalidate();
        }

        // Overridden by ListSelector, which caches a specificity-ordered copy of _selectors and
        // must drop it whenever the underlying list changes.
        protected virtual void Invalidate() => _specificity = null;

        #endregion

        #region IEnumerable implementation

        public IEnumerator<ISelector> GetEnumerator() => _selectors.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        #endregion
    }
}
