namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The options of assignedNodes() and assignedElements().
    /// </summary>
    public sealed class AssignedNodesOptions
    {
        /// <summary>
        /// Gets or sets if slots are replaced by what they show: their assigned nodes, or else
        /// their fallback content.
        /// </summary>
        [DomName("flatten")]
        public Boolean Flatten { get; set; }
    }

    /// <summary>
    /// What the slots of a shadow tree show (HTML 4.12.4).
    /// </summary>
    [DomExposed("HTMLSlotElement")]
    public static class HtmlSlotElementExtensions
    {
        /// <summary>
        /// Gets the nodes of the host assigned to the slot.
        /// </summary>
        /// <param name="slot">The slot.</param>
        /// <param name="options">If the result is flattened.</param>
        /// <returns>The assigned nodes.</returns>
        [DomName("assignedNodes")]
        public static INode[] AssignedNodes(this IHtmlSlotElement slot, AssignedNodesOptions? options = null) =>
            (options?.Flatten == true ? Flatten(slot) : GetAssigned(slot)).ToArray();

        /// <summary>
        /// Gets the elements of the host assigned to the slot.
        /// </summary>
        /// <param name="slot">The slot.</param>
        /// <param name="options">If the result is flattened.</param>
        /// <returns>The assigned elements.</returns>
        [DomName("assignedElements")]
        public static IElement[] AssignedElements(this IHtmlSlotElement slot, AssignedNodesOptions? options = null) =>
            slot.AssignedNodes(options).OfType<IElement>().ToArray();

        private static IEnumerable<INode> GetAssigned(IHtmlSlotElement slot)
        {
            var host = slot.GetAncestor<IShadowRoot>()?.Host;

            if (host is null)
            {
                yield break;
            }

            foreach (var node in host.ChildNodes)
            {
                var assigned = node switch
                {
                    IText text => text.AssignedSlot,
                    IElement element => element.AssignedSlot,
                    _ => null,
                };

                if (ReferenceEquals(assigned, slot))
                {
                    yield return node;
                }
            }
        }

        private static IEnumerable<INode> Flatten(IHtmlSlotElement slot)
        {
            var assigned = GetAssigned(slot).ToList();
            var nodes = assigned.Count > 0 ? (IEnumerable<INode>)assigned : slot.ChildNodes;

            foreach (var node in nodes)
            {
                if (node is IHtmlSlotElement inner && (assigned.Count > 0 || inner.GetAncestor<IShadowRoot>() is not null))
                {
                    foreach (var flattened in Flatten(inner))
                    {
                        yield return flattened;
                    }
                }
                else
                {
                    yield return node;
                }
            }
        }
    }
}
