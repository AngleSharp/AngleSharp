namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// A list of selectors, which is the basis for CompoundSelector and
    /// SelectorGroup.
    /// </summary>
    abstract class Selectors : CssNode, IEnumerable<ISelector>
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

        public Priority Specifity
        {
            get 
            {
                var sum = new Priority();

                for (var i = 0; i < _selectors.Count; i++)
                {
                    sum += _selectors[i].Specifity;
                }

                return sum;
            }
        }

        public String Text
        {
            get { return this.ToCss(); }
        }

        public Int32 Length 
        {
            get { return _selectors.Count; } 
        }

        public ISelector this[Int32 index]
        {
            get { return _selectors[index]; }
            set { _selectors[index] = value; }
        }

        #endregion

        #region Methods

        public void Add(ISelector selector)
        {
            _selectors.Add(selector);
        }

        public void Remove(ISelector selector)
        {
            _selectors.Remove(selector);
        }

        #endregion

        #region IEnumerable implementation

        public IEnumerator<ISelector> GetEnumerator()
        {
            return _selectors.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}
