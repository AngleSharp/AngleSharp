using System;

namespace AngleSharp
{
    /// <summary>
    /// Represents a set of possible options that might be applicable
    /// (or not) for a specific document generation.
    /// </summary>
    public class DocumentOptions
    {
        #region Default

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

        #endregion

        #region Options

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

        /// <summary>
        /// Gets or sets the current CSS mode. The default option will
        /// parse CSS stylesheets and inline-definitions, otherwise no
        /// CSS will be parsed (or downloaded if possible).
        /// </summary>
        public State Styling
        {
            get;
            set;
        }

        #endregion

        #region Generators

        internal HtmlOptions ForHtml()
        {
            return new HtmlOptions()
            {
                IsScripting = (Scripting == State.Default || Scripting == State.Enabled) && false,//TODO deactivated for the moment - last criteria: HasScriptingEngine?
                IsStyling = Styling != State.Disabled
            };
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
