namespace AngleSharp.Css.MediaFeatures
{
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;

    sealed class ScriptingMediaFeature : MediaFeature
    {
        #region Fields

        static readonly IValueConverter<ScriptingState> Converter = Map.ScriptingStates.ToConverter();
        ScriptingState _state;

        #endregion

        #region ctor

        public ScriptingMediaFeature()
            : base(FeatureNames.Scripting)
        {
            _state = ScriptingState.None;
        }

        #endregion

        #region Properties

        public ScriptingState State
        {
            get { return _state; }
        }

        #endregion

        #region Methods

        protected override Boolean TrySetDefault()
        {
            _state = ScriptingState.None;
            return true;
        }

        protected override Boolean TrySetCustom(ICssValue value)
        {
            return Converter.TryConvert(value, m => _state = m);
        }

        public override Boolean Validate(RenderDevice device)
        {
            var options = device.Options;
            var available = ScriptingState.None;

            if (options != null && options.IsScripting())
                available = device.DeviceType == RenderDevice.Kind.Screen ? ScriptingState.Enabled : ScriptingState.InitialOnly;

            return _state == available;
        }

        #endregion
    }
}
