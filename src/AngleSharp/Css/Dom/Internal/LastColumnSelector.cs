namespace AngleSharp.Css.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom;
    using System;

    /// <summary>
    /// The nth-last-column selector.
    /// </summary>
    sealed class LastColumnSelector : ChildSelector, ISelector
    {
        public LastColumnSelector(Int32 step, Int32 offset, ISelector kind)
            : base(PseudoClassNames.NthLastColumn, step, offset, kind)
        {
        }

        public Boolean Match(IElement element, IElement scope)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                var n = Math.Sign(Step);
                var k = 0;

                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i] is IHtmlTableCellElement child)
                    {
                        var span = child.ColumnSpan;
                        k += span;

                        if (child == element)
                        {
                            var diff = k - Offset;

                            for (var index = 0; index < span; index++, diff--)
                            {
                                if (diff == 0 || (Math.Sign(diff) == n && diff % Step == 0))
                                {
                                    return true;
                                }
                            }

                            return false;
                        }
                    }
                }
            }

            return false;
        }
    }
}
