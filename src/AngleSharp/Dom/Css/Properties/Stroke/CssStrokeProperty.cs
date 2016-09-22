namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

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
