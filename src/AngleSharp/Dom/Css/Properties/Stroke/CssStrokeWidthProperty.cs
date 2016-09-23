namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-width
	/// Gets the value that should be used for the stroke-width.
	/// </summary>
	sealed class CssStrokeWidthProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter;

		#endregion

		#region ctor

		internal CssStrokeWidthProperty()
			: base(PropertyNames.StrokeWidth, PropertyFlags.Animatable)
		{
		}

		#endregion

		#region Properties

		internal override IValueConverter Converter
		{
			get { return StyleConverter; }
		}

		#endregion
	}
}
