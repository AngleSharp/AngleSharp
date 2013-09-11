using System;

namespace AngleSharp.DTD
{
    sealed class ElementNameDeclarationEntry : ElementQuantifiedDeclarationEntry
    {
        #region Properties

        public String Name
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public override Boolean Check(NodeInspector inspector)
        {
            if (inspector.Current.NodeName == Name)
            {
                inspector.Index++;
                return true;
            }

            return false;
        }

        #endregion
    }
}
