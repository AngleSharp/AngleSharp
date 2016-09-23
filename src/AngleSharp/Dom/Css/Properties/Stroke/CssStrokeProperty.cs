namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke
	/// Gets the value that should be used for the stroke-width.
	/// </summary>
	sealed class CssStrokeProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.PaintConverter;

		#endregion

		#region ctor

		internal CssStrokeProperty()
			: base(PropertyNames.Stroke, PropertyFlags.Animatable)
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
