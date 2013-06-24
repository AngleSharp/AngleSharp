using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for all HTML form controls that contain a state.
    /// </summary>
    public abstract class HTMLFormControlElementWithState : HTMLFormControlElement
    {
        internal HTMLFormControlElementWithState()
        {
            CanContainRangeEndpoint = false;
        }

        /// <summary>
        /// Gets the status if the element can contain a range endpoint.
        /// </summary>
        public Boolean CanContainRangeEndpoint
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the status if the element should save and restore the control state.
        /// </summary>
        public Boolean ShouldSaveAndRestoreFormControlState
        {
            get;
            private set;
        }

        /// <summary>
        /// This method is not implemented yet.
        /// </summary>
        /// <returns>The current state.</returns>
        public FormControlState SaveControlState()
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Resets the form control state to the given state.
        /// </summary>
        /// <param name="state">The desired state.</param>
        public void RestoreFormControlState(FormControlState state)
        {
        }
    }
}
