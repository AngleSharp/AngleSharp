using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents a set of possible options that might be applicable
    /// (or not) for a specific document generation.
    /// </summary>
    public class DocumentOptions
    {
        static readonly DocumentOptions _defaultOptions;

        static DocumentOptions()
        {
            _defaultOptions = new DocumentOptions();
        }

        /// <summary>
        /// Gets the default options. Changing the properties of the
        /// default options will change the default parameters.
        /// </summary>
        public static DocumentOptions Default
        {
            get { return _defaultOptions; }
        }

        /// <summary>
        /// Gets or sets the current scripting mode. The default option
        /// activates scripting if an engine is available, otherwise it
        /// is disabled.
        /// </summary>
        public State Scripting
        {
            get;
            set;
        }

        #region Internal

        internal Boolean IsScripting
        {
            get
            {
                if (Scripting == State.Default || Scripting == State.Enabled)
                    return false;//TODO return true if a scripting engine is registered

                return false;
            }
        }

        #endregion

        #region Enumerations

        /// <summary>
        /// The possible value states.
        /// </summary>
        public enum State
        {
            /// <summary>
            /// The default (initial) state.
            /// </summary>
            Default,
            /// <summary>
            /// The option should be enabled.
            /// </summary>
            Enabled,
            /// <summary>
            /// The option should be disabled.
            /// </summary>
            Disabled
        }

        #endregion
    }
}
