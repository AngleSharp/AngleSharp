using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	/// <summary>
	/// Information can be found on MDN:
	/// https://developer.mozilla.org/pl/docs/Web/CSS/text-align-last
	/// Gets the selected text-align-last.
	/// </summary>
	sealed class CssTextAlignLastProperty : CssProperty
	{
		#region Fields

		static readonly IValueConverter StyleConverter = Converters.TextAlignLastConverter;

		#endregion

		#region ctor

		public CssTextAlignLastProperty()
			: base(PropertyNames.TextAlignLast)
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
