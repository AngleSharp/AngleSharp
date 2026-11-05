namespace AngleSharp.Html.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html.Dom.Events;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    /// <summary>
    /// Members of HTML elements added with the popover API.
    /// </summary>
    [DomExposed("HTMLElement")]
    public static class HtmlPopoverMemberExtensions
    {
        private static readonly ConditionalWeakTable<Document, PopoverStack> _stacks = new();
        private static readonly ConditionalWeakTable<HtmlElement, PopoverState> _states = new();
        private static readonly ConditionalWeakTable<IHtmlElement, PopoverTargetReference> _targetReferences = new();

        /// <summary>
        /// Gets the normalized popover attribute value.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The popover mode, or null if the attribute is absent.</returns>
        [DomName("popover")]
        [DomAccessor(Accessors.Getter)]
        public static String? GetPopover(this IHtmlElement element)
        {
            var value = element.GetAttribute(AttributeNames.Popover);

            if (value is null)
            {
                return null;
            }

            if (String.IsNullOrEmpty(value) || String.Equals(value, "auto", StringComparison.OrdinalIgnoreCase))
            {
                return "auto";
            }

            if (String.Equals(value, "hint", StringComparison.OrdinalIgnoreCase))
            {
                return "hint";
            }

            return "manual";
        }

        /// <summary>
        /// Sets or removes the popover attribute.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <param name="value">The value to set, or null to remove the attribute.</param>
        [DomName("popover")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPopover(this IHtmlElement element, String? value)
        {
            if (value is null)
            {
                element.RemoveAttribute(AttributeNames.Popover);
            }
            else
            {
                element.SetAttribute(AttributeNames.Popover, value);
            }
        }

        /// <summary>
        /// Shows the popover element.
        /// </summary>
        /// <param name="element">The popover element.</param>
        [DomName("showPopover")]
        public static void ShowPopover(this IHtmlElement element) => Show(element);

        /// <summary>
        /// Hides the popover element.
        /// </summary>
        /// <param name="element">The popover element.</param>
        [DomName("hidePopover")]
        public static void HidePopover(this IHtmlElement element) => Hide(element);

        /// <summary>
        /// Toggles the popover element and returns whether it is open.
        /// </summary>
        /// <param name="element">The popover element.</param>
        /// <param name="force">The desired open state, if specified.</param>
        /// <returns>True if the popover is open.</returns>
        [DomName("togglePopover")]
        public static Boolean TogglePopover(this IHtmlElement element, Boolean? force = null)
        {
            EnsurePopover(element);
            var state = _states.GetOrCreateValue((HtmlElement)element);

            if (force == state.IsOpen)
            {
                return state.IsOpen;
            }

            if (state.IsOpen)
            {
                Hide(element);
            }
            else
            {
                Show(element);
            }

            return state.IsOpen;
        }

        /// <summary>
        /// Gets whether the popover is currently open.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>True if the element is an open popover.</returns>
        public static Boolean IsPopoverOpen(this IHtmlElement element) =>
            element is HtmlElement html && _states.TryGetValue(html, out var state) && state.IsOpen;

        internal static void RemoveDisconnectedPopovers(INode root)
        {
            if (root.Owner is not Document owner || !_stacks.TryGetValue(owner, out var stack))
            {
                return;
            }

            for (var i = stack.Items.Count - 1; i >= 0; i--)
            {
                var popover = stack.Items[i];

                if (root.Contains(popover) &&
                    !popover.IsConnected() &&
                    _states.TryGetValue(popover, out var state) &&
                    !state.IsTransitioning)
                {
                    Hide(popover, false);
                }
            }
        }

        internal static void PopoverAttributeChanged(HtmlElement element)
        {
            if (!element.HasAttribute(AttributeNames.Popover) &&
                _states.TryGetValue(element, out var state) &&
                state.IsOpen &&
                !state.IsTransitioning)
            {
                Hide(element, false, false);
            }
        }

        private static void Show(IHtmlElement element)
        {
            EnsurePopover(element);
            var html = (HtmlElement)element;

            if (!element.IsConnected())
            {
                throw new DomException(DomError.InvalidState);
            }

            var state = _states.GetOrCreateValue(html);
            var stack = _stacks.GetOrCreateValue(html.Owner);

            if (state.IsOpen || state.IsTransitioning || stack.IsShowingPopover || stack.HidingPopoverNestingCount != 0)
            {
                throw new DomException(DomError.InvalidState);
            }

            state.IsTransitioning = true;
            stack.IsShowingPopover = true;
            var shouldQueueToggle = false;

            try
            {
                var beforeToggle = new ToggleEvent(EventNames.BeforeToggle, false, true, "closed", "open");

                if (!element.Dispatch(beforeToggle))
                {
                    return;
                }

                if (!element.HasAttribute(AttributeNames.Popover) || !element.IsConnected())
                {
                    return;
                }

                if (GetPopover(element) == "auto")
                {
                    for (var i = stack.Items.Count - 1; i >= 0; i--)
                    {
                        var current = stack.Items[i];

                        if (GetPopover(current) == "auto" && !IsAncestor(current, element))
                        {
                            Hide(current, false);
                        }
                    }
                }

                if (!element.HasAttribute(AttributeNames.Popover) || !element.IsConnected())
                {
                    return;
                }

                state.IsOpen = true;
                stack.Items.Add(html);
                shouldQueueToggle = true;
            }
            finally
            {
                state.IsTransitioning = false;
                stack.IsShowingPopover = false;
            }

            if (shouldQueueToggle)
            {
                QueueToggle(html, state, "closed");
            }
        }

        private static void Hide(IHtmlElement element) => Hide(element, true);

        private static void Hide(IHtmlElement element, Boolean requireOpen) => Hide(element, requireOpen, true);

        private static void Hide(IHtmlElement element, Boolean requireOpen, Boolean requirePopover)
        {
            if (requirePopover)
            {
                EnsurePopover(element);
            }

            if (element is not HtmlElement html || !_states.TryGetValue(html, out var state) || !state.IsOpen)
            {
                if (requireOpen)
                {
                    throw new DomException(DomError.InvalidState);
                }

                return;
            }

            if (state.IsTransitioning)
            {
                throw new DomException(DomError.InvalidState);
            }

            _stacks.TryGetValue(html.Owner, out var stack);
            state.IsTransitioning = true;

            if (stack is not null)
            {
                stack.HidingPopoverNestingCount++;
            }

            var shouldQueueToggle = false;

            try
            {
                element.Dispatch(new ToggleEvent(EventNames.BeforeToggle, false, false, "open", "closed"));
                state.IsOpen = false;

                if (stack is not null)
                {
                    stack.Items.Remove(html);
                }

                shouldQueueToggle = true;
            }
            finally
            {
                state.IsTransitioning = false;
                if (stack is not null)
                {
                    stack.HidingPopoverNestingCount--;
                }
            }

            if (shouldQueueToggle)
            {
                QueueToggle(html, state, "open");
            }
        }

        private static void EnsurePopover(IHtmlElement element)
        {
            if (element is not HtmlElement || !element.HasAttribute(AttributeNames.Popover))
            {
                throw new DomException(DomError.InvalidState);
            }
        }

        private static Boolean IsAncestor(IHtmlElement ancestor, IHtmlElement element)
        {
            for (var parent = element.ParentElement; parent is not null; parent = parent.ParentElement)
            {
                if (Object.ReferenceEquals(parent, ancestor))
                {
                    return true;
                }
            }

            return false;
        }

        private static void QueueToggle(HtmlElement element, PopoverState state, String oldState)
        {
            if (!state.ToggleQueued)
            {
                state.ToggleQueued = true;
                state.PendingOldState = oldState;

                element.Owner.QueueTask(() =>
                {
                    var pendingOldState = state.PendingOldState!;
                    var pendingNewState = state.IsOpen ? "open" : "closed";
                    state.ToggleQueued = false;
                    state.PendingOldState = null;
                    element.Dispatch(new ToggleEvent(EventNames.Toggle, false, false, pendingOldState, pendingNewState));
                });
            }
        }

        internal static IHtmlElement? GetTargetElement(IHtmlElement invoker)
        {
            if (_targetReferences.TryGetValue(invoker, out var reference))
            {
                return reference.Target;
            }

            var id = invoker.GetAttribute(AttributeNames.PopoverTarget);
            return id is null ? null : (invoker.Owner as IDocument)?.GetElementById(id) as IHtmlElement;
        }

        internal static void SetTargetElement(IHtmlElement invoker, IHtmlElement? target)
        {
            _targetReferences.Remove(invoker);

            if (target is null)
            {
                invoker.RemoveAttribute(AttributeNames.PopoverTarget);
            }
            else
            {
                _targetReferences.Add(invoker, new PopoverTargetReference(target));

                if (!String.IsNullOrEmpty(target.Id))
                {
                    invoker.SetAttribute(AttributeNames.PopoverTarget, target.Id);
                }
                else
                {
                    invoker.RemoveAttribute(AttributeNames.PopoverTarget);
                }
            }
        }

        internal static String GetTargetAction(IHtmlElement invoker)
        {
            var action = invoker.GetAttribute(AttributeNames.PopoverTargetAction);

            if (String.Equals(action, "show", StringComparison.OrdinalIgnoreCase))
            {
                return "show";
            }

            if (String.Equals(action, "hide", StringComparison.OrdinalIgnoreCase))
            {
                return "hide";
            }

            return "toggle";
        }

        internal static void SetTargetAction(IHtmlElement invoker, String value) =>
            invoker.SetAttribute(AttributeNames.PopoverTargetAction, value);

        private sealed class PopoverStack
        {
            public readonly List<HtmlElement> Items = new();
            public Boolean IsShowingPopover;
            public Int32 HidingPopoverNestingCount;
        }

        private sealed class PopoverState
        {
            public Boolean IsOpen;
            public Boolean IsTransitioning;
            public Boolean ToggleQueued;
            public String? PendingOldState;
        }

        private sealed class PopoverTargetReference
        {
            public PopoverTargetReference(IHtmlElement target) => Target = target;

            public IHtmlElement Target { get; }
        }
    }

    /// <summary>
    /// Popover invoker members for buttons.
    /// </summary>
    [DomExposed("HTMLButtonElement")]
    public static class HtmlButtonPopoverMemberExtensions
    {
        /// <summary>
        /// Gets the popover target element of a button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The target element, if any.</returns>
        [DomName("popoverTargetElement")]
        [DomAccessor(Accessors.Getter)]
        public static IHtmlElement? GetPopoverTargetElement(this IHtmlButtonElement button) =>
            HtmlPopoverMemberExtensions.GetTargetElement(button);

        /// <summary>
        /// Sets the popover target element of a button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="target">The target element, or null to clear it.</param>
        [DomName("popoverTargetElement")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPopoverTargetElement(this IHtmlButtonElement button, IHtmlElement? target) =>
            HtmlPopoverMemberExtensions.SetTargetElement(button, target);

        /// <summary>
        /// Gets the popover target action of a button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <returns>The target action.</returns>
        [DomName("popoverTargetAction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPopoverTargetAction(this IHtmlButtonElement button) =>
            HtmlPopoverMemberExtensions.GetTargetAction(button);

        /// <summary>
        /// Sets the popover target action of a button.
        /// </summary>
        /// <param name="button">The button.</param>
        /// <param name="value">The action to set.</param>
        [DomName("popoverTargetAction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPopoverTargetAction(this IHtmlButtonElement button, String value) =>
            HtmlPopoverMemberExtensions.SetTargetAction(button, value);
    }

    /// <summary>
    /// Popover invoker members for inputs.
    /// </summary>
    [DomExposed("HTMLInputElement")]
    public static class HtmlInputPopoverMemberExtensions
    {
        /// <summary>
        /// Gets the popover target element of an input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The target element, if any.</returns>
        [DomName("popoverTargetElement")]
        [DomAccessor(Accessors.Getter)]
        public static IHtmlElement? GetPopoverTargetElement(this IHtmlInputElement input) =>
            HtmlPopoverMemberExtensions.GetTargetElement(input);

        /// <summary>
        /// Sets the popover target element of an input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="target">The target element, or null to clear it.</param>
        [DomName("popoverTargetElement")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPopoverTargetElement(this IHtmlInputElement input, IHtmlElement? target) =>
            HtmlPopoverMemberExtensions.SetTargetElement(input, target);

        /// <summary>
        /// Gets the popover target action of an input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>The target action.</returns>
        [DomName("popoverTargetAction")]
        [DomAccessor(Accessors.Getter)]
        public static String GetPopoverTargetAction(this IHtmlInputElement input) =>
            HtmlPopoverMemberExtensions.GetTargetAction(input);

        /// <summary>
        /// Sets the popover target action of an input.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <param name="value">The action to set.</param>
        [DomName("popoverTargetAction")]
        [DomAccessor(Accessors.Setter)]
        public static void SetPopoverTargetAction(this IHtmlInputElement input, String value) =>
            HtmlPopoverMemberExtensions.SetTargetAction(input, value);
    }
}
