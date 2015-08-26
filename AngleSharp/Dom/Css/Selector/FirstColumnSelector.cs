namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Dom.Html;
    using System;

    /// <summary>
    /// The nth-column selector.
    /// </summary>
    sealed class FirstColumnSelector : ChildSelector
    {
        public FirstColumnSelector()
            : base(PseudoClassNames.NthColumn)
        {
        }

        public override Boolean Match(IElement element)
        {
            var parent = element.ParentElement;

            if (parent == null)
                return false;

            var n = Math.Sign(_step);
            var k = 0;

            for (var i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as IHtmlTableCellElement;

                if (child == null)
                    continue;

                var span = child.ColumnSpan;
                k += span;

                if (child == element)
                {
                    var diff = k - _offset;

                    for (int index = 0; index < span; index++, diff--)
                    {
                        if (diff == 0 || (Math.Sign(diff) == n && diff % _step == 0))
                            return true;
                    }

                    return false;
                }
            }

            return false;
        }
    }
}
