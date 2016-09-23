namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	internal class CssStrokeMiterlimitProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.StrokeMiterlimitConverter;

		#endregion

		#region ctor

		public CssStrokeMiterlimitProperty()
			: base(PropertyNames.StrokeMiterlimit, PropertyFlags.Animatable)
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
