namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-dasharray
	/// Gets the value that should be used for the stroke-dasharray.
	/// </summary>
	sealed class CssStrokeDasharrayProperty : CssProperty
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
