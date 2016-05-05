using AngleSharp.DOM;
using System;
using System.Collections.Generic;

namespace AngleSharp.DTD
{
    sealed class ElementMixedDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        #region Members

        List<String> _names;

        #endregion

        #region ctor

        public ElementMixedDeclarationEntry()
        {
            _names = new List<String>();
            _type = ElementContentType.Mixed;
        }

        #endregion

        #region Properties

        public List<String> Names
        {
            get { return _names; }
        }

        #endregion

        #region Methods

        public override Boolean Check(NodeInspector inspector)
        {
            if (_quantifier == ElementQuantifier.One && inspector.Length > 1)
                return false;

            for (; inspector.Index < inspector.Length; inspector.Index++)
            {
                var child = inspector.Current;

                if (child is Element && !_names.Contains(child.NodeName))
                    return false;
            }

            return true;
        }

        #endregion
    }
}
