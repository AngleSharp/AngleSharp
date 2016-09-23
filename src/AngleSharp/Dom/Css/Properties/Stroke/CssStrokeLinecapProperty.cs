namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;
	using Extensions;

	internal class CssStrokeLinecapProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.StrokeLinecapConverter.OrDefault(StrokeLinecap.Butt);

		#endregion

		#region ctor

		public CssStrokeLinecapProperty()
			: base(PropertyNames.StrokeLinecap, PropertyFlags.Animatable)
		{
		}

		#endregion

		#region Properties;

		internal override IValueConverter Converter
		{
			get
			{
				return StyleConverter;
			}
		}

		#endregion
	}
}
