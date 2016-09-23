namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	internal class CssStrokeLinejoinProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.StrokeLinejoinConverter;

		#endregion

		#region ctor

		public CssStrokeLinejoinProperty()
			: base(PropertyNames.StrokeLinejoin, PropertyFlags.Animatable)
		{
		}

		#endregion

		#region Properties

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
