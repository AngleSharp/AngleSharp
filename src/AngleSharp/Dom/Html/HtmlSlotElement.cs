namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an HTML slot element.
    /// </summary>
    sealed class HtmlSlotElement : HtmlElement, IHtmlSlotElement
    {
        #region ctor

        public HtmlSlotElement(Document owner, String prefix = null)
            : base(owner, TagNames.Slot, prefix)
        {
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return this.GetOwnAttribute(AttributeNames.Name); }
            set { this.SetOwnAttribute(AttributeNames.Name, value); }
        }

        #endregion

        #region Methods

        public IEnumerable<INode> GetDistributedNodes()
        {
            var host = this.GetAncestor<IShadowRoot>()?.Host;

            if (host != null)
            {
                var list = new List<INode>();

                foreach (var node in host.ChildNodes)
                {
                    if (Object.ReferenceEquals(GetAssignedSlot(node), this))
                    {
                        var otherSlot = node as HtmlSlotElement;

                        if (otherSlot != null)
                        {
                            list.AddRange(otherSlot.GetDistributedNodes());
                        }
                        else
                        {
                            list.Add(node);
                        }
                    }
                }

                return list;
            }

            return Enumerable.Empty<INode>();
        }

        #endregion

        #region Helpers

        private static IElement GetAssignedSlot(INode node)
        {
            switch (node.NodeType)
            {
                case NodeType.Text:
                    return ((IText)node).AssignedSlot;
                case NodeType.Element:
                    return ((IElement)node).AssignedSlot;
                default:
                    return default(IElement);
            }
        }

        #endregion
    }
}
