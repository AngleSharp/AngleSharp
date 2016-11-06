namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the base class for all HTML form controls that contain a state.
    /// </summary>
    abstract class HtmlFormControlElementWithState : HtmlFormControlElement
    {
        #region ctor

        public HtmlFormControlElementWithState(Document owner, String name, String prefix, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, flags)
        {
            CanContainRangeEndpoint = false;
        }

        #endregion

        #region Internal Properties

        /// <summary>
        /// Gets the status if the element can contain a range endpoint.
        /// </summary>
        internal Boolean CanContainRangeEndpoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the status if the element should save and restore the control state.
        /// </summary>
        internal Boolean ShouldSaveAndRestoreFormControlState
        {
            get;
            private set;
        }

        #endregion

        #region Internal Methods

        /// <summary>
        /// Saves the current control's state.
        /// </summary>
        /// <returns>The current state.</returns>
        internal abstract FormControlState SaveControlState();

        /// <summary>
        /// Resets the form control state to the given state.
        /// </summary>
        /// <param name="state">The desired state.</param>
        internal abstract void RestoreFormControlState(FormControlState state);

        #endregion
    }
}
