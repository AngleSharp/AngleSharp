using System;

namespace AngleSharp.DTD
{
    sealed class ElementEmptyDeclarationEntry : ElementDeclarationEntry
    {
        public ElementEmptyDeclarationEntry()
        {
            _type = ElementContentType.Empty;
        }

        public override Boolean Check(NodeInspector inspector)
        {
            return inspector.Length == 0;
        }
    }
}
