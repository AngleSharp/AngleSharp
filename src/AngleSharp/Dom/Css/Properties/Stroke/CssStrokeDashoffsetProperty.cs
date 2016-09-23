namespace AngleSharp.Dom.Css
{
	using AngleSharp.Css;

	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/en-US/docs/Web/SVG/Attribute/stroke-dashoffset
	/// Gets the value that should be used for the stroke-dashoffset.
	/// </summary>
	sealed class CssStrokeDashoffsetProperty : CssProperty
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
