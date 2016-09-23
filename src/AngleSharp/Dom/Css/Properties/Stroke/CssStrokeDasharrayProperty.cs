namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	internal class CssStrokeDasharrayProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.StrokeDasharrayConverter;

		#endregion

		#region ctor

		public CssStrokeDasharrayProperty()
			: base(PropertyNames.StrokeDasharray, PropertyFlags.Animatable | PropertyFlags.Unitless)
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
