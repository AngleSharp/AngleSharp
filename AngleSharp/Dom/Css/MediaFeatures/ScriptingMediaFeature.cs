namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class ScriptingMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter TheConverter = Map.ScriptingStates.ToConverter();

        #endregion

        #region ctor

        public ScriptingMediaFeature()
            : base(FeatureNames.Scripting)
        {
        }

        #endregion

        #region Internal Properties

        internal override IValueConverter Converter
        {
            // Default: ScriptingState.None
            get { return TheConverter; }
        }

        #endregion

        #region Methods

        public override Boolean Validate(RenderDevice device)
        {
            var state = ScriptingState.None;
            var options = device.Options;
            var available = ScriptingState.None;

            if (options != null && options.IsScripting())
            {
                available = device.DeviceType == RenderDevice.Kind.Screen ? ScriptingState.Enabled : ScriptingState.InitialOnly;
            }

            return state == available;
        }

        #endregion
    }
}
