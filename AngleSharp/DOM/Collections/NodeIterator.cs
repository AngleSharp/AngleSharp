namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Linq;

    sealed class NodeIterator : INodeIterator
    {
        #region Fields

        readonly INode _root;
        readonly FilterSettings _settings;
        readonly NodeFilter _filter;
        HTMLCollection _iterator;
        INode _reference;
        Boolean _beforeNode;

        #endregion

        #region ctor

        public NodeIterator(INode root, FilterSettings settings, NodeFilter filter)
        {
            _root = root;
            _settings = settings;
            _filter = filter ?? (m => FilterResult.Accept);
            _reference = _root;
            _beforeNode = false;
            _iterator = new HTMLCollection(_root);//TODO Use with Filter Settings
        }

        #endregion

        #region Properties

        public INode Root
        {
            get { return _root; }
        }

        public FilterSettings Settings
        {
            get { return _settings; }
        }

        public NodeFilter Filter
        {
            get { return _filter; }
        }

        public INode Reference
        {
            get { return _reference; }
        }

        public Boolean IsBeforeReference
        {
            get { return _beforeNode; }
        }

        #endregion

        #region Methods

        public INode Next()
        {
            if (!_beforeNode)
            {
                var result = FilterResult.Accept;

                do
                {
                    INode node = null;
                    var match = false;

                    foreach (var item in _iterator)
                    {
                        node = item;

                        if (match == true)
                            break;

                        match = item == _reference;
                    }

                    _reference = node;

                    if (node == null)
                        break;

                    result = _filter(_reference);
                }
                while (result != FilterResult.Accept);
            }
            
            _beforeNode = false;
            return _reference;
        }

        public INode Previous()
        {
            if (_beforeNode)
            {
                var result = FilterResult.Accept;

                do
                {
                    INode node = null;

                    foreach (var item in _iterator)
                    {
                        if (item == _reference)
                            break;

                        node = item;
                    }

                    _reference = node;

                    if (node == null)
                        break;

                    result = _filter(_reference);
                }
                while (result != FilterResult.Accept);
            }

            _beforeNode = true;
            return _reference;
        }

        #endregion
    }
}
