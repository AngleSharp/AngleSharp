namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.IO;

    /// <summary>
    /// Represents a feature expression within a media query.
    /// </summary>
    abstract class MediaFeature : CssNode, IMediaFeature
    {
        #region Fields

        readonly Boolean _min;
        readonly Boolean _max;
        readonly String _name;
        CssValue _value;

        #endregion

        #region ctor

        internal MediaFeature(String name)
        {
            _name = name;
            _min = name.StartsWith("min-");
            _max = name.StartsWith("max-");
        }

        #endregion

        #region Properties

        public String Name
        {
            get { return _name; }
        }

        public Boolean IsMinimum
        {
            get { return _min; }
        }

        public Boolean IsMaximum
        {
            get { return _max; }
        }

        public String Value
        {
            get { return HasValue ? _value.CssText : String.Empty; }
        }

        public Boolean HasValue
        {
            get { return _value != null && _value.Count > 0; }
        }

        #endregion

        #region Internal Properties

        internal abstract IValueConverter Converter
        {
            get;
        }

        #endregion

        #region Methods

        internal Boolean TrySetValue(CssValue value)
        {
            var result = false;

            if (value == null)
            {
                result = !IsMinimum && !IsMaximum && Converter.HasDefault();
            }
            else
            {
                result = Converter.Convert(value) != null;
            }

            if (result)
            {
                _value = value;
            }

            return result;
        }

        public abstract Boolean Validate(RenderDevice device);

        #endregion

        #region String Representation

        public override void ToCss(TextWriter writer, IStyleFormatter formatter)
        {
            var value = HasValue ? Value : null;
            writer.Write(formatter.Constraint(_name, value));
        }

        #endregion
    }
}
