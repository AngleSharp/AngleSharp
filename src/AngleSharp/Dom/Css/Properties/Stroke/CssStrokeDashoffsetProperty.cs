namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	internal class CssStrokeDashoffsetProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.LengthOrPercentConverter;

		#endregion

		#region ctor

		public CssStrokeDashoffsetProperty()
			: base(PropertyNames.StrokeDashoffset, PropertyFlags.Animatable)
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
