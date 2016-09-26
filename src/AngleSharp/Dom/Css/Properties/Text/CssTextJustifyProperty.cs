namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	sealed class CssTextJustifyProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.TextJustifyConverter;

		#endregion

		#region ctor

		public CssTextJustifyProperty()
			: base(PropertyNames.TextJustify)
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
