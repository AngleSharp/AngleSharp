namespace AngleSharp.DOM
{
	using System;

	[DOM("Text")]
	public interface IText : ICharacterData
	{
		[DOM("splitText")]
		IText Split(Int32 offset);

		[DOM("wholeText")]
		String Text { get; }
	}
}