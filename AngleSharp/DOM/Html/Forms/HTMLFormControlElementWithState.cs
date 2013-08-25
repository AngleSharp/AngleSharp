using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for all HTML form controls that contain a state.
    /// </summary>
    public abstract class HTMLFormControlElementWithState : HTMLFormControlElement
    {
        #region ctor

        internal HTMLFormControlElementWithState()
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
        /// This method is not implemented yet.
        /// </summary>
        /// <returns>The current state.</returns>
        internal FormControlState SaveControlState()
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Resets the form control state to the given state.
        /// </summary>
        /// <param name="state">The desired state.</param>
        internal void RestoreFormControlState(FormControlState state)
        {
        }

        #endregion
    }
}
