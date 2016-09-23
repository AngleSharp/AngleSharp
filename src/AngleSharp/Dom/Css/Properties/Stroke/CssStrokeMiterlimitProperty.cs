namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-miterlimit
	/// Gets the value that should be used for the stroke-miterlimit.
	/// </summary>
	sealed class CssStrokeMiterlimitProperty : CssProperty
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
