namespace AngleSharp.Dom.Events
{
    using AngleSharp.Attributes;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the event arguments for a composed event.
    /// </summary>
    [DomName("CompositionEvent")]
    public class CompositionEvent : UiEvent
    {
        #region ctor

        /// <summary>
        /// Creates a new event.
        /// </summary>
        public CompositionEvent()
        {
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="bubbles">If the event is bubbling.</param>
        /// <param name="cancelable">If the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="data">Sets the data to carry.</param>
        public CompositionEvent(String type, Boolean bubbles, Boolean cancelable, IWindow view, String data)
        {
            Init(type, bubbles, cancelable, view, data);
        }

        /// <summary>
        /// Creates a new event and initializes it.
        /// </summary>
        /// <param name="type">The type of the event.</param>
        /// <param name="eventInitDict">
        /// An optional dictionary with optional keys such as
        /// bubbles (boolean) and cancelable (boolean).
        /// </param>
        [DomConstructor]
        public CompositionEvent(String type, IDictionary<String, Object> eventInitDict = null)
            : base(type, eventInitDict)
        {
            Data = (eventInitDict.TryGet("data") ?? String.Empty).ToString();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the associated data.
        /// </summary>
        [DomName("data")]
        public String Data
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes the composition event.
        /// </summary>
        /// <param name="type">The type of event.</param>
        /// <param name="bubbles">Determines if the event bubbles.</param>
        /// <param name="cancelable">Determines if the event is cancelable.</param>
        /// <param name="view">Sets the associated view for the UI event.</param>
        /// <param name="data">Sets the data to carry.</param>
        [DomName("initCompositionEvent")]
        public void Init(String type, Boolean bubbles, Boolean cancelable, IWindow view, String data)
        {
            Init(type, bubbles, cancelable, view, 0);
            Data = data;
        }

        #endregion
    }
}
